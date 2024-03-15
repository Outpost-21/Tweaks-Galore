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
    [HarmonyPatch(typeof(PowerNetGraphics), "PrintWirePieceConnecting")]
    public static class Patch_PowerNetGraphics_PrintWirePieceConnecting
    {
        [HarmonyPrefix]
        public static bool Prefix(bool forPowerOverlay) 
        { 
            return TweaksGaloreMod.settings.tweak_hiddenConduits ? forPowerOverlay : true; 
        }
    }
}
