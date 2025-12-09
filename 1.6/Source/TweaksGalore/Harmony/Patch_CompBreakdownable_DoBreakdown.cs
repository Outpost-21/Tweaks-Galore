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
    // Fuck this shit, tired of coming back to exclude shittily coded buildings, goodbye breakdowns.

	[HarmonyPatch(typeof(CompBreakdownable), "CanBreakdownNow")]
	public static class Patch_CompBreakdownable_CanBreakdownNow
    {
		[HarmonyPrefix]
		public static bool Prefix(bool __result)
		{
            if (TGTweakDefOf.Tweak_NoBreakdowns.BoolValue)
            {
                __result = false;
                return false;
            }
			return true;
		}
    }

    [HarmonyPatch(typeof(CompBreakdownable), "DoBreakdown")]
    public static class Patch_CompBreakdownable_DoBreakdown
    {
        [HarmonyPrefix]
        public static bool Prefix()
        {
            return !TGTweakDefOf.Tweak_NoBreakdowns.BoolValue;
        }
    }
}
