namespace Module05;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE 4 ⭐⭐⭐ — Le polymorphisme en action
///
///  Ces méthodes reçoivent un tableau de Combattant.
///  Elles ne savent PAS (et n'ont pas besoin de savoir) s'il y a
///  dedans des guerriers, des mages ou des archers.
///
///  C'est TOUT l'intérêt : tu pourras ajouter une classe Voleur
///  demain, ces méthodes marcheront sans être modifiées.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Utilitaires
{
    // ───────────────────────────────────────────────────────────────
    //  4.1 ⭐ — NombreDeVivants
    //
    //  Compte combien de combattants sont encore vivants.
    //
    //  💡 Un foreach + un compteur. EstVivant est hérité de
    //     Combattant : disponible sur tous, quel que soit leur type.
    // ───────────────────────────────────────────────────────────────
    public static int NombreDeVivants(Combattant[] equipe)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  4.2 ⭐⭐ — LePlusBlesse
    //
    //  Retourne le combattant VIVANT qui a le plus de PV manquants.
    //  Retourne null si l'équipe est vide ou si personne n'est vivant.
    //
    //  En cas d'égalité, retourne le PREMIER trouvé.
    //
    //  💡 C'est le motif du "maximum" du module 3, mais sur des
    //     objets. Attention : ici on ne peut pas partir de equipe[0]
    //     (il pourrait être mort). Pars de null et traite le cas.
    //
    //       Combattant resultat = null;
    //       foreach (...) {
    //           if (!c.EstVivant) continue;
    //           if (resultat == null || c.PointsDeVieManquants > ...) {
    //               resultat = c;
    //           }
    //       }
    // ───────────────────────────────────────────────────────────────
    public static Combattant LePlusBlesse(Combattant[] equipe)
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  4.3 ⭐⭐ — AttaqueGroupee
    //
    //  TOUS les combattants vivants de l'équipe attaquent la même
    //  cible, dans l'ordre du tableau.
    //  Retourne le TOTAL des dégâts infligés.
    //
    //  ⚠️ Chacun attaque à SA façon : le guerrier avec sa force,
    //     le mage avec son sort, l'archer avec ses flèches.
    //     Et pourtant tu écris UN SEUL appel : c.Attaquer(cible).
    //     C# choisit la bonne version tout seul. C'est ça, le
    //     polymorphisme. ✨
    //
    //  💡 Les morts n'attaquent pas — mais leur méthode Attaquer
    //     retourne déjà 0, donc tu peux te contenter d'additionner.
    // ───────────────────────────────────────────────────────────────
    public static int AttaqueGroupee(Combattant[] equipe, Combattant cible)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  4.4 ⭐⭐⭐ — NombreDeSoigneurs
    //
    //  Compte combien de membres de l'équipe savent soigner,
    //  c'est-à-dire combien implémentent l'interface ISoigneur.
    //
    //  💡 L'opérateur "is" teste le type :
    //       if (c is ISoigneur) { ... }
    //
    //  ⚠️ Ne teste PAS "if (c is Mage)" ! L'intérêt de l'interface,
    //     c'est justement de ne pas dépendre des classes concrètes.
    //     Si tu ajoutes un Paladin qui soigne aussi, ton code doit
    //     le compter sans être modifié.
    // ───────────────────────────────────────────────────────────────
    public static int NombreDeSoigneurs(Combattant[] equipe)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  4.5 ⭐⭐⭐ — SoinDUrgence
    //
    //  Cherche le premier ISoigneur VIVANT de l'équipe, et lui fait
    //  soigner le combattant le plus blessé (LePlusBlesse).
    //  Retourne le nombre de PV rendus, ou 0 si c'est impossible.
    //
    //  ⚠️ Un soigneur ne se soigne pas lui-même ici : si le plus
    //     blessé EST le soigneur, retourne 0.
    //
    //  💡 Le motif "tester ET ranger" :
    //       if (c is ISoigneur soigneur) {
    //           // ici, "soigneur" est utilisable directement
    //           soigneur.SoignerAllie(...);
    //       }
    //  💡 Réutilise ta méthode LePlusBlesse !
    //  💡 Pour savoir si deux variables désignent le MÊME objet :
    //       if (blesse == c) { ... }
    //     Sur des objets, == compare les RÉFÉRENCES (les boîtes),
    //     pas le contenu.
    // ───────────────────────────────────────────────────────────────
    public static int SoinDUrgence(Combattant[] equipe)
    {
        // TODO:
        return 0;
    }
}
