using Module07;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 7 — Lambda & LINQ
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 7 — LAMBDA & LINQ               ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

List<Heros> equipe = Equipes.Exemple();

Console.WriteLine("--- L'équipe d'exemple ---");
foreach (Heros h in equipe)
{
    Console.WriteLine($"    {h}");
}
Console.WriteLine();

// ─── 1. Une variable qui contient une méthode ─────────────────────
Console.WriteLine("--- 1. Func : une METHODE dans une variable ---");

Func<int, int> doubler = x => x * 2;
Func<int, int, int> additionner = (a, b) => a + b;
Func<string> saluer = () => "Bonjour !";
Action<string> crier = texte => Console.WriteLine($"    >>> {texte.ToUpper()} !!!");

Console.WriteLine($"    doubler(21)        = {doubler(21)}");
Console.WriteLine($"    additionner(3, 4)  = {additionner(3, 4)}");
Console.WriteLine($"    saluer()           = {saluer()}");
crier("attention");
Console.WriteLine();

// ─── 2. Passer un comportement en paramètre ───────────────────────
Console.WriteLine("--- 2. Une méthode, tous les critères ---");

Console.WriteLine($"    Appliquer(10, x => x * 3) = {Exo.Appliquer(10, x => x * 3)}");
Console.WriteLine($"    Appliquer(10, x => x - 1) = {Exo.Appliquer(10, x => x - 1)}");
Console.WriteLine("    Une seule méthode, un comportement différent à chaque appel.");
Console.WriteLine();

// ─── 3. Avant / après ─────────────────────────────────────────────
Console.WriteLine("--- 3. AVANT (module 6) vs MAINTENANT ---");
Console.WriteLine();

// AVANT : 7 lignes
List<Heros> vivantsALaMain = new List<Heros>();
foreach (Heros h in equipe)
{
    if (h.PointsDeVie > 0)
    {
        vivantsALaMain.Add(h);
    }
}

// MAINTENANT : 1 ligne
List<Heros> vivantsLinq = equipe.Where(h => h.PointsDeVie > 0).ToList();

Console.WriteLine($"    À la main : {vivantsALaMain.Count} vivants (7 lignes)");
Console.WriteLine($"    En LINQ   : {vivantsLinq.Count} vivants (1 ligne)");
Console.WriteLine();

// ─── 4. Le catalogue en action ────────────────────────────────────
Console.WriteLine("--- 4. Le catalogue LINQ ---");

Console.WriteLine($"    Count()                    = {equipe.Count()}");
Console.WriteLine($"    Count(vivant)              = {equipe.Count(h => h.EstVivant)}");
Console.WriteLine($"    Sum(Or)                    = {equipe.Sum(h => h.Or)}");
Console.WriteLine($"    Average(Niveau)            = {equipe.Average(h => h.Niveau)}");
Console.WriteLine($"    Max(Niveau)                = {equipe.Max(h => h.Niveau)}   <- un NOMBRE");
Console.WriteLine($"    MaxBy(Niveau)              = {equipe.MaxBy(h => h.Niveau).Nom}   <- un HÉROS");
Console.WriteLine($"    Any(classe == Voleur)      = {equipe.Any(h => h.Classe == "Voleur")}");
Console.WriteLine($"    All(vivant)                = {equipe.All(h => h.EstVivant)}");
Console.WriteLine($"    First(Mage).Nom            = {equipe.First(h => h.Classe == "Mage").Nom}");
Console.WriteLine($"    FirstOrDefault(Paladin)    = {(equipe.FirstOrDefault(h => h.Classe == "Paladin")?.Nom ?? "(null)")}");
Console.WriteLine();

// ─── 5. Enchaîner ─────────────────────────────────────────────────
Console.WriteLine("--- 5. Enchaîner les étapes ---");
Console.WriteLine();
Console.WriteLine("    « Les noms des mages vivants, triés par or décroissant »");
Console.WriteLine();

var resultat = equipe
    .Where(h => h.Classe == "Mage")
    .Where(h => h.EstVivant)
    .OrderByDescending(h => h.Or)
    .Select(h => h.Nom)
    .ToList();

Console.WriteLine($"    -> [{string.Join(", ", resultat)}]");
Console.WriteLine();

// ─── 6. GroupBy ───────────────────────────────────────────────────
Console.WriteLine("--- 6. GroupBy : faire des paquets ---");

foreach (var groupe in equipe.GroupBy(h => h.Classe))
{
    Console.WriteLine($"    {groupe.Key,-10} : {groupe.Count()} ({string.Join(", ", groupe.Select(h => h.Nom))})");
}
Console.WriteLine();

// ─── 7. ⚠️ L'exécution différée ────────────────────────────────────
Console.WriteLine("--- 7. ⚠️ Une requête est une RECETTE, pas un plat ---");

List<Heros> liste = new List<Heros> { new Heros("A", "Mage", 1, 10, 0) };

var requete = liste.Where(h => h.EstVivant);         // rien n'est calculé
var figee = liste.Where(h => h.EstVivant).ToList();  // calculé MAINTENANT

liste.Add(new Heros("B", "Mage", 1, 10, 0));

Console.WriteLine($"    requete.Count() = {requete.Count()}   <- B est là ! (recalculé)");
Console.WriteLine($"    figee.Count     = {figee.Count}   <- figé avant l'ajout");
Console.WriteLine();
Console.WriteLine("    => Termine par .ToList() quand tu veux figer le résultat.");
Console.WriteLine();

// ─── 8. Tes exercices ─────────────────────────────────────────────
Console.WriteLine("--- 8. Tes exercices ---");

Console.WriteLine($"    OrTotal           = {Exo.OrTotal(equipe)}");
Console.WriteLine($"    NiveauMoyen       = {Exo.NiveauMoyen(equipe)}");
Console.WriteLine($"    NombreDeVivants   = {Exo.NombreDeVivants(equipe)}");
Heros riche = Exo.LePlusRiche(equipe);
Console.WriteLine($"    LePlusRiche       = {(riche == null ? "(null)" : riche.Nom)}");
Console.WriteLine($"    Noms des vivants  = [{string.Join(", ", Exo.NomsDesVivantsParOrdreAlphabetique(equipe))}]");
Console.WriteLine($"    ClassePlusRepres. = {Exo.ClasseLaPlusRepresentee(equipe) ?? "(null)"}");
Console.WriteLine();
Console.WriteLine("    Rapport :");
Console.WriteLine(Exo.Rapport(equipe));
Console.WriteLine();

Console.WriteLine("À toi ! Ouvre Exo.cs — et pas un seul foreach. 🪄");
