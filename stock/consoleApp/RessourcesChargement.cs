using System.Reflection;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System;

namespace consoleApp
{
    public static class RessourcesChargement
    {
        public static List<Formulaires.FormulaireNouveauCompte> ChargerPremiersDepuisRessource()
        {
            var result = new List<Formulaires.FormulaireNouveauCompte>();
            try
            {
                var asm = Assembly.GetExecutingAssembly();
                var resourceName = asm.GetManifestResourceNames().FirstOrDefault(n => n.EndsWith("premiers.json"));
                if (resourceName == null) return result;

                using var stream = asm.GetManifestResourceStream(resourceName);
                if (stream == null) return result;

                using var doc = JsonDocument.Parse(stream);
                if (doc.RootElement.ValueKind != JsonValueKind.Array) return result;

                foreach (var element in doc.RootElement.EnumerateArray())
                {
                    // construire un dictionnaire insensible à la casse des propriétés pour accès direct par nom
                    var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (var prop in element.EnumerateObject())
                    {
                        // récupérer la valeur texte si possible
                        try
                        {
                            if (prop.Value.ValueKind == JsonValueKind.String)
                                map[prop.Name] = prop.Value.GetString();
                            else
                                map[prop.Name] = prop.Value.ToString();
                        }
                        catch
                        {
                            // ignorer la propriété problématique
                        }
                    }

                    // récupérer les champs attendus (Nom, NAS, MotDePasse)
                    map.TryGetValue("Nom", out var nom);
                    map.TryGetValue("NAS", out var nas);
                    map.TryGetValue("MotDePasse", out var mdp);

                    // si le nom est manquant, on ignore l'entrée
                    if (string.IsNullOrWhiteSpace(nom)) continue;

                    var form = new Formulaires.FormulaireNouveauCompte
                    {
                        Nom = nom,
                        NAS = nas ?? string.Empty,
                        MotDePasse = mdp ?? string.Empty,
                        MotDePasseConfirmation = mdp ?? string.Empty
                    };

                    result.Add(form);
                }
            }
            catch (Exception)
            {
                // en cas d'erreur, retourner la liste partiellement remplie ou vide
            }

            return result;
        }
    }
}
