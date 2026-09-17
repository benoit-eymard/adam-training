# 🏆 Défi du Module 1 — Le Générateur de Fiche de Personnage

> Pas de tests ici, pas de solution imposée. **C'est ton programme.**
> Le but : assembler tout ce que tu viens d'apprendre en un truc qui tourne.

---

## La mission

Écris un programme qui **interroge le joueur** et lui affiche une belle fiche de
personnage.

### Exemple d'exécution attendue

```
=== CRÉATION DE PERSONNAGE ===

Nom de ton héros : Kaelis
Sa classe (Guerrier/Mage/Archer) : Mage
Son niveau : 5
Sa force (1-20) : 8
Son intelligence (1-20) : 18

╔════════════════════════════════╗
║      FICHE DE PERSONNAGE       ║
╚════════════════════════════════╝

  Nom           : Kaelis
  Classe        : Mage
  Niveau        : 5

  Force         : 8
  Intelligence  : 18
  Moyenne stats : 13.0

  Points de vie : 150
  Or de départ  : 2 or, 50 argent, 0 cuivre

Bonne aventure, Kaelis !
```

---

## Le cahier des charges

- [ ] Demander le **nom**, la **classe**, le **niveau**, la **force** et
      l'**intelligence**
- [ ] Afficher une fiche encadrée et bien alignée
- [ ] Calculer et afficher la **moyenne des statistiques** (avec la virgule !)
- [ ] Calculer les **points de vie** : `100 + (niveau × 10)`
- [ ] Calculer l'**or de départ** : `niveau × 5000` pièces de cuivre,
      converties en or/argent/cuivre (réutilise ton exercice 5 !)
- [ ] Un message de fin personnalisé avec le nom du héros

---

## Où écrire le code ?

Dans `Exercices/Program.cs`. Mets ton défi **tout en bas**, après la démo —
ou commente la démo si elle te gêne.

Tu peux appeler tes propres exercices depuis le défi :

```csharp
Console.WriteLine(Exo.ConvertirEnOr(25000));
```

---

## 🌶️ Si tu veux corser

Aucune obligation — mais si tu t'amuses :

- **Facile** : ajoute de la couleur avec
  `Console.ForegroundColor = ConsoleColor.Cyan;`
  (pense à `Console.ResetColor();` après)
- **Moyen** : ajoute d'autres stats (agilité, chance) et calcule une
  **puissance totale**
- **Moyen** : demande aussi l'âge du héros et affiche son année de naissance
- **Corsé** : fais que les points de vie dépendent aussi de la force :
  `100 + (niveau × 10) + (force × 2)`
- **Corsé** : affiche une barre de statistique en caractères :
  force 8 → `[########------------]`
  *(indice : ça devient beaucoup plus simple avec une boucle... module 2 !)*

---

## ✍️ Quand tu as fini

1. Montre-le à quelqu'un. Fais-le tourner devant lui.
2. Coche le défi dans [PROGRESSION.md](../../PROGRESSION.md)
3. Note dans ton carnet de bord ce qui t'a le plus bloqué.

Puis direction le **Module 2** : tu vas apprendre à faire des programmes qui
**décident** et qui **répètent**. C'est là que ça devient vraiment vivant. 🚀
