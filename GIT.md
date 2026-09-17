# 🌿 Git — sauvegarder et partager ton travail

> Git, c'est la **machine à remonter le temps** de ton code. Il garde une photo
> de ton projet à chaque fois que tu le lui demandes, et tu peux revenir à
> n'importe laquelle. Plus jamais de « j'ai tout cassé et je ne sais plus
> comment c'était avant ».
>
> C'est aussi **l'outil le plus utilisé du métier**. Tous les développeurs du
> monde s'en servent, tous les jours.
>
> ⏱️ Lecture : ~15 min · À lire avant le module 1

---

## 1. Les trois mots à comprendre

| Mot | Ce que ça veut dire |
|-----|---------------------|
| **commit** | « prends une photo de mon code maintenant » |
| **branche** | une ligne de temps parallèle, où tu bosses sans déranger les autres |
| **push** | « envoie mes photos sur GitHub » (pour les sauvegarder et les partager) |

Et un quatrième, très important ici :

| Mot | Ce que ça veut dire |
|-----|---------------------|
| **main** | la branche principale : **le cours d'origine, propre, avec les `// TODO:`** |

---

## 2. ⚠️ La règle n°1 de ce dépôt

> ### On ne travaille JAMAIS directement sur `main`.

**Pourquoi ?** Parce que `main` doit rester le cours **vierge** : celui où les
exercices sont encore vides. C'est ta référence. Si tu y écris tes réponses, tu
la détruis — et tu ne pourras plus jamais recommencer un exercice à zéro, ni
comparer.

**Donc** : une branche par module. `module-1` pour le module 1, `module-2` pour
le module 2, etc.

```
main         ●─────────────────────────────────────  (le cours vierge, intact)
              \
module-1       ●───●───●                              (ton travail du module 1)
                        \
module-2                 ●───●───●                    (module 1 + 2)
                                  \
module-3                           ●───●              (module 1 + 2 + 3)
```

👉 Remarque : chaque nouvelle branche part de la **précédente**, pas de `main`.
Comme ça tu **gardes tout ton travail** au fur et à mesure. Ta branche du module
8 contiendra les 8 modules terminés.

---

## 3. La toute première fois

### Dire à Git qui tu es

À faire **une seule fois** sur ton ordinateur :

```bash
git config --global user.name "Adam"
```

```bash
git config --global user.email "ton.email@exemple.com"
```

*(cet email apparaîtra dans tes commits — utilise celui de ton compte GitHub)*

### Récupérer le cours

> 📨 **Avant tout** : tu as reçu une invitation de Benoît à rejoindre le dépôt
> (par mail, et sur https://github.com/notifications). **Accepte-la**, sinon
> tes `push` seront refusés avec une erreur de permission.

```bash
git clone https://github.com/benoit-eymard/adam-training.git
```

Ça crée un dossier `adam-training`. Ouvre-le dans VS Code.

```bash
cd adam-training
```

---

## 4. Démarrer un module

**Avant de toucher au module 1**, crée ta branche :

```bash
git switch -c module-1
```

`switch -c` veut dire « crée une branche et va dessus » (*c* pour *create*).

Vérifie où tu es, à tout moment :

```bash
git branch
```

L'étoile `*` indique ta branche actuelle :

```
* module-1
  main
```

✅ Tu es sur `module-1`. Tu peux coder.

---

## 5. Le cycle de travail (à répéter sans arrêt)

Tu viens de finir un exercice, tes tests sont verts. Sauvegarde-le :

### 1️⃣ Regarder ce qui a changé

```bash
git status
```

Il te liste les fichiers modifiés. Prends l'habitude de **toujours** commencer
par là.

### 2️⃣ Choisir ce qu'on photographie

```bash
git add .
```

Le `.` veut dire « tout ce qui a changé ». Tu peux aussi être précis :

```bash
git add Modules/01-Premiers-Pas/Exercices/Exo.cs
```

### 3️⃣ Prendre la photo

```bash
git commit -m "Module 1 : exercices 1 à 3 terminés"
```

Le texte après `-m`, c'est le **message de commit** : il explique ce que tu as
fait. Voir plus bas comment bien l'écrire.

### 4️⃣ Envoyer sur GitHub

La **première fois** sur une branche :

```bash
git push -u origin module-1
```

Ensuite, sur cette même branche, il suffit de :

```bash
git push
```

---

## 6. Passer au module suivant

Quand le module 1 est fini (tests verts + défi fait), tu crées la branche du
module 2 **depuis ta branche du module 1** :

```bash
git switch -c module-2
```

*(tu étais sur `module-1`, donc `module-2` part de là et hérite de tout ton
travail)*

Puis, la première fois :

```bash
git push -u origin module-2
```

Et tu recommences le cycle. 🔁

> ⚠️ **Avant de créer la branche suivante, vérifie que tu as bien tout
> commité** : `git status` doit dire *« nothing to commit, working tree
> clean »*.

---

## 7. Écrire un bon message de commit

Un bon message répond à : **« qu'est-ce que ce commit change ? »**

```bash
✅ "Module 2 : FizzBuzz et barre de vie terminés"
✅ "Module 4 : classe Personnage, tous les tests passent"
✅ "Corrige le calcul des dégâts critiques dans le défi"

❌ "modif"
❌ "ça marche"
❌ "aaaaa"
❌ "test test test"
```

**Les règles simples :**
- à l'impératif ou au constat : « Ajoute », « Corrige », « Termine »
- une ligne, moins de 70 caractères
- dis **quoi**, pas **comment** (le code dit déjà le comment)

> 💡 Tu écris ces messages pour **toi dans trois mois**. Le jour où tu chercheras
> « mais quand est-ce que j'ai cassé ça ? », tu seras content de lire autre
> chose que « modif ».

---

## 8. Voir son historique

```bash
git log --oneline
```

```
a3f9c21 Module 1 : défi terminé, générateur de fiche
7b2e884 Module 1 : exercices 4 et 5
1c5d033 Module 1 : exercices 1 à 3 terminés
870d7f1 Parcours C# complet en 8 modules
```

Chaque ligne est une photo. Le code bizarre au début (`a3f9c21`) est
l'**identifiant** du commit.

Pour voir ce qu'un commit a changé :

```bash
git show a3f9c21
```

Pour voir ce que tu as modifié **depuis** ton dernier commit :

```bash
git diff
```

---

## 9. 🆘 Au secours, j'ai fait une bêtise

C'est **normal**. Tout le monde se plante avec Git au début. Voilà les
situations les plus fréquentes.

### « J'ai commité sur `main` par erreur ! »

Le grand classique. Pas de panique, rien n'est perdu :

```bash
git branch module-1
```

```bash
git reset --hard origin/main
```

```bash
git switch module-1
```

Ligne 1 : tu crées une branche qui pointe sur ton travail.
Ligne 2 : tu remets `main` comme sur GitHub (⚠️ **seulement si tu n'as pas
encore poussé** ton erreur).
Ligne 3 : tu vas sur ta branche, ton travail est intact.

### « Mon `push` sur `main` est refusé ! »

```
! [remote rejected] main -> main (protected branch hook declined)
```

**C'est voulu, et c'est une bonne nouvelle** : le garde-fou a fonctionné, tu as
essayé de pousser sur le cours vierge. Ton travail n'est pas perdu, il est juste
au mauvais endroit. Mets-le sur une branche :

```bash
git branch module-1
```

```bash
git reset --hard origin/main
```

```bash
git switch module-1
```

Puis pousse normalement :

```bash
git push -u origin module-1
```

### « J'ai modifié un fichier et je veux annuler »

```bash
git restore Modules/01-Premiers-Pas/Exercices/Exo.cs
```

⚠️ **Irréversible** : tes modifications non commitées sur ce fichier sont
perdues. C'est fait pour.

### « Je veux recommencer un exercice depuis zéro »

Récupère la version vierge depuis `main` :

```bash
git checkout main -- Modules/01-Premiers-Pas/Exercices/Exo.cs
```

Le fichier redevient comme au départ, avec ses `// TODO:`. Pratique pour
refaire un exercice après avoir lu la solution.

### « Mon message de commit est nul »

Tant que tu ne l'as **pas encore poussé** :

```bash
git commit --amend -m "Un bien meilleur message"
```

### « J'ai peur de tout casser »

Ton travail poussé sur GitHub est en sécurité. Tant que tu as fait `push`, même
si tu détruis ton dossier local, tu peux tout récupérer avec un `clone`.

**C'est exactement pour ça que Git existe.** Pousse souvent.

---

## 10. Les choses à NE PAS faire

| ❌ | Pourquoi |
|---|---|
| `git push --force` | tu peux effacer le travail de quelqu'un d'autre, **définitivement** |
| Travailler sur `main` | tu détruis le cours vierge |
| Commiter `bin/` et `obj/` | ce sont des fichiers générés, inutiles et énormes (le `.gitignore` les bloque déjà) |
| Un seul commit géant à la fin | si tu casses quelque chose, tu ne peux revenir nulle part |
| Attendre une semaine pour pousser | un disque dur, ça meurt |

---

## 11. Des messages que tu vas voir (et qui ne sont pas des erreurs)

**`warning: LF will be replaced by CRLF`**
Windows et Linux ne marquent pas les fins de ligne pareil. Git s'en occupe tout
seul. **Ignore ce message**, il apparaîtra souvent.

**`nothing to commit, working tree clean`**
Tout est déjà sauvegardé. C'est une **bonne** nouvelle.

**`Your branch is up to date with 'origin/module-1'`**
GitHub a exactement la même chose que toi. Parfait.

**`Everything up-to-date`**
Tu as fait `push` alors qu'il n'y avait rien de neuf. Sans conséquence.

---

## 12. 🧾 L'antisèche

Garde cette page ouverte les premières semaines.

### Au quotidien

```bash
git status
```
```bash
git add .
```
```bash
git commit -m "Mon message"
```
```bash
git push
```

### Les branches

| Je veux... | Commande |
|------------|----------|
| Voir où je suis | `git branch` |
| Créer une branche et y aller | `git switch -c module-2` |
| Aller sur une branche existante | `git switch module-1` |
| Revenir au cours vierge | `git switch main` |
| Publier une nouvelle branche | `git push -u origin module-2` |
| Faire relire un module fini | ouvrir une PR sur GitHub (§13) |

### Regarder

| Je veux... | Commande |
|------------|----------|
| Ce qui a changé | `git status` |
| Le détail des changements | `git diff` |
| Mon historique | `git log --oneline` |
| Le contenu d'un commit | `git show a3f9c21` |

### Réparer

| Je veux... | Commande |
|------------|----------|
| Annuler mes modifs d'un fichier | `git restore <fichier>` |
| Reprendre un exercice à zéro | `git checkout main -- <fichier>` |
| Corriger mon dernier message | `git commit --amend -m "..."` |

---

## 13. 🔍 La Pull Request : faire relire son code

À la fin de chaque module, tu vas ouvrir une **Pull Request** (PR). C'est le
moment où tu dis : *« j'ai fini, viens voir ce que j'ai fait »*.

Une PR, c'est **une conversation autour de ton code**. GitHub affiche
exactement ce que tu as changé, ligne par ligne, et Benoît peut commenter
**directement sur une ligne précise** :

> 💬 *ligne 42 — ici tu pourrais utiliser `Math.Max` au lieu du `if`, ça fait
> la même chose en plus court. Qu'est-ce que tu en penses ?*

C'est **exactement comme ça que travaillent tous les développeurs du monde**.
Personne ne pousse du code sans le faire relire. Même les plus expérimentés —
surtout eux, en fait.

### ⚠️ Ici, on ne FUSIONNE jamais la PR

Normalement, une PR finit par être *mergée* (fusionnée) dans `main`. **Pas ici.**

Parce que `main` doit rester le cours vierge. Si on fusionnait ta PR, tes
réponses atterriraient dans le cours et le détruiraient — exactement ce qu'on
cherche à éviter depuis le début.

👉 **Ta PR sert uniquement à la relecture.** Quand la discussion est finie, on la
**ferme** sans fusionner. Ton travail reste sur ta branche, bien au chaud.

> 🧠 Retiens ça : une Pull Request et un merge sont **deux choses séparées**.
> La PR, c'est la conversation. Le merge, c'est une décision qui vient après —
> et qu'on peut très bien ne jamais prendre.

### Ouvrir sa PR

**1.** Assure-toi d'avoir tout poussé :

```bash
git status
```
```bash
git push
```

**2.** Va sur https://github.com/benoit-eymard/adam-training

GitHub affiche un bandeau jaune : **« module-1 had recent pushes »** avec un
bouton **Compare & pull request**. Clique dessus.

*(Pas de bandeau ? Onglet **Pull requests** → bouton vert **New pull request**.)*

**3.** ⚠️ **Règle la branche de base.** Tout en haut, tu vois :

```
base: main  ←  compare: module-2
```

- Pour le **module 1** : laisse `base: main` ✅
- Pour les **modules 2 à 8** : clique sur `base: main` et choisis la branche du
  **module précédent** (`module-1` pour le module 2, etc.)

**Pourquoi ?** Parce que tes branches sont chaînées. Si tu laisses `main`, la PR
du module 5 affichera aussi tout ton travail des modules 1 à 4 — illisible.
En prenant la branche précédente, la PR ne montre **que le nouveau module**.

**4.** Remplis le titre et la description, puis **Create pull request**.

### Le titre et la description

Le titre, simple et clair :

```
Module 2 : décisions et boucles
```

La description se remplit toute seule à partir d'un modèle. Tu n'as qu'à cocher
et compléter :

```markdown
## Ce que j'ai fait
- Les 7 exercices du module 2
- Le défi : le Juste Prix, avec le mode « l'ordinateur devine »

## Tests
- [x] `dotnet test Tests` : 48/48 ✅

## Ce qui m'a bloqué
La barre de vie. Je divisais avant de multiplier, et j'avais toujours 0.

## Mes questions
- Est-ce que mon `switch` est mieux que mes `if` ? Je n'arrive pas à choisir.
- Ma méthode `Jouer()` fait 40 lignes, c'est trop long non ?
```

> 💡 **La section « Mes questions » est la plus importante.** C'est là que tu
> apprends le plus. Une PR sans question, c'est une occasion gâchée.

### Répondre aux commentaires

Benoît va laisser des remarques. **Ce ne sont pas des reproches** — c'est le
principe même de l'exercice, et ça arrive à tout le monde, toute sa carrière.

Pour chaque commentaire, tu as trois réponses possibles, toutes valables :

| Réponse | Quand |
|---------|-------|
| **Je corrige** | tu es d'accord → tu modifies le code |
| **Je demande** | tu n'as pas compris → *« pourquoi c'est mieux ? »* |
| **Je ne suis pas d'accord** | tu as une raison → explique-la ! |

La troisième est parfaitement légitime. Un relecteur peut se tromper, ou ne pas
avoir vu ton intention. **Défends ton code si tu as un argument** — c'est comme
ça qu'on progresse, des deux côtés.

Pour corriger, tu n'ouvres **pas** une nouvelle PR : tu travailles normalement
sur ta branche.

```bash
git add .
```
```bash
git commit -m "Simplifie le calcul des dégâts suite à la relecture"
```
```bash
git push
```

**La PR se met à jour toute seule.** ✨ C'est ça qui rend les PR si pratiques.

Quand un point est réglé, clique sur **Resolve conversation** sous le
commentaire. La discussion se replie : on voit d'un coup d'œil ce qu'il reste.

### Fermer la PR

Quand tout est réglé et que Benoît a validé :

1. Tu coches le module dans [PROGRESSION.md](PROGRESSION.md)
2. Tu cliques sur **Close pull request** (en bas, à côté du champ de commentaire)
3. ⚠️ **Surtout pas** sur le gros bouton vert **Merge pull request** !

La PR fermée reste consultable pour toujours. Dans six mois, tu pourras relire
tes propres questions de débutant et mesurer le chemin parcouru. C'est plus
gratifiant qu'on ne le croit. 😊

### 🧑‍🏫 Côté relecteur (pour Benoît)

Quelques repères pour que la relecture reste un plaisir :

- **Commence par ce qui est bien.** Toujours. Un ado qui ne reçoit que des
  critiques arrête au bout de trois modules.
- **Maximum 3 ou 4 remarques par PR.** Au-delà, c'est décourageant et il ne
  retient rien. Garde le reste pour la prochaine fois.
- **Pose des questions plutôt que des ordres.** *« Pourquoi as-tu choisi un
  `while` ici ? »* fait plus réfléchir que *« mets un `for` »*.
- **Distingue le bloquant du facultatif.** Une convention : préfixe par
  `nit:` (*nitpick*) ce qui n'est qu'une préférence, pas un vrai problème.
- **Si les tests passent, le code est correct.** Le reste, c'est du style — et
  le style se discute, il ne s'impose pas.

---

## 14. Pour plus tard

Quand tu seras à l'aise, il te restera à découvrir :

- **`git pull`** — récupérer les changements faits par quelqu'un d'autre
- **`git merge`** — fusionner deux branches
- **`git stash`** — mettre son travail de côté 5 minutes
- **`git bisect`** — trouver automatiquement le commit qui a introduit un bug 🕵️

Rien d'urgent. Les 10 commandes de l'antisèche couvrent 95 % de ce qu'un
développeur professionnel tape dans une journée.

---

## 🎯 En résumé

```bash
# Une fois, au début de chaque module
git switch -c module-1
git push -u origin module-1

# Puis, en boucle, à chaque exercice terminé
git status
git add .
git commit -m "Module 1 : exercice 3 terminé"
git push
```

Et à la fin du module :

```bash
# 1. Tout est poussé ?
git status

# 2. Ouvre la Pull Request sur GitHub
#    base = la branche du module précédent (main pour le module 1)
#    -> Benoît relit, tu réponds, tu corriges
#    -> puis CLOSE (jamais Merge !)

# 3. Et on enchaîne
git switch -c module-2
git push -u origin module-2
```

**Commite souvent. Pousse souvent. Ne touche jamais à `main`.**

C'est tout. 🌿
