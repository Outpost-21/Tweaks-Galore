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
    [HarmonyPatch(typeof(HeadTypeDef), "GetGraphic")]
    public class Patch_HeadTypeDef_GetGraphic
	{
        [HarmonyPostfix]
        public static void Postfix(HeadTypeDef __instance, ref Graphic_Multi __result, Color color, bool dessicated, bool skinColorOverriden)
		{
            if (TGTweakDefOf.Tweak_DisableSpecialSkinShader.BoolValue)
			{
				Shader shader = ShaderDatabase.Cutout;
				for (int i = 0; i < __instance.graphics.Count; i++)
				{
					if (color.IndistinguishableFrom(__instance.graphics[i].Key) && __instance.graphics[i].Value.Shader == shader)
					{
						__result = __instance.graphics[i].Value;
						return;
					}
				}
				Graphic_Multi graphic_Multi = (Graphic_Multi)GraphicDatabase.Get<Graphic_Multi>(__instance.graphicPath, shader, Vector2.one, color);
				__instance.graphics.Add(new KeyValuePair<Color, Graphic_Multi>(color, graphic_Multi));
				__result = graphic_Multi;
			}
		}
	}
}