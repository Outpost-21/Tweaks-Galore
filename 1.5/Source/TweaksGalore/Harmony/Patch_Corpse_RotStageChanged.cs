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
	[HarmonyPatch(typeof(Corpse), "RotStageChanged")]
	public class Patch_Corpse_RotStageChanged
	{
        [HarmonyPrefix]
        public static void Prefix(Corpse __instance)
        {
            if (TGTweakDefOf.Tweak_TaintDisabled.BoolValue)
            {
                return;
            }
            Pawn pawn = __instance.InnerPawn;
            if (pawn.apparel != null && TGTweakDefOf.Tweak_TaintOnRot.BoolValue)
            {
                ThingOwner<Apparel> wornApparel = (ThingOwner<Apparel>)pawn.apparel.GetDirectlyHeldThings();
                if (TGTweakDefOf.Tweak_OnlyTaintFirstLayer.BoolValue)
                {
                    wornApparel.TaintInnermostLayer();
                }
                else
                {
                    wornApparel.TaintAll();
                }
            }
            return;
        }
    }
}
