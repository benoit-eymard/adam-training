namespace Module09;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 9 — RÉÉCRIS LINQ TOI-MÊME
///
///  Cette classe est `static` : c'est obligatoire pour héberger des
///  méthodes d'extension.
///
///  Le `this` devant le premier paramètre est ce qui permet
///  d'écrire :
///        nombres.MonWhere(n => n > 5)
///  au lieu de :
///        MonLinq.MonWhere(nombres, n => n > 5)
///
///  ⚠️ Les 3 premières méthodes DOIVENT utiliser yield return.
///     Des tests vérifient qu'elles sont réellement PARESSEUSES :
///     si tu construis une List à l'intérieur, ils échoueront.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class MonLinq
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐⭐ — MonWhere  (le « filter »)
    //
    //  Ne garde que les éléments qui vérifient le critère.
    //
    //      new[] { 1, 2, 3, 4 }.MonWhere(n => n % 2 == 0)   → 2, 4
    //
    //  💡 Le squelette :
    //       foreach (T element in source)
    //       {
    //           if (critere(element))
    //           {
    //               yield return element;
    //           }
    //       }
    //
    //  ⚠️ PAS de List, PAS de return classique. Uniquement
    //     yield return — sinon le test de paresse te grillera.
    //
    //  ⚠️ Comme la méthode contient un yield, le `return` ci-dessous
    //     doit DISPARAÎTRE : on ne peut pas mélanger les deux.
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<T> MonWhere<T>(this IEnumerable<T> source, Func<T, bool> critere)
    {
        // TODO: remplace tout le corps par une boucle avec yield return
        yield break;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐⭐ — MonSelect  (le « map »)
    //
    //  Transforme chaque élément.
    //
    //      new[] { 1, 2, 3 }.MonSelect(n => n * 10)     → 10, 20, 30
    //      heros.MonSelect(h => h.Nom)                  → des string
    //
    //  ⚠️ Remarque les DEUX types génériques :
    //       TSource  = ce qui entre
    //       TResultat = ce qui sort
    //     C'est ce qui permet de changer de type en chemin.
    //
    //  💡 Comme MonWhere, mais sans condition : on transforme tout.
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<TResultat> MonSelect<TSource, TResultat>(
        this IEnumerable<TSource> source, Func<TSource, TResultat> transformation)
    {
        // TODO:
        yield break;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐⭐ — MonTake
    //
    //  Ne garde que les `nombre` premiers éléments.
    //
    //      new[] { 1, 2, 3, 4, 5 }.MonTake(2)   → 1, 2
    //      new[] { 1, 2 }.MonTake(10)           → 1, 2   (pas d'erreur)
    //      new[] { 1, 2 }.MonTake(0)            → rien
    //
    //  ⚠️ C'EST LA MÉTHODE LA PLUS IMPORTANTE DU MODULE.
    //     Elle doit s'ARRÊTER dès qu'elle a son compte, sans lire un
    //     élément de plus. C'est ce qui rend les séquences infinies
    //     utilisables — et c'est ce que l'Espion va vérifier.
    //
    //  💡 Un compteur, et `yield break;` dès qu'il est atteint :
    //       if (nombre <= 0) yield break;
    //       int pris = 0;
    //       foreach (T element in source)
    //       {
    //           yield return element;
    //           pris++;
    //           if (pris >= nombre) yield break;
    //       }
    //
    //  🔴 L'ORDRE EST SUBTIL — et c'est tout l'intérêt de l'exercice.
    //     Mets le test APRÈS le yield return, jamais avant.
    //
    //     Si tu testes en début de boucle, le `foreach` doit demander
    //     UN ÉLÉMENT DE PLUS à la source avant de découvrir qu'il
    //     faut s'arrêter. Le résultat est correct, mais tu as lu un
    //     élément pour rien — et l'Espion le verra.
    //
    //     Sur une source infinie ça marche quand même ; sur une
    //     source coûteuse (un appel réseau par élément !) ça fait
    //     une requête inutile à chaque fois.
    // ───────────────────────────────────────────────────────────────
    public static IEnumerable<T> MonTake<T>(this IEnumerable<T> source, int nombre)
    {
        // TODO:
        yield break;
    }


    // ═══════════════════════════════════════════════════════════
    //  ⚠️ CHANGEMENT DE FAMILLE
    //
    //  Les deux méthodes qui suivent ne rendent PAS une séquence,
    //  mais UNE SEULE VALEUR. Elles ne peuvent donc pas être
    //  paresseuses : pour compter, il faut bien tout parcourir.
    //
    //  Ce sont des opérations « terminales » — comme Sum, Count,
    //  First ou ToList. Ce sont elles qui DÉCLENCHENT le calcul de
    //  toute la chaîne.
    //
    //  Donc ici : PAS de yield, un vrai return.
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐ — MonCount
    //
    //      new[] { 1, 2, 3 }.MonCount()   → 3
    //      new int[0].MonCount()          → 0
    //
    //  💡 Un compteur et un foreach. Pas de yield.
    // ───────────────────────────────────────────────────────────────
    public static int MonCount<T>(this IEnumerable<T> source)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐⭐⭐ — MonAggregate  (le « reduce »)
    //
    //  Réduit toute la séquence à une seule valeur, en partant de
    //  `depart` (le « seed » du module 7).
    //
    //      new[] { 3, 1, 4 }.MonAggregate(0, (t, n) => t + n)   → 8
    //      new[] { 3, 1, 4 }.MonAggregate(1, (t, n) => t * n)   → 12
    //      new int[0].MonAggregate(42, (t, n) => t + n)         → 42
    //
    //  💡 C'est l'accumulateur du module 3, en générique :
    //       T accumulateur = depart;
    //       foreach (...) { accumulateur = operation(accumulateur, element); }
    //       return accumulateur;
    //
    //  🧠 Une fois celle-ci écrite, tu as reconstruit le cœur de
    //     LINQ : filter, map et reduce. Tout le reste s'exprime
    //     à partir de ces trois-là.
    // ───────────────────────────────────────────────────────────────
    public static T MonAggregate<T>(this IEnumerable<T> source, T depart, Func<T, T, T> operation)
    {
        // TODO:
        return depart;
    }
}
