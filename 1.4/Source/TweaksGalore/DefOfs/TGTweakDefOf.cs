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

        public static TweakDef Tweak_SkillTweaks;
        public static TweakDef Tweak_SkillLossMultiplier;
        public static TweakDef Tweak_SkillGainMultiplier;
        public static TweakDef Tweak_SkillLossThreshold;
        public static TweakDef Tweak_NotMyBestWork;
        public static TweakDef Tweak_NotMyBestwork_BaseLevelCap;
        public static TweakDef Tweak_NotMyBestwork_MinorPassionCap;
        public static TweakDef Tweak_NotMyBestwork_MajorPassionCap;
        public static TweakDef Tweak_DisableSpecialSkinShader;
        public static TweakDef Tweak_NFSM_PreventByTrainability;
        public static TweakDef Tweak_NFSM_PreventByNuzzleable;
        public static TweakDef Tweak_NFSM_PreventByWildness;
        public static TweakDef Tweak_NFSM_PreventByCombatPower;
        public static TweakDef Tweak_NFSM_PreventWhenTaming;
        public static TweakDef Tweak_PowerAdjusting;
        public static TweakDef Tweak_StackableChunks_Stone;
        public static TweakDef Tweak_StackableChunks_Slag;
        public static TweakDef Tweak_MechanitorTweaks;
        public static TweakDef Tweak_MechanitorDisableRange;
        public static TweakDef Tweak_MechanitorBandwidthBase;
        public static TweakDef Tweak_MechanitorControlGroupBase;
        public static TweakDef Tweak_BandwidthPerBandNode;
        public static TweakDef Tweak_WaitThisIsBetter;
        public static TweakDef Tweak_PrisonersDontHaveKeys;
        public static TweakDef Tweak_PDHKPrisoners;
        public static TweakDef Tweak_PDHKSlaves;
        public static TweakDef Tweak_PDHKOwnDoor;
        public static TweakDef Tweak_ProperSuppression;
        public static TweakDef Tweak_UninstallableMechShields;
        public static TweakDef Tweak_IncidentPawnStats;
        public static TweakDef Tweak_NoMemeLimit;
        public static TweakDef Tweak_InfestationBlockingFloors;
        public static TweakDef Tweak_ShowGenesTab;
        public static TweakDef Tweak_SlimRim;
        public static TweakDef Tweak_SlimRimFat;
        public static TweakDef Tweak_SlimRimHulk;
        public static TweakDef Tweak_SlimRimThin;
        public static TweakDef Tweak_TraitCountAdjustment;
        public static TweakDef Tweak_TraitCountRange;
        public static TweakDef Tweak_HiddenWires;
        public static TweakDef Tweak_NextRestockTimer;
        public static TweakDef Tweak_SkillLearningPerDay;
        public static TweakDef Tweak_MechanoidAdaptation;
        public static TweakDef Tweak_LowerPrisonerExpectations;
        public static TweakDef Tweak_MisanthropeTrait;
        public static TweakDef Tweak_HuntersCanMelee;
        public static TweakDef Tweak_HuntersCanMeleeFisting;
        public static TweakDef Tweak_AnimaTweaks;
        public static TweakDef Tweak_DisableAnimaScream;
        public static TweakDef Tweak_TaintDisabled;
        public static TweakDef Tweak_TaintOnRot;
        public static TweakDef Tweak_OnlyTaintFirstLayer;
        public static TweakDef Tweak_GauranlenTweaks;
        public static TweakDef Tweak_GauranlenConnectionLossBuildings;
        public static TweakDef Tweak_PoluxTweaks;
    }
}
