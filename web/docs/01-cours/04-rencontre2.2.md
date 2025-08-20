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

## RubberDucky (environ 20 minutes par équipe)

Matériel:
- 1 clé USB RubberDucky (voir avec le prof l'ordre pour les équipes)
- vos postes de travail: les injections proposées par le prof ne font aucun dommage
- si la clé USB a été modifiée, demande au prof de la restaurer

TODO 
- POSTE leur faire essayer des inject.bin déjà présents sur la clé
- basculer entre le mode clavier et le mode USB
- bonus: regarder l'IDE?

## Key logger (environ 20 minutes par équipe)

Matériel: 
- 1 keylogger physique (voir avec le prof l'ordre pour les équipes)
- vos postes de travail: les informations collectées par le keylogger ne sont pas sensibles

TODO
- leur faire installer le keylogger physique
- démontrer l'accès aux logs de frappe
- splitter l'équipe en deux : équipe A choisit un mot de passe compliqué mais pas un vrai à eux, équipe B le devine avec le keylogger
- faire l'exercice inverse
- RAPPORT? TRACE? 

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