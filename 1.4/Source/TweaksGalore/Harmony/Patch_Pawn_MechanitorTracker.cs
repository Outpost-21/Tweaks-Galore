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
	[HarmonyPatch(typeof(Pawn_MechanitorTracker), "DrawCommandRadius")]
	public class Patch_Pawn_MechanitorTracker_DrawCommandRadius
	{
		[HarmonyPrefix]
		public static bool Prefix()
		{
			if(TweaksGaloreMod.settings.tweak_mechanitorTweaks && TweaksGaloreMod.settings.tweak_mechanitorDisableRange)
            {
				// No need to do anything, just disabling the drawing.
				return false;
            }
			return true;
		}
	}

	[HarmonyPatch(typeof(Pawn_MechanitorTracker), "CanCommandTo")]
	public class Patch_Pawn_MechanitorTracker_CanCommandTo
	{
		[HarmonyPrefix]
		public static bool Prefix(Pawn_MechanitorTracker __instance, ref LocalTargetInfo target, ref bool __result)
		{
			if (TweaksGaloreMod.settings.tweak_mechanitorTweaks && TweaksGaloreMod.settings.tweak_mechanitorDisableRange)
			{
				if (!target.Cell.InBounds(__instance.pawn.MapHeld))
				{
					__result = false;
					return false;
				}
				__result = true;
				return false;
			}
			return true;
		}
	}
}
