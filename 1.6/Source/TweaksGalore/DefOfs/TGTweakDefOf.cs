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
    [DefOf]
    public static class TGTweakDefOf
    {
        static TGTweakDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TGTweakDefOf));
        }

        public static TweakCategoryDef TweakCategory_Vanilla;

        public static TweakSubSectionDef TweakSubSection_PowerAdjusting;

        public static TweakDef 
            Tweak_FasterSmoothingFactor,
            Tweak_AnimalResourceLabel,
            Tweak_DisableSpecialSkinShader,
            Tweak_PowerAdjusting,
            Tweak_StackableChunks_Stone,
            Tweak_StackableChunks_Slag,
            Tweak_IncidentPawnStats,
            Tweak_InfestationBlockingFloors,
            Tweak_HiddenWires,
            Tweak_NextRestockTimer,
            Tweak_SkillLearningPerDay,
            Tweak_MechanoidAdaptation,
            Tweak_LowerPrisonerExpectations,
            Tweak_DisableLethalDamageThreshold,
            Tweak_TraderPawnAlert,
            Tweak_OrbitalTraderAlert,
            Tweak_DontGenerateRelations,
            Tweak_TradeableMeals,
            Tweak_PinataDeathAcidifiers;

        public static TweakDef
            Tweak_TechTraversal_Enabled,
            Tweak_TechTraversal_AlwaysLowestLevel,
            Tweak_TechTraversal_ShowTechCounter,
            //Tweak_TechTraversal_CostIncreasePerTechLevel,
            //Tweak_TechTraversal_CostDecreasePerTechLevel,
            Tweak_TechTraversal_PercentageNeeded,
            Tweak_TechTraversal_IgnoreTechprints,
            Tweak_TechTraversal_OnlyVanillaResearch;

        public static TweakDef 
            Tweak_NFSM_PreventByTrainability,
            Tweak_NFSM_PreventByNuzzleable,
            Tweak_NFSM_PreventByWildness,
            Tweak_NFSM_PreventByCombatPower,
            Tweak_NFSM_PreventWhenTaming;

        public static TweakDef 
            Tweak_PrisonersDontHaveKeys,
            Tweak_PDHKPrisoners,
            Tweak_PDHKSlaves,
            Tweak_PDHKOwnDoor;

        public static TweakDef 
            Tweak_TraitCountAdjustment,
            Tweak_TraitCountRange;

        public static TweakDef 
            Tweak_SlimRim,
            Tweak_SlimRimFat,
            Tweak_SlimRimHulk,
            Tweak_SlimRimThin;

        public static TweakDef 
            Tweak_HuntersCanMelee,
            Tweak_HuntersCanMeleeFisting;

        public static TweakDef
            Tweak_TaintDisabled,
            Tweak_TaintOnRot,
            Tweak_OnlyTaintFirstLayer;

        public static TweakDef 
            Tweak_DynamicPopulation_CriticallyLow,
            Tweak_DynamicPopulation_Low,
            Tweak_DynamicPopulation_Typical,
            Tweak_DynamicPopulation_High,
            Tweak_DynamicPopulation_CriticallyHigh,
            Tweak_DynamicPopulation_Maximum;

        public static TweakDef 
            Tweak_SkillTweaks,
            Tweak_SkillLossMultiplier,
            Tweak_SkillGainMultiplier,
            Tweak_SkillLossThreshold;

        [MayRequireRoyalty]
        public static TweakDef
            Tweak_AnimaTweaks,
            Tweak_DisableAnimaScream,
            Tweak_UninstallableMechShields,
            Tweak_WaitThisIsBetter;

        [MayRequireIdeology]
        public static TweakDef 
            Tweak_ProperSuppression,
            Tweak_SuppressionThreshold,
            Tweak_NoMemeLimit,
            Tweak_GauranlenTweaks,
            Tweak_GauranlenConnectionGain,
            Tweak_GauranlenConnectionLossBuildings;

        [MayRequireBiotech]
        public static TweakDef 
            Tweak_MechSkillLevel,
            Tweak_MechDischargeRate,
            Tweak_SpawnPregnancyChance,
            Tweak_MechanitorTweaks,
            Tweak_MechanitorDisableRange,
            Tweak_MechanitorBandwidthBase,
            Tweak_MechanitorControlGroupBase,
            Tweak_BandwidthPerBandNode,
            Tweak_ShowGenesTab,
            Tweak_PoluxTweaks;

        [MayRequireOdyssey]
        public static TweakDef
            Tweak_GravshipTweaks,
            Tweak_Gravship_DisableCooldown,
            Tweak_GravshipLandingOutcome_GravNausea,
            Tweak_GravshipLandingOutcome_MinorGravshipCrash,
            Tweak_GravshipLandingOutcome_OverheatedGravEngine,
            Tweak_GravshipLandingOutcome_ThrusterBreakdown,
            Tweak_ShuttleTweaks;
    }
}
