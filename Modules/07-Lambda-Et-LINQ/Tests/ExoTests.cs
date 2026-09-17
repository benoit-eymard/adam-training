using Module07;
using Xunit;

namespace Module07.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 7.  Lance-les avec :  dotnet test Tests
//
//  L'équipe d'exemple (définie dans Heros.cs) :
//    Thorin    Guerrier  niv.12  120 PV  300 po
//    Elyra     Mage      niv. 8   60 PV  150 po
//    Sylas     Archer    niv.10    0 PV  220 po   ← mort
//    Kaelis    Mage      niv.15   95 PV  800 po
//    Brunhild  Guerrier  niv. 5   80 PV   40 po
//    Nym       Voleur    niv. 7    0 PV   10 po   ← mort
// ═══════════════════════════════════════════════════════════════════

public class PartieA_Delegues
{
    [Fact] public void Appliquer_double() => Assert.Equal(42, Exo.Appliquer(21, x => x * 2));
    [Fact] public void Appliquer_ajoute() => Assert.Equal(15, Exo.Appliquer(5, x => x + 10));
    [Fact] public void Appliquer_carre() => Assert.Equal(16, Exo.Appliquer(4, x => x * x));
    [Fact] public void Appliquer_identite() => Assert.Equal(7, Exo.Appliquer(7, x => x));

    [Fact] public void AppliquerDeuxFois_double() => Assert.Equal(12, Exo.AppliquerDeuxFois(3, x => x * 2));
    [Fact] public void AppliquerDeuxFois_ajoute() => Assert.Equal(21, Exo.AppliquerDeuxFois(1, x => x + 10));

    [Fact]
    public void CompterSi_pairs()
    {
        Assert.Equal(2, Exo.CompterSi(new List<int> { 1, 2, 3, 4 }, n => n % 2 == 0));
    }

    [Fact]
    public void CompterSi_aucun()
    {
        Assert.Equal(0, Exo.CompterSi(new List<int> { 1, 2, 3 }, n => n > 10));
    }

    [Fact]
    public void CompterSi_tous()
    {
        Assert.Equal(3, Exo.CompterSi(new List<int> { 1, 2, 3 }, n => true));
    }

    [Fact]
    public void CompterSi_liste_vide()
    {
        Assert.Equal(0, Exo.CompterSi(new List<int>(), n => true));
    }
}

public class PartieB_FiltrerTransformer
{
    [Fact]
    public void Vivants()
    {
        var vivants = Exo.Vivants(Equipes.Exemple());

        Assert.Equal(4, vivants.Count);
        Assert.DoesNotContain(vivants, h => h.Nom == "Sylas");
        Assert.DoesNotContain(vivants, h => h.Nom == "Nym");
    }

    [Fact]
    public void Vivants_garde_l_ordre_d_origine()
    {
        var vivants = Exo.Vivants(Equipes.Exemple());
        Assert.Equal("Thorin", vivants[0].Nom);
        Assert.Equal("Elyra", vivants[1].Nom);
    }

    [Fact]
    public void Vivants_equipe_vide()
    {
        Assert.Empty(Exo.Vivants(new List<Heros>()));
    }

    [Fact]
    public void Noms()
    {
        var noms = Exo.Noms(Equipes.Exemple());
        Assert.Equal(6, noms.Count);
        Assert.Equal("Thorin", noms[0]);
        Assert.Equal("Nym", noms[5]);
    }

    [Fact]
    public void DeClasse_Mage()
    {
        var mages = Exo.DeClasse(Equipes.Exemple(), "Mage");
        Assert.Equal(2, mages.Count);
        Assert.All(mages, h => Assert.Equal("Mage", h.Classe));
    }

    [Fact]
    public void DeClasse_inconnue()
    {
        Assert.Empty(Exo.DeClasse(Equipes.Exemple(), "Paladin"));
    }
}

public class PartieC_Agreger
{
    [Fact]
    public void OrTotal()
    {
        // 300 + 150 + 220 + 800 + 40 + 10
        Assert.Equal(1520, Exo.OrTotal(Equipes.Exemple()));
    }

    [Fact]
    public void OrTotal_equipe_vide()
    {
        Assert.Equal(0, Exo.OrTotal(new List<Heros>()));
    }

    [Fact]
    public void NiveauMoyen()
    {
        // (12 + 8 + 10 + 15 + 5 + 7) / 6 = 9.5
        Assert.Equal(9.5, Exo.NiveauMoyen(Equipes.Exemple()), 4);
    }

    [Fact]
    public void NiveauMoyen_equipe_vide_ne_plante_pas()
    {
        // .Average() sur une liste vide lève une exception !
        Assert.Equal(0.0, Exo.NiveauMoyen(new List<Heros>()), 4);
    }

    [Fact]
    public void LePlusRiche()
    {
        Assert.Equal("Kaelis", Exo.LePlusRiche(Equipes.Exemple()).Nom);
    }

    [Fact]
    public void LePlusRiche_equipe_vide()
    {
        Assert.Null(Exo.LePlusRiche(new List<Heros>()));
    }

    [Fact]
    public void NombreDeVivants()
    {
        Assert.Equal(4, Exo.NombreDeVivants(Equipes.Exemple()));
    }

    [Fact]
    public void NombreDeVivants_equipe_vide()
    {
        Assert.Equal(0, Exo.NombreDeVivants(new List<Heros>()));
    }
}

public class PartieD_TrierTester
{
    [Fact]
    public void TriesParNiveauDecroissant()
    {
        var tries = Exo.TriesParNiveauDecroissant(Equipes.Exemple());

        Assert.Equal(6, tries.Count);
        Assert.Equal("Kaelis", tries[0].Nom);     // 15
        Assert.Equal("Thorin", tries[1].Nom);     // 12
        Assert.Equal("Sylas", tries[2].Nom);      // 10
        Assert.Equal("Brunhild", tries[5].Nom);   // 5
    }

    [Fact]
    public void TriesParNiveau_equipe_vide()
    {
        Assert.Empty(Exo.TriesParNiveauDecroissant(new List<Heros>()));
    }

    [Fact] public void AuMoinsUn_mage() => Assert.True(Exo.AuMoinsUn(Equipes.Exemple(), "Mage"));
    [Fact] public void AuMoinsUn_voleur() => Assert.True(Exo.AuMoinsUn(Equipes.Exemple(), "Voleur"));
    [Fact] public void AuMoinsUn_paladin() => Assert.False(Exo.AuMoinsUn(Equipes.Exemple(), "Paladin"));
    [Fact] public void AuMoinsUn_equipe_vide() => Assert.False(Exo.AuMoinsUn(new List<Heros>(), "Mage"));

    [Fact]
    public void TousVivants_faux_sur_l_equipe_exemple()
    {
        Assert.False(Exo.TousVivants(Equipes.Exemple()));
    }

    [Fact]
    public void TousVivants_vrai_quand_ils_le_sont()
    {
        var equipe = new List<Heros>
        {
            new Heros("A", "Mage", 1, 10, 0),
            new Heros("B", "Mage", 1, 20, 0)
        };
        Assert.True(Exo.TousVivants(equipe));
    }

    [Fact]
    public void TousVivants_sur_equipe_vide_vaut_TRUE()
    {
        // Surprenant, mais mathématiquement correct :
        // aucun élément ne contredit la condition.
        Assert.True(Exo.TousVivants(new List<Heros>()));
    }
}

public class PartieE_Enchainer
{
    [Fact]
    public void NomsDesVivantsParOrdreAlphabetique()
    {
        var noms = Exo.NomsDesVivantsParOrdreAlphabetique(Equipes.Exemple());

        Assert.Equal(new List<string> { "Brunhild", "Elyra", "Kaelis", "Thorin" }, noms);
    }

    [Fact]
    public void NomsDesVivants_equipe_vide()
    {
        Assert.Empty(Exo.NomsDesVivantsParOrdreAlphabetique(new List<Heros>()));
    }

    [Fact]
    public void LesPlusRiches()
    {
        var riches = Exo.LesPlusRiches(Equipes.Exemple(), 2);

        Assert.Equal(2, riches.Count);
        Assert.Equal("Kaelis", riches[0].Nom);    // 800
        Assert.Equal("Thorin", riches[1].Nom);    // 300
    }

    [Fact]
    public void LesPlusRiches_n_trop_grand_ne_plante_pas()
    {
        Assert.Equal(6, Exo.LesPlusRiches(Equipes.Exemple(), 100).Count);
    }

    [Fact]
    public void LesPlusRiches_zero()
    {
        Assert.Empty(Exo.LesPlusRiches(Equipes.Exemple(), 0));
    }

    [Fact]
    public void OrDesVivants()
    {
        // 300 + 150 + 800 + 40  (sans Sylas ni Nym)
        Assert.Equal(1290, Exo.OrDesVivants(Equipes.Exemple()));
    }

    [Fact]
    public void OrDesVivants_equipe_vide()
    {
        Assert.Equal(0, Exo.OrDesVivants(new List<Heros>()));
    }

    [Fact]
    public void RepartitionParClasse()
    {
        var repartition = Exo.RepartitionParClasse(Equipes.Exemple());

        Assert.Equal(4, repartition.Count);
        Assert.Equal(2, repartition["Guerrier"]);
        Assert.Equal(2, repartition["Mage"]);
        Assert.Equal(1, repartition["Archer"]);
        Assert.Equal(1, repartition["Voleur"]);
    }

    [Fact]
    public void RepartitionParClasse_equipe_vide()
    {
        Assert.Empty(Exo.RepartitionParClasse(new List<Heros>()));
    }

    [Fact]
    public void ClasseLaPlusRepresentee()
    {
        var equipe = new List<Heros>
        {
            new Heros("A", "Mage", 1, 10, 0),
            new Heros("B", "Mage", 1, 10, 0),
            new Heros("C", "Mage", 1, 10, 0),
            new Heros("D", "Guerrier", 1, 10, 0)
        };

        Assert.Equal("Mage", Exo.ClasseLaPlusRepresentee(equipe));
    }

    [Fact]
    public void ClasseLaPlusRepresentee_equipe_vide()
    {
        Assert.Null(Exo.ClasseLaPlusRepresentee(new List<Heros>()));
    }

    [Fact]
    public void Rapport()
    {
        string attendu =
            "Kaelis (Mage) — niveau 15\n" +
            "Thorin (Guerrier) — niveau 12\n" +
            "Elyra (Mage) — niveau 8\n" +
            "Brunhild (Guerrier) — niveau 5";

        Assert.Equal(attendu, Exo.Rapport(Equipes.Exemple()));
    }

    [Fact]
    public void Rapport_equipe_vide()
    {
        Assert.Equal("", Exo.Rapport(new List<Heros>()));
    }

    [Fact]
    public void Rapport_ne_finit_pas_par_un_retour_a_la_ligne()
    {
        Assert.False(Exo.Rapport(Equipes.Exemple()).EndsWith("\n"));
    }
}

public class LeMomentOuTuComprends
{
    [Fact]
    public void Une_requete_complete_en_quelques_lignes()
    {
        var equipe = Equipes.Exemple();

        // « Les noms des mages vivants de niveau 10 ou plus,
        //    triés par or décroissant »
        var resultat = equipe
            .Where(h => h.Classe == "Mage")
            .Where(h => h.EstVivant)
            .Where(h => h.Niveau >= 10)
            .OrderByDescending(h => h.Or)
            .Select(h => h.Nom)
            .ToList();

        Assert.Single(resultat);
        Assert.Equal("Kaelis", resultat[0]);

        // En boucles, ça ferait une quinzaine de lignes.
        // Et ce serait bien moins clair. 🪄
    }
}

public class PartieF_Aggregate
{
    [Fact] public void Somme_simple() => Assert.Equal(8, Exo.SommeAvecAggregate(new List<int> { 3, 1, 4 }));
    [Fact] public void Somme_un_seul() => Assert.Equal(42, Exo.SommeAvecAggregate(new List<int> { 42 }));
    [Fact] public void Somme_negatifs() => Assert.Equal(-2, Exo.SommeAvecAggregate(new List<int> { 3, -5 }));

    [Fact]
    public void Somme_liste_vide_ne_plante_pas()
    {
        // Sans seed, Aggregate leverait une InvalidOperationException ici.
        Assert.Equal(0, Exo.SommeAvecAggregate(new List<int>()));
    }

    [Fact]
    public void Chemin_complet()
    {
        Assert.Equal("Entrée > Couloir > Trésor",
            Exo.Chemin(new List<string> { "Entrée", "Couloir", "Trésor" }));
    }

    [Fact]
    public void Chemin_une_seule_etape_sans_separateur()
    {
        Assert.Equal("Entrée", Exo.Chemin(new List<string> { "Entrée" }));
    }

    [Fact]
    public void Chemin_vide_ne_plante_pas()
    {
        Assert.Equal("", Exo.Chemin(new List<string>()));
    }

    [Fact]
    public void LePlusFortAvecAggregate()
    {
        Assert.Equal("Kaelis", Exo.LePlusFortAvecAggregate(Equipes.Exemple()).Nom);
    }

    [Fact]
    public void LePlusFortAvecAggregate_equipe_vide()
    {
        Assert.Null(Exo.LePlusFortAvecAggregate(new List<Heros>()));
    }

    [Fact]
    public void LePlusFortAvecAggregate_en_cas_d_egalite_le_premier()
    {
        var equipe = new List<Heros>
        {
            new Heros("A", "Mage", 10, 10, 0),
            new Heros("B", "Mage", 10, 10, 0)
        };
        Assert.Equal("A", Exo.LePlusFortAvecAggregate(equipe).Nom);
    }

    [Fact]
    public void LePlusFortAvecAggregate_donne_le_meme_resultat_que_MaxBy()
    {
        var equipe = Equipes.Exemple();
        Assert.Equal(equipe.MaxBy(h => h.Niveau).Nom,
                     Exo.LePlusFortAvecAggregate(equipe).Nom);
    }
}

public class PartieF_SelectMany_et_Zip
{
    [Fact]
    public void TousLesObjets_aplatit_dedoublonne_et_trie()
    {
        // Remarque l'ordre : "Épée" se range juste apres "Bouclier".
        // OrderBy trie LINGUISTIQUEMENT (E accentue ~ E), pas par code
        // de caractere. C'est ce qu'attend un lecteur humain.
        var attendu = new List<string> { "Arc", "Bâton", "Bouclier", "Épée", "Grimoire", "Potion" };
        Assert.Equal(attendu, Exo.TousLesObjets(Equipes.Exemple()));
    }

    [Fact]
    public void TousLesObjets_equipe_vide()
    {
        Assert.Empty(Exo.TousLesObjets(new List<Heros>()));
    }

    [Fact]
    public void TousLesObjets_sans_aucun_objet()
    {
        var equipe = new List<Heros> { new Heros("A", "Mage", 1, 10, 0) };
        Assert.Empty(Exo.TousLesObjets(equipe));
    }

    [Fact]
    public void Duels()
    {
        var gauche = new List<Heros> { new Heros("Thorin", "Guerrier", 1, 10, 0),
                                       new Heros("Elyra", "Mage", 1, 10, 0) };
        var droite = new List<Heros> { new Heros("Gobelin", "Monstre", 1, 10, 0),
                                       new Heros("Orc", "Monstre", 1, 10, 0) };

        Assert.Equal(new List<string> { "Thorin vs Gobelin", "Elyra vs Orc" },
                     Exo.Duels(gauche, droite));
    }

    [Fact]
    public void Duels_s_arrete_a_la_plus_courte()
    {
        var gauche = new List<Heros> { new Heros("A", "Mage", 1, 10, 0),
                                       new Heros("B", "Mage", 1, 10, 0),
                                       new Heros("C", "Mage", 1, 10, 0) };
        var droite = new List<Heros> { new Heros("X", "Mage", 1, 10, 0) };

        Assert.Single(Exo.Duels(gauche, droite));
        Assert.Equal("A vs X", Exo.Duels(gauche, droite)[0]);
    }

    [Fact]
    public void Duels_avec_une_equipe_vide()
    {
        Assert.Empty(Exo.Duels(new List<Heros>(), Equipes.Exemple()));
    }
}
