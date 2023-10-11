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
        [HarmonyPostfix]
        public static void Postfix(ref Shader __result, bool skinColorOverriden)
		{
            if (TGTweakDefOf.Tweak_DisableSpecialSkinShader.BoolValue)
			{
				LogUtil.LogMessage("Cutout Skin Shader Used");
				__result = ShaderDatabase.Cutout;
				return;
			}
			__result = (skinColorOverriden ? ShaderDatabase.CutoutSkinColorOverride : ShaderDatabase.CutoutSkin);
		}
	}
}