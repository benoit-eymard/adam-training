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
    //  A.1 — Le verrou     ✅ DÉJÀ ÉCRIT, rien à faire ici
    //
    //  C'est l'objet sur lequel les threads vont faire la queue.
    //  Observe ses deux mots-clés, ils comptent autant l'un que l'autre :
    //
    //    - private  : personne d'autre ne peut s'en servir pour bloquer
    //                 ta classe pendant des secondes.
    //
    //    - readonly : il ne peut JAMAIS être remplacé. Si on pouvait le
    //                 changer en cours de route, deux threads
    //                 verrouilleraient deux objets DIFFÉRENTS et
    //                 passeraient tous les deux — la protection
    //                 s'évanouirait sans le moindre message d'erreur.
    //
    //  👉 Ton travail commence en A.2.
    // ───────────────────────────────────────────────────────────────

    private readonly object _verrou = new object();


    // ───────────────────────────────────────────────────────────────
    //  A.2 — La valeur actuelle
    //
    //  💡 public int Valeur => _valeur;
    // ───────────────────────────────────────────────────────────────

    // TODO: remplace le `0` de la ligne ci-dessous
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
