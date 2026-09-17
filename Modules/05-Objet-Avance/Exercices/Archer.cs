namespace Module05;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 2 ⭐⭐ — L'Archer
///
///  Il tire des flèches. Quand il n'en a plus, il se bat au
///  corps à corps... et ça fait beaucoup moins mal.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Archer : Combattant
{
    // ───────────────────────────────────────────────────────────────
    //  2.1 — Les constantes
    //
    //  "const" = une valeur qui ne changera JAMAIS.
    //  Les mettre en constantes plutôt qu'en chiffres dans le code,
    //  c'est se donner un seul endroit à modifier pour équilibrer
    //  le jeu. (On appelle ça éviter les "nombres magiques".)
    //
    //  DÉJÀ ÉCRIT pour toi — utilise-les dans Attaquer !
    // ───────────────────────────────────────────────────────────────

    public const int DegatsFleche = 15;
    public const int DegatsCorpsACorps = 3;


    // ───────────────────────────────────────────────────────────────
    //  2.2 — Ses propriétés propres
    //
    //    Fleches (int) — combien il lui en reste
    // ───────────────────────────────────────────────────────────────

    public int Fleches { get; private set; }

    //  APlusDeFleches : propriété calculée, vraie s'il lui reste au
    //  moins une flèche.
    // TODO:
    public bool APlusDeFleches => false;


    // ───────────────────────────────────────────────────────────────
    //  2.3 — Le constructeur
    //
    //  new Archer("Sylas", 90, 12)  → 90 PV max, 12 flèches
    // ───────────────────────────────────────────────────────────────
    public Archer(string nom, int pointsDeVieMax, int fleches)
        : base(nom, pointsDeVieMax)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  2.4 — Ramasser
    //
    //  Récupère des flèches. Ramasser(5) ajoute 5 flèches.
    //
    //  ⚠️ On ne ramasse pas un nombre négatif de flèches !
    //     Si nombre <= 0, ne fais rien.
    // ───────────────────────────────────────────────────────────────
    public void Ramasser(int nombre)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  2.5 — Attaquer
    //
    //  Règles :
    //    - un archer MORT n'attaque pas → retourne 0
    //    - s'il lui reste des flèches :
    //         il en consomme UNE et inflige DegatsFleche (15)
    //    - s'il n'en a plus :
    //         il inflige DegatsCorpsACorps (3), sans rien consommer
    //    - retourne les dégâts infligés
    //
    //  💡 Utilise les constantes, pas les chiffres 15 et 3 !
    // ───────────────────────────────────────────────────────────────
    public override int Attaquer(Combattant cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  2.6 — Decrire
    //
    //  new Archer("Sylas", 90, 12).Decrire()
    //     → "Sylas (90/90 PV) — Archer (12 flèches)"
    // ───────────────────────────────────────────────────────────────
    public override string Decrire()
    {
        // TODO:
        return "";
    }
}
