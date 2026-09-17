namespace Module05;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  ⭐ CETTE INTERFACE EST DÉJÀ ÉCRITE.
///
///  Une INTERFACE est un CONTRAT : elle dit ce qu'une classe doit
///  savoir faire, sans dire comment.
///
///  Remarque : aucun corps de méthode, jamais. Juste des signatures
///  suivies d'un point-virgule.
///
///  Par convention, le nom d'une interface commence par un I.
///
///  👉 C'est le Mage qui devra SIGNER ce contrat :
///         public class Mage : Combattant, ISoigneur
///     (d'abord la classe de base, ensuite les interfaces)
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public interface ISoigneur
{
    /// <summary>
    /// Soigne un allié et retourne le nombre de PV effectivement rendus.
    /// Retourne 0 si le soin est impossible.
    /// </summary>
    int SoignerAllie(Combattant cible);
}
