---
id: r18
title: Rencontre 18 - Inventaire
sidebar_label: R18 - Inventaire
draft: false
hide_table_of_contents: false
---


Pour sécuriser un réseau d'ordinateurs, il est important de bien faire l'inventaire de ce qui s'y trouve. On doit bien connaître autant les appareils connectés à notre réseau ainsi que les logiciels installés dans ces appareils.

## Inventaire matériel

Il importe de connaître les appareils qui sont connectés à notre réseau local car:
- ils pourraient s'y trouver à notre insu et constituer une menace
- ils pourraient être vulnérables et permettre à un attaquant d'atteindre notre réseau privé


### Identifier les appareils connectés au réseau

Dans un réseau domestique, plusieurs appareils se connectent au réseau local, souvent par WiFi, afin d'accéder à Internet.
- ordinateur, laptop
- passerelle NAT
- téléphone, tablette
- console de jeu vidéo
- électroménagers
- éclairage
- thermostats
- climatiseurs et thermopompes
- contrôleurs de pompe de piscine
- etc.

Tous ces appareils sont susceptiples de se connecter sur Internet et sont donc potentiellement vulnérables.


### Outils d'identification

Pour connaître la liste des ordinateurs qui sont connectés à votre environnement réseau, vous pouvez avoir recours à certains outils.

La manière la plus simple est d'accéder à l'interface de gestion de votre point d'accès WiFi. Souvent, il s'agit d'ouvrir une page Web à l'adresse de votre passerelle par défaut (par exemple, `https:\\192.168.0.1\`) ou encore en utilisant une application sur un téléphone ou une tablette. Cet appareil contrôle généralement qui est connecté sur le réseau et possèdent un serveur DHCP qui assigne les adresses IP des clients.

Si vous n'êtes pas en mesure d'accéder à cette interface, vous pouvez toujours utiliser un outil pour scanner votre réseau, comme NMAP. Bien sûr, ce n'est pas parfait

:::caution
Les outils de scannage de ports comme NMAP devraient seulement être utilisés dans un réseau qui vous appartient. Effectuer un scan de ports ou de vulnérabilités dans un réseau sans autorisation est illégal. Certains systèmes de détection d'intrusion sont capables de détecter ces outils.
:::

:::info À faire à la maison
Essayez de faire l'inventaire de votre réseau domestique. **N'utilisez pas d'outils de scan sur un réseau qui ne vous appartient pas!!**
:::


## Inventaire logiciel

Sous Windows, lorsqu'on "installe" un logiciel, qu'est-ce qui se passe? Vous avez sans doute vu en *Systèmes d'exploitation* que lorsqu'on installe une application sous Windows (avec un fichier MSI ou un installateur de type *setup.exe*), celle-ci est visible dans le panneau de configuration des applis.

Il y a en fait trois types d'applications sous Windows:
- Les applications classiques (MSI, setup.exe, etc.)
- Les applications universelles (MSIX, APPX, StoreApp)
- Les applications portables (aucune installation requise)


### Les applications classiques

Si vous installez une application en utilisant un installateur de type *.MSI* ou *setup.exe*, il s'agit probablement d'une application classique.

Typiquement, vous exécutez l'installateur en tant qu'administrateur du système et vous répondez à des questions afin de dicter comment l'application s'installera, avec quelles composantes, etc. Puis, l'installateur extrait les fichiers contenus dans l'installateur, généralement dans le répertoire `C:\Program Files\` (ou `C:\Program Files(x86)\` dans le cas d'une application compilée pour l'architecture x86 à 32 bits). Puis, l'installateur crée un raccourci dans le menu Démarrer et/ou sur le bureau, qui pointe vers le fichier .EXE principal de l'application.

Certaines applications classiques, plus rares, sont capables de s'installer même lorsqu'on n'est pas administrateur de la machine. Comme les utilisateurs non-admin du système ne possèdent pas de privilège d'écriture dans `C:\Program Files`, l'installateur vise plutôt le profil de l'utilisateur (`C:\Users\Nomdutilisateur\AppData\Local\`).

Windows connaît ses applications installées car l'installateur enregistre cette dernière dans une liste qu'on appelle ARP (pour *Add or Remove Programs*). C'est un ensemble de clés dans le registre de Windows qui contiennent la liste des applications installées. Lorsqu'on ouvre l'outil `appwiz.cpl`, on peut obtenir cette liste.

Alternativement, on peut afficher plus de détails des applications installées à l'aide d'un outil spécialisé, comme l'outil [UninstallView de NirSoft](https://www.nirsoft.net/utils/uninstall_view.html).

Les informations concernant les applications installées se trouvent dans la base de registre, dans l'une des clés suivantes (accessible par l'éditeur de registre, `regedit.exe`):
```
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall
HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall
HKEY_CURRENT_USER\Software\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall
```
![ARP Registry](r17-01-arpregisry.png)

:::caution
Windows reconnait les applications installées uniquement grâce au registre ARP. Pour "masquer" une application de l'inventaire tout en la gardant installée, il suffit d'effacer son entrée au registre. Autrement dit, ce n'est pas parce qu'une application n'apparaît pas dans la liste des applis installées que cela signifie qu'elle n'est plus présente sur le système.
:::


### Les applications universelles (UWP)

Les applications universelles (en abrégé UWP pour Universal Windows Platform) sont un autre type d'application disponible sous Windows. Elles doivent généralement être enregistrées pour être utilisées. On les installe en lançant un paquet de type *.msix*, *.appx*, *.msixbundle* ou *.appxbundle*, mais la plupart du temps, elles sont installées à travers le *Microsoft Store*. C'est pourquoi on les appelle souvent des *store apps*.

On peut obtenir la liste des applications universelles installées à travers les paramètres de Windows, dans la section Applis, mais cette liste mélange les applications UWP et classiques. Pour obtenir la liste des applications UWP seulement, on peut lancer la commande suivante à partir d'une invite PowerShell:

```powershell
Get-AppxPackage | select Name, Version, InstallLocation
```

Ou encore, l'outil [InstallAppView de NirSoft](https://www.nirsoft.net/utils/installed_app_view.html) montre la liste des applications universelles installées dans une interface graphique.


### Les applications portables

On désigne sous le nom d'application portable tout programme (des fichiers *.exe* notamment) qui peut être utilisé sans avoir besoin d'installation. Ces applications sont très difficiles à inventorier car ce ne sont que des fichiers aucunement enregistrés dans le système. On doit alors faire une recherche de tous les fichiers EXE.


:::info Gestionnaires de paquets sous Windows
Bien que ce soit moins répandu, il existe aussi des gestionnaires de paquets sous Windows, à l'instar de *apt*, *yum* et *snap* sous Linux. Les plus populaires sont:
- [WinGet](https://learn.microsoft.com/en-us/windows/package-manager/winget/) (inclus dans Windows mais peu connu)
- [Chocolatey](https://chocolatey.org/)
- [Scoop](https://scoop.sh/)
:::


### Exercice sur l'inventaire matériel

Tout d'abord, téléchargez et installez l'application WinMerge (https://winmerge.org/downloads/) puis installez-la.

Lancez la commande `appwiz.cpl` et constatez que WinMerge s'y trouve.

Ensuite, téléchargez le programme portable [UninstallView de NirSoft](https://www.nirsoft.net/utils/uninstall_view.html). Choisissez la version **64-bit**. Il s'agit d'un fichier ZIP, donc extrayez-le quelque part sur votre disque dur et lancez l'exécutable.

Vous devriez voir l'application WinMerge dans la liste, ainsi que plus de détails sur l'enregistrement de l'application sur le système.

Maintenant, **sans désinstaller l'application**, faites en sorte qu'elle n'apparaisse plus dans l'inventaire. L'application doit toujours être fonctionnelle mais ne doit plus être détectée par UninstallView.


## Les inventaires en entreprise

Les organisations (comme la DISTI) utilisent des outils plus avancés de gestion de leur inventaire. Elles peuvent se le permettre car elles ont le contrôle de son réseau. En contrôlant la configuration des ordinateurs qui font partie du réseau, elles peuvent installer des outils capables de lire des informations d'inventaire en utilisant des protocoles d'accès distant comme SSH, RPC, WinRM, etc. Les admins peuvent aussi déployer des agents résidents sur les postes de travail et les serveurs pour collecter des informations de configuration, comme le nom de l'utilisateur, la quantité d'espace disponible, les applications installées, et bien d'autres informations. 

Les données d'inventaire sont très utiles pour avoir une bonne compréhension de ce qui se trouve dans notre réseau. Les admins s'en servent notamment pour:
- S'assurer que les applications et les systèmes d'exploitation sont à jour
- S'assurer que les logiciels installés sont autorisés
- Se conformer aux conditions d'utilisation des logiciels (par exemple, payer les licences logicielles)
- Éviter que des appareils qui présentent des vulnérabilités mettent en péril la sécurité et la stabilité du réseau
- Prévoir divers problèmes opérationnels ou de sécurité avant qu'il ne soit trop tard

Ces outils et techniques dépassent le cadre de ce cours; ils sont vus plus en détails dans les cours spécifiques au profil Réseautique.



## Récapituilation: les bonnes pratiques

Vous êtes rendus assez connaissant pour qu'on puisse expliquer pourquoi et évaluer le risque pris quand on ne les
applique pas.

Pour chaque bonne pratique, nous allons avoir une conversation pour voir. L'exercice principal du jour
est de prendre des notes sur le risques et le pourquoi. C'est un peu un moyen de revisiter ce qu'on a vu
jusqu'à maintenant.

Ensuite vous pouvez avoir 2 profils:
1. Je vais appliquer les bonnes pratiques mais je n'ai pas besoin de tout comprendre: c'est le minimum pour être un technicien compétent
2. Je veux comprendre les bonnes pratiques en profondeur, ce profil vous permet de potentiellement tracer une carrière cybersec


:::info Déroulement de la séance
Dans cette séance de cours, on va discuter de chacun des points ci-dessous. Après la discussion, il sera de votre responsabilité de prendre des notes.

On verra :
- si vous voyez des exploits évidents, s'ils sont graves
- si vous appliquez déjà cette pratique ou pas toujours, si non pourquoi?
- si on a des exemples passés d'attaques évitables
:::

### Installer les mises à jour logicielles

Il faut installer régulièrement les mises à jour du système d'exploitation et des applications.

**Pourquoi? (risque encouru)**


### Choisir un mot de passe complexe et le protéger

Ici il y a 2 éléments : on souhaite qu'un mot de passe soit complexe pour qu'il soit difficile à deviner.
Ensuite, il faut s'assurer qu'on ne permet pas d'attaque facile pour l'obtenir.

**Pourquoi? (risque encouru)**


### Activer la double authentification

Il faut activer la double authentification lorsque possible.

**Pourquoi? (risque encouru)**


### Limiter les permissions / droits le plus possible

Essentiellement, l'idée est qu'un système doit avoir le moins de services possibles, le moins d'accès possibles
le moins d'utilisateurs possibles afin de permettre l'utilisation prévue.

C'est appliqué souvent et beaucoup dans les grandes entreprises. Par exemple, de nombreux stagiaires attendent
plusieurs jours avant d'avoir les accès pour travailler.

**Pourquoi? (risque encouru)**



### Utiliser un VPN pour l'accès à distance

Plusieurs entreprises exigent que les employés en télétravail installent un logiciel VPN pour accéder à leurs systèmes 
à distance, plutôt que d'exposer leurs serveurs sur Internet.

**Pourquoi? (risque encouru)**


### Ne pas ouvrir un fichier / insérer une clé USB

Si on trouve un fichier ou une clé USB dont on ne connait pas la nature ou la provenance, il est généralement 
déconseillé de l'ouvrir ou l'insérer sur un ordinateur, à moins que celui-ci soit totalement isolé du réseau.

**Pourquoi? (risque encouru)**


### Ne pas cliquer sur un lien / pièce jointe 

Si on reçoit par courriel ou SMS un hyperlien ou un fichier en pièce jointe, il n'est pas conseillé de l'ouvrir ou de
le télécharger.

**Pourquoi? (risque encouru)**
