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
    //  1.1 — Les propriétés     ✅ DÉJÀ ÉCRITES, rien à faire ici
    //
    //  Une arme a un Nom (string) et un DegatsBonus (int).
    //
    //  Décortique la syntaxe : tu vas la relire tout le module.
    //
    //      public string Nom { get; private set; }
    //
    //      public       -> visible depuis l'extérieur de la classe
    //      string       -> le type de la donnée
    //      Nom          -> son nom, en PascalCase
    //      get;         -> tout le monde peut LIRE la valeur
    //      private set; -> seul l'intérieur de la classe peut la MODIFIER
    //
    //  ⚠️ Pourquoi sont-elles fournies ? Parce que les tests s'en
    //     servent. Sans elles, le projet de tests ne compilerait pas du
    //     tout, et tu aurais une pluie d'erreurs illisibles au lieu de
    //     résultats exploitables. C'est pareil dans Monstre.cs et
    //     Personnage.cs : les propriétés sont données, le COMPORTEMENT
    //     est à toi.
    // ───────────────────────────────────────────────────────────────

    public string Nom { get; private set; }
    public int DegatsBonus { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  1.2 — Le compteur static     ✅ DÉJÀ ÉCRIT
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
