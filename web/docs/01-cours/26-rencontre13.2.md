# Rencontre 13.2 Injection SQL attaque

Une injection SQL c'est quoi?
- une situation où l'utilisateur peut exécuter du SQL de son choix sur la BD
- PAS LE PROGRAMMEUR mais l'utilisateur, nooooooooooonnnnnnnnnnn
- parce que le programmeur a pris ce que l'utilisateur lui a donné et l'a mélangé sans faire attention 
avec son code.

## Exemple
L'application vous demande un nom d'utilisateur et vous tapez 
```sql
'); DROP DATABASE test; -- 
```

Ces données sont ensuite envoyées au serveur dans le code de l'application.


