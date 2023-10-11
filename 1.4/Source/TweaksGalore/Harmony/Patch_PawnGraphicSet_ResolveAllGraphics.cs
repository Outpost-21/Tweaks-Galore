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
					__instance.nakedGraphic = GraphicDatabase.Get<Graphic_Multi>(__instance.pawn.story.bodyType.bodyNakedGraphicPath, ShaderDatabase.Cutout, Vector2.one, __instance.pawn.story.SkinColor);
					PortraitsCache.SetDirty(__instance.pawn);
					GlobalTextureAtlasManager.TryMarkPawnFrameSetDirty(__instance.pawn);
				}
			}
		}
	}
}