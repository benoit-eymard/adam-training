using Module05;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 5 — Héritage & polymorphisme
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 5 — OBJET, AVANCÉ               ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. Un tableau qui mélange les types ──────────────────────────
Console.WriteLine("--- 1. Un seul tableau, trois types ---");

Combattant[] equipe =
{
    new Guerrier("Thorin", 120, 15),
    new Mage("Elyra", 80, 40),
    new Archer("Sylas", 90, 12)
};

Console.WriteLine("  Le tableau est de type Combattant[], et pourtant :");
foreach (Combattant c in equipe)
{
    Console.WriteLine($"    {c}");
}
Console.WriteLine();
Console.WriteLine("  ✨ UN seul appel à Decrire(), TROIS résultats");
Console.WriteLine("     différents. C'est le polymorphisme.");
Console.WriteLine();

// ─── 2. Le type réel de chaque objet ──────────────────────────────
Console.WriteLine("--- 2. Qui est qui ? ---");

foreach (Combattant c in equipe)
{
    string type = c.GetType().Name;
    string soigneur = c is ISoigneur ? "sait soigner" : "ne soigne pas";
    Console.WriteLine($"    {c.Nom,-8} est un {type,-9} et {soigneur}");
}
Console.WriteLine();

// ─── 3. Chacun attaque à sa façon ─────────────────────────────────
Console.WriteLine("--- 3. Même ordre, comportements différents ---");

Combattant boss = new Guerrier("Boss", 500, 20);

foreach (Combattant c in equipe)
{
    int degats = c.Attaquer(boss);      // ⬅️ UN seul appel
    Console.WriteLine($"    {c.Nom,-8} inflige {degats,3} dégâts -> Boss à {boss.PointsDeVie} PV");
}
Console.WriteLine();
Console.WriteLine("  Le guerrier tape, le mage lance un sort, l'archer");
Console.WriteLine("  décoche une flèche. Le code appelant n'en sait rien.");
Console.WriteLine();

// ─── 4. base.Decrire() en action ──────────────────────────────────
Console.WriteLine("--- 4. L'enfant complète le parent ---");

var thorin = new Guerrier("Thorin", 120, 15);
Console.WriteLine($"    {thorin.Decrire()}");
Console.WriteLine("     └────────────┘ └──────────────────┘");
Console.WriteLine("      base.Decrire()   l'ajout du Guerrier");
Console.WriteLine();

// ─── 5. La rage, une mécanique propre au Guerrier ─────────────────
Console.WriteLine("--- 5. Chaque classe a ses règles ---");

var mannequin = new Guerrier("Mannequin", 1000, 0);
Console.WriteLine($"    Attaque normale     : {thorin.Attaquer(mannequin)} dégâts");
thorin.EntrerEnRage();
Console.WriteLine($"    Après EntrerEnRage(): {thorin.Attaquer(mannequin)} dégâts");
Console.WriteLine($"    La rage est retombée: {thorin.Attaquer(mannequin)} dégâts");
Console.WriteLine();

// ─── 6. L'interface ISoigneur ─────────────────────────────────────
Console.WriteLine("--- 6. Trier par CAPACITÉ, pas par type ---");

var blesse = new Guerrier("Blessé", 120, 10);
blesse.SubirDegats(60);
Console.WriteLine($"    Avant : {blesse}");

foreach (Combattant c in equipe)
{
    if (c is ISoigneur soigneur)        // "sait-il soigner ?"
    {
        int rendus = soigneur.SoignerAllie(blesse);
        Console.WriteLine($"    {c.Nom} soigne {rendus} PV");
    }
}
Console.WriteLine($"    Après : {blesse}");
Console.WriteLine();

// ─── 7. Tes utilitaires ───────────────────────────────────────────
Console.WriteLine("--- 7. Tes utilitaires ---");

Combattant[] groupe =
{
    new Guerrier("Thorin", 120, 15),
    new Mage("Elyra", 80, 40),
    new Archer("Sylas", 90, 12)
};
groupe[0].SubirDegats(60);
groupe[2].SubirDegats(20);

Console.WriteLine($"    NombreDeVivants   : {Utilitaires.NombreDeVivants(groupe)}");
Console.WriteLine($"    NombreDeSoigneurs : {Utilitaires.NombreDeSoigneurs(groupe)}");

Combattant plusBlesse = Utilitaires.LePlusBlesse(groupe);
Console.WriteLine($"    LePlusBlesse      : {(plusBlesse == null ? "(aucun)" : plusBlesse.Nom)}");
Console.WriteLine($"    SoinDUrgence      : {Utilitaires.SoinDUrgence(groupe)} PV rendus");

var cible = new Guerrier("Cible", 500, 5);
Console.WriteLine($"    AttaqueGroupee    : {Utilitaires.AttaqueGroupee(groupe, cible)} dégâts au total");
Console.WriteLine();

Console.WriteLine("À toi ! Lis Combattant.cs et ISoigneur.cs, puis");
Console.WriteLine("remplis Guerrier.cs, Archer.cs, Mage.cs, Utilitaires.cs ⚔️");
