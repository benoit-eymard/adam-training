# 💡 Solutions — Module 4

> ⛔ **À lire après avoir essayé.**
> Ce module est un gros morceau : c'est normal d'y revenir plusieurs fois.

---

## Exercice 1 — La classe Arme

```csharp
public class Arme
{
    public string Nom { get; private set; }
    public int DegatsBonus { get; private set; }

    public static int NombreDArmesCreees { get; private set; }

    public Arme(string nom, int degatsBonus)
    {
        Nom = nom;
        DegatsBonus = degatsBonus;
        NombreDArmesCreees++;
    }

    public override string ToString() => $"{Nom} (+{DegatsBonus})";
}
```

**Trois points :**

1. **`Nom = nom;`** — majuscule à gauche (la propriété), minuscule à droite (le
   paramètre). C'est toute l'utilité de la convention de nommage : sans elle, il
   faudrait écrire `this.nom = nom;`.

2. **`NombreDArmesCreees++`** dans le constructeur. Comme il est `static`, il
   n'y a **qu'une seule** de ces variables pour tout le programme. Chaque `new`
   l'incrémente.

3. **`=>` pour `ToString()`** : c'est la forme courte d'une méthode à une seule
   ligne. `=> expression;` équivaut à `{ return expression; }`.

---

## Exercice 2 — La classe Monstre

```csharp
public class Monstre
{
    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }
    public int Degats { get; private set; }

    public bool EstVivant => PointsDeVie > 0;

    public Monstre(string nom, int pointsDeVie, int degats)
    {
        Nom = nom;
        PointsDeVie = pointsDeVie;
        Degats = degats;
    }

    public void SubirDegats(int degats)
    {
        PointsDeVie = Math.Max(0, PointsDeVie - degats);
    }

    public int Attaquer(Personnage cible)
    {
        if (!EstVivant)
        {
            return 0;      // un mort n'attaque pas
        }
        cible.SubirDegats(Degats);
        return Degats;
    }

    public override string ToString() => $"{Nom} ({PointsDeVie} PV)";
}
```

### 🧠 Pourquoi `EstVivant` est CALCULÉ et pas stocké

C'est le point le plus important du module. Imagine la version stockée :

```csharp
public bool EstVivant { get; private set; }   // ❌ mauvaise idée
```

Il faudrait alors penser à la mettre à jour **partout** :

```csharp
public void SubirDegats(int d)
{
    PointsDeVie = Math.Max(0, PointsDeVie - d);
    if (PointsDeVie == 0) EstVivant = false;   // à ne pas oublier...
}
```

Et le jour où tu ajoutes une méthode `Empoisonner()`, ou `Exploser()`, ou
`SubirDegatsDeZone()` — tu oublieras. Tu auras un monstre à 0 PV encore
« vivant », et tu passeras une soirée à chercher pourquoi.

Avec `=> PointsDeVie > 0`, **c'est impossible**. La vérité est calculée à chaque
lecture, à partir de la seule source qui compte.

> **La règle** : ne stocke jamais ce que tu peux déduire.

### Le garde-fou `if (!EstVivant) return 0;`

C'est une **sortie anticipée**. Elle place la règle métier (« les morts
n'attaquent pas ») là où elle est impossible à contourner : dans l'objet
lui-même.

---

## Exercice 3 — La classe Personnage

```csharp
public class Personnage
{
    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }
    public int PointsDeVieMax { get; private set; }
    public int Force { get; private set; }
    public Arme ArmeEquipee { get; private set; }

    public bool EstVivant => PointsDeVie > 0;
    public int PointsDeVieManquants => PointsDeVieMax - PointsDeVie;

    public int DegatsTotaux
    {
        get
        {
            if (ArmeEquipee == null)
            {
                return Force;
            }
            return Force + ArmeEquipee.DegatsBonus;
        }
    }

    public Personnage(string nom, int pointsDeVieMax, int force)
    {
        Nom = nom;
        PointsDeVieMax = pointsDeVieMax;
        PointsDeVie = pointsDeVieMax;   // on démarre en pleine forme
        Force = force;
        ArmeEquipee = null;             // mains nues
    }

    public void SubirDegats(int degats)
    {
        PointsDeVie = Math.Max(0, PointsDeVie - degats);
    }

    public void Soigner(int soins)
    {
        if (!EstVivant)
        {
            return;                     // pas de résurrection
        }
        PointsDeVie = Math.Min(PointsDeVieMax, PointsDeVie + soins);
    }

    public void Equiper(Arme arme)
    {
        ArmeEquipee = arme;
    }

    public int Attaquer(Monstre cible)
    {
        if (!EstVivant)
        {
            return 0;
        }
        int degats = DegatsTotaux;
        cible.SubirDegats(degats);
        return degats;
    }

    public int BoirePotion(Potion potion)
    {
        int soins = potion.Boire();
        Soigner(soins);
        return soins;
    }

    public override string ToString()
    {
        string arme = ArmeEquipee == null ? "mains nues" : ArmeEquipee.ToString();
        return $"{Nom} ({PointsDeVie}/{PointsDeVieMax} PV) — {arme}";
    }
}
```

### Le point délicat : `ArmeEquipee` peut être `null`

```csharp
return Force + ArmeEquipee.DegatsBonus;   // 💥 si pas d'arme
```

`null` veut dire « cette variable ne pointe sur aucun objet ». Lui demander
`.DegatsBonus`, c'est demander la couleur d'une voiture qui n'existe pas → le
programme s'arrête avec une **`NullReferenceException`**.

C'est, statistiquement, **l'erreur la plus fréquente de toute la
programmation**. Son inventeur, Tony Hoare, l'a lui-même appelée « mon erreur à
un milliard de dollars ».

Le réflexe : **tester avant d'utiliser**.

```csharp
if (ArmeEquipee == null) { ... }
```

> 💡 C# a des raccourcis pour ça, que tu croiseras bientôt :
> ```csharp
> return Force + (ArmeEquipee?.DegatsBonus ?? 0);
> ```
> `?.` veut dire « si ce n'est pas null, accède à... sinon donne null », et `??`
> veut dire « si c'est null, utilise cette valeur à la place ». Élégant, mais
> écris d'abord la version avec `if` : c'est elle qui te fait comprendre le
> problème.

### `BoirePotion` : la composition en action

```csharp
public int BoirePotion(Potion potion)
{
    int soins = potion.Boire();   // la potion gère SA logique
    Soigner(soins);               // le personnage gère LA SIENNE
    return soins;
}
```

Trois lignes, et pourtant deux objets qui collaborent parfaitement :

- `Potion.Boire()` sait décrémenter ses utilisations et retourner 0 si elle est
  vide. Le personnage n'a pas à le savoir.
- `Soigner()` sait plafonner au maximum et refuser de ressusciter. La potion n'a
  pas à le savoir.

**Chaque objet ne connaît que ses propres règles.** C'est exactement le but de
la programmation objet : au lieu d'un gros bloc de code qui sait tout, on a des
petites entités qui savent chacune une chose, et qui se parlent.

> 🧠 Un bon test : si tu ajoutes demain une `PotionEmpoisonnee` qui rend des PV
> négatifs, tu n'as **rien** à changer dans `Personnage`. C'est le signe que le
> découpage est bon.

### `ToString()` et l'opérateur ternaire

```csharp
string arme = ArmeEquipee == null ? "mains nues" : ArmeEquipee.ToString();
```

`condition ? siVrai : siFaux` — c'est un `if/else` qui tient sur une ligne et
qui **produit une valeur**. Pratique quand les deux branches sont courtes.
Dès que ça devient long, repasse à un vrai `if`.

---

## ✅ Bilan du module

Tu viens de franchir la plus grosse marche du parcours. Tu sais maintenant :

- créer tes propres **types** avec des classes
- **protéger** l'état d'un objet (`private set`, garde-fous)
- **déduire** plutôt que stocker (propriétés calculées)
- faire **collaborer** des objets
- gérer le cas `null`

### Ce qui coince encore

Imagine que tu veuilles un `Guerrier`, un `Mage` et un `Archer`. Ils partagent
tous : un nom, des PV, `SubirDegats`, `Soigner`, `EstVivant`... Vas-tu
copier-coller la classe `Personnage` trois fois ?

Et comment écrire une méthode `Combat(???, ???)` qui accepte **n'importe quel**
type de combattant ?

C'est exactement le problème que résout le **module 5** : l'**héritage** et le
**polymorphisme**. ⚔️
