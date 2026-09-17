# 🏆 Défi final — Le Donjon en Temps Réel

> Le dernier défi du parcours. Il utilise **tout** ce que tu as appris depuis le
> module 1.

---

## La mission

Un combat de RPG **en temps réel** : les monstres n'attendent pas poliment ton
tour. Si tu réfléchis trop longtemps, tu prends des coups.

```
╔═══════════════════════════════════════════════════╗
║          LE DONJON — Salle 3                      ║
╚═══════════════════════════════════════════════════╝

  Kaelis   [########--] 82/100      ⏱️  4s
  Gobelin  [#####-----] 15/30
  Orc      [##########] 60/60

  > _

  ⚔️  Le Gobelin t'attaque ! -7 PV
```

Le chronomètre tourne. Le joueur tape sa commande. Les monstres attaquent
**pendant** qu'il réfléchit.

---

## 🧱 L'architecture

Trois choses qui tournent **en même temps** :

```csharp
// 1. La boucle des monstres : ils attaquent toutes les 3 secondes
Task boucleMonstres = Task.Run(async () =>
{
    while (!partieTerminee)
    {
        await Task.Delay(3000);
        FaireAttaquerLesMonstres();
    }
});

// 2. Le rafraîchissement de l'affichage, 4 fois par seconde
Task boucleAffichage = Task.Run(async () =>
{
    while (!partieTerminee)
    {
        await Task.Delay(250);
        RafraichirEcran();
    }
});

// 3. La saisie du joueur (sur le thread principal)
while (!partieTerminee)
{
    string commande = Console.ReadLine();
    TraiterCommande(commande);
}
```

> ⚠️ **Et voilà : tu as maintenant trois threads qui touchent aux MÊMES
> données** (les PV du héros, la liste des monstres, l'état de la partie).
>
> **C'est exactement le terrain de jeu des race conditions.** Ce défi n'est pas
> un exercice de style : c'est le vrai problème que ce module t'a appris à
> résoudre.

---

## 🔒 Ce qui DOIT être protégé

Toute donnée lue par un thread et écrite par un autre :

```csharp
private readonly object _verrouCombat = new object();

void FaireAttaquerLesMonstres()
{
    lock (_verrouCombat)
    {
        foreach (var m in monstres.Where(m => m.EstVivant))
        {
            m.Attaquer(heros);
        }
    }
}

void TraiterCommande(string commande)
{
    lock (_verrouCombat)
    {
        // le héros attaque, boit une potion...
    }
}
```

**Sans ces `lock`**, tu auras des bugs impossibles à reproduire :

- le héros meurt alors qu'il vient de boire une potion
- un monstre mort attaque quand même
- les PV affichés ne correspondent à rien
- `InvalidOperationException` parce qu'un thread modifie la liste pendant qu'un
  autre la parcourt (`foreach` + `Remove`, souvenir du module 6 !)

> 🧠 **Le vrai enseignement du défi** : ces bugs n'arriveront pas tout de suite.
> Ils arriveront la 15ᵉ fois que tu lances le jeu, sans raison apparente. Et
> tu comprendras enfin pourquoi ce module insiste tant.

### Le drapeau partagé

```csharp
private volatile bool _partieTerminee = false;
```

Le mot-clé **`volatile`** dit au compilateur : « cette variable peut changer
depuis un autre thread, ne l'optimise pas, relis-la vraiment à chaque fois ».
Sans lui, un thread peut garder une copie périmée en cache et tourner à l'infini.

---

## 📋 Le cahier des charges

- [ ] Un héros (tes classes du module 5 : `Guerrier`, `Mage`, `Archer`)
- [ ] 2 à 3 monstres par salle
- [ ] **Les monstres attaquent automatiquement** toutes les N secondes
- [ ] **L'affichage se rafraîchit** en continu, avec le chronomètre
- [ ] Le joueur tape des commandes : `attaque`, `potion`, `rage`, `fuite`
- [ ] **Toutes les données partagées sont protégées par `lock`**
- [ ] 3 salles de difficulté croissante
- [ ] Sauvegarde JSON entre les salles (module 6)
- [ ] Un écran de statistiques de fin (LINQ, module 7)

---

## 💡 Astuces techniques

### Effacer l'écran proprement

```csharp
Console.Clear();              // simple, mais ça scintille
Console.SetCursorPosition(0, 0);   // mieux : on réécrit par-dessus
```

### Lire une touche sans bloquer

```csharp
if (Console.KeyAvailable)
{
    ConsoleKeyInfo touche = Console.ReadKey(true);
    // ...
}
```

Contrairement à `Console.ReadLine()`, ça ne bloque pas : parfait pour une boucle
de jeu.

### Arrêter proprement les tâches de fond

```csharp
_partieTerminee = true;
await Task.WhenAll(boucleMonstres, boucleAffichage);
Console.WriteLine("Partie terminée.");
```

Sans ce `await`, ton programme pourrait se fermer pendant qu'une tâche écrit
encore à l'écran. Toujours **attendre la fin** de ce qu'on a lancé.

---

## 🌶️ Pour aller plus loin

- **Moyen** : chaque monstre a **sa propre vitesse d'attaque** — chacun sa
  propre `Task` avec son propre `Task.Delay`

- **Moyen** : un **temps de recharge** sur les compétences (la rage ne se
  relance pas avant 10 secondes)

- **Moyen** : des **effets sur la durée** — un poison qui retire 2 PV par
  seconde pendant 10 secondes, dans sa propre tâche

- **Corsé** : remplace tous tes `lock` par un **`SemaphoreSlim`**, qui
  fonctionne avec `await` (là où `lock` ne le peut pas)
  ```csharp
  private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

  await _semaphore.WaitAsync();
  try { /* zone protégée */ }
  finally { _semaphore.Release(); }
  ```
  ⚠️ Le `finally` est **obligatoire** : sans lui, une exception laisserait le
  sémaphore verrouillé pour toujours.

- **Corsé** : un **`CancellationToken`** pour arrêter proprement toutes les
  tâches d'un coup — c'est la façon professionnelle de faire
  ```csharp
  var cts = new CancellationTokenSource();
  await Task.Delay(3000, cts.Token);     // s'interrompt si on annule
  cts.Cancel();                          // stoppe tout
  ```

---

## 🐛 Les bugs que tu vas rencontrer

**L'affichage est illisible, tout se mélange**
→ Deux threads écrivent dans la console en même temps. Mets un `lock` autour de
l'affichage, ou fais écrire un seul thread.

**Le programme ne s'arrête pas quand je quitte**
→ Une tâche de fond tourne encore. Vérifie ton drapeau `_partieTerminee`, et
qu'il est bien `volatile`.

**« InvalidOperationException: Collection was modified »**
→ Un thread modifie la liste des monstres pendant qu'un autre la parcourt.
`lock` autour des deux.

**Les PV du héros sont incohérents**
→ Race condition classique. Une attaque et un soin en même temps : l'un écrase
l'autre. Toutes les modifications de PV doivent passer par le même verrou.

**Ça marche... puis ça ne marche plus, sans que j'aie rien changé**
→ Bienvenue dans le monde de la concurrence. 😄 C'est le signe qu'il manque un
verrou quelque part.

---

## 🎓 Quand tu as fini

Prends un moment. Regarde ce que tu viens de construire :

- des **classes** avec de l'héritage et du polymorphisme
- des **collections** manipulées avec LINQ
- une **sauvegarde** sur disque
- plusieurs **threads** qui coopèrent sans se marcher dessus
- une **gestion d'erreurs** qui empêche le programme de mourir

**C'est un vrai programme.** Pas un exercice.

1. **Fais-y jouer quelqu'un.** C'est le seul test qui compte.
2. Coche le module 8 et le projet final dans
   [PROGRESSION.md](../../PROGRESSION.md)
3. Relis ton carnet de bord depuis le module 1. Tu verras le chemin parcouru.

---

## 🚀 Et après ?

Tu as maintenant les bases solides d'un développeur. La suite dépend de ce que
tu as envie de faire :

| Envie | Direction |
|-------|-----------|
| 🎮 Des jeux | **Unity** — c'est du C#, tu es prêt |
| 🌐 Des sites web | **ASP.NET Core** ou **Blazor** |
| 🖥️ Des applis fenêtrées | **Avalonia** ou **MAUI** |
| 💾 Des données | **Entity Framework** + SQL |
| 🤝 Travailler à plusieurs | **Git** et GitHub (le plus utile de tous) |

Mais le meilleur conseil reste le même :

> **Choisis un projet dont TU as envie, et construis-le.**
> Tu buteras, tu chercheras, tu trouveras. C'est comme ça qu'on devient bon —
> pas en suivant des cours. Même celui-ci. 😉

Bravo. 🏆
