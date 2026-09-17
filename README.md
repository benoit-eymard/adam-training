# 🎮 Apprendre le C# — Parcours complet

Bienvenue ! Ce dépôt est un cours de programmation C# en **8 modules**, conçu pour être
suivi en autonomie, à ton rythme.

Le fil rouge : tu vas construire petit à petit un **jeu de rôle (RPG)** — des
personnages, des monstres, des combats, un inventaire, des sauvegardes. À la fin,
tu auras un vrai jeu que tu as codé toi-même.

---

## 🗺️ Le parcours

| # | Module | Ce que tu apprends | Ce que tu construis |
|---|--------|--------------------|---------------------|
| 1 | [Premiers pas](Modules/01-Premiers-Pas/) | Variables, types, console, calculs | Un générateur de fiches de perso |
| 2 | [Décisions & boucles](Modules/02-Decisions-Et-Boucles/) | `if`, `switch`, `for`, `while` | Le jeu du Juste Prix |
| 3 | [Algorithmie](Modules/03-Algorithmie/) | Méthodes, tableaux, décomposer un problème | Un Morpion à 2 joueurs |
| 4 | [Objet — les bases](Modules/04-Objet-Bases/) | Classes, propriétés, constructeurs | Personnage vs Monstre : premier combat |
| 5 | [Objet — avancé](Modules/05-Objet-Avance/) | Héritage, polymorphisme, interfaces | Guerrier, Mage, Archer |
| 6 | [Collections & données](Modules/06-Collections-Et-Donnees/) | `List`, `Dictionary`, erreurs, fichiers | Inventaire + sauvegarde de partie |
| 7 | [Lambda & LINQ](Modules/07-Lambda-Et-LINQ/) | Délégués, lambdas, LINQ | Statistiques sur tes parties |
| 8 | [Threading & async](Modules/08-Threading-Et-Async/) | `Task`, `async`/`await`, concurrence | Un combat en temps réel |

La difficulté monte progressivement. **Ne saute pas de module** : chacun s'appuie sur
le précédent.

---

## 🚀 Démarrer

1. Installe les outils : suis le guide [INSTALLATION.md](INSTALLATION.md) (10 minutes).
2. Ouvre le module 1 : [Modules/01-Premiers-Pas/COURS.md](Modules/01-Premiers-Pas/COURS.md)
3. Note ta progression dans [PROGRESSION.md](PROGRESSION.md)

---

## 🔁 Comment fonctionne un module

Chaque module suit toujours le même rythme :

```
📖 COURS.md        →  Tu lis la théorie (15-20 min). Prends ton temps.
▶️  dotnet run      →  Tu lances la démo pour VOIR le concept en action.
✍️  Exercices.cs    →  Tu remplis les // TODO: c'est là que tu codes.
✅ dotnet test     →  Les tests te disent tout de suite si c'est bon.
💡 SOLUTIONS.md    →  Tu compares avec la correction commentée.
🏆 DEFI.md         →  Un mini-projet libre pour valider le module.
```

### Les deux commandes à retenir

Place-toi dans le dossier du module, puis :

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```

Le premier lance le programme. Le second vérifie tes exercices.

---

## 🧭 Si tu es bloqué

C'est **normal** d'être bloqué. Tout le monde l'est, tout le temps, même les pros.
Dans l'ordre :

1. **Relis le message d'erreur.** Il dit presque toujours la vérité. Le numéro de
   ligne est un indice, pas une accusation.
2. **Relis la section du COURS** qui parle du concept.
3. **Regarde le GLOSSAIRE** : [GLOSSAIRE.md](GLOSSAIRE.md)
4. **Essaie encore 10 minutes.** C'est souvent là que ça se débloque.
5. **Regarde la solution** — et surtout, comprends *pourquoi* elle marche.
   Recopier sans comprendre ne sert à rien.
6. **Demande.** Poser une question précise, c'est déjà la moitié de la réponse.

---

## 📏 Les règles du jeu

- **Un module par semaine environ.** Ce n'est pas une course.
- **Tape le code à la main**, ne fais pas de copier-coller. Tes doigts apprennent aussi.
- **Casse des choses.** Modifie les exemples, change les valeurs, vois ce qui explose.
  C'est comme ça qu'on apprend le plus vite.
- **Le code qui marche mais que tu ne comprends pas** n'est pas du code qui marche.
