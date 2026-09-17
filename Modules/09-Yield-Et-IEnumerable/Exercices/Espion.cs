namespace Module09;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  ⭐ DÉJÀ ÉCRIT — l'espion qui prouve la paresse.
///
///  Cette séquence COMPTE combien de ses éléments ont réellement
///  été lus. C'est elle qui permet aux tests de vérifier que tes
///  méthodes sont bien paresseuses.
///
///  Si tu écris MonTake avec une List, l'espion dira que tu as lu
///  1000 éléments alors qu'on ne t'en demandait que 3 — et le test
///  te le reprochera. 😈
///
///  Regarde son code : c'est exactement un générateur avec yield,
///  avec juste un compteur en plus.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Espion
{
    /// <summary>Combien d'éléments ont été RÉELLEMENT lus.</summary>
    public static int NombreDeLectures { get; private set; }

    public static void Reinitialiser()
    {
        NombreDeLectures = 0;
    }

    /// <summary>
    /// Produit 0, 1, 2, ... jusqu'à (combien - 1), en comptant
    /// chaque élément réellement demandé.
    /// </summary>
    public static IEnumerable<int> Nombres(int combien)
    {
        for (int i = 0; i < combien; i++)
        {
            NombreDeLectures++;     // ⬅️ on note le passage
            yield return i;
        }
    }
}
