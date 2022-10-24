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
	[HarmonyPatch(typeof(Hediff_BandNode), "CurStage", MethodType.Getter)]
	public class Patch_Hediff_BandNode_CurStage
	{
		[HarmonyPrefix]
		public static bool Prefix(Hediff_BandNode __instance, ref HediffStage __result)
		{
			int tweakBandwidth = Mathf.RoundToInt(TweaksGaloreMod.settings.tweak_mechanitorBandNodeBandwidth);
			if (tweakBandwidth > 1)
            {
				if(__instance.curStage == null && __instance.cachedTunedBandNodesCount > 0)
                {
					StatModifier statModifier = new StatModifier();
					statModifier.stat = StatDefOf.MechBandwidth;
					statModifier.value = __instance.cachedTunedBandNodesCount * tweakBandwidth;
					__instance.curStage = new HediffStage();
					__instance.curStage.statOffsets = new List<StatModifier> { statModifier };
                }
				__result = __instance.curStage;
				return false;
            }
			return true;
		}
	}
}
