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

        public static TweakDef Tweak_FasterSmoothingFactor;
        public static TweakDef Tweak_AnimalResourceLabel;
        public static TweakDef Tweak_DisableSpecialSkinShader;
        public static TweakDef Tweak_PowerAdjusting;
        public static TweakDef Tweak_StackableChunks_Stone;
        public static TweakDef Tweak_StackableChunks_Slag;
        public static TweakDef Tweak_WaitThisIsBetter;
        public static TweakDef Tweak_UninstallableMechShields;
        public static TweakDef Tweak_IncidentPawnStats;
        public static TweakDef Tweak_InfestationBlockingFloors;
        public static TweakDef Tweak_HiddenWires;
        public static TweakDef Tweak_NextRestockTimer;
        public static TweakDef Tweak_SkillLearningPerDay;
        public static TweakDef Tweak_MechanoidAdaptation;
        public static TweakDef Tweak_LowerPrisonerExpectations;
        public static TweakDef Tweak_MisanthropeTrait;
        public static TweakDef Tweak_DisableLethalDamageThreshold;
        public static TweakDef Tweak_TraderPawnAlert;
        public static TweakDef Tweak_OrbitalTraderAlert;

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
            Tweak_DisableAnimaScream;

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

        [MayRequireAnomaly]
        public static TweakDef 
            Tweak_MakeStoneSarcophagiUnescapable;
    }
}
