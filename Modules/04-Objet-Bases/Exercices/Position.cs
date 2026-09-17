namespace Module04;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 4 ⭐⭐ — L'égalité : deux objets identiques
///
///  Le problème (section 10 du COURS) :
///
///      var a = new Personnage("Kaelis", 100, 12);
///      var b = new Personnage("Kaelis", 100, 12);
///      a == b        // false ! 🤨
///
///  Ici tu le résous de DEUX façons :
///    - 4.1 / 4.2 : à la main, avec Equals et GetHashCode
///    - 4.3 / 4.4 : avec un record, qui fait tout ça tout seul
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Position
{
    public int Ligne { get; private set; }
    public int Colonne { get; private set; }

    public Position(int ligne, int colonne)
    {
        Ligne = ligne;
        Colonne = colonne;
    }

    public override string ToString() => $"({Ligne}, {Colonne})";


    // ───────────────────────────────────────────────────────────────
    //  4.1 ⭐⭐ — Equals
    //
    //  Deux Position sont égales si elles ont la MÊME ligne et la
    //  MÊME colonne.
    //
    //      new Position(3, 5).Equals(new Position(3, 5))  → true
    //      new Position(3, 5).Equals(new Position(9, 9))  → false
    //      new Position(3, 5).Equals(null)                → false
    //      new Position(3, 5).Equals("bonjour")           → false
    //
    //  💡 Le motif « tester le type ET ranger », en version négative :
    //
    //       if (obj is not Position autre)
    //       {
    //           return false;          // pas une Position (ou null)
    //       }
    //       return ... compare Ligne et Colonne ...;
    //
    //  ⚠️ N'oublie pas "override" : tu REDÉFINIS la méthode Equals
    //     que tout objet possède déjà.
    // ───────────────────────────────────────────────────────────────
    public override bool Equals(object obj)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  4.2 ⭐ — GetHashCode
    //
    //  L'empreinte numérique de l'objet. Dictionary et HashSet s'en
    //  servent pour ranger et retrouver les choses très vite.
    //
    //  ⚠️ LA RÈGLE ABSOLUE :
    //     deux objets ÉGAUX doivent avoir le MÊME hash.
    //     Si tu la violes, tes objets disparaissent mystérieusement
    //     des dictionnaires. Un bug cauchemardesque à retrouver.
    //
    //  💡 Ne l'écris jamais à la main, C# le fait pour toi :
    //       return HashCode.Combine(Ligne, Colonne);
    //
    //  (C'est pour ça que le compilateur râle quand on redéfinit
    //   Equals sans GetHashCode : les deux vont toujours ensemble.)
    // ───────────────────────────────────────────────────────────────
    public override int GetHashCode()
    {
        // TODO:
        return 0;
    }
}


/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  Le RECORD  —  tout ce que tu viens d'écrire, en une ligne.
///
///  Cette déclaration :
///
///      public record Coordonnee(int Ligne, int Colonne)
///
///  ✅ te donne GRATUITEMENT, sans écrire une ligne de plus :
///        - le constructeur
///        - les propriétés Ligne et Colonne (en lecture seule)
///        - Equals et GetHashCode, qui comparent le CONTENU
///        - l'opérateur ==            (contrairement à Position !)
///        - un ToString() lisible
///
///  Elle est fournie parce que les tests s'en servent. En revanche,
///  les deux membres à l'intérieur sont à toi. 👇
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public record Coordonnee(int Ligne, int Colonne)
{
    // ───────────────────────────────────────────────────────────────
    //  4.3 ⭐ — EstOrigine
    //
    //  Vrai quand la coordonnée est la case (0, 0).
    //
    //  💡 Une propriété calculée, comme au module 4 section 3 —
    //     ça marche pareil dans un record.
    // ───────────────────────────────────────────────────────────────

    // TODO: remplace le `false` de la ligne ci-dessous
    public bool EstOrigine => false;


    // ───────────────────────────────────────────────────────────────
    //  4.4 ⭐⭐ — Deplacer
    //
    //  Retourne une NOUVELLE coordonnée, décalée.
    //
    //      new Coordonnee(3, 5).Deplacer(1, -2)  →  Coordonnee(4, 3)
    //
    //  ⚠️ Un record est IMMUABLE : `Ligne = Ligne + 1;` ne compile
    //     même pas. On ne modifie pas, on FABRIQUE UNE COPIE.
    //
    //  💡 Le mot-clé `with` fait exactement ça :
    //
    //       return this with { Ligne = Ligne + deltaLigne, ... };
    //
    //     Ça se lit : « moi, mais avec ces champs-là changés ».
    //     L'objet d'origine n'est pas touché.
    // ───────────────────────────────────────────────────────────────
    public Coordonnee Deplacer(int deltaLigne, int deltaColonne)
    {
        // TODO:
        return this;
    }
}
