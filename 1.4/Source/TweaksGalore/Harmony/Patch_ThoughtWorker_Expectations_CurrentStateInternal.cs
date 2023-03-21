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
	[HarmonyPatch(typeof(ThoughtWorker_Expectations))]
	[HarmonyPatch("CurrentStateInternal")]
	public class Patch_ThoughtWorker_Expectations_CurrentStateInternal
	{
		[HarmonyPostfix]
		public static ThoughtState Postfix(ThoughtState __result, Pawn p)
		{
            if (TweaksGaloreMod.settings.patch_lowPrisonerExpectations)
			{
				return p.IsPrisoner ? ThoughtState.ActiveAtStage(Math.Min(__result.StageIndex + 3, 5)) : __result;
			}
			return __result;
		}
	}
}
