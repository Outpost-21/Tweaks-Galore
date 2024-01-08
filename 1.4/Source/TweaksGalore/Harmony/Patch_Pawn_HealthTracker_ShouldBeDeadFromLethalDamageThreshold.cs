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
    [HarmonyPatch(typeof(Pawn_HealthTracker), "ShouldBeDeadFromLethalDamageThreshold")]
    public class Patch_Pawn_HealthTracker_ShouldBeDeadFromLethalDamageThreshold
	{
        [HarmonyPostfix]
        public static void Postfix(ref bool __result)
		{
            if (TGTweakDefOf.Tweak_DisableLethalDamageThreshold.BoolValue)
            {
                __result = false;
            }
		}
	}
}