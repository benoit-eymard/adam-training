using Module01;
using Xunit;

namespace Module01.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Ces tests vérifient tes exercices automatiquement.
//  Lance-les avec :  dotnet test Tests
//
//  🔴 Rouge = pas encore bon. Lis le message, il dit ce qui cloche.
//  🟢 Vert  = c'est gagné, passe à la suite !
//
//  Tu peux lire ce fichier, mais ne le modifie pas : c'est l'arbitre.
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_SePresenter
{
    [Fact]
    public void Adam_13ans()
    {
        Assert.Equal("Je m'appelle Adam et j'ai 13 ans.", Exo.SePresenter("Adam", 13));
    }

    [Fact]
    public void Kaelis_27ans()
    {
        Assert.Equal("Je m'appelle Kaelis et j'ai 27 ans.", Exo.SePresenter("Kaelis", 27));
    }
}

public class Exercice2_Calculatrice
{
    [Theory]
    [InlineData(7, 3, 10)]
    [InlineData(0, 0, 0)]
    [InlineData(-5, 5, 0)]
    [InlineData(100, 250, 350)]
    public void Additionner_fonctionne(int a, int b, int attendu)
    {
        Assert.Equal(attendu, Exo.Additionner(a, b));
    }

    [Fact]
    public void Moyenne_de_12_15_18_vaut_15()
    {
        Assert.Equal(15.0, Exo.Moyenne(12, 15, 18), precision: 4);
    }

    [Fact]
    public void Moyenne_garde_les_decimales()
    {
        // Si tu obtiens 1 au lieu de 1.6666, c'est le piège de la
        // division entière : divise par 3.0 et non par 3 !
        Assert.Equal(1.6666666, Exo.Moyenne(1, 2, 2), precision: 4);
    }
}

public class Exercice3_PointsDeVie
{
    [Fact]
    public void Degats_normaux()
    {
        Assert.Equal(70, Exo.PointsDeVieRestants(100, 30));
    }

    [Fact]
    public void Degats_exactement_mortels()
    {
        Assert.Equal(0, Exo.PointsDeVieRestants(100, 100));
    }

    [Fact]
    public void Les_PV_ne_deviennent_jamais_negatifs()
    {
        Assert.Equal(0, Exo.PointsDeVieRestants(100, 150));
    }

    [Fact]
    public void Aucun_degat()
    {
        Assert.Equal(50, Exo.PointsDeVieRestants(50, 0));
    }
}

public class Exercice4_FicheDePersonnage
{
    [Fact]
    public void Fiche_de_Kaelis()
    {
        string attendu = "Nom    : Kaelis\nClasse : Mage\nNiveau : 5";
        Assert.Equal(attendu, Exo.FicheDePersonnage("Kaelis", "Mage", 5));
    }

    [Fact]
    public void Fiche_de_Thorin()
    {
        string attendu = "Nom    : Thorin\nClasse : Guerrier\nNiveau : 12";
        Assert.Equal(attendu, Exo.FicheDePersonnage("Thorin", "Guerrier", 12));
    }
}

public class Exercice5_ConvertirEnOr
{
    [Theory]
    [InlineData(12345, "1 or, 23 argent, 45 cuivre")]
    [InlineData(250, "0 or, 2 argent, 50 cuivre")]
    [InlineData(7, "0 or, 0 argent, 7 cuivre")]
    [InlineData(0, "0 or, 0 argent, 0 cuivre")]
    [InlineData(10000, "1 or, 0 argent, 0 cuivre")]
    [InlineData(999999, "99 or, 99 argent, 99 cuivre")]
    public void Conversion_correcte(int cuivre, string attendu)
    {
        Assert.Equal(attendu, Exo.ConvertirEnOr(cuivre));
    }
}
