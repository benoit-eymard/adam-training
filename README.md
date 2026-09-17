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
2. Apprends à sauvegarder ton travail : [GIT.md](GIT.md) (15 minutes) —
   **à lire avant de commencer à coder**.
3. Crée ta branche du module 1 :
   ```bash
   git switch -c module-1
   ```
4. Ouvre le module 1 : [Modules/01-Premiers-Pas/COURS.md](Modules/01-Premiers-Pas/COURS.md)
5. Note ta progression dans [PROGRESSION.md](PROGRESSION.md)

---

## 🌿 Où écrire ton code ?

> ### ⚠️ Jamais sur la branche `main`.

`main`, c'est le **cours d'origine**, avec les exercices encore vides. C'est ta
référence : si tu écris tes réponses dedans, tu la détruis.

**Une branche par module**, chacune créée depuis la précédente :

```
main         ●──────────────────────────────  le cours vierge, intact
              \
module-1       ●───●───●                      ton travail du module 1
                        \
module-2                 ●───●───●            module 1 + 2
                                  \
module-3                           ●───●      module 1 + 2 + 3
```

Au début de chaque module :

```bash
git switch -c module-2
```

Et à chaque exercice terminé :

```bash
git add . && git commit -m "Module 2 : exercice 3 terminé" && git push
```

Tout est expliqué en détail dans [GIT.md](GIT.md) — y compris **comment te
rattraper** quand tu fais une bêtise (ça arrivera, c'est normal).

---

## 🔍 La relecture de fin de module

Quand un module est terminé, tu ouvres une **Pull Request** sur GitHub. C'est
une conversation autour de ton code : Benoît le relit, commente les lignes qui
l'intriguent, et tu réponds.

C'est la partie la plus formatrice du parcours. **Tous** les développeurs
professionnels travaillent comme ça, tous les jours.

| | |
|---|---|
| **Quand ?** | à la fin de chaque module, défi compris |
| **Base de la PR** | la branche du module **précédent** (`main` pour le module 1) |
| **À remplir** | le modèle s'affiche tout seul — surtout la section **« Mes questions »** |
| **À la fin** | **Close**, jamais **Merge** — `main` doit rester le cours vierge |

> ⚠️ Une PR n'est pas un examen. Recevoir des remarques sur son code, c'est le
> quotidien du métier — pas une sanction. Et tu as le droit de ne pas être
> d'accord, à condition d'expliquer pourquoi. 😉

Le mode d'emploi complet est dans [GIT.md, section 13](GIT.md#13--la-pull-request--faire-relire-son-code).

---

## 🔁 Comment fonctionne un module

Chaque module suit toujours le même rythme :

```
🌿 git switch -c   →  Tu crées la branche du module.
📖 COURS.md        →  Tu lis la théorie (15-20 min). Prends ton temps.
▶️  dotnet run      →  Tu lances la démo pour VOIR le concept en action.
✍️  Exercices.cs    →  Tu remplis les // TODO: c'est là que tu codes.
✅ dotnet test     →  Les tests te disent tout de suite si c'est bon.
🌿 git commit      →  Tests verts = tu sauvegardes. À chaque exercice !
💡 SOLUTIONS.md    →  Tu compares avec la correction commentée.
🏆 DEFI.md         →  Un mini-projet libre pour valider le module.
🌿 git push        →  Ton travail est en sécurité sur GitHub.
🔍 Pull Request    →  Tu fais relire ton code, tu poses tes questions.
```

### Les trois commandes à retenir

Place-toi dans le dossier du module, puis :

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```

La première lance le programme. La seconde vérifie tes exercices.

Et dès qu'un exercice passe au vert, depuis la racine du projet :

```bash
git add . && git commit -m "Module 1 : exercice 3 terminé" && git push
```

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
- **Commite dès que les tests sont verts**, et pousse à la fin de chaque séance.
  Un disque dur, ça meurt. Ton travail sur GitHub, non.
- **Ne travaille jamais sur `main`.** Une branche par module.
- **Ouvre une Pull Request à la fin de chaque module**, et pose tes questions
  dedans. C'est fait pour.

---

## 🗂️ Les fichiers de ce dépôt

| Fichier | À quoi ça sert |
|---------|----------------|
| [INSTALLATION.md](INSTALLATION.md) | installer VS Code et le SDK .NET |
| [GIT.md](GIT.md) | sauvegarder ton travail, les branches, se rattraper |
| [PROGRESSION.md](PROGRESSION.md) | ta checklist et ton carnet de bord |
| [GLOSSAIRE.md](GLOSSAIRE.md) | tout le vocabulaire, des variables au deadlock |
