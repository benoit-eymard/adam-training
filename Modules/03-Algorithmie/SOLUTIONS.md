# 💡 Solutions — Module 3

> ⛔ **À lire après avoir essayé.**

---

## Exercice 1 — La somme

```csharp
public static int Somme(int[] nombres)
{
    int total = 0;
    foreach (int n in nombres)
    {
        total += n;
    }
    return total;
}
```

**Le motif de l'accumulateur.** Trois temps, toujours les mêmes :

1. une variable qui démarre à la valeur neutre (`0` pour une somme, `1` pour un
   produit)
2. une boucle qui la fait grandir
3. on retourne le résultat

Le tableau vide marche tout seul : la boucle ne fait aucun tour, on retourne
`0`. 🎁

---

## Exercice 2 — Le maximum

```csharp
public static int Maximum(int[] nombres)
{
    int max = nombres[0];
    for (int i = 1; i < nombres.Length; i++)
    {
        if (nombres[i] > max)
        {
            max = nombres[i];
        }
    }
    return max;
}
```

**Deux détails qui comptent :**

1. **On part de `nombres[0]`**, pas de `0`. Avec `{-5, -2, -9}`, partir de `0`
   donnerait `0` — une valeur qui n'existe même pas dans le tableau. Le test
   `Max_que_des_negatifs` est là exprès pour t'attraper. 😈

2. **La boucle démarre à `i = 1`**, pas à `0`. Comparer `nombres[0]` avec
   lui-même ne sert à rien. (Démarrer à `0` marche aussi, c'est juste un tour
   gaspillé.)

> 🧠 Ce motif — « je parie sur le premier, je change d'avis si je trouve mieux »
> — sert pour le maximum, le minimum, le plus court chemin, le meilleur coup aux
> échecs... C'est un classique absolu.

---

## Exercice 3 — La moyenne

```csharp
public static double Moyenne(int[] notes)
{
    if (notes.Length == 0)
    {
        return 0;
    }
    return (double)Somme(notes) / notes.Length;
}
```

**Trois choses :**

1. **Le garde-fou en premier.** Sans le `if`, un tableau vide provoquerait une
   division par zéro. En `double`, ça ne plante même pas : ça donne `NaN`
   (« Not a Number »), une valeur qui contamine ensuite tous tes calculs. Pire
   qu'un plantage.

2. **`Somme(notes)` réutilise ton exercice 1.** C'est exactement l'idée de la
   décomposition : chaque méthode s'appuie sur les précédentes.

3. **`(double)` devant** force le calcul en décimal. Ça s'appelle un **cast** :
   « traite cet `int` comme un `double` ». Sans lui, `7 / 2` redonnerait `3`.

---

## Exercice 4 — La recherche

```csharp
public static int IndexDe(int[] tableau, int valeur)
{
    for (int i = 0; i < tableau.Length; i++)
    {
        if (tableau[i] == valeur)
        {
            return i;
        }
    }
    return -1;
}
```

**Le `return i` à l'intérieur de la boucle** fait deux choses d'un coup : il
donne la réponse et il arrête la recherche. Inutile de continuer à chercher ce
qu'on a trouvé.

C'est aussi ce qui garantit qu'on retourne la **première** occurrence.

**Le `return -1` à la fin** n'est atteint que si la boucle est allée au bout
sans rien trouver. La convention `-1` = « absent » est universelle : tu la
retrouveras dans `string.IndexOf`, `List.IndexOf`, et dans la plupart des
langages.

---

## Exercice 5 — Inverser un tableau

```csharp
public static int[] Inverser(int[] tableau)
{
    int[] resultat = new int[tableau.Length];
    for (int i = 0; i < tableau.Length; i++)
    {
        resultat[i] = tableau[tableau.Length - 1 - i];
    }
    return resultat;
}
```

**Le calcul d'index**, vérifié sur `[10, 20, 30]` (`Length` = 3) :

| `i` | `Length - 1 - i` | `resultat[i]` reçoit |
|-----|------------------|----------------------|
| 0 | 2 | `30` |
| 1 | 1 | `20` |
| 2 | 0 | `10` |

→ `[30, 20, 10]` ✅

**Pourquoi un nouveau tableau ?** Parce que le test
`L_original_n_est_PAS_modifie` le vérifie. Une méthode qui modifie ce qu'on lui
donne est une source de bugs terrible : l'appelant ne s'y attend pas.

> 🧠 **Une bonne habitude** : par défaut, une méthode **calcule et retourne**
> plutôt qu'elle ne **modifie**. Si elle modifie, son nom doit le crier fort
> (`TrierSurPlace` plutôt que `Trier`).

**Variante** avec deux index qui se croisent (comme le palindrome) :

```csharp
int gauche = 0, droite = tableau.Length - 1;
while (gauche < droite) { /* échange */ gauche++; droite--; }
```

---

## Exercice 6 — Le tri à bulles

```csharp
public static int[] Trier(int[] tableau)
{
    // 1. On copie, pour ne pas toucher à l'original
    int[] tab = new int[tableau.Length];
    for (int i = 0; i < tableau.Length; i++)
    {
        tab[i] = tableau[i];
    }

    // 2. On trie la copie
    for (int tour = 0; tour < tab.Length - 1; tour++)
    {
        for (int i = 0; i < tab.Length - 1; i++)
        {
            if (tab[i] > tab[i + 1])
            {
                int temp = tab[i];
                tab[i] = tab[i + 1];
                tab[i + 1] = temp;
            }
        }
    }

    return tab;
}
```

**Comment ça marche**, sur `[3, 1, 2]` :

```
Tour 1 :  [3, 1, 2]  3 > 1 ? oui → échange → [1, 3, 2]
          [1, 3, 2]  3 > 2 ? oui → échange → [1, 2, 3]
Tour 2 :  [1, 2, 3]  1 > 2 ? non
          [1, 2, 3]  2 > 3 ? non
→ trié !
```

À chaque passage, la plus grande valeur restante « remonte » jusqu'à sa place
finale, comme une bulle dans l'eau. D'où le nom.

**Pourquoi `Length - 1` dans la boucle intérieure ?** Parce qu'on lit
`tab[i + 1]`. Si `i` allait jusqu'à `Length - 1`, alors `i + 1` vaudrait
`Length` → 💥 `IndexOutOfRangeException`.

**Pourquoi `Length - 1` tours ?** Dans le pire des cas (tableau à l'envers), il
faut autant de passages qu'il y a d'éléments, moins un.

> ⚠️ **Le tri à bulles est LENT.** Pour 1000 éléments, il fait environ un
> million de comparaisons. Les vrais tris (comme `Array.Sort`) en font environ
> 10 000. Mais le tri à bulles s'écrit en 10 lignes et se comprend en 5 minutes
> — c'est pour ça qu'on l'apprend en premier.

**Amélioration classique** : si un passage complet n'a provoqué aucun échange,
c'est que le tableau est déjà trié. On peut s'arrêter :

```csharp
bool echangeFait = true;
while (echangeFait)
{
    echangeFait = false;
    for (int i = 0; i < tab.Length - 1; i++)
    {
        if (tab[i] > tab[i + 1])
        {
            int temp = tab[i];
            tab[i] = tab[i + 1];
            tab[i + 1] = temp;
            echangeFait = true;
        }
    }
}
```

---

## Exercice 7 — Le palindrome

```csharp
public static bool EstPalindrome(string texte)
{
    string propre = texte.ToLower().Replace(" ", "");

    int gauche = 0;
    int droite = propre.Length - 1;

    while (gauche < droite)
    {
        if (propre[gauche] != propre[droite])
        {
            return false;      // une seule différence suffit
        }
        gauche++;
        droite--;
    }

    return true;               // on s'est croisés sans rien trouver
}
```

**Le motif des deux index qui se rapprochent.** On part des deux extrémités et
on avance vers le centre :

```
e s o p e ... e p o s e
↑                     ↑
gauche             droite     e == e ? oui, on continue

  ↑                 ↑
  gauche        droite         s == s ? oui, on continue
  ...
```

**Trois points clés :**

1. **`return false` dès la première différence.** Inutile de vérifier le reste :
   une seule paire qui cloche suffit à conclure. C'est ce qu'on appelle une
   **sortie anticipée**, et c'est presque toujours une bonne idée.

2. **`while (gauche < droite)`, pas `<=`.** Quand les deux index se rencontrent
   (mot de longueur impaire), le caractère du milieu n'a pas besoin d'être
   comparé avec lui-même.

3. **Les cas limites se gèrent tout seuls.** Chaîne vide : `droite` vaut `-1`,
   la condition `0 < -1` est fausse d'entrée → `true`. Un caractère : `0 < 0`
   est faux → `true`. Aucun `if` spécial nécessaire. 🎁

**Version plus courte** (mais qui construit une chaîne entière pour rien) :

```csharp
string propre = texte.ToLower().Replace(" ", "");
char[] lettres = propre.ToCharArray();
Array.Reverse(lettres);
return propre == new string(lettres);
```

---

---

# 🔁 Partie B — La récursivité

## Exercice 8 — La factorielle

```csharp
public static long Factorielle(int n)
{
    if (n <= 1)
    {
        return 1;              // CAS DE BASE
    }
    return n * Factorielle(n - 1);   // CAS RÉCURSIF
}
```

**Le squelette de toute méthode récursive**, et il ne change jamais :

1. le **cas de base** en premier — sinon on ne s'arrête jamais
2. le **cas récursif** ensuite, sur un problème **plus petit**

**Pourquoi `n <= 1` et pas `n == 1` ?** Pour que `Factorielle(0)` réponde `1`
(la convention mathématique) au lieu de partir vers l'infini négatif. Un cas de
base trop étroit est une cause classique de `StackOverflowException`.

> 💡 **Le raisonnement à adopter** — et il est contre-intuitif au début :
> **fais confiance à la récursion.** Quand tu écris `n * Factorielle(n - 1)`,
> ne cherche pas à dérouler dans ta tête les 5 appels suivants. Suppose
> simplement que `Factorielle(n - 1)` te donne la bonne réponse, et demande-toi
> juste : « qu'est-ce que j'en fais ? ». Si le cas de base est correct et que tu
> te rapproches de lui, ça marche.

---

## Exercice 9 — Fibonacci

```csharp
public static long Fibonacci(int n)
{
    if (n <= 1)
    {
        return n;              // DEUX cas de base d'un coup : 0→0 et 1→1
    }
    return Fibonacci(n - 1) + Fibonacci(n - 2);   // DEUX appels
}
```

`return n` règle élégamment les deux cas de base à la fois : pour `n = 0` il
rend `0`, pour `n = 1` il rend `1`. Exactement ce qu'on veut.

### ⚠️ Cette version est un piège magnifique

Elle est la traduction **exacte** de la définition mathématique. Elle est
limpide. Et elle est **catastrophiquement lente**.

Regarde `Fibonacci(5)` :

```
                    F(5)
              ┌──────┴──────┐
            F(4)           F(3)
          ┌──┴──┐        ┌──┴──┐
        F(3)   F(2)    F(2)   F(1)
       ┌─┴─┐   ┌─┴─┐   ┌─┴─┐
     F(2) F(1) F(1) F(0) F(1) F(0)
```

`F(3)` est calculé **2 fois**, `F(2)` **3 fois**, `F(1)` **5 fois**. Et ça
double à chaque étage :

| `n` | Appels |
|-----|--------|
| 10 | ~177 |
| 30 | ~2,7 millions |
| 40 | ~330 millions |
| 50 | ~40 **milliards** |

**La leçon** : élégant ne veut pas dire efficace. Un code magnifique peut être
inutilisable.

### La solution : la mémoïsation

On garde ce qu'on a déjà calculé (module 6 : `Dictionary` !) :

```csharp
private static Dictionary<int, long> _memo = new Dictionary<int, long>();

public static long FibonacciRapide(int n)
{
    if (n <= 1) return n;
    if (_memo.ContainsKey(n)) return _memo[n];       // déjà calculé ?

    long resultat = FibonacciRapide(n - 1) + FibonacciRapide(n - 2);
    _memo[n] = resultat;                             // on retient
    return resultat;
}
```

`Fibonacci(50)` passe de **plusieurs jours** à **instantané**. Trois lignes
ajoutées.

> 🧠 La mémoïsation est une des idées les plus rentables de l'informatique. Elle
> porte un nom savant — *programmation dynamique* — mais l'idée tient en une
> phrase : **ne calcule jamais deux fois la même chose**.

---

## Exercice 10 — La somme des chiffres

```csharp
public static int SommeDesChiffres(int n)
{
    if (n == 0)
    {
        return 0;
    }
    return n % 10 + SommeDesChiffres(n / 10);
}
```

Le modulo et la division entière du **module 1** reviennent, et ils font
exactement le travail :

| Étape | `n` | `n % 10` (le dernier chiffre) | `n / 10` (le reste) |
|-------|-----|------------------------------|---------------------|
| 1 | 123 | **3** | 12 |
| 2 | 12 | **2** | 1 |
| 3 | 1 | **1** | 0 |
| 4 | 0 | cas de base → 0 | — |

Total : `3 + 2 + 1 + 0` = **6** ✅

`n / 10` rapproche bien du cas de base : le nombre perd un chiffre à chaque
appel. Un nombre à 9 chiffres, c'est 10 appels. Aucun risque de débordement.

> 💡 Remarque que `SommeDesChiffres(0)` rend `0` — et c'est correct, sans aucun
> cas particulier à ajouter. Quand le cas de base est bien choisi, les cas
> limites se règlent tout seuls. On l'a déjà vu au module 2 avec le compte à
> rebours. 🎁

---

## Exercice 11 — Inverser un texte

```csharp
public static string InverserTexte(string texte)
{
    if (texte.Length == 0)
    {
        return "";
    }
    return texte[texte.Length - 1]
         + InverserTexte(texte.Substring(0, texte.Length - 1));
}
```

L'idée : **le dernier caractère d'abord**, puis l'inverse de tout le reste.

```
InverserTexte("abc")
   = 'c' + InverserTexte("ab")
   = 'c' + ('b' + InverserTexte("a"))
   = 'c' + ('b' + ('a' + InverserTexte("")))
   = 'c' + ('b' + ('a' + ""))
   = "cba"   ✅
```

**Deux pièges classiques :**

- `texte[texte.Length - 1]` : n'oublie pas le `- 1`. Le dernier index est
  `Length - 1`, jamais `Length`.
- `Substring(0, texte.Length - 1)` : « à partir de 0, sur `Length - 1`
  caractères » — donc tout sauf le dernier.

> ⚠️ En vrai, on n'inverse pas une chaîne comme ça : chaque appel **fabrique une
> nouvelle chaîne** (les `string` sont immuables, module 3 section 7), donc
> inverser un texte de 1000 caractères en crée 1000. La version à deux index de
> l'exercice 5 est bien plus efficace.
>
> Mais comme exercice de récursivité sur les chaînes, c'est parfait. **Savoir
> quand ne PAS utiliser un outil fait partie du métier.**

---

## ✅ Ce que la récursivité t'a appris

- Un problème peut se définir **en fonction de lui-même**
- Il faut **toujours** un cas de base, et **toujours** se rapprocher de lui
- La pile d'appels est **limitée** (~10 000 appels)
- Élégant ≠ efficace (Fibonacci !)
- **Fais confiance à la récursion** : suppose que l'appel plus petit marche

Tu la retrouveras partout : parcourir des dossiers, explorer un arbre, lire du
JSON imbriqué, les algorithmes de tri rapides (*quicksort*, *mergesort*), et
l'intelligence artificielle des jeux.

🐞 **Et si tu es perdu** : point d'arrêt + panneau **Call Stack**. Voir la
cascade d'appels empilée vaut tous les schémas du monde.

---

## ✅ Bilan du module

Tu sais maintenant **découper un problème** et manipuler des **collections de
données**. Ce sont les deux compétences qui font passer du « je sais écrire des
lignes de code » au « je sais construire un programme ».

Mais ton code commence à avoir un problème : tes données (nom, PV, force) et tes
fonctions qui les manipulent sont **séparées**. Tu passes sans arrêt les mêmes
paramètres d'une méthode à l'autre.

Le module 4 résout ça, et c'est **le grand tournant du parcours** : la
**programmation orientée objet**. Tu vas arrêter de manipuler des données
éparpillées, et commencer à créer tes propres **types**. 🗡️
