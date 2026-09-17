using Module04;
using Xunit;

// Les tests s'exécutent un par un (et pas en parallèle) :
// le compteur static de la classe Arme est partagé par tout le programme.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Module04.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 4.  Lance-les avec :  dotnet test Tests
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_Arme
{
    [Fact]
    public void Le_constructeur_range_les_valeurs()
    {
        var epee = new Arme("Épée longue", 8);
        Assert.Equal("Épée longue", epee.Nom);
        Assert.Equal(8, epee.DegatsBonus);
    }

    [Fact]
    public void Deux_armes_sont_independantes()
    {
        var epee = new Arme("Épée", 8);
        var arc = new Arme("Arc", 5);

        Assert.Equal("Épée", epee.Nom);
        Assert.Equal("Arc", arc.Nom);
        Assert.Equal(8, epee.DegatsBonus);
        Assert.Equal(5, arc.DegatsBonus);
    }

    [Fact]
    public void Le_compteur_static_augmente()
    {
        int avant = Arme.NombreDArmesCreees;

        new Arme("Dague", 3);
        new Arme("Masse", 10);

        Assert.Equal(avant + 2, Arme.NombreDArmesCreees);
    }

    [Fact]
    public void ToString_est_personnalise()
    {
        Assert.Equal("Épée longue (+8)", new Arme("Épée longue", 8).ToString());
        Assert.Equal("Dague (+3)", new Arme("Dague", 3).ToString());
    }
}

public class Exercice2_Monstre
{
    [Fact]
    public void Le_constructeur_range_les_valeurs()
    {
        var gobelin = new Monstre("Gobelin", 30, 7);
        Assert.Equal("Gobelin", gobelin.Nom);
        Assert.Equal(30, gobelin.PointsDeVie);
        Assert.Equal(7, gobelin.Degats);
    }

    [Fact]
    public void Un_monstre_neuf_est_vivant()
    {
        Assert.True(new Monstre("Gobelin", 30, 7).EstVivant);
    }

    [Fact]
    public void SubirDegats_retire_des_PV()
    {
        var gobelin = new Monstre("Gobelin", 30, 7);
        gobelin.SubirDegats(10);
        Assert.Equal(20, gobelin.PointsDeVie);
    }

    [Fact]
    public void Les_PV_ne_deviennent_jamais_negatifs()
    {
        var gobelin = new Monstre("Gobelin", 30, 7);
        gobelin.SubirDegats(100);
        Assert.Equal(0, gobelin.PointsDeVie);
    }

    [Fact]
    public void A_zero_PV_le_monstre_n_est_plus_vivant()
    {
        var gobelin = new Monstre("Gobelin", 30, 7);
        gobelin.SubirDegats(30);
        Assert.False(gobelin.EstVivant);
    }

    [Fact]
    public void Attaquer_inflige_des_degats_au_personnage()
    {
        var gobelin = new Monstre("Gobelin", 30, 7);
        var heros = new Personnage("Kaelis", 100, 12);

        int infliges = gobelin.Attaquer(heros);

        Assert.Equal(7, infliges);
        Assert.Equal(93, heros.PointsDeVie);
    }

    [Fact]
    public void Un_monstre_mort_n_attaque_pas()
    {
        var gobelin = new Monstre("Gobelin", 30, 7);
        var heros = new Personnage("Kaelis", 100, 12);
        gobelin.SubirDegats(30);

        int infliges = gobelin.Attaquer(heros);

        Assert.Equal(0, infliges);
        Assert.Equal(100, heros.PointsDeVie);
    }

    [Fact]
    public void ToString_est_personnalise()
    {
        Assert.Equal("Gobelin (30 PV)", new Monstre("Gobelin", 30, 7).ToString());
    }
}

public class Exercice3_Personnage_Construction
{
    [Fact]
    public void Le_constructeur_range_les_valeurs()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        Assert.Equal("Kaelis", heros.Nom);
        Assert.Equal(12, heros.Force);
    }

    [Fact]
    public void Un_personnage_neuf_demarre_en_pleine_forme()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        Assert.Equal(100, heros.PointsDeVie);
        Assert.Equal(100, heros.PointsDeVieMax);
    }

    [Fact]
    public void Un_personnage_neuf_n_a_pas_d_arme()
    {
        Assert.Null(new Personnage("Kaelis", 100, 12).ArmeEquipee);
    }

    [Fact]
    public void Deux_personnages_sont_independants()
    {
        var a = new Personnage("Kaelis", 100, 12);
        var b = new Personnage("Thorin", 150, 18);

        a.SubirDegats(50);

        Assert.Equal(50, a.PointsDeVie);
        Assert.Equal(150, b.PointsDeVie);   // b n'a pas bougé !
    }
}

public class Exercice3_Personnage_ProprietesCalculees
{
    [Fact]
    public void EstVivant_vrai_au_depart()
    {
        Assert.True(new Personnage("Kaelis", 100, 12).EstVivant);
    }

    [Fact]
    public void EstVivant_faux_a_zero_PV()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(100);
        Assert.False(heros.EstVivant);
    }

    [Fact]
    public void PointsDeVieManquants_nul_en_pleine_forme()
    {
        Assert.Equal(0, new Personnage("Kaelis", 100, 12).PointsDeVieManquants);
    }

    [Fact]
    public void PointsDeVieManquants_apres_degats()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(30);
        Assert.Equal(30, heros.PointsDeVieManquants);
    }

    [Fact]
    public void DegatsTotaux_sans_arme_vaut_la_force()
    {
        Assert.Equal(12, new Personnage("Kaelis", 100, 12).DegatsTotaux);
    }

    [Fact]
    public void DegatsTotaux_avec_arme()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.Equiper(new Arme("Épée longue", 8));
        Assert.Equal(20, heros.DegatsTotaux);
    }
}

public class Exercice3_Personnage_Actions
{
    [Fact]
    public void SubirDegats_retire_des_PV()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(30);
        Assert.Equal(70, heros.PointsDeVie);
    }

    [Fact]
    public void Les_PV_ne_deviennent_jamais_negatifs()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(999);
        Assert.Equal(0, heros.PointsDeVie);
    }

    [Fact]
    public void Soigner_rend_des_PV()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(30);
        heros.Soigner(20);
        Assert.Equal(90, heros.PointsDeVie);
    }

    [Fact]
    public void Soigner_ne_depasse_pas_le_maximum()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(30);
        heros.Soigner(500);
        Assert.Equal(100, heros.PointsDeVie);
    }

    [Fact]
    public void On_ne_ressuscite_PAS_un_mort()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(100);
        heros.Soigner(50);

        Assert.Equal(0, heros.PointsDeVie);
        Assert.False(heros.EstVivant);
    }

    [Fact]
    public void Equiper_change_l_arme()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        var epee = new Arme("Épée", 8);
        var arc = new Arme("Arc", 5);

        heros.Equiper(epee);
        Assert.Equal("Épée", heros.ArmeEquipee.Nom);

        heros.Equiper(arc);
        Assert.Equal("Arc", heros.ArmeEquipee.Nom);
        Assert.Equal(17, heros.DegatsTotaux);
    }
}

public class Exercice3_Personnage_Combat
{
    [Fact]
    public void Attaquer_inflige_les_degats_totaux()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        var gobelin = new Monstre("Gobelin", 30, 7);

        int infliges = heros.Attaquer(gobelin);

        Assert.Equal(12, infliges);
        Assert.Equal(18, gobelin.PointsDeVie);
    }

    [Fact]
    public void Attaquer_avec_une_arme_fait_plus_mal()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.Equiper(new Arme("Épée longue", 8));
        var gobelin = new Monstre("Gobelin", 30, 7);

        int infliges = heros.Attaquer(gobelin);

        Assert.Equal(20, infliges);
        Assert.Equal(10, gobelin.PointsDeVie);
    }

    [Fact]
    public void Un_personnage_mort_n_attaque_pas()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(100);
        var gobelin = new Monstre("Gobelin", 30, 7);

        int infliges = heros.Attaquer(gobelin);

        Assert.Equal(0, infliges);
        Assert.Equal(30, gobelin.PointsDeVie);
    }

    [Fact]
    public void Un_combat_complet()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.Equiper(new Arme("Épée longue", 8));
        var gobelin = new Monstre("Gobelin", 30, 7);

        heros.Attaquer(gobelin);    // 30 -> 10
        gobelin.Attaquer(heros);    // 100 -> 93
        heros.Attaquer(gobelin);    // 10 -> 0, mort

        Assert.False(gobelin.EstVivant);
        Assert.Equal(93, heros.PointsDeVie);

        gobelin.Attaquer(heros);    // il est mort, rien ne se passe
        Assert.Equal(93, heros.PointsDeVie);
    }
}

public class Exercice3_Personnage_Potion
{
    [Fact]
    public void BoirePotion_soigne()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(50);

        int rendus = heros.BoirePotion(new Potion("Potion mineure", 25, 1));

        Assert.Equal(25, rendus);
        Assert.Equal(75, heros.PointsDeVie);
    }

    [Fact]
    public void BoirePotion_ne_depasse_pas_le_maximum()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(10);

        heros.BoirePotion(new Potion("Potion majeure", 80, 1));

        Assert.Equal(100, heros.PointsDeVie);
    }

    [Fact]
    public void Une_potion_vide_ne_fait_rien()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.SubirDegats(50);
        var potion = new Potion("Potion", 25, 1);

        potion.Boire();   // on la vide d'abord

        int rendus = heros.BoirePotion(potion);

        Assert.Equal(0, rendus);
        Assert.Equal(50, heros.PointsDeVie);
    }
}

public class Exercice3_Personnage_ToString
{
    [Fact]
    public void Sans_arme()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        Assert.Equal("Kaelis (100/100 PV) — mains nues", heros.ToString());
    }

    [Fact]
    public void Avec_arme()
    {
        var heros = new Personnage("Kaelis", 100, 12);
        heros.Equiper(new Arme("Épée longue", 8));
        Assert.Equal("Kaelis (100/100 PV) — Épée longue (+8)", heros.ToString());
    }

    [Fact]
    public void Apres_des_degats()
    {
        var heros = new Personnage("Thorin", 150, 18);
        heros.SubirDegats(50);
        Assert.Equal("Thorin (100/150 PV) — mains nues", heros.ToString());
    }
}
