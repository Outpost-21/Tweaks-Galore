using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace TweaksGalore
{
    public class TweaksGaloreSettings : ModSettings
    {
        // Vanilla
        public bool tweak_insultingSpreeNerf = true;
        public bool tweak_lagFreeLamps = true;
        public bool tweak_fasterSmoothing = true;
        public float tweak_fasterSmoothing_factor = 3f;
        public bool tweak_destroyAnything = true;
        public bool tweak_skilledStonecutting = true;
        public bool tweak_dontPackFood = true;
        public bool tweak_preReleaseShipParts = true;
        public bool tweak_fixDeconstructionReturn = true;
        public bool tweak_noBreakdowns = true;

        public bool patch_incidentPawnStats = true;

        // Power
        public bool tweak_powerUsageTweaks = true;
        public float tweak_powerUsage_lamp = 30f;
        public float tweak_powerUsage_sunlamp = 2900f;
        public float tweak_powerUsage_autodoor = 50f;
        public float tweak_powerUsage_vanometricCell = 1000f;

        // Resources
        public bool tweak_strongerSteel = true;
        public bool tweak_noMineablePlasteel = true;
        public bool tweak_noMineableComponents = true;
        public bool tweak_prettyPreciousMetals = true;
        public bool tweak_metalDoesntBurn = true;
        public bool tweak_oldComponentNames = true;
        public bool tweak_stackableChunks = true;
        public float tweak_stackableChunks_stone = 5f;
        public float tweak_stackableChunks_slag = 5f;

        // Mechanoids
        public float tweak_mechanoidHeatArmour = 2f;

        public bool patch_disableMechanoidAdapting = true;

        // Royalty
        public bool tweak_delayedRoyalty = true;
        public bool tweak_replantableAnima = true;

        // Ideology
        public bool tweak_ancientDeconstruction = true;
        public bool tweak_ancientDeconstruction_mode = true;
        public bool tweak_replantableGauranlen = true;

        public bool patch_properSuppression = true;

        // Mods
        // Wall Light
        //public bool tweak_wallLight_vanillaColours = true;

        public override void ExposeData()
        {
            base.ExposeData();
            // Vanilla
            Scribe_Values.Look(ref tweak_insultingSpreeNerf, "tweak_insultingSpreeNerf", true);
            Scribe_Values.Look(ref tweak_lagFreeLamps, "tweak_lagFreeLamps", true);
            Scribe_Values.Look(ref tweak_fasterSmoothing, "tweak_fasterSmoothing", true);
            Scribe_Values.Look(ref tweak_fasterSmoothing_factor, "tweak_fasterSmoothing_factor", 3f);
            Scribe_Values.Look(ref tweak_destroyAnything, "tweak_destroyAnything", true);
            Scribe_Values.Look(ref tweak_skilledStonecutting, "tweak_skilledStonecutting", true);
            Scribe_Values.Look(ref tweak_dontPackFood, "tweak_dontPackFood", true);
            Scribe_Values.Look(ref tweak_preReleaseShipParts, "tweak_preReleaseShipParts", true);
            Scribe_Values.Look(ref tweak_fixDeconstructionReturn, "tweak_fixDeconstructionReturn", true);
            Scribe_Values.Look(ref tweak_noBreakdowns, "tweak_noBreakdowns", true);

            Scribe_Values.Look(ref patch_incidentPawnStats, "patch_incidentPawnStats", true);

            // Power
            Scribe_Values.Look(ref tweak_powerUsageTweaks, "tweak_powerUsageTweaks", true);
            Scribe_Values.Look(ref tweak_powerUsage_lamp, "tweak_powerUsage_lamp", 30f);
            Scribe_Values.Look(ref tweak_powerUsage_sunlamp, "tweak_powerUsage_sunlamp", 2900f);
            Scribe_Values.Look(ref tweak_powerUsage_autodoor, "tweak_powerUsage_autodoor", 50f);
            Scribe_Values.Look(ref tweak_powerUsage_vanometricCell, "tweak_powerUsage_vanometricCell", 1000f);

            // Resources
            Scribe_Values.Look(ref tweak_strongerSteel, "tweak_strongerSteel", true);
            Scribe_Values.Look(ref tweak_noMineablePlasteel, "tweak_noMineablePlasteel", true);
            Scribe_Values.Look(ref tweak_noMineableComponents, "tweak_noMineableComponents", true);
            Scribe_Values.Look(ref tweak_prettyPreciousMetals, "tweak_prettyPreciousMetals", true);
            Scribe_Values.Look(ref tweak_metalDoesntBurn, "tweak_metalDoesntBurn", true);
            Scribe_Values.Look(ref tweak_oldComponentNames, "tweak_oldComponentNames", true);
            Scribe_Values.Look(ref tweak_stackableChunks, "tweak_stackableChunks", true);
            Scribe_Values.Look(ref tweak_stackableChunks_stone, "tweak_stackableChunks_stone", 5f);
            Scribe_Values.Look(ref tweak_stackableChunks_slag, "tweak_stackableChunks_slag", 5f);

            // Mechanoids
            Scribe_Values.Look(ref tweak_mechanoidHeatArmour, "tweak_mechanoidHeatArmour", 2f);
            Scribe_Values.Look(ref patch_disableMechanoidAdapting, "tweak_disableMechanoidAdapting", true);

            // Royalty
            Scribe_Values.Look(ref tweak_delayedRoyalty, "tweak_delayedRoyalty", true);
            Scribe_Values.Look(ref tweak_replantableAnima, "tweak_replantableAnima", true);

            // Ideology
            Scribe_Values.Look(ref tweak_ancientDeconstruction, "tweak_ancientDeconstruction", true);
            Scribe_Values.Look(ref tweak_ancientDeconstruction_mode, "tweak_ancientDeconstruction_mode", true);
            Scribe_Values.Look(ref tweak_replantableGauranlen, "tweak_replantableGauranlen", true);

            Scribe_Values.Look(ref patch_properSuppression, "patch_properSuppression", true);

            // Mods
            // Wall Light
            //Scribe_Values.Look(ref tweak_wallLight_vanillaColours, "tweak_wallLight_vanillaColours", true);
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }
}
