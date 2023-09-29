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
    [HarmonyPatch(typeof(ShaderUtility), "GetSkinShader")]
    public class Patch_ShaderUtility_GetSkinShader
	{
        [HarmonyPrefix]
        public static bool Prefix(ref Shader __result, bool skinColorOverriden)
		{
            if (TGTweakDefOf.Tweak_DisableSpecialSkinShader.BoolValue)
			{
				LogUtil.LogMessage("Cutout Skin Shader Used");
				__result = ShaderDatabase.Cutout;
				return false;
			}
			__result = (skinColorOverriden ? ShaderDatabase.CutoutSkinColorOverride : ShaderDatabase.CutoutSkin);
			return true;
		}
	}
}