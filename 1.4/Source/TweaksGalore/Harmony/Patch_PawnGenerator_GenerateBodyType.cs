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
	[HarmonyPatch(typeof(PawnGenerator), "GenerateBodyType")]
	public class Patch_PawnGenerator_GenerateBodyType
    {
        [HarmonyPostfix]
        public static void Postfix(Pawn pawn)
        {
            if (TweaksGaloreMod.settings.patch_slimRim)
            {
                if (pawn.def == ThingDefOf.Human &&
                    ((pawn.story.bodyType == BodyTypeDefOf.Thin && TweaksGaloreMod.settings.patch_slimRim_thin) ||
                    (pawn.story.bodyType == BodyTypeDefOf.Fat && TweaksGaloreMod.settings.patch_slimRim_fat) ||
                    (pawn.story.bodyType == BodyTypeDefOf.Hulk && TweaksGaloreMod.settings.patch_slimRim_hulk)))
                {
                    pawn.story.bodyType = (pawn.gender == Gender.Female) ? BodyTypeDefOf.Female : BodyTypeDefOf.Male;
                }
            }
        }
    }
}
