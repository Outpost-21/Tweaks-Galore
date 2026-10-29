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
        public static void Prefix(ref float xp, bool direct, SkillRecord __instance)
        {
            // Skill Tweaks
            if (TGTweakDefOf.Tweak_SkillTweaks.BoolValue)
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
        }
    }
}
