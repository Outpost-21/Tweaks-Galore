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
	[HarmonyPatch(typeof(SkillRecord), "LearningSaturatedToday", MethodType.Getter)]
	public static class Patch_SkillRecord_LearningSaturatedToday
	{
		[HarmonyPrefix]
		public static bool Prefix(SkillRecord __instance, ref bool __result)
		{
			int tweakValue = TGTweakDefOf.Tweak_SkillLearningPerDay.IntValue;
			if (tweakValue != 4000)
            {
				__result = __instance.xpSinceMidnight > tweakValue;
				return false;
            }
			return true;
		}
	}
}
