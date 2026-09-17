using Module03;
using Xunit;

namespace Module03.Tests;

// ═══════════════════════════════════════════════════════════════════
//  Tests du module 3.  Lance-les avec :  dotnet test Tests
// ═══════════════════════════════════════════════════════════════════

public class Exercice1_Somme
{
    [Fact] public void Somme_simple() => Assert.Equal(6, Exo.Somme(new[] { 1, 2, 3 }));
    [Fact] public void Somme_avec_negatifs() => Assert.Equal(5, Exo.Somme(new[] { 10, -5 }));
    [Fact] public void Somme_un_seul() => Assert.Equal(42, Exo.Somme(new[] { 42 }));
    [Fact] public void Somme_tableau_vide() => Assert.Equal(0, Exo.Somme(new int[0]));
    [Fact] public void Somme_grande() => Assert.Equal(5050, Exo.Somme(Enumerable.Range(1, 100).ToArray()));
}

public class Exercice2_Maximum
{
    [Fact] public void Max_simple() => Assert.Equal(9, Exo.Maximum(new[] { 3, 9, 5 }));
    [Fact] public void Max_en_premier() => Assert.Equal(100, Exo.Maximum(new[] { 100, 9, 5 }));
    [Fact] public void Max_en_dernier() => Assert.Equal(100, Exo.Maximum(new[] { 3, 9, 100 }));
    [Fact] public void Max_un_seul() => Assert.Equal(42, Exo.Maximum(new[] { 42 }));

    [Fact]
    public void Max_que_des_negatifs()
    {
        // Si tu obtiens 0, c'est que tu es parti de 0 au lieu de nombres[0] !
        Assert.Equal(-2, Exo.Maximum(new[] { -5, -2, -9 }));
    }

    [Fact] public void Max_valeurs_identiques() => Assert.Equal(7, Exo.Maximum(new[] { 7, 7, 7 }));
}

public class Exercice3_Moyenne
{
    [Fact] public void Moyenne_entiere() => Assert.Equal(20.0, Exo.Moyenne(new[] { 10, 20, 30 }), 4);
    [Fact] public void Moyenne_decimale() => Assert.Equal(1.5, Exo.Moyenne(new[] { 1, 2 }), 4);
    [Fact] public void Moyenne_tiers() => Assert.Equal(1.6666666, Exo.Moyenne(new[] { 1, 2, 2 }), 4);

    [Fact]
    public void Moyenne_tableau_vide_ne_plante_pas()
    {
        Assert.Equal(0.0, Exo.Moyenne(new int[0]), 4);
    }
}

public class Exercice4_IndexDe
{
    [Fact] public void Trouve_au_milieu() => Assert.Equal(1, Exo.IndexDe(new[] { 10, 20, 30 }, 20));
    [Fact] public void Trouve_au_debut() => Assert.Equal(0, Exo.IndexDe(new[] { 10, 20, 30 }, 10));
    [Fact] public void Trouve_a_la_fin() => Assert.Equal(2, Exo.IndexDe(new[] { 10, 20, 30 }, 30));
    [Fact] public void Absent_retourne_moins_un() => Assert.Equal(-1, Exo.IndexDe(new[] { 10, 20, 30 }, 99));
    [Fact] public void Tableau_vide_retourne_moins_un() => Assert.Equal(-1, Exo.IndexDe(new int[0], 1));

    [Fact]
    public void Retourne_la_PREMIERE_occurrence()
    {
        Assert.Equal(0, Exo.IndexDe(new[] { 5, 5, 5 }, 5));
    }
}

public class Exercice5_Inverser
{
    [Fact] public void Inverser_impair() => Assert.Equal(new[] { 3, 2, 1 }, Exo.Inverser(new[] { 1, 2, 3 }));
    [Fact] public void Inverser_pair() => Assert.Equal(new[] { 4, 3, 2, 1 }, Exo.Inverser(new[] { 1, 2, 3, 4 }));
    [Fact] public void Inverser_un_seul() => Assert.Equal(new[] { 7 }, Exo.Inverser(new[] { 7 }));
    [Fact] public void Inverser_vide() => Assert.Equal(new int[0], Exo.Inverser(new int[0]));

    [Fact]
    public void L_original_n_est_PAS_modifie()
    {
        int[] original = { 1, 2, 3 };
        Exo.Inverser(original);
        Assert.Equal(new[] { 1, 2, 3 }, original);
    }
}

public class Exercice6_Trier
{
    [Fact] public void Tri_simple() => Assert.Equal(new[] { 1, 2, 3 }, Exo.Trier(new[] { 3, 1, 2 }));
    [Fact] public void Tri_decroissant() => Assert.Equal(new[] { 2, 3, 4, 5 }, Exo.Trier(new[] { 5, 4, 3, 2 }));
    [Fact] public void Tri_deja_trie() => Assert.Equal(new[] { 1, 2, 3 }, Exo.Trier(new[] { 1, 2, 3 }));
    [Fact] public void Tri_un_seul() => Assert.Equal(new[] { 1 }, Exo.Trier(new[] { 1 }));
    [Fact] public void Tri_vide() => Assert.Equal(new int[0], Exo.Trier(new int[0]));
    [Fact] public void Tri_avec_doublons() => Assert.Equal(new[] { 1, 2, 2, 3 }, Exo.Trier(new[] { 2, 3, 1, 2 }));
    [Fact] public void Tri_avec_negatifs() => Assert.Equal(new[] { -9, -5, 0, 3 }, Exo.Trier(new[] { 3, -5, 0, -9 }));

    [Fact]
    public void Tri_long()
    {
        int[] melange = { 42, 7, 19, 3, 88, 1, 56, 23, 99, 12 };
        int[] attendu = { 1, 3, 7, 12, 19, 23, 42, 56, 88, 99 };
        Assert.Equal(attendu, Exo.Trier(melange));
    }

    [Fact]
    public void L_original_n_est_PAS_modifie()
    {
        int[] original = { 3, 1, 2 };
        Exo.Trier(original);
        Assert.Equal(new[] { 3, 1, 2 }, original);
    }
}

public class Exercice7_EstPalindrome
{
    [Theory]
    [InlineData("kayak", true)]
    [InlineData("radar", true)]
    [InlineData("Kayak", true)]           // casse ignorée
    [InlineData("RADAR", true)]
    [InlineData("bonjour", false)]
    [InlineData("engage le jeu", false)]
    [InlineData("esope reste ici et se repose", true)]  // espaces ignorés
    [InlineData("a", true)]
    [InlineData("", true)]
    [InlineData("ab", false)]
    [InlineData("aa", true)]
    [InlineData("abcba", true)]
    [InlineData("abccba", true)]
    [InlineData("abcdba", false)]
    public void Detecte_les_palindromes(string texte, bool attendu)
    {
        Assert.Equal(attendu, Exo.EstPalindrome(texte));
    }
}

public class Exercice8_Factorielle
{
    [Theory]
    [InlineData(0, 1L)]          // 0! vaut 1 par convention
    [InlineData(1, 1L)]
    [InlineData(2, 2L)]
    [InlineData(5, 120L)]
    [InlineData(10, 3628800L)]
    [InlineData(20, 2432902008176640000L)]   // d'ou le "long" !
    public void Factorielle_correcte(int n, long attendu)
    {
        Assert.Equal(attendu, Exo.Factorielle(n));
    }
}

public class Exercice9_Fibonacci
{
    [Theory]
    [InlineData(0, 0L)]
    [InlineData(1, 1L)]
    [InlineData(2, 1L)]
    [InlineData(3, 2L)]
    [InlineData(7, 13L)]
    [InlineData(10, 55L)]
    [InlineData(20, 6765L)]
    public void Fibonacci_correct(int n, long attendu)
    {
        Assert.Equal(attendu, Exo.Fibonacci(n));
    }

    [Fact]
    public void Chaque_terme_est_la_somme_des_deux_precedents()
    {
        for (int n = 2; n <= 15; n++)
        {
            Assert.Equal(Exo.Fibonacci(n - 1) + Exo.Fibonacci(n - 2), Exo.Fibonacci(n));
        }
    }
}

public class Exercice10_SommeDesChiffres
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(7, 7)]
    [InlineData(10, 1)]
    [InlineData(123, 6)]
    [InlineData(9999, 36)]
    [InlineData(1000000, 1)]
    public void Somme_correcte(int n, int attendu)
    {
        Assert.Equal(attendu, Exo.SommeDesChiffres(n));
    }
}

public class Exercice11_InverserTexte
{
    [Theory]
    [InlineData("", "")]
    [InlineData("a", "a")]
    [InlineData("ab", "ba")]
    [InlineData("abc", "cba")]
    [InlineData("Kaelis", "sileaK")]
    [InlineData("kayak", "kayak")]      // un palindrome, forcement
    public void Inverse_correctement(string texte, string attendu)
    {
        Assert.Equal(attendu, Exo.InverserTexte(texte));
    }

    [Fact]
    public void Inverser_deux_fois_redonne_l_original()
    {
        Assert.Equal("Bonjour", Exo.InverserTexte(Exo.InverserTexte("Bonjour")));
    }
}
