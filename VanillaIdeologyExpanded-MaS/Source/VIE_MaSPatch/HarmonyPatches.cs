using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

using HarmonyLib;
using TweaksGalore;
using VanillaMemesExpanded;

namespace VIE_MaSPatch
{
    [StaticConstructorOnStartup]
    public static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            new Harmony("neronix17.vie_mas_patch.tweaksgalore").PatchAll();
        }
	}

	[HarmonyPatch(typeof(VanillaMemesExpanded_IdeoFoundation_InitPrecepts_Patch))]
	[HarmonyPatch("SetMaxToOptions")]
	public static class VanillaMemesExpanded_IdeoFoundation_InitPrecepts_Patch_SetMaxToOptions_Patch
	{
		[HarmonyPrefix]
		public static bool Prefix()
		{
			if (TweaksGaloreMod.settings.patch_noMemeLimit)
			{
				return false;
			}
			return true;
		}
	}

}
