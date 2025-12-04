using System;
using System.Collections.Generic;
using Sharprompt;

// Phase 1 on distribue uniquement l'exécutable
// Phase 2 on donne accès au code source pour fixer


// mettre les mots de passe visible dans la console dans la version distribuée
// ajouter un mode admin protégé par un mot de passe unique
// avec la liste utilisateurs

// Commande pout generer le .exe stantalone
// dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true

// https://github.com/shibayan/Sharprompt for the win
namespace consoleApp;

class Program
{
    private static int utilisateurConnecteID = -1; 
   
    public static void Main(string[] args) {
        try
        {
            Console.WriteLine("Bienvenue dans l'application la plus sure du monde!");
            Console.WriteLine("Nous on code en SQL comme des boss");
            DonneesAcces.BDCreerTables();
            bool quit = false;
            do
            {
                quit = MainMenu();
            } while (!quit);
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e);
        }
    }

    public static bool MainMenu() {
        string prompt = utilisateurConnecteID < 0 ? "Non connecté" : DonneesAcces.BDUtilisateurParSonID(utilisateurConnecteID).Nom;
        var choix = Prompt.Select("Quelle action veux-tu effectuer ? " + prompt, 
            new[] { 
                "(De)Connexion",
                "Créer un compte",
                "Nouvelle note", "Mes notes",
                "Lister utilisateurs",
                "Effacer la console", "Effacer la BD", "Quitter" });
        switch (choix)
        {
            case "Quitter": return true;
            case "Effacer la console": Console.Clear(); break;
            case "Créer un compte": CreerCompte(); break;
            case "Effacer la BD": EffacerBD(); break;
            case "Nouvelle note": NouvelleNote(); break;
            case "Mes notes": MesNotes(); break;
            case "(De)Connexion":
                if (utilisateurConnecteID < 0)
                { Connexion(); }
                else { Deconnexion(); }
                break;
            case "Lister utilisateurs": ListerUtilisateurs(); break;
        }
        return false;
    }

    private static void MonProfil()
    {
        Console.Clear();
        if (utilisateurConnecteID < 0)
        {
            Console.WriteLine("Tu n'es pas connecté, tu ne peux pas voir ton profil");
            return;
        }
        Console.WriteLine("Nom: " + utilisateurConnecteID + 
                          " \n  Mot de passe  : " + DonneesAcces.BDUtilisateurParSonID(utilisateurConnecteID));
        MesNotes();
    }

    private static void ListerUtilisateurs()
    {
        Console.Clear();
        List<DonneesUtilisateur> utilisateurs = DonneesAcces.BDLireUtilisateurs();
        foreach (var utilisateur in utilisateurs)
        { Console.WriteLine(utilisateur.Nom + " " + utilisateur.ID ); }
    }
    private static void MesNotes()
    {
        if (utilisateurConnecteID < 0)
        {
            Console.WriteLine("Tu n'es pas connecté, tu ne peux pas voir tes revenus");
            return;
        } 
        Console.Clear();
        List<DonneesNote> liste = DonneesAcces.BdNotesPour(utilisateurConnecteID);
        if (liste.Count == 0)
        {
            Console.WriteLine("Tu n'as pas encore entré de notes");
            return;
        }
        foreach (var item in liste)
        {
            Console.WriteLine("===================================== Note");
            Console.WriteLine(item.Contenu );
            Console.WriteLine();
        }
    }

    private static void NouvelleNote()
    {
        if (utilisateurConnecteID < 0)
        {
            Console.Error.WriteLine("il faut être connecté pour entrer des revenus");
            return;
        }
        string note = Prompt.Input<string>("Merci d'entrer le texte de la note:");
        DonneesAcces.BDCreerNote(utilisateurConnecteID, note);
        Console.WriteLine("La note  a été enregistrée  ");
    }

    private static void EffacerBD()
    {
        DonneesAcces.BDEffacerTout();
        utilisateurConnecteID  = -1;
    }

    public static void CreerCompte()
    {
        Formulaires.FormulaireNouveauCompte result = Prompt.Bind<Formulaires.FormulaireNouveauCompte>();
        var id = DonneesAcces.BDCreerUtilisateur(result);
        utilisateurConnecteID = (int) id;
    }
    
    public static void Connexion()
    {
        Console.WriteLine("Connexion");
        Formulaires.FormulaireConnexion formulaire = Prompt.Bind<Formulaires.FormulaireConnexion>();
        Console.WriteLine(formulaire.MotDePasse);
        // TODO remplacer par une requete SQL directe
        DonneesUtilisateur utilisateur = DonneesAcces.BDUtilisateurParSonNomEtMDP(formulaire.Nom, formulaire.MotDePasse);
        if (utilisateur != null)
        {
            Console.WriteLine("Connexion réussie, bienvenue " + formulaire.Nom);
            utilisateurConnecteID = utilisateur.ID;
            Console.WriteLine("Connexion réussie, bienvenue " + utilisateurConnecteID);
        }
        else
        {
            Console.Error.WriteLine("Connexion échouée, mauvais nom et/ou mot de passe");
        }
    }
    
    public static void Deconnexion()
    {
        Console.WriteLine("Deconnexion");
        utilisateurConnecteID = -1;
    }
}