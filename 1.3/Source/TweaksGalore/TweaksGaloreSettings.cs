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
        public bool tweak_insultingSpreeNerf = false;
        public bool tweak_lagFreeLamps = false;
        public bool tweak_fasterSmoothing = false;
        public float tweak_fasterSmoothing_factor = 3f;
        public bool tweak_destroyAnything = false;
        public bool tweak_skilledStonecutting = false;
        public bool tweak_dontPackFood = false;
        public bool tweak_preReleaseShipParts = false;
        public bool tweak_fixDeconstructionReturn = false;
        public bool tweak_noBreakdowns = false;

        public bool patch_incidentPawnStats = false;

        // Power
        public bool tweak_powerUsageTweaks = false;
        public float tweak_powerUsage_lamp = 30f;
        public float tweak_powerUsage_sunlamp = 2900f;
        public float tweak_powerUsage_autodoor = 50f;
        public float tweak_powerUsage_vanometricCell = 1000f;

        // Resources
        public bool tweak_strongerSteel = false;
        public bool tweak_noMineablePlasteel = false;
        public bool tweak_noMineableComponents = false;
        public bool tweak_prettyPreciousMetals = false;
        public bool tweak_metalDoesntBurn = false;
        public bool tweak_oldComponentNames = false;
        public bool tweak_stackableChunks = false;
        public float tweak_stackableChunks_stone = 5f;
        public float tweak_stackableChunks_slag = 5f;

        // Mechanoids
        public float tweak_mechanoidHeatArmour = 2f;

        public bool patch_disableMechanoidAdapting = false;

        // Royalty
        public bool tweak_delayedRoyalty = false;
        public bool tweak_replantableAnima = false;

        // Ideology
        public bool tweak_ancientDeconstruction = false;
        public bool tweak_ancientDeconstruction_mode = false;
        public bool tweak_replantableGauranlen = false;

        public bool patch_properSuppression = false;
        public bool patch_noMemeLimit = false;

        public override void ExposeData()
        {
            base.ExposeData();
            // Vanilla
            Scribe_Values.Look(ref tweak_insultingSpreeNerf, "tweak_insultingSpreeNerf", false);
            Scribe_Values.Look(ref tweak_lagFreeLamps, "tweak_lagFreeLamps", false);
            Scribe_Values.Look(ref tweak_fasterSmoothing, "tweak_fasterSmoothing", false);
            Scribe_Values.Look(ref tweak_fasterSmoothing_factor, "tweak_fasterSmoothing_factor", 3f);
            Scribe_Values.Look(ref tweak_destroyAnything, "tweak_destroyAnything", false);
            Scribe_Values.Look(ref tweak_skilledStonecutting, "tweak_skilledStonecutting", false);
            Scribe_Values.Look(ref tweak_dontPackFood, "tweak_dontPackFood", false);
            Scribe_Values.Look(ref tweak_preReleaseShipParts, "tweak_preReleaseShipParts", false);
            Scribe_Values.Look(ref tweak_fixDeconstructionReturn, "tweak_fixDeconstructionReturn", false);
            Scribe_Values.Look(ref tweak_noBreakdowns, "tweak_noBreakdowns", false);

            Scribe_Values.Look(ref patch_incidentPawnStats, "patch_incidentPawnStats", false);

            // Power
            Scribe_Values.Look(ref tweak_powerUsageTweaks, "tweak_powerUsageTweaks", false);
            Scribe_Values.Look(ref tweak_powerUsage_lamp, "tweak_powerUsage_lamp", 30f);
            Scribe_Values.Look(ref tweak_powerUsage_sunlamp, "tweak_powerUsage_sunlamp", 2900f);
            Scribe_Values.Look(ref tweak_powerUsage_autodoor, "tweak_powerUsage_autodoor", 50f);
            Scribe_Values.Look(ref tweak_powerUsage_vanometricCell, "tweak_powerUsage_vanometricCell", 1000f);

            // Resources
            Scribe_Values.Look(ref tweak_strongerSteel, "tweak_strongerSteel", false);
            Scribe_Values.Look(ref tweak_noMineablePlasteel, "tweak_noMineablePlasteel", false);
            Scribe_Values.Look(ref tweak_noMineableComponents, "tweak_noMineableComponents", false);
            Scribe_Values.Look(ref tweak_prettyPreciousMetals, "tweak_prettyPreciousMetals", false);
            Scribe_Values.Look(ref tweak_metalDoesntBurn, "tweak_metalDoesntBurn", false);
            Scribe_Values.Look(ref tweak_oldComponentNames, "tweak_oldComponentNames", false);
            Scribe_Values.Look(ref tweak_stackableChunks, "tweak_stackableChunks", false);
            Scribe_Values.Look(ref tweak_stackableChunks_stone, "tweak_stackableChunks_stone", 5f);
            Scribe_Values.Look(ref tweak_stackableChunks_slag, "tweak_stackableChunks_slag", 5f);

            // Mechanoids
            Scribe_Values.Look(ref tweak_mechanoidHeatArmour, "tweak_mechanoidHeatArmour", 2f);
            Scribe_Values.Look(ref patch_disableMechanoidAdapting, "tweak_disableMechanoidAdapting", false);

            // Royalty
            Scribe_Values.Look(ref tweak_delayedRoyalty, "tweak_delayedRoyalty", false);
            Scribe_Values.Look(ref tweak_replantableAnima, "tweak_replantableAnima", false);

            // Ideology
            Scribe_Values.Look(ref tweak_ancientDeconstruction, "tweak_ancientDeconstruction", false);
            Scribe_Values.Look(ref tweak_ancientDeconstruction_mode, "tweak_ancientDeconstruction_mode", false);
            Scribe_Values.Look(ref tweak_replantableGauranlen, "tweak_replantableGauranlen", false);

            Scribe_Values.Look(ref patch_properSuppression, "patch_properSuppression", false);
            Scribe_Values.Look(ref patch_noMemeLimit, "patch_noMemeLimit", false);
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
