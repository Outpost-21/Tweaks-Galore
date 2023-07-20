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
    public class SectionWorker_Genepacks : SectionWorker
    {
        public Dictionary<string, float> sectionHeights = new Dictionary<string, float>();

        public float GetSectionHeight(string section)
        {
            if (!sectionHeights.ContainsKey(section))
            {
                sectionHeights.Add(section, float.MaxValue);
            }
            return sectionHeights[section];
        }

        public void SetSectionHeight(string section, float value)
        {
            sectionHeights[section] = value;
        }

        public Dictionary<GeneDef, ModContentPack> cachedGeneDictionary = new Dictionary<GeneDef, ModContentPack>();

        public Dictionary<GeneDef, ModContentPack> CachedGeneDictionary
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

        public List<ModContentPack> cachedModListing = new List<ModContentPack>();

        public List<ModContentPack> CachedModListing
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

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            if (listing.ButtonTextLabeled("", "Restore Section Defaults"))
            {
                DefaultUtil.RestoreSettings_Genepacks(settings);
                Messages.Message("Tweaks Galore: 'Genepacks' tweaks restored to defaults. Restart required to take effect.", MessageTypeDefOf.CautionInput);
            }

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

            SetGeneSettingsValues(settings);
        }

        public void DrawBiotechGenepackSettings(Listing_Standard listing)
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

        public void DrawModGenepackSettings(Listing_Standard listing, ModContentPack curMCP)
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
                    if (i == (modGenes.Count + (modGenes.Count % 2f == 0f ? 0f : 1f)) / 2f)
                    {
                        geneListing.NewColumn();
                    }
                    DrawGeneSetting(geneListing, modGenes[i]);
                }
                SetSectionHeight(categoryString, geneListing.MaxColumnHeightSeen);
                listing.EndSection(geneListing);
            }
        }

        public void DrawGeneSetting(Listing_Standard listing, GeneDef curGene)
        {
            bool tempBool = settings.genepacksEnabled[curGene.defName];
            string tooltip = curGene.LabelCap.Colorize(ColoredText.TipSectionTitleColor) + "\n\n" + curGene.DescriptionFull;
            listing.CheckboxLabeled(curGene.LabelCap, ref tempBool, tooltip);
            settings.genepacksEnabled[curGene.defName] = tempBool;
        }

        public List<GeneDef> GetGenesFromOfficialContent()
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

        public List<GeneDef> GetGenesFromContentPack(ModContentPack pack)
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

        public override void DoOnStartup(TweaksGaloreSettings settings)
        {
            if (settings.defaultGenepacksEnabled.NullOrEmpty())
            {
                settings.defaultGenepacksEnabled = new Dictionary<string, bool>();
            }
            if (settings.genepacksEnabled.NullOrEmpty())
            {
                settings.genepacksEnabled = new Dictionary<string, bool>();
            }

            List<GeneDef> list = (from x in DefDatabase<GeneDef>.AllDefs where (bool)(x.modContentPack != null) select x).ToList();
            foreach (GeneDef gene in list)
            {
                if (!settings.defaultGenepacksEnabled.ContainsKey(gene.defName))
                {
                    settings.defaultGenepacksEnabled.Add(gene.defName, gene.canGenerateInGeneSet);
                }
                if (!settings.genepacksEnabled.ContainsKey(gene.defName))
                {
                    settings.genepacksEnabled.Add(gene.defName, gene.canGenerateInGeneSet);
                }
            }
        }

        public void SetGeneSettingsValues(TweaksGaloreSettings settings)
        {
            foreach (KeyValuePair<string, bool> pair in settings.genepacksEnabled)
            {
                GeneDef gene = DefDatabase<GeneDef>.GetNamedSilentFail(pair.Key);
                if (gene != null)
                {
                    gene.canGenerateInGeneSet = pair.Value;
                }
            }
        }
    }
}
