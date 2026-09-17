namespace Module05;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 1 ⭐ — Le Guerrier
///
///  Un Guerrier EST UN Combattant : il hérite de Nom, PointsDeVie,
///  SubirDegats, Soigner, EstVivant... sans rien recopier.
///  Il ajoute sa Force et sa Rage.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Guerrier : Combattant
{
    // ───────────────────────────────────────────────────────────────
    //  1.1 — Ses propriétés PROPRES
    //
    //    Force  (int)  — ses dégâts de base
    //    EnRage (bool) — s'il est en rage, sa prochaine attaque
    //                    fait DOUBLE dégâts
    // ───────────────────────────────────────────────────────────────

    public int Force { get; private set; }
    public bool EnRage { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  1.2 — Le constructeur
    //
    //  new Guerrier("Thorin", 120, 15)
    //     → 120 PV max, force 15, pas en rage au départ
    //
    //  ⚠️ Il DOIT appeler le constructeur du parent avec ": base(...)"
    //     C'est lui qui s'occupe du Nom et des PointsDeVie.
    //
    //  💡 La syntaxe :
    //       public Guerrier(string nom, int pvMax, int force)
    //           : base(nom, pvMax)
    //       {
    //           Force = force;
    //       }
    // ───────────────────────────────────────────────────────────────
    public Guerrier(string nom, int pointsDeVieMax, int force)
        : base(nom, pointsDeVieMax)
    {
        // TODO: range force dans Force
    }


    // ───────────────────────────────────────────────────────────────
    //  1.3 — EntrerEnRage
    //
    //  Met le guerrier en rage. Sa PROCHAINE attaque fera double
    //  dégâts, puis la rage retombera.
    // ───────────────────────────────────────────────────────────────
    public void EntrerEnRage()
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  1.4 — Attaquer  (override OBLIGATOIRE : elle est abstract
    //                   dans Combattant)
    //
    //  Règles :
    //    - un guerrier MORT n'attaque pas → retourne 0
    //    - dégâts = Force, ou Force × 2 s'il est EnRage
    //    - la cible subit les dégâts
    //    - ⚠️ la rage RETOMBE après l'attaque (EnRage devient false)
    //    - retourne les dégâts infligés
    //
    //  Exemples avec force 15 :
    //    attaque normale      → 15 dégâts
    //    après EntrerEnRage() → 30 dégâts
    //    l'attaque suivante   → 15 dégâts (la rage est retombée)
    //
    //  💡 N'oublie PAS le mot-clé "override" !
    // ───────────────────────────────────────────────────────────────
    public override int Attaquer(Combattant cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  1.5 — Decrire  (override d'une méthode virtual)
    //
    //  new Guerrier("Thorin", 120, 15).Decrire()
    //     → "Thorin (120/120 PV) — Guerrier (force 15)"
    //
    //  💡 Ne réécris PAS la première partie ! Appelle base.Decrire()
    //     qui te donne déjà "Thorin (120/120 PV)", et ajoute le reste.
    //  💡 Le tiret est un tiret cadratin "—" (copie-colle-le).
    // ───────────────────────────────────────────────────────────────
    public override string Decrire()
    {
        // TODO:
        return "";
    }
}
