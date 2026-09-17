using System.Text.Json;

namespace Module06;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 9 ⭐⭐⭐ — Sauvegarder et charger une partie
///
///  C'est le moment où ton programme laisse une trace sur le disque.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Sauvegarde
{
    // ───────────────────────────────────────────────────────────────
    //  9.1 ⭐⭐ — Enregistrer
    //
    //  Transforme la partie en JSON et l'écrit dans le fichier.
    //  Retourne true si ça a marché, false en cas de problème
    //  (disque plein, chemin invalide, droits insuffisants...).
    //
    //  ⚠️ Retourne false SANS planter si partie est null.
    //
    //  💡 Les trois étapes :
    //       string json = JsonSerializer.Serialize(partie, options);
    //       File.WriteAllText(chemin, json);
    //       return true;
    //
    //  💡 ICI, try/catch est JUSTIFIÉ : un disque plein ou un chemin
    //     interdit, tu ne peux pas le prévoir avec un if.
    //       catch (Exception) { return false; }
    //
    //  💡 Utilise OptionsJson (défini plus bas) pour un fichier
    //     lisible par un humain.
    // ───────────────────────────────────────────────────────────────
    public static bool Enregistrer(Partie partie, string chemin)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  9.2 ⭐⭐⭐ — Charger
    //
    //  Lit le fichier et reconstruit la Partie.
    //  Retourne null si :
    //    - le fichier n'existe pas
    //    - son contenu n'est pas du JSON valide (fichier corrompu)
    //    - n'importe quel autre problème de lecture
    //
    //  💡 Vérifie d'abord l'existence — ça, ça se teste :
    //       if (!File.Exists(chemin)) return null;
    //
    //  💡 Puis try/catch pour le reste (fichier corrompu = ce que tu
    //     ne peux PAS prévoir) :
    //       string json = File.ReadAllText(chemin);
    //       return JsonSerializer.Deserialize<Partie>(json);
    //
    //  ⚠️ Deserialize<Partie> : les chevrons disent QUEL type
    //     reconstruire. Sans eux, C# ne peut pas deviner.
    // ───────────────────────────────────────────────────────────────
    public static Partie Charger(string chemin)
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  9.3 ⭐ — Supprimer
    //
    //  Supprime le fichier de sauvegarde s'il existe.
    //  Retourne true s'il a été supprimé, false s'il n'existait pas
    //  ou si la suppression a échoué.
    // ───────────────────────────────────────────────────────────────
    public static bool Supprimer(string chemin)
    {
        // TODO:
        return false;
    }


    // ─── DÉJÀ ÉCRIT — les options de formatage du JSON ────────────
    // WriteIndented met le JSON en forme sur plusieurs lignes, avec
    // de l'indentation. Sans ça, tout est écrit sur une seule ligne :
    // valide, mais illisible pour un humain.
    public static readonly JsonSerializerOptions OptionsJson =
        new JsonSerializerOptions { WriteIndented = true };
}
