# 💡 Solutions — Module 1

> ⛔ **Ne lis cette page qu'après avoir vraiment essayé.**
> Un exercice raté puis compris t'apprend dix fois plus qu'une solution recopiée.
>
> Et si ta solution est différente de la mienne mais que les tests passent :
> **la tienne est bonne aussi**. Il y a toujours plusieurs chemins.

---

## Exercice 1 — Se présenter

```csharp
public static string SePresenter(string prenom, int age)
{
    return $"Je m'appelle {prenom} et j'ai {age} ans.";
}
```

**À retenir** : le `$` devant les guillemets active l'interpolation. Sans lui,
`{prenom}` s'afficherait littéralement, accolades comprises.

Sans interpolation, il aurait fallu écrire :
`return "Je m'appelle " + prenom + " et j'ai " + age + " ans.";`
— plus long, et on oublie facilement les espaces autour des `+`.

---

## Exercice 2 — La calculatrice

```csharp
public static int Additionner(int a, int b)
{
    return a + b;
}

public static double Moyenne(int note1, int note2, int note3)
{
    return (note1 + note2 + note3) / 3.0;
}
```

**Deux pièges dans la moyenne :**

1. **Les parenthèses.** Sans elles, `note1 + note2 + note3 / 3.0` ne divise que
   la troisième note — la priorité des opérations s'applique comme en maths.

2. **Le `3.0`.** Si tu écris `/ 3`, les deux côtés sont des entiers et C# fait une
   division entière : `Moyenne(1, 2, 2)` renverrait `1` au lieu de `1.6666`.
   Le `.0` force C# à travailler en décimal.

> 🔍 **Règle générale** : dès qu'un `double` apparaît dans un calcul, tout le
> calcul se fait en `double`.

---

## Exercice 3 — Points de vie

```csharp
public static int PointsDeVieRestants(int pointsDeVie, int degats)
{
    return Math.Max(0, pointsDeVie - degats);
}
```

`Math.Max(a, b)` renvoie le plus grand des deux. Donc si `pointsDeVie - degats`
donne `-50`, on garde `0`.

**Sans `Math.Max`** (tu verras le `if` au module 2, c'est tout aussi valable) :

```csharp
int restants = pointsDeVie - degats;
if (restants < 0)
{
    restants = 0;
}
return restants;
```

Les deux versions font exactement la même chose. La première est plus courte,
la seconde plus explicite. Aucune n'est « meilleure » dans l'absolu.

> 💡 `Math` contient plein d'outils utiles : `Math.Min`, `Math.Abs` (valeur
> absolue), `Math.Round`, `Math.Sqrt`... Tape `Math.` dans VS Code et regarde
> ce qu'il te propose.

---

## Exercice 4 — La fiche de personnage

```csharp
public static string FicheDePersonnage(string nom, string classe, int niveau)
{
    return $"Nom    : {nom}\nClasse : {classe}\nNiveau : {niveau}";
}
```

Le `\n` insère un retour à la ligne **à l'intérieur** de la chaîne. Une seule
chaîne, trois lignes à l'affichage.

**Si ton test échoue**, c'est presque sûrement une histoire d'espaces. Compte :
`Nom` + 4 espaces, `Classe` + 1 espace, `Niveau` + 1 espace — pour que les `:`
soient alignés verticalement.

> Autre écriture possible, plus lisible sur les fiches longues — la **chaîne
> textuelle** (`@` devant les guillemets), qui garde les retours à la ligne réels :
>
> ```csharp
> return $@"Nom    : {nom}
> Classe : {classe}
> Niveau : {niveau}";
> ```
>
> ⚠️ Attention : sur Windows, un vrai retour à la ligne dans le fichier peut
> valoir `\r\n` et non `\n` — le test deviendrait rouge. C'est pour ça qu'on
> préfère `\n` explicite ici.

---

## Exercice 5 — Le convertisseur de pièces

```csharp
public static string ConvertirEnOr(int cuivre)
{
    int argentTotal = cuivre / 100;      // 12345 / 100 = 123
    int cuivreRestant = cuivre % 100;    // 12345 % 100 = 45

    int or = argentTotal / 100;          // 123 / 100 = 1
    int argentRestant = argentTotal % 100; // 123 % 100 = 23

    return $"{or} or, {argentRestant} argent, {cuivreRestant} cuivre";
}
```

**L'idée clé** : on applique **deux fois le même raisonnement**.

| Étape | Calcul | Résultat | Signification |
|-------|--------|----------|---------------|
| 1 | `12345 / 100` | `123` | on peut faire 123 pièces d'argent |
| 2 | `12345 % 100` | `45` | il reste 45 cuivres orphelins |
| 3 | `123 / 100` | `1` | avec ces 123 argents, on fait 1 or |
| 4 | `123 % 100` | `23` | il reste 23 argents |

`/` te dit **combien de fois ça rentre**.
`%` te dit **ce qui dépasse**.

Ces deux opérateurs vont ensemble comme le marteau et le clou. Tu les utiliseras
partout : convertir des secondes en heures/minutes, découper un nombre en
chiffres, savoir si un nombre est pair...

**Version compacte** (même chose, écrite en une expression) :

```csharp
return $"{cuivre / 10000} or, {cuivre / 100 % 100} argent, {cuivre % 100} cuivre";
```

C'est plus court, mais franchement moins clair. **Dans le doute, choisis la
version lisible.** Le code court n'est pas un but en soi.

---

## ✅ Bilan du module

Tu sais maintenant :

- créer des variables et choisir leur type
- éviter le piège de la division entière
- utiliser `%` pour récupérer un reste
- construire du texte avec `$"{...}"`
- lire ce que tape l'utilisateur

**Un seul concept manque pour tout débloquer : la capacité à décider et à
répéter.** C'est le module 2. 👉
