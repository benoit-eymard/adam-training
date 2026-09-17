# Module 4 — Objet, les bases 🗡️

> **Objectif** : arrêter de manipuler des données éparpillées et créer tes
> **propres types**. C'est LE tournant du parcours.
>
> ⏱️ Lecture : ~30 min · Exercices : ~2h30

---

## 1. Le problème qu'on cherche à résoudre

Au module 3, pour gérer un héros, tu écrivais ça :

```csharp
string nom = "Kaelis";
int pv = 100;
int pvMax = 100;
int force = 12;

pv = SubirDegats(pv, 30);
string etat = Etat(pv, pvMax);
AfficherFiche(nom, pv, pvMax, force);
```

Ça marche. Mais regarde les problèmes qui s'accumulent :

**1. Les données sont éparpillées.** Rien ne dit que `nom`, `pv` et `force`
appartiennent au même héros. C'est dans ta tête, pas dans le code.

**2. Tu passes toujours les mêmes paramètres.** Chaque méthode a besoin de 3 ou
4 valeurs. Ajoute un `niveau`, et tu dois modifier **toutes** tes signatures.

**3. Et avec deux héros ?**

```csharp
string nom1 = "Kaelis"; int pv1 = 100; int force1 = 12;
string nom2 = "Thorin"; int pv2 = 150; int force2 = 18;
```

Avec dix héros, c'est ingérable. Avec un tableau par attribut
(`string[] noms, int[] pvs...`), c'est encore pire : il suffit d'un décalage
d'index pour que Thorin hérite des PV de Kaelis.

**4. Rien ne protège tes données.** N'importe qui peut écrire `pv = -9999;`.
Rien ne l'en empêche.

> 💡 **L'idée de l'objet** : regrouper **les données** ET **les actions qui vont
> avec** dans une seule entité, qui se protège elle-même.

---

## 2. La classe : un plan de construction

Une **classe** décrit ce qu'un objet **a** (ses données) et ce qu'il **sait
faire** (ses méthodes).

```csharp
public class Personnage
{
    // Ce qu'il A
    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }

    // Ce qu'il SAIT FAIRE
    public void SubirDegats(int degats)
    {
        PointsDeVie = Math.Max(0, PointsDeVie - degats);
    }
}
```

**La classe est le plan. L'objet est la maison.** Avec un plan, tu peux
construire autant de maisons que tu veux :

```csharp
Personnage kaelis = new Personnage("Kaelis", 100, 12);
Personnage thorin = new Personnage("Thorin", 150, 18);
```

Deux objets **indépendants**. `kaelis.SubirDegats(30)` ne touche pas à Thorin.

Et maintenant, regarde comme le code de tout à l'heure devient lisible :

```csharp
kaelis.SubirDegats(30);
Console.WriteLine(kaelis.Nom);
Console.WriteLine(kaelis.EstVivant);
```

Plus de paramètres qui traînent. Chaque personnage **porte** ses propres données.

---

## 3. Les propriétés

Une **propriété**, c'est une donnée de l'objet, avec un portail d'entrée et de
sortie.

```csharp
public string Nom { get; set; }
```

- `get` = on peut **lire** la valeur
- `set` = on peut **écrire** la valeur

### Contrôler qui a le droit d'écrire

```csharp
public int PointsDeVie { get; private set; }
```

`private set` signifie : **tout le monde peut lire, mais seul l'intérieur de la
classe peut modifier**.

```csharp
Console.WriteLine(kaelis.PointsDeVie);  // ✅ autorisé
kaelis.PointsDeVie = 9999;              // ❌ erreur de compilation !
kaelis.SubirDegats(30);                 // ✅ la SEULE porte d'entrée
```

C'est ça, l'**encapsulation** : l'objet contrôle lui-même comment ses données
changent. Personne ne peut le mettre dans un état incohérent.

> 🧠 **La question à se poser pour chaque propriété** : « est-ce que quelqu'un
> d'extérieur a une raison légitime de modifier ça directement ? » Si la réponse
> est non — et c'est souvent non — mets `private set`.

### `public` ou `private` ?

| | Visible depuis l'extérieur | Usage |
|---|---|---|
| `public` | ✅ | ce que l'objet expose au monde |
| `private` | ❌ | les rouages internes |

**Règle de départ** : mets tout en `private`, et n'ouvre en `public` que ce qui
doit vraiment l'être.

### Les propriétés calculées

Certaines données n'ont pas besoin d'être stockées : elles se **déduisent** des
autres.

```csharp
public bool EstVivant
{
    get { return PointsDeVie > 0; }
}

// Version courte, identique :
public bool EstVivant => PointsDeVie > 0;
```

Pas de `set`, pas de stockage : c'est recalculé à chaque lecture. Impossible
d'être désynchronisé — c'est exactement ce qu'on veut.

```csharp
public int PointsDeVieManquants => PointsDeVieMax - PointsDeVie;
public string Etat => PointsDeVie > 50 ? "En forme" : "Blessé";
```

---

## 4. Le constructeur

Le **constructeur** est la méthode appelée automatiquement au moment du `new`.
Son rôle : préparer l'objet pour qu'il soit **immédiatement utilisable**.

```csharp
public class Personnage
{
    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }
    public int PointsDeVieMax { get; private set; }
    public int Force { get; private set; }

    // ⬇️ LE CONSTRUCTEUR
    public Personnage(string nom, int pointsDeVieMax, int force)
    {
        Nom = nom;
        PointsDeVieMax = pointsDeVieMax;
        PointsDeVie = pointsDeVieMax;   // on démarre en pleine forme
        Force = force;
    }
}
```

**Ses deux règles, à retenir par cœur :**

1. Il porte **exactement le même nom que la classe**
2. Il n'a **aucun type de retour** — pas même `void`

```csharp
public Personnage(...)        // ✅ constructeur
public void Personnage(...)   // ❌ ce n'est PLUS un constructeur,
                              //    juste une méthode au nom trompeur
```

> 💡 Remarque `PointsDeVie = pointsDeVieMax;` : le constructeur ne se contente
> pas de recopier les paramètres, il peut **calculer** l'état initial. Un
> personnage neuf démarre à pleine vie, c'est une règle du jeu, et elle vit ici.

### Plusieurs constructeurs (surcharge)

```csharp
public Personnage(string nom) : this(nom, 100, 10)
{
    // un personnage par défaut : 100 PV, 10 de force
}
```

`: this(...)` appelle l'autre constructeur. Pratique pour éviter de dupliquer le
code d'initialisation.

---

## 5. Les méthodes d'un objet

Une méthode d'objet travaille **sur les données de son objet**. Pas besoin de
les lui passer en paramètre : elle les a déjà.

```csharp
public void SubirDegats(int degats)
{
    PointsDeVie = Math.Max(0, PointsDeVie - degats);
}

public void Soigner(int soins)
{
    if (!EstVivant)
    {
        return;    // on ne ressuscite pas les morts !
    }
    PointsDeVie = Math.Min(PointsDeVieMax, PointsDeVie + soins);
}
```

Compare avec le module 3 :

```csharp
// Avant : la donnée entre, la donnée sort, rien n'est protégé
int nouveauPv = SubirDegats(pv, 30);

// Maintenant : l'objet gère son propre état
kaelis.SubirDegats(30);
```

Remarque les deux **garde-fous** : `Math.Max(0, ...)` empêche les PV négatifs,
`Math.Min(PointsDeVieMax, ...)` empêche de dépasser le maximum. **Ces règles
vivent dans l'objet.** Personne ne peut les contourner, et personne n'a besoin
de s'en souvenir.

### Les objets se parlent entre eux

```csharp
public int Attaquer(Monstre cible)
{
    int degats = Force;
    cible.SubirDegats(degats);
    return degats;
}
```

Un `Personnage` reçoit un `Monstre` en paramètre et appelle **sa** méthode.
C'est ça, un programme objet : des objets qui s'envoient des messages.

```csharp
kaelis.Attaquer(gobelin);
gobelin.Attaquer(kaelis);
```

Ça se lit presque comme du français. C'est le but.

---

## 6. `this`

`this` désigne « l'objet en cours ». Il est **implicite** la plupart du temps :

```csharp
public void SubirDegats(int degats)
{
    PointsDeVie -= degats;        // sous-entendu : this.PointsDeVie
    this.PointsDeVie -= degats;   // strictement identique
}
```

Il devient **obligatoire** quand un paramètre porte le même nom qu'une
propriété :

```csharp
public Personnage(string nom)
{
    this.nom = nom;    // this.nom = le champ, nom = le paramètre
}
```

> 💡 C'est justement pour éviter cette confusion qu'on écrit les propriétés en
> **PascalCase** (`Nom`) et les paramètres en **camelCase** (`nom`). Avec cette
> convention, `Nom = nom;` est parfaitement clair.

---

## 7. `static` : ce qui appartient à la classe

Jusqu'ici tu écrivais `static` partout sans savoir pourquoi. Voici la réponse.

| | Sans `static` | Avec `static` |
|---|---|---|
| Appartient à | **chaque objet** | **la classe entière** |
| S'appelle avec | `kaelis.SubirDegats(10)` | `Personnage.CompterTotal()` |
| Exemple | les PV de Kaelis | le nombre total de personnages créés |

```csharp
public class Arme
{
    public string Nom { get; private set; }         // propre à CHAQUE arme
    public static int NombreCreees { get; private set; }  // commun à TOUTES

    public Arme(string nom)
    {
        Nom = nom;
        NombreCreees++;      // une seule variable, partagée
    }
}
```

```csharp
new Arme("Épée");
new Arme("Arc");
Console.WriteLine(Arme.NombreCreees);   // 2 — via la CLASSE, pas un objet
```

`Math.Max` est `static` : il n'y a aucune raison de créer un objet `Math`.
`Console.WriteLine` aussi.

> ⚠️ Une méthode `static` **ne peut pas** accéder aux données d'un objet — elle
> ne sait pas de quel objet on parle. C'est la cause de l'erreur
> « *an object reference is required for the non-static field* », que tu as
> peut-être déjà croisée.

---

## 8. Les objets sont des références ⚠️

Souviens-toi des tableaux du module 3. Même histoire, en plus important :

```csharp
Personnage a = new Personnage("Kaelis", 100, 12);
Personnage b = a;          // PAS une copie !

b.SubirDegats(50);
Console.WriteLine(a.PointsDeVie);   // 50 😱
```

`a` et `b` sont **deux étiquettes sur la même boîte**. Pour avoir deux
personnages distincts, il faut **deux `new`** :

```csharp
Personnage a = new Personnage("Kaelis", 100, 12);
Personnage b = new Personnage("Kaelis", 100, 12);  // un AUTRE Kaelis
```

Conséquence directe :

```csharp
a == b   // false ! Ce ne sont pas les mêmes boîtes,
         // même si leur contenu est identique.
```

### `null` : la boîte qui n'existe pas

```csharp
Arme armeEquipee = null;     // le personnage n'a pas d'arme
Console.WriteLine(armeEquipee.Nom);   // 💥 NullReferenceException
```

C'est **l'erreur la plus fréquente de toute la programmation**. Le réflexe :

```csharp
if (armeEquipee != null)
{
    Console.WriteLine(armeEquipee.Nom);
}
```

---

## 9. `ToString()` : comment l'objet se présente

Par défaut, afficher un objet donne quelque chose d'inutile :

```csharp
Console.WriteLine(kaelis);   // Module04.Personnage 🙄
```

On peut changer ça :

```csharp
public override string ToString()
{
    return $"{Nom} ({PointsDeVie}/{PointsDeVieMax} PV)";
}
```

```csharp
Console.WriteLine(kaelis);   // Kaelis (70/100 PV) 😎
```

Le mot `override` veut dire « je remplace le comportement par défaut ». C'est ta
première rencontre avec le **polymorphisme** — le grand sujet du module 5.

---

## 🎯 Récapitulatif

| Concept | Syntaxe |
|---------|---------|
| Définir une classe | `public class Personnage { }` |
| Propriété lisible/modifiable | `public int Force { get; set; }` |
| Propriété protégée en écriture | `public int PointsDeVie { get; private set; }` |
| Propriété calculée | `public bool EstVivant => PointsDeVie > 0;` |
| Constructeur | `public Personnage(string nom) { Nom = nom; }` |
| Créer un objet | `var p = new Personnage("Kaelis", 100, 12);` |
| Appeler une méthode | `p.SubirDegats(30);` |
| Donnée partagée par la classe | `public static int Compteur;` |
| Affichage personnalisé | `public override string ToString()` |

---

## ▶️ À toi de jouer

Ce module a **trois fichiers** à remplir, dans cet ordre :

1. `Exercices/Arme.cs` ⭐ — la plus simple, pour se faire la main
2. `Exercices/Monstre.cs` ⭐⭐
3. `Exercices/Personnage.cs` ⭐⭐⭐ — la plus complète

Un quatrième fichier, `Exercices/Potion.cs`, est **entièrement écrit** : c'est
ton **modèle**. Lis-le en premier, il contient tout ce dont tu as besoin.

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```

Puis [SOLUTIONS.md](SOLUTIONS.md) et le [DEFI.md](DEFI.md) : ton premier vrai
combat de RPG. ⚔️
