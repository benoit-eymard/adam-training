using System.Diagnostics;
using Module08;
using Xunit;

namespace Module08.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 8.  Lance-les avec :  dotnet test Tests
//
//  ⏱️ Ces tests sont plus LENTS que d'habitude (quelques secondes) :
//     ils attendent vraiment. C'est normal.
//
//  Remarque les "async Task" sur les méthodes de test : xUnit sait
//  attendre une méthode de test asynchrone.
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_AdditionnerAsync
{
    [Fact]
    public async Task Additionne_bien()
    {
        Assert.Equal(7, await Exo.AdditionnerAsync(3, 4));
        Assert.Equal(0, await Exo.AdditionnerAsync(0, 0));
        Assert.Equal(-5, await Exo.AdditionnerAsync(-10, 5));
    }

    [Fact]
    public async Task Elle_attend_vraiment()
    {
        var chrono = Stopwatch.StartNew();
        await Exo.AdditionnerAsync(1, 1);
        chrono.Stop();

        Assert.True(chrono.ElapsedMilliseconds >= 40,
            $"Attendu au moins 40 ms d'attente, mesuré {chrono.ElapsedMilliseconds} ms. " +
            "As-tu bien mis await Task.Delay(50) ?");
    }
}

public class Exercice2_Sequentiel
{
    [Fact]
    public async Task Somme_des_longueurs()
    {
        Assert.Equal(5, await Exo.ChargerSequentielAsync(new[] { "ab", "cde" }));
    }

    [Fact]
    public async Task Tableau_vide()
    {
        Assert.Equal(0, await Exo.ChargerSequentielAsync(new string[0]));
    }

    [Fact]
    public async Task Une_seule_ressource()
    {
        Assert.Equal(6, await Exo.ChargerSequentielAsync(new[] { "abcdef" }));
    }
}

public class Exercice3_Parallele
{
    [Fact]
    public async Task Les_longueurs_dans_l_ORDRE()
    {
        int[] resultat = await Exo.ChargerToutesAsync(new[] { "ab", "cde", "f" });
        Assert.Equal(new[] { 2, 3, 1 }, resultat);
    }

    [Fact]
    public async Task Tableau_vide()
    {
        Assert.Empty(await Exo.ChargerToutesAsync(new string[0]));
    }

    [Fact]
    public async Task C_EST_VRAIMENT_PARALLELE()
    {
        // 5 ressources × 100 ms.
        // En séquentiel : ~500 ms.  En parallèle : ~100 ms.
        string[] ressources = { "a", "bb", "ccc", "dddd", "eeeee" };

        var chrono = Stopwatch.StartNew();
        await Exo.ChargerToutesAsync(ressources);
        chrono.Stop();

        Assert.True(chrono.ElapsedMilliseconds < 350,
            $"Mesuré {chrono.ElapsedMilliseconds} ms pour 5 chargements de 100 ms. " +
            "C'est du séquentiel ! Lance TOUTES les tâches d'abord (sans await), " +
            "puis attends-les avec Task.WhenAll.");
    }
}

public class Exercice4_TotalParallele
{
    [Fact]
    public async Task Somme_correcte()
    {
        Assert.Equal(5, await Exo.ChargerParalleleAsync(new[] { "ab", "cde" }));
    }

    [Fact]
    public async Task Tableau_vide()
    {
        Assert.Equal(0, await Exo.ChargerParalleleAsync(new string[0]));
    }

    [Fact]
    public async Task Meme_resultat_que_le_sequentiel_mais_plus_vite()
    {
        string[] ressources = { "a", "bb", "ccc", "dddd", "eeeee" };

        int sequentiel = await Exo.ChargerSequentielAsync(ressources);
        int parallele = await Exo.ChargerParalleleAsync(ressources);

        Assert.Equal(sequentiel, parallele);
        Assert.Equal(15, parallele);
    }
}

public class Exercice5_GestionErreur
{
    [Fact]
    public async Task Ressource_valide()
    {
        Assert.Equal(3, await Exo.ChargerOuMoinsUnAsync("abc"));
    }

    [Fact]
    public async Task Ressource_vide_retourne_moins_un()
    {
        Assert.Equal(-1, await Exo.ChargerOuMoinsUnAsync(""));
    }

    [Fact]
    public async Task Ressource_null_retourne_moins_un()
    {
        Assert.Equal(-1, await Exo.ChargerOuMoinsUnAsync(null));
    }
}

public class Exercice6_CalculParallele
{
    [Fact]
    public async Task Meme_resultat_qu_un_calcul_d_un_seul_bloc()
    {
        const int taille = 2_000_000;

        long attendu = Simulateur.CalculLourd(0, taille);
        long obtenu = await Exo.CalculerEnParalleleAsync(taille);

        Assert.Equal(attendu, obtenu);
    }

    [Fact]
    public async Task Fonctionne_sur_une_petite_taille()
    {
        Assert.Equal(Simulateur.CalculLourd(0, 10),
                     await Exo.CalculerEnParalleleAsync(10));
    }

    [Fact]
    public async Task Fonctionne_sur_une_taille_impaire()
    {
        // Si tu perds un élément au milieu, ce test le verra !
        Assert.Equal(Simulateur.CalculLourd(0, 1001),
                     await Exo.CalculerEnParalleleAsync(1001));
    }

    [Fact]
    public async Task Fonctionne_sur_zero()
    {
        Assert.Equal(0, await Exo.CalculerEnParalleleAsync(0));
    }
}

public class Exercice7_CompteurThreadSafe
{
    [Fact]
    public void Un_compteur_neuf_vaut_zero()
    {
        Assert.Equal(0, new Compteur().Valeur);
    }

    [Fact]
    public void IncrementerSecurise_incremente()
    {
        var c = new Compteur();
        c.IncrementerSecurise();
        c.IncrementerSecurise();
        Assert.Equal(2, c.Valeur);
    }

    [Fact]
    public void Reinitialiser_remet_a_zero()
    {
        var c = new Compteur();
        c.IncrementerSecurise();
        c.IncrementerSecurise();
        c.Reinitialiser();
        Assert.Equal(0, c.Valeur);
    }

    [Fact]
    public async Task LE_TEST_DU_MODULE_aucun_increment_perdu()
    {
        var compteur = new Compteur();

        int resultat = await Exo.MartyriserLeCompteurAsync(compteur, 4, 10_000);

        Assert.Equal(40_000, resultat);
    }

    [Fact]
    public async Task Et_ca_marche_a_TOUS_LES_COUPS()
    {
        // Une race condition marche parfois. On répète pour être sûr :
        // si un seul lock manque, ce test finira par échouer.
        for (int essai = 0; essai < 5; essai++)
        {
            var compteur = new Compteur();
            int resultat = await Exo.MartyriserLeCompteurAsync(compteur, 8, 5_000);

            Assert.Equal(40_000, resultat);
        }
    }

    [Fact]
    public async Task Avec_une_seule_tache()
    {
        var compteur = new Compteur();
        Assert.Equal(100, await Exo.MartyriserLeCompteurAsync(compteur, 1, 100));
    }

    [Fact]
    public async Task Avec_zero_tache()
    {
        var compteur = new Compteur();
        Assert.Equal(0, await Exo.MartyriserLeCompteurAsync(compteur, 0, 100));
    }
}
