using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;

namespace TweaksGalore
{
	[HarmonyPriority(800)]
	[HarmonyPatch(typeof(PawnGenerator), "GenerateTraits", null)]
	public static class Patch_PawnGenerator_GenerateTraits
	{
		[HarmonyPriority(700)]
		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			List<CodeInstruction> list = new List<CodeInstruction>(instructions);
			MethodInfo methodInfo = AccessTools.Method(typeof(Rand), "RangeInclusive", null, null);
			MethodInfo operand = AccessTools.Method(typeof(Patch_PawnGenerator_GenerateTraits), "GetRandomTraitCount", null, null);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].opcode == OpCodes.Call && list[i].operand == methodInfo)
				{
					list[i].operand = operand;
					break;
				}
			}
			return list;
		}

		private static int GetRandomTraitCount(int min, int max)
		{
			return Rand.RangeInclusive(TweaksGaloreMod.settings.tweak_traitCountRange.min, TweaksGaloreMod.settings.tweak_traitCountRange.max);
		}
    }
}
