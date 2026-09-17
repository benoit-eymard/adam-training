namespace Module05;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  ⭐ CETTE CLASSE EST DÉJÀ ÉCRITE — c'est ton modèle.
///
///  C'est la classe de BASE dont hériteront Guerrier, Mage et Archer.
///  Lis-la en entier avant de commencer.
///
///  Elle est "abstract" : on ne peut PAS écrire new Combattant(...).
///  Un "combattant générique" n'existe pas — seuls existent des
///  guerriers, des mages, des archers.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public abstract class Combattant
{
    // ─── Ce que TOUS les combattants ont en commun ────────────────
    public string Nom { get; private set; }
    public int PointsDeVie { get; private set; }
    public int PointsDeVieMax { get; private set; }

    public bool EstVivant => PointsDeVie > 0;
    public int PointsDeVieManquants => PointsDeVieMax - PointsDeVie;

    // ─── Le constructeur de la classe de base ─────────────────────
    // Il est "protected" : seules les classes filles peuvent
    // l'appeler, via ": base(...)". Personne d'autre n'en a besoin,
    // puisqu'on ne peut pas créer un Combattant directement.
    protected Combattant(string nom, int pointsDeVieMax)
    {
        Nom = nom;
        PointsDeVieMax = pointsDeVieMax;
        PointsDeVie = pointsDeVieMax;
    }

    // ─── Le code COMMUN, écrit UNE SEULE FOIS ─────────────────────
    // Guerrier, Mage et Archer héritent de ces deux méthodes.
    // Zéro copier-coller. Un bug corrigé ici est corrigé partout.

    public void SubirDegats(int degats)
    {
        PointsDeVie = Math.Max(0, PointsDeVie - degats);
    }

    public void Soigner(int soins)
    {
        if (!EstVivant)
        {
            return;      // pas de résurrection
        }
        PointsDeVie = Math.Min(PointsDeVieMax, PointsDeVie + soins);
    }

    // ─── La méthode ABSTRAITE ─────────────────────────────────────
    // Pas de corps : juste un point-virgule.
    //
    // Sens : "tout combattant sait attaquer, mais MOI je ne peux pas
    // dire comment — ça dépend du type. Chaque classe fille DOIT
    // fournir sa version."
    //
    // Si une classe fille oublie de l'implémenter → erreur de
    // compilation. Le compilateur te protège.
    public abstract int Attaquer(Combattant cible);

    // ─── Une méthode VIRTUELLE ────────────────────────────────────
    // Elle a un comportement par défaut, que les classes filles
    // PEUVENT (mais ne DOIVENT pas) redéfinir avec "override".
    public virtual string Decrire()
    {
        return $"{Nom} ({PointsDeVie}/{PointsDeVieMax} PV)";
    }

    // ToString() appelle Decrire(). Comme Decrire() est virtual,
    // afficher un Combattant affichera automatiquement la bonne
    // version. C'est ça, le polymorphisme.
    public override string ToString() => Decrire();
}
