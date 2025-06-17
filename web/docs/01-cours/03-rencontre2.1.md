---
id: r03
title: Rencontre 3 - CID faille/exploit/fix etc.
sidebar_label: R03 - CID faille/exploit/fix etc.
draft: false
hide_table_of_contents: false
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

:::note Plan de la rencontre

<Tabs>

<TabItem value="deroulement" label="👨‍🏫 Déroulement">

1. Concepts et de la terminologie
1. Critères de sensibilité (triade CID)
1. Les types de hackers
1. Les équipes lors de tests de cybersécurité
1. Travail sur le TP1

</TabItem>

<TabItem value="documents" label="📚 Documents">

- [Présentation PowerPoint](/docs/3U4-R03-Terminologie.pptx)

</TabItem>

</Tabs>

:::


## Vulnérabilités et exploits

Une **vulnérabilité** est une faille ou une faiblesse dans un système ou une application.

Une **menace** est un événement potentiel qui pourrait nuire ou entraîner des dommages.

Le **risque** qu’une menace se concrétise est plus grand lorsque des vulnérabilités subsistent.

Une **attaque**, ou **exploit**, est un acte délibéré de concrétiser une menace.


## Pourquoi on attaque?

- Obtenir des données confidentielles?
- Nuire à un ennemi?
- Voler des renseignements personnels?
- Détourner des fonds?
- Modifier des données?


## Types d'attaque

- Hameçonnage (phishing)
- Déni de service (DoS)
- Déni de service distribué (DDoS)
- Rançongiciel (ransomware)
- Exécution de code à distance (remote code exécution)
- *etc.*


## Critères de sensibilité (triade CID)

| Critère         | Définition                                                                                           |
| --------------- | ---------------------------------------------------------------------------------------------------- |
| Confidentialité | L’information n’est accessible qu’aux personnes dont l’accès est autorisé.                           |
| Intégrité       | L’information est authentique, correcte et fiable; elle n’a pas subi d’altération.                   |
| Disponibilité   | L’information est disponible et les utilisateurs peuvent y accéder chaque fois qu’ils en ont besoin. |


## Exercice

Faites une petite recherche sur ces événements et dites quel(s) critère(s) de la triade CID ont été compromis.
- [Desjardins (2019)](https://fr.wikipedia.org/wiki/Caisses_Desjardins#Vol_de_donn%C3%A9es_personnelles)
- [Ashley Madison (2015)](https://fr.wikipedia.org/wiki/Ashley_Madison#Piratage_et_fuite_de_donn%C3%A9es)
- [CrowdStrike (2024)](https://en.wikipedia.org/wiki/2024_CrowdStrike_incident)
- [Equifax (2017)](https://en.wikipedia.org/wiki/2017_Equifax_data_breach)
- [Université de Santa Clara (2011)](https://www.wired.com/2011/11/santa-clara-university-hacked/)
- [L’attaque de Mafiaboy (2000)](https://fr.wikipedia.org/wiki/Michael_Calce)
- [Attaque NotPetya contre l’Ukraine (2017)](https://en.wikipedia.org/wiki/2017_Ukraine_ransomware_attacks)


## Traçabilité

On ajoute parfois à la triade CID un quatrième critère, celui de la traçabilité.
Les entreprises vont mettre en place des mesures pour remonter à la source en cas d’attaque, à l’aide de systèmes de détection et de journalisation.


## Les hackers

Les hackers sont des experts en cybersécurité qui cherchent à **exploiter des vulnérabilités** d’un système informatique. Ce sont des spécialistes de la cybersécurité **offensive**.

Les hackers se déclinent en plusieurs catégories en fonction de leur intention et de leur sens de l’éthique.

- Le ***Black Hat*** est un cybercriminel qui utilise ses compétences dans le but de nuire ou de faire du profit.

- Le ***White Hat***, ou hacker éthique, opère dans la légalité. Il réalise des tests d’intrusion avec la permission ou à la demande d’une organisation pour aider cette dernière à sécuriser ses systèmes et ses données.

- Le ***Grey Hat*** est situé entre les deux. Il perpètre ses attaques, illégalement ou non, sans intention malveillante ou pécuniaire.

- Le ***script kiddie*** est un amateur qui se croit compétent car il a appris une technique de hacking ou découvert un outil qu’il utilise sans comprendre ce qu’il fait. Il perpètre souvent ses attaques dans le but d’accroître sa réputation auprès de ses pairs.

- Le ***hacktiviste*** est un *grey hat* qui utilise des techniques de hacking pour défendre une cause qu’il croit juste et vertueuse, souvent de manière illégale.


## Red Team / Blue Team

Dans les entreprises, des exercices sont parfois organisés pour mettre à l’épreuve leurs cyberdéfenses.

Dans ce jeu, l’équipe rouge (***red team***) est constituée de white hats. Son but est d’attaquer les systèmes de l’entreprise pour en trouver des failles et les exploiter. L’équipe bleue (***blue team***) a pour mission de bloquer ces attaques.

À la fin de la partie, l’équipe rouge **documente** ses découvertes et émet des **recommandations** à l’équipe bleue.

Le ***purple team*** est une équipe mixte composée à la fois de spécialistes offensifs et défensifs.

Voir: https://www.crowdstrike.com/cybersecurity-101/purple-teaming/



:::note Plan de la rencontre

<Tabs>

<TabItem value="deroulement" label="👨‍🏫 Déroulement">

1. CVSS et métriques de base
1. Exemples d'évaluation
1. Exercices - évaluation de vulnérabilités
1. Travail sur le TP1

</TabItem>

<TabItem value="documents" label="📚 Documents">

- [Présentation PowerPoint](/docs/3U4-R04-Évaluation.pptx)

</TabItem>

</Tabs>

:::

Si tu es exposé à 50 menaces de cybersécurité de toutes sortes et que tu dois décider laquelle est la plus importante
à gérer, il faut que tu t'équipes d'outils pour l'évaluer.

Un de ces outils est le **[Common Vulnerability Scoring System (CVSS)](https://en.wikipedia.org/wiki/Common_Vulnerability_Scoring_System)**.

## Métriques de base du CVSS

Le CVSS est un système permettant d'évaluer le niveau de criticité d'une vulnérabilité. Il vise à nous aider à prioriser notre réponse à des vulnérabilités connues. En évaluant certaines métriques, le CVSS produit un score; plus le score est élevé, plus la vulnérabilité est sérieuse et plus il est urgent de la sécuriser. Vous pouvez utiliser [cet outil](https://www.first.org/cvss/calculator/3.1) pour calculer le score CVSS.

Dans ce cours, nous nous atterderons seulement aux métriques de base.

### Vecteur d'attaque (AV)

Le vecteur d'attaque décrit comment la vulnérabilité peut être exploitée.

#### Réseau (AV:N)
La vulnérabilité est exploitable par le réseau et peut passer à travers un routeur.

#### Adjacent (AV:A)
La vulnérabilité est exploitable par le réseau, mais demande soit une proximité locale (bluetooth, WiFi) soit sur le même segment du réseau local.

#### Local (AV:L)
La vulnérabilité est exploitable seulement avec un accès local au système, soit directement, soit à distance à l'aide de protocole comme SSH ou RDP, ou encore par ingénierie sociale.

#### Physique (AV:P)
La vulnérabilité est exploitable seulement avec un accès physique et direct.

### Complexité de l'attaque (AC)

La métrique de complexité décrit le niveau de difficulté de l'exploit. Il n'est pas ici question du niveau de compétence requis pour exploiter la vulnérabilité ou si l'attaque est "compliquée" à réaliser (par exemple, on doit envoyer du code en assembleur et c'est difficile à apprendre). On parle plutôt des conditions dans laquelle l'attaque doit être réalisée.

#### Bas (AC:L)
L'attaque peut réussir sans circonstances particulières et sans grands efforts de préparation.

#### Haut (AC:H)
Le succès de l'attaque dépend de circonstances hors du contrôle de l'attaquant, qui devra investir des efforts considérables pour préparer son attaque.


### Privilèges nécesaires (PR)

La métrique de privilège décrit le niveau de privilège requis par un attaquant afin de réussir son exploit.

#### Aucun (PR:N)
L'attaquant n'a pas besoin de s'authentifier ou de s'identifier pour l'attaque.

#### Bas (PR:L)
L'attaquant doit être authentifié et disposer d'un accès de base.

#### Élevé (PR:H)
L'attaquant doit être authentifié à l'aide d'un compte disposant de privilèges élevés ou significatifs.


### Interaction nécessaire de l'utilisateur (UI)

La métrique d'interaction avec l'utilisateur décrit si le succès d'un exploit dépend d'une action particulière de la part d'un utilisateur tiers (autre que l'attaquant).

#### Aucune (UI:N)
La vulnérabilité peut être exploitée sans dépendre d'une quelconque interaction avec un utilisateur.

#### Requise (UI:R)
Le succès de l'exploit dépend d'une action de la part d'un utilisateur (par exemple, cliquer sur un lien dans un courriel).


### Portée (S)

La métrique de portée décrit si une attaque réalisée avec succès sur le système vulnérable peut causer un impact sur un autre système.

#### Changée (S:C)
Une vulnérabilité exploitée peut avoir des répercussions sur d'autres systèmes.

#### Inchangée (S:U)
Le dommage causé par l'exploitation de la vulnérabilité est limité au système vulnérable.


### Impact sur la confidentialité

La métrique de confidentialité décrit si l'exploitation de la vulnérabilité a le potentiel de permettre l'accès à des données sensibles par des personnes non autorisées.

#### Aucune (C:N)
Aucun impact sur la confidentialité.

#### Faible (C:L)
Il y a un impact sur le confidentialité, mais l'étendue de l'information compromise est partielle ou l'attaquant n'a pas de contrôle sur les données qu'il accède.

#### Élevée (C:H)
Un attaquant peut avoir accès à l'entièreté des données du système, incluant des données sensibles.



### Impact sur l'intégrité

La métrique de confidentialité décrit si l'exploitation de la vulnérabilité a le potentiel de permettre la modification ou l'altération de données.

#### Aucune (I:N)
Aucun impact sur l'intégrité de l'information.

#### Faible (I:L)
L'impact sur l'intégrité de l'information est circonscrit et limité.

#### Élevée (I:H)
Un attaquant peut modifier toutes les données du système compromis.



### Impact sur la disponibilité

La métrique de disponibilité décrit si l'exploitation de la vulnérabilité a le potentiel d'empêcher l'accès à l'information par les personnes autorisées.

#### Aucune (A:N)
Aucun impact sur la disponibilité.

#### Faible (A:L)
La disponibilité est affecté de manière intermittente ou partielle, ou la performance peut être dégradée

#### Élevée (A:H)
Un attaquant peut rendre le système vulnérable complètement indisponible.



## Exemples

### Exemple 1 : une attaque de déni de service (DDoS) sur le site omnivox pendant la période de remise des notes

**Résumé :**
> On est rendus le 28 décembre et demain c'est la date limite pour remettre les notes. Plusieurs profs commencent à se plaindre: la plupart du temps, on ne peut pas accéder et quand on accède c'est très lent.

On va évaluer ça:
- Vecteur d'attaque: réseau
- Complexité d'attaque: là c'est pas évident, il faut quand même prendre le contrôle de plusieurs postes à moins que ce soit un grand nombre d'étudiants coordonnés
- Niveau de privilège nécessaire: aucun
- Interaction nécessaire de l'utilisateur: aucune
- Portée de l'impact: inchangée
- Confidentialité: aucune
- Intégrité: aucune
- Disponibilité: élevée

On va donc avoir un score de 7.5/10. C'est assez élevé, on va donc devoir s'en occuper rapidement.

### Exemple 2 : une attaque de type un étudiant installe un keylogger

**Résumé :**
> Un étudiant a placé un keylogger physique sur le poste du prof dans le local D0605. Il a pu récupérer les mots de passe des 8 profs qui donnent des cours dans ce local. Cela inclut son prof pour un cours qu'il est au bord de couler.

On va évaluer ça:
- Vecteur d'attaque: physique
- Complexité d'attaque: faible
- Niveau de privilège nécessaire: aucun
- Interaction nécessaire de l'utilisateur: requise
- Portée de l'impact: inchangée
- Confidentialité: élevée
- Intégrité: élevée
- Disponibilité: aucune (à moins que le pirate détruise des fichiers sans backup mais là il y a un backup)

On va donc avoir un score de 5.9 / 10.

On voit que le score est un indicateur mais également qu'on est forcé de réfléchir selon des critères partagés avec le
reste de la communauté cybersecurité.

On voit aussi que la disponibilité change selon la présence de sauvegardes ou pas ce qui peut donner des idées d'amélioration pour
limiter l'impact d'une attaque.

## Exercices par équipe de 3-4 :

Chaque équipe enverra un membre expliquer les différentes composantes et le score final.

Déterminer chaque composante du CVSS 3.1 et le score final. Pensez à prendre en note, ça pourrait servir
pour les révisions pour l'examen.

### Exercice 1

> Joris un des profs du département d'informatique a reçu un courriel venant d'un collègue d'un autre collège. Dedans il y avait un `.exe` avec supposément la démo d'un TP dans un cours qu'il donne.
>
> En ouvrant le `.exe` depuis son poste au collège, apparemment rien ne se passe. Il continue ses affaires.
>
> Une heure plus tard, il essaie d'ouvrir un fichier sur son disque réseau Z: et il y a un fichier
`LIS_MOI.txt` qui accompagne un énorme fichier `stuff.encrypted`, tout le reste a disparu.


### Exercice 2

> Giacomo après avoir configuré son serveur de courriel et authentifié son domaine avec SPF, DKIM et DMARC se rend compte qu'il peut envoyer des courriels `@cegepmontpetit.ca` avec n'importe quel préfixe.
>
> Il commence par envoyer un courriel à son prof de la part de la direction du collège pour lui dire qu'il a
maintenant le droit à 50% de temps supplémentaire pour ses examens.









