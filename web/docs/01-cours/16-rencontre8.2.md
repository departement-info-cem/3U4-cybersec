---
id: r16
title: Rencontre 16 - Malware et antivirus
sidebar_label: R16 - Malware et antivirus
draft: false
hide_table_of_contents: false
toc_max_heading_level: 4
---

Dans cette séance de cours, nous étudierons les différents types de logiciels malveillants ainsi que les manières de s'en protéger.

Mais tout d'abord, quelques exercices de rappel sur le NAT, vu au dernier cours.

## Retour sur le NAT

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



## Les logiciels malveillants (*malware*)

Parfois appelés &laquo;&nbsp;virus&nbsp;&raquo; par abus de langage, les logiciels malveillants, ou *malware*, désignent tout logiciel ou programme informatique qui, lorsqu'on l'exécute sur un ordinateur, effectue des actions qui ne sont pas consenties par l'utilisateur du système et qui ne sont pas dans son avantage. Il en existe plusieurs types, mais tous ont en commun d'être du **code exécutable**.

Voici les principaux types de *malware*:


- **Les virus :**
Un virus est un programme qui s'attache à un logiciel ou un fichier légitime et se propage lorsque ce logiciel est exécuté ou ce fichier est lu. Son mode de propagation passe par un fichier. De nos jours, les réels virus ne sont plus très répendus, mais le terme &laquo;&nbsp;virus&nbsp;&raquo; est utilisé pour désigner d'autres types de logiciels malveillants.

- **Les vers (*worms*) :**
Un ver informatique est un logiciel malveillant, sous la forme d'un programme exécutable ou d'un script, qui se propage en se répliquant sur un réseau. Leur code comprend généralement deux parties distinctes:
  - La **charge utile**, ou *payload*. Cette partie détermine ce que fait le ver: endommager des fichiers, voler des données, envoyer un DDoS, etc.
  - Le mécanisme de **réplication**, qui scanne le réseau à la recherche d'autres hôtes vulnérables afin de se répliquer

- **Les chevaux de Troie (*trojan*) :**
Un cheval de Troie est un type de *malware* qui se présente comme un logiciel légitime. Il tente d'être perçu par les utilisateurs comme attrayant, les incitant à l'exécuter. Une fois lancé, le programme déballe sa charge utile, qui généralement permet à l'attaquant d'accéder au système à distance.

- **Les logiciels espions (*spyware*) :**
Un *spyware* collecte des informations sur l'utilisateur à son insu, comme les mots de passe, les informations de paiement ou l'activité en ligne. 

- **Les publiciels (*adware*) :**
Un *adware* sert à afficher des publicités indésirables à l'utilisateur. Il peut suivre les activités de navigation et même modifier le comportement des navigateurs. Le but premier de ce type de logiciel malveillant est de faire des revenus publicitaires.

- **Les rançongiciels (*ransomware*) :**
Un *ransomware* est un code exécutable qui, lorsqu'il est lancé, chiffre le maximum de données appartenant à la victime, les rendant inaccessibles. Il demande ensuite une rançon à la victime avec promesse de restituer ses données. Même après paiement de la rançon, il n'y a aucune garantie que les données seront récupérées.

- **Les *keyloggers* :**
Un *keylogger* est un logiciel qui enregistre les frappes de touches clavier pour voler des informations sensibles comme des mots de passe et des NIP de carte bancaire.

- **Les *botnets* et les *zombies* :**
Le *botnet* est un réseau de machines infectées par un logiciel malveillant pouvant être contrôlées à distance par un attaquant. La machine infectée est parfois appelée un &laquo;&nbsp;*zombie*&nbsp;&raquo;. Le programme de botnet est souvent très petit et peu intrusif, se contentant de faire des requêtes de temps à autres sur un serveur contrôlé par l'attaquant; c'est par là que ce dernier peut passer des commandes. L'usage le plus fréquent d'un botnet est l'attaque par déni de service distribué (DDoS).

- **Les *rootkits* :**
Un *rootkit* est un logiciel installé à très bas niveau et est activé très tôt dans le processus de démarrage de l'ordinateur. Il permet d'influencer le chargement du système d'exploitation en vue de modifier son fonctionnement, de façon à obtenir des privilèges d'administration ou de masquer d'autres *malwares*.


## Comment se protéger

### La vigilance

La règle d'or est la vigilance. 

- Ne cliquez pas sur des liens en lesquels vous n'avez pas confiance.
- Ne téléchargez pas de fichiers sans en être certain de la provenance, en particulier les fichiers exécutables et les scripts.
- Si vous téléchargez un fichier exécutable, ne l'exécutez pas.
- N'insérez pas d'appareils comme une clé USB, qui pourraient contenir du code malicieux.


### Les antivirus 

Un antivirus est un logiciel conçu pour détecter, prévenir et supprimer les logiciels malveillants (*malware*) sur un ordinateur ou un réseau. 


#### Comment ils détectent les malwares?

Un logiciel antivirus utilise plusieurs procédés pour détecter les logiciels malveillants.

Il compare les fichiers et les programmes sur l'ordinateur avec une base de données de **signatures** de malwares connus. Chaque antivirus possède sa manière de calculer la signature, en analysant des fragments de code, des motifs récurrents ou des séquences uniques. L'antivirus peut scanner l'entièreté du système (scan complet) ou un fichier spécifique à la demande. On peut configurer l'antivirus pour effectuer des scans périodiques à l'intervalle et au moment voulu. La majorité des antivirus vont aussi effectuer des analyses **en temps réel** dès qu'un fichier arrive sur l'ordinateur.

L'antivirus tente aussi d'identifier les menaces en utilisant des méthodes d'**analyse heuristique**. Plutôt que de rechercher des similitudes dans le contenu des fichiers, il analyse le comportement des programmes pour détecter des activités suspectes ou malveillantes. Ce type d'analyse permet d'intercepter des logiciels malveillants même si ceux-ci sont totalement inédits et ne sont pas encore répertoriés dans la base de données de signatures.

Si l'antivirus identifie un fichier comme un *malware*, celui-ci peut prendre action immédiatement, en supprimant le fichier ou en le plaçant en **quarantaine** (le fichier est toujours là, mais le système d'exploitation interdit son exécution).


#### Comment les maintenir?

Les logiciels malveillants évoluent rapidement, donc les antivirus doivent suivre la cadence. 

Il ne suffit pas d'installer ou d'activer un antivirus, il faut le mettre à jour régulièrement. Les mises à jour apportent de nouvelles signatures reconnaissables et de nouvelles méthodes de détection heuristique.


#### Lequel choisir?

Il y a un grand nombre de logiciels antivirus sur le marché. 

Pour comparer adéquament les antivirus, on peut rechercher les caractéristiques suivantes:
- Un haut taux de détection de vrais malwares
- Un faible taux de faux positifs (un fichier légitime qui est considéré à tort comme un virus)
- Une faible empreinte sur les ressources du système (il ne ralentit pas les ordinateurs)
- La mise à jour régulière et fréquente des signatures
- Un lien de confiance élevé (par exemple, l'antivirus russe Kaspersky est-il digne de confiance?)
- Des capacités d'administration centralisée (pour les admins d'entreprises)

Sous Windows 10 et 11 ainsi que les éditions serveur, l'antivirus **Windows Defender** est intégré au système d'exploitation. Il offre une protection de base contre les malware. Contrairement à la croyance populaire, il est assez performant et fiable et offre une protection en temps réel. Il utilise une protection basée sur le Cloud pour analyser les menaces en temps réel. Il fait partie de la solution Windows Defender qui incorpore aussi des protections contre le *phishing* et un service de pare-feu.

Cependant, certains antivirus commerciaux offrent typiquement des taux de détection légèrement supérieurs ainsi que des fonctionnalités supplémentaires afin d'offrir une plus-value. Par exemple, la suite **Norton 360** offre une solution VPN intégrée, un gestionnaire de mots de passe et une surveillance du dark web. La suite **McAfee** offre des outils de nettoyage et une protection Web avancée. Il s'agit d'arguments de vente, bien sûr.

Des antivirus gratuits existent aussi, mais ont perdu en popularité depuis que Windows incorpore le sien. Les principaux sont **Avast**, **AVG** et **Avira** et offrent tout de même des niveaux de protection et une performance comparables aux antivirus commerciaux.

Le site [av-test.org](https://www.av-test.org/fr/antivirus/) répertorie une analyse détaillée des principaux antivirus sur le marché.

Lien: https://www.av-test.org/fr/antivirus/


:::caution Les faux antivirus
Il faut faire attention aux faux antivirus qui nous sont offerts dans des annonces publicitaires. On voit à l'occasion des faux messages indiquant qu'un virus a été détecté, en vous proposant un lien vers un antivirus capable de vous protéger. Ne vous laissez pas berner!
:::


#### Mythe ou réalité?

On nous a toujours dit que les antivirus ne sont utiles que sous Windows, car il n'existe pas de logiciels malveillants sur Mac, Linux ou Android. Est-ce vrai?


### Et si mon antivirus ne détecte rien?

Même le meilleur antivirus pourrait passer à côté d'un logiciel malveillant. Il arrive qu'on doive se débarasser d'un *malware* par nous-mêmes. Sous Windows, certains outils peuvent nous aider à les identifier et les éliminer.


#### Le gestionnaire de tâches

Le gestionnaire de tâches permet de voir tous les processus en cours d'exécution sur le système. 

![Gestionnaire de tâches](taskmgr.png)

On y accède de plusieurs manières:
- La séquence de touches `Ctrl` + `Maj` + `Esc`
- Clic-droit sur la barre de tâches, cliquer sur Gestionnaire de tâches
- Clic-droit sur le menu Démarrer, cliquer sur Gestionnaire de tâches
- Séquence de touches `Ctrl` + `Alt` + `Suppr.`, cliquer sur Gestionnaire de tâches
- La commande `taskmgr`

Il faut aller dans l'**onglet Détails** pour voir l'ensemble des processus. 

Lorsque vous identifiez un fichier exécutable qui vous semble suspect ou anormal, vous pouvez cliquer-droit et:
- ouvrir les propriétés pour voir certains détails de ce fichier ainsi que son emplacement
- effectuer une recherche en ligne
- terminer l'exécution du fichier
- ouvrir l'emplacement du fichier dans l'explorateur


#### Scan de virus à la demande

Si vous avez un fichier exécutable mais n'êtes pas certain(e) de sa légitimité, vous pouvez scanner ce fichier à l'aide de votre antivirus. Souvent, cela peut se faire directement à partir du menu contextuel. Référez-vous à la documentation de votre antivirus.

![Scan à la demande](ondemandscan.png)


#### Virustotal.com

Pour des résultats plus détaillés, vous pouvez téléverser votre fichier suspect chez [VirusTotal](https://www.virustotal.com/) pour qu'il soit tester avec le maximum d'antivirus sur le marché. VirusTotal fait une analyse à la demande auprès de plusieurs dizaine de logiciels et vous indique quels sont ceux qui y ont trouvé une menace. Plus la proportion d'antivirus trouvant une menace est élevée, plus grandes sont les chances que le fichier soit effectivement malicieux.

Lien: https://www.virustotal.com/

![VirusTotal.com](virustotal.png)

:::info Process Explorer
L'outil [Process Explorer](https://learn.microsoft.com/fr-ca/sysinternals/downloads/process-explorer) de Microsoft est une sorte de gestionnaire de tâches mais avec plusieurs fonctionnalités avancées. Entre autres, il permet d'analyser automatiquement les processus en cours d'exécution avec VirusTotal.

![Process Explorer et Virus Total](procexp.png)

Téléchargement: https://learn.microsoft.com/fr-ca/sysinternals/downloads/process-explorer
:::


#### Autoruns

Souvent, un logiciel malveillant est un programme qui s'exécute en arrière-plan du système. Sous Windows, il existe plusieurs manières d'exécuter un programme automatiquement au démarrage de l'ordinateur ou de la session utilisateur:

- Dans le planificateur de tâches
- Comme service
- Dans le répertoire Startup du menu Démarrer
- Dans la clé Run de la base de registre
- Etc.

Les programmeurs de malware peuvent parfois être très créatifs dans leur manière de lancer leur programme automatiquement.

L'outils [Autoruns](https://learn.microsoft.com/fr-ca/sysinternals/downloads/autoruns) dresse la liste des programmes qui démarrent automatiquement. Si vous vous demandez pourquoi un programme est lancé automatiquement à chaque démarrage, vous risquez de le trouver là.

![Autoruns](autoruns.png)

Téléchargement: https://learn.microsoft.com/fr-ca/sysinternals/downloads/autoruns


## Exercice sur les *malwares*

Vous aurez besoin d'une machine virtuelle pour faire cet exercice.

- Téléchargez le fichier suivant `\\ed5depinfo\Logiciels\_Cours\3U4\Win10_RickRollVM.7z`
- Extrayez la VM sous `C:\VM\VMware`
- Double-cliquez sur le fichier `.VMX`
- Dans VMWare Workstation Pro, démarrez la machine virtuelle
- Loggez-vous avec le compte d'Alice
  - Nom d'utilisateur: `alice`
  - Mot de passe: `Passw0rd`

Vous remarquerez sans doute que cette VM vous *rickroll* continuellement. Il semble qu'un programme malveillant a réussi à passer sans être détecté par l'antivirus.

Utilisez les outils présentés précédemment pour détecter et éliminer le logiciel malveillant. N'hésitez pas à demander l'aide du prof si vous êtes bloqué.
