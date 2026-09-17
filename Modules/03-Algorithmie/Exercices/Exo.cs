namespace Module03;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 3 — ALGORITHMIE
///
///  Remplace chaque // TODO: par ton code.
///  Vérifie avec :  dotnet test Tests
///
///  💡 Tu as le droit (et même intérêt) à créer tes PROPRES méthodes
///     privées pour t'aider. Ajoute-les où tu veux dans cette classe.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Exo
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐ — La somme
    //
    //  Somme([1, 2, 3])   → 6
    //  Somme([10, -5])    → 5
    //  Somme([])          → 0    (un tableau vide, c'est permis !)
    //
    //  💡 Le motif de l'ACCUMULATEUR :
    //     - une variable total qui part de 0
    //     - un foreach qui ajoute chaque élément
    //     - on retourne le total
    // ───────────────────────────────────────────────────────────────
    public static int Somme(int[] nombres)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐ — Le maximum
    //
    //  Maximum([3, 9, 5])     → 9
    //  Maximum([-5, -2, -9])  → -2    ⚠️ attention au piège !
    //  Maximum([42])          → 42
    //
    //  ⚠️ NE PARS PAS DE 0. Avec des nombres tous négatifs, tu
    //     retournerais 0, qui n'est même pas dans le tableau.
    //     Pars du PREMIER élément : nombres[0].
    //
    //  (On te garantit que le tableau n'est jamais vide ici.)
    // ───────────────────────────────────────────────────────────────
    public static int Maximum(int[] nombres)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐ — La moyenne
    //
    //  Moyenne([10, 20, 30]) → 20.0
    //  Moyenne([1, 2])       → 1.5
    //  Moyenne([])           → 0.0    (pas de division par zéro !)
    //
    //  💡 Tu peux réutiliser ta méthode Somme ! C'est même le but :
    //     une méthode qui en appelle une autre, c'est de la
    //     décomposition.
    //  ⚠️ Divise par notes.Length en forçant le double :
    //     (double)Somme(notes) / notes.Length
    //  ⚠️ Si le tableau est vide, retourne 0 SANS faire la division.
    // ───────────────────────────────────────────────────────────────
    public static double Moyenne(int[] notes)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐⭐ — La recherche
    //
    //  Retourne l'INDEX de la première occurrence de la valeur,
    //  ou -1 si elle n'y est pas.
    //
    //  IndexDe([10, 20, 30], 20) → 1
    //  IndexDe([10, 20, 30], 10) → 0
    //  IndexDe([10, 20, 30], 99) → -1
    //  IndexDe([5, 5, 5], 5)     → 0    (la PREMIÈRE occurrence)
    //
    //  💡 Une boucle for (tu as besoin de l'index !), un if, un return
    //     immédiat quand tu trouves, et un return -1 tout à la fin.
    // ───────────────────────────────────────────────────────────────
    public static int IndexDe(int[] tableau, int valeur)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐⭐ — Inverser un tableau
    //
    //  Inverser([1, 2, 3])    → [3, 2, 1]
    //  Inverser([1, 2, 3, 4]) → [4, 3, 2, 1]
    //  Inverser([])           → []
    //
    //  ⚠️ Retourne un NOUVEAU tableau. Le tableau d'origine ne doit
    //     PAS être modifié (un test le vérifie !).
    //
    //  💡 Crée un tableau de même taille : new int[tableau.Length]
    //  💡 La case i du résultat reçoit la case (Length - 1 - i)
    //     de l'original. Fais-le sur papier avec [10, 20, 30] pour
    //     te convaincre.
    // ───────────────────────────────────────────────────────────────
    public static int[] Inverser(int[] tableau)
    {
        // TODO:
        return new int[0];
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 6 ⭐⭐⭐ — Le tri à bulles
    //
    //  Trier([3, 1, 2])     → [1, 2, 3]
    //  Trier([5, 4, 3, 2])  → [2, 3, 4, 5]
    //  Trier([1])           → [1]
    //
    //  ⚠️ Retourne un NOUVEAU tableau trié. L'original reste intact.
    //  ⚠️ INTERDIT d'utiliser Array.Sort ! Tout l'intérêt est de
    //     coder l'algorithme toi-même.
    //
    //  💡 Étape 1 : copie le tableau (une boucle, ou .Clone()).
    //  💡 Étape 2 : deux boucles imbriquées. À chaque passage,
    //     compare les voisins tab[i] et tab[i+1], et échange-les
    //     s'ils sont dans le mauvais ordre.
    //  💡 L'échange NÉCESSITE une variable temporaire :
    //       int temp = tab[i];
    //       tab[i] = tab[i + 1];
    //       tab[i + 1] = temp;
    //  ⚠️ La boucle intérieure va jusqu'à Length - 1 (sinon tab[i+1]
    //     sort du tableau et ça plante !)
    // ───────────────────────────────────────────────────────────────
    public static int[] Trier(int[] tableau)
    {
        // TODO:
        return new int[0];
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 7 ⭐⭐⭐ — Le palindrome
    //
    //  Un palindrome se lit pareil dans les deux sens.
    //  On IGNORE la casse (majuscules/minuscules) et les ESPACES.
    //
    //  EstPalindrome("kayak")        → true
    //  EstPalindrome("Kayak")        → true    (casse ignorée)
    //  EstPalindrome("engage le jeu") → false
    //  EstPalindrome("esope reste ici et se repose") → true
    //  EstPalindrome("bonjour")      → false
    //  EstPalindrome("")             → true    (rien = palindrome)
    //  EstPalindrome("a")            → true
    //
    //  💡 Étape 1 : nettoie le mot.
    //       texte.ToLower().Replace(" ", "")
    //  💡 Étape 2 : compare le 1er avec le dernier, le 2e avec
    //       l'avant-dernier, etc. Dès que deux ne correspondent
    //       pas → return false. Si tu vas au bout → return true.
    //  💡 Deux index qui se rapprochent : gauche part de 0,
    //       droite part de Length - 1. Tant que gauche < droite,
    //       compare, puis gauche++ et droite--.
    // ───────────────────────────────────────────────────────────────
    public static bool EstPalindrome(string texte)
    {
        // TODO:
        return false;
    }

    // ═══════════════════════════════════════════════════════════
    //  PARTIE B — LA RÉCURSIVITÉ
    //
    //  Une méthode qui s'appelle elle-même. Chacune de ces méthodes
    //  a besoin de DEUX choses :
    //     1. un CAS DE BASE  -> quand s'arrêter (sinon 💥 crash)
    //     2. un CAS RÉCURSIF -> se rappeler sur un problème PLUS PETIT
    //
    //  🐞 Pose un point d'arrêt et regarde le panneau Call Stack :
    //     tu verras les appels s'empiler. Voir DEBUG.md.
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 8 ⭐⭐ — La factorielle
    //
    //  n! = n × (n-1) × (n-2) × ... × 1
    //
    //  Factorielle(0) → 1      (par convention mathématique)
    //  Factorielle(1) → 1
    //  Factorielle(5) → 120    (5 × 4 × 3 × 2 × 1)
    //
    //  ⚠️ INTERDIT d'utiliser une boucle — c'est tout l'exercice !
    //
    //  💡 Le squelette de TOUTE méthode récursive :
    //       if (cas de base) { return quelque chose; }
    //       return ... Factorielle(plus petit) ...;
    //
    //  ⚠️ Le type de retour est long (pas int) : 20! dépasse
    //     largement la capacité d'un int.
    // ───────────────────────────────────────────────────────────────
    public static long Factorielle(int n)
    {
        // TODO: le cas de base, PUIS le cas récursif
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 9 ⭐⭐ — La suite de Fibonacci
    //
    //  Chaque nombre est la somme des deux précédents :
    //      0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, ...
    //
    //  Fibonacci(0)  → 0
    //  Fibonacci(1)  → 1
    //  Fibonacci(2)  → 1    (0 + 1)
    //  Fibonacci(10) → 55
    //
    //  💡 Ici il y a DEUX cas de base (0 et 1), et le cas récursif
    //     fait DEUX appels.
    //
    //  ⚠️ Ne teste pas Fibonacci(50) : cette version ferait environ
    //     40 MILLIARDS d'appels. Relis la fin de la section 6 du
    //     COURS pour comprendre pourquoi. 😅
    // ───────────────────────────────────────────────────────────────
    public static long Fibonacci(int n)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 10 ⭐⭐⭐ — La somme des chiffres
    //
    //  Additionne les chiffres d'un nombre, un par un.
    //
    //  SommeDesChiffres(7)    → 7
    //  SommeDesChiffres(123)  → 6     (1 + 2 + 3)
    //  SommeDesChiffres(9999) → 36
    //  SommeDesChiffres(0)    → 0
    //
    //  💡 Le modulo et la division entière du module 1 reviennent !
    //       123 % 10  vaut 3    -> le dernier chiffre
    //       123 / 10  vaut 12   -> tout le reste
    //
    //  💡 Donc : dernier chiffre + SommeDesChiffres(le reste)
    //  💡 Le cas de base : quand il ne reste plus rien (n vaut 0).
    // ───────────────────────────────────────────────────────────────
    public static int SommeDesChiffres(int n)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 11 ⭐⭐⭐ — Inverser un texte, en récursif
    //
    //  InverserTexte("abc")   → "cba"
    //  InverserTexte("a")     → "a"
    //  InverserTexte("")      → ""
    //  InverserTexte("Kaelis")→ "sileaK"
    //
    //  ⚠️ INTERDIT : les boucles, et Array.Reverse.
    //
    //  💡 L'idée : le dernier caractère, PUIS l'inverse de tout le reste.
    //  💡 Deux outils sur les chaînes :
    //       texte[texte.Length - 1]          -> le dernier caractère
    //       texte.Substring(0, texte.Length - 1) -> tout sauf le dernier
    //  💡 Le cas de base : une chaîne vide s'inverse en chaîne vide.
    // ───────────────────────────────────────────────────────────────
    public static string InverserTexte(string texte)
    {
        // TODO:
        return "";
    }
}
