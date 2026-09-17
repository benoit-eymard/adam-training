# 🏆 Défi du Module 7 — Le Tableau de Bord de la Guilde

> Reprends ton gestionnaire du module 6, et ajoute-lui un écran de
> statistiques. **Sans écrire une seule boucle.**

---

## La mission

```
╔═══════════════════════════════════════════════════╗
║        TABLEAU DE BORD — Les Lames d'Argent       ║
╚═══════════════════════════════════════════════════╝

  📊 VUE D'ENSEMBLE
     Membres          : 12  (9 vivants, 3 tombés)
     Niveau moyen     : 9.4
     Trésor total     : 4 830 po
     Niveau médian    : 8

  🏆 LE PODIUM
     1. Kaelis    (Mage niv.15)      800 po
     2. Thorin    (Guerrier niv.12)  300 po
     3. Sylas     (Archer niv.10)    220 po

  ⚔️  PAR CLASSE
     Guerrier   ████████░░  4 membres  niv. moyen 8.5   1 240 po
     Mage       ██████░░░░  3 membres  niv. moyen 11.3  1 950 po
     Archer     ████░░░░░░  2 membres  niv. moyen 9.0     440 po
     Voleur     ██░░░░░░░░  1 membre   niv. moyen 7.0      10 po

  💰 ÉCONOMIE
     Le plus riche    : Kaelis (800 po)
     Le plus pauvre   : Nym (10 po)
     Écart de richesse: 790 po
     Or moyen         : 402 po

  ⚠️  ALERTES
     • 3 membres sont tombés au combat
     • 2 membres ont moins de 50 po
     • Aucun soigneur dans la guilde !
```

---

## ⛔ La règle du défi

**Zéro `foreach`, zéro `for` dans tes calculs.**

Tu as le droit à un `foreach` **uniquement pour afficher** le résultat d'une
requête (par exemple parcourir les groupes pour les imprimer). Tout le reste
doit être du LINQ.

C'est une contrainte artificielle, et c'est volontaire : c'est comme ça qu'on
acquiert un réflexe.

---

## 📋 Le cahier des charges

- [ ] Nombre total, vivants, morts (`Count` avec critère)
- [ ] Niveau moyen (⚠️ gérer l'équipe vide)
- [ ] Trésor total (`Sum`)
- [ ] **Podium des 3 plus riches** (`OrderByDescending` + `Take`)
- [ ] **Répartition par classe** avec, pour chaque classe : le nombre, le niveau
      moyen et l'or total (`GroupBy` + agrégations dans le groupe)
- [ ] Le plus riche, le plus pauvre, l'écart (`MaxBy`, `MinBy`)
- [ ] Une section **alertes** conditionnelle (`Any`, `Count`)
- [ ] Une **barre graphique** proportionnelle par classe
      *(réutilise ta `BarreDeVie` du module 2 !)*

---

## 💡 Les requêtes qui vont te servir

### Agréger à l'intérieur d'un groupe

```csharp
var stats = membres
    .GroupBy(m => m.Classe)
    .Select(g => new
    {
        Classe = g.Key,
        Nombre = g.Count(),
        NiveauMoyen = g.Average(m => m.Niveau),
        OrTotal = g.Sum(m => m.Or)
    })
    .OrderByDescending(s => s.Nombre)
    .ToList();

foreach (var s in stats)   // ✅ foreach d'AFFICHAGE, autorisé
{
    Console.WriteLine($"{s.Classe,-10} {s.Nombre} membres, niv. {s.NiveauMoyen:F1}");
}
```

> 🆕 **`new { ... }` sans nom de classe** : c'est un **type anonyme**. C# crée
> une petite classe temporaire à la volée, juste pour transporter ces quatre
> valeurs. Très pratique en LINQ, quand créer une vraie classe serait excessif.
>
> ⚠️ Un type anonyme ne sort pas de la méthode où il est créé — c'est sa limite.
> Pour le retourner, il faut une vraie classe.

### Le formatage des nombres

```csharp
$"{moyenne:F1}"      // 9.4   (1 décimale)
$"{moyenne:F2}"      // 9.43  (2 décimales)
$"{or:N0}"           // 4 830 (séparateur de milliers)
$"{nom,-10}"         // aligné à gauche sur 10 caractères
$"{nombre,5}"        // aligné à droite sur 5 caractères
```

### La médiane (⭐⭐⭐)

Le niveau qui coupe l'équipe en deux :

```csharp
var tries = membres.Select(m => m.Niveau).OrderBy(n => n).ToList();
int mediane = tries[tries.Count / 2];
```

*(la vraie médiane fait la moyenne des deux valeurs centrales quand le nombre
d'éléments est pair — à toi de voir si tu veux être rigoureux 😄)*

---

## 🌶️ Pour aller plus loin

- **Facile** : un **classement complet** numéroté
  ```csharp
  .Select((m, index) => $"{index + 1}. {m.Nom}")
  ```
  *(eh oui, `Select` a une surcharge qui donne l'index !)*

- **Moyen** : une **recherche** — le joueur tape un bout de nom, tu affiches
  tous les membres correspondants
  ```csharp
  membres.Where(m => m.Nom.ToLower().Contains(recherche.ToLower()))
  ```

- **Moyen** : des **filtres combinables** — le joueur choisit une classe, un
  niveau minimum, vivant ou non, et tu construis la requête au fur et à mesure
  ```csharp
  IEnumerable<Membre> requete = membres;
  if (classeChoisie != null) requete = requete.Where(m => m.Classe == classeChoisie);
  if (niveauMin > 0) requete = requete.Where(m => m.Niveau >= niveauMin);
  var resultat = requete.ToList();
  ```
  *(c'est exactement comme ça qu'on construit une recherche dans une vraie
  application — l'exécution différée devient un atout !)*

- **Corsé** : un **export CSV** du tableau de bord
  ```csharp
  string csv = string.Join("\n", membres.Select(m => $"{m.Nom};{m.Classe};{m.Niveau}"));
  File.WriteAllText("guilde.csv", csv);
  ```
  Ouvre le fichier dans Excel ou LibreOffice. 😎

- **Corsé** : des **suggestions automatiques** — « il manque un soigneur »,
  « Brunhild est très en retard sur le reste de l'équipe », « vous pourriez
  acheter 3 potions avec votre or »

---

## 🐛 Les pièges de ce défi

**« InvalidOperationException: Sequence contains no elements »**
→ `Average`, `Max`, `Min` ou `First` sur une collection vide. Après un `Where`,
il ne reste peut-être rien ! Teste avec `Any()` d'abord, ou utilise les
variantes `...OrDefault`.

**« Ma requête donne un résultat différent à chaque appel »**
→ Exécution différée : la source a changé entre-temps. `ToList()`.

**« Je n'arrive pas à retourner mon type anonyme »**
→ C'est normal, c'est sa limite. Crée une vraie classe (ou un `record`).

**« Mon pourcentage vaut toujours 0 »**
→ Division entière. `(double)nombre / total * 100`.

---

## ✍️ Quand tu as fini

1. **Compte les `foreach` dans tes calculs.** S'il y en a zéro, bravo. 🎉
2. Reprends une méthode LINQ que tu as écrite et **réécris-la en boucles**.
   Regarde la différence de longueur et de lisibilité. C'est le moment où le
   module prend tout son sens.
3. Coche le module 7 dans [PROGRESSION.md](../../PROGRESSION.md)

---

Dernière ligne droite. Au **module 8**, tu vas apprendre à faire **plusieurs
choses en même temps** — et découvrir les bugs les plus retors de toute la
programmation. ⚡
