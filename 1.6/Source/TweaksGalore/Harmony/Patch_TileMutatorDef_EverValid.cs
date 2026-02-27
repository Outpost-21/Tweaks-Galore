using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;
using System.Runtime;
using System.Security.Cryptography;

namespace TweaksGalore
{
    [HarmonyPatch(typeof(TileMutatorDef), "EverValid")]
    public class Patch_TileMutatorDef_EverValid
    {
        [HarmonyPrefix]
        public static bool Prefix(ref bool __result, TileMutatorDef __instance)
		{
            if (ModLister.OdysseyInstalled && TweaksGaloreMod.settings.GetBoolSetting("TweakSection_Odyssey_TileMutatorControl", false))
            {
                return TweaksGaloreMod.settings.tweak_tileMutatorControlSettings[__instance.defName];
            }
            return true;
		}
	}
}