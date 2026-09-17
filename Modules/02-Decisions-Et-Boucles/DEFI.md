# 🏆 Défi du Module 2 — Le Juste Prix

> Ton premier **vrai jeu**. Celui-là, tu pourras le faire jouer à quelqu'un.

---

## La mission

L'ordinateur choisit un nombre secret entre 1 et 100. Le joueur doit le trouver.
À chaque proposition, le programme répond **« c'est plus »** ou **« c'est
moins »**, jusqu'à ce que le joueur trouve.

### Exemple d'exécution

```
╔═══════════════════════════════╗
║       LE JUSTE PRIX           ║
╚═══════════════════════════════╝

J'ai choisi un nombre entre 1 et 100.
À toi de le trouver !

Ta proposition : 50
C'est PLUS ! (essai 1)

Ta proposition : 75
C'est MOINS ! (essai 2)

Ta proposition : 62
C'est MOINS ! (essai 3)

Ta proposition : 56
🎉 BRAVO ! C'était bien 56 !
Tu l'as trouvé en 4 essais.
```

---

## Comment tirer un nombre au hasard

```csharp
Random hasard = new Random();
int secret = hasard.Next(1, 101);   // un entier entre 1 et 100 inclus
```

⚠️ `Next(1, 101)` et non `Next(1, 100)` : la **borne haute est exclue**. C'est
un piège très fréquent en programmation, retiens-le.

> 💡 Pendant que tu développes, affiche le secret pour te faciliter les tests,
> puis commente cette ligne quand ça marche :
> ```csharp
> Console.WriteLine($"[DEBUG] le secret est {secret}");
> ```

---

## Le cahier des charges

- [ ] Tirer un nombre secret entre 1 et 100
- [ ] Boucler tant que le joueur n'a pas trouvé (`while`)
- [ ] Lire sa proposition et la convertir en `int`
- [ ] Répondre « c'est plus » / « c'est moins » / « trouvé ! » (`if`)
- [ ] Compter les essais et l'afficher à la fin

---

## 🌶️ Pour aller plus loin

- **Facile** : limite le joueur à **10 essais**. S'il échoue, révèle le secret.
  *(indice : ajoute une condition dans ton `while`, ou un `break`)*

- **Facile** : ajoute un commentaire selon la performance —
  moins de 5 essais « Excellent ! », moins de 8 « Pas mal », sinon « Ouf ! »

- **Moyen** : propose de **rejouer** à la fin (« Une autre partie ? o/n »).
  *(indice : un `do...while` autour de toute la partie)*

- **Moyen** : mémorise le **meilleur score** de la session et affiche-le entre
  les parties.

- **Corsé** : **inverse les rôles**. C'est le joueur qui choisit un nombre dans
  sa tête, et l'ordinateur qui devine. Tu lui réponds « plus » / « moins » /
  « trouvé ». Fais en sorte que l'ordinateur trouve en **7 coups maximum**.
  *(indice : il doit toujours proposer le milieu de l'intervalle restant — ça
  s'appelle la recherche dichotomique, et c'est un algorithme fondamental)*

- **Corsé** : détecte le **triche**. Si les réponses du joueur sont
  contradictoires (il a dit « plus » puis « moins » sur des nombres
  incompatibles), démasque-le.

---

## 🎨 Bonus présentation

```csharp
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("BRAVO !");
Console.ResetColor();
```

Couleurs disponibles : `Red`, `Green`, `Yellow`, `Cyan`, `Magenta`, `White`,
`DarkGray`...

Et pour faire durer le suspense :

```csharp
Thread.Sleep(500);   // attend une demi-seconde
```

*(petit avant-goût du module 8 : `Thread`, ça te dira quelque chose plus tard...)*

---

## ✍️ Quand tu as fini

1. **Fais-y jouer quelqu'un.** Regarde-le jouer sans rien dire — tu verras
   immédiatement ce qui manque à ton programme.
2. Coche le module 2 dans [PROGRESSION.md](../../PROGRESSION.md)
3. Direction le **Module 3** : tu vas apprendre à découper ton code en morceaux
   réutilisables. Et coder un **Morpion**. 🎯
