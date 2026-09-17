namespace Module08;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  ⭐ DÉJÀ ÉCRIT — il simule des travaux lents.
///
///  Dans un vrai programme, ces méthodes iraient chercher des
///  données sur Internet ou sur le disque. Ici, elles font juste
///  semblant d'attendre — ce qui suffit parfaitement pour
///  comprendre l'asynchrone.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Simulateur
{
    /// <summary>Durée simulée d'un chargement, en millisecondes.</summary>
    public const int DureeMs = 100;

    /// <summary>
    /// Simule le chargement d'une ressource (téléchargement, lecture
    /// disque...). Rend la longueur du nom, après DureeMs.
    ///
    /// Remarque "await Task.Delay" et pas "Thread.Sleep" : le thread
    /// n'est PAS bloqué, il part travailler ailleurs pendant l'attente.
    /// </summary>
    public static async Task<int> ChargerAsync(string ressource)
    {
        await Task.Delay(DureeMs);
        return ressource.Length;
    }

    /// <summary>
    /// Même chose, mais ÉCHOUE si la ressource est null ou vide.
    /// Sert à s'entraîner à la gestion d'erreur en async.
    /// </summary>
    public static async Task<int> ChargerRisqueAsync(string ressource)
    {
        await Task.Delay(DureeMs);

        if (string.IsNullOrEmpty(ressource))
        {
            throw new InvalidOperationException("Ressource introuvable !");
        }

        return ressource.Length;
    }

    /// <summary>
    /// Un calcul SYNCHRONE qui prend du temps processeur.
    /// (Ce n'est pas de l'attente : le CPU travaille vraiment.)
    /// Le résultat est déterministe : même entrée, même sortie.
    /// </summary>
    public static long CalculLourd(int depart, int fin)
    {
        long total = 0;
        for (int i = depart; i < fin; i++)
        {
            total += (long)i * i % 9973;
        }
        return total;
    }
}
