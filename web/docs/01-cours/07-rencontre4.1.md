---
id: r07
title: Rencontre 7 - Ingénierie sociale
sidebar_label: R07 - Ingénierie sociale
draft: false
hide_table_of_contents: false
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

:::note Plan de la rencontre 7

<Tabs>

<TabItem value="deroulement" label="👨‍🏫 Déroulement">

1. Retour sur le dernier cours
2. Discussion sur l'ingénierie sociale et le phishing
3. Travail sur le TP

</TabItem>

<TabItem value="documents" label="📚 Documents">

</TabItem>

</Tabs>

:::


L'humain est souvent l'élément le plus vulnérable d'un système. On appelle ingénierie sociale les diverses techniques permettant d'exploiter la vulnérabilité humaine. Les attaques d'ingénierie sociale impliquent généralement des techniques psychologiques de manipulation, d'influence sociale ou d'exploitation de la confiance. 


## Qu'est-ce que le hameçonnage (phishing)

Le hameçonnage, ou *phishing*, est une catégorie d'attaque d'ingénierie sociale, c'est-à-dire que la vulnérabilité principale qu'il exploite sont celle de l'humain. 

Une attaque de phishing consiste à envoyer un message que la victime croit légitime, afin de la manipuler pour la conduire à poser une action désirée par l'attaquant. Habituellement, cette action a pour effet de communiquer 
- de l'information confidentielle, 
- modifier une donnée 
- exécuter du code malicieux tel qu'un virus ou un rançongiciel. 

Les messages de hameçonnage passent généralement par courriel, mais peuvent aussi être véhiculés sous la forme de texto (*smishing*) ou de message vocal (*vishing*).

L'attaquant peut choisir ses victimes de plusieurs manières possibles:


### Le *phishing* de masse

C'est la forme la plus répendue de hameçonnage. Elle consiste à envoyer des messages massivement à un très grand nombre de personnes, à la manière des pourriels (*spams*). Sous cette forme, le hameçonnage n'est pas ciblé envers un individu précis, mais le succès de l'attaque repose sur le fait qu'un petit pourcentage des victimes potentielles se fait avoir. L'objectif de ces attaques est généralement de voler ou extorquer de l'argent aux victimes.

```
De: roger1284@protonmail.com
À: paul.meilleur@macompagnie.com
Sujet: urgent


A transaction of $3867.22 have been registered in your bank account. If you 
thing this is an error, please log in to:

https://bank-web-access.info/login?id=8fd8a9e387ab1d83

```


### Le *spear phishing*

C'est un type de hameçonnage ciblé. Son nom découle de l'analogie qui oppose la pêche à la ligne, où on attend que n'importe quel poisson morde, et la pêche au harpon qui vise un poisson spécifique. Le *spear phishing* cible un groupe de personne spécifiques, comme les employés d'une entreprise ou d'un département. Typiquement, cela peut prendre la forme d'un courriel provenant soi-disant d'une figure d'autorité et demandant à la victime de poser une action rapidement pour répondre à une urgence. La particularité de ce type d'hameçonnage est qu'il est personnalisé car il vise une personne ou un groupe de personnes en particulier. L'attaquant qui perpètre ce type d'attaque souhaite généralement viser une entreprise en particulier, ou encore tente de rehausser la crédibilité de son attaque.

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

Le *whaling* est un type particulier de hameçonnage visant spécifiquement les très gros poissons (PDG, vice-président, ministre). Typiquement, le récit de l'attaquant vise à mettre pression sur la victime en brandissant de lourdes conséquences légales ou financières concernant l'organisation dont elle est imputable.


```
De: Autorité des marchés financiers <info@autorite-marches-financiers-qc-ca.ru>
À: yvon.bosse@grossecompagnie.com
Sujet: URGENT - Violation de la loi A-33.2
Pièce jointe: [poursuite-grossecompagnie.pdf.exe]

Bonjour M. Bossé,

Nous vous adressons à vous en votre qualité de PDG de Grosse Compagnie Inc. Nos
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

## Moyens de défense

Il existe plusieurs moyens de défense pour prévenir le phishing, ou du moins limiter son impact.

### Éducation et sensibilisation

Il est essentiel de former les utilisateurs à reconnaître les tentatives de phishing et à ne pas cliquer sur les liens suspects. Pour ce faire, de nombreuses entreprises ont recours à des formations obligatoires pour sensibiliser les employés. Souvent, les entreprises envoient également de fausses tentatives de *phishing* aux employés afin de collecter des statistiques sur le succès de la formation.


### Filtres anti-spam

Ces filtres peuvent aider à bloquer les emails de *phishing* avant qu'ils n'atteignent les utilisateurs, par l'analyse de mots-clés dans le message ou des métadonnées du courriel. Ils peuvent toutefois laisser passer une petite quantité de courriels malveillants (faux-négatifs), ou encore bloquer des courriels légitimes (faux-positifs). Certains filtres peuvent dynamiquement modifier les liens dans un courriel pour les faire passer par un service de vérification, ou ajouter des message d'avertissement dans le corps du message pour mettre en garde contre un lien ou une provenance externe, etc.

### Antivirus

Si la charge utile du courriel de phishing comprend un fichier malveillant (soit en pièce jointe ou en lien de téléchargement), un antivirus peut être utile pour détruire le fichier dès qu'il entre sur l'ordinateur de la victime, avant qu'il n'ait eu le temps de faire du dommage.

### Authentification à deux facteurs (2FA)

Si le courriel de *phishing* a pour but d'intercepter l'identifiant et le mot de passe de la victime, l'authentification à deux facteurs peut empêcher l'attaquant de les utiliser.

### Principe du plus bas privilège

Les politiques de sécurité strictes peuvent aider à minimiser l'étendue des dégâts causés par un *phishing* réussi, en faisant en sorte que la victime ait accès au moins de données possibles.

