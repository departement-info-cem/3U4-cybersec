---
id: r15
title: Rencontre 15 - NAT et accès distant
sidebar_label: R15 - NAT et accès distant
draft: false
hide_table_of_contents: false
toc_max_heading_level: 4
---

Est-il possible pour un hacker de prendre le contrôle de votre poste de travail à la maison à partir d'Internet, par SSH ou RDP ou d'autres protocoles de prise de contrôle à distance? Comment les hackers peuvent s'y prendre? Et comment se protéger contre ces attaques?

Mais tout d'abord, parlons des passerelles NAT, qui sont très communes dans les entreprises comme les ménages.


## Les passerelles NAT

Vous avez probablement un NAT à la maison, peut-être sans le savoir. C'est un appareil qui garantit un accès à Internet tout en isolant votre réseau interne de plusieurs menaces provenant de l'extérieur. Voici comment ça fonctionne.


### Qu'est-ce qu'une passerelle NAT?

Avec le protocole IP version 4, l'adressage IP se fait sur 32 bits. Cela donne $2^{32}$  adresses possibles, soit environ 4 milliards, ce qui semblait énorme au départ. En théorie, chaque ordinateur doit avoir une adresse IP unique pour communiquer adéquatement dans le réseau, mais avec le succès qu'Internet a connu, on a maintenant beaucoup plus que 4 milliards d'appareils connectés sur Internet et nécessitant une adresse unique. 

Le NAT (Network Address Translation) vise à résoudre ce problème. C'est une technique utilisée dans les réseaux informatiques pour modifier les adresses IP dans les en-têtes des paquets IP en cours de transit à travers un routeur ou un pare-feu. On utilise souvent cette technique lorsqu'on a plus de machines dans notre réseau interne que d'adresse IP publiques à notre disposition. 

Par exemple, à la maison, votre fournisseur d'accès Internet vous donne une seule adresse IP publique, routable sur Internet. Toutefois, vous avez plus d'une machine à la maison (ordinateurs, cellulaires, tablettes, télés connectées, etc.). Ainsi, vous pouvez avoir un grand nombre d'appareils qui partagent la même adresse IP sur Internet.

Comment est-ce possible? Comment le serveur peut savoir à qui retourner la réponse à une requête si plusieurs clients partagent la même adresse IP?

La passerelle NAT possède deux catégories d'interfaces réseau: une interface **WAN** (*wide area network*), du côté de votre fournisseur Internet, et une ou plusieurs interfaces **LAN** (*local area network*). La passerelle agit comme serveur DHCP du côté LAN pour assigner une adresse IP privée à tous les hôtes de votre réseau local (typiquement dans la plage 192.168.x.x ou 10.x.x.x). Dès que votre ordinateur tente d'envoyer un paquet IP vers Internet, le NAT intercepte ce paquet et modifie l'adresse IP de la source pour son adresse publique. Il envoie le paquet au serveur tout en gardant une copie de l'échange dans sa mémoire. Dès que la passerelle NAT reçoit la réponse du serveur, il regarde dans sa liste pour savoir quelle machine de son réseau interne a envoyé le paquet, puis modifie à nouveau l'entête du paquet et le renvoie au demandeur.

Le fonctionnement du NAT est différent de celui d'un pare-feu car il ne fonctionne pas au moyen de règles de filtrage. Il procure quand même un bon niveau de protection contre les attaques provenant d'Internet en rendant invisible de l'extérieur toutes les machines du réseau local. Si un attaquant tente de se connecter à mon ordinateur se situant derrière un NAT, c'est le NAT qui recevra la requête, et comme cette communication n'a pas été initiée par ma machine, le NAT détruira tout simplement cette requête.


### Principe de fonctionnement

On va distinguer:
- Une adresse **publique** sur l'internet (**unique pour toute l'humanité**) 
- Une adresse **privée** sur un réseau local (**unique sur le réseau local**)

:::info Comment reconnaître une adresse privée?
Les adresses IPv4 privées sont des plages d'adresses IP réservées pour une utilisation au sein de réseaux privés. Elles ne sont pas routables sur l'Internet public. Voici les plages d'adresses IPv4 privées définies par le [RFC 1918](https://tools.ietf.org/html/rfc1918):

| Classe | Adresse réseau   | De:           | À :               |
| :----- | :--------------- | :------------ | :---------------- |
| A      | `10.0.0.0/8`     | `10.0.0.0`    | `10.255.255.255`  |
| B      | `172.16.0.0/12`  | `172.16.0.0`  | `172.31.255.255`  |
| C      | `192.168.0.0/24` | `192.168.0.0` | `192.168.255.255` |

Par ailleurs, il existe d'autres plages d'adresses IP qui ne sont pas routables sur Internet mais qui ne doivent pas non plus être utilisées dans des réseaux privés, sauf dans quelques cas spécifiques:
- La plage `169.254.0.0/16` (169.254.0.0 à 169.254.255.255), appelée APIPA, sert à permettre l'établissement de réseaux *ad hoc* lorsqu'il n'y a pas de serveur DHCP accessible par les clients. Cette norme est définie dans le [RFC 3927](https://tools.ietf.org/html/rfc3927). 
- La plage `224.0.0.0/4` (224.0.0.0 à 239.255.255.255), appelée Classe D, est réservée à des usages de multidiffusion (multicast), et la plage `240.0.0.0/4` (240.0.0.0 à 255.255.255.255), appelée Classe E, est réservée à des usages expérimentaux. Aucune de ces adresses n'est autorisée sur Internet et dans des des réseaux privés routés.
:::

---

### Exemple

import Tabs from '@theme/Tabs';
import TabItem from '@theme/TabItem';

<Tabs>

<TabItem value="scenario" label="Scénario">

![Schema réseau](r15a.png)

À partir de votre ordinateur à la maison, vous ouvrez un navigateur pour consulter votre site Web préféré:

- L'adresse IP privée de votre ordinateur (la **source**) est `192.168.0.110`, son masque est `255.255.255.0` et sa passerelle par défaut est `192.168.0.1`.
- Le "routeur" (**NAT**) à la maison possède deux adresses IP: 
  - Son adresse interne (**LAN**) est `192.168.0.1`. Elle concorde avec l'adresse de la passerelle par défaut de l'ordinateur. 
  - Son adresse externe (**WAN**) est `40.40.40.121`. Elle lui a été attribuée par le fournisseur d'accès Internet (FAI).
- Le serveur Web qui héberge le site Web (la **destination**) possède l'adresse IP `45.45.45.45` et répond au protocole HTTPS, sur le port `443/tcp`.

Voici, étape par étape, ce qui se passe. Bien sûr, ce processus est simplifié pour faciliter la compréhension. 

</TabItem>


<TabItem value="etape1" label="Étape 1">

![Étape 1](r15-ex1.png)

Ton ordinateur crée une requête Web vers le serveur. Il ouvre un port temporaire au hasard pour lui permettre de recevoir la réponse. Il crée un paquet à envoyer au serveur.
- Source: `192.168.0.110` port `4545`
- Destination: `45.45.45.45` port `443`

Puisque l'adresse de destination est dans un réseau différent, le paquet est acheminé à la passerelle par défaut.

</TabItem>

<TabItem value="etape2" label="Étape 2">

![Étape 2](r15-ex2.png)

La passerelle par défaut (le *routeur*) ne peut pas acheminer tel quel le paquet, puisque l'adresse de source fait partie des plages d'adresses privées, interdites sur Internet. Autrement dit, il est impossible que le serveur Web lui réponde. Il doit effectuer une **traduction NAT**.

- L'adresse de source privée (`192.168.0.110`) va être remplacée par la sienne, qui est publique (`40.40.40.121`).
- Le port de source va être remplacé par un port au hasard disponible dans la table de NAT (par exemple, `10003`)
- Il crée une entrée dans sa table NAT, pour se rappeler à quel ordinateur, dans le réseau privé, acheminer la réponse du serveur.
  - L'adresse IP locale (`192.168.0.110`)
  - Le port de retour original (`4545`)
  - Le port de retour modifié (`10003`)

Puis il envoie le paquet modifié au routeur du fournisseur d'accès Internet. Les routeurs IP de l'Internet acheminent les paquets à la machine du serveur.

</TabItem>

<TabItem value="etape3" label="Étape 3">

![Étape 3](r15-ex3.png)

Le serveur reçoit la requête et produit une réponse. Il la met dans un paquet IP à retourner au client:
- Source = `45.45.45.45` port `443`
- Destination = `40.40.40.121` port `10003` (l'adresse et le port de la partie publique du routeur)

Les routeurs IP de l'internet acheminent les paquets au routeur de la maison, à l'adresse `40.40.40.121`.

</TabItem>

<TabItem value="etape4" label="Étape 4">

![Étape 4](r15-ex4.png)

Le routeur de la maison reçoit le paquet en provenance de `45.45.45.45` sur son port `10003`. Il consulte sa table de NATtage pour savoir quelle machine interne a initié cette communication. 

Puisqu'il y a une entrée correspondant au port `10003`, il construit un nouveau paquet avec comme destination l'adresse interne inscrite dans cette entrée de la table, `192.168.0.110` et le port `4545`.

Si aucune entrée correspondant au port `10003` n'avait été trouvé dans la table, le routeur n'aurait pas été capable d'acheminer cette communication dans le réseau interne. Il aurait donc ignoré le paquet, causant sa destruction immédiate.

</TabItem>

</Tabs>

---

Quelques observations:
- il y a une modification d'adresse IP et port à l'aller et au retour
- on part de l'hypothèse que le protocole respecte les conventions IP et TCP sur les adresses et ports
- s'il n'y a pas de paquet qui est sorti du réseau local, un paquet ne peut pas arriver au routeur et être envoyé à une machine avec adresse privée

![Principe de fonctionnement d'un NAT](nat.png)


### Exercice 1

Dans la situation suivante:
- mon adresse IP privée est **192.168.0.111**
- j'envoie une requête HTTPS au serveur cegepmontpetit.ca (adresse IP est **35.203.2.187**)
- le port source a été fixé à **6054**
- mon routeur à la maison applique un NAT dynamique son adresse IP publique est **77.88.99.11**
- après le passage de la requête la table NAT contient une entrée
  - IP **192.168.0.111**
  - port source original **6054**
  - port source traduit **6754**

📝 Remplissez les en-têtes IP et TCP demandées pour la requête reçue par le serveur:
```
IP destination:............. ___.___.___.___
IP source:.................. ___.___.___.___
TCP port destination:....... _____
TCP port source:............ _____
HTTPS:...................... Encrypté
```

### Exercice 2

La requête locale:

```
IP destination:............. 66.147.239.61
IP source:.................. 192.168.1.45
TCP port destination:....... 443
TCP port source:............ 9876
HTTPS:...................... Encrypté
```

a été traduite par NAT dans la requête:

```
IP destination:............. 66.147.239.61
IP source:.................. 192.222.150.48
TCP port destination:....... 443
TCP port source:............ 5675
HTTPS:...................... Encrypté
```

📝 Quelle entrée a été ajoutée dans la table NAT?
```markdown

```

📝 Quelle est l'adresse IP **publique** du routeur NAT?
```markdown

```



### Questions / discussions

1. Selon vous, si 2 ordinateurs de ma maison sur le Wifi envoient tous les deux des requêtes au même serveur
comment le routeur peut savoir à quel ordi, il doit envoyer une réponse quand elle arrive de l'Internet?

2. Si à chaque fois que j'envoie une requête vers l'Internet, il se crée une entrée dans une table de NAT,
depuis le temps comment la mémoire vive de mon routeur n'a pas explosé?

3. Quand je suis à la maison, c'est quoi mon adresse IP?


### Sens de l'initiation

Derrière un NAT, dans le réseau local, les appareils possèdent une adresse IP privée, non routable. Les appareils dans le réseau local ne sont pas conscients de leur adresse IP publique puisque c'est le NAT qui s'occupe de faire la traduction pour eux. Conséquemment, les appareils sur Internet ne connaissent pas ceux du réseau local; la seule manière qu'ils ont de communiquer avec eux est via la passerelle NAT, qui se rappelle de qui a initié la demande.

Autrement dit, les appareils derrière un NAT ne peuvent recevoir du trafic que lorsqu'ils ont eux-mêmes initié une connexion. Le seul appareil du réseau local qui puisse être accédé directement sans avoir initié la communication est la passerelle NAT elle-même. Si elle reçoit un paquet qui n'a pas été initié par un appareil du réseau privé, ce paquet sera détruit.

**Le NAT constitue donc une protection très efficace contre les cyberattaques provenant d'Internet, pour autant que la passerelle soit exempte de vulnérabilités.**


### Redirection de port

Pour permettre à du trafic initié à l'extérieur du réseau local d'être acheminé à une machine du réseau privé, on peut configurer la passerelle NAT pour ouvrir un port d'écoute et le rediriger vers une machine du réseau interne, en effectuant la traduction dans l'autre sens. Ce procédé s'appelle la **redirection de port**.

Par exemple, un serveur Web situé dans le réseau local (par exemple, 192.168.0.105) écoute sur le port 443 (https). Un client sur Internet accède au serveur avec l'adresse du NAT (40.40.40.121) sur son port 443. Elle redirige les requêtes entrantes sur ce port au serveur du réseau interne et réachemine les réponses aux clients en ayant fait la demande. Dans la perspective du client, le serveur Web est la passerelle NAT.

Pour que la redirection de port fonctionne, il faut préalablement configurer le NAT en spécifiant un port d'écoute, ainsi que l'adresse privée et le port du serveur. Fait intéressant, le port externe et le port interne ne sont pas forcément les mêmes. On pourrait avoir le NAT qui écoute sur le port 443 mais qui redirige ses requêtes sur le port 1234 sur le serveur interne.


## Contrôle à distance

Sachant que la grande majorité des réseaux IPv4 sont situés derrière un NAT, on rappelle la question de départ: comment un peut-on prendre le contrôle d'un appareil situé dans un réseau privé, comme à la maison ou au collège?


### Scénario 1: Le gentil technicien

Paul est technicien informatique et doit prendre le contrôle de la machine d'un client perdu, Jean. Jean est derrière un NAT. 

Selon vous:
- Est-ce que Paul peut utiliser une application qui envoie directement une demande à la machine de Jean? Mettons qu'il y a un serveur SSH sur la machine de Jean?
- Si ce n'est pas le cas, comment on fait?

Notre responsabilité surtout en IT est de s'assurer que la solution qu'on utilise pour administrer à distance n'ouvre pas une brèche pour hacker à distance.


### Scénario 2: Le méchant scammeur qui vient de l'étranger

1. Marie-Thérèse reçoit un texto qui lui indique que son compte à la BMO a été piraté.
2. Elle doit cliquer sur un lien pour résoudre le problème.
3. Quand elle clique sur le lien elle arrive sur une page qui décrit le problème et indique au scammer qu'il a une victime potentielle.
4. Il appelle sur le numéro de Marie-Thérèse, elle est devant son ordinateur sur la page du scammer
5. Il lui demande de télécharger un logiciel pour résoudre son problème.
6. Comme le site lui montre une page qui ressemble à celle de sa banque avec un montant de 0$, elle panique
7. La personne est une femme elle parle doucement et Marie-Thérèse est rassurée
8. Elle télécharge le logiciel qui initie une communication avec un serveur à travers son NAT
9. Le scammer peut maintenant via le même serveur communiquer directement avec le poste de Marie-Thérèse

Une fois que c'est fait, le scammer peut:
- collecter toutes les frappes clavier en mode keylogger
- accéder aux fichiers

Il n'est pas obligé d'obtenir des informations personnelles de la personne puisque l'application 
installée peut collecter des données, réinitier des communications plus tard avec le serveur etc.


### Les solutions d'accès à distance

Il y a deux grands types de solutions pour l'accès à distance.

#### Les services directs

Les services directs désignent les services d'accès à distance qui acceptent les connexions directes sans nécessiter d'intermédiaire. Ces services sont surtout utilisés par des administrateurs système qui ont accès au réseau local (soit physiquement ou à l'aide d'un VPN). Les appareils (serveurs, postes de travail, etc.) exécutent un service en arrière-plan qui écoutent sur un port spécifique en attente d'une connexion.

Les principaux services directs sont:

- SSH (Secure Shell): Ce protocole est surtout utilisé sur les systèmes Linux/UNIX et permet d'accéder à la ligne de commande. Son port par défaut est 22/tcp.
- RDP (Remote Desktop Protocol): Ce protocole est surtout utilisé sous Windows et permet d'accéder à l'interface graphique du système. Son port par défaut est 3389/tcp.
- VNC (Virtual Network Computing): Ce protocole, tout comme RDP, permet d'accéder à l'interface graphique à distance. Son port par défaut est 5900/tcp.

À moins de configurer de la redirection de ports, ces services ne sont pas utilisables à travers un NAT. 


#### Les services avec intermédiaire

Ces solutions utilisent des serveurs intermédiaires (souvent sur le Cloud) pour faciliter la connexion entre les ordinateurs. Elles sont souvent plus faciles à configurer et offrent des fonctionnalités supplémentaires comme le transfert de fichiers et les réunions en ligne. Leur principal avantage est de permettre leur utilisation à travers un NAT.

Sur l'ordinateur qu'on souhaite être contrôlable à distance, on installe un logiciel qui démarre automatiquement dès la mise sous tension de la machine. Plutôt que d'ouvrir un port d'écoute, de logiciel ouvre une connexion TCP avec un serveur sur Internet, ce qui fait en sorte que le NAT sait maintenant à quelle machine du réseau local acheminer le trafic provenant de ce serveur. C'est ce serveur qui agit comme intermédiaire et qui contrôle les autorisations d'accès.

Les principaux services sont:

- [Chrome Remote Desktop](https://remotedesktop.google.com) (Gratuit)
- [TeamViewer](https://www.teamviewer.com/) (Gratuit pour un usage non commercial)
- [LogMeIn Pro](https://www.logmein.com/fr/pro)
- [GoToMyPC](https://get.gotomypc.com/)
- [AnyDesk](https://anydesk.com/fr)
- [Microsoft Quick Assist](https://apps.microsoft.com/detail/9p7bp5vnwkx5?hl=fr-ca&gl=CA)


Ce sont aussi des services de ce type qui sont utilisés par les scammeurs et les hackers. En utilisant l'ingénierie sociale, ils arrivent à manipuler leur victime pour qu'elle installe une application qui démarre une connexion vers un serveur contrôlé par l'attaquant. Ça crée un &laquo;&nbsp;*backdoor*&nbsp;&raquo; qui permet à l'attaquant d'effectuer des actions à distance à travers le NAT, comme la prise de contrôle à distance, l'envoi de commandes ou le vol de données.
