using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace TweaksGalore
{
	[HarmonyPatch(typeof(LandingOutcomeWorker_GravNausea), "ApplyOutcome")]
	public class Patch_LandingOutcomeWorker_GravNausea_ApplyOutcome
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue && TGTweakDefOf.Tweak_GravshipLandingOutcome_GravNausea.BoolValue)
            {
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(LandingOutcomeWorker_MinorGravshipCrash), "ApplyOutcome")]
    public class Patch_LandingOutcomeWorker_MinorGravshipCrash_ApplyOutcome
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue && TGTweakDefOf.Tweak_GravshipLandingOutcome_MinorGravshipCrash.BoolValue)
            {
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(LandingOutcomeWorker_OverheatedGravEngine), "ApplyOutcome")]
    public class Patch_LandingOutcomeWorker_OverheatedGravEngine_ApplyOutcome
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue && (TGTweakDefOf.Tweak_GravshipLandingOutcome_OverheatedGravEngine.BoolValue || TGTweakDefOf.Tweak_Gravship_DisableCooldown.BoolValue))
            {
                return false;
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(LandingOutcomeWorker_ThrusterBreakdown), "ApplyOutcome")]
    public class Patch_LandingOutcomeWorker_ThrusterBreakdown_ApplyOutcome
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue && TGTweakDefOf.Tweak_GravshipLandingOutcome_ThrusterBreakdown.BoolValue)
            {
                return false;
            }
            return true;
        }
    }
}
