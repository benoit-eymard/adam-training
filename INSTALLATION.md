# 🛠️ Installation des outils

Tu as besoin de **deux choses** : le SDK .NET (pour exécuter du C#) et VS Code
(pour écrire le code). Compte 10 minutes.

---

## 1. Le SDK .NET

Le SDK, c'est le moteur : il transforme ton code C# en programme qui s'exécute.

1. Va sur **https://dotnet.microsoft.com/download**
2. Télécharge **.NET 8.0 SDK** (bouton "Download .NET SDK x64")
3. Lance l'installateur, clique "Suivant" jusqu'au bout.

### Vérifier que ça marche

Ouvre un terminal (touche Windows, tape `powershell`, Entrée) et tape :

```bash
dotnet --version
```

Tu dois voir un numéro de version s'afficher (par exemple `8.0.203`).
Si tu vois une erreur du type « commande introuvable », **redémarre ton PC** — le
terminal n'a pas encore vu la nouvelle installation.

---

## 2. Visual Studio Code

C'est ton éditeur de texte pour programmeur.

1. Va sur **https://code.visualstudio.com/**
2. Télécharge et installe.

### L'extension C# (indispensable)

Sans elle, VS Code ne comprend pas le C#.

1. Ouvre VS Code
2. Clique sur l'icône des extensions dans la barre de gauche
   (les 4 carrés) — ou appuie sur `Ctrl+Shift+X`
3. Cherche **"C# Dev Kit"** (éditeur : Microsoft)
4. Clique sur **Install**

Elle installe automatiquement l'extension "C#" dont elle dépend.

### Extensions bonus (confort)

- **French Language Pack** — met VS Code en français
- **Error Lens** — affiche les erreurs directement à côté de la ligne fautive.
  Très pratique quand on débute.

---

## 3. Ouvrir le cours

Dans VS Code : `Fichier` → `Ouvrir un dossier...` → choisis le dossier
`TrainingC#`.

Pour ouvrir un terminal **directement dans VS Code** : `Ctrl+ù`
(ou menu `Terminal` → `Nouveau terminal`). C'est là que tu taperas
`dotnet run` et `dotnet test`.

---

## 4. Test final

Dans le terminal de VS Code, tape :

```bash
cd "Modules/01-Premiers-Pas"
```

```bash
dotnet run --project Exercices
```

Si un programme se lance et t'affiche du texte : **tout est prêt.** 🎉

---

## 5. Et ensuite ?

- [GIT.md](GIT.md) — sauvegarder ton travail (**à lire avant de coder**)
- [DEBUG.md](DEBUG.md) — le débogueur de VS Code. Tu peux le garder pour le
  module 2, mais n'attends pas plus : c'est ce qui t'évitera des heures de
  `Console.WriteLine`.

---

## 🆘 Problèmes fréquents

**« dotnet n'est pas reconnu »**
→ Redémarre le terminal. Si ça persiste, redémarre le PC.

**VS Code ne propose pas l'autocomplétion C#**
→ L'extension C# Dev Kit n'est pas installée ou pas encore chargée.
Vérifie dans les extensions, puis `Ctrl+Shift+P` → `Developer: Reload Window`.

**« Le chemin spécifié est introuvable »**
→ Tu n'es pas dans le bon dossier. Tape `pwd` pour voir où tu es,
et `ls` pour voir ce qu'il y a autour.

**Le terminal affiche plein de texte rouge**
→ Lis la **première** erreur, pas la dernière. Les suivantes sont souvent des
conséquences de la première.
