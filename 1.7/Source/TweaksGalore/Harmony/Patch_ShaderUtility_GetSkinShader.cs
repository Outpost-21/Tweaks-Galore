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
        public static void Postfix(ref Shader __result, Pawn pawn)
		{
            if (TGTweakDefOf.Tweak_DisableSpecialSkinShader.BoolValue)
			{
				if(__result == ShaderDatabase.CutoutSkin)
				{
					__result = ShaderDatabase.Cutout;
				}
			}
		}
	}
}