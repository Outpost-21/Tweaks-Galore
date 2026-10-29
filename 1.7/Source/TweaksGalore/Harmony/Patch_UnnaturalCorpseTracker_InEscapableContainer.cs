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
    [HarmonyPatch(typeof(UnnaturalCorpseTracker), "InEscapableContainer")]
    public class Patch_UnnaturalCorpseTracker_InEscapableContainer
	{
        [HarmonyPostfix]
        public static void Postfix(UnnaturalCorpseTracker __instance, ref bool __result)
		{
            if (TGTweakDefOf.Tweak_MakeStoneSarcophagiUnescapable.BoolValue && __result)
			{
				Building building = __instance.corpse.ParentHolder as Building;
				if (building.def.defName == "Sarcophagus" && (building.Stuff?.stuffCategories?.Contains(StuffCategoryDefOf.Stony) ?? false))
                {
					__result = false;
					return;
                }
			}
		}
	}
}