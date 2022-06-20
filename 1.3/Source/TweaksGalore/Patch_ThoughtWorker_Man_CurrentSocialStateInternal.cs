using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using HarmonyLib;

namespace TweaksGalore
{
	[HarmonyPatch(typeof(ThoughtWorker_Man), "CurrentSocialStateInternal")]
	public static class Patch_ThoughtWorker_Man_CurrentSocialStateInternal
	{
		[HarmonyPrefix]
		public static bool Prefix(Pawn p, Pawn other, ThoughtState __result)
		{
			if (TweaksGaloreMod.settings.tweak_misanthropeTrait)
			{
				TraitDef dislikesHumanity = DefDatabase<TraitDef>.GetNamed("DislikesHumanity");
				if (!p.story.traits.HasTrait(dislikesHumanity))
				{
					__result = ThoughtState.Inactive;
					return false;
				}
			}
			return true;
		}
	}
}
