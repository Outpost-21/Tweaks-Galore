using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
	[HarmonyPatch(typeof(WITab_Caravan_Gear), "TryEquipDraggedItem")]
	public static class WITab_Caravan_Gear_TryEquipDraggedItem
	{
		[HarmonyTranspiler]
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				CodeMatcher codeMatcher = new CodeMatcher(instructions);
				int pos = codeMatcher.MatchForward(false, new CodeMatch(OpCodes.Ldstr, "MessageCantEquipIncapableOfViolence")).Pos;
				int pos2 = codeMatcher.MatchForward(false, new CodeMatch(OpCodes.Ret)).Pos;
				return codeMatcher.RemoveInstructionsInRange(pos, pos2).Instructions();
			}
			return instructions;
		}
	}
}
