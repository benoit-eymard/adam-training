namespace Module06;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  ⭐ DÉJÀ ÉCRITE — la classe de SAUVEGARDE (un "DTO").
///
///  Remarque qu'elle est différente des classes du jeu :
///    - toutes ses propriétés ont un "set" PUBLIC
///    - elle a un constructeur sans paramètres
///    - elle ne contient AUCUNE logique, aucun garde-fou
///
///  C'est volontaire : JsonSerializer a besoin de ça pour pouvoir
///  relire l'objet. Une classe de sauvegarde n'a qu'un seul métier,
///  transporter des données — les règles du jeu vivent ailleurs.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Partie
{
    public string NomDuHeros { get; set; }
    public int Niveau { get; set; }
    public int PointsDeVie { get; set; }
    public int Or { get; set; }
    public List<string> Objets { get; set; } = new List<string>();

    public override string ToString()
    {
        return $"{NomDuHeros} — niveau {Niveau}, {PointsDeVie} PV, "
             + $"{Or} po, {Objets.Count} objet(s)";
    }
}
