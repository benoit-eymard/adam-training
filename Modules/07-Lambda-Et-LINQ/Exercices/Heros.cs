namespace Module07;

/// <summary>
/// ⭐ DÉJÀ ÉCRITE — le héros sur lequel portent tous les exercices.
/// </summary>
public class Heros
{
    public string Nom { get; set; }
    public string Classe { get; set; }
    public int Niveau { get; set; }
    public int PointsDeVie { get; set; }
    public int Or { get; set; }

    /// <summary>Les objets que porte le héros. Vide par défaut.</summary>
    public List<string> Objets { get; set; } = new List<string>();

    public Heros(string nom, string classe, int niveau, int pointsDeVie, int or)
    {
        Nom = nom;
        Classe = classe;
        Niveau = niveau;
        PointsDeVie = pointsDeVie;
        Or = or;
    }

    public bool EstVivant => PointsDeVie > 0;

    public override string ToString()
        => $"{Nom} ({Classe} niv.{Niveau}, {PointsDeVie} PV, {Or} po)";
}

/// <summary>
/// ⭐ DÉJÀ ÉCRITE — une équipe d'exemple, utilisée par la démo et
/// par les tests. Pratique pour expérimenter.
/// </summary>
public static class Equipes
{
    public static List<Heros> Exemple() => new List<Heros>
    {
        new Heros("Thorin",  "Guerrier", 12, 120, 300)
            { Objets = new List<string> { "Épée", "Bouclier" } },
        new Heros("Elyra",   "Mage",      8,  60, 150)
            { Objets = new List<string> { "Bâton", "Potion" } },
        new Heros("Sylas",   "Archer",   10,   0, 220)    // mort
            { Objets = new List<string> { "Arc", "Potion" } },
        new Heros("Kaelis",  "Mage",     15,  95, 800)
            { Objets = new List<string> { "Bâton", "Grimoire", "Potion" } },
        new Heros("Brunhild","Guerrier",  5,  80,  40)
            { Objets = new List<string> { "Épée" } },
        new Heros("Nym",     "Voleur",    7,   0,  10)    // mort
            { Objets = new List<string>() }
    };
}
