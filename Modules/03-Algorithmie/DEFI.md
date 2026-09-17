# 🏆 Défi du Module 3 — Le Morpion

> Ton plus gros projet jusqu'ici. **Ne le code pas d'un bloc** — c'est
> exactement le piège que ce module t'apprend à éviter.

---

## La mission

Un Morpion (tic-tac-toe) jouable à **deux joueurs** sur le même clavier.

```
╔═══════════════════════════════╗
║          MORPION              ║
╚═══════════════════════════════╝

   1 | 2 | 3
  ---+---+---
   4 | 5 | 6
  ---+---+---
   7 | 8 | 9

Joueur X, choisis ta case : 5

   1 | 2 | 3
  ---+---+---
   4 | X | 6
  ---+---+---
   7 | 8 | 9

Joueur O, choisis ta case : 5
⛔ Cette case est déjà prise !

Joueur O, choisis ta case : 1
...

   O | 2 | 3
  ---+---+---
   4 | X | 6
  ---+---+---
   7 | 8 | X

🎉 Le joueur X a gagné !
```

---

## ⚠️ La méthode : découpe AVANT de coder

C'est le vrai exercice. Avant d'écrire une ligne, liste les **petits problèmes** :

```csharp
static void AfficherGrille(char[] grille)
static bool CaseLibre(char[] grille, int numeroCase)
static bool JoueurAGagne(char[] grille, char symbole)
static bool GrillePleine(char[] grille)
static int DemanderCoup(char[] grille, char joueur)
static char ChangerDeJoueur(char joueur)
```

Chaque méthode fait **une seule chose**, tient en quelques lignes, et se teste
séparément. Code-les **une par une**, et vérifie chacune avant de passer à la
suivante.

> 🧠 Si tu commences par `Main` et que tu essaies d'écrire tout le jeu d'affilée,
> tu vas te noyer. C'est garanti. Commence par `AfficherGrille`, fais-la marcher,
> puis la suivante.

---

## La grille

Un tableau de 9 `char` suffit :

```csharp
char[] grille = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
```

Astuce : on l'initialise avec les **numéros des cases**. Comme ça, l'affichage
sert à la fois de plateau et de mode d'emploi. Quand un joueur joue la case 5,
on remplace `'5'` par `'X'`.

> ⚠️ **Le décalage d'index.** La case n°5 pour le joueur, c'est `grille[4]` pour
> toi. `grille[numeroCase - 1]`. Tu vas te tromper au moins une fois. 😄

---

## Détecter la victoire

Il y a **8 combinaisons gagnantes** : 3 lignes, 3 colonnes, 2 diagonales.

```csharp
static bool JoueurAGagne(char[] g, char s)
{
    // Les lignes
    if (g[0] == s && g[1] == s && g[2] == s) return true;
    if (g[3] == s && g[4] == s && g[5] == s) return true;
    // ... à toi de compléter
}
```

**Version plus élégante** (et bien plus courte) — un tableau à deux dimensions
qui liste les combinaisons :

```csharp
int[,] combinaisons = {
    {0,1,2}, {3,4,5}, {6,7,8},   // lignes
    {0,3,6}, {1,4,7}, {2,5,8},   // colonnes
    {0,4,8}, {2,4,6}             // diagonales
};

for (int c = 0; c < 8; c++)
{
    if (g[combinaisons[c,0]] == s &&
        g[combinaisons[c,1]] == s &&
        g[combinaisons[c,2]] == s)
    {
        return true;
    }
}
return false;
```

Les deux marchent. La première est plus simple à écrire, la seconde plus simple
à **modifier** (essaie de passer en grille 4×4 avec la version 1... 😅).

---

## Le cahier des charges

- [ ] Afficher la grille proprement
- [ ] Demander son coup au joueur courant
- [ ] Refuser une case déjà prise **et redemander**
- [ ] Refuser une saisie invalide (lettre, nombre hors 1-9)
- [ ] Alterner X et O
- [ ] Détecter la victoire (8 combinaisons)
- [ ] Détecter le match nul (grille pleine, pas de gagnant)
- [ ] Annoncer le résultat

---

## 🌶️ Pour aller plus loin

- **Facile** : colorer X en rouge et O en bleu
- **Facile** : afficher le nombre de coups joués
- **Moyen** : demander leurs **prénoms** aux joueurs et les utiliser dans les
  messages
- **Moyen** : un **tableau des scores** sur plusieurs manches
- **Corsé** : une **IA qui joue aléatoirement** (mode 1 joueur)
  *(indice : elle tire une case au hasard parmi les cases libres)*
- **Très corsé** : une **IA qui ne perd jamais**. Règles, par ordre de priorité :
  1. si elle peut gagner ce tour → elle gagne
  2. si l'adversaire peut gagner au tour suivant → elle bloque
  3. sinon → le centre, puis un coin, puis n'importe quoi

---

## 🐛 Les bugs que tu vas rencontrer

**Le décalage d'index.** Case 5 → `grille[4]`. Systématiquement.

**La victoire détectée trop tôt ou trop tard.** Vérifie la victoire **après**
avoir posé le symbole, pas avant.

**Le match nul annoncé alors qu'il y a un gagnant.** Teste la victoire **avant**
de tester la grille pleine.

**La boucle infinie sur une saisie invalide.** Si `int.Parse` plante sur une
lettre, utilise `int.TryParse` :

```csharp
if (int.TryParse(Console.ReadLine(), out int coup))
{
    // la saisie était bien un nombre, il est dans "coup"
}
else
{
    Console.WriteLine("Ce n'est pas un nombre !");
}
```

*(`TryParse` est ta première rencontre avec la gestion d'erreur — sujet complet
au module 6.)*

---

## ✍️ Quand tu as fini

1. **Fais-y jouer quelqu'un.** Un Morpion, ça se joue à deux. 😄
2. Relis ton code : **combien de lignes fait ta méthode la plus longue ?**
   Si elle dépasse 20 lignes, essaie de la découper. C'est un excellent exercice.
3. Coche le module 3 dans [PROGRESSION.md](../../PROGRESSION.md)

---

Direction le **Module 4**. Tu vas découvrir pourquoi, à partir d'un certain
point, les tableaux et les méthodes ne suffisent plus — et ce qu'on invente pour
les remplacer. C'est **le** tournant du parcours. 🗡️
