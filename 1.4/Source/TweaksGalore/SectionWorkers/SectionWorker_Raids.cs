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
    public class SectionWorker_Raids : SectionWorker
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

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            foreach (FactionDef faction in DefDatabase<FactionDef>.AllDefs)
            {
                if (!faction.isPlayer && !faction.pawnGroupMakers.NullOrEmpty())
                {
                    DoFactionRaidSettings(listing, faction);
                }
            }
        }

        public override void DoOnStartup()
        {

        }

        public override void DoSectionRestore()
        {

        }

        public void DoFactionRaidSettings(Listing_Standard listing, FactionDef faction)
        {
            string categoryString = "Cat_FactionRaids_" + faction.defName;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(faction.LabelCap, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                FactionRaidSettings factionSettings = settings.tweak_factionRaidSettings[faction.defName];
                Listing_Standard section1 = listing.BeginSection(GetSectionHeight(categoryString));
                section1.ColumnWidth -= 26f;
                section1.ColumnWidth *= 0.5f;
                section1.Note($"Type: {(faction.humanlikeFaction ? "Humanlike" : "Animal")}\n Always Enemy: {faction.permanentEnemy.ToString().CapitalizeFirst()}", GameFont.Tiny, Color.gray);
                factionSettings.DoSettings(section1);
                SetSectionHeight(categoryString, section1.MaxColumnHeightSeen);
                listing.EndSection(section1);
                Listing_Standard section2 = listing.BeginSection(GetSectionHeight(categoryString));
                section2.ColumnWidth -= 26f;
                section2.ColumnWidth *= 0.5f;
                section2.Note($"Type: {(faction.humanlikeFaction ? "Humanlike" : "Animal")}\n Always Enemy: {faction.permanentEnemy.ToString().CapitalizeFirst()}", GameFont.Tiny, Color.gray);
                factionSettings.DoRaidStrategies(section2);
                SetSectionHeight(categoryString, section2.MaxColumnHeightSeen);
                listing.EndSection(section2);
            }
        }

        public FactionRaidSettings MakeNewFactionRaidSetting(FactionDef faction)
        {
            return new FactionRaidSettings()
            {

            };
        }
    }
}
