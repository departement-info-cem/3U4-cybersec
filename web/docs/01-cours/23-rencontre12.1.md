---
id: r23
title: Rencontre 23 - Hachage et fonction à sens unique
sidebar_label: R23 - Hachage et fonction à sens unique
draft: false
hide_table_of_contents: false
---

# Le hachage, une mesure de second rideau

## En quoi cela sécurise? (5 minutes) 

https://fr.wikipedia.org/wiki/Fonction_à_sens_unique

Après avoir lu, la page wikipedia, par équipe de 4, faites une phrase qui explique en quoi cela sécurise le mot de passe?
```




```

## Pour quoi ça s'utilise

- On n'a pas besoin de l'information fournie pour l'application
- On a juste besoin de vérifier que c'est la même info que celle fournie à l'inscription

## Comment ça s'implante en code

:warning: **Ne jamais implanter une fonction de crypto soi-même**: On n'a pas encore le niveau!

https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm?view=net-8.0

L'algorithme le plus classique pour nous SHA256:
- l'implantation renvoit souvent le hash sous forme de byte[]
- on peut ensuite le convertir en string pour le stocker dans la BD en base64
- une petite rechercher sur comment on convertit un byte[] en string en base64 devrait suffire

## Activité Attends une minute, c'est quoi l'avantage de SHA256 sur MD5

- prenez quelques mots de passe simples et produisez les hachages avec MD5 et SHA256 (vous trouverez un site web pour le faire)
- prenez quelques mots de passe plus complexes et produisez les hachages avec MD5 et SHA256
- essayez ensuite de voir quels hachages vous pouvez retrouver avec crackstation
- voyez-vous une différence selon l'algorithme de hachage?
- si je devine le mot de passe comme pour bob dans le TP2, est-ce que l'algorithme de hachage change quelque chose?

### la grosse question, contre quelles attaques nous protège le changement de md5 vers SHA256

- crackstation : non, ils ont aussi les hachages pour tous les algos de hachage classique, ça ne change rien
- deviner le mot de passe en connaissant des infos persos : non plus, le mot de passe reste le même, juste le hachage qui change

Alors donc:
- les hachages sont plus longs donc la probabilité d'une collision (2 mots de passe qui donnent le même hachage) est plus faible
- ce n'est pas tant différent

## Est-ce qu'on veut saler notre mot de passe?

Pour rappel, saler un mot de passe c'est:
- concaténer le mot de passe fourni par l'utilisateur avec une chaîne si possible complexe
- ensuite on hash
- ensuite on stocke dans la BD
- si le sel est spécifique à chaque utilisateur, il faudra alors le stocker dans la BD également

## bcrypt, le standard de l'industrie et pourquoi

- bcrypt ajoute un **salt** tout seul sur BlowFish 
  - PROTECTION CONTRE un mot de passe utilisateur simple
- bcrypt s'adapte aux ressources de calcul qui augmentent et peut devenir de plus en plus difficile à calculer
  - PROTECTION CONTRE une attaque en force brute où on calcule plein de hachages > ça devient impossiblement long

Wow wow, comment ça il ajoute automatiquement un hachage?
- en fait on tire au hasard un salt, on calcule un hachage
- on stocke en clair le salt dans le résultat avec le hachage
```
22 character salt (en clair) and 31 character hash
```
- quand on veut comparer plus tard, on peut recalculer puisqu'on a le salt en clair

Explication de comment BCrypt marche [bcrypt](https://en.wikipedia.org/wiki/Bcrypt)

https://www.youtube.com/watch?v=O6cmuiTBZVs

## Pour le reste du cours

Travailler à implanter Bcrypt dans l'application fournie. Vous devrez sans doute:
1. chercher une librairie qui implante bcrypt
2. ajouter un paquet nuget dans votre solution
3. changer le code pour utiliser la librairie

Pour toutes ces étapes, essayez de le faire seul. Par contre, n'oubliez que le prof est toujours là pour
débloquer les situations.

## Aussi : utilisation de hachage pour détecter une attaque

1. Bob qui développe une page web utilise un CDN pour charger jquery 1.8
2. il ajoute donc une balise **&lt;script src="https://supercdn.com/1.8/jquery.min.js" &gt;&lt;/script&gt;**
3. il a un peu peur qu'un jour quelqu'un pirate le site supercdn.com et modifie le code de jquery.min.js pour faire des choses malveillantes
4. arrive alors l'attribut integrity
5. on y met le hachage du code source correct
6. si un jour le navigateur télécharge le fichier, calcule le hachage et voit qu'il ne correspond pas, il n'exécute pas le code

Dans ce cas, une attaque devient beaucoup plus compliquée:
1. le pirate doit prendre le contrôle du site supercdn
2. le pirate doit effectuer une modification qui:
  - fait le truc méchant voulu en étant du javascript valide
  - donne le même hachage que le code d'origine

**ici se trouve la grande fragilité de MD5**:
- on n'a pas d'attaque de type préimage sur MD5, c'est-à-dire qu'on ne sait pas facilement trouver une chaîne qui donne un hachage donné
- par contre, il y a des attaques qui permettent de compléter un fichier pour qu'il donne le même hachage donc:
  - je peux modifier le fichier jquery.min.js pour faire un truc vraiment pas cool
  - ajouter un début de commentaire puis créer une chaîne dans le commentaire qui va faire que ça donnera le même hachage
  - le navigateur va télécharger le fichier, calculer le hachage et voir que c'est bon
  - et le pirate a réussi à injecter un fichier js malicieux sans être détecté

MORALITÉ: **ne jamais utiliser MD5 pour des hachages de sécurité, ce qui s'est traduit par ne jamais utiliser MD5**




