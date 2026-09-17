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
}
