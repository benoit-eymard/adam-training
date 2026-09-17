# Module 3 — Algorithmie 🐤

> **Objectif** : arrêter d'écrire de gros blocs de code et apprendre à
> **découper**. Tu vas créer tes propres méthodes, manipuler des tableaux, et
> découvrir les algorithmes classiques : chercher, trier, inverser.
>
> ⏱️ Lecture : ~25 min · Exercices : ~2h

---

## 1. Un algorithme, c'est quoi ?

Une **recette**. Une suite d'étapes précises qui, partant de données en entrée,
produit un résultat en sortie.

Tu en connais déjà plein sans le savoir : une recette de cuisine, un itinéraire,
la méthode pour poser une division. La programmation, c'est apprendre à écrire
des recettes **si précises** qu'une machine peut les suivre sans réfléchir.

**La compétence centrale de ce module** : découper un gros problème en petits
problèmes que tu sais résoudre. Ça s'appelle la **décomposition**, et c'est
littéralement 80 % du métier.

> Exemple : « coder un Morpion » est trop gros. Mais :
> « afficher la grille », « demander un coup », « vérifier que la case est
> libre », « détecter une ligne gagnante », « changer de joueur » — ça, chaque
> morceau est faisable.

---

## 2. Créer ses propres méthodes

Jusqu'ici tu remplissais des méthodes existantes. Maintenant tu vas en écrire.

```csharp
static int Doubler(int nombre)
{
    return nombre * 2;
}
```

Anatomie :

| Morceau | Nom | Rôle |
|---------|-----|------|
| `static` | modificateur | on en reparle au module 4 |
| `int` | **type de retour** | ce que la méthode renvoie |
| `Doubler` | **nom** | en PascalCase, un **verbe** de préférence |
| `(int nombre)` | **paramètres** | ce qu'elle reçoit |
| `return nombre * 2;` | **corps** | ce qu'elle fait |

On l'appelle comme ça :

```csharp
int resultat = Doubler(21);   // resultat vaut 42
```

### Une méthode qui ne retourne rien : `void`

```csharp
static void AfficherTitre(string texte)
{
    Console.WriteLine("=== " + texte + " ===");
}

AfficherTitre("COMBAT");   // pas de = , on l'appelle juste
```

`void` veut dire « vide » : cette méthode **fait** quelque chose au lieu de
**calculer** quelque chose.

### Plusieurs paramètres

```csharp
static int Degats(int force, int bonusArme, bool coupCritique)
{
    int total = force + bonusArme;
    if (coupCritique)
    {
        total *= 2;
    }
    return total;
}
```

### `return` quitte immédiatement

```csharp
static string Verdict(int pv)
{
    if (pv <= 0)
    {
        return "Mort";      // on sort TOUT DE SUITE
    }
    return "Vivant";        // atteint seulement si pv > 0
}
```

Tout code écrit après un `return` exécuté est ignoré. C'est bien pratique : ça
évite des `else` inutiles.

---

## 3. Pourquoi découper ? Quatre raisons

**1. Ne pas se répéter.** Si tu copies-colles trois fois le même bloc et que tu
trouves un bug, tu dois le corriger trois fois. Et tu en oublieras un.

**2. Nommer, c'est expliquer.** Compare :

```csharp
if (t[0] == t[4] && t[4] == t[8] && t[0] != ' ')  // 🤨 euh ?
if (DiagonaleGagnante(grille))                     // 😌 ah, d'accord
```

Le deuxième se lit à voix haute. Une bonne méthode rend un commentaire inutile.

**3. Tester un morceau à la fois.** Un bug dans une méthode de 5 lignes se
trouve en 30 secondes. Dans une méthode de 200 lignes, ça prend une soirée.

**4. Réfléchir à un seul niveau.** Quand tu écris `AfficherGrille()`, tu n'as
pas à penser aux règles du jeu. Ton cerveau ne jongle qu'avec une chose.

> 🧠 **La règle du pouce** : si tu ne peux pas décrire ce que fait ta méthode
> en **une seule phrase sans « et »**, elle en fait trop. Découpe-la.

---

## 4. Les tableaux

Un **tableau** stocke plusieurs valeurs du même type dans une seule variable.

```csharp
int[] scores = new int[5];              // 5 cases, toutes à 0
int[] notes = { 12, 15, 8, 18, 10 };    // créé et rempli d'un coup
string[] noms = { "Kaelis", "Thorin" };
```

### Accéder aux cases

```csharp
Console.WriteLine(notes[0]);   // 12  — la PREMIÈRE case
Console.WriteLine(notes[4]);   // 10  — la dernière
notes[2] = 20;                 // on modifie la 3e case

Console.WriteLine(notes.Length);  // 5  — la taille
```

> 🔴 **Les index commencent à ZÉRO.** Un tableau de 5 cases a les index
> `0, 1, 2, 3, 4`. Il n'y a **pas** d'index 5.
>
> Écrire `notes[5]` fait planter le programme avec
> `IndexOutOfRangeException`. C'est l'erreur la plus courante de tout le métier,
> et elle t'arrivera cette semaine. 😄

### Parcourir un tableau

```csharp
// Avec for : tu as l'index, tu peux modifier les cases
for (int i = 0; i < notes.Length; i++)
{
    Console.WriteLine($"Note n°{i + 1} : {notes[i]}");
}

// Avec foreach : plus simple quand tu veux juste LIRE
foreach (int note in notes)
{
    Console.WriteLine(note);
}
```

**Quand utiliser lequel ?**

| | `for` | `foreach` |
|---|---|---|
| J'ai besoin de l'index | ✅ | ❌ |
| Je veux modifier les cases | ✅ | ❌ |
| Je veux juste lire | ✅ mais verbeux | ✅ idéal |

Note bien `i < notes.Length` et **pas** `i <= notes.Length` : avec `<=` tu
dépasses d'une case et tu plantes.

### Les tableaux sont des références ⚠️

```csharp
int[] a = { 1, 2, 3 };
int[] b = a;        // b et a désignent LE MÊME tableau !
b[0] = 99;
Console.WriteLine(a[0]);   // 99 😱
```

Un tableau n'est pas *copié* quand on l'affecte : les deux variables pointent
vers la même zone mémoire. Pour faire une vraie copie :

```csharp
int[] copie = new int[a.Length];
for (int i = 0; i < a.Length; i++)
{
    copie[i] = a[i];
}
// ou, plus court :
int[] copie2 = (int[])a.Clone();
```

C'est un concept important qui reviendra en force au module 4 avec les objets.

---

## 5. Les algorithmes classiques

Ces quatre motifs reviennent partout. Apprends-les une fois, réutilise-les mille
fois.

### La somme (accumulateur)

```csharp
int total = 0;                    // on part de l'élément neutre
foreach (int n in nombres)
{
    total += n;                   // on accumule
}
return total;
```

### Le maximum

```csharp
int max = nombres[0];             // on parie sur le premier
for (int i = 1; i < nombres.Length; i++)
{
    if (nombres[i] > max)
    {
        max = nombres[i];         // on change d'avis si on trouve mieux
    }
}
return max;
```

> ⚠️ Pourquoi partir de `nombres[0]` et pas de `0` ? Parce qu'avec des nombres
> tous négatifs (`{-5, -2, -9}`), partir de `0` donnerait `0` — qui n'est même
> pas dans le tableau. **Toujours partir d'une valeur réelle.**

### La recherche linéaire

```csharp
for (int i = 0; i < tableau.Length; i++)
{
    if (tableau[i] == valeurCherchee)
    {
        return i;                 // trouvé ! on sort tout de suite
    }
}
return -1;                        // parcouru en entier, pas trouvé
```

Le `-1` est la convention universelle pour « absent ». On ne peut pas retourner
`0`, qui est un index valide.

### Le tri à bulles

Le plus simple des tris. L'idée : on compare chaque paire de voisins et on les
échange s'ils sont dans le mauvais ordre. On recommence jusqu'à ce que plus rien
ne bouge — les grandes valeurs « remontent » comme des bulles.

```csharp
for (int tour = 0; tour < tab.Length - 1; tour++)
{
    for (int i = 0; i < tab.Length - 1; i++)
    {
        if (tab[i] > tab[i + 1])
        {
            // échanger les deux voisins
            int temporaire = tab[i];
            tab[i] = tab[i + 1];
            tab[i + 1] = temporaire;
        }
    }
}
```

**L'échange via une variable temporaire** mérite qu'on s'arrête dessus :

```csharp
int temp = a;
a = b;
b = temp;
```

Sans le `temp`, `a = b; b = a;` écraserait `a` avant de pouvoir le sauver — et
les deux vaudraient `b`. C'est comme échanger le contenu de deux verres pleins :
il te faut un troisième verre.

> 💡 En vrai, on utilise `Array.Sort(tab)` qui est bien plus rapide. Mais coder
> un tri au moins une fois dans sa vie, ça change ta façon de voir le code.

---

## 6. La récursivité : une méthode qui s'appelle elle-même

Voilà l'idée la plus déroutante de ce module — et l'une des plus puissantes de
toute la programmation.

**Une méthode a le droit de s'appeler elle-même.**

```csharp
static long Factorielle(int n)
{
    if (n <= 1)                          // le CAS DE BASE : on s'arrête
    {
        return 1;
    }
    return n * Factorielle(n - 1);       // le CAS RÉCURSIF : on s'appelle
}
```

`Factorielle(5)` vaut `5 × 4 × 3 × 2 × 1 = 120`. Et cette méthode de 5 lignes le
calcule sans la moindre boucle.

### Comment ça marche vraiment

Déroulons `Factorielle(4)` pas à pas. Chaque appel **met le précédent en
attente** :

```
Factorielle(4)  →  4 * Factorielle(3)        ⏸️ en attente
                        Factorielle(3)  →  3 * Factorielle(2)   ⏸️
                                                Factorielle(2)  →  2 * Factorielle(1)  ⏸️
                                                                        Factorielle(1)  →  1   ✅ CAS DE BASE
                                                2 * 1 = 2       ✅ on remonte
                                        3 * 2 = 6               ✅
                        4 * 6 = 24                              ✅
```

On **descend** jusqu'au cas de base, puis on **remonte** en calculant.

> 🐞 **Regarde-le en vrai.** Pose un point d'arrêt dans `Factorielle` et ouvre le
> panneau **Call Stack** ([DEBUG.md](../../DEBUG.md)) : tu verras les appels
> empilés les uns sur les autres. C'est le meilleur moyen de comprendre la
> récursivité — bien mieux que n'importe quel dessin.

### ⚠️ Les deux règles vitales

Toute méthode récursive a **obligatoirement** ces deux morceaux :

| Morceau | Rôle | Si tu l'oublies |
|---------|------|-----------------|
| **Le cas de base** | quand s'arrêter | 💥 `StackOverflowException` |
| **Le cas récursif** | se rappeler sur un problème **plus petit** | 💥 `StackOverflowException` |

**Le cas de base d'abord, toujours.** Si tu écris le `return n * Factorielle(n-1)`
avant le `if`, ta méthode ne s'arrêtera jamais.

Et le cas récursif doit **se rapprocher** du cas de base. `Factorielle(n - 1)`
descend vers 1 : bien. `Factorielle(n)` ou `Factorielle(n + 1)` : catastrophe.

```csharp
static int Boucle(int n)
{
    return Boucle(n - 1);    // 💥 pas de cas de base → crash immédiat
}
```

### 💥 Le StackOverflowException

Chaque appel en attente occupe de la place dans une zone mémoire appelée **la
pile** (*stack*). Elle n'est pas infinie : environ **10 000 appels** et elle
déborde.

C'est une des rares erreurs que `try/catch` **ne peut pas** rattraper : le
programme meurt sur le coup. Quand tu la vois, c'est presque toujours un cas de
base manquant ou mal écrit.

### Récursivité ou boucle ?

Tout ce qui s'écrit avec une récursion s'écrit aussi avec une boucle, et
inversement.

```csharp
// Récursif : élégant, proche de la définition mathématique
static long Factorielle(int n) => n <= 1 ? 1 : n * Factorielle(n - 1);

// Itératif : plus rapide, ne remplit pas la pile
static long FactorielleBoucle(int n)
{
    long resultat = 1;
    for (int i = 2; i <= n; i++) resultat *= i;
    return resultat;
}
```

| | Récursif | Boucle |
|---|---|---|
| Lisibilité | ✨ souvent magnifique | correcte |
| Vitesse | plus lent | plus rapide |
| Mémoire | remplit la pile | constante |
| Sur les **arbres** | 🏆 imbattable | pénible |

**Quand la récursivité gagne vraiment** : quand le problème est lui-même
récursif. Parcourir des dossiers (un dossier contient des dossiers), explorer un
arbre généalogique, analyser du JSON (un objet contient des objets), trouver la
sortie d'un labyrinthe. En boucle, ce sont des cauchemars ; en récursif, dix
lignes.

### Le piège de Fibonacci

```csharp
static long Fibonacci(int n)
{
    if (n <= 1) return n;                              // 0→0, 1→1
    return Fibonacci(n - 1) + Fibonacci(n - 2);        // deux appels !
}
```

C'est la définition mathématique exacte, et c'est magnifique. Mais **deux**
appels récursifs, c'est un arbre qui double à chaque étage :

| `n` | Nombre d'appels |
|-----|-----------------|
| 10 | ~177 |
| 30 | ~2,7 millions |
| 50 | ~40 **milliards** (des jours de calcul) |

`Fibonacci(5)` calcule `Fibonacci(3)` **deux fois**, `Fibonacci(2)` **trois
fois**… On recalcule sans arrêt les mêmes choses.

> 💡 La solution s'appelle la **mémoïsation** : garder les résultats déjà
> calculés dans un `Dictionary` (module 6 !) pour ne jamais refaire deux fois le
> même travail. Avec ça, `Fibonacci(50)` devient instantané. C'est une des idées
> les plus rentables de l'informatique.

---

## 7. Les chaînes sont des suites de caractères

```csharp
string mot = "Kaelis";

Console.WriteLine(mot.Length);        // 6
Console.WriteLine(mot[0]);            // 'K'
Console.WriteLine(mot[mot.Length - 1]); // 's'  — le dernier

foreach (char c in mot)
{
    Console.WriteLine(c);
}
```

Une `string` se parcourt comme un tableau de `char`. Quelques méthodes utiles :

```csharp
mot.ToUpper()          // "KAELIS"
mot.ToLower()          // "kaelis"
mot.Contains("ael")    // true
mot.Replace("a", "@")  // "K@elis"
mot.Trim()             // enlève les espaces au début et à la fin
mot.Substring(1, 3)    // "ael"  (à partir de l'index 1, sur 3 caractères)
```

> ⚠️ Toutes ces méthodes **ne modifient rien** : elles retournent une **nouvelle**
> chaîne. `mot.ToUpper();` tout seul ne sert à rien —
> il faut écrire `mot = mot.ToUpper();`.
>
> En C#, les `string` sont **immuables** : une fois créées, on ne peut plus les
> changer. On ne peut que fabriquer une nouvelle chaîne à partir de l'ancienne.

---

## 8. Méthode pour attaquer un problème dur

Quand tu bloques, applique cette recette :

1. **Écris le problème en français**, sur papier. Sans code.
2. **Fais-le à la main** sur un petit exemple. Note chaque étape.
3. **Trouve la règle** que tu as appliquée sans y penser.
4. **Traduis cette règle** en C#, une étape à la fois.
5. **Teste sur le petit exemple**, puis sur les cas bizarres :
   tableau vide, un seul élément, valeurs négatives, valeurs identiques.

Les **cas limites** de l'étape 5 sont ce qui distingue le code qui marche « chez
moi » du code qui marche vraiment.

---

## 🎯 Récapitulatif

| Je veux... | J'écris |
|------------|---------|
| Créer une méthode | `static int Nom(int param) { return ...; }` |
| Une méthode sans retour | `static void Nom() { ... }` |
| Créer un tableau vide | `int[] t = new int[10];` |
| Créer un tableau rempli | `int[] t = { 1, 2, 3 };` |
| Sa taille | `t.Length` |
| Le parcourir avec index | `for (int i = 0; i < t.Length; i++)` |
| Le parcourir en lecture | `foreach (int x in t)` |
| Échanger deux valeurs | `int tmp = a; a = b; b = tmp;` |
| Dire « pas trouvé » | `return -1;` |
| Une méthode récursive | un **cas de base** (`if`) + un **cas récursif** |
| Voir la cascade d'appels | panneau **Call Stack** du débogueur |

---

## ▶️ À toi de jouer

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```

Puis [SOLUTIONS.md](SOLUTIONS.md), et le [DEFI.md](DEFI.md) : tu vas coder un
**Morpion jouable à deux**. C'est ton plus gros projet jusqu'ici. 🎯
