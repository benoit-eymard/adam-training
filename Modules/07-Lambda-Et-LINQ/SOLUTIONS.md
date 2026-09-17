# 💡 Solutions — Module 7

> ⛔ **À lire après avoir essayé.**
>
> Remarque la longueur de ce fichier comparée aux exercices : c'est normal.
> LINQ, c'est **peu de code et beaucoup d'idées**.

---

## Partie A — Les délégués

```csharp
public static int Appliquer(int valeur, Func<int, int> operation)
    => operation(valeur);

public static int AppliquerDeuxFois(int valeur, Func<int, int> operation)
    => operation(operation(valeur));

public static int CompterSi(List<int> nombres, Func<int, bool> critere)
{
    int compteur = 0;
    foreach (int n in nombres)
    {
        if (critere(n)) compteur++;
    }
    return compteur;
}
```

**`operation(valeur)`** : on appelle une variable comme une méthode, parce
qu'elle **contient** une méthode. C'est l'idée qui débloque tout le module.

`AppliquerDeuxFois` se lit de l'intérieur vers l'extérieur : on applique une
fois, puis on réapplique sur ce résultat.

### `CompterSi`, c'est `Count` fait main

```csharp
CompterSi(nombres, n => n % 2 == 0)     // ta version
nombres.Count(n => n % 2 == 0)          // celle de LINQ
```

**Identiques.** `Count` fait exactement ce que tu viens d'écrire — ce n'est
pas de la magie, juste une boucle que quelqu'un a écrite une fois pour toutes.
Toutes les méthodes LINQ sont comme ça.

---

## Partie B — Filtrer et transformer

```csharp
public static List<Heros> Vivants(List<Heros> equipe)
    => equipe.Where(h => h.PointsDeVie > 0).ToList();

public static List<string> Noms(List<Heros> equipe)
    => equipe.Select(h => h.Nom).ToList();

public static List<Heros> DeClasse(List<Heros> equipe, string classe)
    => equipe.Where(h => h.Classe == classe).ToList();
```

### `Where` et `Select` : la différence fondamentale

| | Ce que ça fait | Combien d'éléments en sortie | Quel type |
|---|---|---|---|
| `Where` | **filtre** | **moins** (ou autant) | **le même** |
| `Select` | **transforme** | **exactement autant** | **différent** |

```csharp
equipe.Where(h => ...)      // List<Heros> → List<Heros>, plus court
equipe.Select(h => h.Nom)   // List<Heros> → List<string>, même longueur
```

`Where` prend une lambda qui rend un `bool` (une question).
`Select` prend une lambda qui rend **ce qu'on veut** (une transformation).

> 💡 Remarque que `DeClasse` capture le paramètre `classe` dans sa lambda. Une
> lambda peut utiliser les variables de son entourage — on appelle ça une
> **fermeture** (*closure*). C'est très pratique, et c'est ce qui rend les
> lambdas si expressives.

---

## Partie C — Agréger

```csharp
public static int OrTotal(List<Heros> equipe)
    => equipe.Sum(h => h.Or);

public static double NiveauMoyen(List<Heros> equipe)
{
    if (equipe.Count == 0) return 0;
    return equipe.Average(h => h.Niveau);
}

public static Heros LePlusRiche(List<Heros> equipe)
    => equipe.MaxBy(h => h.Or);

public static int NombreDeVivants(List<Heros> equipe)
    => equipe.Count(h => h.EstVivant);
```

### ⚠️ Le piège `Average` sur liste vide

```csharp
new List<Heros>().Sum(h => h.Or)        // ✅ 0
new List<Heros>().Average(h => h.Or)    // 💥 InvalidOperationException
```

Pourquoi cette différence ? **La somme de rien vaut zéro**, c'est une définition
mathématique solide. **La moyenne de rien n'existe pas** — il faudrait diviser
par zéro. LINQ refuse de deviner à ta place, et c'est un bon choix : mieux vaut
une erreur claire qu'un résultat faux.

D'où la garde `if (equipe.Count == 0) return 0;`.

### `Max` vs `MaxBy` — à ne jamais confondre

```csharp
equipe.Max(h => h.Or)      // → 800  (le montant le plus élevé)
equipe.MaxBy(h => h.Or)    // → Kaelis (le HÉROS qui a ce montant)
```

Tu voudras **presque toujours** `MaxBy`. Avant .NET 6, il fallait écrire
`equipe.OrderByDescending(h => h.Or).FirstOrDefault()` — plus lent et plus long.

Bonus : `MaxBy` rend `null` sur une liste vide, alors que `Max` lève une
exception. Une garde de moins à écrire.

### `Count(critere)` plutôt que `Where(...).Count()`

```csharp
equipe.Count(h => h.EstVivant)              // ✅ un seul passage
equipe.Where(h => h.EstVivant).Count()      // 😐 marche, mais deux étapes
```

Beaucoup de méthodes LINQ acceptent un critère directement : `Count`, `First`,
`Any`, `All`, `Sum`... Prends le réflexe, c'est plus court **et** plus rapide.

---

## Partie D — Trier et tester

```csharp
public static List<Heros> TriesParNiveauDecroissant(List<Heros> equipe)
    => equipe.OrderByDescending(h => h.Niveau).ToList();

public static bool AuMoinsUn(List<Heros> equipe, string classe)
    => equipe.Any(h => h.Classe == classe);

public static bool TousVivants(List<Heros> equipe)
    => equipe.All(h => h.EstVivant);
```

**Ton tri à bulles du module 3 : une ligne.** Et `OrderBy` n'est pas seulement
plus court, il est aussi **beaucoup plus rapide** (il utilise un tri fusion, pas
un tri à bulles).

Ce qui ne rend pas le module 3 inutile, au contraire : maintenant tu **sais** ce
qui se passe derrière. Quelqu'un qui n'a jamais codé un tri ne comprend pas
pourquoi trier un million d'éléments coûte quelque chose.

### `Any` et `All` s'arrêtent tôt

```csharp
equipe.Any(h => h.Classe == "Mage")    // s'arrête au PREMIER mage trouvé
equipe.All(h => h.EstVivant)           // s'arrête au PREMIER mort trouvé
```

Ils ne parcourent pas toute la collection si la réponse est déjà connue. Sur une
grande liste, c'est énorme.

### `All` sur une liste vide rend `true`

Ça surprend tout le monde. « Tous les héros de cette équipe vide sont vivants »
est **vrai**, parce qu'aucun ne peut la contredire. Les mathématiciens appellent
ça la **vérité vide**. Les programmeurs appellent ça un bug de 2 heures quand on
l'oublie. 😄

---

## Partie E — Enchaîner

```csharp
public static List<string> NomsDesVivantsParOrdreAlphabetique(List<Heros> equipe)
    => equipe
        .Where(h => h.EstVivant)
        .OrderBy(h => h.Nom)
        .Select(h => h.Nom)
        .ToList();

public static List<Heros> LesPlusRiches(List<Heros> equipe, int n)
    => equipe.OrderByDescending(h => h.Or).Take(n).ToList();

public static int OrDesVivants(List<Heros> equipe)
    => equipe.Where(h => h.EstVivant).Sum(h => h.Or);

public static Dictionary<string, int> RepartitionParClasse(List<Heros> equipe)
    => equipe.GroupBy(h => h.Classe).ToDictionary(g => g.Key, g => g.Count());

public static string ClasseLaPlusRepresentee(List<Heros> equipe)
{
    var groupe = equipe.GroupBy(h => h.Classe).MaxBy(g => g.Count());
    return groupe?.Key;
}

public static string Rapport(List<Heros> equipe)
    => string.Join("\n", equipe
        .Where(h => h.EstVivant)
        .OrderByDescending(h => h.Niveau)
        .Select(h => $"{h.Nom} ({h.Classe}) — niveau {h.Niveau}"));
```

### L'ordre des étapes

**Filtrer → trier → transformer.** Toujours.

```csharp
.Where(...)      // 1. on réduit le nombre d'éléments
.OrderBy(...)    // 2. on trie ce qu'il reste (moins il y en a, mieux c'est)
.Select(...)     // 3. on garde juste ce qui nous intéresse
.ToList()        // 4. on matérialise
```

Trier avant de filtrer donne le même résultat mais travaille pour rien.

Et surtout, `Select` en dernier : une fois qu'on a transformé les héros en
chaînes, on a **perdu** les autres informations. On ne peut plus trier par
niveau.

### `OrDesVivants` : deux méthodes, une seule ligne

```csharp
equipe.Where(h => h.EstVivant).Sum(h => h.Or)
```

`Where` réduit, `Sum` agrège. Chaque méthode fait une chose, et on les compose.
C'est toute la philosophie de LINQ — et, plus largement, de la **programmation
fonctionnelle**.

> 💡 Astuce de virtuose (à connaître, pas forcément à utiliser) :
> ```csharp
> equipe.Sum(h => h.EstVivant ? h.Or : 0)
> ```
> Un seul passage au lieu de deux. Plus rapide, mais moins lisible. Sur 6
> héros, la différence est nulle — **choisis la lisibilité**.

### `GroupBy` : le concept le plus puissant du module

```csharp
equipe.GroupBy(h => h.Classe)
```

Ça fabrique des **paquets**. Chaque paquet :
- a une **`.Key`** — la valeur commune (`"Mage"`, `"Guerrier"`...)
- **contient** les éléments correspondants (on peut faire `.Count()`,
  `.Sum()`, `.Select()` dessus)

```csharp
foreach (var groupe in equipe.GroupBy(h => h.Classe))
{
    Console.WriteLine($"{groupe.Key} : {groupe.Count()} membres");
    Console.WriteLine($"  Or total : {groupe.Sum(h => h.Or)}");
    Console.WriteLine($"  Le plus fort : {groupe.MaxBy(h => h.Niveau).Nom}");
}
```

Tout le motif « compter des occurrences » du module 6 (10 lignes, un
dictionnaire, un `ContainsKey`) tient maintenant en :

```csharp
.GroupBy(h => h.Classe).ToDictionary(g => g.Key, g => g.Count())
```

### `groupe?.Key` — l'opérateur de navigation sûre

```csharp
return groupe?.Key;
```

Le `?.` se lit : « **si ce n'est pas null**, accède à `.Key` ; **sinon**, rends
`null` ». Il remplace :

```csharp
if (groupe == null) return null;
return groupe.Key;
```

C'est le raccourci qu'on avait évoqué au module 4. Maintenant que tu as écrit la
version longue une dizaine de fois, tu peux l'utiliser. 😄

### `string.Join` sur une requête LINQ

```csharp
string.Join("\n", equipe.Where(...).Select(...))
```

Pas besoin de `.ToList()` : `string.Join` sait consommer n'importe quelle
collection. Le problème du « `\n` en trop à la fin » du module 2 disparaît
complètement — `Join` ne met des séparateurs **qu'entre** les éléments.

Et sur une collection vide, il rend `""`. Le cas limite se gère tout seul. 🎁

---

## 🧠 Quand NE PAS utiliser LINQ

LINQ est formidable, mais ce n'est pas une religion.

**Évite-le quand :**

- **La requête devient illisible.** Si tu as 8 méthodes enchaînées avec des
  lambdas de 3 lignes, une boucle bien nommée sera plus claire.
- **Tu as besoin de l'index.** `for` reste plus direct.
- **Tu dois modifier la collection** pendant le parcours.
- **La performance est critique** dans une boucle serrée. LINQ alloue des objets
  intermédiaires ; une boucle manuelle n'en alloue aucun. (Mais mesure avant de
  t'inquiéter — dans 99 % des cas, ça n'a aucune importance.)

> **La règle** : LINQ pour décrire **ce que tu veux**. Une boucle quand tu dois
> contrôler précisément **comment**.

---

## ✅ Bilan du module

Tu sais maintenant :

- stocker un **comportement** dans une variable (`Func`, `Action`)
- écrire des **lambdas**
- **filtrer, transformer, trier, agréger, regrouper** en une ligne
- éviter les pièges (`Average` sur vide, exécution différée, `Max` vs `MaxBy`)

**Et surtout** : tu as changé de niveau de pensée. Tu ne dis plus à la machine
*comment* faire, tu lui dis *ce que tu veux*. C'est un cap que beaucoup de
développeurs mettent des années à franchir.

### La dernière marche

Il reste une chose que ton programme ne sait pas faire : **plusieurs choses à la
fois**.

Quand tu écris `File.ReadAllText`, ton programme **attend**, complètement figé.
Quand tu télécharges trois fichiers, tu les prends l'un après l'autre alors
qu'ils pourraient arriver ensemble. Quand tu affiches un timer pendant que le
joueur réfléchit... tu ne peux pas.

Le **module 8** — le dernier — te donne ce pouvoir : **threading et async**. ⚡
