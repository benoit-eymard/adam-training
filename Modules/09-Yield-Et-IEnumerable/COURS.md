# Module 9 — Sous le capot : `yield` et `IEnumerable` 🔬

> **Objectif** : arrêter d'utiliser LINQ comme une boîte noire. Tu vas
> **réécrire toi-même** `Where`, `Select` et `Aggregate` — et découvrir au
> passage une des idées les plus élégantes du langage.
>
> ⏱️ Lecture : ~30 min · Exercices : ~2h
>
> 📌 **Prérequis** : modules 3 (méthodes), 6 (collections) et 7 (LINQ).

---

## 1. La question qu'on n'a jamais posée

Au module 7, tu as appris que les requêtes LINQ sont **différées** :

```csharp
var requete = equipe.Where(h => h.EstVivant);   // rien ne se passe
foreach (var h in requete) { }                  // MAINTENANT ça calcule
```

On t'a dit **que** ça marchait comme ça. Jamais **pourquoi**.

Et il y a plus troublant encore :

```csharp
var nombres = Naturels();          // TOUS les entiers, de 0 à l'infini
var premiers5 = nombres.Take(5);   // et ça ne plante pas ?!
```

Comment une collection **infinie** peut-elle tenir en mémoire ?

**Réponse : elle n'y tient pas.** Elle n'existe même pas. Et c'est tout le sujet
de ce module.

---

## 2. Ce que `foreach` fait vraiment

Depuis le module 3, tu écris ça sans y penser :

```csharp
foreach (int n in nombres)
{
    Console.WriteLine(n);
}
```

Le compilateur, lui, le traduit à peu près en :

```csharp
var enumerateur = nombres.GetEnumerator();
while (enumerateur.MoveNext())
{
    int n = enumerateur.Current;
    Console.WriteLine(n);
}
```

Décortiquons ces trois membres, parce que tout le module tient dedans :

| Membre | Ce qu'il fait |
|--------|---------------|
| `GetEnumerator()` | donne un **curseur** posé avant le premier élément |
| `MoveNext()` | avance d'un cran. Rend `false` quand il n'y a plus rien |
| `Current` | l'élément sur lequel le curseur est posé |

> 🧠 **L'idée clé** : `foreach` ne demande **jamais** « donne-moi tous tes
> éléments ». Il demande, un par un, « **et le suivant ?** ».
>
> C'est ce qui rend l'infini possible. Tant que quelqu'un peut répondre « le
> suivant est… », la collection n'a pas besoin d'exister en entier.

### `IEnumerable<T>` : le contrat

```csharp
public interface IEnumerable<T>
{
    IEnumerator<T> GetEnumerator();
}
```

Une seule méthode ! C'est **l'interface la plus importante de tout .NET** :
`List`, tableau, `Dictionary`, `Queue`, `HashSet`, `string`… tout l'implémente.

Et c'est aussi ce que **LINQ** attend et rend. C'est pour ça que `Where` marche
aussi bien sur un tableau que sur un dictionnaire : il ne connaît que ce contrat.

> 💡 **Le réflexe de pro** : quand tu écris une méthode qui *lit* une
> collection, prends `IEnumerable<T>` en paramètre plutôt que `List<T>`. Elle
> acceptera alors tout : listes, tableaux, résultats LINQ, générateurs…

---

## 3. `yield return` : la méthode qui se met en pause

Écrire un `IEnumerator` à la main est pénible. Alors C# a inventé un raccourci
un peu magique.

```csharp
static IEnumerable<int> Compter()
{
    Console.WriteLine("-> je démarre");
    yield return 1;
    Console.WriteLine("-> je reprends");
    yield return 2;
    Console.WriteLine("-> j'ai fini");
}
```

Maintenant, exécute ça :

```csharp
var sequence = Compter();
Console.WriteLine("la méthode a été appelée");

foreach (int n in sequence)
{
    Console.WriteLine($"   reçu : {n}");
}
```

Tu obtiens :

```
la méthode a été appelée          ← RIEN ne s'est encore exécuté !
-> je démarre
   reçu : 1
-> je reprends
   reçu : 2
-> j'ai fini
```

### 🤯 Ce qui se passe vraiment

1. **Appeler la méthode n'exécute AUCUNE ligne.** Elle rend immédiatement un
   objet « prêt à produire ».
2. À chaque tour de `foreach`, la méthode **reprend là où elle s'était
   arrêtée**, jusqu'au prochain `yield return`.
3. `yield return` **rend une valeur ET met la méthode en pause** — variables
   locales conservées, position mémorisée.

Une méthode normale s'exécute d'un bloc et retourne une fois. Une méthode avec
`yield` est une **machine à produire** qui rend la main entre chaque valeur.

> 🐞 **Vois-le en vrai.** Pose un point d'arrêt sur chaque `yield return` et
> avance en `F10` ([DEBUG.md](../../DEBUG.md)). Tu verras l'exécution
> **sauter entre la boucle et la méthode**. C'est le meilleur moment du module.

### `yield break` : s'arrêter avant la fin

```csharp
static IEnumerable<int> Jusqua(int max)
{
    for (int i = 0; ; i++)
    {
        if (i > max)
        {
            yield break;      // fini, plus rien à produire
        }
        yield return i;
    }
}
```

`yield break` est au générateur ce que `return` est à une méthode normale.

### Les règles de `yield`

- Le type de retour doit être `IEnumerable<T>` (ou `IEnumerator<T>`)
- **Interdit** de mélanger `yield return` et un `return valeur;` classique
- Interdit dans une lambda
- Interdit dans un bloc `try` qui a un `catch` (autorisé avec `finally`)

---

## 4. Enfin : pourquoi LINQ est différé

Tu as maintenant tout ce qu'il faut pour comprendre `Where`. Le voici,
**en entier** :

```csharp
public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> critere)
{
    foreach (T element in source)
    {
        if (critere(element))
        {
            yield return element;
        }
    }
}
```

**C'est tout.** Six lignes. Aucune magie, aucun code compilé secret.

Et comme la méthode contient un `yield`, **l'appeler n'exécute rien**. Elle rend
un objet qui *saura* filtrer, quand on le lui demandera. D'où l'exécution
différée du module 7 : ce n'était pas une décision de conception mystérieuse,
c'est simplement **la conséquence de `yield`**.

### La chaîne devient un tuyau

```csharp
equipe.Where(h => h.EstVivant).Select(h => h.Nom).Take(2)
```

On pourrait croire que ça construit trois listes intermédiaires. **Pas du
tout.** Ça fabrique trois maillons, et **rien ne circule** tant qu'on ne tire
pas dessus.

Quand le `foreach` final demande un élément :

```
foreach  ──demande──►  Take  ──demande──►  Select  ──demande──►  Where  ──demande──►  equipe
                                                                                         │
foreach  ◄──"Kaelis"── Take  ◄──"Kaelis"── Select  ◄──Kaelis───  Where  ◄────Kaelis──────┘
```

**Un seul élément traverse toute la chaîne à la fois.** Puis le suivant. Et
quand `Take(2)` a eu ses deux éléments, il fait `yield break` — et **plus
personne n'est sollicité**, même s'il restait un million d'éléments derrière.

### 💡 Les trois conséquences

**1. On peut manipuler l'infini.**
```csharp
Naturels().Where(n => n % 7 == 0).Take(3).ToList();   // [0, 7, 14]
```
Ça termine, parce qu'on ne demande que 3 éléments.

**2. On ne calcule que le strict nécessaire.**
```csharp
grandeListe.Where(x => TrèsLent(x)).First();
```
`TrèsLent` n'est appelé que jusqu'au premier succès. Pas une fois de plus.

**3. Et le piège du module 7.** La requête se ré-exécute **à chaque parcours** :
```csharp
var r = liste.Where(...);
r.Count();    // parcourt tout
r.Count();    // reparcourt TOUT
```
D'où `.ToList()` pour figer. Maintenant tu sais pourquoi.

---

## 5. Les méthodes d'extension

Tu as sûrement remarqué le `this` bizarre :

```csharp
public static IEnumerable<T> Where<T>(this IEnumerable<T> source, ...)
                                      ↑↑↑↑
```

Ce mot-clé fait quelque chose de remarquable : il permet d'**ajouter une méthode
à un type que tu ne possèdes pas**.

```csharp
public static class MesOutils
{
    public static string Crier(this string texte)
    {
        return texte.ToUpper() + " !!!";
    }
}
```

```csharp
"bonjour".Crier();              // "BONJOUR !!!"  😎
MesOutils.Crier("bonjour");     // exactement la même chose
```

Tu n'as pas modifié `string` — c'est impossible, il appartient à Microsoft. Mais
tu peux **lui coller des méthodes**.

**Les trois règles :**
1. la classe doit être `static`
2. la méthode doit être `static`
3. le premier paramètre porte `this`

> 🧠 **C'est comme ça que LINQ existe.** `Where`, `Select`, `Sum`… ne sont pas
> dans `List<T>`. Ce sont des méthodes d'extension définies dans une classe
> `Enumerable`, sur `IEnumerable<T>`. C'est pour ça qu'il faut parfois écrire
> `using System.Linq;` — sans ce `using`, les extensions sont invisibles et
> `.Where` n'existe pas.

---

## 6. Générateurs finis et infinis

```csharp
// FINI
static IEnumerable<int> Compter(int debut, int fin)
{
    for (int i = debut; i <= fin; i++)
    {
        yield return i;
    }
}

// INFINI 😱
static IEnumerable<int> Naturels()
{
    int n = 0;
    while (true)          // oui, une boucle infinie. C'est voulu.
    {
        yield return n;
        n++;
    }
}
```

`while (true)` ferait normalement planter ton programme. Ici, **non** : la
méthode se met en pause à chaque `yield return` et n'avance que si on le lui
demande.

```csharp
Naturels().Take(5).ToList();      // [0, 1, 2, 3, 4] — termine !
```

> 🔴 **MAIS** : `Naturels().ToList()` tourne **pour l'éternité** et finit par
> saturer la mémoire. `ToList()` demande *tous* les éléments — et il n'y a pas
> de fin.
>
> **La règle** : sur une séquence infinie, il faut **toujours** un `Take`, un
> `First`, ou un `TakeWhile` pour la borner.

### Un exemple qui claque : Fibonacci

```csharp
static IEnumerable<long> Fibonacci()
{
    long a = 0, b = 1;
    while (true)
    {
        yield return a;
        (a, b) = (b, a + b);      // échange sans variable temporaire !
    }
}
```

```csharp
Fibonacci().Take(10).ToList();                      // [0,1,1,2,3,5,8,13,21,34]
Fibonacci().Where(n => n % 2 == 0).Take(5).ToList(); // les 5 premiers pairs
Fibonacci().First(n => n > 1000);                    // 1597
```

Compare avec la version récursive du module 3, qui faisait des milliards
d'appels pour `n = 50`. Ici, chaque nombre est calculé **une seule fois**, à la
demande, et on peut s'arrêter quand on veut.

> 💡 `(a, b) = (b, a + b);` est une affectation de **tuple** : les deux valeurs
> de droite sont calculées **d'abord**, puis rangées. Plus besoin de variable
> temporaire — souviens-toi de l'échange du module 3. 😄

---

## 7. Bonus : `IAsyncEnumerable` (module 8 + module 9)

Et si chaque élément demandait une **attente** ? Par exemple lire les lignes
d'un fichier énorme, ou des résultats qui arrivent du réseau page par page.

```csharp
static async IAsyncEnumerable<string> LireLignesAsync(string chemin)
{
    using var lecteur = new StreamReader(chemin);
    string ligne;
    while ((ligne = await lecteur.ReadLineAsync()) != null)
    {
        yield return ligne;
    }
}
```

```csharp
await foreach (string ligne in LireLignesAsync("enorme.txt"))
{
    Console.WriteLine(ligne);
}
```

`async` + `yield` + `await foreach` : le meilleur des modules 8 et 9. On traite
un fichier de 10 Go **sans jamais le charger en mémoire**, et sans jamais
bloquer un thread.

Rien à faire dessus ici — sache simplement que ça existe et que tu as maintenant
les deux moitiés de l'idée.

---

## 🎯 Récapitulatif

| Concept | À retenir |
|---------|-----------|
| `IEnumerable<T>` | le contrat « je sais me faire parcourir » |
| `foreach` | demande les éléments **un par un**, jamais tous |
| `yield return x` | rend `x` **et met la méthode en pause** |
| `yield break` | termine la séquence |
| Appeler un générateur | **n'exécute rien** — d'où l'exécution différée |
| Méthode d'extension | `static` + `this` en 1er paramètre |
| Séquence infinie | possible, mais **borne-la** (`Take`, `First`) |
| `ToList()` | force tout le calcul — jamais sur l'infini ! |

---

## ▶️ À toi de jouer

Deux fichiers à remplir :

1. `Exercices/MonLinq.cs` — réécris `Where`, `Select`, `Take`, `Count`,
   `Aggregate`
2. `Exercices/Generateurs.cs` — produis des séquences, dont deux infinies

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```

> 🎯 **Ce que tu vas prouver** : des tests vérifient que tes méthodes sont
> réellement **paresseuses** — qu'elles ne lisent que le strict nécessaire. Si
> tu construis une `List` à l'intérieur, ils échoueront. Le seul moyen de les
> faire passer, c'est `yield`.

Puis [SOLUTIONS.md](SOLUTIONS.md) et le [DEFI.md](DEFI.md).
