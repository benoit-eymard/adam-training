# 🐞 Le débogueur — arrête de deviner

> Jusqu'ici, quand ton code ne marche pas, tu ajoutes des `Console.WriteLine`
> partout pour voir ce qui se passe. Ça fonctionne… mais c'est lent, ça salit ton
> code, et tu finis toujours par en oublier un.
>
> Le **débogueur** fait infiniment mieux : il met ton programme **en pause** et
> te laisse regarder à l'intérieur, ligne par ligne, variable par variable.
>
> ⏱️ Lecture : ~10 min · À lire dès le module 2

---

## 1. La différence, en une image

```csharp
// Avec des Console.WriteLine : tu DEVINES
Console.WriteLine("ici 1");
Console.WriteLine($"cuivre = {cuivre}");
int argentTotal = cuivre / 100;
Console.WriteLine($"argentTotal = {argentTotal}");   // 😩
```

```csharp
// Avec le débogueur : tu REGARDES
int argentTotal = cuivre / 100;   // 🔴 point d'arrêt ici
```

Le programme s'arrête sur cette ligne, et VS Code t'affiche **la valeur de
toutes tes variables**, sans que tu aies rien écrit.

---

## 2. Poser un point d'arrêt

Un **point d'arrêt** (*breakpoint*), c'est un panneau STOP que tu poses sur une
ligne de code.

👉 **Clique dans la marge, juste à gauche du numéro de ligne.** Un gros point
rouge 🔴 apparaît.

*(raccourci clavier : `F9` quand le curseur est sur la ligne)*

Pour l'enlever : reclique dessus.

---

## 3. Lancer le débogage

### La méthode la plus utile : déboguer un test qui échoue

C'est **celle que tu utiliseras le plus** — parce que tes tests sont déjà là
pour te dire ce qui est cassé.

1. Ouvre le panneau **Testing** (l'icône bécher 🧪 dans la barre de gauche)
2. Trouve ton test rouge
3. **Clic droit** dessus → **Debug Test**

Le test s'exécute et s'arrête sur ton point d'arrêt. Tu vois exactement les
valeurs que le test a passées à ta méthode. 🎯

### L'autre méthode : lancer la démo

1. Appuie sur **`F5`**
2. VS Code te demande quelle configuration lancer → choisis
   **« Module X — démo »**

*(les configurations sont déjà prêtes dans le dépôt, tu n'as rien à créer)*

---

## 4. La barre de contrôle

Quand le programme est en pause, une petite barre apparaît en haut de l'écran :

| Bouton | Touche | Ce que ça fait |
|--------|--------|----------------|
| **Continue** | `F5` | repart jusqu'au prochain point d'arrêt |
| **Step Over** | `F10` | exécute la ligne et passe à la suivante |
| **Step Into** | `F11` | **entre dans** la méthode appelée sur cette ligne |
| **Step Out** | `Maj+F11` | finit la méthode courante et remonte |
| **Stop** | `Maj+F5` | tout arrêter |

> 💡 **`F10` est celle que tu utiliseras 90 % du temps.** Tu avances d'une ligne,
> tu regardes les variables, tu avances encore. C'est tout.
>
> **`F11`** sert quand la ligne appelle **ta** méthode et que tu veux voir
> dedans. Attention : sur une ligne comme `Console.WriteLine(...)`, `F11`
> t'emmène dans les entrailles de .NET — ce n'est pas ce que tu veux.
> `Maj+F11` pour ressortir.

---

## 5. Regarder à l'intérieur

### Le survol à la souris

**Le plus simple de tous** : passe ta souris au-dessus d'une variable. Sa valeur
s'affiche dans une bulle. Immédiat.

### Le panneau Variables

En haut à gauche pendant le débogage. Il liste **toutes** les variables
accessibles, avec leur valeur, mises à jour à chaque pas.

Sur un objet, clique sur la petite flèche pour **déplier** son contenu :

```
▼ heros            {Kaelis (100/100 PV) — mains nues}
    Nom            "Kaelis"
    PointsDeVie    100
    PointsDeVieMax 100
    Force          12
    ArmeEquipee    null          ← ah, voilà le problème
```

### Le panneau Watch (surveillance)

Tu peux y taper **n'importe quelle expression** et la voir évaluée en direct :

```
cuivre / 100          →  123
cuivre % 100          →  45
notes.Length          →  5
heros.EstVivant       →  true
```

**C'est redoutable pour les calculs.** Tu testes ta formule *avant* de l'écrire
dans ton code.

👉 Panneau **Watch** → bouton `+` → tape ton expression.

### La pile d'appels (Call Stack)

Elle répond à : **« comment suis-je arrivé ici ? »**

```
Exo.ConvertirEnOr(int)      ← tu es ici
Program.<Main>$(string[])   ← c'est lui qui t'a appelé
```

Clique sur une ligne pour remonter et voir les variables de l'appelant.
C'est **indispensable** avec la récursivité : tu vois toute la cascade d'appels
empilée, les uns sur les autres.

---

## 6. Un exercice guidé (5 minutes)

Fais-le une fois pour de vrai. Tu ne reviendras plus en arrière.

1. Ouvre `Modules/01-Premiers-Pas/Exercices/Exo.cs`
2. Écris la solution de `ConvertirEnOr` (ou prends-la dans `SOLUTIONS.md`)
3. Pose un point d'arrêt 🔴 sur la **première ligne** de la méthode
4. Panneau **Testing** 🧪 → clic droit sur `Conversion_correcte` → **Debug Test**
5. Le programme s'arrête. Dans **Variables**, regarde `cuivre`.
6. Appuie sur **`F10`**. Une nouvelle variable apparaît. Regarde sa valeur.
7. Continue au `F10`, ligne par ligne, et **regarde les nombres se construire**.
8. Ajoute `cuivre / 10000` dans **Watch**. Quelle valeur ?

> 🎯 **Ce que tu viens de faire**, c'est ce qu'un développeur professionnel fait
> plusieurs fois par jour. Ce n'est pas de la magie : c'est de l'observation.

---

## 7. Le point d'arrêt conditionnel (le truc de pro)

Ta boucle tourne 1000 fois et le bug n'arrive qu'au tour 847. Tu ne vas pas
appuyer 847 fois sur `F5`.

👉 **Clic droit sur le point d'arrêt** → **Edit Breakpoint…** → tape une
condition :

```csharp
i == 847
```

```csharp
heros.PointsDeVie < 0
```

Le programme ne s'arrêtera **que** quand la condition est vraie. 🎯

C'est l'outil parfait pour attraper un bug qui n'apparaît que dans un cas
précis.

---

## 8. Quand ça plante : s'arrêter sur l'exception

Une `NullReferenceException` ? Une `IndexOutOfRangeException` ?

Dans le panneau **Run and Debug** (à gauche), section **Breakpoints**, coche
**« All Exceptions »** (ou *User-Unhandled Exceptions*).

Le programme s'arrêtera **au moment précis** où l'exception est levée — avant de
mourir. Tu vois l'état exact de toutes les variables au moment du crash.

C'est **beaucoup** plus utile que de lire la trace d'erreur après coup.

---

## 9. 🆘 Ça ne marche pas

**« Mon point d'arrêt est gris / creux, il ne s'active pas »**
→ Le code n'est pas compilé à jour, ou tu débogues le mauvais projet. Fais
`dotnet build` et relance.

**« `F5` ne me propose rien »**
→ Vérifie que l'extension **C# Dev Kit** est bien installée
(voir [INSTALLATION.md](INSTALLATION.md)).

**« Je suis perdu dans du code que je n'ai pas écrit »**
→ Tu as fait `F11` sur une méthode de .NET. `Maj+F11` pour ressortir.

**« Le programme ne s'arrête jamais sur mon point d'arrêt »**
→ Cette ligne n'est peut-être jamais exécutée. C'est déjà une information
précieuse : ton `if` ne se déclenche pas comme tu le crois.

**« Ça avance tout seul, je ne contrôle rien »**
→ Tu as appuyé sur `F5` (Continue) au lieu de `F10` (Step Over).

---

## 🎯 L'antisèche

| Je veux… | Comment |
|----------|---------|
| Poser un point d'arrêt | clic dans la marge, ou `F9` |
| Déboguer un test | Testing 🧪 → clic droit → **Debug Test** |
| Lancer la démo | `F5` |
| Avancer d'une ligne | `F10` |
| Entrer dans ma méthode | `F11` |
| Ressortir | `Maj+F11` |
| Voir une valeur | survole la variable à la souris |
| Tester une expression | panneau **Watch** |
| Savoir qui m'a appelé | panneau **Call Stack** |
| M'arrêter au tour 847 | point d'arrêt **conditionnel** |
| Arrêter tout | `Maj+F5` |

---

> **La règle** : dès que tu te dis « mais pourquoi ça fait ça ?! », ne rajoute
> pas un `Console.WriteLine`. Pose un point d'arrêt et **regarde**. 🐞
