# 💡 Solutions — Module 2

> ⛔ **À lire seulement après avoir vraiment essayé.**
> Si tes tests passent avec un code différent : ta solution est bonne aussi.

---

## Exercice 1 — Majeur ou mineur

```csharp
public static bool EstMajeur(int age)
{
    return age >= 18;
}
```

**Le réflexe à prendre** : une comparaison **est déjà** un booléen. Pas besoin
de `if`.

Beaucoup écrivent ça au début :

```csharp
if (age >= 18)
    return true;
else
    return false;
```

Ça marche, mais c'est du bruit : `return age >= 18;` dit exactement la même
chose en une ligne.

---

## Exercice 2 — Le plus grand des trois

```csharp
public static int LePlusGrand(int a, int b, int c)
{
    return Math.Max(a, Math.Max(b, c));
}
```

On lit de l'intérieur vers l'extérieur : on compare d'abord `b` et `c`, puis on
compare le gagnant avec `a`.

**Version avec des `if`** (tout aussi correcte) :

```csharp
int max = a;
if (b > max) max = b;
if (c > max) max = c;
return max;
```

Cette version-là a un gros avantage : elle **se généralise**. Avec 50 nombres
dans un tableau, c'est exactement ce motif qu'on utilisera (module 3).

---

## Exercice 3 — La note en lettre

```csharp
public static string NoteEnLettre(double note)
{
    if (note >= 16) return "A";
    else if (note >= 14) return "B";
    else if (note >= 12) return "C";
    else if (note >= 10) return "D";
    else if (note >= 8) return "E";
    else return "F";
}
```

**Pourquoi l'ordre est vital** : C# s'arrête à la **première** condition vraie.
Pour `note = 18`, `18 >= 16` est vrai → on retourne `"A"` et on ne regarde
jamais la suite.

Si tu avais commencé par `if (note >= 8) return "E";`, **toutes** les notes au
dessus de 8 auraient donné `"E"`. C'est un bug classique et sournois, parce que
le programme ne plante pas : il donne juste des réponses fausses.

> 💡 Remarque : comme chaque branche fait un `return`, les `else` sont
> techniquement facultatifs ici. Un `return` quitte la méthode immédiatement.
> Les garder rend l'intention plus visible.

---

## Exercice 4 — Le compte à rebours

```csharp
public static string CompteARebours(int depart)
{
    string resultat = "";

    for (int i = depart; i >= 1; i--)
    {
        resultat += $"{i}, ";
    }

    resultat += "Décollage !";
    return resultat;
}
```

**L'astuce** : au lieu de se battre pour savoir quand ne PAS mettre la virgule,
on met `", "` après **chaque** nombre — et comme `"Décollage !"` arrive juste
après, ça tombe pile.

Pour `depart = 0`, la boucle ne fait **aucun tour** (la condition `0 >= 1` est
fausse d'entrée), et on obtient directement `"Décollage !"`. Le cas limite se
gère tout seul. 🎁

> 🧠 **À retenir** : quand un cas particulier se gère tout seul grâce à la
> structure de ton code, c'est souvent le signe que tu as choisi la bonne
> structure.

---

## Exercice 5 — La table de multiplication

```csharp
public static string TableDeMultiplication(int nombre)
{
    string resultat = "";

    for (int i = 1; i <= 10; i++)
    {
        if (i > 1)
        {
            resultat += "\n";     // saut de ligne AVANT, sauf au 1er tour
        }
        resultat += $"{nombre} x {i} = {nombre * i}";
    }

    return resultat;
}
```

**Le problème du séparateur** : avec 10 lignes, il faut 9 séparateurs, pas 10.
Deux façons de s'en sortir :

1. **Ajouter le `\n` avant, sauf au premier tour** (ci-dessus)
2. **Ajouter le `\n` après, puis retirer le dernier** :
   ```csharp
   resultat = resultat.TrimEnd('\n');
   ```

La première est plus propre : on ne crée jamais le problème.

> 💡 Il existe encore plus élégant avec `string.Join` — tu le découvriras au
> module 6 avec les collections.

---

## Exercice 6 — FizzBuzz

```csharp
public static string FizzBuzz(int nombre)
{
    if (nombre % 3 == 0 && nombre % 5 == 0)
    {
        return "FizzBuzz";
    }
    else if (nombre % 3 == 0)
    {
        return "Fizz";
    }
    else if (nombre % 5 == 0)
    {
        return "Buzz";
    }
    else
    {
        return $"{nombre}";
    }
}
```

**Le cas qui piège** : `15`. Il est divisible par 3 **et** par 5. Si tu testes
`% 3` en premier, tu retournes `"Fizz"` et tu ne verras jamais `"FizzBuzz"`.

> 🧠 **La règle générale, encore** : dans une cascade de conditions, le cas le
> plus **spécifique** passe en premier. « Divisible par 3 ET par 5 » est plus
> exigeant que « divisible par 3 » : il passe donc devant.

**Version alternative** (astucieuse, mais moins lisible) :

```csharp
string resultat = "";
if (nombre % 3 == 0) resultat += "Fizz";
if (nombre % 5 == 0) resultat += "Buzz";
return resultat == "" ? $"{nombre}" : resultat;
```

Ici, on **accumule** : 15 reçoit `"Fizz"` puis `"Buzz"` et devient naturellement
`"FizzBuzz"`. Plus besoin de cas spécial. Le `? :` s'appelle l'**opérateur
ternaire** : `condition ? siVrai : siFaux`.

---

## Exercice 7 — La barre de vie

```csharp
public static string BarreDeVie(int pointsDeVie, int pointsDeVieMax)
{
    int segmentsPleins = pointsDeVie * 10 / pointsDeVieMax;

    string barre = "";
    for (int i = 0; i < segmentsPleins; i++)
    {
        barre += "#";
    }
    for (int i = 0; i < 10 - segmentsPleins; i++)
    {
        barre += "-";
    }

    return $"[{barre}] {pointsDeVie}/{pointsDeVieMax}";
}
```

**LE piège, encore la division entière.** Regarde bien l'ordre :

| Écriture | Pour 45/100 | Résultat |
|----------|-------------|----------|
| `pv * 10 / pvMax` | `45 * 10 = 450`, puis `450 / 100` | **4** ✅ |
| `pv / pvMax * 10` | `45 / 100 = 0`, puis `0 * 10` | **0** ❌ |

Comme `*` et `/` ont la même priorité, C# les évalue **de gauche à droite**.
En multipliant d'abord, on garde la précision.

> 🧠 **Règle d'or** : avec des entiers, **multiplie avant de diviser**.

**Version courte** avec le constructeur de `string` :

```csharp
int pleins = pointsDeVie * 10 / pointsDeVieMax;
string barre = new string('#', pleins) + new string('-', 10 - pleins);
return $"[{barre}] {pointsDeVie}/{pointsDeVieMax}";
```

`new string('#', 4)` crée la chaîne `"####"`. Pratique, mais écris d'abord la
version avec les boucles : c'est elle qui t'apprend à réfléchir.

---

## ✅ Bilan du module

Tu sais maintenant faire des programmes qui **décident** et qui **répètent** —
ce qui est, littéralement, tout ce qu'un ordinateur sait faire.

Ce qui te manque encore : **organiser** ce code. Tes méthodes vont devenir
longues, tu vas copier-coller des morceaux, et ça va devenir le bazar.

Le module 3 t'apprend à **découper** : méthodes, tableaux, et l'art de casser un
gros problème en petits problèmes solubles. 👉
