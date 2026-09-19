using System.Drawing;
using Module01;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 1 — Lis, exécute, puis MODIFIE !
//
//  Lance-moi avec :  dotnet run --project Exercices
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 1 — PREMIERS PAS EN C#          ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. Les variables et leurs types ──────────────────────────────
Console.WriteLine("--- 1. Les types ---");

string nom = "Kaelis";
int niveau = 5;
double PointsDeVieRestants = 87.5;
char initiale = 'K';
bool estVivant = true;

Console.WriteLine($"Nom        : {nom}");
Console.WriteLine($"Initiale   : {initiale}");
Console.WriteLine($"Niveau     : {niveau}");
Console.WriteLine($"PV         : {PointsDeVieRestants}");
Console.WriteLine($"En vie     : {estVivant}");
Console.WriteLine();

// ─── 2. Le piège de la division entière ───────────────────────────
Console.WriteLine("--- 2. Attention à la division ---");

int divisionEntiere = 7 / 2;
double divisionReelle = 7.0 / 2.0;

Console.WriteLine($"7 / 2     = {divisionEntiere}   <-- la virgule est PERDUE");
Console.WriteLine($"7.0 / 2.0 = {divisionReelle}");
Console.WriteLine();

// ─── 3. Le modulo ─────────────────────────────────────────────────
Console.WriteLine("--- 3. Le modulo (le reste) ---");

Console.WriteLine($"17 / 5 = {17 / 5}  (combien de fois 5 tient dans 17)");
Console.WriteLine($"17 % 5 = {17 % 5}  (ce qu'il reste)");
Console.WriteLine($"10 est pair ? {10 % 2 == 0}");
Console.WriteLine($"7 est pair ?  {7 % 2 == 0}");
Console.WriteLine();

// ─── 4. Les opérateurs raccourcis ─────────────────────────────────
Console.WriteLine("--- 4. Les raccourcis ---");

int score = 100;
Console.WriteLine($"Score de départ : {score}");

score += 50;
Console.WriteLine($"Après score += 50 : {score}");

score -= 20;
Console.WriteLine($"Après score -= 20 : {score}");

score *= 2;
Console.WriteLine($"Après score *= 2  : {score}");

score++;
Console.WriteLine($"Après score++     : {score}");
Console.WriteLine();

// ─── 5. Tes exercices en action ───────────────────────────────────
Console.WriteLine("--- 5. Tes exercices ---");
Console.WriteLine("(ils afficheront n'importe quoi tant que tu n'as pas");
Console.WriteLine(" rempli les TODO dans Exo.cs — c'est normal !)");
Console.WriteLine();

Console.WriteLine($"Exo 1 : {Exo.SePresenter("Adam", 13)}");
Console.WriteLine($"Exo 2 : 7 + 3 = {Exo.Additionner(7, 3)}");
Console.WriteLine($"Exo 2 : moyenne de 12, 15, 18 = {Exo.Moyenne(12, 15, 18)}");
Console.WriteLine($"Exo 3 : 100 PV - 30 dégâts = {Exo.PointsDeVieRestants(100, 30)}");
Console.WriteLine($"Exo 3 : 100 PV - 150 dégâts = {Exo.PointsDeVieRestants(100, 150)}");
Console.WriteLine();
Console.WriteLine("Exo 4 :");
Console.WriteLine(Exo.FicheDePersonnage("Kaelis", "Mage", 5));
Console.WriteLine();
Console.WriteLine($"Exo 5 : 12345 pièces de cuivre = {Exo.ConvertirEnOr(12345)}");
Console.WriteLine();
//─── 6. Bonus : le dialogue ───────────────────────────────────────
//Décommente ces lignes quand tu veux tester la saisie clavier !

Console.Write("Nom de ton héros : ");
 string tonNom = Console.ReadLine();
 Console.Write("Choisi une classe (Mage/Guerrier/Archer): ");
 string taClasse = (Console.ReadLine());
 Console.Write("Quel est ton niveau : ");
 int tonNiveau = int.Parse (Console.ReadLine());
 Console.Write("Inteligence(1-20) : ");
 int inteligence = int.Parse (Console.ReadLine());
 Console.Write("Force(1-20) : ");
 int force = int.Parse (Console.ReadLine());
 string argent = ConvertirEnOr();
 double moyenStat = (inteligence + force)/2.0;
 int pointsDeVie = niveau * 10;
  
Console.WriteLine($"\n\n\nNom           : {tonNom}\nClasse        : {taClasse}\nNiveau        : {tonNiveau}\n\nInteligence   : {inteligence}\nForce         : {force}\nMoyenne stats : {moyenStat}\n\nPoints de vie : {pointsDeVie}\nOr de depart  : {argent}");

Console.WriteLine("Fin de la démo. Maintenant, ouvre Exo.cs ! 🚀");
 string ConvertirEnOr()
    {
        int cuivre = tonNiveau * 5000;
        int argent = cuivre / 100; // TODO: étape 1 — combien de pièces d'argent au total ?
        int cuivreRestant = cuivre % 100;// TODO: étape 2 — combien de cuivre reste-t-il ?
        int or = argent / 100;// TODO: étape 3 — dans ces pièces d'argent, combien d'or ?
        int argentRestant = argent % 100;// TODO: étape 4 — combien d'argent reste-t-il ?
        // TODO: étape 5 — assemble la phrase avec $"..."
        return $"{or} or, {argentRestant} argent, {cuivreRestant} cuivre";
    }

