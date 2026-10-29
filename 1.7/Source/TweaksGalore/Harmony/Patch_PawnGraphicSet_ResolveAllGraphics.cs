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
    [HarmonyPatch(typeof(PawnGraphicSet), "ResolveAllGraphics")]
    public class Patch_PawnGraphicSet_ResolveAllGraphics
	{
        [HarmonyPostfix]
        public static void Postfix(PawnGraphicSet __instance)
		{
            if (TGTweakDefOf.Tweak_DisableSpecialSkinShader.BoolValue)
			{
				if (__instance.pawn.RaceProps.Humanlike)
				{
					LogUtil.LogMessage("Cutout Skin Shader Used");
					if(__instance.nakedGraphic.Shader == ShaderDatabase.CutoutSkin || __instance.nakedGraphic.Shader == ShaderDatabase.CutoutSkinColorOverride)
                    {
						__instance.nakedGraphic = GraphicDatabase.Get<Graphic_Multi>(__instance.nakedGraphic.path, ShaderDatabase.Cutout, __instance.nakedGraphic.drawSize, __instance.nakedGraphic.Color, __instance.nakedGraphic.ColorTwo);
						PortraitsCache.SetDirty(__instance.pawn);
						GlobalTextureAtlasManager.TryMarkPawnFrameSetDirty(__instance.pawn);
					}
				}
			}
		}
	}
}