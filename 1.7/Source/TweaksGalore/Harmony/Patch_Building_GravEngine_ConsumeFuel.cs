using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;
using RimWorld.Planet;

namespace TweaksGalore
{
	[HarmonyPatch(typeof(Building_GravEngine), "ConsumeFuel")]
	public class Patch_Building_GravEngine_ConsumeFuel
    {
        [HarmonyPostfix]
        public static void Postfix(Building_GravEngine __instance, PlanetTile tile)
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue && TGTweakDefOf.Tweak_Gravship_DisableCooldown.BoolValue)
            {
                __instance.cooldownCompleteTick = -1;
            }
        }
    }
}
