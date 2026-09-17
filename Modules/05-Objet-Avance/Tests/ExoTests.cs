using Module05;
using Xunit;

namespace Module05.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 5.  Lance-les avec :  dotnet test Tests
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_Guerrier
{
    [Fact]
    public void Le_constructeur_initialise_tout()
    {
        var thorin = new Guerrier("Thorin", 120, 15);

        Assert.Equal("Thorin", thorin.Nom);          // hérité de Combattant
        Assert.Equal(120, thorin.PointsDeVie);       // hérité
        Assert.Equal(120, thorin.PointsDeVieMax);    // hérité
        Assert.Equal(15, thorin.Force);              // propre au Guerrier
        Assert.False(thorin.EnRage);
    }

    [Fact]
    public void Un_Guerrier_EST_UN_Combattant()
    {
        Assert.IsAssignableFrom<Combattant>(new Guerrier("Thorin", 120, 15));
    }

    [Fact]
    public void Il_herite_de_SubirDegats()
    {
        var thorin = new Guerrier("Thorin", 120, 15);
        thorin.SubirDegats(40);
        Assert.Equal(80, thorin.PointsDeVie);
        Assert.True(thorin.EstVivant);
    }

    [Fact]
    public void Attaquer_inflige_la_Force()
    {
        var thorin = new Guerrier("Thorin", 120, 15);
        var cible = new Guerrier("Cible", 100, 5);

        int degats = thorin.Attaquer(cible);

        Assert.Equal(15, degats);
        Assert.Equal(85, cible.PointsDeVie);
    }

    [Fact]
    public void La_rage_double_les_degats()
    {
        var thorin = new Guerrier("Thorin", 120, 15);
        var cible = new Guerrier("Cible", 100, 5);

        thorin.EntrerEnRage();
        Assert.True(thorin.EnRage);

        int degats = thorin.Attaquer(cible);

        Assert.Equal(30, degats);
        Assert.Equal(70, cible.PointsDeVie);
    }

    [Fact]
    public void La_rage_retombe_apres_l_attaque()
    {
        var thorin = new Guerrier("Thorin", 120, 15);
        var cible = new Guerrier("Cible", 200, 5);

        thorin.EntrerEnRage();
        thorin.Attaquer(cible);          // 30 dégâts

        Assert.False(thorin.EnRage);
        Assert.Equal(15, thorin.Attaquer(cible));   // retour à la normale
    }

    [Fact]
    public void Un_mort_n_attaque_pas()
    {
        var thorin = new Guerrier("Thorin", 120, 15);
        var cible = new Guerrier("Cible", 100, 5);
        thorin.SubirDegats(120);

        Assert.Equal(0, thorin.Attaquer(cible));
        Assert.Equal(100, cible.PointsDeVie);
    }

    [Fact]
    public void Decrire_complete_la_version_du_parent()
    {
        var thorin = new Guerrier("Thorin", 120, 15);
        Assert.Equal("Thorin (120/120 PV) — Guerrier (force 15)", thorin.Decrire());
    }
}

public class Exercice2_Archer
{
    [Fact]
    public void Le_constructeur_initialise_tout()
    {
        var sylas = new Archer("Sylas", 90, 12);
        Assert.Equal("Sylas", sylas.Nom);
        Assert.Equal(90, sylas.PointsDeVie);
        Assert.Equal(12, sylas.Fleches);
        Assert.True(sylas.APlusDeFleches);
    }

    [Fact]
    public void Sans_fleches_APlusDeFleches_est_faux()
    {
        Assert.False(new Archer("Sylas", 90, 0).APlusDeFleches);
    }

    [Fact]
    public void Attaquer_consomme_une_fleche()
    {
        var sylas = new Archer("Sylas", 90, 12);
        var cible = new Guerrier("Cible", 100, 5);

        int degats = sylas.Attaquer(cible);

        Assert.Equal(Archer.DegatsFleche, degats);
        Assert.Equal(11, sylas.Fleches);
        Assert.Equal(85, cible.PointsDeVie);
    }

    [Fact]
    public void Sans_fleches_il_tape_au_corps_a_corps()
    {
        var sylas = new Archer("Sylas", 90, 0);
        var cible = new Guerrier("Cible", 100, 5);

        int degats = sylas.Attaquer(cible);

        Assert.Equal(Archer.DegatsCorpsACorps, degats);
        Assert.Equal(0, sylas.Fleches);      // pas de flèches négatives !
        Assert.Equal(97, cible.PointsDeVie);
    }

    [Fact]
    public void Il_epuise_son_carquois()
    {
        var sylas = new Archer("Sylas", 90, 2);
        var cible = new Guerrier("Cible", 200, 5);

        Assert.Equal(15, sylas.Attaquer(cible));
        Assert.Equal(15, sylas.Attaquer(cible));
        Assert.Equal(3, sylas.Attaquer(cible));   // plus rien
        Assert.Equal(0, sylas.Fleches);
    }

    [Fact]
    public void Ramasser_ajoute_des_fleches()
    {
        var sylas = new Archer("Sylas", 90, 2);
        sylas.Ramasser(5);
        Assert.Equal(7, sylas.Fleches);
    }

    [Fact]
    public void Ramasser_un_nombre_negatif_ne_fait_rien()
    {
        var sylas = new Archer("Sylas", 90, 2);
        sylas.Ramasser(-10);
        Assert.Equal(2, sylas.Fleches);
    }

    [Fact]
    public void Un_mort_n_attaque_pas()
    {
        var sylas = new Archer("Sylas", 90, 12);
        var cible = new Guerrier("Cible", 100, 5);
        sylas.SubirDegats(90);

        Assert.Equal(0, sylas.Attaquer(cible));
        Assert.Equal(12, sylas.Fleches);     // aucune flèche gaspillée
    }

    [Fact]
    public void Decrire()
    {
        Assert.Equal("Sylas (90/90 PV) — Archer (12 flèches)",
            new Archer("Sylas", 90, 12).Decrire());
    }
}

public class Exercice3_Mage
{
    [Fact]
    public void Le_constructeur_demarre_avec_tout_son_mana()
    {
        var elyra = new Mage("Elyra", 80, 40);
        Assert.Equal("Elyra", elyra.Nom);
        Assert.Equal(80, elyra.PointsDeVie);
        Assert.Equal(40, elyra.Mana);
        Assert.Equal(40, elyra.ManaMax);
    }

    [Fact]
    public void Un_Mage_signe_le_contrat_ISoigneur()
    {
        Assert.IsAssignableFrom<ISoigneur>(new Mage("Elyra", 80, 40));
    }

    [Fact]
    public void Le_sort_consomme_du_mana()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var cible = new Guerrier("Cible", 100, 5);

        int degats = elyra.Attaquer(cible);

        Assert.Equal(Mage.DegatsDuSort, degats);
        Assert.Equal(30, elyra.Mana);
        Assert.Equal(75, cible.PointsDeVie);
    }

    [Fact]
    public void Sans_mana_il_tape_au_baton()
    {
        var elyra = new Mage("Elyra", 80, 5);   // pas assez pour un sort
        var cible = new Guerrier("Cible", 100, 5);

        int degats = elyra.Attaquer(cible);

        Assert.Equal(Mage.DegatsBaton, degats);
        Assert.Equal(5, elyra.Mana);            // rien n'a été dépensé
    }

    [Fact]
    public void Il_epuise_son_mana()
    {
        var elyra = new Mage("Elyra", 80, 20);
        var cible = new Guerrier("Cible", 500, 5);

        Assert.Equal(25, elyra.Attaquer(cible));   // mana 20 -> 10
        Assert.Equal(25, elyra.Attaquer(cible));   // mana 10 -> 0
        Assert.Equal(5, elyra.Attaquer(cible));    // plus de mana
        Assert.Equal(0, elyra.Mana);
    }

    [Fact]
    public void Un_mort_n_attaque_pas()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var cible = new Guerrier("Cible", 100, 5);
        elyra.SubirDegats(80);

        Assert.Equal(0, elyra.Attaquer(cible));
        Assert.Equal(40, elyra.Mana);
    }

    [Fact]
    public void SoignerAllie_rend_des_PV_et_coute_du_mana()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var thorin = new Guerrier("Thorin", 120, 15);
        thorin.SubirDegats(50);      // 70/120

        int rendus = elyra.SoignerAllie(thorin);

        Assert.Equal(Mage.PointsDeSoin, rendus);
        Assert.Equal(100, thorin.PointsDeVie);
        Assert.Equal(25, elyra.Mana);
    }

    [Fact]
    public void SoignerAllie_retourne_les_PV_REELLEMENT_rendus()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var thorin = new Guerrier("Thorin", 120, 15);
        thorin.SubirDegats(10);      // 110/120 : il ne manque que 10 PV

        int rendus = elyra.SoignerAllie(thorin);

        Assert.Equal(10, rendus);          // et pas 30 !
        Assert.Equal(120, thorin.PointsDeVie);
    }

    [Fact]
    public void Pas_assez_de_mana_pour_soigner()
    {
        var elyra = new Mage("Elyra", 80, 10);
        var thorin = new Guerrier("Thorin", 120, 15);
        thorin.SubirDegats(50);

        Assert.Equal(0, elyra.SoignerAllie(thorin));
        Assert.Equal(10, elyra.Mana);
        Assert.Equal(70, thorin.PointsDeVie);
    }

    [Fact]
    public void On_ne_soigne_pas_un_mort()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var thorin = new Guerrier("Thorin", 120, 15);
        thorin.SubirDegats(120);

        Assert.Equal(0, elyra.SoignerAllie(thorin));
        Assert.Equal(40, elyra.Mana);     // pas de mana gaspillé
    }

    [Fact]
    public void On_ne_soigne_pas_quelqu_un_en_pleine_forme()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var thorin = new Guerrier("Thorin", 120, 15);

        Assert.Equal(0, elyra.SoignerAllie(thorin));
        Assert.Equal(40, elyra.Mana);
    }

    [Fact]
    public void Un_mage_mort_ne_soigne_pas()
    {
        var elyra = new Mage("Elyra", 80, 40);
        var thorin = new Guerrier("Thorin", 120, 15);
        thorin.SubirDegats(50);
        elyra.SubirDegats(80);

        Assert.Equal(0, elyra.SoignerAllie(thorin));
    }

    [Fact]
    public void Decrire()
    {
        Assert.Equal("Elyra (80/80 PV) — Mage (40/40 mana)",
            new Mage("Elyra", 80, 40).Decrire());
    }
}

public class Exercice4_Polymorphisme
{
    private static Combattant[] CreerEquipe() => new Combattant[]
    {
        new Guerrier("Thorin", 120, 15),
        new Mage("Elyra", 80, 40),
        new Archer("Sylas", 90, 12)
    };

    [Fact]
    public void Un_tableau_peut_melanger_les_types()
    {
        Combattant[] equipe = CreerEquipe();
        Assert.Equal(3, equipe.Length);
    }

    [Fact]
    public void NombreDeVivants_au_complet()
    {
        Assert.Equal(3, Utilitaires.NombreDeVivants(CreerEquipe()));
    }

    [Fact]
    public void NombreDeVivants_apres_des_pertes()
    {
        Combattant[] equipe = CreerEquipe();
        equipe[0].SubirDegats(120);
        equipe[2].SubirDegats(90);

        Assert.Equal(1, Utilitaires.NombreDeVivants(equipe));
    }

    [Fact]
    public void NombreDeVivants_equipe_vide()
    {
        Assert.Equal(0, Utilitaires.NombreDeVivants(new Combattant[0]));
    }

    [Fact]
    public void LePlusBlesse()
    {
        Combattant[] equipe = CreerEquipe();
        equipe[0].SubirDegats(20);   // Thorin : 20 manquants
        equipe[1].SubirDegats(50);   // Elyra  : 50 manquants
        equipe[2].SubirDegats(10);   // Sylas  : 10 manquants

        Assert.Equal("Elyra", Utilitaires.LePlusBlesse(equipe).Nom);
    }

    [Fact]
    public void LePlusBlesse_ignore_les_morts()
    {
        Combattant[] equipe = CreerEquipe();
        equipe[1].SubirDegats(80);   // Elyra est MORTE (80 manquants)
        equipe[0].SubirDegats(20);   // Thorin : 20 manquants

        Assert.Equal("Thorin", Utilitaires.LePlusBlesse(equipe).Nom);
    }

    [Fact]
    public void LePlusBlesse_equipe_vide_retourne_null()
    {
        Assert.Null(Utilitaires.LePlusBlesse(new Combattant[0]));
    }

    [Fact]
    public void LePlusBlesse_tous_morts_retourne_null()
    {
        Combattant[] equipe = CreerEquipe();
        foreach (var c in equipe) c.SubirDegats(999);

        Assert.Null(Utilitaires.LePlusBlesse(equipe));
    }

    [Fact]
    public void AttaqueGroupee_chacun_attaque_a_SA_facon()
    {
        Combattant[] equipe = CreerEquipe();
        var cible = new Guerrier("Boss", 500, 10);

        int total = Utilitaires.AttaqueGroupee(equipe, cible);

        // Guerrier 15 + Mage 25 + Archer 15 = 55
        Assert.Equal(55, total);
        Assert.Equal(445, cible.PointsDeVie);
    }

    [Fact]
    public void AttaqueGroupee_ignore_les_morts()
    {
        Combattant[] equipe = CreerEquipe();
        equipe[1].SubirDegats(80);   // Elyra morte
        var cible = new Guerrier("Boss", 500, 10);

        int total = Utilitaires.AttaqueGroupee(equipe, cible);

        Assert.Equal(30, total);     // 15 + 0 + 15
    }

    [Fact]
    public void NombreDeSoigneurs()
    {
        Assert.Equal(1, Utilitaires.NombreDeSoigneurs(CreerEquipe()));
    }

    [Fact]
    public void NombreDeSoigneurs_sans_mage()
    {
        Combattant[] equipe = {
            new Guerrier("A", 100, 10),
            new Archer("B", 100, 5)
        };
        Assert.Equal(0, Utilitaires.NombreDeSoigneurs(equipe));
    }

    [Fact]
    public void NombreDeSoigneurs_avec_deux_mages()
    {
        Combattant[] equipe = {
            new Guerrier("A", 100, 10),
            new Mage("B", 80, 40),
            new Mage("C", 80, 40)
        };
        Assert.Equal(2, Utilitaires.NombreDeSoigneurs(equipe));
    }

    [Fact]
    public void SoinDUrgence_soigne_le_plus_blesse()
    {
        Combattant[] equipe = CreerEquipe();
        equipe[0].SubirDegats(60);   // Thorin : 60/120

        int rendus = Utilitaires.SoinDUrgence(equipe);

        Assert.Equal(30, rendus);
        Assert.Equal(90, equipe[0].PointsDeVie);
    }

    [Fact]
    public void SoinDUrgence_sans_soigneur()
    {
        Combattant[] equipe = {
            new Guerrier("A", 100, 10),
            new Archer("B", 100, 5)
        };
        equipe[0].SubirDegats(50);

        Assert.Equal(0, Utilitaires.SoinDUrgence(equipe));
    }

    [Fact]
    public void SoinDUrgence_personne_a_soigner()
    {
        Assert.Equal(0, Utilitaires.SoinDUrgence(CreerEquipe()));
    }

    [Fact]
    public void SoinDUrgence_le_soigneur_ne_se_soigne_pas_lui_meme()
    {
        Combattant[] equipe = CreerEquipe();
        equipe[1].SubirDegats(40);   // c'est Elyra la plus blessée

        Assert.Equal(0, Utilitaires.SoinDUrgence(equipe));
    }
}

public class Polymorphisme_LaDemonstration
{
    [Fact]
    public void Une_seule_boucle_trois_comportements()
    {
        Combattant[] equipe =
        {
            new Guerrier("Thorin", 120, 15),
            new Mage("Elyra", 80, 40),
            new Archer("Sylas", 90, 12)
        };

        string[] descriptions = new string[3];
        for (int i = 0; i < equipe.Length; i++)
        {
            descriptions[i] = equipe[i].Decrire();   // UN seul appel...
        }

        // ... et TROIS résultats différents. C'est le polymorphisme.
        Assert.Equal("Thorin (120/120 PV) — Guerrier (force 15)", descriptions[0]);
        Assert.Equal("Elyra (80/80 PV) — Mage (40/40 mana)", descriptions[1]);
        Assert.Equal("Sylas (90/90 PV) — Archer (12 flèches)", descriptions[2]);
    }

    [Fact]
    public void ToString_passe_par_Decrire()
    {
        Combattant c = new Guerrier("Thorin", 120, 15);
        Assert.Equal(c.Decrire(), c.ToString());
    }
}
