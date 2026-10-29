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
	[HarmonyPatch(typeof(PawnRelationWorker), "BaseGenerationChanceFactor")]
	public class Patch_PawnRelationWorker_BaseGenerationChanceFactor
    {
		[HarmonyPostfix]
		public static void Postfix(ref float __result)
		{
			if (__result > 0 && TGTweakDefOf.Tweak_DontGenerateRelations.BoolValue)
			{
				__result = 0f;
			}
		}
	}
}
