# Module 2 — Décisions & boucles 🐣

> **Objectif** : ton programme va enfin **décider tout seul** et **répéter** des
> actions. C'est ce qui sépare une liste de courses d'un vrai programme.
>
> ⏱️ Lecture : ~25 min · Exercices : ~1h30

---

## 1. Les booléens : vrai ou faux, rien d'autre

Avant de décider, il faut savoir **poser une question**. En C#, une question a
toujours une réponse de type `bool` : `true` ou `false`.

```csharp
int pv = 87;

bool estVivant = pv > 0;        // true
bool estEnPleineForme = pv == 100;  // false
```

### Les opérateurs de comparaison

| Opérateur | Question posée |
|-----------|----------------|
| `==` | est **égal** à ? |
| `!=` | est **différent** de ? |
| `>` | est strictement **supérieur** à ? |
| `<` | est strictement **inférieur** à ? |
| `>=` | est supérieur **ou égal** ? |
| `<=` | est inférieur **ou égal** ? |

> 🔴 **LE piège classique** : `=` range une valeur, `==` compare.
> `pv = 0` met les PV à zéro. `pv == 0` demande « les PV sont-ils à zéro ? ».
> Confondre les deux est un rite de passage. Heureusement, le compilateur
> t'engueule la plupart du temps.

### Combiner des conditions

| Opérateur | Se lit | Vrai quand... |
|-----------|--------|---------------|
| `&&` | ET | **les deux** sont vraies |
| `\|\|` | OU | **au moins une** est vraie |
| `!` | NON | inverse le résultat |

```csharp
bool peutAttaquer = estVivant && aDeLEnergie;      // les deux
bool estVaincu = pv <= 0 || aFui;                  // l'un ou l'autre
bool estMort = !estVivant;                         // l'inverse
```

---

## 2. `if` : prendre une décision

```csharp
if (pv <= 0)
{
    Console.WriteLine("Tu es vaincu...");
}
```

Ça se lit : « **SI** les PV sont inférieurs ou égaux à 0, **ALORS** exécute ce
bloc. » Sinon, C# saute par-dessus les accolades et continue plus bas.

### `else` : sinon

```csharp
if (pv > 50)
{
    Console.WriteLine("Tu es en forme !");
}
else
{
    Console.WriteLine("Attention, tu es blessé.");
}
```

### `else if` : plusieurs cas

```csharp
if (pv > 75)
{
    Console.WriteLine("Pleine forme");
}
else if (pv > 40)
{
    Console.WriteLine("Un peu amoché");
}
else if (pv > 0)
{
    Console.WriteLine("Critique !");
}
else
{
    Console.WriteLine("Mort.");
}
```

⚠️ **L'ordre compte énormément.** C# teste les conditions **de haut en bas** et
s'arrête **à la première qui est vraie**. Si tu écrivais `pv > 0` en premier,
tous les autres cas deviendraient inatteignables : pour `pv = 90`, la première
condition serait déjà vraie.

> 🧠 **Règle à retenir** : dans une cascade de `if/else if`, va du cas **le plus
> restrictif** au cas **le plus large**.

### Le piège des accolades

```csharp
if (pv <= 0)
    Console.WriteLine("Mort");
    Console.WriteLine("Game over");  // 😱 s'exécute TOUJOURS !
```

Sans accolades, le `if` ne contrôle que **la ligne juste après**. L'indentation
te trompe les yeux, mais pas le compilateur.

**Mets toujours les accolades.** Toujours. Même pour une seule ligne.

---

## 3. `switch` : quand on compare une même valeur à plein de cas

Quand tu enchaînes « si c'est A... sinon si c'est B... sinon si c'est C », il
existe plus lisible :

```csharp
switch (classe)
{
    case "Guerrier":
        Console.WriteLine("Tu frappes fort !");
        break;
    case "Mage":
        Console.WriteLine("Tu lances un sort !");
        break;
    case "Archer":
        Console.WriteLine("Tu tires une flèche !");
        break;
    default:
        Console.WriteLine("Classe inconnue.");
        break;
}
```

- `case` = un cas possible
- `break` = « j'ai fini, je sors » (**obligatoire**, sinon erreur de compilation)
- `default` = « aucun des cas ci-dessus »

### La version moderne (plus courte)

```csharp
string action = classe switch
{
    "Guerrier" => "Tu frappes fort !",
    "Mage"     => "Tu lances un sort !",
    "Archer"   => "Tu tires une flèche !",
    _          => "Classe inconnue."   // le _ remplace default
};
```

Les deux formes sont correctes. Utilise celle que tu trouves la plus lisible.

> **`switch` ou `if` ?** `switch` quand tu compares **une seule variable** à des
> valeurs précises. `if` dès qu'il y a des intervalles (`pv > 40`) ou des
> conditions combinées.

---

## 4. `for` : répéter un nombre connu de fois

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine($"Tour {i}");
}
```

Affiche : `Tour 1`, `Tour 2`, ... `Tour 5`.

Les trois parties entre parenthèses, séparées par des `;` :

| Partie | Rôle | Quand ? |
|--------|------|---------|
| `int i = 1` | **initialisation** | une seule fois, au tout début |
| `i <= 5` | **condition de continuation** | testée **avant chaque** tour |
| `i++` | **incrément** | exécuté **après chaque** tour |

Déroulé pas à pas :

```
i = 1 → 1 <= 5 ? oui → on affiche "Tour 1" → i devient 2
i = 2 → 2 <= 5 ? oui → on affiche "Tour 2" → i devient 3
...
i = 5 → 5 <= 5 ? oui → on affiche "Tour 5" → i devient 6
i = 6 → 6 <= 5 ? NON → on sort de la boucle
```

### Variantes utiles

```csharp
for (int i = 10; i >= 1; i--)     // compter à l'envers : 10, 9, 8...
for (int i = 0; i < 20; i += 2)   // de 2 en 2 : 0, 2, 4, 6...
```

> 💡 Pourquoi `i` ? Par tradition (« index »). Pour des boucles imbriquées on
> continue avec `j`, puis `k`. Mais si un vrai nom est plus clair (`ligne`,
> `tour`, `joueur`), utilise-le.

---

## 5. `while` : répéter tant que...

Quand tu ne sais **pas à l'avance** combien de tours il faudra :

```csharp
int pv = 100;

while (pv > 0)
{
    pv -= 15;
    Console.WriteLine($"Touché ! Il reste {pv} PV");
}
Console.WriteLine("Vaincu !");
```

La condition est testée **avant** chaque tour. Si elle est fausse dès le départ,
le bloc n'est **jamais** exécuté.

### 🔴 La boucle infinie

```csharp
int i = 0;
while (i < 10)
{
    Console.WriteLine(i);
    // 😱 on a oublié i++ : i vaut toujours 0, la condition est
    //    éternellement vraie, le programme tourne pour l'éternité
}
```

**Ça t'arrivera.** Pour arrêter un programme emballé : `Ctrl + C` dans le
terminal.

Dans une boucle `while`, demande-toi toujours : **qu'est-ce qui, dans mon bloc,
va finir par rendre la condition fausse ?** S'il n'y a pas de réponse, c'est un
bug.

### `do...while` : au moins une fois

```csharp
string reponse;
do
{
    Console.Write("Continuer ? (oui/non) ");
    reponse = Console.ReadLine();
}
while (reponse != "non");
```

La condition est testée **à la fin** : le bloc s'exécute donc toujours au moins
une fois. Parfait pour un menu ou une demande de saisie.

---

## 6. `break` et `continue`

```csharp
for (int i = 1; i <= 10; i++)
{
    if (i == 5)
    {
        break;      // on QUITTE la boucle : 1, 2, 3, 4
    }
    Console.WriteLine(i);
}

for (int i = 1; i <= 10; i++)
{
    if (i % 2 != 0)
    {
        continue;   // on SAUTE ce tour : 2, 4, 6, 8, 10
    }
    Console.WriteLine(i);
}
```

- `break` → sortie immédiate de la boucle
- `continue` → passe au tour suivant sans finir celui-ci

---

## 7. Boucles imbriquées

Une boucle **dans** une boucle. Indispensable pour tout ce qui est en 2D :
grilles, tableaux, damiers.

```csharp
for (int ligne = 1; ligne <= 3; ligne++)
{
    for (int colonne = 1; colonne <= 4; colonne++)
    {
        Console.Write("*");
    }
    Console.WriteLine();  // fin de ligne
}
```

Affiche :
```
****
****
****
```

La boucle intérieure fait **un tour complet** à chaque tour de la boucle
extérieure. Ici : 3 × 4 = 12 étoiles.

---

## 8. Construire du texte petit à petit

Un motif que tu utiliseras beaucoup dans les exercices :

```csharp
string resultat = "";           // on part d'une chaîne vide

for (int i = 1; i <= 5; i++)
{
    resultat += i;              // on ajoute au fur et à mesure
    if (i < 5)
    {
        resultat += ", ";       // une virgule, sauf après le dernier
    }
}
// resultat vaut "1, 2, 3, 4, 5"
```

Le `if (i < 5)` évite la virgule en trop à la fin. C'est un détail bête qui fait
échouer beaucoup de tests — pense-y.

---

## 🎯 Récapitulatif

| Je veux... | J'utilise |
|------------|-----------|
| Faire un choix | `if` / `else if` / `else` |
| Comparer une valeur à des cas précis | `switch` |
| Répéter un nombre connu de fois | `for` |
| Répéter tant qu'une condition tient | `while` |
| Répéter au moins une fois | `do...while` |
| Sortir d'une boucle | `break` |
| Sauter un tour | `continue` |
| Parcourir une grille | deux `for` imbriqués |

---

## ▶️ À toi de jouer

```bash
dotnet run --project Exercices
```

Puis ouvre `Exercices/Exo.cs`, remplis les `// TODO:`, et vérifie :

```bash
dotnet test Tests
```

Ensuite : [SOLUTIONS.md](SOLUTIONS.md) puis le [DEFI.md](DEFI.md) — tu vas coder
ton premier vrai jeu. 🎮
