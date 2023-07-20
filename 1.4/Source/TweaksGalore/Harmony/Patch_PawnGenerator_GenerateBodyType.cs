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
            if (TGTweakDefOf.Tweak_SlimRim.BoolValue)
            {
                if (pawn.def == ThingDefOf.Human &&
                    ((pawn.story.bodyType == BodyTypeDefOf.Thin && TGTweakDefOf.Tweak_SlimRimThin.BoolValue) ||
                    (pawn.story.bodyType == BodyTypeDefOf.Fat && TGTweakDefOf.Tweak_SlimRimFat.BoolValue) ||
                    (pawn.story.bodyType == BodyTypeDefOf.Hulk && TGTweakDefOf.Tweak_SlimRimHulk.BoolValue)))
                {
                    pawn.story.bodyType = (pawn.gender == Gender.Female) ? BodyTypeDefOf.Female : BodyTypeDefOf.Male;
                }
            }
        }
    }
}
