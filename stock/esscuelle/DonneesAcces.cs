using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;

//https://sqlitebrowser.org/   https://download.sqlitebrowser.org/DB.Browser.for.SQLite-3.12.2-win64.zip

// https://sqlitebrowser.org/ pour regarder dans le dedans de la BD
// https://www.codeguru.com/dotnet/using-sqlite-in-a-c-application/
namespace consoleApp
{
    class DonneesAcces
    {
        static SqliteConnection sqlite_conn = InitialiserConnexion();

        private static SqliteConnection InitialiserConnexion()
        {
            SqliteConnection sqlite_conn;
            var path = Path.Combine(".", "data");
            System.IO.Directory.CreateDirectory(path);
            var databasePath = Path.Combine(path, "dabase.db");
            sqlite_conn = new SqliteConnection("Data Source="+databasePath+";");
            sqlite_conn.Open();
            return sqlite_conn;
        }

        public static void BDCreerTables()
        {
            sqlite_conn.Open();
            SqliteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE IF NOT EXISTS MUtilisateur" +
                               " (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                               " nom VARCHAR(200)," +
                               " motDePasse VARCHAR(200))";

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            // create a second table MAnneeRevenu with the user name, the year and the income
            string Createsql1 = "CREATE TABLE IF NOT EXISTS MNote" +
                                " (id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                " note VARCHAR(200)," +
                                " utilisateurID INT," +
                                " FOREIGN KEY(utilisateurID) REFERENCES MUtilisateur(id))";
            var sqlite_cmd2 = sqlite_conn.CreateCommand();
            sqlite_cmd2.CommandText = Createsql1;
            try
            {
                sqlite_cmd.ExecuteNonQuery();
                sqlite_cmd2.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Data);
            }
            finally
            {
                sqlite_conn.Close();
            }
        }

        public static long BDCreerUtilisateur(Formulaires.FormulaireNouveauCompte compte)
        {
            sqlite_conn.Open();
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
           sqlite_cmd.CommandText = "INSERT INTO MUtilisateur (nom, motDePasse) " +
                         "VALUES('" + compte.Nom + "', '" + compte.MotDePasse + "');";
            sqlite_cmd.ExecuteNonQuery();

            // Get the last inserted ID
            sqlite_cmd.CommandText = "SELECT last_insert_rowid()";
            long lastId = (long)sqlite_cmd.ExecuteScalar();

            sqlite_conn.Close();
            return lastId;
        }
            
        public static List<DonneesUtilisateur> BDLireUtilisateurs()
        {
            sqlite_conn.Open();
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            List<DonneesUtilisateur> liste = new List<DonneesUtilisateur>();
            sqlite_cmd.CommandText = "SELECT * FROM MUtilisateur";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                //string myreader = sqlite_datareader.GetString(0);
                DonneesUtilisateur compte = new DonneesUtilisateur();
                compte.ID = sqlite_datareader.GetInt32(0);
                compte.Nom = sqlite_datareader.GetString(1);
                compte.MotDePasse = sqlite_datareader.GetString(2);
                liste.Add(compte);
            }

            sqlite_conn.Close();
            return liste;
        }

        public static List<DonneesNote> BdNotesPour(int utilisateurID)
        {
            sqlite_conn.Open();
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            List<DonneesNote> liste = new List<DonneesNote>();
            // execute the request and extract the data
            sqlite_cmd.CommandText = "SELECT * FROM MNote WHERE utilisateurID = '" + utilisateurID + "'";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                DonneesNote note = new DonneesNote();
                note.ID = sqlite_datareader.GetInt32(0);
                note.Contenu = sqlite_datareader.GetString(1);
                note.UserID = sqlite_datareader.GetInt32(2);
                liste.Add(note);
            }
            return liste;
        }

        public static DonneesUtilisateur BDUtilisateurParSonNomEtMDP(string nom, string mdp)
        {
            sqlite_conn.Open();
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM MUtilisateur WHERE nom = '" + nom + "' AND motDePasse = '" + mdp + "'";
            Console.WriteLine(sqlite_cmd.CommandText);
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            // if there is no such user, return null
            if (!sqlite_datareader.HasRows)
            {
                sqlite_conn.Close();
                return null;
            }
            DonneesUtilisateur compte = new DonneesUtilisateur();
            // get the user from the database
            while (sqlite_datareader.Read())
            {
                compte.ID = sqlite_datareader.GetInt32(0);
                compte.Nom = sqlite_datareader.GetString(1);
                compte.MotDePasse = sqlite_datareader.GetString(2);
            }

            sqlite_conn.Close();
            return compte;
        }
            
        public static DonneesUtilisateur BDUtilisateurParSonNom(string nom)
        {
            sqlite_conn.Open();
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM MUtilisateur WHERE nom = '" + nom + "' ";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            // if there is no such user, return null
            if (!sqlite_datareader.HasRows)
            {
                sqlite_conn.Close();
                return null;
            }
            DonneesUtilisateur compte = new DonneesUtilisateur();
            // get the user from the database
            while (sqlite_datareader.Read())
            {
                compte.ID = sqlite_datareader.GetInt32(0);
                compte.Nom = sqlite_datareader.GetString(1);
                compte.MotDePasse = sqlite_datareader.GetString(2);
            }
            sqlite_conn.Close();
            return compte;
        }

        public static void BDEffacerTout()
        {
            // delete all the users
            sqlite_conn.Open();
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM MUtilisateur";
            sqlite_cmd.ExecuteNonQuery();
            // make a second command to erase the other table
            sqlite_cmd.CommandText = "DELETE FROM MNote";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();

        }

        public static void BDCreerNote(int utilisateurID, string note)
        {
            sqlite_conn.Open();
            SqliteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO MNote (id, note, utilisateurID) VALUES(null, @note, @uid)";
            sqlite_cmd.Parameters.AddWithValue("@note", note);
            sqlite_cmd.Parameters.AddWithValue("@uid", utilisateurID);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        public static DonneesUtilisateur BDUtilisateurParSonID(int utilisateurConnecteId)
        {
            // get the user from the database
            sqlite_conn.Open();
            SqliteDataReader sqlite_datareader;
            SqliteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM MUtilisateur WHERE id = '" + utilisateurConnecteId + "'";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            // if there is no such user, return null
            if (!sqlite_datareader.HasRows)
            {
                sqlite_conn.Close();
                return null;
            }
            DonneesUtilisateur compte = new DonneesUtilisateur();
            while (sqlite_datareader.Read())
            {
                compte.ID = sqlite_datareader.GetInt32(0);
                compte.Nom = sqlite_datareader.GetString(1);
                compte.MotDePasse = sqlite_datareader.GetString(2);
            }
            sqlite_conn.Close();
            return compte;
        }
    }
}