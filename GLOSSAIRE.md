# 📚 Glossaire

Le vocabulaire du C#, expliqué simplement. Reviens ici dès qu'un mot te bloque.

---

## Les bases

**Variable** — une boîte étiquetée qui contient une valeur.
`int age = 13;` → une boîte nommée `age` qui contient `13`.

**Type** — la nature de ce qu'on met dans la boîte : un nombre entier (`int`),
du texte (`string`), un vrai/faux (`bool`)... En C#, une boîte a un type fixé pour toujours.

**Déclarer** — créer la boîte. **Affecter** — y mettre une valeur.

**Constante (`const`)** — une boîte scellée : sa valeur ne peut plus changer.

**Instruction** — une ligne d'ordre donnée au programme. Elle finit par `;`.

**Bloc** — un groupe d'instructions entre accolades `{ }`.

**Commentaire** — du texte ignoré par la machine, écrit pour les humains.
`// sur une ligne` ou `/* sur plusieurs */`

**Compiler** — traduire ton C# en langage machine. S'il y a une faute, le
compilateur refuse et affiche une **erreur de compilation**.

---

## Le flux d'exécution

**Condition (`if`)** — « si ceci est vrai, alors fais cela ».

**Booléen (`bool`)** — une valeur qui vaut soit `true`, soit `false`.

**Boucle** — répéter des instructions.
`for` quand on sait combien de fois, `while` quand on répète tant qu'une condition
est vraie, `foreach` pour parcourir une collection.

**Itération** — un tour de boucle.

**`break`** — sortir de la boucle immédiatement.
**`continue`** — passer directement au tour suivant.

---

## Organiser le code

**Méthode** (ou fonction) — un bloc de code nommé, qu'on peut appeler autant de
fois qu'on veut. Elle peut prendre des **paramètres** (des entrées) et **retourner**
une valeur (une sortie).

**Paramètre** — la variable déclarée dans la définition de la méthode.
**Argument** — la valeur réelle qu'on passe au moment de l'appel.

**`void`** — une méthode qui ne retourne rien (elle *fait* quelque chose au lieu de
*calculer* quelque chose).

**Signature** — le nom d'une méthode + la liste des types de ses paramètres.

**Surcharge (overload)** — plusieurs méthodes du même nom, avec des paramètres
différents.

**Portée (scope)** — la zone où une variable existe. Hors de son bloc `{ }`, elle
n'existe plus.

**Tableau (`array`)** — une série de cases numérotées, de taille fixe.
`int[] scores = new int[5];` — les indices vont de `0` à `4`.

**Index** — la position dans un tableau. **Ça commence à 0**, toujours.

---

## L'objet

**Classe** — le plan de construction. Décrit ce qu'un objet *a* et ce qu'il *sait faire*.

**Objet / Instance** — un exemplaire concret fabriqué à partir du plan.
La classe `Personnage` est le plan ; `heros` est un objet.

**`new`** — le mot-clé qui fabrique un objet à partir d'une classe.

**Champ (field)** — une variable qui appartient à un objet.

**Propriété (property)** — un champ avec un portail d'entrée (`set`) et de sortie
(`get`), qui permet de contrôler les accès.

**Constructeur** — la méthode spéciale appelée au moment du `new`. Elle prépare
l'objet.

**Encapsulation** — cacher l'intérieur de l'objet et n'exposer que ce qui est utile.
`private` cache, `public` expose.

**Héritage** — une classe qui reprend tout d'une autre et y ajoute sa spécialité.
`Guerrier` hérite de `Personnage`.

**Classe de base / classe dérivée** — le parent et l'enfant.

**Polymorphisme** — plusieurs objets différents répondent au même ordre, chacun à sa
façon. On dit `Attaquer()` à un Mage et à un Guerrier : les deux comprennent, mais
ne font pas la même chose.

**`virtual` / `override`** — le parent autorise (`virtual`), l'enfant redéfinit (`override`).

**`abstract`** — une classe ou méthode incomplète, qu'on est obligé de compléter
dans les classes filles. On ne peut pas faire `new` sur une classe abstraite.

**Interface** — un contrat : la liste de ce qu'une classe doit savoir faire, sans
dire comment. Par convention son nom commence par `I` : `ISoignable`.

**`this`** — « l'objet en cours », celui sur lequel la méthode est appelée.

**`base`** — « la version du parent ».

**`static`** — appartient à la classe elle-même, pas à un objet particulier.

**`null`** — « rien du tout ». Une référence qui ne pointe sur aucun objet.
Source d'erreur numéro 1 (`NullReferenceException`).

---

## Collections & données

**`List<T>`** — un tableau élastique : il grandit et rétrécit tout seul.

**`Dictionary<TCle, TValeur>`** — un annuaire : à chaque clé correspond une valeur.

**Générique (`<T>`)** — un type paramétrable. `List<int>`, `List<string>`... un seul
code, plein d'usages.

**Exception** — une erreur qui survient *pendant* l'exécution.

**`try` / `catch`** — « essaie ceci ; si ça explose, fais cela ».

**Sérialiser** — transformer un objet en texte (souvent du JSON) pour le sauvegarder.
**Désérialiser** — l'opération inverse.

---

## Lambda & LINQ

**Délégué (`delegate`)** — une variable qui contient... une méthode. Ça permet de
passer un comportement en paramètre.

**`Func<...>`** — un délégué qui retourne une valeur.
**`Action<...>`** — un délégué qui ne retourne rien.

**Lambda** — une mini-méthode écrite à la volée, sans nom.
`x => x * 2` se lit « x donne x fois 2 ».

**LINQ** — un langage de requête sur les collections.
`.Where()` filtre, `.Select()` transforme, `.OrderBy()` trie.

**Exécution différée** — une requête LINQ ne calcule rien tant qu'on ne lit pas son
résultat. Appeler `.ToList()` force le calcul.

---

## Threading & async

**Thread** — un fil d'exécution. Par défaut ton programme n'en a qu'un.

**Task** — une promesse de résultat : « ce travail est lancé, il finira plus tard ».

**`async` / `await`** — `async` marque une méthode qui sait attendre,
`await` dit « attends ce résultat sans bloquer le reste ».

**Synchrone** — une chose après l'autre.
**Asynchrone** — on lance et on continue, sans attendre bêtement.

**Parallélisme** — plusieurs calculs réellement en même temps, sur plusieurs cœurs.

**Concurrence** — plusieurs tâches en cours en même temps (pas forcément simultanées).

**Race condition** — deux threads touchent la même donnée en même temps, et le
résultat devient imprévisible. Le bug le plus sournois qui existe.

**`lock`** — un verrou : un seul thread à la fois peut entrer dans ce bloc.

**Deadlock** — deux threads s'attendent mutuellement, pour toujours. Tout est figé.

**Thread-safe** — se dit d'un code qui reste correct même si plusieurs threads
l'utilisent en même temps.
