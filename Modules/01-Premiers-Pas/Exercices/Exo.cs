namespace Module01;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 1 — À TOI DE JOUER
///
///  Remplace chaque // TODO: par ton code.
///  Vérifie avec :  dotnet test Tests
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Exo
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐ — Se présenter
    //
    //  Retourne une phrase de présentation.
    //
    //  SePresenter("Adam", 13)  doit donner exactement :
    //      Je m'appelle Adam et j'ai 13 ans.
    //
    //  💡 Utilise l'interpolation : $"..."
    //  💡 Pour écrire une apostrophe dans du texte, aucun problème :
    //     "Je m'appelle" fonctionne tel quel.
    // ───────────────────────────────────────────────────────────────
    public static string SePresenter(string prenom, int age)
    {
       
        return $"Je m'appelle {prenom} et j'ai {age} ans.";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐ — La calculatrice
    // ───────────────────────────────────────────────────────────────

    /// <summary>Additionne deux nombres. Additionner(7, 3) → 10</summary>
    public static int Additionner(int a, int b)
    {
        // TODO: retourne la somme de a et b
        return a + b;
    }

    /// <summary>
    /// Calcule la moyenne de trois notes.
    /// Moyenne(12, 15, 18) → 15.0
    /// Moyenne(10, 11, 12) → 11.0
    /// Moyenne(1, 2, 2)    → 1.6666...
    ///
    /// ⚠️ PIÈGE : si tu divises par 3 (un int), tu perds la virgule !
    ///    Divise par 3.0 pour obtenir un vrai résultat décimal.
    /// </summary>
    public static double Moyenne(int note1, int note2, int note3)
    {
        // TODO: additionne les trois notes, puis divise par 3.0
        //       (n'oublie pas les parenthèses autour de l'addition !)
        return (note1 + note2+note3)/3.0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐ — Points de vie
    //
    //  Le héros a des PV et subit des dégâts.
    //  Retourne ses PV restants.
    //
    //  PointsDeVieRestants(100, 30)  → 70
    //  PointsDeVieRestants(100, 150) → 0    (et PAS -50 !)
    //
    //  💡 Les PV ne descendent jamais en dessous de zéro.
    //     Tu peux utiliser Math.Max(0, ...) qui retourne le plus
    //     grand des deux nombres qu'on lui donne.
    //     Exemple : Math.Max(0, -50) vaut 0.
    // ───────────────────────────────────────────────────────────────
    public static int PointsDeVieRestants(int pointsDeVie, int degats)
    {
        // TODO:
        return  Math.Max(0, pointsDeVie - degats);
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐⭐ — La fiche de personnage
    //
    //  Retourne une fiche sur 3 lignes.
    //
    //  FicheDePersonnage("Kaelis", "Mage", 5) doit donner EXACTEMENT :
    //
    //      Nom    : Kaelis
    //      Classe : Mage
    //      Niveau : 5
    //
    //  (sans espace en trop à la fin, et sans retour à la ligne final)
    //
    //  💡 \n crée un retour à la ligne à l'intérieur d'une chaîne.
    //  💡 Compte bien les espaces avant les ':' pour aligner !
    // ───────────────────────────────────────────────────────────────
    public static string FicheDePersonnage(string nom, string classe, int niveau)
    {
        // TODO:
        return $"Nom    : {nom}\nClasse : {classe}\nNiveau : {niveau}";
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐⭐⭐ — Le convertisseur de pièces
    //
    //  Dans notre RPG :
    //      100 pièces de cuivre = 1 pièce d'argent
    //      100 pièces d'argent  = 1 pièce d'or
    //
    //  À partir d'un montant en cuivre, retourne le détail.
    //
    //  ConvertirEnOr(12345) → "1 or, 23 argent, 45 cuivre"
    //  ConvertirEnOr(250)   → "0 or, 2 argent, 50 cuivre"
    //  ConvertirEnOr(7)     → "0 or, 0 argent, 7 cuivre"
    //
    //  💡 C'est l'exercice le plus dur du module. Prends une feuille.
    //  💡 La division entière / et le modulo % sont tes deux outils.
    //     12345 / 100  vaut 123  → le nombre total d'argent
    //     12345 % 100  vaut 45   → le cuivre qui reste
    //     ... et maintenant, refais la même chose sur ces 123 argent.
    // ───────────────────────────────────────────────────────────────
    public static string ConvertirEnOr(int cuivre)
    {
        int argent = cuivre / 100; // TODO: étape 1 — combien de pièces d'argent au total ?
        int cuivreRestant = cuivre % 100;// TODO: étape 2 — combien de cuivre reste-t-il ?
        int or = argent / 100;// TODO: étape 3 — dans ces pièces d'argent, combien d'or ?
        int argentRestant = argent % 100;// TODO: étape 4 — combien d'argent reste-t-il ?
        // TODO: étape 5 — assemble la phrase avec $"..."
        return $"{or} or, {argentRestant} argent, {cuivreRestant} cuivre";
    }
}
