# Module 6 — Collections & données 🎒

> **Objectif** : des collections qui grandissent toutes seules, des programmes
> qui ne plantent plus, et des parties qu'on peut sauvegarder.
>
> ⏱️ Lecture : ~30 min · Exercices : ~2h30

---

## 1. `List<T>` : le tableau élastique

Le problème du tableau : sa **taille est figée** à la création.

```csharp
Combattant[] equipe = new Combattant[3];
// Et si un 4e héros rejoint l'équipe ? 😬
```

La solution :

```csharp
List<string> inventaire = new List<string>();

inventaire.Add("Épée");           // il y a 1 élément
inventaire.Add("Potion");         // 2
inventaire.Add("Bouclier");       // 3
inventaire.Remove("Potion");      // 2
```

Le `<string>` se lit « de string ». C'est un **générique** : `List<T>` est un
moule dont tu choisis le contenu.

```csharp
List<int> scores = new List<int>();
List<Combattant> equipe = new List<Combattant>();
List<Arme> armurerie = new List<Arme>();
```

> 💡 Un seul code dans .NET gère tous ces cas. Sans les génériques, il aurait
> fallu écrire `ListDeString`, `ListDeInt`, `ListDeCombattant`... C'est une des
> plus belles idées du langage.

### Les opérations principales

```csharp
List<string> sac = new List<string> { "Épée", "Potion", "Carte" };

sac.Add("Corde");              // ajoute à la fin
sac.Insert(0, "Torche");       // insère à la position 0
sac.Remove("Potion");          // retire la 1re occurrence (retourne bool)
sac.RemoveAt(2);               // retire par index
sac.Clear();                   // vide tout

sac.Count                      // ⚠️ Count, pas Length !
sac.Contains("Épée")           // true / false
sac.IndexOf("Épée")            // l'index, ou -1
sac[0]                         // accès par index, comme un tableau
sac.Sort();                    // trie sur place
sac.Reverse();                 // inverse sur place
```

| | Tableau | `List<T>` |
|---|---|---|
| Taille | fixe | élastique |
| Compter | `.Length` | `.Count` |
| Ajouter | impossible | `.Add()` |
| Accès par index | ✅ | ✅ |

> ⚠️ **`Length` vs `Count`** : tu vas te tromper. Souvent. Le tableau a une
> **longueur**, la liste a un **compte**. Aucune logique profonde — c'est
> historique. VS Code te corrigera.

### Parcourir une liste

```csharp
foreach (string objet in sac)
{
    Console.WriteLine(objet);
}
```

> 🔴 **Piège sévère** : **ne modifie jamais une liste pendant que tu la
> parcours** avec `foreach`. Le programme lève une `InvalidOperationException`.
>
> ```csharp
> foreach (string o in sac)
> {
>     if (o == "Potion") sac.Remove(o);   // 💥
> }
> ```
>
> **Les solutions** : parcourir **à l'envers** avec un `for`, ou construire une
> nouvelle liste.
>
> ```csharp
> for (int i = sac.Count - 1; i >= 0; i--)
> {
>     if (sac[i] == "Potion") sac.RemoveAt(i);   // ✅
> }
> ```
>
> À l'envers, retirer un élément ne décale pas ceux qu'il reste à visiter.

---

## 2. `Dictionary<TCle, TValeur>` : l'annuaire

Une `List` retrouve par **position**. Un `Dictionary` retrouve par **clé**.

```csharp
Dictionary<string, int> scores = new Dictionary<string, int>();

scores["Thorin"] = 120;
scores["Elyra"] = 95;

Console.WriteLine(scores["Thorin"]);   // 120
```

C'est l'outil idéal dès que tu penses « à X correspond Y » :

```csharp
Dictionary<string, int> prix;           // nom d'objet → prix
Dictionary<char, int> occurrences;      // lettre → nombre d'apparitions
Dictionary<string, Combattant> equipe;  // nom → personnage
```

### Les opérations

```csharp
scores["Sylas"] = 80;           // ajoute OU écrase si la clé existe
scores.Add("Kira", 70);         // ajoute, mais 💥 si la clé existe déjà
scores.Remove("Kira");
scores.ContainsKey("Thorin")    // true
scores.Count                    // nombre de paires
```

> 🔴 **Lire une clé absente fait planter le programme** avec une
> `KeyNotFoundException`. Toujours vérifier :
>
> ```csharp
> if (scores.ContainsKey("Inconnu"))
> {
>     Console.WriteLine(scores["Inconnu"]);
> }
>
> // ou, en une seule opération (plus rapide) :
> if (scores.TryGetValue("Inconnu", out int valeur))
> {
>     Console.WriteLine(valeur);
> }
> ```

### Le motif « compter des occurrences »

À connaître par cœur, il sert constamment :

```csharp
Dictionary<string, int> comptes = new Dictionary<string, int>();

foreach (string mot in mots)
{
    if (comptes.ContainsKey(mot))
    {
        comptes[mot]++;         // déjà vu : on incrémente
    }
    else
    {
        comptes[mot] = 1;       // première fois : on initialise à 1
    }
}
```

### Parcourir un dictionnaire

```csharp
foreach (KeyValuePair<string, int> paire in scores)
{
    Console.WriteLine($"{paire.Key} : {paire.Value}");
}

// Plus court, avec var :
foreach (var paire in scores)
{
    Console.WriteLine($"{paire.Key} : {paire.Value}");
}

foreach (string nom in scores.Keys) { }      // juste les clés
foreach (int score in scores.Values) { }     // juste les valeurs
```

> ⚠️ L'ordre de parcours d'un `Dictionary` n'est **pas garanti**. Si l'ordre
> compte pour toi, trie explicitement, ou utilise une `List`.

---

## 3. Les exceptions : quand ça tourne mal

Une **exception**, c'est une erreur qui survient **pendant** l'exécution — pas à
la compilation. Le programme s'arrête net.

```csharp
int age = int.Parse("bonjour");     // 💥 FormatException
int[] t = new int[3];
Console.WriteLine(t[10]);           // 💥 IndexOutOfRangeException
Personnage p = null;
Console.WriteLine(p.Nom);           // 💥 NullReferenceException
Console.WriteLine(10 / 0);          // 💥 DivideByZeroException
```

Tu les as toutes déjà rencontrées. 😄

### `try` / `catch`

```csharp
try
{
    int age = int.Parse(Console.ReadLine());
    Console.WriteLine($"Tu as {age} ans.");
}
catch (FormatException)
{
    Console.WriteLine("Ce n'est pas un nombre valide !");
}
```

- `try` : « essaie ce bloc »
- `catch` : « si ça explose avec cette erreur-là, fais plutôt ça »

Le programme **continue** au lieu de mourir.

### Attraper le bon type

```csharp
try
{
    // ...
}
catch (FormatException e)
{
    Console.WriteLine($"Format invalide : {e.Message}");
}
catch (OverflowException)
{
    Console.WriteLine("Ce nombre est bien trop grand !");
}
catch (Exception e)              // le filet de sécurité : attrape TOUT
{
    Console.WriteLine($"Erreur inattendue : {e.Message}");
}
```

⚠️ L'ordre va **du plus précis au plus général**. `Exception` est le parent de
toutes les exceptions ; s'il passe en premier, les autres `catch` deviennent
inatteignables (et le compilateur refuse).

### `finally`

```csharp
finally
{
    // s'exécute TOUJOURS : erreur ou pas
}
```

Sert à libérer des ressources (fermer un fichier, une connexion).

### 🧠 N'abuse pas de `try/catch`

```csharp
// ❌ Mauvais : on utilise une exception pour un cas NORMAL
try { int n = int.Parse(saisie); }
catch { n = 0; }

// ✅ Bon : on teste, sans exception
if (int.TryParse(saisie, out int n)) { }
else { n = 0; }
```

**Une exception doit rester exceptionnelle.** Un utilisateur qui tape n'importe
quoi, c'est prévisible, donc ce n'est pas exceptionnel : ça se teste. Les
exceptions sont **lentes** et masquent la logique.

> **La règle** : `try/catch` pour ce que tu **ne peux pas** prévoir (disque
> plein, fichier corrompu, réseau coupé). Un `if` pour ce que tu **peux** prévoir.

### `TryParse` : le motif à retenir

```csharp
if (int.TryParse(saisie, out int nombre))
{
    // la conversion a marché, "nombre" contient le résultat
}
else
{
    // ce n'était pas un nombre — aucune exception levée
}
```

Le mot-clé `out` signifie « ce paramètre est une **sortie** supplémentaire ». La
méthode retourne un `bool` (ça a marché ou non) **et** remplit `nombre`.

Il existe partout : `double.TryParse`, `bool.TryParse`,
`DateTime.TryParse`, `dictionnaire.TryGetValue`...

---

## 4. Les fichiers

```csharp
// Écrire (écrase le fichier s'il existe)
File.WriteAllText("partie.txt", "Kaelis;100;12");

// Lire tout
string contenu = File.ReadAllText("partie.txt");

// Ligne par ligne
string[] lignes = File.ReadAllLines("partie.txt");
File.WriteAllLines("partie.txt", lignes);

// Ajouter à la fin
File.AppendAllText("journal.txt", "Nouvelle entrée\n");

// Vérifier l'existence — le réflexe AVANT de lire
if (File.Exists("partie.txt")) { }

File.Delete("partie.txt");
```

> ⚠️ Lire un fichier absent lève une `FileNotFoundException`. Et un fichier
> peut aussi être verrouillé, corrompu, sur un disque plein... **C'est le cas
> typique où `try/catch` est justifié** : tu ne peux pas tout prévoir.

---

## 5. Le JSON : sauvegarder des objets

Écrire `"Kaelis;100;12"` à la main, ça marche... jusqu'au jour où un nom
contient un `;`. Le **JSON** est un format texte standard, lisible par les
humains et par tous les langages.

```csharp
using System.Text.Json;

Partie partie = new Partie { NomDuHeros = "Kaelis", Niveau = 5 };

// Objet → texte JSON  (SÉRIALISER)
string json = JsonSerializer.Serialize(partie);
File.WriteAllText("sauvegarde.json", json);

// Texte JSON → objet  (DÉSÉRIALISER)
string lu = File.ReadAllText("sauvegarde.json");
Partie chargee = JsonSerializer.Deserialize<Partie>(lu);
```

Le fichier ressemble à ça :

```json
{"NomDuHeros":"Kaelis","Niveau":5,"Objets":["Épée","Potion"]}
```

Pour le rendre lisible :

```csharp
var options = new JsonSerializerOptions { WriteIndented = true };
string json = JsonSerializer.Serialize(partie, options);
```

```json
{
  "NomDuHeros": "Kaelis",
  "Niveau": 5,
  "Objets": ["Épée", "Potion"]
}
```

### ⚠️ Les deux règles du JSON en C#

Pour qu'une classe soit sérialisable :

1. ses propriétés doivent être **`public`** avec un **`get` ET un `set`
   publics** (un `private set` ne sera pas relu !)
2. elle doit avoir un **constructeur sans paramètres**

```csharp
public class Partie
{
    public string NomDuHeros { get; set; }     // ✅ get ET set publics
    public int Niveau { get; set; }
    public List<string> Objets { get; set; } = new List<string>();
}
```

> 🧠 Ça contredit ce qu'on a dit au module 4 sur `private set` ! C'est un vrai
> compromis d'ingénieur : on crée souvent une classe **spécifique à la
> sauvegarde** (simple, ouverte, sans logique), distincte des classes du jeu
> (protégées, avec leurs règles). On dit que c'est un **DTO** — *Data Transfer
> Object*. Chaque classe a un seul métier.

---

## 6. Les initialiseurs

Deux raccourcis d'écriture très courants :

```csharp
// Initialiseur de collection
List<string> sac = new List<string> { "Épée", "Potion", "Carte" };

Dictionary<string, int> prix = new Dictionary<string, int>
{
    ["Épée"] = 50,
    ["Potion"] = 10
};

// Initialiseur d'objet : crée ET remplit
Partie p = new Partie
{
    NomDuHeros = "Kaelis",
    Niveau = 5
};
```

---

## 🎯 Récapitulatif

| Je veux... | J'écris |
|------------|---------|
| Une collection élastique | `List<string> l = new List<string>();` |
| Ajouter / retirer | `l.Add(x);` `l.Remove(x);` |
| Sa taille | `l.Count` (pas `Length` !) |
| Associer clé → valeur | `Dictionary<string, int> d = new();` |
| Lire sans risque | `if (d.TryGetValue(k, out int v))` |
| Convertir sans risque | `if (int.TryParse(s, out int n))` |
| Gérer une erreur imprévisible | `try { } catch (Exception e) { }` |
| Lire un fichier | `File.ReadAllText(chemin)` |
| Vérifier avant de lire | `if (File.Exists(chemin))` |
| Objet → JSON | `JsonSerializer.Serialize(obj)` |
| JSON → objet | `JsonSerializer.Deserialize<T>(json)` |

---

## ▶️ À toi de jouer

Trois fichiers à remplir :

1. `Exercices/Exo.cs` — listes, dictionnaires, exceptions
2. `Exercices/Inventaire.cs` — une vraie classe qui utilise une `List<Objet>`
3. `Exercices/Sauvegarde.cs` — fichiers et JSON

```bash
dotnet run --project Exercices
```

```bash
dotnet test Tests
```
