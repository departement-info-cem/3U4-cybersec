---
id: r17
title: Rencontre 17 - Logiciels malveillants
sidebar_label: R17 - Logiciels malveillants
draft: false
hide_table_of_contents: false
---


## Les logiciels malveillants (*malware*)

Parfois appelés « virus » par abus de langage, les logiciels malveillants, ou *malware*, désignent tout logiciel ou programme informatique qui, lorsqu'on l'exécute sur un ordinateur, effectue des actions qui ne sont pas consenties par l'utilisateur du système et qui ne lui sont pas favorables. Il en existe plusieurs types, mais tous ont en commun d'être du **code exécutable**.

Voici les principaux types de *malware* :


- **Les virus :**
Un virus est un programme qui s'attache à un logiciel ou un fichier légitime et se propage lorsque ce logiciel est exécuté ou ce fichier est lu. Son mode de propagation passe par un fichier. De nos jours, les véritables virus ne sont plus très répandus, mais le terme « virus » est utilisé pour désigner d'autres types de logiciels malveillants.

- **Les vers (*worms*) :**
Un ver informatique est un logiciel malveillant, sous la forme d'un programme exécutable ou d'un script, qui se propage en se répliquant sur un réseau. Leur code comprend généralement deux parties distinctes :
  - La **charge utile**, ou *payload*. Cette partie détermine ce que fait le ver : endommager des fichiers, voler des données, lancer un DDoS, etc.
  - Le mécanisme de **réplication**, qui scanne le réseau à la recherche d'autres hôtes vulnérables afin de se répliquer.

- **Les chevaux de Troie (*trojan*) :**
Un cheval de Troie est un type de *malware* qui se présente comme un logiciel légitime. Il tente d'être perçu par les utilisateurs comme attrayant, les incitant à l'exécuter. Une fois lancé, le programme déploie sa charge utile, qui généralement permet à l'attaquant d'accéder au système à distance.

- **Les logiciels espions (*spyware*) :**
Un *spyware* collecte des informations sur l'utilisateur à son insu, comme les mots de passe, les informations de paiement ou l'activité en ligne.

- **Les publiciels (*adware*) :**
Un *adware* sert à afficher des publicités indésirables à l'utilisateur. Il peut suivre les activités de navigation et même modifier le comportement des navigateurs. Le but premier de ce type de logiciel malveillant est de générer des revenus publicitaires.

- **Les rançongiciels (*ransomware*) :**
Un *ransomware* est un code exécutable qui, lorsqu'il est lancé, chiffre un maximum de données appartenant à la victime, les rendant inaccessibles. Il demande ensuite une rançon à la victime avec la promesse de restituer ses données. Même après paiement de la rançon, il n'y a aucune garantie que les données seront récupérées.

- **Les *keyloggers* :**
Un *keylogger* est un logiciel qui enregistre les frappes clavier pour voler des informations sensibles comme des mots de passe et des NIP de carte bancaire.

- **Les *botnets* et les *zombies* :**
Le *botnet* est un réseau de machines infectées par un logiciel malveillant pouvant être contrôlées à distance par un attaquant. La machine infectée est parfois appelée un « *zombie* ». Le programme de botnet est souvent très petit et peu intrusif, se contentant de faire des requêtes de temps à autre sur un serveur contrôlé par l'attaquant ; c'est par là que ce dernier peut passer des commandes. L'usage le plus fréquent d'un botnet est l'attaque par déni de service distribué (DDoS).

- **Les *rootkits* :**
Un *rootkit* est un logiciel installé à très bas niveau et activé très tôt dans le processus de démarrage de l'ordinateur. Il permet d'influencer le chargement du système d'exploitation afin de modifier son fonctionnement, d'obtenir des privilèges d'administration ou de masquer d'autres *malwares*.


## Comment se protéger

### La vigilance

La règle d'or est la vigilance.

- Ne cliquez pas sur des liens en lesquels vous n'avez pas confiance.
- Ne téléchargez pas de fichiers sans être certain·e de leur provenance, en particulier les fichiers exécutables et les scripts.
- Si vous téléchargez un fichier exécutable, ne l'exécutez pas.
- N'insérez pas d'appareils comme une clé USB, qui pourraient contenir du code malveillant.


### Les antivirus

Un antivirus est un logiciel conçu pour détecter, prévenir et supprimer les logiciels malveillants (*malware*) sur un ordinateur ou un réseau.


#### Comment ils détectent les malwares ?

Un logiciel antivirus utilise plusieurs procédés pour détecter les logiciels malveillants.

Il compare les fichiers et les programmes sur l'ordinateur avec une base de données de **signatures** de malwares connus. Chaque antivirus possède sa manière de calculer la signature, en analysant des fragments de code, des motifs récurrents ou des séquences uniques. L'antivirus peut analyser l'intégralité du système (scan complet) ou un fichier spécifique à la demande. On peut configurer l'antivirus pour effectuer des scans périodiques à intervalles réguliers. La majorité des antivirus effectuent aussi des analyses **en temps réel** dès qu'un fichier arrive sur l'ordinateur.

L'antivirus tente aussi d'identifier les menaces en utilisant des méthodes d'**analyse heuristique**. Plutôt que de rechercher des similitudes dans le contenu des fichiers, il analyse le comportement des programmes pour détecter des activités suspectes ou malveillantes. Ce type d'analyse permet d'intercepter des logiciels malveillants même si ceux-ci sont totalement inédits et ne sont pas encore répertoriés dans la base de données de signatures.

Si l'antivirus identifie un fichier comme un *malware*, celui-ci peut agir immédiatement, en supprimant le fichier ou en le plaçant en **quarantaine** (le fichier est toujours là, mais le système d'exploitation interdit son exécution).


#### Comment les maintenir ?

Les logiciels malveillants évoluent rapidement, donc les antivirus doivent suivre la cadence.

Il ne suffit pas d'installer ou d'activer un antivirus : il faut le mettre à jour régulièrement. Les mises à jour apportent de nouvelles signatures et de nouvelles méthodes de détection heuristique.


#### Lequel choisir ?

Il y a un grand nombre de logiciels antivirus sur le marché.

Pour comparer adéquatement les antivirus, on peut rechercher les caractéristiques suivantes :
- Un haut taux de détection de vrais malwares
- Un faible taux de faux positifs (un fichier légitime qui est considéré à tort comme un virus)
- Une faible empreinte sur les ressources du système (qu'il ne ralentisse pas les ordinateurs)
- La mise à jour régulière et fréquente des signatures
- Un niveau de confiance élevé (par exemple, l'antivirus Kaspersky est-il digne de confiance ?)
- Des capacités d'administration centralisée (pour les administrateurs d'entreprise)

Sous Windows 10 et 11 ainsi que pour les éditions serveur, l'antivirus **Windows Defender** est intégré au système d'exploitation. Il offre une protection de base contre les malwares. Contrairement à la croyance populaire, il est assez performant et fiable et offre une protection en temps réel. Il utilise une protection basée sur le cloud pour analyser les menaces en temps réel. Il fait partie de la solution Windows Defender qui incorpore aussi des protections contre le *phishing* et un service de pare-feu.

Cependant, certains antivirus commerciaux offrent typiquement des taux de détection légèrement supérieurs ainsi que des fonctionnalités supplémentaires. Par exemple, la suite **Norton 360** offre une solution VPN intégrée, un gestionnaire de mots de passe et une surveillance du dark web. La suite **McAfee** offre des outils de nettoyage et une protection Web avancée. Il s'agit d'arguments marketing, bien sûr.

Des antivirus gratuits existent aussi, mais leur adoption a diminué depuis que Windows intègre le sien. Parmi les principaux on trouve **Avast**, **AVG** et **Avira**, qui offrent tout de même des niveaux de protection et des performances comparables aux solutions commerciales.

Le site [av-test.org](https://www.av-test.org/fr/antivirus/) propose une analyse détaillée des principaux antivirus sur le marché.

Lien : https://www.av-test.org/fr/antivirus/


:::caution Les faux antivirus
Il faut se méfier des faux antivirus qui nous sont proposés dans des annonces publicitaires. On voit parfois des messages indiquant qu'un virus a été détecté et proposant un lien vers un soi-disant antivirus capable de vous protéger. Ne vous laissez pas berner !
:::


#### Mythe ou réalité ?

On nous a toujours dit que les antivirus ne sont utiles que sous Windows, car il n'existerait pas de logiciels malveillants sur macOS, Linux ou Android. Est-ce vrai ?


### Et si mon antivirus ne détecte rien ?

Même le meilleur antivirus peut passer à côté d'un logiciel malveillant. Il arrive qu'on doive se débarrasser d'un *malware* par nous‑même. Sous Windows, certains outils peuvent nous aider à les identifier et à les éliminer.


#### Le gestionnaire de tâches

Le gestionnaire de tâches permet de voir tous les processus en cours d'exécution sur le système.

![Gestionnaire de tâches](taskmgr.png)

On y accède de plusieurs manières :
- La séquence de touches `Ctrl` + `Maj` + `Esc`
- Clic droit sur la barre des tâches, puis cliquer sur Gestionnaire des tâches
- Clic droit sur le menu Démarrer, puis cliquer sur Gestionnaire des tâches
- Séquence de touches `Ctrl` + `Alt` + `Suppr.`, puis cliquer sur Gestionnaire des tâches
- La commande `taskmgr`

Il faut aller dans l'**onglet Détails** pour voir l'ensemble des processus.

Lorsque vous identifiez un fichier exécutable qui vous semble suspect ou anormal, vous pouvez effectuer un clic droit et :
- ouvrir les propriétés pour voir certains détails du fichier ainsi que son emplacement
- effectuer une recherche en ligne
- terminer l'exécution du processus
- ouvrir l'emplacement du fichier dans l'explorateur


#### Scan de virus à la demande

Si vous avez un fichier exécutable mais n'êtes pas certain·e de sa légitimité, vous pouvez scanner ce fichier à l'aide de votre antivirus. Souvent, cela peut se faire directement à partir du menu contextuel. Référez-vous à la documentation de votre antivirus.

![Scan à la demande](ondemandscan.png)


#### VirusTotal

Pour des résultats plus détaillés, vous pouvez téléverser votre fichier suspect sur [VirusTotal](https://www.virustotal.com/) pour qu'il soit testé par le maximum d'antivirus sur le marché. VirusTotal effectue une analyse à la demande auprès de plusieurs dizaines de produits et vous indique lesquels ont détecté une menace. Plus la proportion d'antivirus détectant une menace est élevée, plus grandes sont les chances que le fichier soit effectivement malicieux.

Lien : https://www.virustotal.com/

![VirusTotal.com](virustotal.png)

:::info Process Explorer
L'outil [Process Explorer](https://learn.microsoft.com/fr-ca/sysinternals/downloads/process-explorer) de Microsoft est une version avancée du gestionnaire de tâches avec plusieurs fonctionnalités supplémentaires. Entre autres, il permet d'analyser automatiquement les processus en cours d'exécution avec VirusTotal.

![Process Explorer et Virus Total](procexp.png)

Téléchargement : https://learn.microsoft.com/fr-ca/sysinternals/downloads/process-explorer
:::


#### Autoruns

Souvent, un logiciel malveillant est un programme qui s'exécute en arrière-plan du système. Sous Windows, il existe plusieurs manières d'exécuter un programme automatiquement au démarrage de l'ordinateur ou de la session utilisateur :

- Dans le planificateur de tâches
- Comme service
- Dans le répertoire Startup du menu Démarrer
- Dans la clé Run du registre
- Etc.

Les programmeurs de malware peuvent parfois être très créatifs dans leur manière de lancer leur programme automatiquement.

L'outil [Autoruns](https://learn.microsoft.com/fr-ca/sysinternals/downloads/autoruns) dresse la liste des programmes qui démarrent automatiquement. Si vous vous demandez pourquoi un programme est lancé automatiquement à chaque démarrage, vous risquez de le trouver là.

![Autoruns](autoruns.png)

Téléchargement : https://learn.microsoft.com/fr-ca/sysinternals/downloads/autoruns


## Exercice sur les *malwares*

Vous aurez besoin d'une machine virtuelle pour faire cet exercice.

- Téléchargez le fichier suivant : `\\ed5depinfo\Logiciels\_Cours\3U4\Win10_RickRollVM.7z`
- Extrayez la VM sous `C:\VM\VMware`
- Double-cliquez sur le fichier `.VMX`
- Dans VMware Workstation Pro, démarrez la machine virtuelle
- Connectez-vous avec le compte d'Alice :
  - Nom d'utilisateur : `alice`
  - Mot de passe : `Passw0rd`

Vous remarquerez sans doute que cette VM vous *rickroll* en continu. Il semble qu'un programme malveillant ait réussi à passer sans être détecté par l'antivirus.

Utilisez les outils présentés précédemment pour détecter et éliminer le logiciel malveillant. N'hésitez pas à demander l'aide du professeur si vous êtes bloqué.


## DÉFI VIRUS !!!

Si vous avez trouvé l'exercice précédent trop facile, le professeur a développé une version plus agressive de son « virus ». Saurez-vous l'éliminer ?

- Téléchargez le fichier suivant : `\\ed5depinfo\Logiciels\_Cours\3U4\Win10_RickBombVM.7z`
- Extrayez la VM sous `C:\VM\VMware`
- Double-cliquez sur le fichier `.VMX`
- Dans VMware Workstation Pro, démarrez la machine virtuelle
- Connectez-vous avec le compte d'Alice :
  - Nom d'utilisateur : `alice`
  - Mot de passe : `Passw0rd`

Vous pouvez demander l'aide du professeur si vous êtes bloqué.
