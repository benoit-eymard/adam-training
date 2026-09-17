using Module09;

// ═══════════════════════════════════════════════════════════════════
//  DÉMO DU MODULE 9 — yield et IEnumerable
// ═══════════════════════════════════════════════════════════════════

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   MODULE 9 — SOUS LE CAPOT DE LINQ       ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.WriteLine();

// ─── 1. Une méthode qui se met en pause ───────────────────────────
Console.WriteLine("--- 1. yield return met la méthode en PAUSE ---");
Console.WriteLine();

var sequence = Bavarde();
Console.WriteLine("  J'ai appelé Bavarde()... et rien ne s'est affiché.");
Console.WriteLine("  Aucune ligne de la méthode n'a été exécutée !");
Console.WriteLine();
Console.WriteLine("  Maintenant je la parcours :");

foreach (int n in sequence)
{
    Console.WriteLine($"       <- reçu {n}");
}
Console.WriteLine();
Console.WriteLine("  👉 L'exécution SAUTE entre la boucle et la méthode.");
Console.WriteLine();

// ─── 2. Ce que foreach fait vraiment ──────────────────────────────
Console.WriteLine("--- 2. foreach, décortiqué ---");

int[] petits = { 10, 20, 30 };
var curseur = ((IEnumerable<int>)petits).GetEnumerator();

Console.WriteLine("  var c = petits.GetEnumerator();");
while (curseur.MoveNext())
{
    Console.WriteLine($"  c.MoveNext() -> true, c.Current = {curseur.Current}");
}
Console.WriteLine("  c.MoveNext() -> false, on sort");
Console.WriteLine();
Console.WriteLine("  C'est EXACTEMENT ce que le compilateur écrit à ta place.");
Console.WriteLine();

// ─── 3. La paresse, mesurée ───────────────────────────────────────
Console.WriteLine("--- 3. La preuve de la paresse ---");
Console.WriteLine();

Espion.Reinitialiser();
var requete = Espion.Nombres(1000).MonWhere(n => n % 2 == 0).MonTake(3);
Console.WriteLine($"  Requête construite sur 1000 nombres.");
Console.WriteLine($"  Éléments réellement lus : {Espion.NombreDeLectures}");
Console.WriteLine();

var resultat = requete.ToList();
Console.WriteLine($"  Après .ToList() -> [{string.Join(", ", resultat)}]");
Console.WriteLine($"  Éléments réellement lus : {Espion.NombreDeLectures}");
Console.WriteLine();
Console.WriteLine("  👉 Pour sortir 3 nombres pairs, il n'en a lu que 5.");
Console.WriteLine("     Pas 1000. Les 995 autres n'ont jamais existé.");
Console.WriteLine();

// ─── 4. L'infini ──────────────────────────────────────────────────
Console.WriteLine("--- 4. Manipuler l'INFINI ---");
Console.WriteLine();

Console.WriteLine($"  Naturels().MonTake(10)");
Console.WriteLine($"    -> [{string.Join(", ", Generateurs.Naturels().MonTake(10))}]");
Console.WriteLine();

Console.WriteLine($"  Naturels().MonWhere(multiple de 7).MonTake(5)");
Console.WriteLine($"    -> [{string.Join(", ", Generateurs.Naturels().MonWhere(n => n % 7 == 0).MonTake(5))}]");
Console.WriteLine();

Console.WriteLine($"  Fibonacci().MonTake(12)");
Console.WriteLine($"    -> [{string.Join(", ", Generateurs.Fibonacci().MonTake(12))}]");
Console.WriteLine();

Console.WriteLine($"  Fibonacci().MonTake(50).Last()  (instantané !)");
var fib50 = Generateurs.Fibonacci().MonTake(50).ToList();
Console.WriteLine($"    -> {(fib50.Count > 0 ? fib50[fib50.Count - 1].ToString() : "(pas encore fait)")}");
Console.WriteLine();
Console.WriteLine("  🔴 Mais JAMAIS .ToList() sur Naturels() tout seul :");
Console.WriteLine("     le programme tournerait pour l'éternité.");
Console.WriteLine();

// ─── 5. Les méthodes d'extension ──────────────────────────────────
Console.WriteLine("--- 5. Les méthodes d'extension ---");
Console.WriteLine();
Console.WriteLine($"  \"bonjour\".Crier()  ->  {"bonjour".Crier()}");
Console.WriteLine();
Console.WriteLine("  On vient d'ajouter une méthode au type string,");
Console.WriteLine("  qui appartient pourtant à Microsoft. C'est exactement");
Console.WriteLine("  comme ça que LINQ s'accroche à toutes les collections.");
Console.WriteLine();

// ─── 6. La comparaison finale ─────────────────────────────────────
Console.WriteLine("--- 6. Ton LINQ vs le vrai ---");

int[] donnees = { 5, 12, 8, 130, 44, 3 };

Console.WriteLine($"  données         = [{string.Join(", ", donnees)}]");
Console.WriteLine($"  Le vrai LINQ    = [{string.Join(", ", donnees.Where(n => n > 4).Select(n => n * 2).Take(3))}]");
Console.WriteLine($"  Le TIEN         = [{string.Join(", ", donnees.MonWhere(n => n > 4).MonSelect(n => n * 2).MonTake(3))}]");
Console.WriteLine();
Console.WriteLine($"  Somme (vraie)   = {donnees.Aggregate(0, (t, n) => t + n)}");
Console.WriteLine($"  Somme (tienne)  = {donnees.MonAggregate(0, (t, n) => t + n)}");
Console.WriteLine();

Console.WriteLine("À toi ! MonLinq.cs puis Generateurs.cs 🔬");


// ═══════════════════════════════════════════════════════════════════
//  Les méthodes de la démo
// ═══════════════════════════════════════════════════════════════════

static IEnumerable<int> Bavarde()
{
    Console.WriteLine("    -> j'entre dans la méthode");
    yield return 1;
    Console.WriteLine("    -> je reprends après le 1er yield");
    yield return 2;
    Console.WriteLine("    -> je reprends après le 2e yield");
    yield return 3;
    Console.WriteLine("    -> j'ai fini");
}

/// <summary>Une méthode d'extension maison, sur string.</summary>
public static class Bonus
{
    public static string Crier(this string texte) => texte.ToUpper() + " !!!";
}
