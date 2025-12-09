using System;

namespace PaperclipsConsole
{
    public static class StartMenu
    {
        public static MenuChoice ShowMenu(bool saveExists)
        {
            Console.Clear();
            Console.CursorVisible = false;

            // Header
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                                              ║");
            Console.WriteLine("║                    UNIVERSAL PAPERCLIPS - Console Edition                    ║");
            Console.WriteLine("║                                                                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine();

            int selectedOption = 0;
            bool choosing = true;

            while (choosing)
            {
                // Menu options
                Console.SetCursorPosition(0, 7);
                
                if (saveExists)
                {
                    // Option 1: Continuer
                    if (selectedOption == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("                          ► CONTINUER LA PARTIE ◄                              ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("                            Continuer la partie                                ");
                    }

                    Console.WriteLine();

                    // Option 2: Nouvelle partie
                    if (selectedOption == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("                          ► NOUVELLE PARTIE ◄                                  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("                            Nouvelle partie                                    ");
                    }

                    Console.WriteLine();

                    // Option 3: Quitter
                    if (selectedOption == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("                          ► QUITTER ◄                                          ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("                            Quitter                                            ");
                    }
                }
                else
                {
                    // Pas de sauvegarde - seulement nouvelle partie ou quitter
                    // Option 1: Nouvelle partie
                    if (selectedOption == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("                          ► NOUVELLE PARTIE ◄                                  ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("                            Nouvelle partie                                    ");
                    }

                    Console.WriteLine();

                    // Option 2: Quitter
                    if (selectedOption == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine("                          ► QUITTER ◄                                          ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("                            Quitter                                            ");
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("                    Utilisez ↑↓ pour naviguer, ENTRÉE pour sélectionner       ");

                // Get input
                var key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOption--;
                        if (selectedOption < 0)
                            selectedOption = saveExists ? 2 : 1;
                        break;

                    case ConsoleKey.DownArrow:
                        selectedOption++;
                        if (selectedOption > (saveExists ? 2 : 1))
                            selectedOption = 0;
                        break;

                    case ConsoleKey.Enter:
                        choosing = false;
                        break;

                    case ConsoleKey.Escape:
                        return MenuChoice.Quit;
                }
            }

            // Process choice
            if (saveExists)
            {
                switch (selectedOption)
                {
                    case 0:
                        return MenuChoice.Continue;
                    case 1:
                        return ConfirmNewGame() ? MenuChoice.NewGame : ShowMenu(saveExists);
                    case 2:
                        return MenuChoice.Quit;
                }
            }
            else
            {
                switch (selectedOption)
                {
                    case 0:
                        return MenuChoice.NewGame;
                    case 1:
                        return MenuChoice.Quit;
                }
            }

            return MenuChoice.Quit;
        }

        private static bool ConfirmNewGame()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                              CONFIRMATION                                    ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                    ⚠️  ATTENTION ⚠️                                            ");
            Console.WriteLine();
            Console.WriteLine("          Commencer une nouvelle partie écrasera votre sauvegarde actuelle!    ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                    Êtes-vous sûr de vouloir continuer?                        ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("                    [O] Oui, nouvelle partie                                   ");
            Console.WriteLine("                    [N] Non, retour au menu                                    ");
            Console.WriteLine();

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.O || key.KeyChar == 'o')
                    return true;
                if (key.Key == ConsoleKey.N || key.KeyChar == 'n' || key.Key == ConsoleKey.Escape)
                    return false;
            }
        }
    }

    public enum MenuChoice
    {
        Continue,
        NewGame,
        Quit
    }
}
