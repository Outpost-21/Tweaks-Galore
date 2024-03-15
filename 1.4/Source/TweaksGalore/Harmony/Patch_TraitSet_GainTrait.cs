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
	[HarmonyPatch(typeof(TraitSet), "GainTrait")]
	public static class Patch_TraitSet_GainTrait
	{
		[HarmonyPrefix]
		public static bool Prefix(TraitSet __instance, Trait trait)
		{
			Pawn pawn = (Pawn)AccessTools.DeclaredField(typeof(TraitSet), "pawn").GetValue(__instance);

			if (TweaksGaloreMod.settings.tweak_misanthropeTrait)
			{
				TraitDef dislikesHumanity = DefDatabase<TraitDef>.GetNamed("DislikesHumanity");
				if (pawn.story.traits.HasTrait(TraitDefOf.DislikesMen) && trait.def == TraitDefOf.DislikesWomen)
				{
					pawn.story.traits.GainTrait(new Trait(dislikesHumanity));
					return false;
				}
				else if (pawn.story.traits.HasTrait(TraitDefOf.DislikesWomen) && trait.def == TraitDefOf.DislikesMen)
				{
					pawn.story.traits.GainTrait(new Trait(dislikesHumanity));
					return false;
				}

				if (pawn.story.traits.HasTrait(dislikesHumanity) && (trait.def == TraitDefOf.DislikesWomen || trait.def == TraitDefOf.DislikesMen))
				{
					return false;
                }
			}
			return true;
		}
	}
}
