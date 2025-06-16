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
    public class FactionRaidSettings : IExposable
    {
        public string factionDefName;

        [Unsaved]
        public FactionDef faction;

        public FactionDef GetFaction
        {
            get
            {
                if(faction == null)
                {
                    faction = DefDatabase<FactionDef>.GetNamedSilentFail(factionDefName);
                }
                return faction;
            }
        }

        public int techsBelow = 0;
        public bool canRaidBelow = true;
        public int techsAbove = 0;
        public bool canRaidAbove = true;
        public bool canRaidEqual = true;
        public bool appliesToFriendlyHelp = false;

        public void DoSettings(Listing_Standard listing)
        {
            listing.CheckboxLabeled("Can Raid Lower Techs", ref canRaidBelow, "If enabled, allows this faction to raid comparitively lower tech players.");
            float techsBelowInt = techsBelow;
            listing.AddLabeledSlider("Raids # Techs Below" + ": " + techsBelowInt.ToString(), ref techsBelowInt, 0, 7, $"Min: {0} (Unlimited)", $"Max: {7}", 1);
            techsBelow = Mathf.RoundToInt(techsBelowInt);

            listing.CheckboxLabeled("Can Raid Higher Techs", ref canRaidAbove, "If enabled, allows this faction to raid comparitively higher tech players.");
            float techsAboveInt = techsAbove;
            listing.AddLabeledSlider("Raids # Techs Higher" + ": " + techsAboveInt.ToString(), ref techsAboveInt, 0, 7, $"Min: {0} (Unlimited)", $"Max: {7}", 1);
            techsAbove = Mathf.RoundToInt(techsAboveInt);

            listing.CheckboxLabeled("Can Raid Equal Techs", ref canRaidAbove, "If enabled, allows this faction to raid players on an equivalent tech to them.");

            listing.CheckboxLabeled("Applies to Friendly Help", ref canRaidAbove, "If enabled, these same restrictions apply to factions coming to help during raids.");
        }

        public Dictionary<string, bool> raidStrategies = new Dictionary<string, bool>();

        public void DoRaidStrategies(Listing_Standard listing)
        {
            if (GetFaction == null) { return; }
            foreach (RaidStrategyDef strategy in DefDatabase<RaidStrategyDef>.AllDefs)
            {
                if (!raidStrategies.ContainsKey(strategy.defName))
                {
                    raidStrategies.Add(strategy.defName, !GetFaction.disallowedRaidStrategies.Contains(strategy));
                }
            }
        }

        public void UpdateStrategyListing()
        {
            if(GetFaction == null) { return; }
            if (raidStrategies.NullOrEmpty())
            {
                raidStrategies = new Dictionary<string, bool>();
            }
            foreach (RaidStrategyDef strategy in DefDatabase<RaidStrategyDef>.AllDefs)
            {
                if (!raidStrategies.ContainsKey(strategy.defName))
                {
                    raidStrategies.Add(strategy.defName, !GetFaction.disallowedRaidStrategies.Contains(strategy));
                }
            }
        }

        public Dictionary<string, bool> raidBiomes = new Dictionary<string, bool>();

        public void DoRaidBiomes(Listing_Standard listing)
        {
            foreach (BiomeDef biome in DefDatabase<BiomeDef>.AllDefs)
            {
                if (biome.canBuildBase)
                {
                    bool tempBool = raidBiomes[biome.defName];
                    listing.CheckboxLabeled(biome.LabelCap, ref tempBool, biome.description);
                    raidBiomes[biome.defName] = tempBool;
                }
            }
        }

        public void UpdateBiomeListing()
        {
            if (GetFaction == null) { return; }
            if (raidBiomes.NullOrEmpty())
            {
                raidBiomes = new Dictionary<string, bool>();
            }
            foreach (BiomeDef biome in DefDatabase<BiomeDef>.AllDefs)
            {
                if (!raidBiomes.ContainsKey(biome.defName))
                {
                    raidBiomes.Add(biome.defName, biome.canBuildBase);
                }
            }
        }

        public void OnStartup()
        {
            UpdateStrategyListing();
            UpdateBiomeListing();
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref factionDefName, "factionDefName");
            Scribe_Values.Look(ref techsBelow, "techsBelow");
            Scribe_Values.Look(ref techsAbove, "techsAbove");
            Scribe_Values.Look(ref canRaidAbove, "canRaidAbove");
            Scribe_Values.Look(ref canRaidBelow, "canRaidBelow");
            Scribe_Values.Look(ref canRaidEqual, "canRaidEqual");
            Scribe_Values.Look(ref appliesToFriendlyHelp, "appliesToFriendlyHelp");
            Scribe_Collections.Look(ref raidStrategies, "raidStrategies");
            Scribe_Collections.Look(ref raidBiomes, "raidBiomes");
        }
    }
}
