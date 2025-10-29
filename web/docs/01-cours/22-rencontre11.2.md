---
id: r22
title: Rencontre 22 - Hachage (attaque)
sidebar_label: R22 - Hachage (attaque)
draft: false
hide_table_of_contents: false
---
# Hachage (attaque)

Dans notre application, la BD peut éventuellement fuiter:
- on a mal protégé un serveur qui héberge la BD
- un employé fâché a copié la BD sur une clé USB et l'a emportée
- on a revendu un serveur de sauvegarde sans effacer les données et il y a une sauvegarde de la BD d'il y a un mois
- etc.

Pour le cas du TP3, la BD est directement dans un fichier quelque part sur l'ordinateur.

## Activité : Mais où est la BD?

Après avoir lancé l'application:
1. partez l'application
2. créez quelques utilisateurs etc.
3. dans les fichiers du projet, essayez de trouver dans quel fichier se trouve la BD

## Activité : Ouvrir une BD

Une fois qu'on a trouvé le fichier d'une BD, on veut trouver une application qui permet de l'ouvrir.

**DataGrip** est une application qui va pouvoir ouvrir une BD même si vous ne connaissez pas son format.

1. Téléchargez DataGrip soit par JetBrains Toolbox ou directement sur le site de JetBrains
2. Ouvrir / Lancer DataGrip
3. Ouvrir le fichier de la BD que vous avez trouvé

Vous devriez maintenant savoir quel moteur de base de données est utilisé et voir les tables.

Vous pouvez prendre en notes vos manipulations pour les inclure dans votre rapport du TP3.

## Activité: sortir les hachages

Vous allez trouver dans le dossier stock du repo un fichier appelé **leaked.db**.

En utilisant une application pour ouvrir la BD, vous pouvez l'ouvrir et la parcourir.

En regardant la bonne table vous allez trouver les hachages des mots de passe.

Essayez de craquer ces mots de passe et prenez en note les mots de passe que vous avez réussi à récupérer.

## Avancer mon TP3

Vous devriez maintenant avoir une piste pour attaquer les mots de passe des premiers ministres.





