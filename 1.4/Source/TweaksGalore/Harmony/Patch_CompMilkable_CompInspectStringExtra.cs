using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;

namespace TweaksGalore
{
	[HarmonyPatch(typeof(CompMilkable))]
	[HarmonyPatch("CompInspectStringExtra")]
	public static class Patch_CompMilkable_CompInspectStringExtra
	{
		[HarmonyPostfix]
		public static void Postfix(ref string __result, CompMilkable __instance)
		{
            if (TGTweakDefOf.Tweak_AnimalResourceLabel.BoolValue)
            {
				int ticks = Mathf.RoundToInt((1f - __instance.Fullness) * 60000f * __instance.GatherResourcesIntervalDays);
				__result += $"({ticks.ToStringTicksToPeriod()} => {__instance.ResourceAmount} {__instance.ResourceDef.label})";
            }
		}
	}
}
