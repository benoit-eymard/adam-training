namespace Module06;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 6 — Listes, dictionnaires et exceptions
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Exo
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐ — ConvertirOuDefaut
    //
    //  Convertit une saisie en nombre. Si ce n'est pas un nombre,
    //  retourne la valeur par défaut — SANS planter.
    //
    //  ConvertirOuDefaut("42", 0)      → 42
    //  ConvertirOuDefaut("bonjour", 0) → 0
    //  ConvertirOuDefaut("", 7)        → 7
    //  ConvertirOuDefaut(null, 7)      → 7
    //  ConvertirOuDefaut("-15", 0)     → -15
    //
    //  ⚠️ N'utilise PAS try/catch ici ! Une saisie invalide est un
    //     cas NORMAL, pas une exception. Utilise int.TryParse.
    //
    //  💡 if (int.TryParse(saisie, out int nombre)) { return nombre; }
    //     return valeurParDefaut;
    // ───────────────────────────────────────────────────────────────
    public static int ConvertirOuDefaut(string saisie, int valeurParDefaut)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐ — SansDoublons
    //
    //  Retourne une nouvelle liste contenant les mêmes éléments,
    //  mais chacun une seule fois, dans l'ordre de première
    //  apparition.
    //
    //  SansDoublons(["a", "b", "a", "c", "b"]) → ["a", "b", "c"]
    //  SansDoublons([])                        → []
    //
    //  ⚠️ La liste d'origine ne doit pas être modifiée.
    //
    //  💡 Une nouvelle List<string>, un foreach, et
    //     resultat.Contains(x) pour savoir si on l'a déjà mis.
    // ───────────────────────────────────────────────────────────────
    public static List<string> SansDoublons(List<string> elements)
    {
        // TODO:
        return new List<string>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐ — CompterMots
    //
    //  Compte combien de fois chaque mot apparaît.
    //
    //  CompterMots(["a", "b", "a"])
    //     → { "a" -> 2, "b" -> 1 }
    //  CompterMots([])
    //     → { }  (un dictionnaire vide)
    //
    //  ⚠️ La casse COMPTE : "Épée" et "épée" sont deux mots différents.
    //
    //  💡 LE motif à connaître par cœur :
    //       if (comptes.ContainsKey(mot))  comptes[mot]++;
    //       else                           comptes[mot] = 1;
    // ───────────────────────────────────────────────────────────────
    public static Dictionary<string, int> CompterMots(string[] mots)
    {
        // TODO:
        return new Dictionary<string, int>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐⭐ — LePlusFrequent
    //
    //  Retourne le mot qui apparaît le plus souvent.
    //  Retourne null si le tableau est vide.
    //  En cas d'égalité, retourne celui rencontré en premier.
    //
    //  LePlusFrequent(["a", "b", "a"]) → "a"
    //  LePlusFrequent(["a", "b"])      → "a"   (égalité : le premier)
    //  LePlusFrequent([])              → null
    //
    //  💡 Réutilise CompterMots ! Puis parcours le dictionnaire avec
    //     le motif du "maximum" (module 3).
    //  ⚠️ L'ordre de parcours d'un Dictionary n'est pas garanti...
    //     Pour gérer l'égalité correctement, parcours plutôt le
    //     TABLEAU d'origine, en consultant le dictionnaire.
    // ───────────────────────────────────────────────────────────────
    public static string LePlusFrequent(string[] mots)
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐⭐ — DiviserSansPlanter
    //
    //  Divise a par b. Si b vaut 0, retourne 0 au lieu de planter.
    //
    //  DiviserSansPlanter(10, 2) → 5
    //  DiviserSansPlanter(10, 0) → 0
    //  DiviserSansPlanter(0, 5)  → 0
    //
    //  ⚠️ Ici, écris-le EXPRÈS avec try/catch pour t'entraîner —
    //     même si en vrai, un simple "if (b == 0)" serait meilleur !
    //     (On te le redit dans les solutions.)
    //
    //  💡 L'exception levée s'appelle DivideByZeroException.
    //       try { return a / b; }
    //       catch (DivideByZeroException) { return 0; }
    // ───────────────────────────────────────────────────────────────
    public static int DiviserSansPlanter(int a, int b)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 6 ⭐⭐ — ElementSur
    //
    //  Retourne l'élément à l'index donné.
    //  Si l'index est invalide (négatif ou trop grand), retourne
    //  la chaîne vide "" — sans planter.
    //
    //  ElementSur(["a","b","c"], 1)  → "b"
    //  ElementSur(["a","b","c"], 9)  → ""
    //  ElementSur(["a","b","c"], -1) → ""
    //  ElementSur([], 0)             → ""
    //  ElementSur(null, 0)           → ""
    //
    //  💡 Ici, PAS de try/catch : un index hors limites se teste !
    //       if (liste == null) return "";
    //       if (index < 0 || index >= liste.Count) return "";
    // ───────────────────────────────────────────────────────────────
    public static string ElementSur(List<string> liste, int index)
    {
        // TODO:
        return "";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 7 ⭐⭐⭐ — MoyenneDesValides
    //
    //  On reçoit des saisies d'utilisateur (du texte). Certaines sont
    //  des nombres, d'autres non. Calcule la moyenne des SEULES
    //  saisies valides.
    //
    //  MoyenneDesValides(["10", "20", "30"])        → 20.0
    //  MoyenneDesValides(["10", "oups", "20"])      → 15.0
    //  MoyenneDesValides(["a", "b"])                → 0.0
    //  MoyenneDesValides([])                        → 0.0
    //
    //  ⚠️ Ne divise JAMAIS par zéro : s'il n'y a aucune saisie
    //     valide, retourne 0 sans faire la division.
    //
    //  💡 Un compteur + un total, et int.TryParse pour filtrer.
    //  💡 N'oublie pas le (double) au moment de diviser !
    // ───────────────────────────────────────────────────────────────
    public static double MoyenneDesValides(string[] saisies)
    {
        // TODO:
        return 0;
    }
}
