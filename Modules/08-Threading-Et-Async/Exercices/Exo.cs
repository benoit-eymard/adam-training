namespace Module08;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  MODULE 8 — THREADING & ASYNC
///
///  ⚠️ Toutes ces méthodes retournent une Task. C'est normal :
///     une méthode async ne retourne JAMAIS directement sa valeur,
///     elle retourne la PROMESSE de cette valeur.
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public static class Exo
{
    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 1 ⭐ — Ta première méthode async
    //
    //  Attend 50 millisecondes, puis retourne a + b.
    //
    //  AdditionnerAsync(3, 4) → 7 (après 50 ms)
    //
    //  💡 await Task.Delay(50);
    //     return a + b;
    //
    //  ⚠️ PAS Thread.Sleep ! Task.Delay n'occupe aucun thread
    //     pendant l'attente ; Thread.Sleep en bloque un pour rien.
    // ───────────────────────────────────────────────────────────────
    public static async Task<int> AdditionnerAsync(int a, int b)
    {
        // TODO:
        await Task.Delay(1);
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 2 ⭐⭐ — Le chargement SÉQUENTIEL
    //
    //  Charge chaque ressource l'une APRÈS l'autre (avec
    //  Simulateur.ChargerAsync) et retourne la somme des longueurs.
    //
    //  ChargerSequentielAsync(["ab", "cde"]) → 5
    //
    //  ⚠️ Cette version est VOLONTAIREMENT lente : avec 5 ressources
    //     à 100 ms, elle prendra 500 ms. C'est le but — l'exercice 3
    //     te montrera la différence.
    //
    //  💡 Un foreach, avec un await à l'intérieur, et un accumulateur.
    // ───────────────────────────────────────────────────────────────
    public static async Task<int> ChargerSequentielAsync(string[] ressources)
    {
        // TODO:
        await Task.Delay(1);
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 3 ⭐⭐⭐ — Le chargement PARALLÈLE
    //
    //  Charge TOUTES les ressources EN MÊME TEMPS, et retourne leurs
    //  longueurs DANS L'ORDRE des ressources reçues.
    //
    //  ChargerToutesAsync(["ab", "cde", "f"]) → [2, 3, 1]
    //
    //  ⚠️ Avec 5 ressources à 100 ms, ça doit prendre ~100 ms au
    //     total, PAS 500 ms. Un test vérifie le chronomètre !
    //
    //  💡 Les DEUX temps :
    //       1. LANCER toutes les tâches, SANS await :
    //            Task<int>[] taches = ressources
    //                .Select(r => Simulateur.ChargerAsync(r))
    //                .ToArray();
    //       2. Les attendre TOUTES :
    //            return await Task.WhenAll(taches);
    //
    //  💡 Task.WhenAll rend les résultats dans l'ordre des TÂCHES,
    //     pas dans l'ordre d'arrivée. L'ordre est donc garanti. 👌
    // ───────────────────────────────────────────────────────────────
    public static async Task<int[]> ChargerToutesAsync(string[] ressources)
    {
        // TODO:
        await Task.Delay(1);
        return new int[0];
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 4 ⭐⭐ — Le total en parallèle
    //
    //  Comme l'exercice 2, mais en parallèle : la somme des longueurs.
    //
    //  💡 Réutilise ChargerToutesAsync, puis .Sum() (module 7 !).
    // ───────────────────────────────────────────────────────────────
    public static async Task<int> ChargerParalleleAsync(string[] ressources)
    {
        // TODO:
        await Task.Delay(1);
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 5 ⭐⭐ — Gérer une erreur en async
    //
    //  Utilise Simulateur.ChargerRisqueAsync, qui LÈVE une exception
    //  si la ressource est null ou vide.
    //  Retourne la longueur, ou -1 si le chargement a échoué.
    //
    //  ChargerOuMoinsUnAsync("abc") → 3
    //  ChargerOuMoinsUnAsync("")    → -1
    //  ChargerOuMoinsUnAsync(null)  → -1
    //
    //  💡 try/catch fonctionne normalement autour d'un await :
    //       try { return await Simulateur.ChargerRisqueAsync(r); }
    //       catch (Exception) { return -1; }
    // ───────────────────────────────────────────────────────────────
    public static async Task<int> ChargerOuMoinsUnAsync(string ressource)
    {
        // TODO:
        await Task.Delay(1);
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 6 ⭐⭐⭐ — Le calcul parallèle (problème n°2 !)
    //
    //  ⚠️ ATTENTION : ici on ne fait PAS d'attente, on fait du CALCUL.
    //     C'est l'autre problème du module. La solution n'est plus
    //     "async/await" mais Task.Run, pour occuper plusieurs cœurs.
    //
    //  Calcule Simulateur.CalculLourd(0, taille) en découpant le
    //  travail en DEUX moitiés traitées en parallèle, puis retourne
    //  la somme des deux résultats.
    //
    //  ⚠️ Le résultat doit être EXACTEMENT le même que
    //     Simulateur.CalculLourd(0, taille) fait d'un seul bloc.
    //
    //  💡 int milieu = taille / 2;
    //     Task<long> t1 = Task.Run(() => Simulateur.CalculLourd(0, milieu));
    //     Task<long> t2 = Task.Run(() => Simulateur.CalculLourd(milieu, taille));
    //     long[] r = await Task.WhenAll(t1, t2);
    //     return r[0] + r[1];
    //
    //  💡 Le "() =>" est une lambda (module 7 !) : Task.Run a besoin
    //     du CODE à exécuter, pas de son résultat. Sans la lambda,
    //     le calcul serait fait AVANT d'être envoyé au thread. 😅
    // ───────────────────────────────────────────────────────────────
    public static async Task<long> CalculerEnParalleleAsync(int taille)
    {
        // TODO:
        await Task.Delay(1);
        return 0;
    }


    // ───────────────────────────────────────────────────────────────
    //  EXERCICE 7 ⭐⭐⭐ — Le compteur martyrisé
    //
    //  Lance "nombreDeTaches" tâches en parallèle. Chacune incrémente
    //  le compteur "incrementsParTache" fois, de façon SÉCURISÉE.
    //  Retourne la valeur finale du compteur.
    //
    //  Avec 4 tâches × 10 000 incréments → doit rendre EXACTEMENT
    //  40 000, à chaque exécution, sans exception.
    //
    //  💡 Construis un tableau de tâches :
    //       Task[] taches = new Task[nombreDeTaches];
    //       for (int t = 0; t < nombreDeTaches; t++)
    //       {
    //           taches[t] = Task.Run(() =>
    //           {
    //               for (int i = 0; i < incrementsParTache; i++)
    //               {
    //                   compteur.IncrementerSecurise();
    //               }
    //           });
    //       }
    //       await Task.WhenAll(taches);
    //       return compteur.Valeur;
    //
    //  ⚠️ Utilise bien IncrementerSecurise, pas IncrementerNonSecurise !
    // ───────────────────────────────────────────────────────────────
    public static async Task<int> MartyriserLeCompteurAsync(
        Compteur compteur, int nombreDeTaches, int incrementsParTache)
    {
        // TODO:
        await Task.Delay(1);
        return 0;
    }
}
