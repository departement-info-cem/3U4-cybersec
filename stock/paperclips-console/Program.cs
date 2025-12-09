using System;
using System.Threading;

namespace PaperclipsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuration pour réduire le scintillement
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Universal Paperclips - Console Edition";
            Console.CursorVisible = false;
            
            // Taille de la console fixe pour éviter le redimensionnement
            try
            {
                Console.SetWindowSize(Math.Min(Console.LargestWindowWidth, 85), Math.Min(Console.LargestWindowHeight, 30));
                Console.SetBufferSize(85, 30);
            }
            catch
            {
                // Ignorer si la configuration échoue (certains terminaux)
            }

            var game = new GameManager();
            
            // Show start menu
            var choice = StartMenu.ShowMenu(game.SaveFileExists());
            
            if (choice == MenuChoice.Quit)
            {
                Console.Clear();
                Console.WriteLine("\n  À bientôt!\n");
                Thread.Sleep(1000);
                return;
            }

            // Load or start new game based on choice
            if (choice == MenuChoice.Continue)
            {
                game.LoadGame();
            }
            else // NewGame
            {
                game.StartNewGame();
            }

            // Show brief intro
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           UNIVERSAL PAPERCLIPS - Console Edition           ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  Objectif: Produire des paperclips et dominer le marché");
            Console.WriteLine();
            Console.WriteLine("  COMMANDES RAPIDES:");
            Console.WriteLine("    P - Créer un paperclip");
            Console.WriteLine("    W - Acheter du wire");
            Console.WriteLine("    + - Augmenter le prix");
            Console.WriteLine("    - - Diminuer le prix");
            Console.WriteLine();
            Console.WriteLine("  Appuyez sur CTRL+M pour le menu complet");
            Console.WriteLine("  Appuyez sur une touche pour commencer...");
            Console.ReadKey(true);

            // Clear screen before starting
            Console.Clear();

            Thread gameLoop = new Thread(() =>
            {
                while (game.IsRunning)
                {
                    game.Update();
                    Thread.Sleep(100);
                }
            });
            gameLoop.Start();

            while (game.IsRunning)
            {
                game.DisplayStatus();
                
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    
                    game.CheckQuit(key);
                    if (!game.IsRunning) break;
                    
                    game.CheckMenu(key);
                    game.ProcessInput(key);
                }
                
                Thread.Sleep(50);
            }

            gameLoop.Join();
            
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("\n  Merci d'avoir joué à Universal Paperclips!");
            Console.WriteLine("  Vos données ont été sauvegardées.\n");
            Thread.Sleep(2000);
        }
    }
}
