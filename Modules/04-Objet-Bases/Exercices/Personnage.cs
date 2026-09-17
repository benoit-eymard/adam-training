namespace Module04;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 3 ⭐⭐⭐ — La classe Personnage
///
///  La plus complète. Prends ton temps, méthode par méthode.
///  Lance les tests souvent : ils te diront exactement où tu en es.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Personnage
{
    // ───────────────────────────────────────────────────────────────
    //  3.1 — Les propriétés
    // ───────────────────────────────────────────────────────────────

    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }
    public int PointsDeVieMax { get; private set; }
    public int Force { get; private set; }

    /// <summary>L'arme équipée, ou null si le personnage est à mains nues.</summary>
    public Arme ArmeEquipee { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  3.2 — Les propriétés calculées
    // ───────────────────────────────────────────────────────────────

    //  EstVivant : vrai tant que PointsDeVie > 0
    // TODO:
    public bool EstVivant => false;

    //  PointsDeVieManquants : combien de PV il faudrait pour être
    //  à nouveau au maximum.
    //    100/100 PV → 0
    //     70/100 PV → 30
    // TODO:
    public int PointsDeVieManquants => 0;

    //  DegatsTotaux : la Force, PLUS le bonus de l'arme équipée.
    //    Force 12, pas d'arme        → 12
    //    Force 12, épée à +8         → 20
    //
    //  ⚠️ ArmeEquipee peut valoir null ! Écrire ArmeEquipee.DegatsBonus
    //     sans vérifier ferait planter le programme avec une
    //     NullReferenceException. TESTE null d'abord.
    //
    //  💡 Une propriété calculée peut avoir un vrai corps :
    //       public int DegatsTotaux
    //       {
    //           get
    //           {
    //               if (...) { ... }
    //               return ...;
    //           }
    //       }
    // TODO:
    public int DegatsTotaux => 0;


    // ───────────────────────────────────────────────────────────────
    //  3.3 — Le constructeur
    //
    //  new Personnage("Kaelis", 100, 12)
    //     → Kaelis démarre avec 100 PV sur 100 max, 12 de force,
    //       et AUCUNE arme équipée.
    //
    //  ⚠️ PointsDeVie ET PointsDeVieMax reçoivent tous les deux la
    //     valeur du paramètre : un personnage neuf est en pleine forme.
    // ───────────────────────────────────────────────────────────────
    public Personnage(string nom, int pointsDeVieMax, int force)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  3.4 — SubirDegats
    //
    //  Retire des PV, sans jamais descendre sous zéro.
    // ───────────────────────────────────────────────────────────────
    public void SubirDegats(int degats)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  3.5 — Soigner
    //
    //  Ajoute des PV, avec DEUX règles :
    //    1. on ne dépasse JAMAIS PointsDeVieMax
    //    2. on ne soigne PAS un personnage mort (pas de résurrection !)
    //
    //  Soigner(20) sur 70/100  → 90/100
    //  Soigner(50) sur 70/100  → 100/100  (et pas 120 !)
    //  Soigner(50) sur  0/100  → 0/100    (il est mort)
    //
    //  💡 Math.Min(PointsDeVieMax, ...) pour le plafond
    //  💡 Une sortie anticipée pour le mort :
    //       if (!EstVivant) { return; }
    // ───────────────────────────────────────────────────────────────
    public void Soigner(int soins)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  3.6 — Equiper
    //
    //  Équipe une arme. Si le personnage en portait déjà une, elle
    //  est simplement remplacée.
    // ───────────────────────────────────────────────────────────────
    public void Equiper(Arme arme)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  3.7 — Attaquer
    //
    //  Le personnage attaque un Monstre :
    //    - il lui inflige ses DegatsTotaux (force + arme)
    //    - il retourne le nombre de dégâts infligés
    //
    //  ⚠️ Un personnage MORT n'attaque pas : retourne 0 sans rien faire.
    // ───────────────────────────────────────────────────────────────
    public int Attaquer(Monstre cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  3.8 — BoirePotion
    //
    //  Boit une potion et se soigne du montant rendu.
    //  Retourne le nombre de PV effectivement rendus par la potion.
    //
    //  ⚠️ Si la potion est vide, elle rend 0 : rien ne se passe.
    //
    //  💡 La classe Potion est déjà écrite : regarde sa méthode Boire().
    //  💡 Tu peux appeler ta propre méthode Soigner() !
    // ───────────────────────────────────────────────────────────────
    public int BoirePotion(Potion potion)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  3.9 — ToString()
    //
    //  Sans arme : "Kaelis (100/100 PV) — mains nues"
    //  Avec arme : "Kaelis (100/100 PV) — Épée longue (+8)"
    //
    //  💡 Le tiret est un tiret CADRATIN "—" entouré d'espaces.
    //     Copie-colle-le depuis ce commentaire pour être sûr !
    //  💡 Pour la partie arme, ArmeEquipee.ToString() fait déjà le
    //     travail... et $"{ArmeEquipee}" l'appelle automatiquement.
    // ───────────────────────────────────────────────────────────────
    public override string ToString()
    {
        // TODO:
        return "";
    }
}
