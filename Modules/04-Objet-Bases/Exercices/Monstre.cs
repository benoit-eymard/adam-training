namespace Module04;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 2 ⭐⭐ — La classe Monstre
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Monstre
{
    // ───────────────────────────────────────────────────────────────
    //  2.1 — Les propriétés     ✅ DÉJÀ ÉCRITES
    //
    //    Nom           (string) — son nom
    //    PointsDeVie   (int)    — ses PV actuels
    //    Degats        (int)    — les dégâts qu'il inflige
    //
    //  Toutes lisibles de partout, modifiables seulement de l'intérieur.
    // ───────────────────────────────────────────────────────────────

    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }
    public int Degats { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  2.2 — La propriété calculée EstVivant
    //
    //  Un monstre est vivant tant que ses PV sont > 0.
    //
    //  ⚠️ NE STOCKE PAS cette information ! Elle se DÉDUIT des PV.
    //     Si tu la stockais, tu devrais penser à la mettre à jour
    //     partout — et tu oublierais un endroit. Toujours.
    //
    //  💡 public bool EstVivant => PointsDeVie > 0;
    // ───────────────────────────────────────────────────────────────

    // TODO: remplace le `false` de la ligne ci-dessous
    public bool EstVivant => false;


    // ───────────────────────────────────────────────────────────────
    //  2.3 — Le constructeur
    //
    //  new Monstre("Gobelin", 30, 7)
    //     → un gobelin avec 30 PV et 7 de dégâts
    // ───────────────────────────────────────────────────────────────
    public Monstre(string nom, int pointsDeVie, int degats)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  2.4 — SubirDegats
    //
    //  Retire des PV. Les PV ne descendent JAMAIS sous zéro.
    //
    //  💡 Math.Max(0, ...) — comme au module 1 !
    //  💡 C'est la SEULE façon de modifier les PV depuis l'extérieur.
    //     C'est tout l'intérêt de l'encapsulation : la règle
    //     "jamais de PV négatifs" est garantie, ici, une fois pour
    //     toutes.
    // ───────────────────────────────────────────────────────────────
    public void SubirDegats(int degats)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  2.5 — Attaquer
    //
    //  Le monstre attaque un Personnage :
    //    - il lui inflige ses Degats
    //    - il retourne le nombre de dégâts infligés
    //
    //  ⚠️ Un monstre MORT n'attaque pas ! S'il n'est pas vivant,
    //     retourne 0 sans rien faire.
    //
    //  💡 Deux objets qui se parlent :  cible.SubirDegats(...)
    // ───────────────────────────────────────────────────────────────
    public int Attaquer(Personnage cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  2.6 — ToString()
    //
    //  new Monstre("Gobelin", 30, 7).ToString()  →  "Gobelin (30 PV)"
    // ───────────────────────────────────────────────────────────────
    public override string ToString()
    {
        // TODO:
        return "";
    }
}
