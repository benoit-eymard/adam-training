# Module 7 — Lambda & LINQ 🪄

> **Objectif** : faire disparaître les boucles. Dix lignes vont devenir une —
> et elle sera plus lisible que les dix.
>
> ⏱️ Lecture : ~30 min · Exercices : ~2h

---

## 1. Une variable qui contient... une méthode

Jusqu'ici, tes variables contenaient des **données** : des nombres, du texte,
des objets. Et si une variable pouvait contenir un **comportement** ?

```csharp
Func<int, int> doubler = x => x * 2;

Console.WriteLine(doubler(21));    // 42
```

`doubler` est une variable. Son contenu, c'est une **méthode**. On peut la
passer en paramètre, la stocker dans une liste, la retourner...

C'est l'idée qui débloque tout le reste du module.

---

## 2. `Func` et `Action`

### `Func<...>` : ça retourne quelque chose

Le **dernier** type entre chevrons est celui du **retour**, les autres sont les
paramètres.

```csharp
Func<int, int> doubler;              // prend un int   → rend un int
Func<int, int, int> additionner;     // prend 2 int    → rend un int
Func<Heros, bool> estVivant;         // prend un Heros → rend un bool
Func<string> direBonjour;            // prend RIEN     → rend un string
```

### `Action<...>` : ça ne retourne rien

```csharp
Action<string> afficher = texte => Console.WriteLine(texte);
Action saluer = () => Console.WriteLine("Salut !");

afficher("Coucou");
saluer();
```

> 🧠 **Moyen mnémotechnique** : une **Fonc**tion **calcule** et rend un résultat.
> Une **Action** **agit** et ne rend rien.

---

## 3. La lambda : une méthode sans nom

```csharp
x => x * 2
```

Ça se lit : « **x donne x fois 2** ». Le `=>` s'appelle « **goes to** ».

C'est exactement équivalent à :

```csharp
static int Doubler(int x)
{
    return x * 2;
}
```

...mais sans nom, sans `return`, sans accolades, et **écrit là où on en a
besoin**.

### Les formes de lambda

```csharp
x => x * 2                        // un paramètre, une expression
(x, y) => x + y                   // plusieurs paramètres : parenthèses
() => Console.WriteLine("hop")    // aucun paramètre : parenthèses vides

// Plusieurs instructions : accolades ET return obligatoires
x =>
{
    int carre = x * x;
    return carre + 1;
}
```

**Remarque** : on n'écrit jamais le type des paramètres. C# le **déduit** du
`Func` ou de la méthode qui reçoit la lambda.

```csharp
Func<int, int> f = x => x * 2;         // C# sait que x est un int
```

### Pourquoi c'est utile

Parce qu'on peut **passer un comportement en paramètre** :

```csharp
static List<Heros> Filtrer(List<Heros> heros, Func<Heros, bool> critere)
{
    List<Heros> resultat = new List<Heros>();
    foreach (Heros h in heros)
    {
        if (critere(h))          // ⬅️ on exécute le comportement reçu
        {
            resultat.Add(h);
        }
    }
    return resultat;
}
```

Et maintenant, **une seule méthode** répond à toutes les questions :

```csharp
Filtrer(equipe, h => h.PointsDeVie > 0);
Filtrer(equipe, h => h.Classe == "Mage");
Filtrer(equipe, h => h.Niveau >= 10 && h.Or > 100);
```

Sans les lambdas, il aurait fallu écrire `FiltrerParVie`, `FiltrerParClasse`,
`FiltrerParNiveauEtOr`... et une nouvelle méthode à chaque nouveau besoin.

---

## 4. LINQ : le langage de requête

Quelqu'un chez Microsoft a eu la bonne idée d'écrire **une fois pour toutes**
les `Filtrer`, `Transformer`, `Trier`, `Compter` — et de les rendre disponibles
sur **toutes** les collections. Ça s'appelle **LINQ**.

```csharp
using System.Linq;     // souvent déjà là grâce aux "usings implicites"
```

### Les deux verbes fondamentaux

```csharp
// WHERE : filtrer — garde ceux qui répondent oui
List<Heros> vivants = equipe.Where(h => h.PointsDeVie > 0).ToList();

// SELECT : transformer — remplace chaque élément par autre chose
List<string> noms = equipe.Select(h => h.Nom).ToList();
```

Compare avec ce que tu écrivais au module 6 :

```csharp
// AVANT — 7 lignes
List<Heros> vivants = new List<Heros>();
foreach (Heros h in equipe)
{
    if (h.PointsDeVie > 0)
    {
        vivants.Add(h);
    }
}

// MAINTENANT — 1 ligne
var vivants = equipe.Where(h => h.PointsDeVie > 0).ToList();
```

> 🧠 **Le vrai gain n'est pas la longueur, c'est le niveau de lecture.** La
> boucle dit *comment* faire (créer une liste, parcourir, tester, ajouter).
> `Where` dit *quoi* on veut. Quand tu relis ton code six mois plus tard, tu
> veux lire le *quoi*.

---

## 5. Le catalogue LINQ

### Filtrer et transformer

```csharp
equipe.Where(h => h.Niveau > 5)        // garde ceux qui matchent
equipe.Select(h => h.Nom)              // transforme chaque élément
equipe.Distinct()                      // supprime les doublons
equipe.Take(3)                         // les 3 premiers
equipe.Skip(2)                         // saute les 2 premiers
equipe.Reverse()                       // inverse l'ordre
```

### Trier

```csharp
equipe.OrderBy(h => h.Niveau)              // croissant
equipe.OrderByDescending(h => h.Niveau)    // décroissant
equipe.OrderBy(h => h.Classe).ThenBy(h => h.Nom)   // tri à 2 niveaux
```

Le tri à bulles du module 3 ? Une méthode. 😄

### Agréger (réduire à une seule valeur)

```csharp
equipe.Count()                     // combien
equipe.Count(h => h.Niveau > 5)    // combien répondent au critère
equipe.Sum(h => h.Or)              // la somme
equipe.Average(h => h.Niveau)      // la moyenne
equipe.Min(h => h.Niveau)          // le minimum
equipe.Max(h => h.Niveau)          // le maximum
```

### Trouver

```csharp
equipe.First()                       // le premier — 💥 si vide
equipe.FirstOrDefault()              // le premier, ou null si vide ✅
equipe.First(h => h.Classe == "Mage")         // le premier mage — 💥 si aucun
equipe.FirstOrDefault(h => h.Classe == "Mage") // ou null ✅
equipe.MaxBy(h => h.Niveau)          // l'ÉLÉMENT qui a le max
equipe.MinBy(h => h.Or)              // l'ÉLÉMENT qui a le min
```

> ⚠️ **`Max` ou `MaxBy` ?**
> `equipe.Max(h => h.Niveau)` rend **le niveau le plus haut** (un `int`).
> `equipe.MaxBy(h => h.Niveau)` rend **le héros** qui a ce niveau.
> Tu voudras presque toujours `MaxBy`.

### Tester

```csharp
equipe.Any()                            // y a-t-il au moins un élément ?
equipe.Any(h => h.Classe == "Mage")     // y a-t-il au moins un mage ?
equipe.All(h => h.PointsDeVie > 0)      // sont-ils TOUS vivants ?
equipe.Contains(monHeros)               // cet élément est-il dedans ?
```

### Regrouper

```csharp
var parClasse = equipe.GroupBy(h => h.Classe);

foreach (var groupe in parClasse)
{
    Console.WriteLine($"{groupe.Key} : {groupe.Count()}");
}
// Guerrier : 2
// Mage : 1
```

`GroupBy` remplace tout le motif « compter des occurrences » du module 6 :

```csharp
Dictionary<string, int> comptes = equipe
    .GroupBy(h => h.Classe)
    .ToDictionary(g => g.Key, g => g.Count());
```

### Convertir

```csharp
.ToList()        // → List<T>
.ToArray()       // → T[]
.ToDictionary(g => g.Key, g => g.Value)    // → Dictionary
```

---

## 6. Enchaîner les requêtes

C'est là que LINQ devient vraiment élégant. Chaque méthode rend une collection,
sur laquelle on peut en appeler une autre.

```csharp
List<string> nomsDesMagesVivants = equipe
    .Where(h => h.Classe == "Mage")        // 1. garde les mages
    .Where(h => h.PointsDeVie > 0)         // 2. garde les vivants
    .OrderBy(h => h.Nom)                   // 3. trie par nom
    .Select(h => h.Nom)                    // 4. ne garde que le nom
    .ToList();                             // 5. matérialise en List
```

Ça se lit **de haut en bas comme une phrase**. Compare avec la version en
boucles : 15 lignes, une liste temporaire, un tri à écrire à la main.

> 💡 **Mets une étape par ligne.** C'est la convention, et ça rend les requêtes
> longues parfaitement lisibles.

### ⚠️ L'ordre des étapes compte

```csharp
equipe.Where(h => h.Niveau > 5).OrderBy(h => h.Nom)   // filtre puis trie
equipe.OrderBy(h => h.Nom).Where(h => h.Niveau > 5)   // trie puis filtre
```

Même résultat, mais la **première est plus rapide** : on trie moins d'éléments.

**Règle** : filtre d'abord (`Where`), trie ensuite (`OrderBy`), transforme en
dernier (`Select`).

---

## 7. L'exécution différée ⚠️

Un piège qui surprend tout le monde une fois :

```csharp
var requete = equipe.Where(h => h.Niveau > 5);   // ⬅️ RIEN n'est calculé ici !

equipe.Add(new Heros("Nouveau", 10));

foreach (var h in requete)     // ⬅️ c'est MAINTENANT que ça s'exécute
{
    // "Nouveau" est là ! La requête a été évaluée après son ajout.
}
```

Une requête LINQ, c'est une **recette**, pas un **plat**. Elle n'est cuisinée
qu'au moment où on la lit — et **à chaque fois** qu'on la lit.

```csharp
var requete = equipe.Where(h => TresLent(h));
int a = requete.Count();    // exécution 1
int b = requete.Count();    // exécution 2 — tout est recalculé ! 😱
```

**La solution** : `ToList()` force le calcul immédiat et mémorise le résultat.

```csharp
var resultat = equipe.Where(h => h.Niveau > 5).ToList();   // calculé maintenant
```

> 🧠 **La règle simple** : termine tes requêtes par `.ToList()` dès que tu vas
> réutiliser le résultat. Les méthodes d'agrégation (`Count`, `Sum`, `Any`,
> `First`...) forcent déjà l'exécution.

---

## 8. Les pièges sur collection vide

```csharp
new List<Heros>().Average(h => h.Niveau)   // 💥 InvalidOperationException
new List<Heros>().Max(h => h.Niveau)       // 💥 InvalidOperationException
new List<Heros>().First()                  // 💥 InvalidOperationException

new List<Heros>().Sum(h => h.Or)           // ✅ 0
new List<Heros>().Count()                  // ✅ 0
new List<Heros>().Any()                    // ✅ false
new List<Heros>().All(h => h.Niveau > 5)   // ✅ true (!)
new List<Heros>().FirstOrDefault()         // ✅ null
new List<Heros>().MaxBy(h => h.Niveau)     // ✅ null
```

`Sum` de rien vaut `0`, logique. Mais la **moyenne** de rien n'existe pas, et le
**maximum** de rien non plus : d'où l'exception.

> 💡 `All` sur une collection vide rend `true`. Ça surprend, mais c'est
> mathématiquement correct : « tous les éléments de cet ensemble vide vérifient
> la condition » — il n'y en a aucun pour la contredire.

**Le réflexe** :

```csharp
if (!equipe.Any()) return 0;
return equipe.Average(h => h.Niveau);
```

---

## 9. La syntaxe alternative (pour info)

LINQ a une seconde écriture, proche du SQL :

```csharp
var resultat = from h in equipe
               where h.Niveau > 5
               orderby h.Nom
               select h.Nom;
```

Équivalent à :

```csharp
var resultat = equipe.Where(h => h.Niveau > 5).OrderBy(h => h.Nom).Select(h => h.Nom);
```

Les deux existent. **La seconde (dite « syntaxe de méthode ») est de loin la
plus utilisée** — c'est celle qu'on travaille dans ce module. Sache juste que
l'autre existe, tu la croiseras.

---

## 10. Les trois verbes universels : map, filter, reduce

Tu viens d'apprendre LINQ. Ce que tu ne sais pas encore, c'est que tu viens
d'apprendre **beaucoup plus que du C#**.

Ces trois opérations existent dans **tous** les langages modernes, sous des noms
différents :

| L'idée | C# | JavaScript | Python |
|--------|-----|-----------|--------|
| **map** — transformer chaque élément | `.Select()` | `.map()` | `map()` |
| **filter** — n'en garder que certains | `.Where()` | `.filter()` | `filter()` |
| **reduce** — tout réduire à une valeur | `.Aggregate()` | `.reduce()` | `reduce()` |

**Le jour où tu apprendras JavaScript ou Python, tu sauras déjà faire ça.** Les
noms changent, l'idée est la même. C'est un des concepts les plus transférables
de toute la programmation.

```csharp
// C#
equipe.Where(h => h.EstVivant).Select(h => h.Nom)
```
```javascript
// JavaScript — c'est le MÊME code
equipe.filter(h => h.estVivant).map(h => h.nom)
```

---

## 11. `Aggregate` : le reduce général

Tu connais déjà `Sum`, `Count`, `Average`, `Max`. Ce sont tous des **reduce
spécialisés** : ils prennent une collection et la réduisent à **une seule
valeur**.

`Aggregate`, c'est le reduce **générique** : celui qui permet de fabriquer tous
les autres.

```csharp
int somme = nombres.Aggregate((total, n) => total + n);
```

Comment ça marche : on prend les éléments **deux par deux**, en gardant le
résultat au fur et à mesure.

```
[3, 1, 4, 1, 5]

  total=3, n=1  →  4
  total=4, n=4  →  8
  total=8, n=1  →  9
  total=9, n=5  →  14      ✅
```

C'est **exactement** l'accumulateur du module 3 — mais en une ligne, et sans
écrire la boucle.

### Avec une valeur de départ (le *seed*)

```csharp
int somme = nombres.Aggregate(0, (total, n) => total + n);
//                            ↑ on part de 0
```

Deux différences, et elles comptent :

| | Sans seed | Avec seed |
|---|---|---|
| Valeur de départ | le **premier élément** | celle que tu donnes |
| Sur une collection **vide** | 💥 `InvalidOperationException` | ✅ rend le seed |
| Type du résultat | forcément celui des éléments | **ce que tu veux** |

Le seed permet de **changer de type** en chemin :

```csharp
// De List<Heros> vers un string
string noms = equipe.Aggregate("", (texte, h) => texte + h.Nom + " ");

// De List<Heros> vers un int
int orTotal = equipe.Aggregate(0, (total, h) => total + h.Or);
```

> ⚠️ **En vrai, utilise `Sum` quand `Sum` suffit.** `Aggregate` est plus
> puissant, mais moins lisible : personne ne devine au premier coup d'œil ce que
> fait `Aggregate(0, (a, b) => a + b)`, alors que `Sum()` se lit tout seul.
>
> `Aggregate` sert quand **aucune méthode spécialisée n'existe** pour ce que tu
> veux faire.

### Reconstruire les autres avec `Aggregate`

C'est un excellent exercice mental — ça montre que tout le reste n'est qu'un cas
particulier :

```csharp
nombres.Aggregate(0, (t, n) => t + n)                      // Sum
nombres.Aggregate(0, (t, n) => t + 1)                      // Count
nombres.Aggregate(1, (t, n) => t * n)                      // le produit
nombres.Aggregate((max, n) => n > max ? n : max)           // Max
mots.Aggregate("", (texte, m) => texte + m)                // string.Concat
```

---

## 12. Trois derniers outils

### `SelectMany` : aplatir

`Select` rend une liste **de listes**. `SelectMany` les **fusionne en une
seule**.

```csharp
// Chaque héros a une liste d'objets
equipe.Select(h => h.Objets)       // List<List<string>>  😩
equipe.SelectMany(h => h.Objets)   // List<string>        ✅
```

```
Thorin  : ["Épée", "Bouclier"]
Elyra   : ["Bâton", "Potion"]        SelectMany
Brunhild: ["Épée"]                   ─────────►  ["Épée", "Bouclier", "Bâton",
                                                  "Potion", "Épée"]
```

Dès que tu te retrouves avec une liste de listes, c'est `SelectMany` qu'il te
fallait.

### `Zip` : marier deux collections

Comme une fermeture éclair : il prend le 1er de chaque, le 2e de chaque, etc.

```csharp
var gauche = new[] { "Thorin", "Elyra" };
var droite = new[] { "Gobelin", "Orc" };

gauche.Zip(droite, (a, b) => $"{a} vs {b}")
// ["Thorin vs Gobelin", "Elyra vs Orc"]
```

⚠️ Il s'arrête à **la plus courte** des deux. Trois à gauche et deux à droite →
deux résultats, sans erreur.

### `Distinct` : supprimer les doublons

```csharp
new[] { "Épée", "Potion", "Épée" }.Distinct()    // ["Épée", "Potion"]
```

L'exercice 2 du module 6 (`SansDoublons`), en une méthode. 😄

> 💡 `Distinct` s'appuie sur `Equals` et `GetHashCode` (module 4, section 10) !
> Sur tes propres classes, il ne fonctionnera correctement que si tu les as
> redéfinis — ou si tu as utilisé un `record`. Tout est lié.

---

## 🎯 Récapitulatif

| Je veux... | LINQ |
|------------|------|
| Filtrer | `.Where(x => ...)` |
| Transformer | `.Select(x => ...)` |
| Trier | `.OrderBy(...)` / `.OrderByDescending(...)` |
| Compter | `.Count()` / `.Count(x => ...)` |
| Additionner | `.Sum(x => ...)` |
| Moyenne | `.Average(x => ...)` ⚠️ vide |
| L'élément le plus... | `.MaxBy(...)` / `.MinBy(...)` |
| Le premier (sûr) | `.FirstOrDefault(...)` |
| Y en a-t-il un ? | `.Any(x => ...)` |
| Tous ? | `.All(x => ...)` |
| Regrouper | `.GroupBy(x => ...)` |
| Matérialiser | `.ToList()` |
| Tout réduire à une valeur | `.Aggregate(seed, (acc, x) => ...)` |
| Aplatir une liste de listes | `.SelectMany(x => ...)` |
| Marier deux collections | `.Zip(autre, (a, b) => ...)` |
| Supprimer les doublons | `.Distinct()` |

**Les trois verbes universels** : `Select` = **map**, `Where` = **filter**,
`Aggregate` = **reduce**. Les mêmes en JavaScript, Python, Java…

**Une lambda** : `parametre => resultat`
**`Func<A, B>`** : prend un A, rend un B
**`Action<A>`** : prend un A, ne rend rien

---

## ▶️ À toi de jouer

`Exercices/Heros.cs` est déjà écrite. Remplis `Exercices/Exo.cs`.

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```

> 🎯 **Règle du module** : chaque exercice doit tenir en **une ou deux lignes**,
> sans `foreach`. Si tu écris une boucle, c'est que tu cherches la mauvaise
> méthode LINQ.
