# 💡 Solutions — Module 9

> ⛔ **À lire après avoir essayé.**

---

## Exercice 1 — `MonWhere` (le *filter*)

```csharp
public static IEnumerable<T> MonWhere<T>(this IEnumerable<T> source, Func<T, bool> critere)
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

**Six lignes.** C'est, à un détail près, le vrai code de `Where` dans .NET.

Ce qui est remarquable, c'est ce qu'il n'y a **pas** : aucune `List`, aucune
allocation, aucun stockage. La méthode ne fait que **relayer** les éléments qui
passent le test.

> 🧠 Et souviens-toi : parce qu'elle contient un `yield`, **l'appeler n'exécute
> rien**. Le `foreach` ci-dessus ne démarre que lorsque quelqu'un consomme le
> résultat. Toute l'exécution différée du module 7 tient dans cette seule
> propriété.

---

## Exercice 2 — `MonSelect` (le *map*)

```csharp
public static IEnumerable<TResultat> MonSelect<TSource, TResultat>(
    this IEnumerable<TSource> source, Func<TSource, TResultat> transformation)
{
    foreach (TSource element in source)
    {
        yield return transformation(element);
    }
}
```

**Les deux types génériques** sont le cœur de l'affaire :

```csharp
MonSelect<int, string>       // des int entrent, des string sortent
```

C'est ce qui permet d'écrire `heros.MonSelect(h => h.Nom)` et d'obtenir une
séquence de `string` à partir d'une séquence de `Heros`. C# **déduit** les deux
types tout seul à partir de la lambda — tu n'as jamais à les écrire.

**Différence avec `Where`** : `Select` ne filtre rien. Il sort **exactement
autant** d'éléments qu'il en entre, mais d'un autre type.

---

## Exercice 3 — `MonTake` et le piège de l'ordre

```csharp
public static IEnumerable<T> MonTake<T>(this IEnumerable<T> source, int nombre)
{
    if (nombre <= 0) yield break;

    int pris = 0;
    foreach (T element in source)
    {
        yield return element;
        pris++;
        if (pris >= nombre) yield break;
    }
}
```

### 🔴 Le détail qui fait toute la différence

Regarde ces deux versions. Elles donnent **le même résultat**. Une seule est
correcte.

```csharp
// ❌ Test AVANT : lit un élément de trop
foreach (T element in source)
{
    if (pris >= nombre) yield break;
    yield return element;
    pris++;
}

// ✅ Test APRÈS : lit exactement ce qu'il faut
foreach (T element in source)
{
    yield return element;
    pris++;
    if (pris >= nombre) yield break;
}
```

**Pourquoi ?** Parce qu'un `foreach` doit **demander** un élément à la source
avant de pouvoir entrer dans la boucle. Dans la version ❌, après avoir produit
le 3ᵉ élément, la boucle repart : elle réclame un 4ᵉ élément à la source, **et
c'est seulement là** qu'elle découvre qu'il faut s'arrêter.

Déroulé sur `MonTake(3)` :

| Version ❌ | Version ✅ |
|---|---|
| demande 0 → produit | demande 0 → produit |
| demande 1 → produit | demande 1 → produit |
| demande 2 → produit | demande 2 → produit, **stop** |
| **demande 3** → stop 😱 | — |
| **4 lectures** | **3 lectures** |

C'est exactement ce que mesure le test `MonTake_ne_lit_QUE_ce_qu_il_faut`.

### Est-ce que ça compte vraiment ?

Sur un tableau en mémoire : non, aucune importance.

Mais imagine que chaque élément soit **une requête réseau**, ou **une ligne lue
sur un disque**, ou **un calcul de 3 secondes**. Une lecture inutile à chaque
appel, c'est une requête gratuite envoyée à un serveur, pour rien.

> 🧠 **C'est ça, écrire du code paresseux correct** : ne jamais demander plus
> que le strict nécessaire. Le vrai `Take` de .NET fait exactement ce que tu
> viens d'écrire.

**Et le `if (nombre <= 0) yield break;`** : sans lui, avec le test *après*, la
boucle produirait le premier élément **avant** de vérifier — et `MonTake(0)`
rendrait un élément au lieu de zéro. Les garde-fous d'abord.

---

## Exercice 4 — `MonCount`

```csharp
public static int MonCount<T>(this IEnumerable<T> source)
{
    int total = 0;
    foreach (T element in source)
    {
        total++;
    }
    return total;
}
```

**Pas de `yield` ici** — et c'est capital.

`MonCount` ne rend pas une séquence, il rend **un nombre**. Il ne peut donc pas
être paresseux : pour savoir combien il y a d'éléments, il faut bien tous les
parcourir.

C'est une **opération terminale**. Comme `Sum`, `First`, `Max`, `ToList`. Ce sont
elles qui **déclenchent** le calcul de toute la chaîne paresseuse construite
avant.

```
MonWhere ──► MonSelect ──► MonTake ──► MonCount
└──────── paresseux ─────────────┘     └ terminal : ça part ! ┘
```

Le test `MonCount_lui_LIT_tout_et_c_est_normal` vérifie explicitement ce
comportement. Ce n'est pas un défaut : c'est la nature de l'opération.

> ⚠️ Conséquence directe : **ne fais jamais `.MonCount()` sur une séquence
> infinie.** Le programme tournerait pour l'éternité.

---

## Exercice 5 — `MonAggregate` (le *reduce*)

```csharp
public static T MonAggregate<T>(this IEnumerable<T> source, T depart, Func<T, T, T> operation)
{
    T accumulateur = depart;
    foreach (T element in source)
    {
        accumulateur = operation(accumulateur, element);
    }
    return accumulateur;
}
```

C'est **l'accumulateur du module 3**, rendu générique :

```csharp
int total = 0;                  // ← depart
foreach (int n in nombres)
{
    total = total + n;          // ← operation
}
return total;                   // ← accumulateur
```

La seule différence : le point de départ **et** l'opération sont fournis par
l'appelant. Le squelette, lui, ne change jamais.

### 🏆 Ce que tu viens de faire

Avec `MonWhere`, `MonSelect` et `MonAggregate`, tu as reconstruit **filter, map
et reduce** — les trois verbes du module 7, ceux qu'on retrouve dans tous les
langages.

Et tout le reste de LINQ s'exprime à partir de là :

```csharp
source.MonAggregate(0, (t, n) => t + n)                    // Sum
source.MonAggregate(0, (t, n) => t + 1)                    // Count
source.MonAggregate(1, (t, n) => t * n)                    // le produit
source.MonAggregate(int.MinValue, (m, n) => n > m ? n : m) // Max
```

LINQ n'est plus une boîte noire. C'est du code que **tu sais écrire**.

---

## Exercices 6 et 7 — Les générateurs finis

```csharp
public static IEnumerable<int> Compter(int debut, int fin)
{
    for (int i = debut; i <= fin; i++)
    {
        yield return i;
    }
}

public static IEnumerable<T> Repeter<T>(T valeur, int combien)
{
    for (int i = 0; i < combien; i++)
    {
        yield return valeur;
    }
}
```

Ici, il n'y a **pas de source** : la séquence est fabriquée à partir de rien.

`Compter(5, 1)` rend une séquence vide sans le moindre `if` : la boucle ne tourne
simplement pas. **Encore un cas limite qui se règle tout seul** quand la
structure est bien choisie — on l'a vu au module 2 avec le compte à rebours, au
module 3 avec la récursivité. C'est un motif récurrent.

---

## Exercices 8 et 9 — L'infini 🤯

```csharp
public static IEnumerable<int> Naturels()
{
    int n = 0;
    while (true)
    {
        yield return n;
        n++;
    }
}

public static IEnumerable<long> Fibonacci()
{
    long a = 0, b = 1;
    while (true)
    {
        yield return a;
        (a, b) = (b, a + b);
    }
}
```

### Pourquoi `while (true)` ne plante pas

Parce que **la méthode ne tourne pas toute seule**. Elle avance d'un cran
uniquement quand quelqu'un réclame l'élément suivant. Entre deux demandes, elle
est **gelée** — ses variables locales conservées, sa position mémorisée.

```
MonTake(5) demande  ──►  Naturels reprend, produit 0, se rendort
MonTake(5) demande  ──►  Naturels reprend, produit 1, se rendort
...
MonTake(5) a ses 5  ──►  Naturels ne sera plus jamais réveillé
```

La séquence est infinie **en droit**, pas en mémoire. Seuls les 5 éléments
demandés ont réellement existé.

### `(a, b) = (b, a + b)`

C'est une affectation de **tuple**. Tout le côté droit est calculé **d'abord**,
puis rangé d'un coup.

```csharp
(a, b) = (b, a + b);        // ✅ une seule étape

a = b;                      // ❌ ici a est déjà écrasé...
b = a + b;                  //    ...donc ce b est faux
```

C'est le problème de l'échange du module 3, réglé par le langage. Plus besoin de
variable temporaire.

### 🔴 Le danger à ne jamais oublier

```csharp
Naturels().MonTake(5).ToList();    // ✅ termine
Naturels().ToList();               // 🔴 tourne à l'infini, sature la RAM
Naturels().MonCount();             // 🔴 pareil
```

**Sur une séquence infinie, il faut TOUJOURS une borne** : `Take`, `First`, ou
`TakeWhile`. Si tu déclenches ça par accident, `Ctrl + C` dans le terminal.

### Fibonacci : la revanche du module 3

Souviens-toi de la version récursive :

```csharp
static long Fibonacci(int n)
{
    if (n <= 1) return n;
    return Fibonacci(n - 1) + Fibonacci(n - 2);   // ~40 milliards d'appels pour n=50
}
```

Elle recalculait sans arrêt les mêmes valeurs. La version générateur calcule
chaque nombre **exactement une fois**, en gardant simplement les deux
précédents — et `Fibonacci().MonTake(50)` est instantané.

Le test `Le_50e_est_INSTANTANE` est là pour te le faire constater.

> 🧠 **Même problème, trois solutions**, sur trois modules :
>
> | Approche | Coût pour n = 50 |
> |---|---|
> | Récursion naïve (module 3) | des jours |
> | Récursion + mémoïsation (module 3) | instantané, mais garde tout en mémoire |
> | Générateur `yield` (module 9) | instantané, et ne garde que 2 nombres |
>
> Il n'y a pas « une bonne façon » de coder. Il y a des compromis, et savoir les
> reconnaître est ce qui distingue un développeur expérimenté.

---

## ✅ Bilan du module

Tu sais maintenant :

- ce que `foreach` fait **réellement** (`GetEnumerator`, `MoveNext`, `Current`)
- écrire un **générateur** avec `yield return` et `yield break`
- **pourquoi** LINQ est différé — ce n'est pas magique, c'est `yield`
- distinguer les opérations **paresseuses** des opérations **terminales**
- écrire une **méthode d'extension** avec `this`
- manipuler des séquences **infinies** sans faire exploser la mémoire

Et surtout : **tu as réécrit LINQ.** Tu ne l'utiliseras plus jamais comme une
boîte noire.

### Là où tu vas le retrouver

- **Lire un fichier de 10 Go** ligne par ligne, sans jamais le charger en entier
- **Paginer** des résultats qui arrivent d'un serveur, page après page
- **Générer** des niveaux, des ennemis, des nombres aléatoires à la demande
- **`IAsyncEnumerable`** (section 7 du cours) : le même principe, mais avec des
  éléments qui arrivent du réseau — `yield` + `await`, les modules 8 et 9 réunis

> 💬 Le jour où tu liras du code professionnel et que tu tomberas sur un
> `yield return` au milieu d'une méthode, tu sauras exactement ce qui se passe.
> Beaucoup de développeurs en poste ne le savent pas. 🔬
