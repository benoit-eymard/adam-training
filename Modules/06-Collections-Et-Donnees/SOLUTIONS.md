# 💡 Solutions — Module 6

> ⛔ **À lire après avoir essayé.**

---

## Exercice 1 — ConvertirOuDefaut

```csharp
public static int ConvertirOuDefaut(string saisie, int valeurParDefaut)
{
    if (int.TryParse(saisie, out int nombre))
    {
        return nombre;
    }
    return valeurParDefaut;
}
```

**Le motif `TryParse`, à graver dans ta mémoire.** Il fait deux choses en un
appel :

- il **retourne** un `bool` : « est-ce que ça a marché ? »
- il **remplit** le paramètre `out` avec le résultat

Le `out int nombre` déclare la variable **au moment de l'appel**. Elle reste
utilisable après le `if`.

`TryParse` gère tout seul les cas `null`, `""`, `"abc"`, `"12.5"` (qui n'est pas
un **entier**) et même les nombres trop grands pour un `int`. Aucune exception
n'est levée.

---

## Exercice 2 — SansDoublons

```csharp
public static List<string> SansDoublons(List<string> elements)
{
    List<string> resultat = new List<string>();
    foreach (string e in elements)
    {
        if (!resultat.Contains(e))
        {
            resultat.Add(e);
        }
    }
    return resultat;
}
```

On construit une **nouvelle** liste — l'originale n'est jamais touchée. C'est
le bon réflexe par défaut.

L'ordre de première apparition est conservé « gratuitement » : on parcourt dans
l'ordre, on ajoute dans l'ordre.

> 💡 En vrai, on utiliserait `.Distinct()` (module 7 !) ou un `HashSet<string>`,
> qui teste l'appartenance bien plus vite. Mais `Contains` en boucle est parfait
> pour comprendre le principe.

---

## Exercice 3 — CompterMots

```csharp
public static Dictionary<string, int> CompterMots(string[] mots)
{
    Dictionary<string, int> comptes = new Dictionary<string, int>();
    foreach (string mot in mots)
    {
        if (comptes.ContainsKey(mot))
        {
            comptes[mot]++;
        }
        else
        {
            comptes[mot] = 1;
        }
    }
    return comptes;
}
```

**Le motif « compter des occurrences ».** Tu le réutiliseras toute ta vie :
compter des mots dans un texte, des clics par bouton, des erreurs par type, des
ventes par produit...

Pourquoi le `if` ? Parce que `comptes[mot]++` sur une clé **absente** lève une
`KeyNotFoundException` : on ne peut pas incrémenter ce qui n'existe pas.

> 💡 Version compacte, une fois le principe compris :
> ```csharp
> comptes.TryGetValue(mot, out int actuel);   // met 0 si absent
> comptes[mot] = actuel + 1;
> ```

---

## Exercice 4 — LePlusFrequent

```csharp
public static string LePlusFrequent(string[] mots)
{
    if (mots.Length == 0) return null;

    Dictionary<string, int> comptes = CompterMots(mots);

    string meilleur = null;
    int meilleurCompte = 0;

    foreach (string mot in mots)          // ⬅️ on parcourt le TABLEAU
    {
        if (comptes[mot] > meilleurCompte)
        {
            meilleur = mot;
            meilleurCompte = comptes[mot];
        }
    }
    return meilleur;
}
```

### 🧠 Pourquoi parcourir le tableau et non le dictionnaire ?

**L'ordre de parcours d'un `Dictionary` n'est pas garanti.** Il dépend de la
façon dont les clés ont été rangées en interne — pas de l'ordre d'insertion. Sur
`["a", "b"]` (les deux à 1 occurrence), parcourir le dictionnaire pourrait
retourner `"b"` un jour et `"a"` un autre.

En parcourant le **tableau d'origine**, on respecte l'ordre réel. Et comme on
utilise `>` strict (et non `>=`), le premier rencontré garde l'avantage en cas
d'égalité.

> ⚠️ Ce genre de subtilité — « ça marche sur ma machine mais pas sur la
> tienne » — est la source de bugs la plus pénible qui soit. Quand un ordre
> compte, ne le laisse jamais au hasard.

---

## Exercice 5 — DiviserSansPlanter

```csharp
public static int DiviserSansPlanter(int a, int b)
{
    try
    {
        return a / b;
    }
    catch (DivideByZeroException)
    {
        return 0;
    }
}
```

**C'était un exercice d'entraînement à la syntaxe.** En vrai, tu devrais écrire :

```csharp
if (b == 0) return 0;
return a / b;
```

**Pourquoi c'est mieux ?**

1. **C'est plus rapide.** Lever et rattraper une exception coûte des milliers de
   fois plus cher qu'un `if`. Dans une boucle, ça se voit.
2. **C'est plus clair.** L'intention (« zéro n'est pas un diviseur valide ») est
   dite explicitement.
3. **`try/catch` attrape parfois trop.** Un `catch` mal ciblé peut masquer un
   bug complètement différent, et tu chercheras pendant des heures.

> **La règle du module** : `if` pour ce que tu peux prévoir, `try/catch` pour ce
> que tu ne peux pas.

---

## Exercice 6 — ElementSur

```csharp
public static string ElementSur(List<string> liste, int index)
{
    if (liste == null) return "";
    if (index < 0 || index >= liste.Count) return "";
    return liste[index];
}
```

Trois lignes, trois clauses de garde, **zéro exception**. L'ordre compte : il
faut tester `null` **avant** d'appeler `.Count` dessus.

---

## Exercice 7 — MoyenneDesValides

```csharp
public static double MoyenneDesValides(string[] saisies)
{
    int total = 0;
    int compteur = 0;

    foreach (string s in saisies)
    {
        if (int.TryParse(s, out int n))
        {
            total += n;
            compteur++;
        }
    }

    if (compteur == 0) return 0;
    return (double)total / compteur;
}
```

**Deux compteurs distincts.** On ne divise pas par `saisies.Length` (qui compte
aussi les intrus) mais par `compteur` (les seules valeurs retenues). Sur
`["10", "oups", "20"]`, la bonne réponse est `15`, pas `10`.

Et le `if (compteur == 0)` évite la division par zéro — avec des `double`, elle
ne planterait même pas : elle donnerait `NaN`, qui contamine silencieusement
tous les calculs suivants. Bien pire qu'un plantage.

---

## Exercice 8 — L'Inventaire

```csharp
public class Inventaire
{
    private readonly List<Objet> _objets = new List<Objet>();

    public int Capacite { get; private set; }

    public int Nombre => _objets.Count;
    public bool EstPlein => _objets.Count >= Capacite;

    public int ValeurTotale
    {
        get
        {
            int total = 0;
            foreach (Objet o in _objets) total += o.Valeur;
            return total;
        }
    }

    public Inventaire(int capacite)
    {
        Capacite = capacite;
    }

    public bool Ajouter(Objet objet)
    {
        if (objet == null) return false;
        if (EstPlein) return false;
        _objets.Add(objet);
        return true;
    }

    public bool Contient(string nom)
    {
        foreach (Objet o in _objets)
        {
            if (o.Nom == nom) return true;
        }
        return false;
    }

    public bool Retirer(string nom)
    {
        for (int i = 0; i < _objets.Count; i++)
        {
            if (_objets[i].Nom == nom)
            {
                _objets.RemoveAt(i);
                return true;       // ⬅️ ESSENTIEL : on sort tout de suite
            }
        }
        return false;
    }

    public Objet ObjetLePlusCher()
    {
        Objet meilleur = null;
        foreach (Objet o in _objets)
        {
            if (meilleur == null || o.Valeur > meilleur.Valeur)
            {
                meilleur = o;
            }
        }
        return meilleur;
    }

    public List<string> Lister()
    {
        List<string> noms = new List<string>();
        foreach (Objet o in _objets) noms.Add(o.Nom);
        return noms;     // une COPIE, pas la liste interne
    }

    public override string ToString()
    {
        string contenu = Nombre == 0 ? "vide" : string.Join(", ", Lister());
        return $"Sac ({Nombre}/{Capacite}) : {contenu}";
    }
}
```

### Le champ privé `_objets`

```csharp
private readonly List<Objet> _objets = new List<Objet>();
```

- **`private`** : personne à l'extérieur ne peut faire `sac._objets.Add(...)` et
  contourner la limite de capacité. Toutes les modifications passent par
  `Ajouter` et `Retirer`, qui font respecter les règles.
- **`readonly`** : la *référence* ne peut plus changer après la construction. On
  peut toujours ajouter et retirer **dans** la liste, mais pas la remplacer par
  une autre. C'est une garantie en plus, gratuite.
- **`_`** : la convention C# pour un champ privé. Elle te dit d'un coup d'œil
  « ceci est interne ».

### 🧠 Pourquoi `Lister()` retourne une copie

```csharp
public List<Objet> Lister() => _objets;    // ❌ CATASTROPHE
```

Avec cette version, n'importe qui pourrait écrire :

```csharp
sac.Lister().Clear();        // 💥 l'inventaire est vidé dans son dos
sac.Lister().Add(objet);     // 💥 la capacité est contournée
```

Tous tes garde-fous seraient inutiles. **Ne rends jamais ta collection interne
telle quelle** : rends une copie, ou une vue en lecture seule
(`IReadOnlyList<T>`).

C'est une erreur qu'on retrouve dans beaucoup de code professionnel, et elle
cause des bugs très difficiles à diagnostiquer, puisque la modification vient
d'ailleurs.

### Le `return true` dans `Retirer`

Sans lui, la boucle continuerait à parcourir une liste **qu'on vient de
modifier** : les indices ont tous glissé d'un cran, et on finirait par lire une
case qui n'existe plus. `return` dès qu'on a trouvé, c'est à la fois plus
correct et plus rapide.

---

## Exercice 9 — La Sauvegarde

```csharp
public static bool Enregistrer(Partie partie, string chemin)
{
    if (partie == null) return false;

    try
    {
        string json = JsonSerializer.Serialize(partie, OptionsJson);
        File.WriteAllText(chemin, json);
        return true;
    }
    catch (Exception)
    {
        return false;
    }
}

public static Partie Charger(string chemin)
{
    if (!File.Exists(chemin)) return null;

    try
    {
        string json = File.ReadAllText(chemin);
        return JsonSerializer.Deserialize<Partie>(json);
    }
    catch (Exception)
    {
        return null;
    }
}

public static bool Supprimer(string chemin)
{
    if (!File.Exists(chemin)) return false;

    try
    {
        File.Delete(chemin);
        return true;
    }
    catch (Exception)
    {
        return false;
    }
}
```

### Le mélange `if` + `try/catch`

Regarde bien `Charger` : il utilise **les deux**, et chacun à sa place.

| Situation | Outil | Pourquoi |
|-----------|-------|----------|
| Le fichier n'existe pas | `if (!File.Exists(...))` | c'est **prévisible** et fréquent |
| Le JSON est corrompu | `try/catch` | tu ne peux pas le savoir sans essayer |
| Le disque est plein | `try/catch` | imprévisible |
| Le fichier est verrouillé | `try/catch` | imprévisible |

C'est exactement la règle du cours, appliquée.

> ⚠️ Note quand même : entre le `File.Exists` et le `File.ReadAllText`,
> quelqu'un pourrait théoriquement supprimer le fichier. Le `try/catch` sert
> aussi de filet pour ça. Les deux se complètent, ils ne s'opposent pas.

### Pourquoi `Partie` a des `set` publics

Souviens-toi du module 4 : on protégeait tout avec `private set`. Ici, c'est
l'inverse — et c'est volontaire.

`JsonSerializer.Deserialize` construit l'objet puis **remplit ses propriétés une
par une**. Avec un `private set`, il ne pourrait pas écrire dedans, et tu
récupérerais une `Partie` vide, sans le moindre message d'erreur.

D'où le compromis : une classe **DTO** (*Data Transfer Object*) simple et
ouverte, uniquement pour la sauvegarde, séparée des classes du jeu qui restent
protégées. **Chaque classe a un seul métier.**

---

## ✅ Bilan du module

Tu sais maintenant :

- utiliser des collections élastiques (`List`) et associatives (`Dictionary`)
- écrire des programmes qui **ne plantent pas** face à l'imprévu
- distinguer ce qui se **teste** de ce qui se **rattrape**
- lire et écrire des fichiers
- sauvegarder et recharger des objets en JSON

### Ce qui va changer au module suivant

Regarde tout le code que tu viens d'écrire :

```csharp
int total = 0;
foreach (Objet o in _objets) total += o.Valeur;
```
```csharp
List<string> resultat = new List<string>();
foreach (string e in elements) if (!resultat.Contains(e)) resultat.Add(e);
```
```csharp
Objet meilleur = null;
foreach (Objet o in _objets) if (meilleur == null || o.Valeur > meilleur.Valeur) meilleur = o;
```

Filtrer, transformer, additionner, chercher le maximum... **toujours les mêmes
boucles**. Au module 7, ces trois exemples vont s'écrire :

```csharp
_objets.Sum(o => o.Valeur)
elements.Distinct().ToList()
_objets.MaxBy(o => o.Valeur)
```

C'est **LINQ**, et le `o => o.Valeur` s'appelle une **lambda**. 🪄
