# 🏆 Défi du Module 4 — L'Arène

> Ton premier vrai **jeu de combat**. À partir d'ici, chaque module va enrichir
> ce même jeu. Garde ton code : tu vas le reprendre.

---

## La mission

Un combat au tour par tour entre ton héros et une série de monstres.

```
╔═══════════════════════════════════════╗
║            L'ARÈNE                    ║
╚═══════════════════════════════════════╝

Nom de ton héros : Kaelis

Kaelis (100/100 PV) — mains nues

Choisis ton arme :
  1. Épée longue (+8)
  2. Arc court (+5)
  3. Bâton de mage (+12)
> 1

Kaelis (100/100 PV) — Épée longue (+8)

═══ COMBAT 1/3 : Gobelin (30 PV) ═══

  Kaelis   [##########] 100/100
  Gobelin  [##########] 30/30

Que fais-tu ?
  1. Attaquer
  2. Boire une potion (2 restantes)
> 1

⚔️  Tu infliges 20 dégâts !
🩸 Le Gobelin te riposte : 7 dégâts.

  Kaelis   [#########-] 93/100
  Gobelin  [###-------] 10/30

...

🎉 Gobelin vaincu ! Tu passes au combat suivant.
```

---

## Le cahier des charges

- [ ] Créer un `Personnage` à partir du nom saisi
- [ ] Proposer un **choix d'arme** parmi trois `Arme`
- [ ] Enchaîner **3 combats** contre des monstres de plus en plus durs
- [ ] À chaque tour : afficher les **barres de vie** (réutilise ton exercice du
      module 2 !), proposer **attaquer** ou **boire une potion**
- [ ] Le monstre riposte s'il est encore vivant
- [ ] Gérer la victoire et la défaite
- [ ] Entre deux combats, le héros **récupère un peu de PV**

---

## 🧱 Conseils de structure

**Réutilise tes classes, n'écris pas de logique de combat dans `Program.cs`.**
Le test : si tu écris `heros.PointsDeVie - degats` quelque part dans
`Program.cs`, c'est que tu contournes ton propre objet. Appelle `SubirDegats`.

`Program.cs` doit se contenter de trois choses :
1. afficher
2. demander
3. appeler les bonnes méthodes des objets

**Découpe en méthodes** (le réflexe du module 3) :

```csharp
static void AfficherEtatDuCombat(Personnage h, Monstre m)
static int DemanderAction()
static Monstre CreerMonstre(int numeroCombat)
static string BarreDeVie(int pv, int pvMax)
```

---

## 🌶️ Pour aller plus loin

- **Facile** : ajoute une chance de **coup critique** (10 % → dégâts ×2)
  ```csharp
  Random hasard = new Random();
  bool critique = hasard.Next(1, 101) <= 10;
  ```

- **Facile** : varie les dégâts (`Force ± 3`) pour que les combats ne soient pas
  identiques

- **Moyen** : ajoute une classe **`Armure`** avec une `Reduction`, et fais que
  `SubirDegats` en tienne compte
  *(indice : ça se passe dans `Personnage`, pas dans `Program.cs`)*

- **Moyen** : ajoute une **expérience** et un **niveau**. À chaque monstre
  vaincu, le héros gagne de l'XP ; à 100 XP il monte de niveau (+20 PV max,
  +3 force) et est soigné à fond.

- **Corsé** : ajoute une **fuite** — le héros peut fuir, mais le monstre a 50 %
  de chances de l'attaquer au passage

- **Corsé** : ajoute un système de **boutique** entre les combats (or gagné par
  monstre, achat de potions et d'armes)
  *(indice : l'exercice 5 du module 1 te ressert !)*

---

## 🐛 Les pièges de ce défi

**« Mon monstre a des PV négatifs »**
→ Tu modifies les PV directement au lieu d'appeler `SubirDegats`. Le garde-fou
est dans la méthode.

**« Le monstre attaque après être mort »**
→ Vérifie `monstre.EstVivant` **avant** sa riposte.

**« Les deux monstres ont les mêmes PV »**
→ Tu as probablement écrit `Monstre m2 = m1;` au lieu de `new Monstre(...)`.
Souviens-toi : les objets sont des **références** !

**« NullReferenceException »**
→ Un objet que tu utilises n'a jamais été créé avec `new`. Regarde la ligne
indiquée par l'erreur, et remonte la trace du `null`.

---

## ✍️ Quand tu as fini

1. **Fais-y jouer quelqu'un.**
2. Relis ton `Program.cs` : y a-t-il des règles du jeu qui devraient être dans
   tes classes ? Déplace-les. C'est le meilleur exercice de ce module.
3. Coche le module 4 dans [PROGRESSION.md](../../PROGRESSION.md)

---

Au **module 5**, tu vas découvrir comment créer un Guerrier, un Mage et un
Archer **sans copier-coller ta classe trois fois** — et comment écrire une
méthode de combat qui les accepte tous. ⚔️
