---
id: r21
title: Rencontre 21 - Sécuriser une application
sidebar_label: R21 - Sécuriser une application
draft: false
hide_table_of_contents: false
---

# Retour sur l'examen

Nous passerons à travers l'examen 2 pour donner un correctif à l'oral. 

Vous pourrez ensuite demander à revoir votre copie pendant la partie pratique.


# Sécuriser une application

Nous allons explorer l'application fournie pour le cours:
- l'application se trouve dans le repo du cours https://github.com/departement-info-cem/3U4-cybersec
- trouvez la section « Releases » 
- vous devriez trouver un fichier vers consoleApp.exe

Dans les 4 prochaines semaines, nous allons voir comment on peut attaquer un système puis le défendre sur 3 aspects:
- le cassage d'un mot de passe sur une BD fuitée
- le cassage d'une encryption sur une BD fuitée
- l'injection SQL sur l'application (sans BD fuitée)

## Exercice / activité: se familiariser avec l'application

1. Téléchargez le repo du cours [ici](https://github.com/departement-info-cem/3U4-cybersec/archive/refs/heads/main.zip)
2. Lancez l'application et utilisez-la.
  - créer un compte
  - se connecter / déconnecter
  - quitter puis la redémarrer
  - essentiellement tester toutes les possibilités de l'application
3. N'hésitez pas à poser des questions sur l'utilisation de l'application.

## Exercice: bouger l'application

1. Lancer l'application
2. Créer un compte et s'y connecter
3. Fermer l'application
4. Déplacer le .exe dans un dossier différent
5. Relancer l'application
6. Essayer de se connecter
7. Qu'est-ce qui se passe?

## Exercice sur la commande strings

Chaque application contient potentiellement des chaînes de caractères qui peuvent être intéressantes pour un attaquant.

Ce type de commandes peut permettre de comprendre quelle 

1. Téléchargez https://learn.microsoft.com/fr-ca/sysinternals/downloads/strings
2. Il s'agit d'une application qui cherche toutes les potentielles chaînes UTF-8 ou ASCII dans un exécutable
3. Créez-vous un fichier texte avec les chaînes trouvées dans une application.
4. Essayez de voir si vous pouvez trouver des chaînes utiles
5. Si ce n'est pas le cas, on peut essayer de chercher des options pour avoir un meilleur résultat:
  - `strings -n 8 consoleApp.exe` pour chercher des chaînes de 8 caractères ou plus
  - si vous cherchez une requête SQL, vous pouvez essayer de filtrer le résultat en filtrant sur `SELECT` ou `INSERT`
  - etc.

Vous pouvez tester cette technique sur quelques applications pour voir si vous trouvez des informations intéressantes.

## Exercice de décompilation

Un outil important pour attaquer ou valider la sécurité d'une application est le décompilateur ou désassembleur.

Pour pouvoir décompiler une application, il faut d'abord réussir à savoir avec quelle plateforme elle a été compilée.

1. Dans notre cas, nous pensons que ça a été fait avec .NET
2. On va utiliser dotPeek pour décompiler l'application
3. Téléchargez dotPeek 
4. Essayez de décompiler l'application fournie
5. Familiarisez-vous avec l'interface de dotPeek
6. Essayez de retrouver le code important de l'application

## Partir votre TP

Pour ce TP, vous allez devoir:
- effectuer des attaques sur l'application
- modifier le code pour sécuriser l'application

Pour cela, aujourd'hui vous allez devoir:
1. Créer votre repo en utilisant le lien envoyé par votre prof
2. Créer votre fichier rapport.md où vous documenterez les attaques
3. Copier le projet de l'application dans votre repo. C'est là que vous mettrez les correctifs.
4. Vous trouverez le projet de l'application [ici](https://github.com/departement-info-cem/3U4-cybersec/)
  - Cherchez un peu et vous devriez trouver le projet consoleApp
  - Si vous ne trouvez pas, demandez à votre prof



