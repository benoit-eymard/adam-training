namespace Module05;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 3 ⭐⭐ — Le Mage
///
///  Le Mage hérite de Combattant ET signe le contrat ISoigneur.
///  Remarque la déclaration ci-dessous :
///
///      public class Mage : Combattant, ISoigneur
///                          ─────┬────  ────┬───
///                       la classe de base   l'interface
///                       (TOUJOURS en premier)
///
///  On n'hérite que d'UNE classe, mais on peut signer autant
///  d'interfaces qu'on veut.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Mage : Combattant, ISoigneur
{
    // ───────────────────────────────────────────────────────────────
    //  3.1 — Les constantes (déjà écrites)
    // ───────────────────────────────────────────────────────────────

    public const int CoutDuSort = 10;
    public const int DegatsDuSort = 25;
    public const int DegatsBaton = 5;

    public const int CoutDuSoin = 15;
    public const int PointsDeSoin = 30;


    // ───────────────────────────────────────────────────────────────
    //  3.2 — Ses propriétés propres     ✅ DÉJÀ ÉCRITES
    // ───────────────────────────────────────────────────────────────

    public int Mana { get; private set; }
    public int ManaMax { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  3.3 — Le constructeur
    //
    //  new Mage("Elyra", 80, 40)
    //     → 80 PV max, 40 mana max, et il DÉMARRE avec tout son mana
    //       (comme les PV : plein au départ)
    // ───────────────────────────────────────────────────────────────
    public Mage(string nom, int pointsDeVieMax, int manaMax)
        : base(nom, pointsDeVieMax)
    {
        // TODO: ManaMax et Mana
    }


    // ───────────────────────────────────────────────────────────────
    //  3.4 — Attaquer
    //
    //  Règles :
    //    - un mage MORT n'attaque pas → retourne 0
    //    - s'il a au moins CoutDuSort (10) de mana :
    //         il dépense 10 mana et inflige DegatsDuSort (25)
    //    - sinon (à court de mana) :
    //         il frappe avec son bâton : DegatsBaton (5), sans coût
    //    - retourne les dégâts infligés
    // ───────────────────────────────────────────────────────────────
    public override int Attaquer(Combattant cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  3.5 — SoignerAllie   (c'est le CONTRAT ISoigneur)
    //
    //  Soigne un allié de PointsDeSoin (30) PV, pour CoutDuSoin (15)
    //  de mana. Retourne le nombre de PV EFFECTIVEMENT rendus.
    //
    //  Retourne 0 SANS RIEN DÉPENSER dans ces quatre cas :
    //    - le mage est mort
    //    - il n'a pas assez de mana (< 15)
    //    - la cible est morte
    //    - la cible est déjà à PV maximum (rien à soigner)
    //
    //  ⚠️ "PV effectivement rendus" : si l'allié n'a que 10 PV
    //     manquants, le soin de 30 n'en rend que 10 → retourne 10.
    //
    //  💡 cible.PointsDeVieManquants existe déjà dans Combattant !
    //  💡 Math.Min(PointsDeSoin, cible.PointsDeVieManquants)
    //  💡 La méthode Soigner() de Combattant plafonne déjà toute
    //     seule : tu peux lui passer 30 sans crainte.
    // ───────────────────────────────────────────────────────────────
    public int SoignerAllie(Combattant cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  3.6 — Decrire
    //
    //  new Mage("Elyra", 80, 40).Decrire()
    //     → "Elyra (80/80 PV) — Mage (40/40 mana)"
    // ───────────────────────────────────────────────────────────────
    public override string Decrire()
    {
        // TODO:
        return "";
    }
}
