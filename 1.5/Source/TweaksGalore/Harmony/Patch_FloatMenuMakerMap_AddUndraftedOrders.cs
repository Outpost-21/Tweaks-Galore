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
	[HarmonyPatch(typeof(FloatMenuMakerMap), "AddJobGiverWorkOrders")]
	public static class Patch_FloatMenuMakerMap_AddUndraftedOrders
	{
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				for (int i = 0; i < list.Count; i++)
				{
					string text = list[i].operand as string;
					if (text == "CannotPrioritizeWorkGiverDisabled")
					{
						list[i].operand = "TweaksGalore.CannotPrioritizeWorkGiverDisabled";
					}
					if (text == "CannotPrioritizeNotAssignedToWorkType")
					{
						list[i].operand = "TweaksGalore.CannotPrioritizeNotAssignedToWorkType";
					}
				}
			}
			return list;
		}
	}
}
