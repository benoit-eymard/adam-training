using Module02;
using Xunit;

namespace Module02.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 2.  Lance-les avec :  dotnet test Tests
//  Tu peux les lire, mais ne les modifie pas : c'est l'arbitre.
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_EstMajeur
{
    [Theory]
    [InlineData(20, true)]
    [InlineData(18, true)]   // 18 ans pile : majeur !
    [InlineData(17, false)]
    [InlineData(13, false)]
    [InlineData(0, false)]
    public void Verifie_la_majorite(int age, bool attendu)
    {
        Assert.Equal(attendu, Exo.EstMajeur(age));
    }
}

public class Exercice2_LePlusGrand
{
    [Theory]
    [InlineData(3, 9, 5, 9)]
    [InlineData(10, 2, 2, 10)]
    [InlineData(1, 2, 3, 3)]
    [InlineData(4, 4, 4, 4)]
    [InlineData(-5, -2, -9, -2)]
    public void Trouve_le_maximum(int a, int b, int c, int attendu)
    {
        Assert.Equal(attendu, Exo.LePlusGrand(a, b, c));
    }
}

public class Exercice3_NoteEnLettre
{
    [Theory]
    [InlineData(20, "A")]
    [InlineData(16, "A")]   // limite basse du A
    [InlineData(15.9, "B")]
    [InlineData(14, "B")]
    [InlineData(13, "C")]
    [InlineData(12, "C")]
    [InlineData(11, "D")]
    [InlineData(10, "D")]
    [InlineData(9, "E")]
    [InlineData(8, "E")]
    [InlineData(7.9, "F")]
    [InlineData(0, "F")]
    public void Convertit_correctement(double note, string attendu)
    {
        Assert.Equal(attendu, Exo.NoteEnLettre(note));
    }
}

public class Exercice4_CompteARebours
{
    [Theory]
    [InlineData(5, "5, 4, 3, 2, 1, Décollage !")]
    [InlineData(3, "3, 2, 1, Décollage !")]
    [InlineData(1, "1, Décollage !")]
    [InlineData(0, "Décollage !")]
    [InlineData(10, "10, 9, 8, 7, 6, 5, 4, 3, 2, 1, Décollage !")]
    public void Compte_a_rebours_correct(int depart, string attendu)
    {
        Assert.Equal(attendu, Exo.CompteARebours(depart));
    }
}

public class Exercice5_TableDeMultiplication
{
    [Fact]
    public void Table_de_3()
    {
        string attendu =
            "3 x 1 = 3\n" +
            "3 x 2 = 6\n" +
            "3 x 3 = 9\n" +
            "3 x 4 = 12\n" +
            "3 x 5 = 15\n" +
            "3 x 6 = 18\n" +
            "3 x 7 = 21\n" +
            "3 x 8 = 24\n" +
            "3 x 9 = 27\n" +
            "3 x 10 = 30";

        Assert.Equal(attendu, Exo.TableDeMultiplication(3));
    }

    [Fact]
    public void Pas_de_retour_a_la_ligne_a_la_fin()
    {
        string table = Exo.TableDeMultiplication(7);
        Assert.False(table.EndsWith("\n"), "Il y a un \\n en trop à la fin !");
        Assert.EndsWith("7 x 10 = 70", table);
    }

    [Fact]
    public void Il_y_a_bien_10_lignes()
    {
        Assert.Equal(10, Exo.TableDeMultiplication(5).Split('\n').Length);
    }
}

public class Exercice6_FizzBuzz
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(2, "2")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(6, "Fizz")]
    [InlineData(7, "7")]
    [InlineData(10, "Buzz")]
    [InlineData(15, "FizzBuzz")]   // le cas qui piège tout le monde
    [InlineData(30, "FizzBuzz")]
    [InlineData(45, "FizzBuzz")]
    [InlineData(98, "98")]
    public void FizzBuzz_correct(int nombre, string attendu)
    {
        Assert.Equal(attendu, Exo.FizzBuzz(nombre));
    }
}

public class Exercice7_BarreDeVie
{
    [Theory]
    [InlineData(100, 100, "[##########] 100/100")]
    [InlineData(50, 100, "[#####-----] 50/100")]
    [InlineData(45, 100, "[####------] 45/100")]
    [InlineData(10, 100, "[#---------] 10/100")]
    [InlineData(0, 100, "[----------] 0/100")]
    [InlineData(30, 60, "[#####-----] 30/60")]
    [InlineData(1, 3, "[###-------] 1/3")]
    public void Dessine_la_barre(int pv, int pvMax, string attendu)
    {
        Assert.Equal(attendu, Exo.BarreDeVie(pv, pvMax));
    }
}
