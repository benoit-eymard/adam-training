namespace Module06;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 8 ⭐⭐⭐ — L'Inventaire
///
///  Une vraie classe (module 4) qui utilise une List (module 6).
///
///  ⚠️ Remarque la propriété "_objets" : elle est PRIVÉE.
///     Personne à l'extérieur ne peut faire inventaire._objets.Add(...)
///     et contourner la limite de capacité. C'est l'encapsulation
///     appliquée à une collection.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Inventaire
{
    // Un CHAMP privé (pas une propriété) : personne d'autre que cette
    // classe ne le voit. Le "_" en préfixe est la convention C# pour
    // les champs privés.
    private readonly List<Objet> _objets = new List<Objet>();

    public int Capacite { get; private set; }


    // ───────────────────────────────────────────────────────────────
    //  8.1 — Les propriétés calculées
    // ───────────────────────────────────────────────────────────────

    //  Nombre : combien d'objets contient l'inventaire
    //  💡 attention : sur une List, c'est .Count (pas .Length !)
    // TODO: remplace le `0` de la ligne ci-dessous
    public int Nombre => 0;

    //  EstPlein : vrai quand on a atteint la capacité
    // TODO: remplace le `false` de la ligne ci-dessous
    public bool EstPlein => false;

    //  ValeurTotale : la somme des valeurs de tous les objets
    //  💡 le motif de l'accumulateur + un foreach
    // TODO: remplace le `0` de la ligne ci-dessous
    public int ValeurTotale => 0;


    // ───────────────────────────────────────────────────────────────
    //  8.2 — Le constructeur
    //
    //  new Inventaire(10) → un sac de 10 places, vide
    // ───────────────────────────────────────────────────────────────
    public Inventaire(int capacite)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  8.3 — Ajouter
    //
    //  Ajoute un objet et retourne true.
    //  Retourne false SANS rien ajouter si :
    //    - l'inventaire est plein
    //    - l'objet est null
    //
    //  💡 Retourner un bool permet à l'appelant de réagir :
    //       if (!sac.Ajouter(epee)) Console.WriteLine("Sac plein !");
    // ───────────────────────────────────────────────────────────────
    public bool Ajouter(Objet objet)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  8.4 — Contient
    //
    //  Vrai si un objet portant ce nom est présent.
    //  La casse compte ("Épée" ≠ "épée").
    //
    //  ⚠️ _objets.Contains(...) attend un Objet, pas un string —
    //     et il compare les RÉFÉRENCES, pas les noms ! Il te faut
    //     un foreach qui compare les .Nom.
    // ───────────────────────────────────────────────────────────────
    public bool Contient(string nom)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  8.5 — Retirer
    //
    //  Retire le PREMIER objet portant ce nom et retourne true.
    //  Retourne false si aucun objet ne porte ce nom.
    //
    //  💡 Parcours avec un for (tu as besoin de l'index), et utilise
    //     _objets.RemoveAt(i) dès que tu trouves. Puis return true
    //     immédiatement — sinon tu continuerais à parcourir une
    //     liste que tu viens de modifier. 💥
    // ───────────────────────────────────────────────────────────────
    public bool Retirer(string nom)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  8.6 — ObjetLePlusCher
    //
    //  Retourne l'objet de plus grande valeur, ou null si
    //  l'inventaire est vide. En cas d'égalité, le premier trouvé.
    // ───────────────────────────────────────────────────────────────
    public Objet ObjetLePlusCher()
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  8.7 — Lister
    //
    //  Retourne la liste des NOMS des objets, dans l'ordre d'ajout.
    //
    //  ⚠️ Retourne une NOUVELLE liste. Ne donne jamais accès à ta
    //     liste interne : l'appelant pourrait la vider dans ton dos !
    // ───────────────────────────────────────────────────────────────
    public List<string> Lister()
    {
        // TODO:
        return new List<string>();
    }


    // ───────────────────────────────────────────────────────────────
    //  8.8 — ToString
    //
    //  new Inventaire(10) vide          → "Sac (0/10) : vide"
    //  avec Épée(50) et Potion(10)      → "Sac (2/10) : Épée, Potion"
    //
    //  💡 string.Join(", ", uneListe) assemble une liste en une
    //     chaîne séparée par des virgules. Très pratique !
    // ───────────────────────────────────────────────────────────────
    public override string ToString()
    {
        // TODO:
        return "";
    }
}
