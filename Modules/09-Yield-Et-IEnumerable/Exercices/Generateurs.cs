namespace Module09;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 9 — LES GÉNÉRATEURS
///
///  Ici tu ne transformes plus une séquence : tu en FABRIQUES une,
///  à partir de rien.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Generateurs
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 6 ⭐ — Compter
    //
    //  Produit tous les entiers de `debut` à `fin` INCLUS.
    //
    //      Compter(1, 5)   → 1, 2, 3, 4, 5
    //      Compter(3, 3)   → 3
    //      Compter(5, 1)   → rien du tout (pas d'erreur !)
    //
    //  💡 Une boucle for et un yield return. C'est tout.
    //  💡 Si debut > fin, la boucle ne tourne pas : le cas se règle
    //     tout seul, aucun if à écrire. 🎁
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<int> Compter(int debut, int fin)
    {
        // TODO:
        yield break;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 7 ⭐⭐ — Repeter
    //
    //  Produit la même valeur `combien` fois.
    //
    //      Repeter("ha", 3)   → "ha", "ha", "ha"
    //      Repeter("ha", 0)   → rien
    //
    //  💡 Générique : ça doit marcher avec des string, des int, des
    //     Personnage... d'où le <T>.
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<T> Repeter<T>(T valeur, int combien)
    {
        // TODO:
        yield break;
    }


    // ═══════════════════════════════════════════════════════════
    //  ⚠️⚠️  LES DEUX SUIVANTS SONT INFINIS  ⚠️⚠️
    //
    //  Oui, tu vas écrire `while (true)`. Oui, c'est voulu.
    //
    //  Ça ne plante pas, parce que yield return MET LA MÉTHODE EN
    //  PAUSE : elle ne produit un élément que si on le lui demande.
    //
    //  🔴 MAIS : ne fais JAMAIS .ToList() dessus, ni .Count().
    //     Le programme tournerait pour l'éternité et saturerait la
    //     mémoire. Il faut TOUJOURS borner avec Take ou First.
    //
    //  ⚠️ Si tu essaies de construire une List à l'intérieur, ton
    //     programme se figera. C'est justement ce que yield évite.
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 8 ⭐⭐⭐ — Naturels
    //
    //  Produit 0, 1, 2, 3, ... à l'infini.
    //
    //      Naturels().MonTake(5)   → 0, 1, 2, 3, 4
    //
    //  💡 int n = 0;
    //     while (true) { yield return n; n++; }
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<int> Naturels()
    {
        // TODO:
        yield break;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 9 ⭐⭐⭐ — Fibonacci, la version qui ne rame pas
    //
    //  Produit 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, ... à l'infini.
    //
    //      Fibonacci().MonTake(5)   → 0, 1, 1, 2, 3
    //
    //  🧠 Compare avec la version RÉCURSIVE du module 3 : elle
    //     faisait 40 milliards d'appels pour n = 50, parce qu'elle
    //     recalculait sans arrêt les mêmes valeurs.
    //     Ici, chaque nombre est calculé UNE SEULE FOIS. Fibonacci()
    //     .MonTake(50) est instantané.
    //
    //  💡 Deux variables qui avancent ensemble :
    //       long a = 0, b = 1;
    //       while (true)
    //       {
    //           yield return a;
    //           (a, b) = (b, a + b);
    //       }
    //
    //  💡 (a, b) = (b, a + b) est une affectation de TUPLE : la
    //     droite est entièrement calculée AVANT d'être rangée. Plus
    //     besoin de variable temporaire (souviens-toi de l'échange
    //     du module 3 !).
    //
    //  ⚠️ Type long, pas int : Fibonacci grandit très vite.
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<long> Fibonacci()
    {
        // TODO:
        yield break;
    }
}
