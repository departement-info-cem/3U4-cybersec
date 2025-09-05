# Injections pour l'application exemple


## Base d'injection pour **batch mode**

Rend le SQL valide et permet d'injecter une autre requête :
- on n'a pas besoin que la requête d'origine fonctionne
- juste qu'elle soit syntaxiquement valide
- pour pouvoir exécuter notre code après
- par contre on est aveugle du résultat

### Concrètement
1. dans l'appli, choisissez connexion
2. dans le champ nom, tapez le SQL suivant
```sql
'; --
```
3. Complétez avec un mot de passe "toto"

La requête complète devient alors :
```sql
SELECT * FROM MUtilisateur WHERE nom = ''; --' AND motDePasse = 'toto'
```
- la première requête SQL est **SELECT * FROM MUtilisateur WHERE nom = '';**
- le "--" est un commentaire en SQL, donc tout ce qui suit est ignoré 
- on peut donc avoir quelque chose qui est du SQL valide
- on a créé un espace entre ; et -- pour pouvoir taper n'importe quelle commande SQL !

### Injection destructrice

1. dans l'appli, créez quelques comptes normalement (pour avoir des données)
2. vous pouvez aller regarder ce qu'il y a dedans avec DataGrip
3. dans l'appli, choisissez connexion
4. dans le champ nom, tapez le SQL suivant
```sql
';    DROP TABLE IF EXISTS MNote; DROP TABLE IF EXISTS MUtilisateur;    --
```
5. Complétez avec un mot de passe quelconque de 4 caractères ou plus

Le SQL devient :
```sql
SELECT * FROM MUtilisateur WHERE nom = '';    DROP TABLE IF EXISTS MNote; DROP TABLE IF EXISTS MUtilisateur;    --' AND motDePasse = 'toto'
```

On a donc :
- une première requête valide qui ne retourne rien, la fonction de l'application ne marchera pas, mais ce n'est pas important dans ce contexte
- la deuxième requête est notre réel objectif, elle va supprimer toutes les données
- le commentaire permet d’ignorer la fin de la première requête


## Modifier une requête légitime

On va essayer de se connecter en tant que n'importe quel utilisateur pour tromper le controle d'accès.

On voudrait se connecter en tant que "alice" sans connaitre le mot de passe.
1. dans l'appli, créez plusieurs comptes dont un "alice"
2. dans l'appli, on déconnecte puis on choisi connexion
3. dans le champ nom tape le SQL suivant
```sql
alice'; --
```
4. dans le mot de passe on tape ce qu'on veut

```sql
SELECT * FROM MUtilisateur WHERE nom = 'alice';--' AND motDePasse = 'toto'
```

En complétant la requête et en ignorant le test sur le mot de passe, on rentre dans n'importe quel compte.

## recuperer le resultat d'une requete

### Injection avec une union

Les injections précédentes mais pas de collecter de l'information. 

Une injection avec union permet de :
1. exécuter une sous-requête
2. prendre le résultat de cette sous-requête 
3. faire l'union avec le résultat de la requête d'origine
4. et du coup avoir dans notre entrée de BD le résultat de la sous-requête
5. si l'application nous affiche ensuite ce champ de donnée on a accompli
  - une technique qui permet de taper une commande
  - ensuite lire de résultat

Les conditions:
- il faut trouver une action SQL qui modifie la BD
- monter une union syntaxiquement correcte

Dans notre application, notre cible va être la création d'un compte.
```sql
bob', 'password') UNION SELECT (SELECT GROUP_CONCAT(sql, '; ') AS creation_statements FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL),sqlite_version(); --
```
- ce que je veux exécuter c'est **(SELECT GROUP_CONCAT(sql, '; ') AS creation_statements FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL)**
- ça me permet de voir le schéma de la base de données et donc toute sa structure pour pouvoir faire des requêtes plus précises
- le union permet de créer un autre tuple dans le résultat de la requête d'origine et de le faire créer
- si j'ai un moyen de voir ce résultat depuis l'application, j'ai gagné
- ici on va simplement demander la liste des utilisateurs.


### Attends mais il y a d'autres solutions pour ça non?

- si j'ai trouvé un spot d'injection, je peux exécuter n'importe quelle commande SQL
- je pourrais trouver une table que je vois ailleurs dans l'appli comme ici mes notes
- monter une base de requête SQL qui permet d'exécuter une commande, prendre le résultat et créer une note avec
- et hop je peux lire le résultat

#### Attaque

Etant donné une commande SQL X que je veux exécuter. Dans mon exemple, j'utiliserai:
```sql
(SELECT GROUP_CONCAT(sql, '; ') AS creation_statements FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL)
```

Attaque pour la commande :
1. je me crée un compte comme d'habitude
2. en listant les utilisateurs, je retrouve mon **ID**, dans mon exemple on va dire que c'est 4
3. je crée un compte avec le nom
```sql 
alice','password'); INSERT INTO MNote (note, utilisateurID) VALUES((X)), ID);-- 
```
4. concretement pour mon exemple, ça donne
```sql
alice','password'); INSERT INTO MNote (note, utilisateurID) VALUES((SELECT GROUP_CONCAT(sql, '; ') AS creation_statements FROM sqlite_master WHERE type = 'table' AND sql IS NOT NULL), 4);-- 
```
5. la création du compte a sans doute échoué, mais on s'en fout
6. en se connectant sur mon compte
7. je peux aller lire mes notes
8. le résultat de la requête sql est dans une note
