using Module02;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 2 — Décisions & boucles
//  Lance-moi avec :  dotnet run --project Exercices
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 2 — DÉCISIONS & BOUCLES         ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. Les booléens ──────────────────────────────────────────────
Console.WriteLine("--- 1. Les booléens ---");

int pv = 87;
Console.WriteLine($"pv = {pv}");
Console.WriteLine($"pv > 0        -> {pv > 0}");
Console.WriteLine($"pv == 100     -> {pv == 100}");
Console.WriteLine($"pv != 100     -> {pv != 100}");
Console.WriteLine($"pv > 50 && pv < 90 -> {pv > 50 && pv < 90}");
Console.WriteLine($"!(pv > 0)     -> {!(pv > 0)}");
Console.WriteLine();

// ─── 2. La cascade de if ──────────────────────────────────────────
Console.WriteLine("--- 2. L'ordre des if compte ! ---");

foreach (int test in new[] { 90, 60, 20, 0 })
{
    string etat;
    if (test > 75) etat = "Pleine forme";
    else if (test > 40) etat = "Un peu amoché";
    else if (test > 0) etat = "Critique !";
    else etat = "Mort";

    Console.WriteLine($"  {test,3} PV -> {etat}");
}
Console.WriteLine();

// ─── 3. Le switch ─────────────────────────────────────────────────
Console.WriteLine("--- 3. Le switch ---");

foreach (string classe in new[] { "Guerrier", "Mage", "Archer", "Pirate" })
{
    string action = classe switch
    {
        "Guerrier" => "frappe avec son épée !",
        "Mage" => "lance une boule de feu !",
        "Archer" => "décoche une flèche !",
        _ => "ne sait pas quoi faire..."
    };
    Console.WriteLine($"  Le {classe} {action}");
}
Console.WriteLine();

// ─── 4. La boucle for ─────────────────────────────────────────────
Console.WriteLine("--- 4. La boucle for ---");

Console.Write("  En avant : ");
for (int i = 1; i <= 5; i++)
{
    Console.Write($"{i} ");
}
Console.WriteLine();

Console.Write("  En arrière : ");
for (int i = 5; i >= 1; i--)
{
    Console.Write($"{i} ");
}
Console.WriteLine();

Console.Write("  De 2 en 2 : ");
for (int i = 0; i <= 10; i += 2)
{
    Console.Write($"{i} ");
}
Console.WriteLine();
Console.WriteLine();

// ─── 5. La boucle while ───────────────────────────────────────────
Console.WriteLine("--- 5. La boucle while : un combat ---");

int pvMonstre = 100;
int tour = 1;
while (pvMonstre > 0)
{
    int degats = 23;
    pvMonstre -= degats;
    if (pvMonstre < 0) pvMonstre = 0;
    Console.WriteLine($"  Tour {tour} : -{degats} dégâts, il reste {pvMonstre} PV");
    tour++;
}
Console.WriteLine("  Le monstre est vaincu !");
Console.WriteLine();

// ─── 6. break et continue ─────────────────────────────────────────
Console.WriteLine("--- 6. break et continue ---");

Console.Write("  break à 5     : ");
for (int i = 1; i <= 10; i++)
{
    if (i == 5) break;
    Console.Write($"{i} ");
}
Console.WriteLine();

Console.Write("  continue impairs : ");
for (int i = 1; i <= 10; i++)
{
    if (i % 2 != 0) continue;
    Console.Write($"{i} ");
}
Console.WriteLine();
Console.WriteLine();

// ─── 7. Boucles imbriquées ────────────────────────────────────────
Console.WriteLine("--- 7. Boucles imbriquées : un triangle ---");

for (int ligne = 1; ligne <= 5; ligne++)
{
    Console.Write("  ");
    for (int colonne = 1; colonne <= ligne; colonne++)
    {
        Console.Write("*");
    }
    Console.WriteLine();
}
Console.WriteLine();

// ─── 8. Tes exercices ─────────────────────────────────────────────
Console.WriteLine("--- 8. Tes exercices ---");
Console.WriteLine();

Console.WriteLine($"Exo 1 : EstMajeur(13) = {Exo.EstMajeur(13)}");
Console.WriteLine($"Exo 2 : LePlusGrand(3, 9, 5) = {Exo.LePlusGrand(3, 9, 5)}");
Console.WriteLine($"Exo 3 : NoteEnLettre(15.5) = {Exo.NoteEnLettre(15.5)}");
Console.WriteLine($"Exo 4 : {Exo.CompteARebours(5)}");
Console.WriteLine("Exo 5 :");
Console.WriteLine(Exo.TableDeMultiplication(7));
Console.Write("Exo 6 : ");
for (int i = 1; i <= 20; i++)
{
    Console.Write($"{Exo.FizzBuzz(i)} ");
}
Console.WriteLine();
Console.WriteLine("Exo 7 :");
Console.WriteLine($"  {Exo.BarreDeVie(100, 100)}");
Console.WriteLine($"  {Exo.BarreDeVie(70, 100)}");
Console.WriteLine($"  {Exo.BarreDeVie(30, 100)}");
Console.WriteLine($"  {Exo.BarreDeVie(0, 100)}");
Console.WriteLine();

Console.WriteLine("À toi de jouer ! Ouvre Exo.cs 🚀");
