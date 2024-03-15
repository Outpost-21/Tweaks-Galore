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
    [HarmonyPatch(typeof(PawnGenerator), "GenerateTraits", null)]
    public static class Patch_PawnGenerator_GenerateTraits
    {
        public static int targetCount = -1;

        [HarmonyPostfix]
        public static void Postfix(Pawn pawn, PawnGenerationRequest request)
        {
            if (TweaksGaloreMod.settings.tweak_traitCountAdjustment)
            {
                if (pawn.story == null || request.AllowedDevelopmentalStages.Newborn() || pawn.DevelopmentalStage != DevelopmentalStage.Adult) { return; }
                targetCount = GetRandomTraitCount();
                int attempts = 0;

                // If there's too many traits, remove all not-forced traits first. Skipping any sexuality ones.
                if(targetCount > pawn.story.traits.allTraits.Count)
                {
                    TryPurgeTraits(pawn, request);
                }

                // Increase to match count.
                while (targetCount > pawn.story.traits.allTraits.Count() && attempts < 300)
                {
                    attempts++;
                    Trait trait = PawnGenerator.GenerateTraitsFor(pawn, 1, request, true).FirstOrDefault();
                    if (trait != null) { attempts = 0; pawn.story.traits.GainTrait(trait); }
                }

                targetCount = -1;
            }
        }

        public static void TryPurgeTraits(Pawn pawn, PawnGenerationRequest request)
        {
            List<Trait> traits = pawn.story.traits.allTraits;
            for (int i = 0; i < traits.Count(); i++)
            {
                Trait curTrait = traits[i];
                if (!curTrait.scenForced && !IsBackstoryTrait(pawn, curTrait) && !IsSexualityTrait(curTrait))
                {
                    pawn.story.traits.RemoveTrait(curTrait);
                }
            }
        }

        public static bool IsBackstoryTrait(Pawn pawn, Trait trait)
        {
            bool result = false;
            if(pawn.story.Childhood?.forcedTraits != null)
            {
                if(pawn.story.Childhood.forcedTraits.Any(t => t.def == trait.def))
                {
                    result = true;
                }
            }
            if (pawn.story.Adulthood?.forcedTraits != null)
            {
                if (pawn.story.Adulthood.forcedTraits.Any(t => t.def == trait.def))
                {
                    result = true;
                }
            }
            return result;
        }

        public static bool IsSexualityTrait(Trait trait)
        {
            return trait.def != TraitDefOf.Gay && trait.def != TraitDefOf.Bisexual && trait.def != TraitDefOf.Asexual;
        }

        public static int GetRandomTraitCount()
        {
            return Rand.RangeInclusive(TweaksGaloreMod.settings.tweak_traitCountRange.min, TweaksGaloreMod.settings.tweak_traitCountRange.max);
        }
    }

    [HarmonyPatch(typeof(PawnGenerator), "GenerateTraitsFor")]
    public static class Patch_PawnGenerator_GenerateTraitsFor
    {
        [HarmonyPrefix]
        public static void Prefix(Pawn pawn, ref int traitCount, PawnGenerationRequest? req = null, bool growthMomentTrait = false)
        {
            if (TweaksGaloreMod.settings.tweak_traitCountAdjustment)
            {
                if (pawn.story.traits.allTraits.Count >= Patch_PawnGenerator_GenerateTraits.targetCount && Patch_PawnGenerator_GenerateTraits.targetCount != -1)
                {
                    traitCount = 0;
                }
            }
        }
    }
}
