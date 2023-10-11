using HarmonyLib;
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
	[HarmonyPatch(typeof(Pawn), "WorkTagIsDisabled")]
	public static class Patch_Pawn_WorkTagIsDisabled
	{
		public static bool Prefix(WorkTags w, Pawn __instance)
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				return !__instance.NonHumanlikeOrWildMan() && w == WorkTags.Violent && !HasWeapon(__instance);
			}
			return true;
		}

		public static bool HasWeapon(Pawn pawn)
		{
			if (pawn.equipment?.Primary == null)
			{
				return false;
			}
			ThingDef def = pawn.equipment.Primary.def;
			if (!def.IsWeapon)
			{
				return false;
			}
			return true;
		}
	}
}
