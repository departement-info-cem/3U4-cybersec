---
id: r16
title: Rencontre 16 - Services d'accès distant
sidebar_label: R16 - Services d'accès distant
draft: false
hide_table_of_contents: false
toc_max_heading_level: 4
---

## Retour sur le NAT

Joris a développé une application permettant de visualiser le fonctionnement d'une passerelle NAT.

https://jorisdeguet.github.io/nat/dist/index.html 



### Exercice de rappel

Dans la situation suivante:
- mon adresse IP privée est **10.14.251.2**
- j'envoie une requête HTTPS au serveur whatismyipaddress.com (adresse IP est **104.19.222.79**)
- le port source a été fixé à **51843**
- mon routeur à la maison applique un NAT dynamique et son adresse IP publique est **24.200.194.42**


📝 Remplissez les en-têtes IP et TCP demandées pour la requête reçue **par le serveur**:
```
IP destination:............. ___.___.___.___
IP source:.................. ___.___.___.___
TCP port destination:....... _____
TCP port source:............ 63748
HTTPS:...................... Encrypté
```

📝 À quoi ressemble l'entrée correspondante dans la table de nattage de votre routeur?
```
┌─────────────────────────────────────╥───────────────────────────────────────┐
│                PRIVÉ                ║                PUBLIC                 │
├─────────────────────────────┬───────╬───────────────────────────────┬───────┤
│ Adresse IP                  │ Port  ║ Adresse IP                    │ Port  │
╞═════════════════════════════╪═══════╬═══════════════════════════════╪═══════╡
│ ___.___.___.___             │ _____ ║ 24.200.194.42                 │ _____ │
└─────────────────────────────┴───────╨───────────────────────────────┴───────┘
```




## Contrôle à distance

La grande majorité des réseaux IPv4 sont situés derrière un NAT. Pour rappel, une passerelle NAT ne met le réseau privé en contact avec Internet que pour les connexions **qui sont initiées par un hôte du réseau privé**. À moins d'avoir configuré une redirection statique de port, toute communication initiée à partir de l'Internet est automatiquement refusée, faute d'entrée dans la table de nattage.

Sachant celà, comment un peut-on prendre le contrôle d'un appareil situé dans un réseau privé, comme à la maison ou au collège, à partir d'Internet?



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



