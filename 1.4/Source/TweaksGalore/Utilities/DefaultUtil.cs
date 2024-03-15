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
    public static class DefaultUtil
    {
        public static void RestoreALL(TweaksGaloreSettings s)
        {
            RestoreSettings_Vanilla(s);
            RestoreSettings_Mechanoids(s);
            RestoreSettings_PennedAnimals(s);
            RestoreSettings_Power(s);
            RestoreSettings_Raids(s);
            RestoreSettings_Resources(s);
            RestoreSettings_Royalty(s);
            RestoreSettings_RoyalTitles(s);
            RestoreSettings_RoyalPermits(s);
            RestoreSettings_Anima(s);
            RestoreSettings_Ideology(s);
            RestoreSettings_Gauranlen(s);
            RestoreSettings_Biotech(s);
            RestoreSettings_Polux(s);
            RestoreSettings_Genepacks(s);
        }

        public static void RestoreSettings_Vanilla(TweaksGaloreSettings s)
        {
            s.tweak_insultingSpreeNerf = false;
            s.tweak_lagFreeLamps = false;
            s.tweak_fasterSmoothing = false;
            s.tweak_fasterSmoothing_factor = 3f;
            s.tweak_destroyAnything = false;
            s.tweak_skilledStonecutting = false;
            s.tweak_dontPackFood = false;
            s.tweak_preReleaseShipParts = false;
            s.tweak_fixDeconstructionReturn = false;
            s.tweak_noBreakdowns = false;
            s.tweak_impassableDeepWater = false;
            s.tweak_oldMegaslothName = false;
            s.tweak_healrootToXerigium = false;
            s.tweak_glowingAmbrosia = false;
            s.tweak_glowingHealroot = false;
            s.tweak_hunterMelee = false;
            s.tweak_hunterMelee_allowSimpleSidearms = false;
            s.tweak_hunterMelee_fistFighting = false;
            s.tweak_boomalopesBleedChemfuel = false;
            s.tweak_chattyComms = false;
            s.tweak_notSoWildBerries = false;
            s.patch_settlementTraderTimer = false;
            s.patch_incidentPawnStats = false;
            s.patch_prisonersDontHaveKeys = false;
            s.patch_pdhk_ownDoor = true;
            s.patch_pdhk_prisoners = true;
            s.patch_pdhk_slaves = true;
            s.patch_strongFloorsStopInfestations = false;
            s.patch_slimRim = false;
            s.patch_slimRim_fat = false;
            s.patch_slimRim_hulk = false;
            s.patch_slimRim_thin = false;
            s.tweak_noFriendShapedManhunters = false;
            s.tweak_NFSMTrainability_Intermediate = false;
            s.tweak_NFSMTrainability_Advanced = false;
            s.tweak_NFSMNuzzleHours = false;
            s.tweak_NFSMWildness = 0.65f;
            s.tweak_NFSMCombatPower = 60f;
            s.tweak_NFSMDisableManhunterOnTame = false;
            s.tweak_traitCountAdjustment = false;
            s.tweak_traitCountRange = new IntRange(3, 3);
            s.tweak_skillRates = false;
            s.tweak_skillRateLoss = 1f;
            s.tweak_skillRateGain = 1f;
            s.tweak_skillRateLossThreshold = 0f;
            s.tweak_disableNarrowHeads = false;
            s.tweak_growableAmbrosia = false;
            s.tweak_growableGrass = false;
            s.tweak_growableMushrooms = false;
            s.tweak_hiddenConduits = false;
        }

        public static void RestoreSettings_Mechanoids(TweaksGaloreSettings s)
        {
            s.tweak_mechanoidHeatArmour = 2f;
            s.patch_disableMechanoidAdapting = false;
            s.tweak_betterGloomlight = false;
            s.tweak_gloomlightSunlamp = false;
            s.tweak_gloomlightDarklight = false;
        }

        public static void RestoreSettings_PennedAnimals(TweaksGaloreSettings s)
        {
            s.restorePennedAnimals = true;
            s.tweak_pennedAnimalConfig = false;
        }

        public static void RestoreSettings_Power(TweaksGaloreSettings s)
        {
            s.tweak_powerUsageTweaks = false;
            s.tweak_powerUsage_lamp = 30f;
            s.tweak_powerUsage_sunlamp = 2900f;
            s.tweak_powerUsage_autodoor = 50f;
            s.tweak_powerUsage_vanometricCell = 1000f;
        }

        public static void RestoreSettings_Raids(TweaksGaloreSettings s)
        {
            s.tweak_noMoreBreachRaids = false;
            s.tweak_noMoreDropPodRaids = false;
            s.tweak_noMoreSapperRaids = false;
            s.tweak_noMoreSiegeRaids = false;
            s.tweak_noCowardlyRaiders = false;
        }

        public static void RestoreSettings_Resources(TweaksGaloreSettings s)
        {
            s.tweak_strongerSteel = false;
            s.tweak_noMineablePlasteel = false;
            s.tweak_noMineableComponents = false;
            s.tweak_prettyPreciousMetals = false;
            s.tweak_metalDoesntBurn = false;
            s.tweak_oldComponentNames = false;
            s.tweak_stackableChunks = false;
            s.tweak_stackableChunks_stone = 5f;
            s.tweak_stackableChunks_slag = 5f;
        }

        public static void RestoreSettings_Royalty(TweaksGaloreSettings s)
        {
            s.tweak_delayedRoyalty = false;
            s.tweak_uninstallableMechShields = false;
            s.tweak_animaMeditationAll = false;
            s.tweak_animaRemoveBackstoryLimits = false;
        }

        public static void RestoreSettings_Anima(TweaksGaloreSettings s)
        {
            s.tweak_animaTweaks = false;
            s.tweak_replantableAnima = false;
            s.tweak_animaDisableScream = false;
            s.tweak_animaScreamDebuff = -6f;
            s.tweak_animaScreamLength = 5f;
            s.tweak_animaScreamStackLimit = 3f;
            s.tweak_animaArtificialBuildingRadius = 34.9f;
            s.tweak_animaShrineBuildingRadius = 34.9f;
            s.tweak_animaBuffBuildingRadius = 9.9f;
            s.tweak_animaShrineBuffBuildingRadius = 9.9f;
            s.tweak_animaMaxBuffBuildings = 4f;
            s.tweak_animaPsylinkLevelNeeds = new List<int>();
            s.tweak_animaBuildableShrines = false;
            s.tweak_animaMeditationGain = 0.5f;
        }

        public static void RestoreSettings_RoyalTitles(TweaksGaloreSettings s)
        {
            s.tweak_royalTitleSettings = s.royalTitleSettingsDefaults;
        }

        public static void RestoreSettings_RoyalPermits(TweaksGaloreSettings s)
        {
            s.tweak_royalPermitSettings = s.royalPermitSettingsDefaults;
        }

        public static void RestoreSettings_Ideology(TweaksGaloreSettings s)
        {
            s.tweak_ancientDeconstruction = false;
            s.tweak_ancientDeconstruction_mode = false;
            s.tweak_unlockedIdeologyBuildings = false;
            s.tweak_darklightGlowPods = false;
            s.tweak_disableDesiredApparel = false;
            s.patch_properSuppression = false;
            s.patch_properSuppressionPercentage = 0.7f;
            s.patch_noMemeLimit = false;
        }

        public static void RestoreSettings_Gauranlen(TweaksGaloreSettings s)
        {
            s.tweak_gauranlenTweaks = false;
            s.tweak_replantableGauranlen = false;
            s.tweak_gauranlenInitialConnectionStrength = new FloatRange(0.25f, 0.45f);
            s.tweak_gauranlenConnectionGainPerHourPruning = 0.01f;
            s.tweak_gauranlenDryadSpawnDays = 8f;
            s.tweak_gauranlenMaxDryads = 4f;
            s.tweak_gauranlenArtificialBuildingRadius = 7.9f;
            s.tweak_gauranlenConnectionLossPerLevel = 1f;
            s.tweak_gauranlenLossPerBuilding = 1f;
            s.tweak_gauranlenPlantGrowthRadius = 7.9f;
            s.tweak_gauranlenMossGrowDays = 5;
            s.tweak_gauranlenCocoonDaysToComplete = 6;
            s.tweak_gauranlenGaumakerDaysToComplete = 4;
            s.tweak_gauranlenPodHarvestYield = 1;
        }

        public static void RestoreSettings_Biotech(TweaksGaloreSettings s)
        {
            s.tweak_flattenComplexity = false;
            s.tweak_flattenMetabolism = false;
            s.tweak_flattenArchites = false;
            s.tweak_showGenesTab = false;
            s.tweak_playerMechTweaks = false;
            s.tweak_mechTweaksAffectMods = false;
            s.tweak_mechanoidSkillLevel = 10f;
            s.tweak_mechanoidRechargeRate = 1.0f;
            s.tweak_mechanoidDrainRate = 1.0f;
            s.tweak_mechanoidWorkSpeed = 0.2f;
            s.tweak_mechanitorTweaks = false;
            s.tweak_mechanitorDisableRange = false;
            s.tweak_mechanitorRangeMulti = 1.0f;
            s.tweak_mechanitorBandwidthBase = 6f;
            s.tweak_mechanitorControlGroupBase = 2f;
            s.tweak_mechanitorBandNodeBandwidth = 1f;
            s.tweak_primitiveVasectomy = false;
            s.tweak_pregnancyLengthMultiplier = 1.0f;
            s.tweak_defaultPregnancyChance = 0.03f;
        }

        public static void RestoreSettings_Polux(TweaksGaloreSettings s)
        {
            s.tweak_poluxTweaks = false;
            s.tweak_replantablePolux = false;
            s.tweak_poluxEffectRadius = 7.9f;
            s.tweak_poluxEffectRate = 1.0f;
            s.tweak_poluxArtificialDisables = false;
        }

        public static void RestoreSettings_Genepacks(TweaksGaloreSettings s)
        {
            s.genepacksEnabled = s.defaultGenepacksEnabled;
        }
    }
}
