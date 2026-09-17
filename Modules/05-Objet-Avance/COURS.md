# Module 5 — Objet, avancé ⚔️

> **Objectif** : arrêter de copier-coller des classes. Tu vas apprendre à
> **factoriser** ce qui est commun et à **spécialiser** ce qui diffère — puis à
> traiter des objets différents **de la même façon**.
>
> ⏱️ Lecture : ~30 min · Exercices : ~2h30

---

## 1. Le problème

Tu veux trois types de combattants. Naïvement :

```csharp
public class Guerrier { public string Nom; public int PointsDeVie; /* ... */ }
public class Mage     { public string Nom; public int PointsDeVie; /* ... */ }
public class Archer   { public string Nom; public int PointsDeVie; /* ... */ }
```

**Trois problèmes graves :**

1. **Le copier-coller.** `SubirDegats`, `Soigner`, `EstVivant` sont identiques
   dans les trois classes. Tu corriges un bug → tu dois le corriger trois fois.

2. **Impossible de les mélanger.** Comment écrire ceci ?
   ```csharp
   ??? [] equipe = { monGuerrier, monMage, monArcher };   // quel type ???
   ```

3. **Impossible d'écrire du code générique.**
   ```csharp
   static void FaireAttaquer(??? combattant)   // quel type ???
   ```

La solution tient en deux idées : **l'héritage** (factoriser le commun) et le
**polymorphisme** (traiter les différences uniformément).

---

## 2. L'héritage : « est un »

```csharp
public class Combattant          // la classe de BASE (le parent)
{
    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }

    public void SubirDegats(int degats) { /* ... */ }
}

public class Guerrier : Combattant   // la classe DÉRIVÉE (l'enfant)
{
    public int Force { get; private set; }
}
```

Le `:` se lit « **hérite de** ». Un `Guerrier` **est un** `Combattant` : il
récupère automatiquement `Nom`, `PointsDeVie`, `SubirDegats`... **sans une seule
ligne de copier-coller**.

```csharp
Guerrier thorin = new Guerrier("Thorin", 120, 15);
thorin.SubirDegats(30);            // méthode héritée de Combattant
Console.WriteLine(thorin.Force);   // propriété propre au Guerrier
```

> 🧠 **Le test « est un »** : avant d'utiliser l'héritage, dis la phrase à voix
> haute. « Un Guerrier **est un** Combattant » ✅. « Une Voiture **est un**
> Moteur » ❌ — là, c'est de la **composition** (une voiture **a un** moteur),
> et ça s'écrit avec une propriété, pas avec `:`.

### `base` : appeler le constructeur du parent

```csharp
public class Guerrier : Combattant
{
    public int Force { get; private set; }

    public Guerrier(string nom, int pointsDeVieMax, int force)
        : base(nom, pointsDeVieMax)      // ⬅️ le parent s'initialise d'abord
    {
        Force = force;                   // puis l'enfant ajoute sa part
    }
}
```

`: base(...)` est **obligatoire** dès que le parent n'a pas de constructeur sans
paramètres. Ordre d'exécution : le parent d'abord, l'enfant ensuite.

### `protected` : le troisième niveau de visibilité

| Modificateur | Accessible depuis... |
|--------------|---------------------|
| `public` | partout |
| `private` | **uniquement** la classe elle-même |
| `protected` | la classe **et ses classes filles** |

`protected` sert à partager quelque chose avec ses enfants sans l'exposer au
monde entier.

---

## 3. Le polymorphisme : une commande, plusieurs comportements

C'est le concept central du module. « Polymorphisme » = « plusieurs formes ».

### `virtual` et `override`

```csharp
public class Combattant
{
    public virtual string Decrire()          // "virtual" = redéfinissable
    {
        return $"{Nom} ({PointsDeVie} PV)";
    }
}

public class Guerrier : Combattant
{
    public override string Decrire()         // "override" = je redéfinis
    {
        return base.Decrire() + $" — Guerrier (force {Force})";
    }
}
```

- `virtual` sur le parent : « mes enfants ont le droit de changer ça »
- `override` sur l'enfant : « je change ça »
- `base.Decrire()` : « d'abord, fais ce que le parent aurait fait »

### ✨ La magie

```csharp
Combattant[] equipe = {
    new Guerrier("Thorin", 120, 15),
    new Mage("Elyra", 80, 40),
    new Archer("Sylas", 90, 12)
};

foreach (Combattant c in equipe)
{
    Console.WriteLine(c.Decrire());
}
```

La variable est de type `Combattant`. Pourtant, chaque objet exécute **sa
propre version** de `Decrire()`. C# choisit la bonne **à l'exécution**, selon le
type réel de l'objet.

```
Thorin (120/120 PV) — Guerrier (force 15)
Elyra (80/80 PV) — Mage (40/40 mana)
Sylas (90/90 PV) — Archer (12 flèches)
```

> 💡 **Pourquoi c'est énorme** : tu peux ajouter une classe `Voleur` demain.
> Cette boucle fonctionnera **sans être modifiée d'une seule ligne**. Le code qui
> utilise les objets n'a pas besoin de connaître leurs types.
>
> Compare avec la version sans polymorphisme :
> ```csharp
> if (c is Guerrier) { /* ... */ }
> else if (c is Mage) { /* ... */ }
> else if (c is Archer) { /* ... */ }   // et il faut penser à rajouter Voleur ici,
>                                        // et dans les 12 autres endroits...
> ```

---

## 4. `abstract` : obliger les enfants à répondre

Parfois, le parent **sait qu'une action existe** mais **ne peut pas la définir**.
Un `Combattant` attaque forcément — mais comment ? Ça dépend du type.

```csharp
public abstract class Combattant
{
    // Une méthode abstraite : pas de corps, juste un point-virgule.
    // "Tous mes enfants DOIVENT fournir leur version."
    public abstract int Attaquer(Combattant cible);

    // Le reste de la classe est normal
    public void SubirDegats(int degats) { /* ... */ }
}
```

**Deux conséquences :**

1. **On ne peut pas instancier une classe abstraite.**
   ```csharp
   Combattant c = new Combattant("X", 100);   // ❌ erreur de compilation
   ```
   Logique : un « combattant générique » n'existe pas. Seuls existent des
   guerriers, des mages, des archers.

2. **Chaque enfant est obligé de fournir `Attaquer`.** S'il oublie, ça ne
   compile pas. Le compilateur devient ton garde-fou.

### `abstract` ou `virtual` ?

| | `virtual` | `abstract` |
|---|---|---|
| A un corps par défaut | ✅ | ❌ |
| L'enfant **peut** redéfinir | ✅ | — |
| L'enfant **doit** redéfinir | ❌ | ✅ |

**En pratique** : `virtual` quand un comportement par défaut a du sens
(`Decrire`), `abstract` quand il n'en a aucun (`Attaquer`).

---

## 5. Les interfaces : un contrat

Une **interface** liste ce qu'une classe doit savoir faire, **sans dire
comment**. C'est un contrat, pas un plan.

```csharp
public interface ISoigneur
{
    int SoignerAllie(Combattant cible);   // pas de corps, jamais
}
```

Par convention, le nom commence par un **`I`** majuscule.

```csharp
public class Mage : Combattant, ISoigneur
{
    public int SoignerAllie(Combattant cible)
    {
        // le Mage remplit sa part du contrat
    }
}
```

`Mage` hérite de `Combattant` **et** signe le contrat `ISoigneur`.

### Pourquoi les interfaces existent

**1. On n'hérite que d'une seule classe, mais on signe autant de contrats qu'on
veut.**

```csharp
public class Paladin : Combattant, ISoigneur, IMontable, IBeni
```

**2. Elles relient des classes sans lien de parenté.** Un `Pretre`, une
`Fontaine` et une `Potion` n'ont rien à voir entre eux — mais tous les trois
peuvent soigner. Ils peuvent tous implémenter `ISoigneur`.

**3. Elles permettent de trier par capacité :**

```csharp
foreach (Combattant c in equipe)
{
    if (c is ISoigneur soigneur)       // "est-ce que celui-là sait soigner ?"
    {
        soigneur.SoignerAllie(blesse);
    }
}
```

`c is ISoigneur soigneur` fait deux choses d'un coup : il teste, **et** il range
l'objet dans une variable du bon type si le test réussit. Très pratique.

> 🧠 **Héritage ou interface ?**
> - Héritage = « **est un** » → partage du **code**
> - Interface = « **sait faire** » → partage d'une **capacité**
>
> Un Mage **est un** Combattant (héritage).
> Un Mage **sait soigner** (interface).

---

## 6. `is`, `as` et le transtypage

```csharp
Combattant c = new Mage("Elyra", 80, 40);

// Tester le type
if (c is Mage)           { /* ... */ }

// Tester ET ranger dans une variable (le plus pratique)
if (c is Mage elyra)     { Console.WriteLine(elyra.Mana); }

// Convertir sans risque : donne null si ça ne colle pas
Mage m = c as Mage;
if (m != null)           { /* ... */ }

// Convertir de force : 💥 plante si ça ne colle pas
Mage m2 = (Mage)c;
```

> ⚠️ **Attention** : si tu écris beaucoup de `if (x is Guerrier) ... else if
> (x is Mage) ...`, c'est en général le **signe qu'il te manque une méthode
> `virtual`**. Le polymorphisme est presque toujours préférable aux tests de
> type.

---

## 7. Le tableau d'ensemble

```
                 ┌─────────────────────┐
                 │  Combattant         │  abstract — on ne peut pas
                 │  (abstract)         │  en créer directement
                 ├─────────────────────┤
                 │ Nom, PointsDeVie    │  ← hérités par tous
                 │ SubirDegats()       │
                 │ Soigner()           │
                 │ EstVivant           │
                 │ abstract Attaquer() │  ← chacun DOIT le définir
                 │ virtual Decrire()   │  ← chacun PEUT le redéfinir
                 └──────────┬──────────┘
                            │
          ┌─────────────────┼─────────────────┐
          │                 │                 │
    ┌─────┴─────┐    ┌──────┴──────┐   ┌──────┴──────┐
    │ Guerrier  │    │    Mage     │   │   Archer    │
    ├───────────┤    ├─────────────┤   ├─────────────┤
    │ Force     │    │ Mana        │   │ Fleches     │
    │ EnRage    │    │ SoignerAllie│   │ Ramasser()  │
    └───────────┘    └──────┬──────┘   └─────────────┘
                            │
                     ┌──────┴──────┐
                     │  ISoigneur  │  interface — un contrat
                     └─────────────┘
```

---

## 🎯 Récapitulatif

| Concept | Syntaxe | Sens |
|---------|---------|------|
| Héritage | `class Guerrier : Combattant` | « est un » |
| Constructeur parent | `: base(nom, pv)` | initialiser la part héritée |
| Méthode redéfinissable | `public virtual ...` | « tu peux changer ça » |
| Redéfinition | `public override ...` | « je change ça » |
| Version du parent | `base.Decrire()` | « d'abord, fais comme lui » |
| Méthode obligatoire | `public abstract int Attaquer(...);` | « tu DOIS définir ça » |
| Classe non instanciable | `public abstract class ...` | un concept, pas un objet |
| Contrat | `public interface ISoigneur` | « sait faire » |
| Tester un type | `if (c is Mage m)` | test + rangement |
| Visible par les enfants | `protected` | entre `public` et `private` |

---

## ▶️ À toi de jouer

`Exercices/Combattant.cs` et `Exercices/ISoigneur.cs` sont **déjà écrits** :
lis-les en premier, c'est ton modèle.

À toi d'écrire, dans l'ordre :

1. `Guerrier.cs` ⭐
2. `Archer.cs` ⭐⭐
3. `Mage.cs` ⭐⭐ (+ l'interface `ISoigneur`)
4. `Utilitaires.cs` ⭐⭐⭐ — le polymorphisme en action

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```
