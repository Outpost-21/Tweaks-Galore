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
	[HarmonyPatch(typeof(ITab_Genes), "Hidden", MethodType.Getter)]
	public class Patch_ITab_Genes_Hidden
	{
		[HarmonyPrefix]
		public static bool Prefix(ITab_Genes __instance, ref bool __result)
		{
			if (TweaksGaloreMod.settings.tweak_showGenesTab)
            {
				__result = false;
				return false;
            }
			return true;
		}
	}

	[HarmonyPatch(typeof(ITab_GenesPregnancy), "IsVisible", MethodType.Getter)]
	public class Patch_ITab_GenesPregnancy_Hidden
	{
		[HarmonyPrefix]
		public static bool Prefix(ITab_Genes __instance, ref bool __result)
		{
			if (TweaksGaloreMod.settings.tweak_showGenesTab)
			{
				__result = false;
				return false;
			}
			return true;
		}
	}
}
