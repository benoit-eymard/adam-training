namespace Module02;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 2 — DÉCISIONS & BOUCLES
///
///  Remplace chaque // TODO: par ton code.
///  Vérifie avec :  dotnet test Tests
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Exo
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐ — Majeur ou mineur
    //
    //  EstMajeur(20) → true
    //  EstMajeur(18) → true   (18 ans pile, on est majeur)
    //  EstMajeur(13) → false
    //
    //  💡 Pas besoin de if ici ! Une comparaison renvoie DÉJÀ un bool.
    //     Tu peux directement retourner le résultat de la comparaison.
    // ───────────────────────────────────────────────────────────────
    public static bool EstMajeur(int age)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐ — Le plus grand des trois
    //
    //  LePlusGrand(3, 9, 5)  → 9
    //  LePlusGrand(10, 2, 2) → 10
    //  LePlusGrand(4, 4, 4)  → 4
    //
    //  💡 Deux approches, les deux sont bonnes :
    //     - avec des if/else if
    //     - avec Math.Max emboîté : Math.Max(a, Math.Max(b, c))
    // ───────────────────────────────────────────────────────────────
    public static int LePlusGrand(int a, int b, int c)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐ — La note en lettre
    //
    //  Convertit une note sur 20 en lettre :
    //      16 et plus  → "A"
    //      14 à 15.99  → "B"
    //      12 à 13.99  → "C"
    //      10 à 11.99  → "D"
    //      8  à 9.99   → "E"
    //      moins de 8  → "F"
    //
    //  NoteEnLettre(18)  → "A"
    //  NoteEnLettre(12)  → "C"
    //  NoteEnLettre(3.5) → "F"
    //
    //  ⚠️ L'ORDRE DES TESTS EST CRUCIAL.
    //     Commence par le cas le plus exigeant (>= 16) et descends.
    //     Si tu testes >= 8 en premier, une note de 18 tombera dans "E" !
    // ───────────────────────────────────────────────────────────────
    public static string NoteEnLettre(double note)
    {
        // TODO:
        return "";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐⭐ — Le compte à rebours
    //
    //  CompteARebours(5) → "5, 4, 3, 2, 1, Décollage !"
    //  CompteARebours(3) → "3, 2, 1, Décollage !"
    //  CompteARebours(1) → "1, Décollage !"
    //  CompteARebours(0) → "Décollage !"
    //
    //  💡 Une boucle for qui DESCEND : for (int i = depart; i >= 1; i--)
    //  💡 Construis le texte morceau par morceau avec +=
    //  💡 Le "Décollage !" est toujours là, même quand depart vaut 0.
    // ───────────────────────────────────────────────────────────────
    public static string CompteARebours(int depart)
    {
        // TODO:
        return "";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐⭐ — La table de multiplication
    //
    //  TableDeMultiplication(3) doit donner EXACTEMENT :
    //
    //      3 x 1 = 3
    //      3 x 2 = 6
    //      3 x 3 = 9
    //      ... jusqu'à ...
    //      3 x 10 = 30
    //
    //  ⚠️ PAS de retour à la ligne après la dernière ligne !
    //
    //  💡 Utilise \n pour séparer les lignes.
    //  💡 Le piège du \n en trop : ajoute le \n AVANT chaque ligne
    //     sauf la première, ou APRÈS chaque ligne sauf la dernière.
    // ───────────────────────────────────────────────────────────────
    public static string TableDeMultiplication(int nombre)
    {
        // TODO:
        return "";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 6 ⭐⭐ — FizzBuzz (le grand classique des entretiens !)
    //
    //  Pour un nombre donné, retourne :
    //      "FizzBuzz"  s'il est divisible par 3 ET par 5
    //      "Fizz"      s'il est divisible par 3 seulement
    //      "Buzz"      s'il est divisible par 5 seulement
    //      le nombre lui-même sinon, en texte
    //
    //  FizzBuzz(3)  → "Fizz"
    //  FizzBuzz(5)  → "Buzz"
    //  FizzBuzz(15) → "FizzBuzz"
    //  FizzBuzz(7)  → "7"
    //
    //  ⚠️ ENCORE une histoire d'ordre : teste "divisible par 3 ET par 5"
    //     EN PREMIER. Sinon 15 sera attrapé par le test "divisible par 3"
    //     et tu ne verras jamais "FizzBuzz".
    //
    //  💡 "divisible par 3" s'écrit : nombre % 3 == 0
    //  💡 Pour transformer un nombre en texte : nombre.ToString()
    //     ou plus simplement $"{nombre}"
    // ───────────────────────────────────────────────────────────────
    public static string FizzBuzz(int nombre)
    {
        // TODO:
        return "";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 7 ⭐⭐⭐ — La barre de vie
    //
    //  Dessine une barre de vie de 10 segments.
    //
    //  BarreDeVie(100, 100) → "[##########] 100/100"
    //  BarreDeVie(50, 100)  → "[#####-----] 50/100"
    //  BarreDeVie(45, 100)  → "[####------] 45/100"
    //  BarreDeVie(0, 100)   → "[----------] 0/100"
    //  BarreDeVie(30, 60)   → "[#####-----] 30/60"
    //
    //  💡 Combien de # ? La proportion de vie, ramenée sur 10 segments.
    //     Avec 45 PV sur 100 :  45 * 10 / 100 = 4 segments pleins.
    //     ⚠️ Fais bien la MULTIPLICATION AVANT la division !
    //        45 / 100 * 10 donnerait 0 (division entière...).
    //
    //  💡 Le reste de la barre : 10 - segmentsPleins tirets.
    //  💡 Deux boucles for : une pour les #, une pour les -.
    //     (ou l'astuce : new string('#', n) crée une chaîne de n '#')
    // ───────────────────────────────────────────────────────────────
    public static string BarreDeVie(int pointsDeVie, int pointsDeVieMax)
    {
        // TODO: étape 1 — calcule le nombre de segments pleins
        // TODO: étape 2 — construis la barre avec des boucles
        // TODO: étape 3 — assemble "[barre] pv/pvMax"
        return "";
    }
}
