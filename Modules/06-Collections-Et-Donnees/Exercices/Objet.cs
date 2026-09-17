namespace Module06;

/// <summary>
/// ⭐ DÉJÀ ÉCRITE — un objet qu'on peut ranger dans l'inventaire.
/// Rien de neuf ici : c'est une classe du module 4.
/// </summary>
public class Objet
{
    public string Nom { get; private set; }
    public int Valeur { get; private set; }

    public Objet(string nom, int valeur)
    {
        Nom = nom;
        Valeur = valeur;
    }

    public override string ToString() => $"{Nom} ({Valeur} po)";
}
