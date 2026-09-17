namespace Module04;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  ⭐ CETTE CLASSE EST TON MODÈLE — elle est DÉJÀ ÉCRITE.
///
///  Lis-la en entier avant de commencer tes exercices.
///  Tout ce dont tu as besoin est illustré ici :
///    - des propriétés en lecture seule de l'extérieur
///    - une propriété calculée
///    - un constructeur
///    - des méthodes qui protègent l'état de l'objet
///    - un ToString() personnalisé
///    - un compteur static
///
///  Ne la modifie pas (sauf pour expérimenter — c'est permis !).
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Potion
{
    // ─── Les DONNÉES de l'objet ────────────────────────────────────
    // "get" public  → tout le monde peut LIRE
    // "private set" → seul l'intérieur de la classe peut MODIFIER
    public string Nom { get; private set; }
    public int PointsDeSoin { get; private set; }
    public int UtilisationsRestantes { get; private set; }

    // ─── Une donnée partagée par TOUTES les potions ────────────────
    // static = appartient à la CLASSE, pas à un objet particulier
    public static int NombreDePotionsCreees { get; private set; }

    // ─── Une propriété CALCULÉE ────────────────────────────────────
    // Rien n'est stocké : la valeur est déduite à chaque lecture.
    // Impossible qu'elle soit désynchronisée. 👌
    public bool EstVide => UtilisationsRestantes <= 0;

    // ─── Le CONSTRUCTEUR ───────────────────────────────────────────
    // Même nom que la classe, aucun type de retour.
    // Son rôle : rendre l'objet immédiatement utilisable.
    public Potion(string nom, int pointsDeSoin, int utilisations)
    {
        Nom = nom;
        PointsDeSoin = pointsDeSoin;
        UtilisationsRestantes = utilisations;

        NombreDePotionsCreees++;   // le compteur partagé augmente
    }

    // ─── Un deuxième constructeur (surcharge) ──────────────────────
    // ": this(...)" appelle l'autre constructeur : pas de duplication.
    public Potion(string nom) : this(nom, 25, 1)
    {
    }

    // ─── Une MÉTHODE qui modifie l'état, avec un garde-fou ─────────
    /// <summary>
    /// Consomme une utilisation et retourne les points de soin.
    /// Retourne 0 si la potion est vide.
    /// </summary>
    public int Boire()
    {
        if (EstVide)
        {
            return 0;      // sortie anticipée : la potion est vide
        }

        UtilisationsRestantes--;
        return PointsDeSoin;
    }

    // ─── Comment l'objet se présente quand on l'affiche ────────────
    public override string ToString()
    {
        return $"{Nom} (+{PointsDeSoin} PV, {UtilisationsRestantes} utilisation(s))";
    }
}
