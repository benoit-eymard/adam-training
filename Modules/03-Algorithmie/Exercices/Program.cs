using Module03;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 3 — Méthodes & tableaux
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 3 — ALGORITHMIE                 ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. Créer et appeler ses propres méthodes ─────────────────────
Console.WriteLine("--- 1. Mes propres méthodes ---");

Console.WriteLine($"  Doubler(21)              = {Doubler(21)}");
Console.WriteLine($"  Degats(10, 5, false)     = {Degats(10, 5, false)}");
Console.WriteLine($"  Degats(10, 5, true)      = {Degats(10, 5, true)}  <- coup critique !");
AfficherTitre("ET UNE MÉTHODE void");
Console.WriteLine();

// ─── 2. Les tableaux ──────────────────────────────────────────────
Console.WriteLine("--- 2. Les tableaux ---");

int[] notes = { 12, 15, 8, 18, 10 };

Console.WriteLine($"  notes.Length = {notes.Length}");
Console.WriteLine($"  notes[0]     = {notes[0]}   <- la PREMIÈRE case");
Console.WriteLine($"  notes[4]     = {notes[4]}   <- la dernière");
Console.WriteLine("  notes[5]     -> 💥 IndexOutOfRangeException !");
Console.WriteLine();

Console.Write("  Avec for     : ");
for (int i = 0; i < notes.Length; i++)
{
    Console.Write($"[{i}]={notes[i]} ");
}
Console.WriteLine();

Console.Write("  Avec foreach : ");
foreach (int note in notes)
{
    Console.Write($"{note} ");
}
Console.WriteLine();
Console.WriteLine();

// ─── 3. Le piège des références ───────────────────────────────────
Console.WriteLine("--- 3. ⚠️ Les tableaux sont des RÉFÉRENCES ---");

int[] a = { 1, 2, 3 };
int[] b = a;              // PAS une copie !
b[0] = 99;
Console.WriteLine($"  a = [{string.Join(", ", a)}]  <- modifié via b !");

int[] vraieCopie = (int[])a.Clone();
vraieCopie[0] = 1;
Console.WriteLine($"  a = [{string.Join(", ", a)}]  <- intact cette fois");
Console.WriteLine($"  copie = [{string.Join(", ", vraieCopie)}]");
Console.WriteLine();

// ─── 4. L'échange de deux valeurs ─────────────────────────────────
Console.WriteLine("--- 4. Échanger deux valeurs ---");

int x = 10, y = 20;
Console.WriteLine($"  Avant : x={x}, y={y}");
int temp = x;
x = y;
y = temp;
Console.WriteLine($"  Après : x={x}, y={y}");
Console.WriteLine("  (sans la variable temp, on perdrait x !)");
Console.WriteLine();

// ─── 5. Les chaînes se parcourent ─────────────────────────────────
Console.WriteLine("--- 5. Une string est une suite de char ---");

string mot = "Kaelis";
Console.WriteLine($"  mot           = {mot}");
Console.WriteLine($"  mot.Length    = {mot.Length}");
Console.WriteLine($"  mot[0]        = {mot[0]}");
Console.WriteLine($"  dernier       = {mot[mot.Length - 1]}");
Console.WriteLine($"  mot.ToUpper() = {mot.ToUpper()}");
Console.Write("  lettre par lettre : ");
foreach (char c in mot)
{
    Console.Write($"{c}.");
}
Console.WriteLine();
Console.WriteLine();

// ─── 6. Tes exercices ─────────────────────────────────────────────
Console.WriteLine("--- 6. Tes exercices ---");

int[] donnees = { 42, 7, 19, 3, 88 };
Console.WriteLine($"  données = [{string.Join(", ", donnees)}]");
Console.WriteLine();
Console.WriteLine($"  Exo 1 : Somme      = {Exo.Somme(donnees)}");
Console.WriteLine($"  Exo 2 : Maximum    = {Exo.Maximum(donnees)}");
Console.WriteLine($"  Exo 3 : Moyenne    = {Exo.Moyenne(donnees)}");
Console.WriteLine($"  Exo 4 : IndexDe 19 = {Exo.IndexDe(donnees, 19)}");
Console.WriteLine($"  Exo 5 : Inverser   = [{string.Join(", ", Exo.Inverser(donnees))}]");
Console.WriteLine($"  Exo 6 : Trier      = [{string.Join(", ", Exo.Trier(donnees))}]");
Console.WriteLine($"  Exo 7 : palindrome \"kayak\"   = {Exo.EstPalindrome("kayak")}");
Console.WriteLine($"  Exo 7 : palindrome \"bonjour\" = {Exo.EstPalindrome("bonjour")}");
Console.WriteLine();
Console.WriteLine("À toi ! Ouvre Exo.cs 🚀");


// ═══════════════════════════════════════════════════════════════════
//  Les méthodes utilisées par la démo.
//  Remarque : dans un fichier Program.cs, les méthodes se déclarent
//  APRÈS le code principal. C'est une particularité de C#.
// ═══════════════════════════════════════════════════════════════════

static int Doubler(int nombre)
{
    return nombre * 2;
}

static int Degats(int force, int bonusArme, bool coupCritique)
{
    int total = force + bonusArme;
    if (coupCritique)
    {
        total *= 2;
    }
    return total;
}

static void AfficherTitre(string texte)
{
    Console.WriteLine($"  === {texte} ===");
}
