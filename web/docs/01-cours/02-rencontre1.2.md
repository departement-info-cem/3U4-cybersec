---
id: r02
title: Rencontre 2 - Authentification
sidebar_label: R02 - Authentification
draft: false
hide_table_of_contents: false
---

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

:::note Plan de la rencontre

<Tabs>

<TabItem value="deroulement" label="👨‍🏫 Déroulement">

1. Retour sur le dernier cours
1. Notions du contrôle d'accès
1. Comment déjouer un mot de passe
1. Facteurs d'authentification
1. Formation des équipes pour le TP1 et choix des sujets

</TabItem>

<TabItem value="documents" label="📚 Documents">

- [Présentation PowerPoint](/docs/3U4-R02-Authentification.pptx)

</TabItem>

</Tabs>

:::


## Le contrôle d'accès

Il apparaît comme une évidence que l'accès aux systèmes informatiques doit être contrôlé d'une manière ou une autre. On peut contrôler les accès de manière physique ou logique.

La notion d'**identification** permet de distinguer les différents utilisateurs d'un système. Généralement, cela se fait au moyen d'un nom d'utilisateur, qui représente en quelque sorte notre identité numérique.

La notion d'**autorisation** permet d'attribuer des droits, des permissions et des privilèges différents pour chaque utilisateur. Ce principe est rendu possible grâce à l'identification. Généralement, cela se fait par des listes d'accès (*ACL*).

La notion d'**authentification** est très importante puisque sans elle, n'importe qui pourrait se faire passer pour n'importe qui. L'authentification est le mécanisme par lequel on arrive à prouver notre identité au système, à démontrer que nous sommes vraiment qui nous prétendons être. Souvent, on accepte un mot de passe ou un NIP en guise de preuve.


## La faiblesse des mots de passe

Un mot de passe est souvent utilisé en guise de preuve d'identité, mais il ne constitue pas vraiment une preuve. Nous avons vu au dernier cours que des mots de passe simples et non salés peuvent être facile à craquer si nous en possédons le hash. Même sans le hash, certains mots de passe sont [faciles à deviner](https://en.wikipedia.org/wiki/List_of_the_most_common_passwords).

Augmenter la complexité du mot de passe est certes une bonne pratique, mais il y a des limites à son efficacité. Si le mot de passe est trop complexe, il risque d'être difficile à retenir. Si cette complexité est imposée à l'utilisateur, celui-ci risque d'être tenté d'écrire son mot de passe dans un fichier ou un post-it. Choisir un mot de passe long mais peu complexe, qu'on appelle parfois une *passphrase*, est plus difficile à craquer à partir du hash, mais plus facile à deviner par des humains (surtout s'il est composé de mots prévisibles, comme le nom de ses enfants).

De toutes les manières, s'il arrive que l'utilisateur n'est plus la seule personne au monde à connaître son mot de passe, alors celui-ci n'est plus une preuve valable de son identité. Alors comment protéger son mot de passe?


## Avez-vous été *pwned*?

Comment fait-on pour obtenir le mot de passe de quelqu'un? Il y a plusieurs manières de s'y prendre.

### Attaque par force brute

La manière la plus intuitive d'obtenir le mot de passe de quelqu'un est de tous les essayer un par un, souvent en commençant par [les plus fréquents](https://en.wikipedia.org/wiki/List_of_the_most_common_passwords). C'est peu pratiquable pour un humain, mais c'est très facile à faire pour un script. On appelle ce stratagème une [**attaque par force brute**](https://fr.wikipedia.org/wiki/Attaque_par_force_brute) (*brute force attack*).

Cette méthode est généralement peu efficace car la plupart des applications vont contrer automatiquement ce stratagème. Plusieurs contremesures:
- Bloquer l'application après un certain nombre de tentatives
- Ajouter un délai de traitement de plus en plus long entre les tentatives
- Imposer un CAPTCHA, c'est-à-dire poser une question facile à répondre pour les humains mais difficile pour les ordinateurs

Par ailleurs, ce type d'attaque est dangereux pour l'attaquant puisqu'il s'expose au risque de se faire prendre. Une attaque par force brute laisse souvent des traces et a le potentiel de déclencher des alertes qui pourraient attirer l'attention des équipes de sécurité.

Le site [PasswordMonster.com](https://www.passwordmonster.com/) permet d'estimer la force d'un mot de passe et combien de temps il faudrait pour le deviner à l'aide d'une attaque par force brute.


### Craquage

Si un *hacker* arrive à mettre la main sur une base de données contenant des mots de passe, ou des hash, il peut tenter de craquer ces derniers comme on a vu au dernier cours. Si les mots de passe ne sont pas salés ou que le hacker a accès à des ordinateurs très performants, il pourrait arriver à retrouver le mot de passe ou un équivalent. Un mot de passe long peut aider à empêcher que votre mot de passe ne soit craquable.


### Ingénierie sociale

Il arrive que les humains soient plus faciles à tromber que les systèmes. Par exemple, le hacker peut faire une recherche sur l'utilisateur pour tenter de deviner son mot de passe, ou encore le contacter en se faisant passer pour un technicien légitime et lui demander son mot de passe. Normalement, vous devriez choisir des mots de passe qui ne sont pas facilement devinables et ne jamais le communiquer à qui que ce soit. Dès que votre mot de passe est connu d'une autre personne que vous, il n'est plus sécuritaire.


### Surveillance du clavier

On a beau chiffrer le mot de passe dans sa transmission et son stockage, il est toujours en texte clair lorsque l'utilisateur le tape sur le clavier. Donc en surveillant les frappes de touches, on peut connaître le mot de passe.


### Surveillance du presse-papier

Et même si le mot de passe n'est pas tapé sur le clavier, qu'on préfère le copier-coller pour ne pas qu'il soit détectable sur le clavier, sachez que le presse-papier (la zone de la mémoire qui contient les données copiées en attente d'être collées) n'est pas chiffré non plus. Il est possible de visualiser le contenu du presse-papier avec un [outil spécialisé](https://www.nirsoft.net/utils/inside_clipboard.html).


### Voler les mots de passe ailleurs

Plusieurs personnes utilisent le même mot de passe pour plusieurs applications. C'est une mauvaise pratique, puisque si l'une de ces applications est compromise, l'attaquant peut deviner votre mot de passe et usurper votre identité sur toutes les autres. C'est particulièrement dangereux si, en plus, votre nom d'utilisateur est aussi le même.

Le site [Have I Been Pwned](https://haveibeenpwned.com/) peut vous aider à identifier le risque que l'un de vos mots de passe soit compromis. Entrez votre identifiant ou votre adresse courriel et l'application vous dira s'il se trouve dans une liste de comptes qui ont fuité. Le cas échéant, il pourrait être judicieux de modifier votre mot de passe, sur cette application mais aussi sur les autres.


## Stratégies pour gérer ses mots de passe

Pour bien gérer ses mots de passe tout en gardant une bonne taille et un niveau de complexité raisonnable, tout en évitant qu'il soit toujours le même partout, on peut recourir à un gestionnaire de mots de passe. Il y a deux types de gestionnaire de mots de passe:

### Stocker les mots de passe localement

Au lieu d'avoir un fichier texte ou Excel avec tous nos mots de passe écrits en clair, il existe des applications qui permettent de stocker localement notre liste de mots de passe de manière sécuritaire, dans un fichier fortement chiffré. Un des plus connu est [KeePass 2](https://keepass.info/download.html).

L'application permet de manipuler des fichiers KBDX qui contiennent des comptes et des mots de passe. Le fichier KBDX est chiffré et exige un mot de passe. On a avantage à choisir un mot de passe solide puisque c'est celui-ci qui donnera accès à tous les autres mots de passe contenus dans le fichier. On peut créer un compte et laisser générer des mots de passe longs et complexes aléatoirement. Pour utiliser le mot de passe, on n'a qu'à le copier-coller; l'application efface automatiquement le presse-papier après usage.


### Stocker les mots de passe dans le nuage

Une autre approche consiste à conserver les mots de passe dans le *cloud*. Il existe plusieurs options, gratuites ou payantes. 

  - [BitWarden](https://bitwarden.com/)
  - [LastPass](https://www.lastpass.com/)
  - [NordPass](https://nordpass.com/fr/)
  - [ProtonPass](https://proton.me/pass)
  - etc.

Ces applications ont l'avantage d'être faciles d'utilisation et centralisées, donc utilisables sur une multitude d'appareils. Mais avant de transférer nos mots de passe, il ne faut pas oublier que ceux-ci deviennent connus de ce fournisseur. S'il se fait pirater, tous nos mots de passe sont compromis d'un coup. Il faut avoir une confiance inébranlable envers la compagnie à qui on confie nos mots de passe.


## Facteurs d'authentification

On a beau employer toutes les bonnes pratiques dans la construction et la gestion de nos mots de passe, il y a toujours un risque que celui-ci soit compromis. Donc même si le mot de passe constitue souvent une preuve acceptable de l'identité d'un utilisateur, ce n'est pas parfait. Pour améliorer la fiabilité de l'authentification, nous allons souvent exiger des preuves supplémentaires. On appelle ces preuves des **facteurs d'authentification**.

Les facteurs d'authentification se déclinent en trois grandes catégories: mémoriel (ce que je sais), matériel (ce que je possède) et corporel (ce que je suis). Un seul de ces facteurs peut être compromis, mais si on en exige plus d'un en même temps, on réduit le risque de compromission. C'est ce qu'on appelle l'**authentification multifactorielle** (*MFA*, *2FA*). 

### Le facteur mémoriel (ce que je connais)

Une manière de prouver mon identité consiste à fournir une information **connue** uniquement de moi et du système.

Quelques exemples de facteurs mémoriels:
- Un mot de passe
- Un NIP
- Les réponses aux questions de sécurité

### Le facteur matériel (ce que je possède) 

On peut également prouver notre identité en procurant la preuve de **possession** d'un objet.

Quelques exemples de facteurs matériels:
- Un téléphone cellulaire
- Une clé USB particulière
- Une carte d'identité électronique
- Un jeton [SecurID](https://en.wikipedia.org/wiki/RSA_SecurID)


### Le facteur corporel (ce que je suis)

On recourt de plus en plus à la **biométrie** en tant que facteur d'authentification. Bien que des caractéristiques biométriques soient difficiles à falsifier en théorie, elles sont aussi plus difficiles à mesurer de manière fiable. Aussi, elles sont souvent stockées localement sur les appareils plutôt que dans des systèmes centralisés.

Quelques exemples de facteurs biométriques:
- Une empreinte digitale
- La reconnaissance faciale
- La reconnaissance vocale


## Outils

### Keepass

[KeePass](https://keepass.info/) est un gestionnaire de mots de passe. Il permet de créer un fichier sécurisé contenant plusieurs mots de passe et inclut des outils pour générer des mots de passe complexes aléatoirement. Il est particulièrement pratique pour gérer des mots de passe de comptes de service dans les départements TI.


### InsideClipboard (NirSoft)

[InsideClipboard](https://www.nirsoft.net/utils/inside_clipboard.html) est un outil qui permet de visualiser le contenu du presse-papier de Windows.


### Clipboardic (NirSoft)

[Clipboardic (NirSoft)](https://www.nirsoft.net/utils/clipboardic.html) est un outil qui permet d'enregistrer l'historique du contenu du presse-papier de Windows.

