# Module 8 — Threading & async ⚡

> **Objectif** : faire plusieurs choses à la fois. C'est le module le plus
> difficile du parcours — et celui qui t'apprendra les bugs les plus retors de
> toute la programmation.
>
> ⏱️ Lecture : ~35 min · Exercices : ~2h30

---

## 1. Le problème

Depuis le module 1, ton programme fait **une chose à la fois**, de haut en bas.

```csharp
string a = TelechargerFichier("a.txt");   // 2 secondes d'attente
string b = TelechargerFichier("b.txt");   // 2 secondes d'attente
string c = TelechargerFichier("c.txt");   // 2 secondes d'attente
// Total : 6 secondes
```

Pendant ces 6 secondes, ton programme est **complètement figé**. Il n'attend
même pas activement : il ne fait **rien**, il patiente pendant que le réseau
travaille.

Or ces trois téléchargements sont indépendants. S'ils partaient **ensemble**,
ça prendrait 2 secondes au lieu de 6.

C'est tout le sujet du module.

---

## 2. Deux problèmes différents (ne les confonds pas)

C'est **la** distinction à comprendre, et beaucoup de développeurs
professionnels la confondent encore.

### 🕐 Problème 1 : ATTENDRE quelque chose d'extérieur

Télécharger un fichier, lire un disque, interroger une base de données,
attendre une saisie clavier.

Ton processeur ne fait **rien** pendant ce temps : il attend.
👉 **Solution : `async` / `await`.**

### 🔥 Problème 2 : CALCULER quelque chose de long

Compter les nombres premiers jusqu'à un milliard, redimensionner 500 photos.

Ton processeur travaille **à fond**, mais sur un seul de ses cœurs.
👉 **Solution : les threads, `Task.Run`, le parallélisme.**

| | Problème 1 | Problème 2 |
|---|---|---|
| Nature | **attente** (I/O) | **calcul** (CPU) |
| Le processeur | dort | sue |
| Solution | `async`/`await` | `Task.Run`, threads |
| Gain | énorme | ×nombre de cœurs |

> 🧠 **L'analogie de la cuisine** :
> - Le gâteau est au four (20 min) → tu ne restes pas planté devant. Tu fais
>   autre chose et tu reviens. **C'est `async`/`await`.**
> - Tu dois éplucher 200 carottes → tu appelles 3 amis pour éplucher en même
>   temps. **C'est le parallélisme.**
>
> Appeler 3 amis pour **regarder le four ensemble** ne fait pas cuire le gâteau
> plus vite. C'est exactement l'erreur que font les gens qui confondent les deux.

---

## 3. La `Task` : une promesse

Une **`Task`** représente un travail **en cours**, dont le résultat arrivera
plus tard.

```csharp
Task<int> promesse = CalculerAsync();    // le travail démarre
// ... on peut faire autre chose ici ...
int resultat = await promesse;           // maintenant, on récupère le résultat
```

- `Task` → un travail qui ne rend rien (comme `void`)
- `Task<int>` → un travail qui rendra un `int`

C'est **un ticket de consigne** : tu ne tiens pas encore ton manteau, mais tu as
la garantie de pouvoir le récupérer.

---

## 4. `async` et `await`

```csharp
static async Task<int> ChargerAsync()
{
    await Task.Delay(1000);      // "attends 1 seconde, sans bloquer"
    return 42;
}
```

**Les trois règles :**

1. **`async`** sur la méthode : « cette méthode contient des `await` »
2. Le type de retour est **`Task`** ou **`Task<T>`**, jamais `int` ou `string`
3. Le nom se termine par **`Async`** — c'est une convention universelle, suis-la

```csharp
int resultat = await ChargerAsync();     // await "déballe" le Task<int>
```

### Ce que fait vraiment `await`

`await` ne veut **pas** dire « bloque ici ». Il veut dire :

> « Ce travail va prendre du temps. **Rends la main** au programme appelant. Il
> fera autre chose. Quand le résultat sera prêt, **reprends ici**, à cette ligne
> exactement. »

C'est la différence fondamentale avec `Thread.Sleep(1000)`, qui bloque
bêtement un thread entier à ne rien faire.

```csharp
Thread.Sleep(1000);        // ❌ le thread est bloqué, gaspillé
await Task.Delay(1000);    // ✅ le thread part travailler ailleurs
```

### ⚠️ Le piège n°1 : `await` dans une boucle

```csharp
// ❌ SÉQUENTIEL — 3 × 2s = 6 secondes
foreach (string fichier in fichiers)
{
    string contenu = await TelechargerAsync(fichier);
    total += contenu.Length;
}
```

Chaque `await` **attend la fin** avant de lancer le suivant. Tu as écrit du code
asynchrone... qui se comporte exactement comme du code synchrone.

C'est l'erreur la plus fréquente avec `async`. **Elle ne provoque aucun bug** —
juste un programme trois fois trop lent. Donc personne ne la remarque.

---

## 5. `Task.WhenAll` : tout lancer, puis tout attendre

```csharp
// ✅ PARALLÈLE — 2 secondes au total
Task<string>[] taches = fichiers
    .Select(f => TelechargerAsync(f))     // on LANCE tout, sans await
    .ToArray();

string[] resultats = await Task.WhenAll(taches);   // on attend TOUT
```

**Les deux temps** :

1. **Lancer sans `await`** → les trois travaux démarrent ensemble
2. **`await Task.WhenAll(...)`** → on attend que tous soient finis

`Task.WhenAll` rend les résultats **dans l'ordre des tâches**, pas dans l'ordre
d'arrivée. Tu peux donc te fier aux indices.

```csharp
await Task.WhenAny(taches);   // attendre seulement le PREMIER qui finit
```

---

## 6. `Task.Run` : déporter un calcul lourd

Pour le **problème 2** (calcul, pas attente) :

```csharp
int resultat = await Task.Run(() => CalculTresLong());
```

`Task.Run` envoie le travail sur un **autre thread**, pris dans un réservoir
géré par .NET (le *thread pool*). Le thread principal reste libre.

Pour vraiment aller plus vite, il faut **découper le travail** :

```csharp
Task<long> moitie1 = Task.Run(() => Somme(nombres, 0, nombres.Length / 2));
Task<long> moitie2 = Task.Run(() => Somme(nombres, nombres.Length / 2, nombres.Length));

long[] resultats = await Task.WhenAll(moitie1, moitie2);
long total = resultats[0] + resultats[1];
```

Deux cœurs travaillent en même temps → environ deux fois plus vite.

> ⚠️ **`Task.Run` ne sert à rien pour de l'attente.**
> `await Task.Run(() => File.ReadAllText(f))` occupe un thread **à attendre**.
> C'est exactement ce qu'on cherchait à éviter. Utilise la vraie version async
> quand elle existe : `await File.ReadAllTextAsync(f)`.

---

## 7. 🔴 La race condition

Voici le bug le plus vicieux de toute la programmation.

```csharp
int compteur = 0;

// 4 tâches qui incrémentent 10 000 fois chacune
await Task.WhenAll(
    Task.Run(() => { for (int i = 0; i < 10000; i++) compteur++; }),
    Task.Run(() => { for (int i = 0; i < 10000; i++) compteur++; }),
    Task.Run(() => { for (int i = 0; i < 10000; i++) compteur++; }),
    Task.Run(() => { for (int i = 0; i < 10000; i++) compteur++; })
);

Console.WriteLine(compteur);   // 40000 ? Presque jamais. 😱
```

Tu obtiendras 37 412. Puis 39 108. Puis 40 000 (une fois sur dix, pour bien
t'embrouiller).

### Pourquoi ?

Parce que `compteur++` **n'est pas une seule opération**. Le processeur fait
trois choses :

```
1. LIRE  la valeur en mémoire        (disons 100)
2. AJOUTER 1                         (101)
3. ÉCRIRE le résultat en mémoire     (100 → 101)
```

Maintenant, deux threads en même temps :

```
Thread A : LIT 100
Thread B : LIT 100          ← il lit AVANT que A ait écrit !
Thread A : calcule 101, ÉCRIT 101
Thread B : calcule 101, ÉCRIT 101

Deux incréments... et le compteur n'a avancé que de 1. Un est PERDU.
```

### Pourquoi c'est le pire bug du monde

- Il est **intermittent** : ça marche 9 fois sur 10
- Il **disparaît quand on le cherche** : ajoute un `Console.WriteLine` pour
  déboguer, et le timing change — le bug s'évanouit
- Il **ne se reproduit pas** de la même façon sur une autre machine
- Il **ne plante pas** : il donne juste des résultats faux

On appelle ça un **heisenbug** (en référence au principe d'incertitude de
Heisenberg) : l'observer le fait disparaître.

---

## 8. `lock` : le verrou

```csharp
private readonly object _verrou = new object();
private int _compteur = 0;

public void Incrementer()
{
    lock (_verrou)
    {
        _compteur++;      // UN SEUL thread à la fois peut être ici
    }
}
```

`lock` garantit qu'**un seul thread à la fois** entre dans le bloc. Les autres
attendent leur tour, sagement.

C'est la cabine d'essayage : un seul client dedans, les autres font la queue.

### Les règles du `lock`

**1. Toujours un objet privé et dédié :**

```csharp
private readonly object _verrou = new object();   // ✅
lock (this)                                        // ❌ n'importe qui peut
lock (typeof(MaClasse))                            //    verrouiller dessus
lock ("chaîne")                                    //    et te bloquer
```

**2. Le bloc doit être le plus COURT possible.** Pendant qu'un thread est
dedans, tous les autres attendent. Un `lock` trop large annule tout le bénéfice
du parallélisme.

**3. Jamais d'`await` dans un `lock`** — le compilateur te l'interdit d'ailleurs.

**4. Toutes les lectures ET écritures** de la donnée partagée doivent passer par
le même verrou. Un seul accès oublié, et la protection ne vaut rien.

### 💀 Le deadlock

Deux verrous, deux threads, et l'ordre inversé :

```csharp
// Thread A                     // Thread B
lock (verrouA)                  lock (verrouB)
{                               {
    lock (verrouB)                  lock (verrouA)
    { ... }                         { ... }
}                               }
```

A tient `verrouA` et veut `verrouB`. B tient `verrouB` et veut `verrouA`.
**Aucun ne lâchera jamais.** Le programme est figé pour l'éternité.

**La prévention** : si tu dois prendre plusieurs verrous, prends-les **toujours
dans le même ordre**, partout dans le programme.

### L'alternative : `Interlocked`

Pour les opérations simples sur des nombres, il existe plus rapide qu'un `lock` :

```csharp
Interlocked.Increment(ref _compteur);   // atomique, garanti indivisible
```

« Atomique » veut dire « en un seul morceau, impossible à interrompre ». Le
processeur garantit lui-même l'opération.

---

## 9. Gérer les erreurs en async

```csharp
try
{
    int resultat = await ChargerAsync();
}
catch (Exception e)
{
    Console.WriteLine($"Échec : {e.Message}");
}
```

`try/catch` fonctionne normalement autour d'un `await`. L'exception levée dans
la méthode async **remonte** au `await`.

> ⚠️ Avec `Task.WhenAll`, si **plusieurs** tâches échouent, le `catch` ne
> t'en montre qu'**une seule**. Pour les voir toutes, il faut inspecter
> `Task.Exception`. C'est un détail de spécialiste — retiens juste que le piège
> existe.

---

## 10. ⚠️ Les pièges à éviter absolument

### `.Result` et `.Wait()` : le blocage mortel

```csharp
int x = ChargerAsync().Result;   // ❌ JAMAIS
ChargerAsync().Wait();           // ❌ JAMAIS
```

Ça **bloque** le thread en attendant — et dans certains contextes (applications
graphiques, serveurs web), ça provoque un **deadlock immédiat et définitif**.

**Toujours `await`.** Si tu ne peux pas `await`, c'est que ta méthode devrait
être `async`.

### `async void` : l'exception qui tue tout

```csharp
async void Faire()      // ❌
async Task Faire()      // ✅
```

Avec `async void`, personne ne peut `await` ta méthode, et une exception à
l'intérieur **fait planter tout le programme** sans que rien ne puisse
l'attraper.

La seule exception à cette règle : les gestionnaires d'événements d'interface
graphique (`button_Click`).

### Le résumé

| ❌ Ne fais pas | ✅ Fais |
|---|---|
| `tache.Result` | `await tache` |
| `tache.Wait()` | `await tache` |
| `async void` | `async Task` |
| `Thread.Sleep` dans de l'async | `await Task.Delay` |
| `await` dans une boucle (si indépendants) | `Task.WhenAll` |
| `Task.Run` pour de l'attente | la vraie méthode `...Async` |

---

## 🎯 Récapitulatif

| Je veux... | J'écris |
|------------|---------|
| Une méthode qui attend | `async Task<int> FaireAsync()` |
| Attendre un résultat | `int x = await FaireAsync();` |
| Attendre sans bloquer | `await Task.Delay(1000);` |
| Tout lancer en parallèle | `await Task.WhenAll(taches)` |
| Le premier qui finit | `await Task.WhenAny(taches)` |
| Déporter un calcul lourd | `await Task.Run(() => Calcul())` |
| Protéger une donnée partagée | `lock (_verrou) { ... }` |
| Incrémenter en sécurité | `Interlocked.Increment(ref x)` |

---

## ▶️ À toi de jouer

`Exercices/Simulateur.cs` est déjà écrit (il simule des travaux lents).
Remplis `Exercices/Exo.cs` et `Exercices/Compteur.cs`.

```bash
dotnet run --project Exercices
```

La démo te montre **la race condition en direct**. Lance-la plusieurs fois : le
résultat change à chaque exécution. C'est le moment où on comprend. 😱

```bash
dotnet test Tests
```

> ⏱️ Ces tests sont plus lents que d'habitude (quelques secondes) : ils
> attendent vraiment. C'est normal.
