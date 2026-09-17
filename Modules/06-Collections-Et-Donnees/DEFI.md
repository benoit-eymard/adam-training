# 🏆 Défi du Module 6 — Le Gestionnaire de Guilde

> Ton premier programme qui **se souvient**. Ferme-le, rouvre-le : tout est
> encore là.

---

## La mission

Une application de gestion complète pour ta guilde : des membres, un inventaire
partagé, un trésor — le tout **sauvegardé sur le disque**.

```
╔═══════════════════════════════════════════╗
║        GESTIONNAIRE DE GUILDE             ║
╚═══════════════════════════════════════════╝

📂 Sauvegarde trouvée : "Les Lames d'Argent" (3 membres, 1250 po)

  1. Voir les membres
  2. Recruter un membre
  3. Renvoyer un membre
  4. Gérer l'inventaire
  5. Statistiques
  6. Sauvegarder
  7. Quitter
> 5

═══ STATISTIQUES ═══

  Membres        : 3
  Niveau moyen   : 6.3
  Le plus fort   : Thorin (niveau 9)
  Trésor         : 1250 po
  Inventaire     : 7/20 objets, valeur 890 po
  Objet le + cher: Couronne ancienne (500 po)

  Répartition par classe :
    Guerrier : 2
    Mage     : 1
```

---

## Le cahier des charges

- [ ] Un menu principal en boucle (`do...while`)
- [ ] Une `List<Membre>` pour les membres (ajout, suppression, affichage)
- [ ] Un `Inventaire` partagé (réutilise ta classe !)
- [ ] Un `Dictionary<string, int>` pour la répartition par classe
      *(réutilise `CompterMots` !)*
- [ ] **Aucune saisie ne doit faire planter le programme** (`TryParse` partout)
- [ ] Sauvegarde et chargement automatique en JSON
- [ ] Au démarrage : proposer de charger la sauvegarde si elle existe
- [ ] À la sortie : proposer de sauvegarder si des changements n'ont pas été
      enregistrés

---

## 🧱 Conseils de structure

**Sépare tes classes de jeu de ta classe de sauvegarde.**

```csharp
// Classe de JEU : protégée, avec ses règles (module 4)
public class Membre
{
    public string Nom { get; private set; }
    public string Classe { get; private set; }
    public int Niveau { get; private set; }
    public void MonterDeNiveau() { Niveau++; }
}

// Classe de SAUVEGARDE : ouverte, sans logique (module 6)
public class MembreSauvegarde
{
    public string Nom { get; set; }
    public string Classe { get; set; }
    public int Niveau { get; set; }
}
```

Puis deux méthodes de conversion :

```csharp
static MembreSauvegarde VersSauvegarde(Membre m)
static Membre DepuisSauvegarde(MembreSauvegarde s)
```

C'est un peu plus de code, mais tu gardes tes garde-fous **et** tu peux
sauvegarder. C'est exactement ce que font les vrais logiciels.

> 💡 **Raccourci autorisé** si ça te paraît lourd : mets des `set` publics
> partout et sauvegarde directement tes classes de jeu. Ça marche. Tu perds
> juste la protection. Choisis en connaissance de cause — c'est ça, une décision
> d'ingénieur.

---

## Le menu qui ne plante jamais

```csharp
static int DemanderChoix(int min, int max)
{
    while (true)
    {
        Console.Write("> ");
        if (int.TryParse(Console.ReadLine(), out int choix)
            && choix >= min && choix <= max)
        {
            return choix;
        }
        Console.WriteLine($"⛔ Entre un nombre entre {min} et {max}.");
    }
}
```

Écris cette méthode **en premier** et utilise-la partout. Elle t'épargnera dix
plantages.

---

## 🌶️ Pour aller plus loin

- **Facile** : un **journal d'événements** — chaque action est ajoutée à un
  fichier texte avec la date
  ```csharp
  File.AppendAllText("journal.txt", $"{DateTime.Now} : {message}\n");
  ```

- **Facile** : trier les membres par niveau avant de les afficher
  *(le tri à bulles du module 3, sur des objets)*

- **Moyen** : une **boutique** — acheter/vendre des objets avec le trésor de la
  guilde. Attention aux règles : assez d'or ? assez de place ?

- **Moyen** : des **quêtes** — chaque quête a une difficulté, rapporte de l'or
  et de l'XP, et peut échouer si l'équipe envoyée est trop faible

- **Corsé** : **plusieurs fichiers de sauvegarde** (`guilde1.json`,
  `guilde2.json`...) avec un menu de sélection au démarrage
  *(indice : `Directory.GetFiles(".", "*.json")`)*

- **Corsé** : une **sauvegarde automatique** toutes les 5 actions, et un
  fichier `.bak` contenant la sauvegarde précédente au cas où

---

## 🐛 Les pièges de ce défi

**« Ma sauvegarde est vide / toutes les valeurs sont à 0 »**
→ Tes propriétés ont un `private set`. Le JSON a écrit le fichier correctement,
mais n'a pas pu le relire. Ouvre le `.json` dans VS Code : si les données y
sont, c'est bien la relecture qui coince.

**« InvalidOperationException pendant que je retire un membre »**
→ Tu modifies une liste pendant un `foreach`. Parcours à l'envers avec un `for`.

**« Le programme plante quand j'appuie juste sur Entrée »**
→ `int.Parse("")` lève une exception. `TryParse` ne l'aurait pas fait.

**« Mon fichier JSON est illisible, tout sur une ligne »**
→ Tu as oublié `new JsonSerializerOptions { WriteIndented = true }`.

**« KeyNotFoundException »**
→ Tu lis une clé de dictionnaire qui n'existe pas. `ContainsKey` ou
`TryGetValue` avant.

---

## ✍️ Quand tu as fini

1. **Lance le programme, ajoute des membres, quitte, relance.** Tout doit être
   là. C'est un moment satisfaisant. 😌
2. **Ouvre ton `.json` dans VS Code.** Tu comprends ce que tu lis ? C'est
   normal — c'est fait pour.
3. **Corromps-le volontairement** (supprime une accolade) et relance. Ton
   programme doit le gérer proprement, pas planter.
4. Coche le module 6 dans [PROGRESSION.md](../../PROGRESSION.md)

---

Au **module 7**, toutes ces boucles `foreach` que tu viens d'écrire vont
disparaître. Vraiment. 🪄
