namespace Module08;

/// <summary>
/// ═══════════════════════════════════════════════════════════════
///  EXERCICE A ⭐⭐⭐ — Le compteur thread-safe
///
///  Cette classe contient DEUX méthodes d'incrémentation :
///    - une NON sécurisée (déjà écrite, volontairement buguée)
///    - une sécurisée (à toi de l'écrire)
///
///  La démo te montrera la différence en direct. 😱
/// ═══════════════════════════════════════════════════════════════
/// </summary>
public class Compteur
{
    private int _valeur = 0;


    // ───────────────────────────────────────────────────────────────
    //  A.1 — Le verrou
    //
    //  Déclare un objet privé qui servira de verrou.
    //
    //  ⚠️ Il doit être :
    //     - private   : personne d'autre ne doit pouvoir s'en servir
    //     - readonly  : il ne doit JAMAIS être remplacé (sinon deux
    //                   threads verrouilleraient des objets différents,
    //                   et la protection ne servirait plus à rien)
    //
    //  💡 private readonly object _verrou = new object();
    // ───────────────────────────────────────────────────────────────

    // TODO: déclare _verrou
    private readonly object _verrou = new object();


    // ───────────────────────────────────────────────────────────────
    //  A.2 — La valeur actuelle
    //
    //  💡 public int Valeur => _valeur;
    // ───────────────────────────────────────────────────────────────

    // TODO:
    public int Valeur => 0;


    // ─── DÉJÀ ÉCRIT — la version BUGUÉE ───────────────────────────
    //
    //  _valeur++ a l'air atomique, mais le processeur fait 3 choses :
    //     1. LIRE la valeur
    //     2. AJOUTER 1
    //     3. ÉCRIRE le résultat
    //
    //  Si deux threads lisent AVANT que l'autre ait écrit, un des
    //  deux incréments est perdu. C'est la RACE CONDITION.
    public void IncrementerNonSecurise()
    {
        _valeur++;
    }


    // ───────────────────────────────────────────────────────────────
    //  A.3 — La version SÉCURISÉE
    //
    //  Incrémente _valeur en garantissant qu'un seul thread à la fois
    //  exécute l'opération.
    //
    //  💡 lock (_verrou)
    //     {
    //         _valeur++;
    //     }
    //
    //  ⚠️ Le bloc lock doit être le PLUS COURT POSSIBLE : pendant
    //     qu'un thread est dedans, tous les autres font la queue.
    // ───────────────────────────────────────────────────────────────
    public void IncrementerSecurise()
    {
        // TODO:
    }


    // ───────────────────────────────────────────────────────────────
    //  A.4 — Réinitialiser
    //
    //  Remet le compteur à zéro. Doit être protégé lui aussi !
    //
    //  ⚠️ Si une seule opération oublie le verrou, toute la protection
    //     s'effondre. TOUS les accès à la donnée partagée doivent
    //     passer par le MÊME verrou.
    // ───────────────────────────────────────────────────────────────
    public void Reinitialiser()
    {
        // TODO:
    }
}
