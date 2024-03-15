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

        public bool tweak_disableNarrowHeads = false;
        public bool tweak_growableAmbrosia = false;
        public bool tweak_growableGrass = false;
        public bool tweak_growableMushrooms = false;

        public bool tweak_hiddenConduits = false;
        public bool patch_lowPrisonerExpectations = false;

        // Penned Animals
        public bool restorePennedAnimals = false;
        public bool tweak_pennedAnimalConfig = false;
        public Dictionary<string, float> tweak_pennedAnimalDict = new Dictionary<string, float>();

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
        public bool tweak_noCowardlyRaiders = false;

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
        public bool tweak_betterGloomlight = false;
        public bool tweak_gloomlightSunlamp = false;
        public bool tweak_gloomlightDarklight = false;

        // User Interface
        public bool patch_copyExportDialogs = false;

        // Royalty
        public bool tweak_delayedRoyalty = false;
        public bool tweak_uninstallableMechShields = false;
        public bool tweak_paintableRoyalStuff = false;
        public bool tweak_workingRoyalty = false;
        public bool tweak_waitThisIsBetter = false;
        public bool tweak_noTradingPermit = false;
        public bool tweak_throneroomAllowAltars = false;
        public bool tweak_throneroomAnyBuildings = false;

        // Anima
        public bool tweak_animaTweaks = false;
        public bool tweak_replantableAnima = false;
        public bool tweak_animaDisableScream = false;
        public float tweak_animaScreamDebuff = -6f;
        public float tweak_animaScreamLength = 5f;
        public float tweak_animaScreamStackLimit = 3f;
        public float tweak_animaArtificialBuildingRadius = 34.9f;
        public float tweak_animaShrineBuildingRadius = 34.9f;
        public float tweak_animaBuffBuildingRadius = 9.9f;
        public float tweak_animaShrineBuffBuildingRadius = 9.9f;
        public float tweak_animaMaxBuffBuildings = 4f;
        public List<int> tweak_animaPsylinkLevelNeeds = new List<int>();
        public bool tweak_animaMeditationAll = false;
        public bool tweak_animaBuildableShrines = false;
        public bool tweak_animaRemoveBackstoryLimits = false;
        public float tweak_animaMeditationGain = 0.5f;

        // Titles
        public Dictionary<string, RoyalTitleSettings> tweak_royalTitleSettings = new Dictionary<string, RoyalTitleSettings>();

        [Unsaved]
        public Dictionary<string, RoyalTitleSettings> royalTitleSettingsDefaults = new Dictionary<string, RoyalTitleSettings>();

        // Permits
        public Dictionary<string, RoyalPermitSettings> tweak_royalPermitSettings = new Dictionary<string, RoyalPermitSettings>();

        [Unsaved]
        public Dictionary<string, RoyalPermitSettings> royalPermitSettingsDefaults = new Dictionary<string, RoyalPermitSettings>();

        // Ideology
        public bool tweak_ancientDeconstruction = false;
        public bool tweak_ancientDeconstruction_mode = false;
        public bool tweak_unlockedIdeologyBuildings = false;
        public bool tweak_darklightGlowPods = false;
        public bool tweak_disableDesiredApparel = false;

        public bool patch_properSuppression = false;
        public float patch_properSuppressionPercentage = 0.7f;
        public bool patch_noMemeLimit = false;

        // Gauranlen
        public bool tweak_gauranlenTweaks = false;
        public bool tweak_replantableGauranlen = false;
        public FloatRange tweak_gauranlenInitialConnectionStrength = new FloatRange(0.25f, 0.45f);
        public float tweak_gauranlenConnectionGainPerHourPruning = 0.01f;
        public float tweak_gauranlenDryadSpawnDays = 8f;
        public float tweak_gauranlenMaxDryads = 4f;
        public float tweak_gauranlenArtificialBuildingRadius = 7.9f;
        public float tweak_gauranlenConnectionLossPerLevel = 1f;
        public float tweak_gauranlenLossPerBuilding = 1f;
        public float tweak_gauranlenPlantGrowthRadius = 7.9f;
        public float tweak_gauranlenMossGrowDays = 5;
        public float tweak_gauranlenCocoonDaysToComplete = 6;
        public float tweak_gauranlenGaumakerDaysToComplete = 4;
        public float tweak_gauranlenPodHarvestYield = 1;

        // Biotech
        public bool tweak_flattenComplexity = false;
        public bool tweak_flattenMetabolism = false;
        public bool tweak_flattenArchites = false;
        public bool tweak_showGenesTab = false;
        public bool tweak_playerMechTweaks = false;
        public bool tweak_mechTweaksAffectMods = false;
        public float tweak_mechanoidSkillLevel = 10f;
        public float tweak_mechanoidRechargeRate = 1.0f;
        public float tweak_mechanoidDrainRate = 1.0f;
        public float tweak_mechanoidWorkSpeed = 0.2f;
        public bool tweak_mechanitorTweaks = false;
        public bool tweak_mechanitorDisableRange = false;
        public float tweak_mechanitorRangeMulti = 1.0f;
        public float tweak_mechanitorBandwidthBase = 6f;
        public float tweak_mechanitorControlGroupBase = 2f;
        public float tweak_mechanitorBandNodeBandwidth = 1f;

        public bool tweak_primitiveVasectomy = false;
        public float tweak_pregnancyLengthMultiplier = 1.0f;
        public float tweak_defaultPregnancyChance = 0.03f;
        public List<PawnKindDef> tweak_pregnancyChanceEditedPawnKinds = new List<PawnKindDef>();


        // Polux
        public bool tweak_poluxTweaks = false;
        public bool tweak_replantablePolux = false;
        public float tweak_poluxEffectRadius = 7.9f;
        public float tweak_poluxEffectRate = 1.0f;
        public bool tweak_poluxArtificialDisables = false;

        // Genepacks
        public bool tweak_genepackTweaks = false;
        public Dictionary<string, bool> genepacksEnabled = new Dictionary<string, bool>();
        public Dictionary<string, bool> defaultGenepacksEnabled = new Dictionary<string, bool>();

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

            Scribe_Values.Look(ref tweak_disableNarrowHeads, "tweak_disableNarrowHeads", false);
            Scribe_Values.Look(ref tweak_growableAmbrosia, "tweak_growableAmbrosia", false);
            Scribe_Values.Look(ref tweak_growableGrass, "tweak_growableGrass", false);
            Scribe_Values.Look(ref tweak_growableMushrooms, "tweak_growableMushrooms", false);

            Scribe_Values.Look(ref tweak_hiddenConduits, "tweak_hiddenConduits", false);
            Scribe_Values.Look(ref patch_lowPrisonerExpectations, "patch_lowPrisonerExpectations", false);

            // Penned Animals
            Scribe_Values.Look(ref restorePennedAnimals, "restorePennedAnimals", false);
            Scribe_Values.Look(ref tweak_pennedAnimalConfig, "tweak_pennedAnimalConfig", false);
            Scribe_Collections.Look(ref tweak_pennedAnimalDict, "tweak_pennedAnimalDict");

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
            Scribe_Values.Look(ref tweak_noCowardlyRaiders, "tweak_noCowardlyRaiders", false);

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
            Scribe_Values.Look(ref tweak_betterGloomlight, "tweak_betterGloomlight", false);
            Scribe_Values.Look(ref tweak_gloomlightSunlamp, "tweak_gloomlightSunlamp", false);
            Scribe_Values.Look(ref tweak_gloomlightDarklight, "tweak_gloomlightDarklight", false);

            // User Interface
            Scribe_Values.Look(ref patch_copyExportDialogs, "patch_copyExportDialogs", false);

            // Royalty
            Scribe_Values.Look(ref tweak_delayedRoyalty, "tweak_delayedRoyalty", false);
            Scribe_Values.Look(ref tweak_replantableAnima, "tweak_replantableAnima", false);
            Scribe_Values.Look(ref tweak_uninstallableMechShields, "tweak_uninstallableMechShields", false);
            Scribe_Values.Look(ref tweak_waitThisIsBetter, "tweak_waitThisIsBetter", false);
            Scribe_Values.Look(ref tweak_noTradingPermit, "tweak_noTradingPermit", false);
            Scribe_Values.Look(ref tweak_throneroomAllowAltars, "tweak_throneroomAllowAltars", false);
            Scribe_Values.Look(ref tweak_throneroomAnyBuildings, "tweak_throneroomAnyBuildings", false);

            Scribe_Values.Look(ref tweak_animaTweaks, "tweak_animaTweaks", false);
            Scribe_Values.Look(ref tweak_animaDisableScream, "tweak_animaDisableScream", false);
            Scribe_Values.Look(ref tweak_animaScreamDebuff, "tweak_animaScreamDebuff", -6f);
            Scribe_Values.Look(ref tweak_animaScreamLength, "tweak_animaScreamLength", 5f);
            Scribe_Values.Look(ref tweak_animaScreamStackLimit, "tweak_animaScreamStackLimit", 3f);
            Scribe_Values.Look(ref tweak_animaArtificialBuildingRadius, "tweak_animaArtificialBuildingRadius", 34.9f);
            Scribe_Values.Look(ref tweak_animaShrineBuildingRadius, "tweak_animaShrineBuildingRadius", 34.9f);
            Scribe_Values.Look(ref tweak_animaBuffBuildingRadius, "tweak_animaBuffBuildingRadius", 9.9f);
            Scribe_Values.Look(ref tweak_animaShrineBuffBuildingRadius, "tweak_animaShrineBuffBuildingRadius", 9.9f);
            Scribe_Values.Look(ref tweak_animaMaxBuffBuildings, "tweak_animaMaxBuffBuildings", 4f);
            Scribe_Collections.Look(ref tweak_animaPsylinkLevelNeeds, "tweak_animaPsylinkLevelNeeds");
            Scribe_Values.Look(ref tweak_animaMeditationAll, "tweak_animaMeditationAll", false);
            Scribe_Values.Look(ref tweak_animaBuildableShrines, "tweak_animaBuildableShrines", false);
            Scribe_Values.Look(ref tweak_animaRemoveBackstoryLimits, "tweak_animaRemoveBackstoryLimits", false);
            Scribe_Values.Look(ref tweak_animaMeditationGain, "tweak_animaMeditationGain", 0.5f);

            // Royal Titles
            Scribe_Collections.Look(ref tweak_royalTitleSettings, "tweak_royalTitleSettings");

            // Royal Permits
            Scribe_Collections.Look(ref tweak_royalPermitSettings, "tweak_royalPermitSettings");

            // Ideology
            Scribe_Values.Look(ref tweak_ancientDeconstruction, "tweak_ancientDeconstruction", false);
            Scribe_Values.Look(ref tweak_ancientDeconstruction_mode, "tweak_ancientDeconstruction_mode", false);
            Scribe_Values.Look(ref tweak_replantableGauranlen, "tweak_replantableGauranlen", false);
            Scribe_Values.Look(ref tweak_unlockedIdeologyBuildings, "tweak_unlockedIdeologyBuildings", false); 
            Scribe_Values.Look(ref tweak_darklightGlowPods, "tweak_darklightGlowPods", false);
            Scribe_Values.Look(ref tweak_disableDesiredApparel, "tweak_disableDesiredApparel", false);

            Scribe_Values.Look(ref patch_properSuppression, "patch_properSuppression", false);
            Scribe_Values.Look(ref patch_properSuppressionPercentage, "patch_properSuppressionPercentage", 0.7f);
            Scribe_Values.Look(ref patch_noMemeLimit, "patch_noMemeLimit", false);

            Scribe_Values.Look(ref tweak_gauranlenTweaks, "tweak_gauranlenTweaks", false);
            Scribe_Values.Look(ref tweak_gauranlenInitialConnectionStrength, "tweak_gauranlenInitialConnectionStrength", new FloatRange(0.25f, 0.45f));
            Scribe_Values.Look(ref tweak_gauranlenConnectionGainPerHourPruning, "tweak_gauranlenConnectionGainPerHourPruning", 0.01f);
            Scribe_Values.Look(ref tweak_gauranlenDryadSpawnDays, "tweak_gauranlenDryadSpawnDays", 8);
            Scribe_Values.Look(ref tweak_gauranlenMaxDryads, "tweak_gauranlenMaxDryads", 4);
            Scribe_Values.Look(ref tweak_gauranlenArtificialBuildingRadius, "tweak_gauranlenArtificialBuildingRadius", 7.9f);
            Scribe_Values.Look(ref tweak_gauranlenConnectionLossPerLevel, "tweak_gauranlenConnectionLossPerLevel", 1f);
            Scribe_Values.Look(ref tweak_gauranlenLossPerBuilding, "tweak_gauranlenLossPerBuilding", 1f);
            Scribe_Values.Look(ref tweak_gauranlenPlantGrowthRadius, "tweak_gauranlenPlantGrowthRadius", 7.9f);
            Scribe_Values.Look(ref tweak_gauranlenMossGrowDays, "tweak_gauranlenMossGrowDays", 5);
            Scribe_Values.Look(ref tweak_gauranlenCocoonDaysToComplete, "tweak_gauranlenCocoonDaysToComplete", 6);
            Scribe_Values.Look(ref tweak_gauranlenGaumakerDaysToComplete, "tweak_gauranlenGaumakerDaysToComplete", 4);
            Scribe_Values.Look(ref tweak_gauranlenPodHarvestYield, "tweak_gauranlenPodHarvestYield", 1);

            // Biotech
            Scribe_Values.Look(ref tweak_poluxTweaks, "tweak_poluxTweaks", false);
            Scribe_Values.Look(ref tweak_replantablePolux, "tweak_replantablePolux", false);
            Scribe_Values.Look(ref tweak_flattenComplexity, "tweak_flattenComplexity", false);
            Scribe_Values.Look(ref tweak_flattenMetabolism, "tweak_flattenMetabolism", false);
            Scribe_Values.Look(ref tweak_flattenArchites, "tweak_flattenArchites", false);
            Scribe_Values.Look(ref tweak_showGenesTab, "tweak_showGenesTab", false);
            Scribe_Values.Look(ref tweak_playerMechTweaks, "tweak_playerMechTweaks", false);
            Scribe_Values.Look(ref tweak_mechTweaksAffectMods, "tweak_mechTweaksAffectMods", false);
            Scribe_Values.Look(ref tweak_mechanoidSkillLevel, "tweak_mechanoidSkillLevel", 10f);
            Scribe_Values.Look(ref tweak_mechanoidRechargeRate, "tweak_mechanoidRechargeRate", 1f);
            Scribe_Values.Look(ref tweak_mechanoidDrainRate, "tweak_mechanoidDrainRate", 1f);
            Scribe_Values.Look(ref tweak_mechanoidWorkSpeed, "tweak_mechanoidWorkSpeed", 0.2f);
            Scribe_Values.Look(ref tweak_mechanitorTweaks, "tweak_mechanitorTweaks", false);
            Scribe_Values.Look(ref tweak_mechanitorDisableRange, "tweak_mechanitorDisableRange", false);
            Scribe_Values.Look(ref tweak_mechanitorRangeMulti, "tweak_mechanitorRangeMulti", 1f);
            Scribe_Values.Look(ref tweak_mechanitorBandwidthBase, "tweak_mechanitorBandwidthBase", 6f);
            Scribe_Values.Look(ref tweak_mechanitorControlGroupBase, "tweak_mechanitorControlGroupBase", 2f);
            Scribe_Values.Look(ref tweak_mechanitorBandNodeBandwidth, "tweak_mechanitorBandNodeBandwidth", 1f);
            Scribe_Values.Look(ref tweak_poluxEffectRadius, "tweak_poluxEffectRadius", 7.9f);
            Scribe_Values.Look(ref tweak_primitiveVasectomy, "tweak_primitiveVasectomy", false);
            Scribe_Values.Look(ref tweak_pregnancyLengthMultiplier, "tweak_pregnancyLengthMultiplier", 1.0f);
            Scribe_Values.Look(ref tweak_poluxEffectRate, "tweak_poluxEffectRate", 1.0f);
            Scribe_Values.Look(ref tweak_poluxArtificialDisables, "tweak_poluxArtificialDisables", false);
            Scribe_Values.Look(ref tweak_defaultPregnancyChance, "tweak_defaultPregnancyChance", 0.03f);

            // Genepacks
            Scribe_Values.Look(ref tweak_genepackTweaks, "tweak_genepackTweaks", false);
            Scribe_Collections.Look(ref genepacksEnabled, "genepacksEnabled");
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
