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
        public bool debugMode = false;

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
        public bool tweak_impassableDeepWater = false;
        public bool tweak_oldMegaslothName = false;
        public bool tweak_healrootToXerigium = false;
        public bool tweak_glowingAmbrosia = false;
        public bool tweak_glowingHealroot = false;

        public bool tweak_hunterMelee = false;
        public bool tweak_hunterMelee_fistFighting = false;
        public bool tweak_hunterMelee_allowSimpleSidearms = false;

        public bool tweak_boomalopesBleedChemfuel = false;
        public bool tweak_chattyComms = false;
        public bool tweak_notSoWildBerries = false;

        public bool patch_settlementTraderTimer = false;
        public bool patch_incidentPawnStats = false;
        public bool patch_prisonersDontHaveKeys = false;
        public bool patch_pdhk_ownDoor = true;
        public bool patch_pdhk_prisoners = true;
        public bool patch_pdhk_slaves = true;
        public bool patch_strongFloorsStopInfestations = false;

        public bool patch_slimRim = false;
        public bool patch_slimRim_fat = false;
        public bool patch_slimRim_hulk = false;
        public bool patch_slimRim_thin = false;

        public bool tweak_noFriendShapedManhunters = false;
        public bool tweak_NFSMTrainability_Intermediate = false;
        public bool tweak_NFSMTrainability_Advanced = false;
        public bool tweak_NFSMNuzzleHours = false;
        public float tweak_NFSMWildness = 0.65f;
        public float tweak_NFSMCombatPower = 60f;
        public bool tweak_NFSMDisableManhunterOnTame = false;

        public bool tweak_traitCountAdjustment = false;
        public IntRange tweak_traitCountRange = new IntRange(3, 3);

        public bool tweak_misanthropeTrait = false;

        public bool tweak_skillRates = false;
        public float tweak_skillRateLoss = 1f;
        public float tweak_skillRateGain = 1f;
        public float tweak_skillRateLossThreshold = 0f;

        // Power
        public bool tweak_powerUsageTweaks = false;
        public float tweak_powerUsage_lamp = 30f;
        public float tweak_powerUsage_sunlamp = 2900f;
        public float tweak_powerUsage_autodoor = 50f;
        public float tweak_powerUsage_vanometricCell = 1000f;

        // Raids
        public bool tweak_noMoreDropPodRaids = false;
        public bool tweak_noMoreBreachRaids = false;
        public bool tweak_noMoreSapperRaids = false;
        public bool tweak_noMoreSiegeRaids = false;

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

        // User Interface
        public bool patch_copyExportDialogs = false;

        // Royalty
        public bool tweak_delayedRoyalty = false;
        public bool tweak_replantableAnima = false;

        // Ideology
        public bool tweak_ancientDeconstruction = false;
        public bool tweak_ancientDeconstruction_mode = false;
        public bool tweak_replantableGauranlen = false;
        public bool tweak_unlockedIdeologyBuildings = false;
        public bool tweak_darklightGlowPods = false;
        public bool tweak_disableDesiredApparel = false;

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
            Scribe_Values.Look(ref tweak_impassableDeepWater, "tweak_impassableDeepWater", false);
            Scribe_Values.Look(ref tweak_oldMegaslothName, "tweak_oldMegaslothName", false);
            Scribe_Values.Look(ref tweak_healrootToXerigium, "tweak_healrootToXerigium", false);
            Scribe_Values.Look(ref tweak_glowingHealroot, "tweak_glowingHealroot", false);
            Scribe_Values.Look(ref tweak_glowingAmbrosia, "tweak_glowingAmbrosia", false);


            Scribe_Values.Look(ref tweak_hunterMelee, "tweak_hunterMelee", false);
            Scribe_Values.Look(ref tweak_hunterMelee_fistFighting, "tweak_hunterMelee_fistFighting", false);
            Scribe_Values.Look(ref tweak_hunterMelee_allowSimpleSidearms, "tweak_hunterMelee_allowSimpleSidearms", false);

            Scribe_Values.Look(ref tweak_boomalopesBleedChemfuel, "tweak_boomalopesBleedChemfuel", false);
            Scribe_Values.Look(ref tweak_chattyComms, "tweak_chattyComms", false);
            Scribe_Values.Look(ref tweak_notSoWildBerries, "tweak_notSoWildBerries", false);

            Scribe_Values.Look(ref patch_settlementTraderTimer, "patch_settlementTraderTimer", false);
            Scribe_Values.Look(ref patch_incidentPawnStats, "patch_incidentPawnStats", false);
            Scribe_Values.Look(ref patch_prisonersDontHaveKeys, "patch_prisonersDontHaveKeys", false);
            Scribe_Values.Look(ref patch_pdhk_ownDoor, "patch_pdhk_ownDoor", true);
            Scribe_Values.Look(ref patch_pdhk_prisoners, "patch_pdhk_prisoners", true);
            Scribe_Values.Look(ref patch_pdhk_slaves, "patch_pdhk_slaves", true);
            Scribe_Values.Look(ref patch_strongFloorsStopInfestations, "patch_strongFloorsStopInfestations", false);

            Scribe_Values.Look(ref patch_slimRim, "patch_slimRim", false);
            Scribe_Values.Look(ref patch_slimRim_fat, "patch_slimRim_fat", false);
            Scribe_Values.Look(ref patch_slimRim_hulk, "patch_slimRim_hulk", false);
            Scribe_Values.Look(ref patch_slimRim_thin, "patch_slimRim_thin", false);

            Scribe_Values.Look(ref tweak_noFriendShapedManhunters, "tweak_noFriendShapedManhunters", false);
            Scribe_Values.Look(ref tweak_NFSMTrainability_Intermediate, "tweak_NFSMTrainability_Intermediate", false);
            Scribe_Values.Look(ref tweak_NFSMTrainability_Advanced, "tweak_NFSMTrainability_Advanced", false);
            Scribe_Values.Look(ref tweak_NFSMNuzzleHours, "tweak_NFSMNuzzleHours", false);
            Scribe_Values.Look(ref tweak_NFSMWildness, "tweak_NFSMWildness", 0.65f);
            Scribe_Values.Look(ref tweak_NFSMCombatPower, "tweak_NFSMCombatPower", 60f);
            Scribe_Values.Look(ref tweak_NFSMDisableManhunterOnTame, "tweak_NFSMDisableManhunterOnTame", false);

            Scribe_Values.Look(ref tweak_traitCountAdjustment, "traitCountAdjustment", false);
            Scribe_Values.Look(ref tweak_traitCountRange, "traitCountRange", new IntRange(3, 3));
            Scribe_Values.Look(ref tweak_misanthropeTrait, "tweak_misanthropyTrait", false);

            Scribe_Values.Look(ref tweak_skillRates, "tweak_skillRates", false);
            Scribe_Values.Look(ref tweak_skillRateLoss, "tweak_skillRateLoss", 1f);
            Scribe_Values.Look(ref tweak_skillRateGain, "tweak_skillRateGain", 1f);
            Scribe_Values.Look(ref tweak_skillRateLossThreshold, "tweak_skillRateLossThreshold", 0f);

            // Power
            Scribe_Values.Look(ref tweak_powerUsageTweaks, "tweak_powerUsageTweaks", false);
            Scribe_Values.Look(ref tweak_powerUsage_lamp, "tweak_powerUsage_lamp", 30f);
            Scribe_Values.Look(ref tweak_powerUsage_sunlamp, "tweak_powerUsage_sunlamp", 2900f);
            Scribe_Values.Look(ref tweak_powerUsage_autodoor, "tweak_powerUsage_autodoor", 50f);
            Scribe_Values.Look(ref tweak_powerUsage_vanometricCell, "tweak_powerUsage_vanometricCell", 1000f);

            // Raids
            Scribe_Values.Look(ref tweak_noMoreDropPodRaids, "tweak_noMoreDropPodRaids", false);
            Scribe_Values.Look(ref tweak_noMoreBreachRaids, "tweak_noMoreBreachRaids", false);
            Scribe_Values.Look(ref tweak_noMoreSapperRaids, "tweak_noMoreSapperRaids", false);
            Scribe_Values.Look(ref tweak_noMoreSiegeRaids, "tweak_noMoreSiegeRaids", false);

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

            // User Interface
            Scribe_Values.Look(ref patch_copyExportDialogs, "patch_copyExportDialogs", false);

            // Royalty
            Scribe_Values.Look(ref tweak_delayedRoyalty, "tweak_delayedRoyalty", false);
            Scribe_Values.Look(ref tweak_replantableAnima, "tweak_replantableAnima", false);

            // Ideology
            Scribe_Values.Look(ref tweak_ancientDeconstruction, "tweak_ancientDeconstruction", false);
            Scribe_Values.Look(ref tweak_ancientDeconstruction_mode, "tweak_ancientDeconstruction_mode", false);
            Scribe_Values.Look(ref tweak_replantableGauranlen, "tweak_replantableGauranlen", false);
            Scribe_Values.Look(ref tweak_unlockedIdeologyBuildings, "tweak_unlockedIdeologyBuildings", false); 
            Scribe_Values.Look(ref tweak_darklightGlowPods, "tweak_darklightGlowPods", false);
            Scribe_Values.Look(ref tweak_disableDesiredApparel, "tweak_disableDesiredApparel", false);

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
