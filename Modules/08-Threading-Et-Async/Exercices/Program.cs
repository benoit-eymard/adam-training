using System.Diagnostics;
using Module08;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 8 — Threading & async
//
//  ⚠️ Remarque : ce fichier utilise "await" au niveau principal.
//     C'est autorisé dans un Program.cs moderne — C# transforme
//     ton programme en méthode async automatiquement.
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 8 — THREADING & ASYNC           ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

string[] ressources = { "alpha", "beta", "gamma", "delta", "epsilon" };

// ─── 1. Séquentiel vs parallèle ───────────────────────────────────
Console.WriteLine("--- 1. Séquentiel vs parallèle ---");
Console.WriteLine($"    {ressources.Length} ressources, {Simulateur.DureeMs} ms chacune.");
Console.WriteLine();

var chrono = Stopwatch.StartNew();
int totalSeq = 0;
foreach (string r in ressources)
{
    totalSeq += await Simulateur.ChargerAsync(r);    // await DANS la boucle
}
chrono.Stop();
long tempsSequentiel = chrono.ElapsedMilliseconds;
Console.WriteLine($"    Séquentiel : {tempsSequentiel,4} ms  (total = {totalSeq})");

chrono.Restart();
Task<int>[] taches = ressources.Select(r => Simulateur.ChargerAsync(r)).ToArray();
int[] resultats = await Task.WhenAll(taches);
chrono.Stop();
long tempsParallele = chrono.ElapsedMilliseconds;
Console.WriteLine($"    Parallèle  : {tempsParallele,4} ms  (total = {resultats.Sum()})");
Console.WriteLine();
Console.WriteLine($"    👉 Environ {(double)tempsSequentiel / Math.Max(1, tempsParallele):F1}× plus rapide,");
Console.WriteLine("       pour exactement le même résultat.");
Console.WriteLine();

// ─── 2. await ne bloque pas ───────────────────────────────────────
Console.WriteLine("--- 2. await rend la main ---");

Task<int> enCours = Simulateur.ChargerAsync("quelque chose de long");
Console.WriteLine("    La tâche est LANCÉE...");
Console.WriteLine("    ...et je peux continuer à travailler ici !");
Console.WriteLine("    (compte jusqu'à 3 pendant que ça charge)");
for (int i = 1; i <= 3; i++) Console.WriteLine($"      {i}...");
int valeur = await enCours;
Console.WriteLine($"    Et voilà le résultat : {valeur}");
Console.WriteLine();

// ─── 3. 🔴 LA RACE CONDITION ──────────────────────────────────────
Console.WriteLine("--- 3. 🔴 LA RACE CONDITION ---");
Console.WriteLine();
Console.WriteLine("    4 tâches × 25 000 incréments = 100 000 attendus.");
Console.WriteLine();

const int NbTaches = 4;
const int Increments = 25_000;

// Version NON sécurisée — le bug en direct
var compteurBugue = new Compteur();
Task[] tachesBuguees = new Task[NbTaches];
for (int t = 0; t < NbTaches; t++)
{
    tachesBuguees[t] = Task.Run(() =>
    {
        for (int i = 0; i < Increments; i++)
        {
            compteurBugue.IncrementerNonSecurise();
        }
    });
}
await Task.WhenAll(tachesBuguees);

int attendu = NbTaches * Increments;
int obtenu = compteurBugue.Valeur;

Console.WriteLine($"    Sans lock : {obtenu,7} / {attendu}   {(obtenu == attendu ? "😐 (par chance !)" : $"😱 {attendu - obtenu} incréments PERDUS")}");

// Version sécurisée
var compteurSur = new Compteur();
Console.WriteLine($"    Avec lock : {await Exo.MartyriserLeCompteurAsync(compteurSur, NbTaches, Increments),7} / {attendu}");
Console.WriteLine();
Console.WriteLine("    ⚠️ RELANCE CE PROGRAMME PLUSIEURS FOIS.");
Console.WriteLine("       La ligne 'sans lock' change à chaque exécution.");
Console.WriteLine("       C'est ça, un bug de concurrence : imprévisible,");
Console.WriteLine("       intermittent, et il disparaît quand tu le cherches.");
Console.WriteLine();

// ─── 4. Pourquoi ça casse ─────────────────────────────────────────
Console.WriteLine("--- 4. Pourquoi ça casse ---");
Console.WriteLine();
Console.WriteLine("    compteur++ n'est PAS une seule opération :");
Console.WriteLine("        1. LIRE la valeur      (100)");
Console.WriteLine("        2. AJOUTER 1           (101)");
Console.WriteLine("        3. ÉCRIRE le résultat  (100 -> 101)");
Console.WriteLine();
Console.WriteLine("    Deux threads en même temps :");
Console.WriteLine("        Thread A : LIT 100");
Console.WriteLine("        Thread B : LIT 100     <- avant que A ait écrit !");
Console.WriteLine("        Thread A : ÉCRIT 101");
Console.WriteLine("        Thread B : ÉCRIT 101");
Console.WriteLine();
Console.WriteLine("    -> deux incréments, un seul compté. Un est PERDU.");
Console.WriteLine();

// ─── 5. Le calcul parallèle (l'autre problème) ────────────────────
Console.WriteLine("--- 5. Calcul parallèle (problème n°2 : le CPU) ---");

const int Taille = 40_000_000;

chrono.Restart();
long resultatSimple = Simulateur.CalculLourd(0, Taille);
chrono.Stop();
long tempsSimple = chrono.ElapsedMilliseconds;

chrono.Restart();
long resultatParallele = await Exo.CalculerEnParalleleAsync(Taille);
chrono.Stop();
long tempsCalculParallele = chrono.ElapsedMilliseconds;

Console.WriteLine($"    Sur un cœur   : {tempsSimple,5} ms  (résultat {resultatSimple})");
Console.WriteLine($"    Sur deux      : {tempsCalculParallele,5} ms  (résultat {resultatParallele})");
Console.WriteLine($"    Processeurs disponibles : {Environment.ProcessorCount}");
if (resultatParallele != resultatSimple)
{
    Console.WriteLine("    (les résultats diffèrent : l'exercice 6 n'est pas encore fait)");
}
Console.WriteLine();

Console.WriteLine("À toi ! Compteur.cs puis Exo.cs ⚡");
