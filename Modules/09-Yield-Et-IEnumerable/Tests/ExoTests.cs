using Module09;
using Xunit;

// L'Espion compte les lectures dans une variable static : les tests
// doivent donc s'executer un par un, pas en parallele.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Module09.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 9.  Lance-les avec :  dotnet test Tests
//
//  ⚠️ Certains tests ne verifient PAS le resultat, mais le NOMBRE
//     D'ELEMENTS LUS. Ce sont eux qui prouvent que tes methodes sont
//     reellement paresseuses. Une version avec List les fera echouer.
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_MonWhere
{
    [Fact]
    public void Filtre_les_pairs()
    {
        Assert.Equal(new[] { 2, 4 },
            new[] { 1, 2, 3, 4 }.MonWhere(n => n % 2 == 0).ToArray());
    }

    [Fact]
    public void Aucun_element_ne_passe()
    {
        Assert.Empty(new[] { 1, 3, 5 }.MonWhere(n => n % 2 == 0));
    }

    [Fact]
    public void Tous_les_elements_passent()
    {
        Assert.Equal(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }.MonWhere(n => true).ToArray());
    }

    [Fact]
    public void Source_vide()
    {
        Assert.Empty(new int[0].MonWhere(n => true));
    }

    [Fact]
    public void Marche_aussi_sur_des_string()
    {
        Assert.Equal(new[] { "Elyra", "Kaelis" },
            new[] { "Thorin", "Elyra", "Kaelis" }.MonWhere(n => n.Length == 5 || n.Length == 6)
                .MonWhere(n => n.Contains("a") && !n.Contains("h")).ToArray());
    }
}

public class Exercice2_MonSelect
{
    [Fact]
    public void Transforme_chaque_element()
    {
        Assert.Equal(new[] { 10, 20, 30 },
            new[] { 1, 2, 3 }.MonSelect(n => n * 10).ToArray());
    }

    [Fact]
    public void Peut_CHANGER_de_type()
    {
        Assert.Equal(new[] { "1", "2", "3" },
            new[] { 1, 2, 3 }.MonSelect(n => n.ToString()).ToArray());
    }

    [Fact]
    public void Garde_le_meme_nombre_d_elements()
    {
        Assert.Equal(4, new[] { 1, 2, 3, 4 }.MonSelect(n => n * 2).MonCount());
    }

    [Fact]
    public void Source_vide()
    {
        Assert.Empty(new int[0].MonSelect(n => n * 2));
    }
}

public class Exercice3_MonTake
{
    [Fact]
    public void Prend_les_premiers()
    {
        Assert.Equal(new[] { 1, 2 }, new[] { 1, 2, 3, 4, 5 }.MonTake(2).ToArray());
    }

    [Fact]
    public void Prendre_plus_que_disponible_ne_plante_pas()
    {
        Assert.Equal(new[] { 1, 2 }, new[] { 1, 2 }.MonTake(10).ToArray());
    }

    [Fact]
    public void Prendre_zero()
    {
        Assert.Empty(new[] { 1, 2, 3 }.MonTake(0));
    }

    [Fact]
    public void Prendre_sur_une_source_vide()
    {
        Assert.Empty(new int[0].MonTake(5));
    }
}

// ═══════════════════════════════════════════════════════════════════
//  ⭐ LES TESTS QUI COMPTENT VRAIMENT : LA PARESSE
// ═══════════════════════════════════════════════════════════════════

public class LaPreuveDeLaParesse
{
    [Fact]
    public void Appeler_MonWhere_ne_lit_RIEN()
    {
        Espion.Reinitialiser();

        var requete = Espion.Nombres(1000).MonWhere(n => n % 2 == 0);

        // On a appele la methode... et pas un seul element n'a ete lu.
        Assert.Equal(0, Espion.NombreDeLectures);
    }

    [Fact]
    public void Appeler_MonSelect_ne_lit_RIEN()
    {
        Espion.Reinitialiser();

        var requete = Espion.Nombres(1000).MonSelect(n => n * 2);

        Assert.Equal(0, Espion.NombreDeLectures);
    }

    [Fact]
    public void MonTake_ne_lit_QUE_ce_qu_il_faut()
    {
        Espion.Reinitialiser();

        var resultat = Espion.Nombres(1000).MonTake(3).ToList();

        Assert.Equal(new[] { 0, 1, 2 }, resultat);
        Assert.Equal(3, Espion.NombreDeLectures);   // 3, pas 1000 !
    }

    [Fact]
    public void Une_chaine_complete_reste_paresseuse()
    {
        Espion.Reinitialiser();

        var resultat = Espion.Nombres(1000)
            .MonWhere(n => n % 2 == 0)
            .MonSelect(n => n * 10)
            .MonTake(3)
            .ToList();

        Assert.Equal(new[] { 0, 20, 40 }, resultat);

        // Pour sortir 3 nombres pairs, il faut lire 0,1,2,3,4 = 5 elements.
        // Surtout PAS les 1000.
        Assert.Equal(5, Espion.NombreDeLectures);
    }

    [Fact]
    public void Sans_consommation_rien_ne_se_calcule_meme_en_chaine()
    {
        Espion.Reinitialiser();

        var requete = Espion.Nombres(1000)
            .MonWhere(n => n % 2 == 0)
            .MonSelect(n => n * 10)
            .MonTake(3);

        Assert.Equal(0, Espion.NombreDeLectures);

        requete.ToList();                            // MAINTENANT ca calcule
        Assert.Equal(5, Espion.NombreDeLectures);
    }
}

public class Exercice4_MonCount
{
    [Fact] public void Compte() => Assert.Equal(3, new[] { 1, 2, 3 }.MonCount());
    [Fact] public void Compte_vide() => Assert.Equal(0, new int[0].MonCount());
    [Fact] public void Compte_un_seul() => Assert.Equal(1, new[] { 42 }.MonCount());

    [Fact]
    public void Compte_apres_un_filtre()
    {
        Assert.Equal(2, new[] { 1, 2, 3, 4 }.MonWhere(n => n % 2 == 0).MonCount());
    }

    [Fact]
    public void MonCount_lui_LIT_tout_et_c_est_normal()
    {
        Espion.Reinitialiser();
        Espion.Nombres(10).MonCount();

        // Pour compter, il FAUT tout parcourir. C'est une operation
        // terminale : elle ne peut pas etre paresseuse.
        Assert.Equal(10, Espion.NombreDeLectures);
    }
}

public class Exercice5_MonAggregate
{
    [Fact]
    public void Somme()
    {
        Assert.Equal(8, new[] { 3, 1, 4 }.MonAggregate(0, (t, n) => t + n));
    }

    [Fact]
    public void Produit()
    {
        Assert.Equal(12, new[] { 3, 1, 4 }.MonAggregate(1, (t, n) => t * n));
    }

    [Fact]
    public void Source_vide_rend_le_depart()
    {
        Assert.Equal(42, new int[0].MonAggregate(42, (t, n) => t + n));
    }

    [Fact]
    public void Maximum()
    {
        Assert.Equal(9, new[] { 3, 9, 5 }.MonAggregate(int.MinValue, (max, n) => n > max ? n : max));
    }

    [Fact]
    public void Marche_sur_des_string()
    {
        Assert.Equal("abc", new[] { "a", "b", "c" }.MonAggregate("", (t, s) => t + s));
    }

    [Fact]
    public void On_peut_reconstruire_MonCount_avec()
    {
        Assert.Equal(4, new[] { 7, 7, 7, 7 }.MonAggregate(0, (t, n) => t + 1));
    }
}

public class Exercice6_Compter
{
    [Fact] public void De_1_a_5() => Assert.Equal(new[] { 1, 2, 3, 4, 5 }, Generateurs.Compter(1, 5).ToArray());
    [Fact] public void Un_seul() => Assert.Equal(new[] { 3 }, Generateurs.Compter(3, 3).ToArray());
    [Fact] public void Bornes_inversees() => Assert.Empty(Generateurs.Compter(5, 1));
    [Fact] public void Negatifs() => Assert.Equal(new[] { -2, -1, 0, 1 }, Generateurs.Compter(-2, 1).ToArray());
}

public class Exercice7_Repeter
{
    [Fact]
    public void Repete_une_string()
    {
        Assert.Equal(new[] { "ha", "ha", "ha" }, Generateurs.Repeter("ha", 3).ToArray());
    }

    [Fact]
    public void Repete_zero_fois()
    {
        Assert.Empty(Generateurs.Repeter("ha", 0));
    }

    [Fact]
    public void Marche_avec_n_importe_quel_type()
    {
        Assert.Equal(new[] { 7, 7 }, Generateurs.Repeter(7, 2).ToArray());
    }
}

// ═══════════════════════════════════════════════════════════════════
//  ⭐ L'INFINI
//
//  Ces tests ne terminent QUE si tes generateurs sont paresseux.
//  C'est la demonstration la plus spectaculaire du module.
// ═══════════════════════════════════════════════════════════════════

public class Exercice8_Naturels
{
    [Fact]
    public void Les_cinq_premiers()
    {
        Assert.Equal(new[] { 0, 1, 2, 3, 4 }, Generateurs.Naturels().MonTake(5).ToArray());
    }

    [Fact]
    public void On_peut_filtrer_une_sequence_INFINIE()
    {
        // Les 3 premiers multiples de 7. Sur une sequence sans fin.
        Assert.Equal(new[] { 0, 7, 14 },
            Generateurs.Naturels().MonWhere(n => n % 7 == 0).MonTake(3).ToArray());
    }

    [Fact]
    public void On_peut_aussi_la_transformer()
    {
        Assert.Equal(new[] { 0, 100, 200 },
            Generateurs.Naturels().MonSelect(n => n * 100).MonTake(3).ToArray());
    }

    [Fact]
    public void Le_1000e_naturel_ne_coute_presque_rien()
    {
        Assert.Equal(999, Generateurs.Naturels().MonTake(1000).Last());
    }
}

public class Exercice9_Fibonacci
{
    [Fact]
    public void Les_dix_premiers()
    {
        Assert.Equal(new long[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 },
            Generateurs.Fibonacci().MonTake(10).ToArray());
    }

    [Fact]
    public void Chaque_terme_est_la_somme_des_deux_precedents()
    {
        long[] suite = Generateurs.Fibonacci().MonTake(20).ToArray();
        for (int i = 2; i < suite.Length; i++)
        {
            Assert.Equal(suite[i - 1] + suite[i - 2], suite[i]);
        }
    }

    [Fact]
    public void Le_50e_est_INSTANTANE()
    {
        // La version recursive du module 3 mettrait des JOURS.
        Assert.Equal(7778742049L, Generateurs.Fibonacci().MonTake(50).Last());
    }

    [Fact]
    public void Les_premiers_pairs()
    {
        Assert.Equal(new long[] { 0, 2, 8, 34, 144 },
            Generateurs.Fibonacci().MonWhere(n => n % 2 == 0).MonTake(5).ToArray());
    }

    [Fact]
    public void Le_premier_qui_depasse_mille()
    {
        Assert.Equal(1597L, Generateurs.Fibonacci().MonWhere(n => n > 1000).MonTake(1).Single());
    }
}
