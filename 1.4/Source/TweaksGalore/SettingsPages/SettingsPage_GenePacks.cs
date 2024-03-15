using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
    public static class SettingsPage_GenePacks
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static Dictionary<GeneDef, ModContentPack> cachedGeneDictionary = new Dictionary<GeneDef, ModContentPack>();

        public static Dictionary<GeneDef, ModContentPack> CachedGeneDictionary
        {
            get
            {
                if (cachedGeneDictionary.NullOrEmpty())
                {
                    cachedGeneDictionary = new Dictionary<GeneDef, ModContentPack>();
                    List<string> startList = (from x in settings.genepacksEnabled.Keys.ToList() orderby x descending select x).ToList();
                    foreach (string name in startList)
                    {
                        GeneDef gene = DefDatabase<GeneDef>.GetNamedSilentFail(name);
                        if (gene != null && gene.modContentPack != null)
                        {
                            cachedGeneDictionary.Add(gene, gene.modContentPack);
                        }
                    }
                }
                return cachedGeneDictionary;
            }
        }

        public static List<ModContentPack> cachedModListing = new List<ModContentPack>();

        public static List<ModContentPack> CachedModListing
        {
            get
            {
                if (cachedModListing.NullOrEmpty())
                {
                    cachedModListing = CachedGeneDictionary.Values.Distinct().ToList();
                    for (int i = 0; i < cachedModListing.Count; i++)
                    {
                        if (cachedModListing[i].IsCoreMod || cachedModListing[i].IsOfficialMod)
                        {
                            cachedModListing.RemoveAt(i);
                        }
                    }
                    cachedModListing.SortBy(cml => cml.Name);
                }
                return cachedModListing;
            }
        }

        public static void DoSettings_Genepacks(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Genepack Tweaks", "This entire section is disabled by default for compatibility sake mostly, so there's no conflicting with other mods that choose to do this sort of tweak. These options allow you to choose whether or not a gene can spawn in genepacks.", ref settings.tweak_genepackTweaks);
            if (settings.tweak_genepackTweaks)
            {

                listing.Note("\bBiotech\b", GameFont.Medium);
                listing.GapLine();
                List<GeneDef> biotechGenes = GetGenesFromOfficialContent();
                biotechGenes.SortBy(gd => gd.label);
                for (int i = 0; i < biotechGenes.Count(); i++)
                {
                    DrawGeneSetting(listing, biotechGenes[i]);
                }
                listing.GapLine();
                for (int i = 0; i < CachedModListing.Count; i++)
                {
                    // Do Modded Genes
                    ModContentPack curMCP = CachedModListing[i];
                    listing.Note("\b" + curMCP.Name + "\b", GameFont.Medium);
                    listing.GapLine();
                    List<GeneDef> modGenes = GetGenesFromContentPack(curMCP);
                    modGenes.SortBy(gd => gd.label);
                    for (int j = 0; j < modGenes.Count(); j++)
                    {
                        DrawGeneSetting(listing, modGenes[j]);
                    }
                    listing.GapLine();
                }

                TweaksGaloreStartup.SetGeneSettingsValues(settings);
            }
        }

        public static void DrawGeneSetting(Listing_Standard listing, GeneDef curGene)
        {
            bool tempBool = settings.genepacksEnabled[curGene.defName];
            string tooltip = curGene.LabelCap.Colorize(ColoredText.TipSectionTitleColor) + "\n\n" + curGene.DescriptionFull;
            listing.CheckboxLabeled(curGene.LabelCap, ref tempBool, tooltip);
            settings.genepacksEnabled[curGene.defName] = tempBool;
        }

        public static List<GeneDef> GetGenesFromOfficialContent()
        {
            List<GeneDef> results = new List<GeneDef>();

            foreach (KeyValuePair<GeneDef, ModContentPack> kvp in CachedGeneDictionary)
            {
                if (kvp.Value.IsCoreMod || kvp.Value.IsOfficialMod)
                {
                    if (!(kvp.Key.endogeneCategory == EndogeneCategory.Melanin))
                    {
                        results.Add(kvp.Key);
                    }
                }
            }

            return results;
        }

        public static List<GeneDef> GetGenesFromContentPack(ModContentPack pack)
        {
            List<GeneDef> results = new List<GeneDef>();

            foreach (KeyValuePair<GeneDef, ModContentPack> kvp in CachedGeneDictionary)
            {
                if (kvp.Value?.PackageId == pack?.PackageId)
                {
                    results.Add(kvp.Key);
                }
            }

            return results;
        }
    }
}
