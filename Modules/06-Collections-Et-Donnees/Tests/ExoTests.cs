using Module06;
using Xunit;

namespace Module06.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 6.  Lance-les avec :  dotnet test Tests
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_ConvertirOuDefaut
{
    [Theory]
    [InlineData("42", 0, 42)]
    [InlineData("-15", 0, -15)]
    [InlineData("0", 7, 0)]
    [InlineData("bonjour", 0, 0)]
    [InlineData("", 7, 7)]
    [InlineData("12.5", 7, 7)]      // ce n'est pas un ENTIER
    [InlineData(null, 7, 7)]
    public void Convertit_ou_retourne_le_defaut(string saisie, int defaut, int attendu)
    {
        Assert.Equal(attendu, Exo.ConvertirOuDefaut(saisie, defaut));
    }
}

public class Exercice2_SansDoublons
{
    [Fact]
    public void Retire_les_doublons_en_gardant_l_ordre()
    {
        var source = new List<string> { "a", "b", "a", "c", "b" };
        Assert.Equal(new List<string> { "a", "b", "c" }, Exo.SansDoublons(source));
    }

    [Fact]
    public void Liste_sans_doublon_inchangee()
    {
        var source = new List<string> { "x", "y", "z" };
        Assert.Equal(new List<string> { "x", "y", "z" }, Exo.SansDoublons(source));
    }

    [Fact]
    public void Liste_vide()
    {
        Assert.Empty(Exo.SansDoublons(new List<string>()));
    }

    [Fact]
    public void L_original_n_est_PAS_modifie()
    {
        var source = new List<string> { "a", "b", "a" };
        Exo.SansDoublons(source);
        Assert.Equal(3, source.Count);
    }
}

public class Exercice3_CompterMots
{
    [Fact]
    public void Compte_correctement()
    {
        var resultat = Exo.CompterMots(new[] { "a", "b", "a" });

        Assert.Equal(2, resultat.Count);
        Assert.Equal(2, resultat["a"]);
        Assert.Equal(1, resultat["b"]);
    }

    [Fact]
    public void Tableau_vide_donne_dictionnaire_vide()
    {
        Assert.Empty(Exo.CompterMots(new string[0]));
    }

    [Fact]
    public void Un_seul_mot_repete()
    {
        var resultat = Exo.CompterMots(new[] { "épée", "épée", "épée" });
        Assert.Single(resultat);
        Assert.Equal(3, resultat["épée"]);
    }

    [Fact]
    public void La_casse_compte()
    {
        var resultat = Exo.CompterMots(new[] { "Épée", "épée" });
        Assert.Equal(2, resultat.Count);
    }
}

public class Exercice4_LePlusFrequent
{
    [Fact] public void Cas_simple() => Assert.Equal("a", Exo.LePlusFrequent(new[] { "a", "b", "a" }));
    [Fact] public void Dernier_gagnant() => Assert.Equal("z", Exo.LePlusFrequent(new[] { "a", "z", "z" }));
    [Fact] public void Tableau_vide() => Assert.Null(Exo.LePlusFrequent(new string[0]));
    [Fact] public void Un_seul_element() => Assert.Equal("seul", Exo.LePlusFrequent(new[] { "seul" }));

    [Fact]
    public void En_cas_d_egalite_le_premier_rencontre()
    {
        Assert.Equal("a", Exo.LePlusFrequent(new[] { "a", "b" }));
        Assert.Equal("b", Exo.LePlusFrequent(new[] { "b", "a" }));
    }
}

public class Exercice5_DiviserSansPlanter
{
    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(7, 2, 3)]        // division entière
    [InlineData(0, 5, 0)]
    [InlineData(10, 0, 0)]       // le cas qui plante normalement
    [InlineData(-10, 2, -5)]
    public void Divise_sans_planter(int a, int b, int attendu)
    {
        Assert.Equal(attendu, Exo.DiviserSansPlanter(a, b));
    }
}

public class Exercice6_ElementSur
{
    private static List<string> Liste() => new List<string> { "a", "b", "c" };

    [Fact] public void Index_valide() => Assert.Equal("b", Exo.ElementSur(Liste(), 1));
    [Fact] public void Premier() => Assert.Equal("a", Exo.ElementSur(Liste(), 0));
    [Fact] public void Dernier() => Assert.Equal("c", Exo.ElementSur(Liste(), 2));
    [Fact] public void Trop_grand() => Assert.Equal("", Exo.ElementSur(Liste(), 9));
    [Fact] public void Juste_au_dessus() => Assert.Equal("", Exo.ElementSur(Liste(), 3));
    [Fact] public void Negatif() => Assert.Equal("", Exo.ElementSur(Liste(), -1));
    [Fact] public void Liste_vide() => Assert.Equal("", Exo.ElementSur(new List<string>(), 0));
    [Fact] public void Liste_null() => Assert.Equal("", Exo.ElementSur(null, 0));
}

public class Exercice7_MoyenneDesValides
{
    [Fact] public void Tout_valide() => Assert.Equal(20.0, Exo.MoyenneDesValides(new[] { "10", "20", "30" }), 4);
    [Fact] public void Avec_intrus() => Assert.Equal(15.0, Exo.MoyenneDesValides(new[] { "10", "oups", "20" }), 4);
    [Fact] public void Aucune_valide() => Assert.Equal(0.0, Exo.MoyenneDesValides(new[] { "a", "b" }), 4);
    [Fact] public void Tableau_vide() => Assert.Equal(0.0, Exo.MoyenneDesValides(new string[0]), 4);
    [Fact] public void Une_seule_valide() => Assert.Equal(42.0, Exo.MoyenneDesValides(new[] { "x", "42", "y" }), 4);

    [Fact]
    public void Garde_les_decimales()
    {
        Assert.Equal(1.6666666, Exo.MoyenneDesValides(new[] { "1", "2", "2" }), 4);
    }
}

public class Exercice8_Inventaire
{
    [Fact]
    public void Un_inventaire_neuf_est_vide()
    {
        var sac = new Inventaire(10);
        Assert.Equal(0, sac.Nombre);
        Assert.Equal(10, sac.Capacite);
        Assert.False(sac.EstPlein);
        Assert.Equal(0, sac.ValeurTotale);
    }

    [Fact]
    public void Ajouter_fonctionne()
    {
        var sac = new Inventaire(10);

        Assert.True(sac.Ajouter(new Objet("Épée", 50)));
        Assert.Equal(1, sac.Nombre);
        Assert.Equal(50, sac.ValeurTotale);
    }

    [Fact]
    public void ValeurTotale_additionne_tout()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));
        sac.Ajouter(new Objet("Potion", 10));
        sac.Ajouter(new Objet("Carte", 5));

        Assert.Equal(65, sac.ValeurTotale);
    }

    [Fact]
    public void On_ne_depasse_PAS_la_capacite()
    {
        var sac = new Inventaire(2);
        sac.Ajouter(new Objet("A", 1));
        sac.Ajouter(new Objet("B", 1));

        Assert.True(sac.EstPlein);
        Assert.False(sac.Ajouter(new Objet("C", 1)));
        Assert.Equal(2, sac.Nombre);
    }

    [Fact]
    public void Ajouter_null_est_refuse()
    {
        var sac = new Inventaire(10);
        Assert.False(sac.Ajouter(null));
        Assert.Equal(0, sac.Nombre);
    }

    [Fact]
    public void Contient()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));

        Assert.True(sac.Contient("Épée"));
        Assert.False(sac.Contient("Bouclier"));
        Assert.False(sac.Contient("épée"));    // la casse compte
    }

    [Fact]
    public void Retirer_fonctionne()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));
        sac.Ajouter(new Objet("Potion", 10));

        Assert.True(sac.Retirer("Épée"));
        Assert.Equal(1, sac.Nombre);
        Assert.False(sac.Contient("Épée"));
        Assert.Equal(10, sac.ValeurTotale);
    }

    [Fact]
    public void Retirer_un_objet_absent_retourne_false()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));

        Assert.False(sac.Retirer("Bouclier"));
        Assert.Equal(1, sac.Nombre);
    }

    [Fact]
    public void Retirer_ne_retire_qu_UNE_occurrence()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Potion", 10));
        sac.Ajouter(new Objet("Potion", 10));

        sac.Retirer("Potion");

        Assert.Equal(1, sac.Nombre);
        Assert.True(sac.Contient("Potion"));
    }

    [Fact]
    public void Retirer_libere_de_la_place()
    {
        var sac = new Inventaire(1);
        sac.Ajouter(new Objet("A", 1));
        sac.Retirer("A");

        Assert.False(sac.EstPlein);
        Assert.True(sac.Ajouter(new Objet("B", 1)));
    }

    [Fact]
    public void ObjetLePlusCher()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Potion", 10));
        sac.Ajouter(new Objet("Couronne", 500));
        sac.Ajouter(new Objet("Épée", 50));

        Assert.Equal("Couronne", sac.ObjetLePlusCher().Nom);
    }

    [Fact]
    public void ObjetLePlusCher_sur_un_sac_vide_retourne_null()
    {
        Assert.Null(new Inventaire(10).ObjetLePlusCher());
    }

    [Fact]
    public void Lister_donne_les_noms_dans_l_ordre()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));
        sac.Ajouter(new Objet("Potion", 10));

        Assert.Equal(new List<string> { "Épée", "Potion" }, sac.Lister());
    }

    [Fact]
    public void Lister_retourne_une_COPIE()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));

        List<string> liste = sac.Lister();
        liste.Clear();                 // on saccage la liste reçue

        Assert.Equal(1, sac.Nombre);   // l'inventaire est intact
    }

    [Fact]
    public void ToString_sac_vide()
    {
        Assert.Equal("Sac (0/10) : vide", new Inventaire(10).ToString());
    }

    [Fact]
    public void ToString_avec_objets()
    {
        var sac = new Inventaire(10);
        sac.Ajouter(new Objet("Épée", 50));
        sac.Ajouter(new Objet("Potion", 10));

        Assert.Equal("Sac (2/10) : Épée, Potion", sac.ToString());
    }
}

public class Exercice9_Sauvegarde : IDisposable
{
    // Chaque test utilise son propre fichier temporaire, supprimé après.
    private readonly string _fichier = Path.Combine(
        Path.GetTempPath(), $"test_partie_{Guid.NewGuid()}.json");

    public void Dispose()
    {
        if (File.Exists(_fichier)) File.Delete(_fichier);
    }

    private static Partie PartieExemple() => new Partie
    {
        NomDuHeros = "Kaelis",
        Niveau = 5,
        PointsDeVie = 87,
        Or = 1250,
        Objets = new List<string> { "Épée longue", "Potion" }
    };

    [Fact]
    public void Enregistrer_cree_le_fichier()
    {
        Assert.True(Sauvegarde.Enregistrer(PartieExemple(), _fichier));
        Assert.True(File.Exists(_fichier));
    }

    [Fact]
    public void Enregistrer_ecrit_bien_du_JSON()
    {
        Sauvegarde.Enregistrer(PartieExemple(), _fichier);
        string contenu = File.ReadAllText(_fichier);

        Assert.Contains("Kaelis", contenu);
        Assert.StartsWith("{", contenu.Trim());
    }

    [Fact]
    public void Enregistrer_null_retourne_false()
    {
        Assert.False(Sauvegarde.Enregistrer(null, _fichier));
    }

    [Fact]
    public void Enregistrer_sur_un_chemin_invalide_retourne_false()
    {
        string cheminImpossible = Path.Combine(
            Path.GetTempPath(), "dossier_qui_n_existe_pas_" + Guid.NewGuid(), "x.json");

        Assert.False(Sauvegarde.Enregistrer(PartieExemple(), cheminImpossible));
    }

    [Fact]
    public void Le_grand_test_aller_retour()
    {
        Sauvegarde.Enregistrer(PartieExemple(), _fichier);
        Partie chargee = Sauvegarde.Charger(_fichier);

        Assert.NotNull(chargee);
        Assert.Equal("Kaelis", chargee.NomDuHeros);
        Assert.Equal(5, chargee.Niveau);
        Assert.Equal(87, chargee.PointsDeVie);
        Assert.Equal(1250, chargee.Or);
        Assert.Equal(2, chargee.Objets.Count);
        Assert.Equal("Épée longue", chargee.Objets[0]);
    }

    [Fact]
    public void Charger_un_fichier_inexistant_retourne_null()
    {
        Assert.Null(Sauvegarde.Charger(_fichier));
    }

    [Fact]
    public void Charger_un_fichier_corrompu_retourne_null()
    {
        File.WriteAllText(_fichier, "ceci n'est pas du JSON {{{ ???");
        Assert.Null(Sauvegarde.Charger(_fichier));
    }

    [Fact]
    public void Supprimer_fonctionne()
    {
        Sauvegarde.Enregistrer(PartieExemple(), _fichier);

        Assert.True(Sauvegarde.Supprimer(_fichier));
        Assert.False(File.Exists(_fichier));
    }

    [Fact]
    public void Supprimer_un_fichier_absent_retourne_false()
    {
        Assert.False(Sauvegarde.Supprimer(_fichier));
    }
}

public class Exercice10_Historique
{
    [Fact]
    public void Un_historique_neuf_est_vide()
    {
        var h = new Historique();
        Assert.Equal(0, h.Nombre);
        Assert.True(h.EstVide);
    }

    [Fact]
    public void Faire_memorise()
    {
        var h = new Historique();
        h.Faire("ecrire");
        h.Faire("colorer");

        Assert.Equal(2, h.Nombre);
        Assert.False(h.EstVide);
    }

    [Fact]
    public void Faire_ignore_le_vide()
    {
        var h = new Historique();
        h.Faire(null);
        h.Faire("");
        Assert.Equal(0, h.Nombre);
    }

    [Fact]
    public void Annuler_retire_la_DERNIERE_action()
    {
        var h = new Historique();
        h.Faire("ecrire");
        h.Faire("colorer");

        Assert.Equal("colorer", h.Annuler());    // la derniere d'abord !
        Assert.Equal("ecrire", h.Annuler());
        Assert.Equal(0, h.Nombre);
    }

    [Fact]
    public void Annuler_sur_un_historique_vide_rend_null()
    {
        Assert.Null(new Historique().Annuler());
    }

    [Fact]
    public void Derniere_ne_retire_PAS()
    {
        var h = new Historique();
        h.Faire("ecrire");
        h.Faire("colorer");

        Assert.Equal("colorer", h.Derniere());
        Assert.Equal("colorer", h.Derniere());   // toujours la !
        Assert.Equal(2, h.Nombre);
    }

    [Fact]
    public void Derniere_sur_un_historique_vide_rend_null()
    {
        Assert.Null(new Historique().Derniere());
    }

    [Fact]
    public void ToutAnnuler_vide_la_pile_dans_le_bon_ordre()
    {
        var h = new Historique();
        h.Faire("a");
        h.Faire("b");
        h.Faire("c");

        Assert.Equal(new List<string> { "c", "b", "a" }, h.ToutAnnuler());
        Assert.Equal(0, h.Nombre);
        Assert.True(h.EstVide);
    }

    [Fact]
    public void ToutAnnuler_sur_un_historique_vide()
    {
        Assert.Empty(new Historique().ToutAnnuler());
    }
}

public class Exercice11_Servir
{
    [Fact]
    public void Sert_dans_l_ordre_d_arrivee()
    {
        var restants = Exo.Servir(new[] { "Thorin", "Elyra", "Sylas" }, 1);
        Assert.Equal(new List<string> { "Elyra", "Sylas" }, restants);
    }

    [Fact]
    public void Servir_personne()
    {
        var restants = Exo.Servir(new[] { "Thorin", "Elyra" }, 0);
        Assert.Equal(new List<string> { "Thorin", "Elyra" }, restants);
    }

    [Fact]
    public void Servir_tout_le_monde()
    {
        Assert.Empty(Exo.Servir(new[] { "Thorin", "Elyra" }, 2));
    }

    [Fact]
    public void Servir_plus_que_le_nombre_de_personnes_ne_plante_pas()
    {
        Assert.Empty(Exo.Servir(new[] { "Thorin", "Elyra" }, 99));
    }

    [Fact]
    public void File_vide()
    {
        Assert.Empty(Exo.Servir(new string[0], 3));
    }
}

public class Exercice12_AuMoinsUnDoublon
{
    [Theory]
    [InlineData(new[] { "a", "b", "a" }, true)]
    [InlineData(new[] { "a", "a" }, true)]
    [InlineData(new[] { "a", "b", "c" }, false)]
    [InlineData(new[] { "a" }, false)]
    [InlineData(new string[0], false)]
    public void Detecte_les_doublons(string[] elements, bool attendu)
    {
        Assert.Equal(attendu, Exo.AuMoinsUnDoublon(elements));
    }

    [Fact]
    public void La_casse_compte()
    {
        Assert.False(Exo.AuMoinsUnDoublon(new[] { "Epee", "epee" }));
    }
}

public class Exercice13_SallesDifferentes
{
    [Theory]
    [InlineData(new[] { "A", "B", "A", "C", "B" }, 3)]
    [InlineData(new[] { "A", "A", "A" }, 1)]
    [InlineData(new[] { "A", "B", "C" }, 3)]
    [InlineData(new string[0], 0)]
    public void Compte_les_salles_distinctes(string[] salles, int attendu)
    {
        Assert.Equal(attendu, Exo.SallesDifferentes(salles));
    }
}
