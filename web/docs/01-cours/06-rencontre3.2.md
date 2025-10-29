---
id: r06
title: Rencontre 6 - Ingénierie sociale
sidebar_label: R06 - Ingénierie sociale
draft: false
hide_table_of_contents: false
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

:::note Plan de la rencontre 6

<Tabs>

<TabItem value="deroulement" label="👨‍🏫 Déroulement">

1. Retour sur les ateliers
2. Discussion sur l'ingénierie sociale et le phishing
3. Travail sur le TP

</TabItem>

</Tabs>

:::


## Retour sur les ateliers

Vous avez pu essayer quatre outils permettant de réaliser divers types d'attaques. Discutons-en ensemble.

#### Qu'est-ce que le <u>keylogger</u> permet de faire? 
  - Quels sont les branchements à effectuer?
  - Mais est-ce que ça fonctionne bien? 
  - La ou lesquelles des composantes CID sont en jeu?
  - Quels sont les défis à surmonter pour mener une telle attaque?
  - Comment pouvez-vous prévenir cette attaque?

#### Que permet de faire la <u>clé USB bootable Hiren's</u>
  - Sur quelle touche fallait-il appuyer au démarrage pour charger Hiren? Est-ce toujours la même sur tous les modèles de PC?
  - Quels sont les défis à surmonter pour mener une telle attaque?
  - Que pourriez-vous mettre en place pour empêcher une telle attaque?
  - Dans un environnement réel, la clé USB bootable est-elle une réelle menace?

#### Comment le <u>Flipper Zero</u> arrive-t-il à contrôler le projecteur?
  - Quels sont les défis auxquels le hacker devrait faire face pour mener cette attaque dans un environnement réel?
  - Comment pourriez-vous empêcher une telle attaque?

#### Comment le <u>Rubber Ducky</u> arrive-t-il à "lancer" un script sur l'ordinateur cible?
  - Comment le rubber ducky pourrait-il être utilisé pour mener une attaque, et quels sont les défis?
  - Comment pourriez-vous prévenir une telle attaque?
  - Un antivirus pourrait-il bloquer cette attaque?

#### En quoi les attaques du <u>Rubber Ducky</u> et du <u>Flipper Zero</u> se ressemblent-elles? 
  - Le Flipper permet-il de réaliser la même attaque que le Ducky?  
  - Pouvez-vous identifier un avantage de chacun?

#### Dans [la vidéo](https://www.youtube.com/watch?v=XJCQBqTmGUU) que vous avez visionnée:
  - À quel atelier cela s'apparente-t-il?
  - Quelle est la différence entre les clés actives et les clés passives?
  - Quel est l'avantage de chacune?

#### Contre quelle(s) attaque(s) se protège-t-on si:
  - On active le chiffrement intégral du disque dur de son ordinateur?
  - On choisit un mot de passe très long
  - On choisit un mot de passe compliqué (par exemple `A3#s%*6x`)


## L'ingénierie sociale

L'humain est souvent l'élément le plus vulnérable d'un système. On appelle *ingénierie sociale* les diverses techniques permettant d'exploiter la vulnérabilité humaine. Les attaques d'ingénierie sociale impliquent généralement des techniques psychologiques de manipulation, d'influence sociale ou d'exploitation de la confiance. 


### L’hameçonnage (*phishing*)

L’hameçonnage, ou *phishing*, est une catégorie d'attaque d'ingénierie sociale, c'est-à-dire que la vulnérabilité principale qu'il exploite est celle de l'humain. 

Une attaque d'hameçonnage consiste à envoyer un message que la victime croit légitime, afin de la manipuler pour la conduire à poser une action désirée par l'attaquant. Habituellement, cette action a pour effet de communiquer: 
- de l'information confidentielle, 
- de modifier une donnée 
- ou d'exécuter du code malicieux tel qu'un virus ou un rançongiciel.

Les messages d’hameçonnage passent généralement par courriel, mais peuvent aussi être véhiculés sous la forme de texto (*smishing*) ou de message vocal (*vishing*).

L'attaquant peut choisir ses victimes de plusieurs manières possibles:


### Le *phishing* de masse

C'est la forme la plus répandue d’hameçonnage. Elle consiste à envoyer des messages massivement à un très grand nombre de personnes, à la manière des pourriels (*spams*). Sous cette forme, l’hameçonnage n'est pas ciblé envers un individu précis, mais le succès de l'attaque repose sur le fait qu'un petit pourcentage des victimes potentielles se fait avoir. L'objectif de ces attaques est généralement de voler ou extorquer de l'argent aux victimes.

```
De: roger1284@protonmail.com
À: paul.meilleur@macompagnie.com
Sujet: urgent


A transaction of $3867.22 have been registered in your bank account. If you 
think this is an error, please log in to:

https://bank-web-access.info/login?id=8fd8a9e387ab1d83

```


### Le *spear phishing*

C'est un type d’hameçonnage ciblé. Son nom découle de l'analogie qui oppose la pêche à la ligne, où on attend que n'importe quel poisson morde, et la pêche au harpon qui vise un poisson spécifique. Le *spear phishing* cible un groupe de personnes spécifiques, comme les employés d'une entreprise ou d'un département. Typiquement, cela peut prendre la forme d'un courriel provenant soi-disant d'une figure d'autorité et demandant à la victime de poser une action rapidement pour répondre à une urgence. La particularité de ce type d'hameçonnage est qu'il est personnalisé, car il vise une personne ou un groupe de personnes en particulier. L'attaquant qui perpètre ce type d'attaque souhaite généralement viser une entreprise en particulier, ou encore tente de rehausser la crédibilité de son attaque.

```
De: Service de la paie <service-paie@paie-cegepmontpetit.ca>
À: paul.meilleur@cegepmontpetit.ca
Sujet: URGENT - Tentative d'intrusion


ATTENTION - URGENT

Bonjour Paul,

Nous avons détecté une modification de votre compte bancaire pour le versement 
de votre paie. Le compte est situé au Liechtenstein. Nous pensons qu'il s'agit 
d'une tentative de fraude. Pour éviter de perdre votre paie, SVP veuillez 
configurer vos informations bancaires dans notre système de paie LE PLUS 
RAPIDEMENT POSSIBLE: https://paie-cegepmontpetit.ca/login?id=8fd8a9e387ab1d83.

Cordialement,

--
Service de la Paie - CÉGEP Édouard-Montpetit

```


### Le *whaling*

Le *whaling* est un type particulier d’hameçonnage visant spécifiquement les très gros poissons (PDG, vice-président, ministre). Typiquement, le récit de l'attaquant vise à mettre de la pression sur la victime en brandissant de lourdes conséquences légales ou financières concernant l'organisation dont elle est imputable.


```
De: Autorité des marchés financiers <info@autorite-marches-financiers-qc-ca.ru>
À: yvon.bosse@grossecompagnie.com
Sujet: URGENT - Violation de la loi A-33.2
Pièce jointe: [poursuite-grossecompagnie.pdf.exe]

Bonjour M. Bossé,

Nous nous adressons à vous en votre qualité de PDG de Grosse Compagnie Inc. Nos
enquêteurs ont décelé d'importantes incohérences dans vos états financiers, 
qui nous laisse croire que votre entreprise a recours à des stratagèmes 
financiers illégaux. Ceci est très sérieux et pourrait mener à des accusations 
criminelles à votre encontre.

SVP, prenez connaissance du document suivant en pièce jointe et relayez 
l'information à vos services juridiques. Un enquêteur se présentera au siège 
social de votre société demain matin à 9h00 avec un mandat de perquisition
en main.

Cordialement

--
Agnès Toutant, CPA
Conseillère principale | Service prévention des fraudes
Autorité des marchés financiers
T: (514) 555-0199

```

### Moyens de défense

Il existe plusieurs moyens de défense pour prévenir l’hameçonnage, ou du moins limiter son impact.

#### Éducation et sensibilisation

Il est essentiel de former les utilisateurs à reconnaître les tentatives d’hameçonnage et à ne pas cliquer sur les liens suspects. Pour ce faire, de nombreuses entreprises ont recours à des formations obligatoires pour sensibiliser les employés. Souvent, les entreprises envoient également de fausses tentatives de *phishing* aux employés afin de collecter des statistiques sur le succès de la formation.


#### Filtres anti-spam

Ces filtres peuvent aider à bloquer les courriels de *phishing* avant qu'ils n'atteignent les utilisateurs, par l'analyse de mots-clés dans le message ou des métadonnées du courriel. Ils peuvent toutefois laisser passer une petite quantité de courriels malveillants (faux négatifs), ou encore bloquer des courriels légitimes (faux positifs). Certains filtres peuvent dynamiquement modifier les liens dans un courriel pour les faire passer par un service de vérification, ou ajouter des messages d'avertissement dans le corps du message pour mettre en garde contre un lien ou une provenance externes, etc.

#### Antivirus

Si la charge utile du courriel d’hameçonnage comprend un fichier malveillant (soit en pièce jointe ou en lien de téléchargement), un antivirus peut être utile pour détruire le fichier dès qu'il entre sur l'ordinateur de la victime, avant qu'il n'ait eu le temps de faire du dommage.

#### Authentification à deux facteurs (2FA)

Si le courriel de *phishing* a pour but d'intercepter l'identifiant et le mot de passe de la victime, l'authentification à deux facteurs peut empêcher l'attaquant de les utiliser.

#### Principe du plus bas privilège

Les politiques de sécurité strictes peuvent aider à minimiser l'étendue des dégâts causés par un *phishing* réussi, en faisant en sorte que la victime ait accès au moins de données possible.

