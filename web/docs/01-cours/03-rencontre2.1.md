---
id: r03
title: Rencontre 3 - Faille/exploit/fix - CID - CVSS 
sidebar_label: R03 - Faille/exploit/fix - CID - CVSS 
draft: false
hide_table_of_contents: false
toc_max_heading_level: 4
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

:::note Plan de la rencontre

<Tabs>

<TabItem value="deroulement" label="👨‍🏫 Déroulement">

1. Notions de vulnérabilité, exploit et correctif
1. Critères de sensibilité (triade CID)
1. Les types de hackers
1. CVSS et métriques de base
1. Travail sur le TP1

</TabItem>

</Tabs>

:::

## 1. Notions de vulnérabilité, exploit et correctif

### Concepts clés en cybersécurité

| **Terme**            | **Définition** |
|----------------------|-----------------|
| **Vulnérabilité**    | Une **faille ou faiblesse** dans un système pouvant être **exploitée**. Peut être de nature technologique ou humaine. |
| **Menace**          | Événement potentiel qui pourrait **causer des dommages**. |
| **Risque**          | Probabilité qu'une menace exploite une vulnérabilité et cause un impact. |
| **Exploit (attaque)** | **Action visant à tirer profit d'une vulnérabilité**. L'exploit est souvent complexe et contient beaucoup d'étapes. Essentiellement, c'est une marche à suivre qu'une personne qualifiée peut appliquer pour mener à bien l'attaque en « exploitant » la vulnérabilité.|
| **Correctif (fix)** | **Solution pour corriger la vulnérabilité et bloquer l'exploit**. On peut valider un correctif en s'assurant que l'exploit ne fonctionne plus.|

### Exemple d'attaque avec exploit et correctif

**Résumé** :
> Un étudiant a placé un keylogger physique sur le poste du prof dans le local D0605. Il a pu récupérer les mots de passe des 8 profs qui donnent des cours dans ce local. Cela inclut son prof pour un cours qu'il est au bord de couler.

##### Vulnérabilité

L'accès physique aux ordinateurs des profs est possible et il n'y a pas de moyen de surveillance des classes.

##### Exploit

- acheter un keylogger physique sur un site de vente en ligne
- attendre une fin de journée un vendredi pour installer le keylogger
  - fermer la porte du local temporairement
  - glisser sa main derrière le poste pour débrancher le clavier
  - brancher le clavier USB du prof dans le keylogger
  - brancher le keylogger dans le port USB du poste
- attendre une période suffisamment longue pour que le keylogger ait enregistré des mots de passe
- revenir pour récupérer le keylogger en procédant à l'inverse de la première étape

##### Correctif

Les détails de l'exploit permettent de trouver un correctif:
- on pourrait empêcher une main de passer derrière les postes
  - espace plus serré
  - boite complètement fermée
- on pourrait empêcher l'accès sans être détecté
  - avec des caméras dans les locaux
  - avec une caméra sur l'arrière des postes
- on pourrait limiter la durée de l'attaque
  - s'assurer qu'un technicien passe régulièrement inspecter les postes
  - former les profs pour qu'ils valident que rien n'est branché en arrière en début de cours

##### Leçons

- Si on ne sait pas comment l'attaque a été menée, on ne peut pas trouver de correctif
- On essaie de se défendre un peu à l'aveugle


**Exemple de vulnérabilité sans réel exploit :** 
  - https://medium.com/@haydengpt/when-72-characters-is-all-it-takes-unpacking-spring-securitys-latest-snafu-6a65164d370b
  - https://spring.io/security/cve-2025-22228

### Exercice par équipe de 3-4 (10 min)

Par groupe de 3 ou 4, déterminer la **vulnérabilité**, l'**exploit** et le **correctif**.

> Joris un des profs du département d'informatique a reçu un courriel venant d'un collègue d'un autre collège. Dedans il y avait un `.exe` avec supposément la démo d'un TP dans un cours qu'il donne.
>
> En ouvrant le `.exe` depuis son poste au collège, apparemment rien ne se passe. Il continue ses affaires.
>
> Une heure plus tard, il essaie d'ouvrir un fichier sur son disque réseau Z: et il y a un fichier `LIS_MOI.txt` qui accompagne un énorme fichier `stuff.encrypted`, tout le reste a disparu.



### Types d'attaques et objectifs visés

Ceci est une liste non exhaustive des objectifs visés et des types d'attaques présentant le potentiel de les atteindre.

| **Objectif**                          | **Types d'attaques** |
|--------------------------------------|--------------------------------|
| **Obtenir des données confidentielles** | Hameçonnage, Exécution de code à distance, Rançongiciel |
| **Nuire à un ennemi**                   | Déni de service (DoS) , Déni de service distribué (DDoS), Rançongiciel |
| **Voler des renseignements personnels** | Hameçonnage, Exécution de code à distance |
| **Détourner des fonds**                 | Hameçonnage, Exécution de code à distance, Rançongiciel |
| **Modifier ou détruire des données**    | Exécution de code à distance, Rançongiciel |


## 2.  Critères de sensibilité (triade CID)

| Critère         | Définition                                                                                           |
| --------------- | ---------------------------------------------------------------------------------------------------- |
| **Confidentialité** | L’information n’est accessible qu’aux personnes dont l’accès est autorisé.                           |
| **Intégrité**       | L’information est authentique, correcte et fiable; elle n’a pas subi d’altération.                   |
| **Disponibilité**   | L’information est disponible et les utilisateurs peuvent y accéder chaque fois qu’ils en ont besoin. |


### Exercice triade CID (10 min)

Faites une petite recherche sur ces événements et dites quel(s) critère(s) de la triade CID ont été compromis.
- [Desjardins (2019)](https://fr.wikipedia.org/wiki/Caisses_Desjardins#Vol_de_donn%C3%A9es_personnelles)
- [Ashley Madison (2015)](https://fr.wikipedia.org/wiki/Ashley_Madison#Piratage_et_fuite_de_donn%C3%A9es)
- [CrowdStrike (2024)](https://en.wikipedia.org/wiki/2024_CrowdStrike_incident)
- [Equifax (2017)](https://en.wikipedia.org/wiki/2017_Equifax_data_breach)
- [Université de Santa Clara (2011)](https://www.wired.com/2011/11/santa-clara-university-hacked/)
- [L’attaque de Mafiaboy (2000)](https://fr.wikipedia.org/wiki/Michael_Calce)
- [Attaque NotPetya contre l’Ukraine (2017)](https://en.wikipedia.org/wiki/2017_Ukraine_ransomware_attacks)


### Traçabilité

On ajoute parfois à la triade CID un quatrième critère, celui de la traçabilité.
Les entreprises vont mettre en place des mesures pour remonter à la source en cas d’attaque, à l’aide de systèmes de détection et de journalisation.


## 3. Les types de hackers

Les hackers sont des experts en cybersécurité qui cherchent à **exploiter des vulnérabilités** d’un système informatique. Ce sont des spécialistes de la cybersécurité **offensive**.

Les hackers se déclinent en plusieurs catégories en fonction de leur intention et de leur sens de l’éthique.

- Le ***Black Hat*** est un cybercriminel qui utilise ses compétences dans le but de nuire ou de faire du profit.

- Le ***White Hat***, ou hacker éthique, opère dans la légalité. Il réalise des tests d’intrusion avec la permission ou à la demande d’une organisation pour aider cette dernière à sécuriser ses systèmes et ses données.

- Le ***Grey Hat*** est situé entre les deux. Il perpètre ses attaques, illégalement ou non, sans intention malveillante ou pécuniaire.

- Le ***hacktiviste*** est un *grey hat* qui utilise des techniques de hacking pour défendre une cause qu’il croit juste et vertueuse, souvent de manière illégale.


## 4. Métriques de base du CVSS

Si tu es exposé à 50 menaces de cybersécurité de toutes sortes et que tu dois décider laquelle est la plus importante
à gérer, il faut que tu t'équipes d'outils pour l'évaluer.

Un de ces outils est le **[Common Vulnerability Scoring System (CVSS)](https://en.wikipedia.org/wiki/Common_Vulnerability_Scoring_System)**.

Le **CVSS** est un système permettant d'**évaluer le niveau de criticité d'une vulnérabilité**. Il vise à nous aider à **prioriser** notre réponse à des vulnérabilités connues. En évaluant certaines métriques, le **CVSS produit un score: plus le score est élevé, plus la vulnérabilité est sérieuse et plus il est urgent de la sécuriser**. Vous pouvez utiliser [**cet outil**](https://www.first.org/cvss/calculator/3.1) pour calculer le score CVSS.

Dans ce cours, **nous nous attarderons seulement aux métriques de base**.

### AV: Vecteur d'attaque

Le vecteur d'attaque décrit comment la vulnérabilité peut être exploitée.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Réseau | AV:N | La vulnérabilité est exploitable par le réseau et peut passer à travers un routeur.|
| Adjacent | AV:A | La vulnérabilité est exploitable par le réseau, mais demande soit une proximité locale (bluetooth, WiFi) soit sur le même segment du réseau local.|
| Local | AV:L | La vulnérabilité est exploitable seulement avec un accès local au système, soit directement, soit à distance à l'aide de protocole comme SSH ou RDP, ou encore par ingénierie sociale. |
| Physique | AV:P | La vulnérabilité est exploitable seulement avec un accès physique et direct.|

### AC: Complexité de l'attaque

La métrique de complexité décrit le niveau de difficulté de l'exploit. Il n'est pas ici question du niveau de compétence requis pour exploiter la vulnérabilité ou si l'attaque est "compliquée" à réaliser (par exemple, on doit envoyer du code en assembleur et c'est difficile à apprendre). On parle plutôt des conditions dans laquelle l'attaque doit être réalisée.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Bas | AC:L | L'attaque peut réussir sans circonstances particulières et sans grands efforts de préparation. |
| Haut | AC:H | Le succès de l'attaque dépend de circonstances hors du contrôle de l'attaquant, qui devra investir des efforts considérables pour préparer son attaque. |


### PR: Privilèges nécessaires

La métrique de privilège décrit le niveau de privilège requis par un attaquant afin de réussir son exploit.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Aucun | PR:N | L'attaquant n'a pas besoin de s'authentifier ou de s'identifier pour l'attaque. |
| Bas | PR:L | L'attaquant doit être authentifié et disposer d'un accès de base. |
| Élevé | PR:H | L'attaquant doit être authentifié à l'aide d'un compte disposant de privilèges élevés ou significatifs. |


### UI: Interaction nécessaire de l'utilisateur

La métrique d'interaction avec l'utilisateur décrit si le succès d'un exploit dépend d'une action particulière de la part d'un utilisateur tiers (autre que l'attaquant).

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Aucune | UI:N | La vulnérabilité peut être exploitée sans dépendre d'une quelconque interaction avec un utilisateur. |
| Requise | UI:R | Le succès de l'exploit dépend d'une action de la part d'un utilisateur (par exemple, cliquer sur un lien dans un courriel). |


### S: Portée (*scope*)

La métrique de portée décrit si une attaque réalisée avec succès sur le système vulnérable peut causer un impact sur un autre système.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Changée | S:C | Une vulnérabilité exploitée peut avoir des répercussions sur d'autres systèmes. |
| Inchangée | S:U | Le dommage causé par l'exploitation de la vulnérabilité est limité au système vulnérable. |


### C: Impact sur la confidentialité

La métrique de confidentialité décrit si l'exploitation de la vulnérabilité a le potentiel de permettre l'accès à des données sensibles par des personnes non autorisées.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Aucune | C:N | Aucun impact sur la confidentialité. |
| Faible | C:L | Il y a un impact sur la confidentialité, mais l'étendue de l'information compromise est partielle ou l'attaquant n'a pas de contrôle sur les données qu'il accède. |
| Élevée | C:H | Un attaquant peut avoir accès à l'entièreté des données du système, incluant des données sensibles. |



### I: Impact sur l'intégrité

La métrique de confidentialité décrit si l'exploitation de la vulnérabilité a le potentiel de permettre la modification ou l'altération de données.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Aucune | I:N | Aucun impact sur l'intégrité de l'information. |
| Faible | I:L | L'impact sur l'intégrité de l'information est circonscrit et limité. |
| Élevée | I:H | Un attaquant peut modifier toutes les données du système compromis. |



### A: Impact sur la disponibilité (*availability*)

La métrique de disponibilité décrit si l'exploitation de la vulnérabilité a le potentiel d'empêcher l'accès à l'information par les personnes autorisées.

| **Valeur** | **Code** | **Description** |
| --- | -- | --- |
| Aucune | A:N | Aucun impact sur la disponibilité. |
| Faible | A:L | La disponibilité est affectée de manière intermittente ou partielle, ou la performance peut être dégradée. |
| Élevée | A:H | Un attaquant peut rendre le système vulnérable complètement indisponible. |



### Exemple de calcul du score CVSS

Cet exemple décrit le calcul du score CVSS d'une vulnérabilité ayant permis une attaque de déni de service (DDoS) sur le site omnivox pendant la période de remise des notes.

**Résumé :**
> On est rendus le 28 décembre, et demain c'est la date limite pour remettre les notes. Plusieurs profs commencent à se plaindre, car la plupart du temps on ne peut pas accéder, et quand on accède, c'est très lent.

**On va évaluer ça:**
- Vecteur d'attaque: réseau
- Complexité d'attaque: là c'est pas évident, il faut quand même prendre le contrôle de plusieurs postes à moins que ce soit un grand nombre d'étudiants coordonnés
- Niveau de privilège nécessaire: aucun
- Interaction nécessaire de l'utilisateur: aucune
- Portée de l'impact: inchangée
- Confidentialité: aucune
- Intégrité: aucune
- Disponibilité: élevée

On va donc avoir un **score de 7.5/10**. C'est assez élevé, on va donc devoir s'en occuper rapidement.

### Exercices par équipe de 3-4 (10 min):

Par groupe de 3 ou 4, **déterminez chaque composante du CVSS 3.1 et le score final**. Pensez à prendre des notes, ça pourrait servir
pour les révisions pour l'examen.

> Joris, un des profs du département d'informatique, a reçu un courriel venant d'un collègue d'un autre collège. Dedans, il y avait un `.exe` avec supposément la démo d'un TP dans un cours qu'il donne.
>
> En ouvrant le `.exe` depuis son poste au collège, apparemment, rien ne se passe. Il continue ses affaires.
>
> Une heure plus tard, il essaie d'ouvrir un fichier sur son disque réseau Z: et il y a un fichier
`LIS_MOI.txt` qui accompagne un énorme fichier `stuff.encrypted`, tout le reste a disparu.











