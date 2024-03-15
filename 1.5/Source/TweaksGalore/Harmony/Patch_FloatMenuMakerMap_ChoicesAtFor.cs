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
	[HarmonyPatch(typeof(FloatMenuMakerMap), "ChoicesAtFor")]
	public static class Patch_FloatMenuMakerMap_ChoicesAtFor
	{
		public static bool skip;

		[HarmonyPriority(100)]
		public static void Prefix()
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				skip = true;
			}
		}

		[HarmonyPriority(700)]
		public static void Postfix()
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				skip = false;
			}
		}
	}
}
