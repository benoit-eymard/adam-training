using Module04;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 4 — Les objets
//
//  La classe Potion est DÉJÀ ÉCRITE : c'est ton modèle.
//  Le reste utilise TES classes — normal que ce soit vide au début.
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 4 — OBJET, LES BASES            ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. Créer des objets à partir d'une classe ────────────────────
Console.WriteLine("--- 1. Une classe, plusieurs objets ---");

Potion mineure = new Potion("Potion mineure", 25, 3);
Potion majeure = new Potion("Potion majeure", 80, 1);

Console.WriteLine($"  {mineure}");
Console.WriteLine($"  {majeure}");
Console.WriteLine("  (l'affichage passe par ToString() — sans lui on");
Console.WriteLine("   verrait juste 'Module04.Potion')");
Console.WriteLine();

// ─── 2. Les objets sont indépendants ──────────────────────────────
Console.WriteLine("--- 2. Chaque objet a SES données ---");

mineure.Boire();
Console.WriteLine($"  Après avoir bu la mineure :");
Console.WriteLine($"    {mineure}   <- une utilisation en moins");
Console.WriteLine($"    {majeure}   <- pas touchée");
Console.WriteLine();

// ─── 3. Le garde-fou dans la méthode ──────────────────────────────
Console.WriteLine("--- 3. L'objet protège son état ---");

Potion petite = new Potion("Petite fiole", 10, 1);
Console.WriteLine($"  1er Boire() : rend {petite.Boire()} PV");
Console.WriteLine($"  2e  Boire() : rend {petite.Boire()} PV  <- elle est vide");
Console.WriteLine($"  EstVide     : {petite.EstVide}");
Console.WriteLine();
Console.WriteLine("  Impossible de boire une potion vide : la règle est");
Console.WriteLine("  DANS l'objet, personne ne peut la contourner.");
Console.WriteLine();

// ─── 4. Le compteur static ────────────────────────────────────────
Console.WriteLine("--- 4. static : partagé par toute la classe ---");
Console.WriteLine($"  Potion.NombreDePotionsCreees = {Potion.NombreDePotionsCreees}");
Console.WriteLine("  (on l'appelle sur la CLASSE, pas sur un objet)");
Console.WriteLine();

// ─── 5. ⚠️ Les objets sont des références ──────────────────────────
Console.WriteLine("--- 5. ⚠️ Deux étiquettes, une seule boîte ---");

Potion a = new Potion("Élixir", 50, 5);
Potion b = a;                 // PAS une copie !
b.Boire();
Console.WriteLine($"  a = {a}");
Console.WriteLine("  On a bu via 'b'... et 'a' a changé aussi.");
Console.WriteLine("  a et b désignent LE MÊME objet.");
Console.WriteLine();

// ─── 6. Tes classes ───────────────────────────────────────────────
Console.WriteLine("--- 6. Tes classes ---");
Console.WriteLine();

Arme epee = new Arme("Épée longue", 8);
Console.WriteLine($"  Arme      : {epee}");

Monstre gobelin = new Monstre("Gobelin", 30, 7);
Console.WriteLine($"  Monstre   : {gobelin}");

Personnage heros = new Personnage("Kaelis", 100, 12);
Console.WriteLine($"  Personnage: {heros}");
Console.WriteLine();

Console.WriteLine("  --- Un combat ---");
heros.Equiper(epee);
Console.WriteLine($"  {heros}");
Console.WriteLine($"  Dégâts totaux : {heros.DegatsTotaux}");
Console.WriteLine();

int tour = 1;
while (heros.EstVivant && gobelin.EstVivant && tour <= 10)
{
    int d1 = heros.Attaquer(gobelin);
    Console.WriteLine($"  Tour {tour} : Kaelis inflige {d1} -> {gobelin}");

    if (gobelin.EstVivant)
    {
        int d2 = gobelin.Attaquer(heros);
        Console.WriteLine($"          Gobelin inflige {d2} -> {heros}");
    }
    tour++;
}
Console.WriteLine();

if (!gobelin.EstVivant)
{
    Console.WriteLine("  🎉 Le gobelin est vaincu !");
}
Console.WriteLine();

Console.WriteLine("À toi ! Lis Potion.cs, puis remplis Arme.cs,");
Console.WriteLine("Monstre.cs et Personnage.cs. 🗡️");
