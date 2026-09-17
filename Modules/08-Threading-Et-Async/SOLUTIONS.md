# 💡 Solutions — Module 8

> ⛔ **À lire après avoir essayé.**
> C'est le module le plus dur du parcours. Si tu as bloqué, c'est normal — tout
> le monde bloque ici.

---

## Exercice A — Le compteur thread-safe

```csharp
public class Compteur
{
    private int _valeur = 0;
    private readonly object _verrou = new object();

    public int Valeur => _valeur;

    public void IncrementerNonSecurise()
    {
        _valeur++;                  // 🔴 race condition garantie
    }

    public void IncrementerSecurise()
    {
        lock (_verrou)
        {
            _valeur++;              // ✅ un seul thread à la fois
        }
    }

    public void Reinitialiser()
    {
        lock (_verrou)
        {
            _valeur = 0;
        }
    }
}
```

### Pourquoi `private readonly`

**`private`** : si le verrou était `public`, n'importe qui pourrait écrire
`lock (monCompteur.Verrou) { /* longue opération */ }` et bloquer ta classe
pendant des secondes. Le verrou est une affaire interne.

**`readonly`** : imagine que le verrou puisse être remplacé en cours de route.
Deux threads verrouilleraient alors **deux objets différents** — et
passeraient tous les deux. La protection s'évanouirait sans le moindre message
d'erreur. `readonly` rend ça impossible dès la compilation.

### Pourquoi `Reinitialiser` a aussi besoin du verrou

C'est **le** point que tout le monde rate.

Un `lock` ne protège pas une *donnée*, il protège un *passage*. Si
`Reinitialiser` écrit dans `_valeur` sans prendre le verrou, il peut le faire
**pendant** qu'un autre thread est en plein milieu de son incrémentation.
Résultat : le `_valeur = 0` est aussitôt écrasé par un `_valeur = 4732` que
l'autre thread avait calculé avant.

> 🧠 **La règle absolue** : *tous* les accès à une donnée partagée — lectures
> **et** écritures — doivent passer par le **même** verrou. Un seul oubli, et
> toute la protection tombe.

---

## Exercice 1 — AdditionnerAsync

```csharp
public static async Task<int> AdditionnerAsync(int a, int b)
{
    await Task.Delay(50);
    return a + b;
}
```

**Les trois marqueurs d'une méthode async :**

1. `async` dans la signature
2. `Task<int>` comme type de retour — **pas** `int`
3. le nom se termine par `Async` (convention universelle)

Et regarde le `return a + b;` : tu retournes un `int`, alors que le type déclaré
est `Task<int>`. Pas d'erreur : le mot-clé `async` fait l'emballage tout seul.
C'est la seule situation en C# où le type de retour et le `return` diffèrent.

---

## Exercice 2 — Le chargement séquentiel

```csharp
public static async Task<int> ChargerSequentielAsync(string[] ressources)
{
    int total = 0;
    foreach (string r in ressources)
    {
        total += await Simulateur.ChargerAsync(r);
    }
    return total;
}
```

C'est l'accumulateur du module 3, avec un `await` dedans.

**Et c'est lent.** Avec 5 ressources à 100 ms, ça prend 500 ms, parce que chaque
`await` attend la fin du précédent avant de lancer le suivant. Ce code est
asynchrone dans sa forme, mais **séquentiel dans son comportement**.

> ⚠️ **L'erreur n°1 avec async.** Elle ne provoque aucun bug — juste un
> programme 5 fois trop lent. Donc personne ne la remarque. Cherche « `await`
> dans une boucle » dès qu'un programme async te paraît lent.

---

## Exercice 3 — Le chargement parallèle

```csharp
public static async Task<int[]> ChargerToutesAsync(string[] ressources)
{
    Task<int>[] taches = ressources
        .Select(r => Simulateur.ChargerAsync(r))    // ⬅️ AUCUN await ici !
        .ToArray();

    return await Task.WhenAll(taches);              // ⬅️ un seul await, à la fin
}
```

### 🔑 La ligne clé : `Select` sans `await`

```csharp
.Select(r => Simulateur.ChargerAsync(r))
```

Appeler une méthode async **sans `await`**, ça la **démarre** sans attendre sa
fin. On obtient une `Task<int>` — un ticket de consigne.

`Select` produit donc 5 tickets **en quelques microsecondes**. Les 5
chargements tournent maintenant **en même temps**.

Puis `Task.WhenAll` attend que tous soient revenus.

```
Séquentiel :  [100ms][100ms][100ms][100ms][100ms]  = 500 ms
Parallèle  :  [100ms]
              [100ms]
              [100ms]                              = 100 ms
              [100ms]
              [100ms]
```

### Le `.ToArray()` n'est pas décoratif

Sans lui, `Select` reste une requête LINQ **différée** (module 7 !). Les tâches
ne seraient créées qu'au moment où `Task.WhenAll` parcourt la séquence... une
par une. Tu retomberais en séquentiel, sans comprendre pourquoi.

> 🧠 C'est exactement le piège de l'exécution différée du module 7, rencontré
> dans un contexte où il fait vraiment mal. Les deux modules se répondent.

### L'ordre est garanti

`Task.WhenAll` rend les résultats **dans l'ordre des tâches**, pas dans l'ordre
d'arrivée. Tu peux te fier aux indices sans crainte.

---

## Exercice 4 — Le total en parallèle

```csharp
public static async Task<int> ChargerParalleleAsync(string[] ressources)
{
    int[] longueurs = await ChargerToutesAsync(ressources);
    return longueurs.Sum();
}
```

Deux lignes, parce que l'exercice 3 fait déjà le travail. C'est la décomposition
du module 3 qui continue de payer, cinq modules plus tard.

---

## Exercice 5 — Gérer une erreur

```csharp
public static async Task<int> ChargerOuMoinsUnAsync(string ressource)
{
    try
    {
        return await Simulateur.ChargerRisqueAsync(ressource);
    }
    catch (Exception)
    {
        return -1;
    }
}
```

`try/catch` fonctionne **normalement** autour d'un `await`. L'exception levée
dans la méthode async remonte jusqu'au `await` qui l'attendait, comme si tout
était synchrone. C'est une des grandes réussites de `async`/`await` : on garde
sa façon habituelle de raisonner.

> ⚠️ Attention avec `Task.WhenAll` : si **plusieurs** tâches échouent, le `catch`
> ne t'en montre qu'une seule. Les autres sont dans `Task.Exception`
> (une `AggregateException`). Détail de spécialiste, mais sache que le piège
> existe.

---

## Exercice 6 — Le calcul parallèle

```csharp
public static async Task<long> CalculerEnParalleleAsync(int taille)
{
    int milieu = taille / 2;

    Task<long> premiere = Task.Run(() => Simulateur.CalculLourd(0, milieu));
    Task<long> seconde  = Task.Run(() => Simulateur.CalculLourd(milieu, taille));

    long[] resultats = await Task.WhenAll(premiere, seconde);
    return resultats[0] + resultats[1];
}
```

### ⚠️ Ce n'est PAS le même problème que les exercices 2-4

| Exercices 2-4 | Exercice 6 |
|---|---|
| on **attend** (I/O simulée) | on **calcule** (CPU) |
| le processeur dort | le processeur travaille |
| `async`/`await` suffit | il faut de vrais threads |
| gain : ×5 avec 5 tâches | gain : ×2 avec 2 cœurs |

`Task.Run` envoie le calcul sur un autre thread, pris dans le réservoir géré par
.NET. Deux cœurs travaillent vraiment en même temps.

### 🔑 Pourquoi la lambda `() =>`

```csharp
Task.Run(() => Simulateur.CalculLourd(0, milieu))    // ✅
Task.Run(Simulateur.CalculLourd(0, milieu))          // ❌ ne compile pas
```

`Task.Run` veut recevoir **du code à exécuter**, pas un résultat. Sans la
lambda, C# calculerait `CalculLourd(...)` **immédiatement**, sur le thread
courant, et enverrait le nombre obtenu — ce qui n'a aucun sens.

C'est exactement le `Func` du module 7 : **un comportement passé en paramètre**.
Sans les lambdas, l'async serait bien plus pénible à écrire.

### Pourquoi le découpage est correct

```csharp
CalculLourd(0, milieu)          // [0, milieu[
CalculLourd(milieu, taille)     // [milieu, taille[
```

Les deux intervalles sont **jointifs** (le second commence exactement où le
premier finit) et **disjoints** (aucun chevauchement). Chaque élément est traité
exactement une fois.

Avec `taille = 1001`, `milieu = 500` : `[0, 500[` puis `[500, 1001[`. Rien n'est
perdu. Le test `Fonctionne_sur_une_taille_impaire` est là exprès pour vérifier
ça. 😈

> 💡 En vrai, on utiliserait `Parallel.For` ou `nombres.AsParallel().Sum()`
> (PLINQ !), qui découpent tout seuls selon le nombre de cœurs. Mais le faire à
> la main une fois, c'est comprendre ce qui se passe derrière.

---

## Exercice 7 — Le compteur martyrisé

```csharp
public static async Task<int> MartyriserLeCompteurAsync(
    Compteur compteur, int nombreDeTaches, int incrementsParTache)
{
    Task[] taches = new Task[nombreDeTaches];

    for (int t = 0; t < nombreDeTaches; t++)
    {
        taches[t] = Task.Run(() =>
        {
            for (int i = 0; i < incrementsParTache; i++)
            {
                compteur.IncrementerSecurise();
            }
        });
    }

    await Task.WhenAll(taches);
    return compteur.Valeur;
}
```

**Le même motif qu'à l'exercice 3** : on lance tout, on attend tout.

Et cette fois, le résultat est **exact à chaque exécution**, parce que le `lock`
de `IncrementerSecurise` sérialise les accès. C'est un peu plus lent qu'un
incrément brut... mais c'est **juste**, et un résultat faux à toute vitesse ne
vaut rien.

> 💡 **Le piège de la capture de variable de boucle.** Ici, la lambda n'utilise
> pas `t`, donc tout va bien. Mais si elle l'utilisait :
> ```csharp
> for (int t = 0; t < n; t++)
>     taches[t] = Task.Run(() => Console.WriteLine(t));   // 😱
> ```
> ...la lambda capture la **variable** `t`, pas sa valeur. Quand les tâches
> s'exécutent, `t` vaut déjà `n`. Tu verrais `n` affiché partout.
>
> **La correction** : copier la valeur dans une variable locale.
> ```csharp
> for (int t = 0; t < n; t++)
> {
>     int copie = t;
>     taches[t] = Task.Run(() => Console.WriteLine(copie));   // ✅
> }
> ```
> *(C# a corrigé ce comportement pour `foreach` depuis la version 5, mais il
> reste vrai pour `for`. C'est un grand classique des entretiens d'embauche.)*

---

## 🧠 Ce qu'il faut vraiment retenir de ce module

### 1. Deux problèmes, deux solutions

- **Attendre** (réseau, disque, base de données) → `async`/`await`
- **Calculer** (traitement long) → `Task.Run`, parallélisme

Les confondre, c'est soit gaspiller des threads à attendre, soit croire qu'on
accélère un téléchargement en achetant un meilleur processeur.

### 2. Une donnée partagée entre threads est une bombe

Dès que deux threads touchent la même variable et qu'au moins un l'**écrit**, il
faut protéger. Sinon, tu auras des bugs :

- intermittents
- irreproductibles
- qui disparaissent quand tu les cherches
- qui ne plantent pas, mais donnent des résultats faux

> **Le meilleur conseil du module** : quand tu peux l'éviter, **ne partage
> rien**. Donne à chaque tâche ses propres données, et rassemble les résultats à
> la fin (c'est exactement ce que fait `Task.WhenAll`). Pas de partage, pas de
> verrou, pas de bug.

### 3. Les pièges mortels

| ❌ | ✅ |
|---|---|
| `tache.Result`, `tache.Wait()` | `await tache` |
| `async void` | `async Task` |
| `await` dans une boucle (tâches indépendantes) | `Task.WhenAll` |
| `Thread.Sleep` dans de l'async | `await Task.Delay` |
| `Task.Run` autour d'une attente | la vraie méthode `...Async` |
| Oublier `.ToArray()` avant `WhenAll` | matérialiser la requête |

---

## 🎓 Bilan du PARCOURS

Tu viens de finir les **8 modules**. Regarde le chemin :

| Module | Ce que tu as appris |
|--------|---------------------|
| 1 | Variables, types, affichage, calculs |
| 2 | Conditions, boucles, logique |
| 3 | Méthodes, tableaux, algorithmes, décomposition |
| 4 | Classes, objets, encapsulation |
| 5 | Héritage, polymorphisme, interfaces |
| 6 | Collections, exceptions, fichiers, JSON |
| 7 | Délégués, lambdas, LINQ |
| 8 | Async, threads, concurrence |

**C'est le programme d'une première année d'école d'informatique.**

Et ce n'est pas seulement du C# : les classes, l'héritage, les lambdas, les
collections, l'async existent presque à l'identique en Java, TypeScript, Python,
Swift, Kotlin. Tu viens d'apprendre **à programmer**, pas juste un langage.

### Et maintenant ?

**Continue avec C# :**
- **records**, `switch` par motifs, tuples, nullable reference types
- **Entity Framework** — parler à une vraie base de données
- **ASP.NET Core** — créer un site web ou une API
- **MAUI** ou **Avalonia** — des applications avec de vraies fenêtres
- **Unity** — des jeux vidéo. C'est du C#, et tu as maintenant tout ce qu'il
  faut pour t'y mettre. 🎮

**Ou explore ailleurs :**
- **Git** et GitHub — sauvegarder et partager ton code (le plus utile de tous)
- **Les tests unitaires** — tu en as lu 350 pendant ce parcours, écris les tiens
- **Un autre langage** : Python, TypeScript... tu iras dix fois plus vite
  maintenant

**Mais surtout : construis quelque chose.** Un projet que *tu* as envie de
faire, aussi bête soit-il. C'est comme ça qu'on progresse vraiment — pas en
suivant des cours. 🚀
