# 🏆 Défi du Module 9 — Le Générateur de Donjon Infini

> Un donjon **sans fin**, qui ne pèse rien en mémoire. Les salles n'existent
> qu'au moment où le joueur y entre.

---

## La mission

```
╔═══════════════════════════════════════════════╗
║        LE DONJON SANS FIN                     ║
╚═══════════════════════════════════════════════╝

  Salle 1  ·  Couloir humide          (vide)
  Salle 2  ·  Caverne obscure         👹 Gobelin (niv. 1)
  Salle 3  ·  Salle aux colonnes      💰 Coffre : 40 po
  Salle 4  ·  Pont de pierre          👹 Orc (niv. 2)
  Salle 5  ·  Bibliothèque oubliée    ⚗️ Potion
  ...
  Salle 42 ·  Antre du dragon         🐉 BOSS (niv. 9)

> continuer / stats / quitter
```

Le donjon est **infini**. Le joueur avance salle par salle, aussi loin qu'il
survit. Et pourtant, ton programme n'en garde presque rien en mémoire.

---

## ⛔ La règle du défi

**Le donjon doit être un `IEnumerable<Salle>` infini, écrit avec `yield`.**

```csharp
static IEnumerable<Salle> Donjon()
{
    int numero = 1;
    while (true)
    {
        yield return GenererSalle(numero);
        numero++;
    }
}
```

**Interdit** : construire une `List<Salle>` de 1000 salles au démarrage. Le but
est justement de ne rien construire d'avance.

> 🧠 **Comment vérifier que tu as réussi ?** Ajoute un compteur dans
> `GenererSalle`. Après que le joueur a visité 5 salles, il doit valoir
> **exactement 5**. Si tu vois 1000, tu as construit le donjon d'avance.
>
> C'est le principe de l'`Espion` des exercices, appliqué à ton propre jeu.

---

## 📋 Le cahier des charges

- [ ] Un `record Salle(int Numero, string Nom, string Contenu)`
      *(record, pas class — c'est une **valeur**, module 4 section 10 !)*
- [ ] Un générateur `Donjon()` infini avec `yield return`
- [ ] La difficulté **monte avec le numéro** de salle
- [ ] Un boss toutes les 10 salles
- [ ] Le joueur avance salle par salle et peut s'arrêter quand il veut
- [ ] Un écran de statistiques en fin de partie (LINQ, module 7)
- [ ] **Preuve de paresse** : afficher le nombre de salles réellement générées

---

## 💡 Les requêtes qui vont te servir

Une fois le donjon écrit, tu peux l'interroger **comme n'importe quelle
collection** — alors qu'il est infini. C'est tout le plaisir du module.

```csharp
// Les 5 prochaines salles à partir de la 20e
Donjon().Skip(19).Take(5)

// Les 3 prochains boss
Donjon().Where(s => s.EstUnBoss).Take(3)

// La première salle avec un trésor
Donjon().First(s => s.Contenu.Contains("Coffre"))

// Explorer tant que le héros survit  ⭐ le plus élégant
Donjon().TakeWhile(s => heros.EstVivant)
```

> 🆕 **`TakeWhile`** : prend les éléments **tant qu'une condition est vraie**, et
> s'arrête au premier qui échoue. C'est le compagnon naturel des séquences
> infinies — et il remplace toute une boucle `while` avec drapeau.

---

## 🧱 Conseils de structure

### Générer une salle de façon déterministe

Pour que la salle 42 soit **toujours la même**, sème le hasard à partir du
numéro :

```csharp
static Salle GenererSalle(int numero)
{
    Random hasard = new Random(numero);   // la graine = le numéro
    // ...
}
```

Deux appels à `GenererSalle(42)` donneront exactement la même salle. C'est ce
qu'on appelle la **génération procédurale**, et c'est ce qui permet à des jeux
comme Minecraft de stocker un monde entier dans un seul nombre. 🌍

### Le niveau de difficulté

```csharp
int difficulte = 1 + numero / 5;
bool estUnBoss = numero % 10 == 0;
```

Le modulo du module 1, encore lui.

---

## 🌶️ Pour aller plus loin

- **Facile** : `Donjon().Where(s => s.EstUnBoss).Take(3)` pour afficher les
  prochains boss en guise de « carte »

- **Moyen** : un générateur **de butin** infini, `Butin()`, que le joueur pioche
  à chaque coffre trouvé

- **Moyen** : `TakeWhile(s => heros.EstVivant)` pour que l'exploration s'arrête
  toute seule à la mort du héros — sans aucune boucle `while`

- **Corsé** : une méthode d'extension **à toi** sur `IEnumerable<Salle>` :
  ```csharp
  public static IEnumerable<Salle> SansBoss(this IEnumerable<Salle> salles)
      => salles.Where(s => !s.EstUnBoss);
  ```
  Tu enrichis LINQ avec ton propre vocabulaire métier. C'est exactement ce que
  font les équipes professionnelles.

- **Corsé** : un générateur **de nombres premiers** infini
  ```csharp
  static IEnumerable<int> Premiers()
  {
      for (int n = 2; ; n++)
      {
          if (EstPremier(n)) yield return n;
      }
  }
  ```
  Puis `Premiers().Take(100).Last()` — le 100ᵉ nombre premier, sans jamais avoir
  décidé d'avance combien en calculer.

- **Très corsé** : un **crible d'Ératosthène paresseux**, où chaque premier
  trouvé filtre la séquence pour les suivants. C'est un grand classique de la
  programmation fonctionnelle, et c'est très élégant.

---

## 🐛 Les pièges de ce défi

**« Mon programme se fige au démarrage »**
→ Tu as fait `.ToList()`, `.Count()` ou `.OrderBy()` sur le donjon infini.
`OrderBy` doit tout lire pour trier — il ne peut pas être paresseux. **Borne
toujours avant** : `Donjon().Take(50).OrderBy(...)`.

**« Toutes mes salles sont identiques »**
→ `new Random()` sans graine, appelé en boucle très vite, peut rendre la même
suite. Utilise `new Random(numero)`.

**« La salle 42 change à chaque partie »**
→ Tu utilises un `Random` partagé au lieu d'un `Random(numero)`.

**« Mon compteur dit 1000 alors que j'ai visité 5 salles »**
→ Tu as construit le donjon d'avance quelque part. Cherche une `List` ou un
`.ToList()` caché.

---

## ✍️ Quand tu as fini

1. **Affiche le nombre de salles générées** à la fin de la partie. Si le joueur
   en a visité 12 et que le compteur dit 12, **tu as compris le module**. 🎯
2. Explique à quelqu'un comment ton donjon peut être infini sans saturer la
   mémoire. Si tu y arrives en deux phrases, c'est gagné.
3. Coche le module 9 dans [PROGRESSION.md](../../PROGRESSION.md)

---

> 💬 Ce que tu viens de coder — une séquence infinie générée à la demande — est
> exactement la technique utilisée par les jeux à monde ouvert, les flux de
> données en temps réel et les API paginées. Tu as 13 ans et tu maîtrises un
> outil que beaucoup de développeurs professionnels n'ont jamais écrit
> eux-mêmes. 🔬
