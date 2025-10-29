---
id: r25
title: Rencontre 25 - Chiffrement symétrique (implantation)
sidebar_label: R25 - Chiffrement symétrique (implantation)
draft: false
hide_table_of_contents: false
---

## Ne pas coder de la crypto soi-même

Vous n'avez sans doute pas le temps ni le niveau pour coder correctement de la crypto symétrique.

Le problème peut être l'algo qui est parfois naïf. Ou alors la mise en oeuvre qui plante.

## Implanter un algo classique via une librairie

Implanter le code C# pour faire la crypto symétrique est assez simple, on peut:
- chercher un peu dans la doc officielle : lien
- chercher sur Google et sauter sur le premier StackOverflow qui a de l'allure
- demander à ChatGPT « comment chiffrer avec BlowFish en .NET »
- demander à Github Copilot direct dans mon VisualStudio

Il nous reste à choisir une clé de chiffrement pour la plupart des algorithmes:
- il faut que la clé soit difficile à deviner (un pirate pourrait essayer des clés comme il essaie des mots de passe)
- il faut s'assurer qu'on stocke la clé dans un endroit peu accessible

## Décompiler le .exe et trouver la clé


> Les solutions de cybersec, Jean-Louis et fils propose une solution de cybersécurité
> ultra sophistiquée de cybersécurité qui se présente en 2 applications ultra sécures
> 
> 1. Notre application utilisateur permet de chiffrer avec une clé de 2048 bits (ça fait beaucoup) vos mots de passe afin de les stocker de façon sécuritaire. Même si un pirate accède à votre ordi il ne pourra pas décoder les mots de passe stockés. Nous utilisons un algo à la pointe du progrès BlowFish par l'immense star de la crypto: Bruce Schneier
> 
> 2. Notre application gardée dans un lieu hyper sécuritaire nous permet si vous oubliez un mot de passe de le retrouver en nous contactant par courriel.


Nous avons acheté la super appli et nous avons chiffré quelques mots de passe pour s'en souvenir.

L'appli se trouve dans le dossier **sym** JeanLouisEtFils du repo. Vous pouvez télécharger les fichiers exécutables dans la section [**Releases**](https://github.com/departement-info-cem/3U4-cybersec/releases/tag/JeanLouisEtFils).

- Essayez avec la commande **strings** pour trouver une chaîne dans un exécutable
- Essayez dotPeek pour décompiler l'appli
- La clé doit se trouver dans le code quelque part, explorez l'application pour trouver où se trouve la clé
de chiffrement. La bonne nouvelle c'est que comme BlowFish est un algo symétrique, c'est aussi la clé
de déchiffrement

## Décrypter les données

C'est la même clé pour chiffrer et déchiffrer. Donc si on connaît la clé qui permet de chiffrer, on a tout ce qu'il faut pour déchiffrer.

On peut alors essayer de programmer un outil servant à décrypter les données avec le même algorithme, la même clé et les mêmes paramètres.
- programmer une application qui Decrypt avec BlowFish et la clé qu'on a trouvée
- passer à travers le fichier ligne par ligne et décrypter les mots de passe

:::tip
Au lieu de programmer un décrypteur vous-mêmes, vous pouvez utiliser un outil déjà fait. Il suffit de ressortir tous les paramètres. Dans le cas de Blowfish, on a:
- La clé symétrique
- Le vecteur d'initialisation (iv)
- Le mode de chiffrement (cipher)
- La méthode de remplissage (padding)

Voici un site qui permet de paramétrer l'algorithme: https://www.toolhelper.cn/en/SymmetricEncryption/Blowfish
:::


Pour valider tu peux utiliser l'application corpoDecryptor dans le dossier **sym**.

## Chiffrement asymétrique

Utiliser un algo asymétrique avec une clé publique de chiffrement et une privée pour le déchiffrement:
- l'application peut chiffrer les mots de passe avec la clé publique >> voir JeanLouisEtFils dans le dossier **asym**
- le déchiffrement nécessite une clé privée >> voir corpoDecryptor dans le dossier **asym**
- la clé privée est plus complexe et en général reliée par un problème de calcul très difficile 
- on utilise ici l'algorithme RSA

Exemple de lien entre clé privée et publique:
- on tire 2 nombres premiers très grands x et y
- la clé publique est **""+x*y**
- la clé privée est **x +":"+y**
- il est très difficile de trouver x et y à partir de x*y mais très facile de vérifier que x*y est bien égal à la clé publique

## Quelles solutions pour cacher la clé

1. Principe #1, la clé ne doit pas être accessible aux utilisateurs. Il faut donc que la partie de la plateforme
qui chiffre se trouve dans les infrastructures de l'entreprise, pas chez les utilisateurs
2. Principe #2, moins il y a de gens qui ont la clé, moins il y a de chances de fuites.
3. On peut donc imaginer que seul le responsable du système en prod a accès à la clé de prod. Les dév peuvent 
avoir accès à une clé différente sur leur environnement de développement.

### Le cas classique du client-serveur sur l'aspect cryptographie 

1. L'application cliente envoie les données au serveur, la sécurité des données repose sur HTTPS (crypto asymétrique).
2. Le serveur chiffre (symétrique) les données avant de les stocker en BD, la sécurité repose sur le secret de la clé.


