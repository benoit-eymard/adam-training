# 💡 Solutions — Module 5

> ⛔ **À lire après avoir essayé.**

---

## Exercice 1 — Le Guerrier

```csharp
public class Guerrier : Combattant
{
    public int Force { get; private set; }
    public bool EnRage { get; private set; }

    public Guerrier(string nom, int pointsDeVieMax, int force)
        : base(nom, pointsDeVieMax)
    {
        Force = force;
    }

    public void EntrerEnRage()
    {
        EnRage = true;
    }

    public override int Attaquer(Combattant cible)
    {
        if (!EstVivant) return 0;

        int degats = EnRage ? Force * 2 : Force;
        EnRage = false;                  // la rage retombe
        cible.SubirDegats(degats);
        return degats;
    }

    public override string Decrire()
    {
        return $"{base.Decrire()} — Guerrier (force {Force})";
    }
}
```

**Regarde ce que cette classe ne contient PAS** : ni `Nom`, ni `PointsDeVie`, ni
`SubirDegats`, ni `Soigner`, ni `EstVivant`. Tout ça vient de `Combattant`,
gratuitement. **C'est ça, l'héritage.**

### L'ordre dans `Attaquer` compte

```csharp
int degats = EnRage ? Force * 2 : Force;   // 1. on LIT la rage
EnRage = false;                            // 2. on l'éteint
cible.SubirDegats(degats);                 // 3. on frappe
```

Si tu écris `EnRage = false;` en premier, tu perds le bonus. Un bug classique :
**on ne peut pas remettre à zéro une valeur avant de l'avoir utilisée.**

### `base.Decrire()`

```csharp
return $"{base.Decrire()} — Guerrier (force {Force})";
        └──────┬───────┘
      "Thorin (120/120 PV)"
```

Le parent fait sa part, l'enfant complète. Si demain tu changes le format dans
`Combattant`, **les trois classes filles suivent automatiquement**.

Sans `base.`, tu aurais dupliqué `$"{Nom} ({PointsDeVie}/{PointsDeVieMax} PV)"`
trois fois. Et tu aurais oublié d'en changer une.

> ⚠️ Attention : à l'intérieur de `Decrire()`, écrire `Decrire()` tout court
> s'appellerait **lui-même**, à l'infini → `StackOverflowException`. Le `base.`
> n'est pas décoratif.

---

## Exercice 2 — L'Archer

```csharp
public class Archer : Combattant
{
    public const int DegatsFleche = 15;
    public const int DegatsCorpsACorps = 3;

    public int Fleches { get; private set; }

    public bool APlusDeFleches => Fleches > 0;

    public Archer(string nom, int pointsDeVieMax, int fleches)
        : base(nom, pointsDeVieMax)
    {
        Fleches = fleches;
    }

    public void Ramasser(int nombre)
    {
        if (nombre <= 0) return;
        Fleches += nombre;
    }

    public override int Attaquer(Combattant cible)
    {
        if (!EstVivant) return 0;

        int degats;
        if (APlusDeFleches)
        {
            Fleches--;
            degats = DegatsFleche;
        }
        else
        {
            degats = DegatsCorpsACorps;
        }

        cible.SubirDegats(degats);
        return degats;
    }

    public override string Decrire()
    {
        return $"{base.Decrire()} — Archer ({Fleches} flèches)";
    }
}
```

### Les constantes plutôt que les nombres magiques

```csharp
degats = 15;                 // 😐 pourquoi 15 ? d'où ça sort ?
degats = DegatsFleche;       // 😃 évident
```

Un « nombre magique », c'est une valeur en dur dont personne ne sait ce qu'elle
représente. En la nommant, tu te donnes **un seul endroit** à modifier pour
équilibrer le jeu — et ton code s'explique tout seul.

### Le garde-fou de `Ramasser`

```csharp
if (nombre <= 0) return;
```

Sans lui, `Ramasser(-10)` **retirerait** 10 flèches. L'objet doit se protéger
contre les usages absurdes : c'est exactement l'idée de l'encapsulation.

---

## Exercice 3 — Le Mage

```csharp
public class Mage : Combattant, ISoigneur
{
    public const int CoutDuSort = 10;
    public const int DegatsDuSort = 25;
    public const int DegatsBaton = 5;
    public const int CoutDuSoin = 15;
    public const int PointsDeSoin = 30;

    public int Mana { get; private set; }
    public int ManaMax { get; private set; }

    public Mage(string nom, int pointsDeVieMax, int manaMax)
        : base(nom, pointsDeVieMax)
    {
        ManaMax = manaMax;
        Mana = manaMax;          // plein au départ, comme les PV
    }

    public override int Attaquer(Combattant cible)
    {
        if (!EstVivant) return 0;

        int degats;
        if (Mana >= CoutDuSort)
        {
            Mana -= CoutDuSort;
            degats = DegatsDuSort;
        }
        else
        {
            degats = DegatsBaton;
        }

        cible.SubirDegats(degats);
        return degats;
    }

    public int SoignerAllie(Combattant cible)
    {
        if (!EstVivant) return 0;
        if (Mana < CoutDuSoin) return 0;
        if (cible == null || !cible.EstVivant) return 0;
        if (cible.PointsDeVieManquants == 0) return 0;

        int rendus = Math.Min(PointsDeSoin, cible.PointsDeVieManquants);
        Mana -= CoutDuSoin;
        cible.Soigner(PointsDeSoin);
        return rendus;
    }

    public override string Decrire()
    {
        return $"{base.Decrire()} — Mage ({Mana}/{ManaMax} mana)";
    }
}
```

### Les quatre gardes de `SoignerAllie`

```csharp
if (!EstVivant) return 0;
if (Mana < CoutDuSoin) return 0;
if (cible == null || !cible.EstVivant) return 0;
if (cible.PointsDeVieManquants == 0) return 0;
```

Ce motif s'appelle des **clauses de garde** (*guard clauses*). On élimine tous
les cas impossibles **en premier**, puis le reste de la méthode ne traite que le
cas normal.

Compare avec la version imbriquée :

```csharp
if (EstVivant) {
    if (Mana >= CoutDuSoin) {
        if (cible != null && cible.EstVivant) {
            if (cible.PointsDeVieManquants > 0) {
                // le vrai code, perdu à 4 niveaux d'indentation 😵
            }
        }
    }
}
```

Les gardes gardent le code **plat** et lisible. Prends cette habitude tôt.

### Le calcul AVANT l'action

```csharp
int rendus = Math.Min(PointsDeSoin, cible.PointsDeVieManquants);  // 1. on calcule
Mana -= CoutDuSoin;                                               // 2. on paie
cible.Soigner(PointsDeSoin);                                      // 3. on agit
return rendus;
```

Il **faut** calculer `rendus` avant d'appeler `Soigner`, sinon
`PointsDeVieManquants` aura déjà changé et on retournerait 0.

> 💡 On passe `PointsDeSoin` (30) à `Soigner`, pas `rendus`. Pourquoi ? Parce que
> `Soigner` **plafonne déjà tout seul** au maximum — c'est son travail, pas le
> nôtre. Chaque méthode fait respecter ses propres règles, et on ne les duplique
> pas.

### Deux parents ? Non : une classe + un contrat

```csharp
public class Mage : Combattant, ISoigneur
```

C# n'autorise **qu'une seule** classe de base (pour éviter des ambiguïtés
insolubles), mais **autant d'interfaces qu'on veut**. La classe de base vient
toujours en premier dans la liste.

---

## Exercice 4 — Le polymorphisme

```csharp
public static int NombreDeVivants(Combattant[] equipe)
{
    int compteur = 0;
    foreach (Combattant c in equipe)
    {
        if (c.EstVivant) compteur++;
    }
    return compteur;
}

public static Combattant LePlusBlesse(Combattant[] equipe)
{
    Combattant resultat = null;
    foreach (Combattant c in equipe)
    {
        if (!c.EstVivant) continue;
        if (resultat == null || c.PointsDeVieManquants > resultat.PointsDeVieManquants)
        {
            resultat = c;
        }
    }
    return resultat;
}

public static int AttaqueGroupee(Combattant[] equipe, Combattant cible)
{
    int total = 0;
    foreach (Combattant c in equipe)
    {
        total += c.Attaquer(cible);
    }
    return total;
}

public static int NombreDeSoigneurs(Combattant[] equipe)
{
    int compteur = 0;
    foreach (Combattant c in equipe)
    {
        if (c is ISoigneur) compteur++;
    }
    return compteur;
}

public static int SoinDUrgence(Combattant[] equipe)
{
    Combattant blesse = LePlusBlesse(equipe);
    if (blesse == null) return 0;

    foreach (Combattant c in equipe)
    {
        if (!c.EstVivant) continue;
        if (c is ISoigneur soigneur)
        {
            if (blesse == c) return 0;      // il ne se soigne pas lui-même
            return soigneur.SoignerAllie(blesse);
        }
    }
    return 0;
}
```

### ✨ La ligne la plus importante du module

```csharp
total += c.Attaquer(cible);
```

**Une seule ligne.** Et pourtant :

- si `c` est un `Guerrier`, C# exécute `Guerrier.Attaquer` (la force)
- si `c` est un `Mage`, C# exécute `Mage.Attaquer` (le sort et le mana)
- si `c` est un `Archer`, C# exécute `Archer.Attaquer` (les flèches)

C# regarde le **type réel** de l'objet **au moment de l'exécution** et choisit la
bonne méthode. Ça s'appelle la **liaison tardive** (*late binding*).

**Écris une classe `Voleur : Combattant` demain** : `AttaqueGroupee` la gérera
sans être modifiée d'un caractère. C'est ce qui rend le code objet
extensible — et c'est pour ça que tout le monde l'utilise.

### `LePlusBlesse` : pourquoi partir de `null` ?

Au module 3, pour le maximum, on partait de `nombres[0]`. Ici c'est impossible :
`equipe[0]` pourrait être **mort**, et on ne doit pas le retourner.

Alors on part de `null` (« je n'ai encore aucun candidat ») et on teste
`resultat == null ||` en premier. L'ordre du `||` est important : C# évalue de
gauche à droite et **s'arrête dès qu'il peut conclure**. Si `resultat` est
`null`, la seconde partie n'est jamais évaluée — ce qui évite un
`NullReferenceException`.

> 💡 Ça s'appelle l'**évaluation court-circuit**, et c'est un outil quotidien :
> ```csharp
> if (arme != null && arme.DegatsBonus > 5)   // sûr
> if (arme.DegatsBonus > 5 && arme != null)   // 💥 trop tard
> ```

### `is ISoigneur` plutôt que `is Mage`

```csharp
if (c is ISoigneur) compteur++;     // ✅ « sait-il soigner ? »
if (c is Mage) compteur++;          // ❌ « est-ce un mage ? »
```

La première version compte aussi les `Paladin`, `Pretre`, `Barde`... que tu
ajouteras plus tard. **On teste la capacité, jamais le type concret.**

C'est la différence entre demander « sais-tu conduire ? » et « es-tu chauffeur
de taxi ? ». La première question est la bonne.

---

## ✅ Bilan du module

Tu maîtrises maintenant les quatre piliers de la programmation objet :

| Pilier | Ce que c'est | Vu au module |
|--------|--------------|--------------|
| **Abstraction** | ne montrer que l'essentiel | 4 |
| **Encapsulation** | protéger son état | 4 |
| **Héritage** | factoriser le commun | 5 |
| **Polymorphisme** | un ordre, plusieurs réponses | 5 |

C'est le socle de C#, Java, Python, TypeScript, Swift... Ce que tu viens
d'apprendre se transpose presque mot pour mot dans tous ces langages.

### La suite

Ton équipe est un `Combattant[]` — un tableau de taille **fixe**. Comment
ajouter un membre en cours de partie ? Comment en retirer un ? Comment
sauvegarder la partie pour la reprendre demain ? Et que se passe-t-il si le
joueur tape « bonjour » au lieu d'un nombre ?

Le **module 6** répond à tout ça : **collections, erreurs et fichiers**. 🎒
