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
        public static TweaksGaloreMod mod = TweaksGaloreMod.mod;
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static Dictionary<string, float> sectionHeights = new Dictionary<string, float>();

        public static float GetSectionHeight(string section)
        {
            if (!sectionHeights.ContainsKey(section))
            {
                sectionHeights.Add(section, float.MaxValue);
            }
            return sectionHeights[section];
        }

        public static void SetSectionHeight(string section, float value)
        {
            sectionHeights[section] = value;
        }

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
            string categoryString = "Cat_Biotech_Genepacks";
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader("Genepacks", mod.headerColor, ref categoryToggle);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                if (listing.ButtonTextLabeled("", "Restore defaults"))
                {
                    DefaultUtil.RestoreSettings_Genepacks(settings);
                    Messages.Message("Tweaks Galore: 'Genepacks' tweaks restored to defaults. Restart required to take effect.", MessageTypeDefOf.CautionInput);
                    TweaksGaloreMod.mod.restoreGenepacks = true;
                }
                if (mod.restoreGenepacks)
                {
                    listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!");
                }
                else
                {

                    listing.CheckboxEnhanced("Enable Genepack Tweaks", "This entire section is disabled by default for compatibility sake mostly, so there's no conflicting with other mods that choose to do this sort of tweak. These options allow you to choose whether or not a gene can spawn in genepacks.", ref settings.tweak_genepackTweaks);
                    if (settings.tweak_genepackTweaks)
                    {
                        DrawBiotechGenepackSettings(listing);
                        for (int i = 0; i < CachedModListing.Count; i++)
                        {
                            // Do Modded Genes
                            DrawModGenepackSettings(listing, CachedModListing[i]);
                        }
                    }
                }

                TweaksGaloreStartup.SetGeneSettingsValues(settings);
            }
        }

        public static void DrawBiotechGenepackSettings(Listing_Standard listing)
        {
            string categoryString = "Cat_Genepacks_Biotech";
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader("Biotech", mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                List<GeneDef> biotechGenes = GetGenesFromOfficialContent();
                biotechGenes.SortBy(gd => gd.label);
                Listing_Standard geneListing = listing.BeginSection(GetSectionHeight(categoryString));
                geneListing.ColumnWidth -= 26f;
                geneListing.ColumnWidth *= 0.5f;
                for (int i = 0; i < biotechGenes.Count(); i++)
                {
                    if (i == (biotechGenes.Count + (biotechGenes.Count % 2f == 0f ? 0f : 1f)) / 2f)
                    {
                        geneListing.NewColumn();
                    }
                    DrawGeneSetting(geneListing, biotechGenes[i]);
                }
                SetSectionHeight(categoryString, geneListing.CurHeight);
                listing.EndSection(geneListing);
            }
        }

        public static void DrawModGenepackSettings(Listing_Standard listing, ModContentPack curMCP)
        {
            string categoryString = "Cat_ModGenepacks_" + curMCP.Name;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(curMCP.Name, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                List<GeneDef> modGenes = GetGenesFromContentPack(curMCP);
                modGenes.SortBy(gd => gd.label);
                Listing_Standard geneListing = listing.BeginSection(GetSectionHeight(categoryString));
                geneListing.ColumnWidth -= 26f;
                geneListing.ColumnWidth *= 0.5f;
                for (int i = 0; i < modGenes.Count(); i++)
                {
                    if(i == (modGenes.Count + (modGenes.Count % 2f == 0f ? 0f : 1f)) / 2f)
                    {
                        geneListing.NewColumn();
                    }
                    DrawGeneSetting(geneListing, modGenes[i]);
                }
                SetSectionHeight(categoryString, geneListing.MaxColumnHeightSeen);
                listing.EndSection(geneListing);
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
