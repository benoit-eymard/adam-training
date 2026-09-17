namespace Module07;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 7 — LAMBDA & LINQ
///
///  🎯 RÈGLE DU MODULE : chaque exercice doit tenir en UNE ou DEUX
///     lignes, SANS foreach. Si tu écris une boucle, c'est que tu
///     cherches la mauvaise méthode LINQ — relis le catalogue du
///     COURS.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Exo
{
    // ═══════════════════════════════════════════════════════════
    //  PARTIE A — Les délégués (Func et Action)
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐ — Appliquer
    //
    //  Applique l'opération reçue à la valeur, et retourne le résultat.
    //
    //  Appliquer(21, x => x * 2)  → 42
    //  Appliquer(5,  x => x + 10) → 15
    //  Appliquer(4,  x => x * x)  → 16
    //
    //  💡 "operation" est une MÉTHODE stockée dans une variable.
    //     Pour l'exécuter, on l'appelle comme une méthode :
    //         operation(valeur)
    // ───────────────────────────────────────────────────────────────
    public static int Appliquer(int valeur, Func<int, int> operation)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐⭐ — AppliquerDeuxFois
    //
    //  Applique l'opération, puis la réapplique sur le résultat.
    //
    //  AppliquerDeuxFois(3, x => x * 2)  → 12   (3→6→12)
    //  AppliquerDeuxFois(1, x => x + 10) → 21   (1→11→21)
    //
    //  💡 Tu peux appeler ta propre méthode Appliquer !
    // ───────────────────────────────────────────────────────────────
    public static int AppliquerDeuxFois(int valeur, Func<int, int> operation)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐ — CompterSi
    //
    //  Compte combien de nombres vérifient le critère.
    //  ⚠️ ÉCRIS-LE AVEC UNE BOUCLE (c'est le seul exercice où on te
    //     le demande) : le but est de comprendre ce que LINQ fait
    //     pour toi dans tous les suivants.
    //
    //  CompterSi([1,2,3,4], n => n % 2 == 0)  → 2
    //  CompterSi([1,2,3,4], n => n > 10)      → 0
    //  CompterSi([], n => true)               → 0
    //
    //  💡 if (critere(n)) { compteur++; }
    // ───────────────────────────────────────────────────────────────
    public static int CompterSi(List<int> nombres, Func<int, bool> critere)
    {
        // TODO: (avec un foreach, exceptionnellement)
        return 0;
    }


    // ═══════════════════════════════════════════════════════════
    //  PARTIE B — LINQ : filtrer et transformer
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐ — Vivants
    //
    //  Retourne la liste des héros vivants (PointsDeVie > 0),
    //  dans l'ordre d'origine.
    //
    //  💡 .Where(h => ...) puis .ToList()
    // ───────────────────────────────────────────────────────────────
    public static List<Heros> Vivants(List<Heros> equipe)
    {
        // TODO:
        return new List<Heros>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐ — Noms
    //
    //  Retourne la liste des NOMS de tous les héros, dans l'ordre.
    //
    //  💡 .Select(h => h.Nom) — transformer, pas filtrer
    // ───────────────────────────────────────────────────────────────
    public static List<string> Noms(List<Heros> equipe)
    {
        // TODO:
        return new List<string>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 6 ⭐⭐ — DeClasse
    //
    //  Retourne les héros de la classe demandée.
    //  DeClasse(equipe, "Mage") → tous les mages, morts ou vivants
    // ───────────────────────────────────────────────────────────────
    public static List<Heros> DeClasse(List<Heros> equipe, string classe)
    {
        // TODO:
        return new List<Heros>();
    }


    // ═══════════════════════════════════════════════════════════
    //  PARTIE C — LINQ : agréger
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 7 ⭐⭐ — OrTotal
    //
    //  La somme de l'or de toute l'équipe.
    //  Sur une équipe vide → 0 (Sum gère ça tout seul 👌)
    //
    //  💡 .Sum(h => h.Or)
    // ───────────────────────────────────────────────────────────────
    public static int OrTotal(List<Heros> equipe)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 8 ⭐⭐ — NiveauMoyen
    //
    //  Le niveau moyen de l'équipe.
    //
    //  ⚠️ PIÈGE : .Average() sur une liste VIDE lève une
    //     InvalidOperationException ! Retourne 0 dans ce cas.
    //
    //  💡 if (equipe.Count == 0) return 0;
    //     return equipe.Average(h => h.Niveau);
    // ───────────────────────────────────────────────────────────────
    public static double NiveauMoyen(List<Heros> equipe)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 9 ⭐⭐ — LePlusRiche
    //
    //  Le héros qui a le plus d'or. null si l'équipe est vide.
    //
    //  ⚠️ .Max(h => h.Or) rend un NOMBRE (le montant).
    //     .MaxBy(h => h.Or) rend le HÉROS. C'est lui qu'il te faut.
    //  💡 MaxBy rend déjà null sur une liste vide 👌
    // ───────────────────────────────────────────────────────────────
    public static Heros LePlusRiche(List<Heros> equipe)
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 10 ⭐⭐ — NombreDeVivants
    //
    //  Combien de héros sont vivants.
    //
    //  💡 .Count(h => ...) accepte directement un critère !
    //     Pas besoin de .Where(...).Count()
    // ───────────────────────────────────────────────────────────────
    public static int NombreDeVivants(List<Heros> equipe)
    {
        // TODO:
        return 0;
    }


    // ═══════════════════════════════════════════════════════════
    //  PARTIE D — LINQ : trier et tester
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 11 ⭐⭐ — TriesParNiveauDecroissant
    //
    //  Toute l'équipe, du plus haut niveau au plus bas.
    //
    //  💡 .OrderByDescending(h => h.Niveau)
    //  (le tri à bulles du module 3... en une ligne 😄)
    // ───────────────────────────────────────────────────────────────
    public static List<Heros> TriesParNiveauDecroissant(List<Heros> equipe)
    {
        // TODO:
        return new List<Heros>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 12 ⭐⭐ — AuMoinsUn
    //
    //  Y a-t-il au moins un héros de cette classe ?
    //
    //  💡 .Any(h => ...)
    // ───────────────────────────────────────────────────────────────
    public static bool AuMoinsUn(List<Heros> equipe, string classe)
    {
        // TODO:
        return false;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 13 ⭐⭐ — TousVivants
    //
    //  Toute l'équipe est-elle vivante ?
    //
    //  💡 .All(h => ...)
    //  ⚠️ Sur une liste VIDE, All rend true. C'est voulu (et c'est
    //     mathématiquement correct) — les tests le vérifient.
    // ───────────────────────────────────────────────────────────────
    public static bool TousVivants(List<Heros> equipe)
    {
        // TODO:
        return false;
    }


    // ═══════════════════════════════════════════════════════════
    //  PARTIE E — Enchaîner (le vrai LINQ)
    // ═══════════════════════════════════════════════════════════

    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 14 ⭐⭐⭐ — NomsDesVivantsParOrdreAlphabetique
    //
    //  Les NOMS des héros VIVANTS, triés par ordre alphabétique.
    //
    //  💡 Trois étapes enchaînées. Dans le bon ordre :
    //       filtrer → trier → transformer → ToList
    //     Écris une étape par ligne, c'est la convention.
    // ───────────────────────────────────────────────────────────────
    public static List<string> NomsDesVivantsParOrdreAlphabetique(List<Heros> equipe)
    {
        // TODO:
        return new List<string>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 15 ⭐⭐⭐ — LesPlusRiches
    //
    //  Les N héros les plus riches, du plus riche au moins riche.
    //
    //  LesPlusRiches(equipe, 2) → les 2 plus riches
    //  Si N dépasse la taille de l'équipe, retourne tout le monde
    //  (.Take gère ça tout seul 👌)
    //
    //  💡 .OrderByDescending(...).Take(n).ToList()
    // ───────────────────────────────────────────────────────────────
    public static List<Heros> LesPlusRiches(List<Heros> equipe, int n)
    {
        // TODO:
        return new List<Heros>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 16 ⭐⭐⭐ — OrDesVivants
    //
    //  La somme de l'or des SEULS héros vivants.
    //
    //  💡 Deux étapes : filtrer, puis sommer.
    //     (ou une seule, avec une astuce — voir les solutions !)
    // ───────────────────────────────────────────────────────────────
    public static int OrDesVivants(List<Heros> equipe)
    {
        // TODO:
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 17 ⭐⭐⭐ — RepartitionParClasse
    //
    //  Combien de héros dans chaque classe ?
    //
    //  Sur l'équipe d'exemple :
    //     { "Guerrier" -> 2, "Mage" -> 2, "Archer" -> 1, "Voleur" -> 1 }
    //
    //  💡 C'est tout le motif "compter des occurrences" du module 6
    //     en UNE ligne :
    //       .GroupBy(h => h.Classe)
    //       .ToDictionary(g => g.Key, g => g.Count())
    //
    //     GroupBy fabrique des paquets ; chaque paquet a une .Key
    //     (la valeur commune) et contient ses éléments.
    // ───────────────────────────────────────────────────────────────
    public static Dictionary<string, int> RepartitionParClasse(List<Heros> equipe)
    {
        // TODO:
        return new Dictionary<string, int>();
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 18 ⭐⭐⭐ — ClasseLaPlusRepresentee
    //
    //  Le nom de la classe qui compte le plus de héros.
    //  null si l'équipe est vide.
    //  En cas d'égalité, n'importe laquelle des gagnantes convient
    //  (les tests sont écrits pour éviter le cas ambigu).
    //
    //  💡 .GroupBy(...) puis .MaxBy(g => g.Count()) puis .Key
    //  ⚠️ MaxBy rend null sur une liste vide — accéder à .Key sur
    //     null ferait planter ! Teste d'abord.
    // ───────────────────────────────────────────────────────────────
    public static string ClasseLaPlusRepresentee(List<Heros> equipe)
    {
        // TODO:
        return null;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 19 ⭐⭐⭐ — Rapport
    //
    //  Un rapport texte, une ligne par héros VIVANT, triés par
    //  niveau DÉCROISSANT, au format :
    //
    //     Kaelis (Mage) — niveau 15
    //     Thorin (Guerrier) — niveau 12
    //     Elyra (Mage) — niveau 8
    //     Brunhild (Guerrier) — niveau 5
    //
    //  ⚠️ Les lignes sont séparées par \n, PAS de \n à la fin.
    //  ⚠️ Équipe vide → chaîne vide ""
    //
    //  💡 string.Join("\n", uneCollection) assemble tout.
    //     Il fonctionne directement sur un résultat LINQ, sans ToList.
    //  💡 Le tiret est un tiret cadratin "—".
    // ───────────────────────────────────────────────────────────────
    public static string Rapport(List<Heros> equipe)
    {
        // TODO:
        return "";
    }
}
