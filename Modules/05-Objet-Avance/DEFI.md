# 🏆 Défi du Module 5 — La Guilde

> Un combat **en équipe**, 3 contre 3. C'est ici que le polymorphisme prend tout
> son sens.

---

## La mission

Le joueur compose une équipe de trois héros et l'envoie affronter un groupe de
monstres, au tour par tour.

```
╔═══════════════════════════════════════════════╗
║               LA GUILDE                       ║
╚═══════════════════════════════════════════════╝

Compose ton équipe (3 membres) :
  1. Guerrier  — 120 PV, frappe fort, peut entrer en rage
  2. Mage      —  80 PV, sorts puissants, sait soigner
  3. Archer    —  90 PV, flèches, faible une fois à sec

Membre 1 > 1
Nom > Thorin
Membre 2 > 2
Nom > Elyra
Membre 3 > 3
Nom > Sylas

═══════════════ TOUR 3 ═══════════════

  TON ÉQUIPE                      LES ENNEMIS
  Thorin (85/120 PV)              Gobelin (10/30 PV)
  Elyra  (80/80 PV) 25 mana       Orc     (45/60 PV)
  Sylas  (90/90 PV) 9 flèches     Troll   (80/80 PV)

C'est au tour de Elyra (Mage).
  1. Attaquer     2. Soigner un allié     3. Passer
> 2

  Qui soigner ?
  1. Thorin (85/120)  2. Sylas (90/90)
> 1

✨ Elyra soigne Thorin de 30 PV !
```

---

## Le cahier des charges

- [ ] Le joueur compose une équipe de 3 `Combattant` (choix du type + du nom)
- [ ] Une équipe ennemie de 3 monstres
- [ ] Un tour = chaque combattant vivant agit, dans l'ordre
- [ ] Pour les héros : le joueur choisit l'action
- [ ] Pour les ennemis : action automatique (aléatoire ou simple)
- [ ] **Le menu d'actions s'adapte au type** : seul un `ISoigneur` voit l'option
      « Soigner », seul un `Guerrier` voit « Entrer en rage »
- [ ] La partie s'arrête quand une équipe entière est éliminée
- [ ] Afficher l'état complet des deux équipes à chaque tour

---

## 🧱 Le cœur du défi

**Ton code de combat ne doit connaître QUE le type `Combattant`.**

```csharp
Combattant[] heros = new Combattant[3];
Combattant[] ennemis = new Combattant[3];

foreach (Combattant c in heros)
{
    if (!c.EstVivant) continue;
    c.Attaquer(ChoisirCible(ennemis));   // ⬅️ polymorphisme
}
```

Le seul endroit où tu as le droit de tester le type, c'est pour **proposer des
options supplémentaires** :

```csharp
Console.WriteLine("  1. Attaquer");
if (c is ISoigneur)  Console.WriteLine("  2. Soigner un allié");
if (c is Guerrier)   Console.WriteLine("  3. Entrer en rage");
```

> 🧠 **Le test qui ne trompe pas** : si tu écris quelque part
> `if (c is Mage) { degats = 25; } else if (c is Guerrier) { degats = 15; }`,
> c'est que tu as **contourné** le polymorphisme. Ces dégâts appartiennent aux
> méthodes `Attaquer` de chaque classe.

---

## Créer les monstres

Le plus simple : réutiliser tes propres classes !

```csharp
Combattant[] ennemis =
{
    new Guerrier("Gobelin", 30, 7),
    new Guerrier("Orc", 60, 12),
    new Archer("Troll lanceur", 80, 5)
};
```

C'est un peu bizarre sémantiquement (un gobelin n'est pas un « guerrier »), mais
ça montre bien l'idée : **le combat ne s'intéresse qu'aux capacités**, pas aux
étiquettes.

**Plus propre** : crée une vraie classe `Monstre : Combattant`, avec ses propres
règles (par exemple une chance de rater son attaque).

---

## 🌶️ Pour aller plus loin

- **Facile** : chaque monstre vaincu rapporte de l'**or** à l'équipe

- **Moyen** : ajoute une classe **`Paladin : Combattant, ISoigneur`** — il
  combat comme un guerrier ET soigne comme un mage. Vérifie que ton code de
  combat le gère **sans une seule modification**. Si c'est le cas, tu as gagné :
  ton architecture est bonne. 🏆

- **Moyen** : une interface **`IEmpoisonnable`** — certains combattants peuvent
  subir un poison qui leur retire des PV à chaque tour

- **Moyen** : une **IA d'ennemi intelligente** : elle vise en priorité le héros
  le plus faible (réutilise `LePlusBlesse` !), ou le soigneur

- **Corsé** : un système de **vitesse** — chaque combattant a une `Vitesse`, et
  l'ordre du tour est déterminé par un tri décroissant sur cette valeur
  *(le tri à bulles du module 3, sur des objets !)*

- **Corsé** : des **effets de statut** (étourdi, brûlé, protégé) qui durent
  plusieurs tours

---

## 🐛 Les pièges de ce défi

**« Je ne peux pas accéder à `.Mana` sur un `Combattant` »**
→ Normal : la variable est de type `Combattant`, qui n'a pas de mana. Soit tu
testes le type (`if (c is Mage m)`), soit — mieux — tu te demandes si cette
information doit vraiment sortir de l'objet.

**« Mon Guerrier attaque comme un Mage »**
→ Tu as oublié `override` sur une méthode. Sans lui, C# croit que tu déclares
une **nouvelle** méthode qui cache celle du parent, et le polymorphisme ne
fonctionne plus. Le compilateur t'aura probablement mis un avertissement —
lis-le.

**« Tous mes héros ont le même nom »**
→ Tu as créé un seul objet et copié la référence 3 fois. Il faut **3 `new`**.

**« StackOverflowException »**
→ Dans un `override`, tu as appelé la méthode elle-même au lieu de `base.`.

---

## ✍️ Quand tu as fini

1. **Ajoute une classe `Voleur`** en 10 minutes. Si ton combat la gère sans être
   modifié, ton architecture objet est solide. C'est le vrai examen de ce module.
2. Coche le module 5 dans [PROGRESSION.md](../../PROGRESSION.md)

---

Au **module 6**, tu vas pouvoir faire grandir ton équipe en cours de partie, la
sauvegarder sur ton disque, et arrêter de planter quand le joueur tape
n'importe quoi. 🎒
