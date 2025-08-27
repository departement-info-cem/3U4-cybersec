---
id: r04
title: Rencontres 4 et 5 - Ateliers
sidebar_label: R04 - Ateliers
draft: false
hide_table_of_contents: false
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

:::note Plan de la rencontre

Les séances 4 et 5 sont consacrées à des ateliers en équipe de 4 personnes:
- RubberDucky : vous utiliserez une clé USB active pour attaquer un poste de travail
- Key logger : vous utiliserez un keylogger pour attaquer un poste de travail
- Hiren : vous utiliserez une clé USB externe pour démarrer un poste de travail, changer un mot de passe, 
- Flipper Zero : vous utiliserez un Flipper Zero pour attaquer un poste de travail

Votre équipe doit compléter les quatre ateliers. Vous pouvez choisir l'ordre dans lequel vous les réalisez.

Le matériel est limité

:::


## Constitution des équipes

Nous vous recommandons de joindre 2 équipes de 2 personnes faisant leur TP ensemble pour
former une équipe de 4 personnes.

Le matériel est limité. Pendant les temps morts, vous pourrez avancer votre TP1.

## RubberDucky (environ 20 minutes par équipe)

### Matériel:
- 1 clé USB RubberDucky (voir avec le prof l'ordre pour les équipes)
- vos postes de travail: les injections proposées par le prof ne font aucun dommage
- si la clé USB a été modifiée, demande au prof de la restaurer

### Mise en contexte :
Le RubberDucky ressemble à une clé USB ordinaire, mais permet en fait d'injecter un script sur sa victime. Une fois branchée dans un ordinateur, celle-ci sera détectée par l'ordinateur comme un clavier, permettant ainsi de forcer la victime a exécuté du code arbitraire.

#### Découverte du fonctionnement
Le RubberDucky possède **deux modes** de fonctionnement;
- Le mode **Clé USB** : Permets d'ajouter, modifier ou supprimer le script qui sera exécuté par l'appareil
- Le mode **Attaque / Injection** : Permets d'exécuter le script lorsque la clé USB est branchée dans un appareil.

Dès que le RubberDucky est connecté à un appareil, il passe automatiquement en mode **Attaque / Injection**. Pour basculer vers le mode **Clé USB,** il suffit d'appuyer sur le bouton caché à l'intérieur du RubberDucky (allez voir le prof si vous ne trouvez pas le bouton en question). Si le RubberDucky n'a pas de script nommé **inject.bin** à la racine ou si le fichier de script est corrompu, il basculera automatiquement en mode **Clé USB** lorsqu'il sera branché.

### À faire : 
L'exercice consiste à simplement attaquer un poste de travail à l'aide de la clé USB, puis de remettre l'ordinateur dans son état initial. Lors de l'exercice, la moitié des membres de l'équipe joueront le rôle des attaquants, et le reste jouera le rôle des victimes.

#### Phase 1
##### L'attaquant (Ordinateur 1) doit : 
1. Connecter le RubberDucky sur son poste en mode **Clé USB**.
2. Parcourir l'arborescence de l'appareil et trouver le fichier de script nommé **SwapScreen.bin**.
3. Copier le script vers la racine du RubberDucky et renommer le fichier en **inject.bin**.
4. Éjecter le RubberDucky de façon sécuritaire (Clic droit sur l'appareil, et choisir **Ejecter**).

##### La victime (ordinateur 2) doit :
1. Connecter le RubberDucky sur son poste.
2. ~~Subir les conséquences de sa naïveté.~~ Comprendre la gravité de vos actions.
3. Basculer le RubberDucky en mode **Clé USB** et supprimer le fichier **inject.bin**.

#### Phase 2
##### L'attaquant (Ordinateur 1) doit : 
1. Connecter le RubberDucky sur son poste en mode **Clé USB**.
2. Parcourir l'arborescence de l'appareil et trouver le fichier de script nommé **SwapBackScreen.bin**.
3. Copier le script vers la racine du RubberDucky et renommer le fichier en **inject.bin**.
4. Éjecter le RubberDucky de façon sécuritaire (Clic droit sur l'appareil, et choisir **Ejecter**).

##### La victime (ordinateur 2) doit :
1. Connecter le RubberDucky sur son poste.
2. Comprendre que l'attaquant a été gentil avec vous et a rétabli votre poste de travail dans son état initial.

### Bonus: Écrire votre propre Script avec l'IDE
Si le groupe suivant n'a toujours pas terminé son atelier, vous pouvez essayer d'écrire votre propre script **Malicieux**. Pour cela, vous devez :

1. Visitez le site de RubberDucky pour utiliser leur [IDE en version gratuite](https://payloadstudio.hak5.org/community/).
2. Jouez avec l'outil et apprenez le langage de script "DuckyScript".
3. Exportez votre script au format **.bin** et remplacez le fichier à la racine du RubberDocky pour que votre script s'exécute (Reprodusez les mêmes étapes que lors de l'exercice).

ATTENTION: Ne pas prendre du code d'internet que vous ne comprenez pas, il pourrait être très dangereux d'exécuter du code provenant d'internet. Le RubbberDucky exécuterait le code comme s'il s'agit de vos actions, vous pourriez donc être accusé d'avoir vous-même exécuté du code malveillant.


## Key logger (environ 20 minutes par équipe)

1. Demandez au professeur un keygrabber. Il y a 2 keyloggers:
    - Le pico qui fait à peu près 1 pouce de long
    - Le régulier qui fait environ 2 pouces de long
2. Séparez votre équipe en deux:
    - Équipe jaune
    - Équipe verte

ATTENTION: nous allons utiliser les postes de travail du collège pour cet atelier.
Il est très important de remettre les connexions USB comme vous les avez trouvées après l'atelier.

### Découverte du fonctionnement
1. Sur un poste,
    - débranchez le clavier de la machine
    - brancher le clavier dans le keylogger
    - brancher le keylogger dans le poste dans le port USB où était le clavier
2. Tapez quelques touches sur le clavier
    - dans une fenêtre Notepad par exemple
    - par exemple "Bonjour, je suis un keylogger 2!@#$%&*()_+"
3. Pour accéder au journal des frappes,
    - appuyer et maintenir sur les lettres "k" "b" et "s" en même temps
    - le keylogger va se mettre en mode "storage" et apparaître comme une clé
    - ouvrez le fichier "log.txt" pour voir les frappes enregistrées

ATTENTION: en aucun cas vous ne toucherez aux autres fichiers du keylogger en mode clé USB.

### Attraper le mot de passe d'un poste de travail

Nous allons simuler une attaque où on essaie de capter le mot de passe d'un ennemi.

1. Équipe jaune: installez le keygrabber sur un de vos postes. Vous devrez sans doute ramper un peu sous le bureau pour le brancher.
2. Équipe jaune: ouvrez un notepad et le laisser ouvert.
3. Équipe verte: pendant que l'équipe jaune ne regarde pas, dans le notepad ouvert:
- tapez "mot de passe 1" puis un mot de passe de votre invention mais simple (que des minuscules)
- tapez "mot de passe 2" puis un mot de passe de votre invention mais compliqué (minuscules, majuscules, chiffres et caractères spéciaux)
- tapez "mot de passe 3" puis le mot de passe le plus compliqué mais de moins de 20 caractères.
- enregistrez le fichier sous "motsPasseVerts.txt" et fermer le notepad
4. Équipe jaune: en utilisant uniquement les informations du keylogger, essayer de retrouver les mots de passe et sauvegarder dans "devineVerts.txt"

Refaites l'activité en inversant les rôles pour avoir les fichier
- "motsPasseJaunes.txt"
- "devineJaunes.txt"

### Synthèse ensemble

1. Essayer de trouver une recette infaillible pour retrouver un mot de passe à partir d'un LOG.TXT
2. Consigner cette recette dans un fichier texte que vous nommerez "recetteKeylogger.txt".
3. Conserver tous les fichiers chacun dans vos notes de cours.
4. Bonus
- Pouvez vous imaginer des moyens de se protéger contre ce type d'attaque?
- Un indice le  keylogger ne sait pas quelle fenêtre est active, il enregistre juste les frappes.
- Proposez une solution casser votre recette infaillible. Vous pouvez valider le tout avec votre prof.


## Boot clé USB externe (environ 20 minutes par équipe)

### Matériel:
- Une clé USB bootable avec Hiren’s BootCD PE.
- Un ordinateur portable du collège sans protection du BIOS (les postes de travail du cégep sont protégés par mot de passe du BIOS)

### Mise en contexte :
Le portable que vous utiliserez contient un compte Administrateur, mais vous n'avez pas le mot de passe pour vous y connecter.

Dans cet exercice : 
1. Vous accèderez au contenu de ce compte, **sans vous y connecter**, pour trouver une image douteuse. 
2. Vous **modifierez le mot de passe** du compte Administrateur pour vous y connecter.

Pour y parvenir, vous utiliserez un **environnement de démarrage externe (Hiren’s BootCD PE)** qui vous permettra d'accéder au contenu du disque dur du portable et vous
fournira des outils pour changer le mot de passe.

#### Quelques précisions sur Hiren’s BootCD PE : 
C’est un mini-système d’exploitation Windows qui permet de démarrer un ordinateur sans utiliser l'OS qui est installé sur le disque. Il fournit différents outils
pour faire de la récupération (ex : fichiers effacés), réparation (ex : modifier un mot de passe), diagnostic (ex : vérifier l'état du disque) ou maintenance. 

### À faire : 

### SECTION A : Démarrage sur l'environnement Hiren :
1. Insérez la clé USB Hiren.
2. Démarrez le portable et **trouvez comment accéder au BIOS**. (**Attention**, si vous n'accédez pas au menu du BIOS dès le démarrage du portable, il faudra le redémarrer)

Lorsque vous serez dans le **menu de démarrage du BIOS**, vous verrez une interface qui ressemble à ceci, mais avec des options légèrement différentes : 

![Image du menu du BIOS](../../static/img/Menu_BIOS.png)

La section **UEFI BOOT** contient les différentes options de démarrage. Par défaut, c'est l'OS Windows (Windows Boot Manager) installé sur le portable qui démarre (premier sur la liste, mais cet ordre peut être modifié). Dans notre cas, nous voulons **démarrer le système d'exploitation externe Hiren** afin de pouvoir utiliser ses outils. 

3. **Sélectionnez l'option de démarrage approprié**.

**Note** : Lorsque vous utilisez un OS externe comme nous le faisons avec Hiren, ce n'est plus Windows qui est installé sur le disque C: (de l'ordinateur courant) qui est lancé. Par conséquent, **tous les processus et mesures de sécurité qui seraient normalement appliqués par Windows ne le sont plus**. Tous les fichiers du disque C: deviennent donc **accessibles**. 

### SECTION B : Trouvez une image douteuse sur le compte Administrateur : 

 Une fois que le démarrage est complété, ouvrez **l'explorateur de fichier** et : 
 1. Accédez au contenu du disque local C: 
 2. Trouvez l'image douteuse que l'administrateur conserve sur son compte. 

### SECTION C : Changer le mot de passe du compte Administrateur 
1. Utilisez le programme **NT Password Edit**  
2. Une fois ouvert, vous verrez que celui-ci peut accéder à **C:\WINDOWS\SYSTEM32\CONFIG\SAM**. Le fichier SAM (Security Account Manager) est la base de données locale qui contient, entre autres, les identifiants et les Hashs. Si vous démarrez l'ordinateur normalement avec Windows, ce fichier est protégé et inaccessible. 
3. **Ouvrez son contenu** (cliquez sur open) pour afficher les comptes utilisateurs et **modifiez le mot de passe du compte Administateur**. 
4. Redémarrez l'ordinateur (sans accéder au BIOS) et **connectez-vous au compte Administrateur** avec le mot de passe que vous avez créé. 

### Comment se protéger : 
Il existe plusieurs manières de se protéger contre des outils bootables externes comme Hiren, en voici quelques-unes.
- L'idéal est de **chiffrer le disque** avec des outils de chiffrement. Par exemple, en utilisant **BitLocker** qui est intégré dans certaines versions de Windows 10 et 11 (doit être activé). Les données du disque deviendraient donc illisibles et cette approche est plutôt simple.   
- Une autre option est de **mettre un mot de passe sur le BIOS**, comme c'est le cas pour les postes de travail au cégep. Ceci empêcherait les modifications des paramètres de démarrage, donc un attaquant ne pourrait pas démarrer l'ordinateur avec la clé USB bootable.  
- Il est également possible de **désactiver le démarrage avec périphériques externes** dans les configurations du BIOS. 
- Surtout, ne pas laisser trainer son ordi n'importe où! 


## Flipper Zero (environ 20 minutes par équipe)

TODO
Cloner un signal Infra Rouge du projecteur?