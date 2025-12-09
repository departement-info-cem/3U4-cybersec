using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Globalization;

namespace PaperclipsConsole
{
    public class GameManager
    {
        private GameState state;
        private readonly string saveDirectory;
        private readonly string savePath;
        private DateTime lastUpdate;
        private DateTime lastAutoSave;
        private DateTime lastDisplay;
        private Random random;
        private bool needsRedraw = true;
        private static readonly CultureInfo invCulture = CultureInfo.InvariantCulture;

        public GameManager()
        {
            saveDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "papaclip"
            );
            savePath = Path.Combine(saveDirectory, "data.json");
            random = new Random();
            // Ne pas charger automatiquement
            lastUpdate = DateTime.Now;
            lastAutoSave = DateTime.Now;
            lastDisplay = DateTime.Now;
        }

        public bool SaveFileExists()
        {
            return File.Exists(savePath);
        }

        public void StartNewGame()
        {
            state = CreateNewGameState();
            SaveGame();
        }

        public void LoadGame()
        {
            if (File.Exists(savePath))
            {
                try
                {
                    string json = File.ReadAllText(savePath);
                    state = JsonSerializer.Deserialize<GameState>(json) ?? CreateNewGameState();
                    Console.WriteLine("Partie chargée avec succès!");
                }
                catch
                {
                    Console.WriteLine("Erreur lors du chargement. Nouvelle partie créée.");
                    state = CreateNewGameState();
                    SaveGame();
                }
            }
            else
            {
                state = CreateNewGameState();
                Console.WriteLine("Nouvelle partie créée avec $20 de départ!");
                SaveGame();
            }
        }

        private GameState CreateNewGameState()
        {
            return new GameState
            {
                Funds = 20.00,
                Wire = 1000,
                Margin = 0.25,
                Clips = 0,
                UnusedClips = 0,
                Demand = 10,
                UnsoldClips = 0,
                ClipmakerLevel = 0,
                ClipperCost = 5.00,
                MarketingLevel = 1,
                AdCost = 100.00,
                WireCost = 20,
                WireAmount = 1000,
                Processors = 1,
                Memory = 1,
                Operations = 0,
                Trust = 2,
                NextTrust = 1000,
                Creativity = 0,
                TotalClipsProduced = 0,
                MegaClipperLevel = 0,
                MegaClipperCost = 500
            };
        }

        // Helper methods for consistent number formatting
        private string FormatInt(long number)
        {
            return number.ToString("N0", invCulture);
        }

        private string FormatMoney(double amount)
        {
            return amount.ToString("F2", invCulture);
        }

        private string FormatDecimal(double amount, int decimals = 2)
        {
            return amount.ToString($"F{decimals}", invCulture);
        }

        public void SaveGame()
        {
            try
            {
                Directory.CreateDirectory(saveDirectory);
                var options = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(state, options);
                File.WriteAllText(savePath, json);
                Console.WriteLine("\n[Partie sauvegardée]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErreur lors de la sauvegarde: {ex.Message}");
            }
        }

        public void MakePaperclip()
        {
            if (state.Wire >= 1)
            {
                state.Wire--;
                state.Clips++;
                state.TotalClipsProduced++;
                state.UnsoldClips++;
                UpdateTrust();
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de wire!");
            }
        }

        public void BuyWire()
        {
            if (state.Funds >= state.WireCost)
            {
                state.Funds -= state.WireCost;
                state.Wire += state.WireAmount;
                state.WireCost = Math.Round(state.WireCost * 1.05, 2);
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de fonds!");
            }
        }

        public void BuyAutoClipper()
        {
            if (state.Funds >= state.ClipperCost)
            {
                state.Funds -= state.ClipperCost;
                state.ClipmakerLevel++;
                state.ClipperCost = Math.Round(Math.Pow(1.1, state.ClipmakerLevel) + 5, 2);
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de fonds!");
            }
        }

        public void BuyMegaClipper()
        {
            if (state.Funds >= state.MegaClipperCost)
            {
                state.Funds -= state.MegaClipperCost;
                state.MegaClipperLevel++;
                state.MegaClipperCost = Math.Round(Math.Pow(1.07, state.MegaClipperLevel) * 1000, 2);
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de fonds!");
            }
        }

        public void BuyMarketing()
        {
            if (state.Funds >= state.AdCost)
            {
                state.Funds -= state.AdCost;
                state.MarketingLevel++;
                state.AdCost = Math.Floor(state.AdCost * 2);
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de fonds!");
            }
        }

        public void RaisePrice()
        {
            state.Margin = Math.Round(state.Margin + 0.01, 2);
            needsRedraw = true;
        }

        public void LowerPrice()
        {
            if (state.Margin >= 0.01)
            {
                state.Margin = Math.Round(state.Margin - 0.01, 2);
                needsRedraw = true;
            }
        }

        public void AddProcessor()
        {
            if (state.Trust > 0)
            {
                state.Processors++;
                state.Trust--;
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de trust!");
            }
        }

        public void AddMemory()
        {
            if (state.Trust > 0)
            {
                state.Memory++;
                state.Trust--;
                needsRedraw = true;
            }
            else
            {
                Console.WriteLine("\nPas assez de trust!");
            }
        }

        private void UpdateTrust()
        {
            if (state.TotalClipsProduced >= state.NextTrust)
            {
                state.Trust++;
                state.NextTrust = (long)(state.NextTrust * 1.618);
                Console.WriteLine($"\n*** TRUST AUGMENTÉ! Nouveau trust: {state.Trust} ***");
            }
        }

        public void Update()
        {
            var now = DateTime.Now;
            var elapsed = (now - lastUpdate).TotalSeconds;
            
            if (elapsed >= 0.1)
            {
                ProduceClips(elapsed);
                SellClips();
                UpdateOperations(elapsed);
                UpdateDemand();
                
                if ((now - lastAutoSave).TotalSeconds >= 30)
                {
                    SaveGame();
                    lastAutoSave = now;
                }
                
                lastUpdate = now;
            }
        }

        private void ProduceClips(double deltaTime)
        {
            if (state.Wire > 0 && state.ClipRate > 0)
            {
                double clipsToMake = state.ClipRate * deltaTime * 10;
                int actualClips = (int)Math.Min(clipsToMake, state.Wire);
                
                state.Wire -= actualClips;
                state.Clips += actualClips;
                state.TotalClipsProduced += actualClips;
                state.UnsoldClips += actualClips;
                UpdateTrust();
            }
        }

        private void SellClips()
        {
            if (state.UnsoldClips > 0)
            {
                double chanceOfPurchase = state.Demand / 100.0;
                if (random.NextDouble() < chanceOfPurchase)
                {
                    int clipsSold = Math.Min(
                        (int)(0.7 * Math.Pow(state.Demand, 1.15)),
                        (int)state.UnsoldClips
                    );
                    
                    state.UnsoldClips -= clipsSold;
                    state.Funds += clipsSold * state.Margin;
                }
            }
        }

        private void UpdateOperations(double deltaTime)
        {
            if (state.Operations < state.MaxOps)
            {
                double opCycle = state.Processors / 10.0 * deltaTime * 10;
                int opBuf = state.MaxOps - (int)state.Operations;
                
                if (opCycle > opBuf)
                    opCycle = opBuf;
                
                state.Operations += (long)opCycle;
            }
        }

        private void UpdateDemand()
        {
            double marketing = Math.Pow(1.1, state.MarketingLevel - 1);
            state.Demand = (int)((0.8 / state.Margin) * marketing * 10);
        }

        public void DisplayStatus()
        {
            // Only redraw every 200ms to reduce flicker
            var now = DateTime.Now;
            if ((now - lastDisplay).TotalMilliseconds < 200 && !needsRedraw)
                return;
            
            lastDisplay = now;
            needsRedraw = false;

            Console.SetCursorPosition(0, 0);
            
            // Header
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    UNIVERSAL PAPERCLIPS - Console Edition                    ║");
            Console.WriteLine("╠══════════════════════════════════════════════════════════════════════════════╣");
            
            // Main stats - Side by side (fixed width with invariant culture)
            string line1 = string.Format(invCulture, "║ Paperclips: {0,12}  │  Wire: {1,10} inches        │ Clips/s: {2,6:F2} ║", 
                state.Clips, state.Wire, state.ClipRate);
            Console.WriteLine(line1);
            
            string line2 = string.Format(invCulture, "║ Fonds: ${0,16:F2}  │  Prix: ${1,6:F2}              │ Demande: {2,4}%   ║",
                state.Funds, state.Margin, state.Demand);
            Console.WriteLine(line2);
            
            string line3 = string.Format(invCulture, "║ Inventaire: {0,9}  │  Wire à vendre: ${1,6:F2}           │ x{2,5}  ║",
                state.UnsoldClips, state.WireCost, state.WireAmount);
            Console.WriteLine(line3);
            
            Console.WriteLine("╠═══════════════════════════════╦══════════════════════════╦═══════════════════╣");
            
            // Production column | Business column | Computing column
            Console.WriteLine($"║ PRODUCTION                    ║ BUSINESS                 ║ COMPUTING         ║");
            Console.WriteLine("╟───────────────────────────────╫──────────────────────────╫───────────────────╢");
            
            // Line 1
            string autoClip = string.Format(invCulture, "AutoClip: {0,3} (${1,7:F2})", 
                state.ClipmakerLevel, state.ClipperCost);
            string marketing = string.Format(invCulture, "Marketing: Niv.{0,2}", 
                state.MarketingLevel);
            string trust = string.Format(invCulture, "Trust: {0,2}→{1,7}", 
                state.Trust, FormatInt(state.NextTrust));
            Console.WriteLine($"║ {autoClip,-29} ║ {marketing,-24} ║ {trust,-17} ║");
            
            // Line 2
            string megaClip = "";
            if (state.MegaClipperLevel > 0 || state.Funds >= 500)
            {
                megaClip = string.Format(invCulture, "MegaClip: {0,3} (${1,7:F0})", 
                    state.MegaClipperLevel, state.MegaClipperCost);
            }
            string adCost = string.Format(invCulture, "Coût: ${0,10:F2}", state.AdCost);
            string proc = string.Format(invCulture, "Proc: {0,2} Mem: {1,2}", 
                state.Processors, state.Memory);
            Console.WriteLine($"║ {megaClip,-29} ║ {adCost,-24} ║ {proc,-17} ║");
            
            // Line 3
            string ops = string.Format(invCulture, "Ops: {0,7}/{1,7}", 
                FormatInt(state.Operations), FormatInt(state.MaxOps));
            Console.WriteLine($"║                               ║                          ║ {ops,-17} ║");
            
            if (state.Creativity > 0 || state.Processors >= 5)
            {
                string creat = string.Format(invCulture, "Créativité: {0,5}", state.Creativity);
                Console.WriteLine($"║                               ║                          ║ {creat,-17} ║");
            }
            
            Console.WriteLine("╠═══════════════════════════════╩══════════════════════════╩═══════════════════╣");
            Console.WriteLine("║ [P]Make  [W]Wire  [A]Auto  [G]Mega  [M]Marketing  [+/-]Prix  [T]Proc  [Y]Mem ║");
            Console.WriteLine("║ [S]Save  [Q]Quit  [Ctrl+M]Menu                                               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            
            // Add blank lines to clear any leftover text
            for (int i = 0; i < 5; i++)
                Console.WriteLine("                                                                                ");
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      MENU PRINCIPAL                        ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("  PRODUCTION:");
            Console.WriteLine("    [P]  Créer un paperclip (1 wire)");
            Console.WriteLine("    [A]  Acheter AutoClipper (${0:F2})", state.ClipperCost);
            if (state.Funds >= 500 || state.MegaClipperLevel > 0)
                Console.WriteLine("    [G]  Acheter MegaClipper (${0:F2})", state.MegaClipperCost);
            Console.WriteLine();
            Console.WriteLine("  RESSOURCES:");
            Console.WriteLine("    [W]  Acheter Wire ({0} inches, ${1:F2})", state.WireAmount, state.WireCost);
            Console.WriteLine();
            Console.WriteLine("  VENTES:");
            Console.WriteLine("    [+]  Augmenter le prix (actuel: ${0:F2})", state.Margin);
            Console.WriteLine("    [-]  Diminuer le prix");
            Console.WriteLine("    [M]  Acheter Marketing - Niveau {0} (${1:F2})", state.MarketingLevel, state.AdCost);
            Console.WriteLine();
            Console.WriteLine("  COMPUTING:");
            Console.WriteLine("    [T]  Ajouter Processeur (Trust: {0})", state.Trust);
            Console.WriteLine("    [Y]  Ajouter Mémoire (Trust: {0})", state.Trust);
            Console.WriteLine();
            Console.WriteLine("  SYSTÈME:");
            Console.WriteLine("    [S]  Sauvegarder");
            Console.WriteLine("    [Q]  Quitter");
            Console.WriteLine();
            Console.WriteLine("  Appuyez sur une touche pour continuer...");
            Console.ReadKey(true);
        }

        public void ProcessInput(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.P:
                    MakePaperclip();
                    break;
                case ConsoleKey.W:
                    BuyWire();
                    break;
                case ConsoleKey.A:
                    BuyAutoClipper();
                    break;
                case ConsoleKey.G:
                    if (state.Funds >= 500 || state.MegaClipperLevel > 0)
                        BuyMegaClipper();
                    break;
                case ConsoleKey.M:
                    BuyMarketing();
                    break;
                case ConsoleKey.OemPlus:
                case ConsoleKey.Add:
                    RaisePrice();
                    break;
                case ConsoleKey.OemMinus:
                case ConsoleKey.Subtract:
                    LowerPrice();
                    break;
                case ConsoleKey.T:
                    AddProcessor();
                    break;
                case ConsoleKey.Y:
                    AddMemory();
                    break;
                case ConsoleKey.S:
                    SaveGame();
                    break;
            }
        }

        public bool IsRunning { get; set; } = true;

        public void CheckQuit(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Q)
            {
                Console.Clear();
                Console.WriteLine("\nSauvegarder avant de quitter? (O/N)");
                var response = Console.ReadKey(true);
                if (response.Key == ConsoleKey.O)
                {
                    SaveGame();
                }
                IsRunning = false;
            }
        }

        public void CheckMenu(ConsoleKeyInfo key)
        {
            if (key.KeyChar == 'm' && key.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                ShowMenu();
            }
        }
    }
}
