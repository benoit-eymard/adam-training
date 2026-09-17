namespace Module04;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 1 ⭐ — La classe Arme
///
///  La plus simple des trois. Inspire-toi de Potion.cs !
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Arme
{
    // ───────────────────────────────────────────────────────────────
    //  1.1 — Les propriétés
    //
    //  Une arme a :
    //    - un Nom (string), lisible de partout, modifiable seulement
    //      depuis l'intérieur de la classe
    //    - un DegatsBonus (int), pareil
    //
    //  💡 Syntaxe : public string Nom { get; private set; }
    // ───────────────────────────────────────────────────────────────

    // TODO: déclare la propriété Nom
    public string Nom { get; private set; }

    // TODO: déclare la propriété DegatsBonus
    public int DegatsBonus { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  1.2 — Le compteur static
    //
    //  NombreDArmesCreees compte TOUTES les armes créées depuis le
    //  début du programme. Il appartient à la CLASSE, pas à un objet.
    //
    //  💡 public static int NombreDArmesCreees { get; private set; }
    // ───────────────────────────────────────────────────────────────

    public static int NombreDArmesCreees { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  1.3 — Le constructeur
    //
    //  new Arme("Épée longue", 8) doit créer une arme nommée
    //  "Épée longue" avec 8 de bonus, et incrémenter le compteur.
    //
    //  ⚠️ Le constructeur porte le MÊME NOM que la classe et n'a
    //     AUCUN type de retour (pas même void).
    // ───────────────────────────────────────────────────────────────
    public Arme(string nom, int degatsBonus)
    {
        // TODO: range nom dans Nom
        // TODO: range degatsBonus dans DegatsBonus
        // TODO: incrémente NombreDArmesCreees
    }


    // ───────────────────────────────────────────────────────────────
    //  1.4 — ToString()
    //
    //  new Arme("Épée longue", 8).ToString()  →  "Épée longue (+8)"
    //
    //  💡 override = "je remplace le comportement par défaut"
    // ───────────────────────────────────────────────────────────────
    public override string ToString()
    {
        // TODO:
        return "";
    }
}
