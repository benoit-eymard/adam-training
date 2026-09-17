using Module06;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 6 — Collections, erreurs, fichiers
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 6 — COLLECTIONS & DONNÉES       ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. La List, un tableau élastique ─────────────────────────────
Console.WriteLine("--- 1. List<T> ---");

List<string> sac = new List<string> { "Épée", "Potion", "Carte" };
Console.WriteLine($"  Départ    : [{string.Join(", ", sac)}]  ({sac.Count} objets)");

sac.Add("Corde");
Console.WriteLine($"  Add       : [{string.Join(", ", sac)}]");

sac.Remove("Potion");
Console.WriteLine($"  Remove    : [{string.Join(", ", sac)}]");

sac.Insert(0, "Torche");
Console.WriteLine($"  Insert(0) : [{string.Join(", ", sac)}]");

sac.Sort();
Console.WriteLine($"  Sort      : [{string.Join(", ", sac)}]");

Console.WriteLine($"  Contains(\"Épée\") = {sac.Contains("Épée")}");
Console.WriteLine($"  ⚠️ .Count sur une List, .Length sur un tableau !");
Console.WriteLine();

// ─── 2. Le Dictionary ─────────────────────────────────────────────
Console.WriteLine("--- 2. Dictionary<TCle, TValeur> ---");

Dictionary<string, int> prix = new Dictionary<string, int>
{
    ["Épée"] = 50,
    ["Potion"] = 10,
    ["Bouclier"] = 35
};

foreach (var paire in prix)
{
    Console.WriteLine($"    {paire.Key,-10} {paire.Value,4} po");
}
Console.WriteLine();

Console.WriteLine($"  prix[\"Épée\"]            = {prix["Épée"]}");
Console.WriteLine($"  prix.ContainsKey(\"Arc\") = {prix.ContainsKey("Arc")}");
Console.WriteLine("  prix[\"Arc\"]             -> 💥 KeyNotFoundException !");
Console.WriteLine();

if (prix.TryGetValue("Arc", out int valeurArc))
{
    Console.WriteLine($"  L'arc coûte {valeurArc}");
}
else
{
    Console.WriteLine("  TryGetValue(\"Arc\") -> false, pas d'exception 👌");
}
Console.WriteLine();

// ─── 3. Les exceptions ────────────────────────────────────────────
Console.WriteLine("--- 3. try / catch ---");

try
{
    int resultat = 10 / int.Parse("0");
    Console.WriteLine(resultat);
}
catch (DivideByZeroException e)
{
    Console.WriteLine($"  Rattrapé : {e.GetType().Name}");
    Console.WriteLine($"  Message  : {e.Message}");
    Console.WriteLine("  ...et le programme CONTINUE. 😌");
}
Console.WriteLine();

// ─── 4. TryParse plutôt que try/catch ─────────────────────────────
Console.WriteLine("--- 4. TryParse : tester, pas rattraper ---");

foreach (string saisie in new[] { "42", "bonjour", "-7", "3.14", "" })
{
    if (int.TryParse(saisie, out int n))
    {
        Console.WriteLine($"  \"{saisie,-8}\" -> {n}");
    }
    else
    {
        Console.WriteLine($"  \"{saisie,-8}\" -> pas un entier");
    }
}
Console.WriteLine();

// ─── 5. Le piège du foreach ───────────────────────────────────────
Console.WriteLine("--- 5. ⚠️ Modifier en parcourant ---");

List<int> nombres = new List<int> { 1, 2, 3, 4, 5, 6 };
Console.WriteLine($"  Avant : [{string.Join(", ", nombres)}]");

// ❌ foreach (int n in nombres) { if (n % 2 == 0) nombres.Remove(n); }
//    -> InvalidOperationException !
//
// ✅ on parcourt À L'ENVERS avec un for :
for (int i = nombres.Count - 1; i >= 0; i--)
{
    if (nombres[i] % 2 == 0)
    {
        nombres.RemoveAt(i);
    }
}
Console.WriteLine($"  Après : [{string.Join(", ", nombres)}]  (pairs retirés)");
Console.WriteLine();

// ─── 6. Tes exercices ─────────────────────────────────────────────
Console.WriteLine("--- 6. Tes exercices ---");

Console.WriteLine($"  ConvertirOuDefaut(\"42\", 0)     = {Exo.ConvertirOuDefaut("42", 0)}");
Console.WriteLine($"  ConvertirOuDefaut(\"oups\", -1)  = {Exo.ConvertirOuDefaut("oups", -1)}");
Console.WriteLine($"  SansDoublons([a,b,a,c])        = [{string.Join(", ", Exo.SansDoublons(new List<string> { "a", "b", "a", "c" }))}]");

var comptes = Exo.CompterMots(new[] { "épée", "potion", "épée" });
Console.WriteLine($"  CompterMots                    = {comptes.Count} clé(s)");
foreach (var p in comptes)
{
    Console.WriteLine($"      {p.Key} -> {p.Value}");
}

Console.WriteLine($"  LePlusFrequent([a,b,a])        = {Exo.LePlusFrequent(new[] { "a", "b", "a" }) ?? "(null)"}");
Console.WriteLine($"  DiviserSansPlanter(10, 0)      = {Exo.DiviserSansPlanter(10, 0)}");
Console.WriteLine($"  MoyenneDesValides([10,oups,20])= {Exo.MoyenneDesValides(new[] { "10", "oups", "20" })}");
Console.WriteLine();

// ─── 7. L'inventaire ──────────────────────────────────────────────
Console.WriteLine("--- 7. Ton inventaire ---");

Inventaire inventaire = new Inventaire(5);
inventaire.Ajouter(new Objet("Épée longue", 50));
inventaire.Ajouter(new Objet("Potion", 10));
inventaire.Ajouter(new Objet("Couronne", 500));

Console.WriteLine($"  {inventaire}");
Console.WriteLine($"  Valeur totale   : {inventaire.ValeurTotale} po");
Objet cher = inventaire.ObjetLePlusCher();
Console.WriteLine($"  Le plus cher    : {(cher == null ? "(aucun)" : cher.ToString())}");
Console.WriteLine($"  Contient Potion : {inventaire.Contient("Potion")}");
Console.WriteLine();

// ─── 8. La sauvegarde ─────────────────────────────────────────────
Console.WriteLine("--- 8. Ta sauvegarde ---");

string fichier = "ma-partie.json";

Partie partie = new Partie
{
    NomDuHeros = "Kaelis",
    Niveau = 5,
    PointsDeVie = 87,
    Or = 1250,
    Objets = inventaire.Lister()
};

if (Sauvegarde.Enregistrer(partie, fichier))
{
    Console.WriteLine($"  ✅ Enregistré dans {Path.GetFullPath(fichier)}");
    Console.WriteLine("  Contenu du fichier :");
    foreach (string ligne in File.ReadAllLines(fichier))
    {
        Console.WriteLine($"      {ligne}");
    }

    Partie relue = Sauvegarde.Charger(fichier);
    Console.WriteLine($"  ✅ Rechargé : {(relue == null ? "(null)" : relue.ToString())}");
}
else
{
    Console.WriteLine("  (Enregistrer ne fait rien pour l'instant — c'est ton exercice 9 !)");
}
Console.WriteLine();

Console.WriteLine("À toi ! Exo.cs, Inventaire.cs, Sauvegarde.cs 🎒");
