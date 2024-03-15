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
	[HarmonyPatch(typeof(FloatMenuUtility), "GetMeleeAttackAction")]
	public static class Patch_FloatMenuUtility_GetMeleeAttackAction
	{
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				List<CodeInstruction> list = new List<CodeInstruction>(instructions);
				for (int i = 0; i < list.Count; i++)
				{
					string text = list[i].operand as string;
					if (text != null && text == "IsIncapableOfViolenceLower")
					{
						list[i].operand = "TweaksGalore.IsIncapableOfViolenceLower";
					}
				}
				return list;
			}
			return instructions;
		}
	}
}
