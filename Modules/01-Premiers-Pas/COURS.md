# Module 1 — Premiers pas 🥚

> **Objectif** : à la fin de ce module, tu sauras stocker des informations, faire
> des calculs, et dialoguer avec l'utilisateur dans la console.
>
> ⏱️ Lecture : ~20 min · Exercices : ~1h

---

## 1. Un programme, c'est quoi ?

Un programme, c'est une **liste d'ordres** exécutés **dans l'ordre, de haut en bas**.
L'ordinateur ne devine rien, n'improvise rien, ne corrige rien. Il fait exactement
ce que tu écris — y compris tes erreurs.

C'est une excellente nouvelle : quand ça ne marche pas, ce n'est jamais « la
machine qui bug ». C'est toujours une instruction mal écrite. Et une instruction
mal écrite, ça se trouve.

Le plus petit programme C# :

```csharp
Console.WriteLine("Bonjour !");
```

Une instruction. Elle se termine par un point-virgule `;`, comme une phrase se
termine par un point. Oublier le `;` est l'erreur numéro 1 des débutants — et
tu la feras. Souvent. Tout le monde la fait.

---

## 2. Les variables : des boîtes étiquetées

Une **variable**, c'est une boîte dans laquelle tu ranges une valeur, avec une
étiquette dessus pour la retrouver.

```csharp
int age = 13;
```

Décomposons :

| Morceau | Rôle |
|---------|------|
| `int` | le **type** : quel genre de chose la boîte peut contenir (ici, un nombre entier) |
| `age` | le **nom** de la boîte (l'étiquette) |
| `=` | « range dedans » — ce n'est **pas** le « égal » des maths |
| `13` | la **valeur** rangée |

Une fois la boîte créée, tu peux changer son contenu :

```csharp
int age = 13;
age = 14;              // bon anniversaire
Console.WriteLine(age); // affiche 14
```

⚠️ Remarque bien : on n'écrit `int` **qu'une seule fois**, à la création. Après,
la boîte existe, on se contente de la remplir.

---

## 3. Les types principaux

En C#, **une boîte a un type et elle le garde à vie**. On ne peut pas mettre du
texte dans une boîte à nombres. Cette rigueur est pénible au début, puis elle
devient ta meilleure alliée : elle attrape la moitié de tes erreurs avant même
que le programme ne démarre.

```csharp
int    niveau     = 5;             // nombre entier
double pointsDeVie = 87.5;         // nombre à virgule
string nom        = "Kaelis";      // du texte (entre guillemets DOUBLES)
char   initiale   = 'K';           // UN seul caractère (guillemets SIMPLES)
bool   estVivant  = true;          // vrai ou faux, rien d'autre
```

### Les pièges à connaître tout de suite

**Piège 1 — la division entière.**

```csharp
int resultat = 7 / 2;     // donne 3, PAS 3.5 !
double vrai  = 7.0 / 2.0; // donne 3.5
```

Quand les deux nombres sont des `int`, C# fait une division **d'entiers** et jette
la virgule. Si tu veux des décimales, il faut au moins un `double` dans le calcul.

**Piège 2 — la virgule des décimaux s'écrit avec un point.**

```csharp
double prix = 19.99;   // ✅ point
double prix = 19,99;   // ❌ erreur de compilation
```

**Piège 3 — `"5"` n'est pas `5`.**
Le premier est du texte, le second un nombre. `"5" + "3"` donne `"53"`, pas `8`.

### Le raccourci `var`

Quand le type est évident, on peut laisser C# le deviner :

```csharp
var nom = "Kaelis";   // C# comprend tout seul : string
var pv  = 100;        // C# comprend tout seul : int
```

`var` ne veut **pas** dire « type libre » : la boîte a bien un type, c'est juste
C# qui l'écrit à ta place. Au début, écris les types en toutes lettres : ça aide à
réfléchir.

---

## 4. Afficher dans la console

```csharp
Console.WriteLine("Bonjour");  // affiche puis passe à la ligne
Console.Write("Bonjour");      // affiche SANS passer à la ligne
```

### Coller du texte et des variables ensemble

Trois façons, de la pire à la meilleure :

```csharp
string nom = "Kaelis";
int niveau = 5;

// 1. La concaténation avec + — vite illisible
Console.WriteLine(nom + " est niveau " + niveau + ".");

// 2. L'interpolation avec $ — LA bonne façon ⭐
Console.WriteLine($"{nom} est niveau {niveau}.");

// 3. Sur plusieurs lignes, avec \n
Console.WriteLine($"Nom : {nom}\nNiveau : {niveau}");
```

Retiens la **numéro 2**. Le `$` devant les guillemets active l'interpolation :
tout ce que tu mets entre `{ }` est remplacé par sa valeur. C'est plus court, plus
lisible, et tu ne te trompes plus dans les espaces.

**Les caractères spéciaux** (dans une chaîne, `\` introduit un code) :

| Code | Effet |
|------|-------|
| `\n` | retour à la ligne |
| `\t` | tabulation |
| `\"` | un vrai guillemet |
| `\\` | un vrai antislash |

---

## 5. Lire ce que tape l'utilisateur

```csharp
Console.Write("Quel est ton nom ? ");
string nom = Console.ReadLine();
Console.WriteLine($"Salut {nom} !");
```

`Console.ReadLine()` attend que l'utilisateur tape quelque chose et appuie sur
Entrée, puis renvoie ce qu'il a tapé — **toujours sous forme de texte**.

Donc pour récupérer un nombre, il faut **convertir** :

```csharp
Console.Write("Ton âge ? ");
string saisie = Console.ReadLine();
int age = int.Parse(saisie);        // texte → nombre entier
```

Ou en une ligne :

```csharp
int age = int.Parse(Console.ReadLine());
```

⚠️ `int.Parse` **plante** si l'utilisateur tape « bonjour ». On verra comment
gérer ça proprement au module 6. Pour l'instant, on fait confiance.

Pour les décimaux : `double.Parse(...)`.

---

## 6. Les calculs

```csharp
int a = 10, b = 3;

int somme    = a + b;   // 13
int diff     = a - b;   // 7
int produit  = a * b;   // 30
int quotient = a / b;   // 3  (division entière !)
int reste    = a % b;   // 1  (le "modulo" : le reste de la division)
```

Le **modulo `%`** a l'air anecdotique mais il est partout. Il répond à la question
« quel est le reste ? ». Deux usages que tu utiliseras des centaines de fois :

```csharp
if (n % 2 == 0)  // n est pair
if (n % 5 == 0)  // n est un multiple de 5
```

### Les raccourcis

```csharp
score = score + 10;   // version longue
score += 10;          // même chose, en plus court
score -= 5;
score *= 2;

vies++;               // +1 (incrémenter)
vies--;               // -1 (décrémenter)
```

### La priorité des opérations

Comme en maths : `*` et `/` avant `+` et `-`. En cas de doute, **mets des
parenthèses**. Elles ne coûtent rien et rendent ton intention évidente.

```csharp
double moyenne = (note1 + note2 + note3) / 3.0;
```

---

## 7. Bien nommer, c'est déjà bien coder

```csharp
int x = 87;     // 😐 x, c'est quoi ?
int pv = 87;    // 🙂 mieux
int pointsDeVie = 87;  // 😃 aucun doute possible
```

Les conventions C# :

- **camelCase** pour les variables : `pointsDeVie`, `nomDuJoueur`
- **PascalCase** pour les méthodes et les classes : `CalculerDegats`, `Personnage`
- Pas d'accents, pas d'espaces, pas de tirets dans les noms
- Un nom qui décrit **ce que c'est**, pas sa longueur

> Une règle de pro : tu écris ton code une fois, tu le relis cent fois. Optimise
> pour la relecture.

---

## 🎯 Récapitulatif

| Je veux... | J'écris |
|------------|---------|
| Créer une variable | `int age = 13;` |
| Afficher du texte | `Console.WriteLine("Salut");` |
| Afficher avec des variables | `Console.WriteLine($"J'ai {age} ans");` |
| Lire du texte | `string s = Console.ReadLine();` |
| Lire un nombre | `int n = int.Parse(Console.ReadLine());` |
| Un reste de division | `a % b` |
| Ajouter 1 | `compteur++;` |

---

## ▶️ À toi de jouer

1. **Lance la démo** pour voir tout ça en action :

```bash
dotnet run --project Exercices
```

2. **Ouvre** `Exercices/Exo.cs` et remplis les `// TODO:`

3. **Vérifie** ton travail :

```bash
dotnet test Tests
```

4. Compare avec [SOLUTIONS.md](SOLUTIONS.md), puis attaque le [DEFI.md](DEFI.md).
