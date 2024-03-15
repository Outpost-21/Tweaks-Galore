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
    [HarmonyPatch(typeof(SkillRecord), "Learn")]
    public class Patch_SkillRecord_Learn
    {
        [HarmonyPrefix]
        public static bool Prefix(ref float xp, bool direct, SkillRecord __instance)
        {
            // Skill Tweaks
            if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_SkillTweaks", false))
            {
                if (xp < 0f)
                {
                    if (TGTweakDefOf.Tweak_SkillLossThreshold.IntValue >= __instance.levelInt)
                    {
                        xp *= 0f;
                    }
                    xp *= TGTweakDefOf.Tweak_SkillLossMultiplier.FloatValue;
                }
                else
                {
                    xp *= TGTweakDefOf.Tweak_SkillGainMultiplier.FloatValue;
                }
            }

            // Not My Best Work
            if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false) && __instance.TotallyDisabled)
            {
                Pawn pawn = __instance?.pawn;
                // Finds settings for limits.
                int levelLimit = 0;
                switch (__instance.passion)
                {
                    case Passion.Minor:
                        levelLimit = TGTweakDefOf.Tweak_NotMyBestwork_MinorPassionCap.IntValue;
                        break;
                    case Passion.Major:
                        levelLimit = TGTweakDefOf.Tweak_NotMyBestwork_MajorPassionCap.IntValue;
                        break;
                    default:
                        levelLimit = TGTweakDefOf.Tweak_NotMyBestwork_BaseLevelCap.IntValue;
                        break;
                }

                if (__instance.levelInt >= levelLimit)
                {
                    // Prevents levelling up.
                    if (__instance.xpSinceLastLevel + xp > __instance.XpRequiredForLevelUp)
                    {
                        xp *= 0f;
                    }
                }
            }
            return true;
        }
    }
}
