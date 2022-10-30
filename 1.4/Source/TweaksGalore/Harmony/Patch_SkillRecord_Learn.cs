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
            TweaksGaloreSettings s = TweaksGaloreMod.settings;
            if (s.tweak_skillRates)
            {
                if (xp < 0f)
                {
                    if ((s.tweak_skillRateLossThreshold - 1) > __instance.levelInt)
                    {
                        return false;
                    }
                    xp *= s.tweak_skillRateLoss;
                }
                else
                {
                    xp *= s.tweak_skillRateGain;
                }
            }
            return true;
        }
    }
}
