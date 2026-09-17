namespace Module06;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 10 ⭐⭐ — L'historique (le Ctrl+Z !)
///
///  Une pile (Stack) qui mémorise les actions faites, pour pouvoir
///  les annuler dans l'ordre inverse.
///
///  C'est EXACTEMENT comme ça que fonctionne le Ctrl+Z de tous les
///  logiciels que tu utilises.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Historique
{
    // Une PILE : dernier posé, premier repris (LIFO).
    private readonly Stack<string> _actions = new Stack<string>();


    // ───────────────────────────────────────────────────────────────
    //  10.1 — Nombre
    //
    //  Combien d'actions sont mémorisées.
    // ───────────────────────────────────────────────────────────────

    // TODO: remplace le `0` de la ligne ci-dessous
    public int Nombre => 0;


    // ───────────────────────────────────────────────────────────────
    //  10.2 — EstVide
    // ───────────────────────────────────────────────────────────────

    // TODO: remplace le `false` de la ligne ci-dessous
    public bool EstVide => false;


    // ───────────────────────────────────────────────────────────────
    //  10.3 — Faire
    //
    //  Mémorise une action.
    //
    //  ⚠️ Une action null ou vide est ignorée (on ne mémorise pas
    //     du vide).
    //
    //  💡 Sur une Stack, on ajoute avec Push(...)
    // ───────────────────────────────────────────────────────────────
    public void Faire(string action)
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  10.4 — Annuler
    //
    //  Retire et retourne la DERNIÈRE action faite.
    //  Retourne null s'il n'y a rien à annuler.
    //
    //      h.Faire("écrire");
    //      h.Faire("colorer");
    //      h.Annuler()   → "colorer"    (la dernière !)
    //      h.Annuler()   → "écrire"
    //      h.Annuler()   → null
    //
    //  ⚠️ Pop() sur une pile VIDE lève une InvalidOperationException.
    //     Teste d'abord — c'est prévisible, donc ça se teste (pas de
    //     try/catch ici).
    //
    //  💡 Sur une Stack, on retire avec Pop()
    // ───────────────────────────────────────────────────────────────
    public string Annuler()
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  10.5 — Derniere
    //
    //  Retourne la dernière action SANS la retirer.
    //  Retourne null si l'historique est vide.
    //
    //  💡 Sur une Stack, on regarde sans retirer avec Peek()
    // ───────────────────────────────────────────────────────────────
    public string Derniere()
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  10.6 — ToutAnnuler
    //
    //  Annule TOUT, et retourne la liste des actions annulées,
    //  dans l'ordre où elles ont été annulées (donc de la plus
    //  récente à la plus ancienne).
    //
    //      h.Faire("a"); h.Faire("b"); h.Faire("c");
    //      h.ToutAnnuler()  → ["c", "b", "a"]
    //      h.Nombre         → 0
    //
    //  💡 Une boucle while tant que ce n'est pas vide, et tu
    //     réutilises ta propre méthode Annuler().
    // ───────────────────────────────────────────────────────────────
    public List<string> ToutAnnuler()
    {
        // TODO:
        return new List<string>();
    }
}
