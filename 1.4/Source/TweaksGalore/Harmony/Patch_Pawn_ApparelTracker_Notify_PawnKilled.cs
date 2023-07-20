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
	[HarmonyPatch(typeof(Pawn_ApparelTracker), "Notify_PawnKilled")]
	public class Patch_Pawn_ApparelTracker_Notify_PawnKilled
    {
        [HarmonyPrefix]
        public static bool Prefix(Pawn_ApparelTracker __instance, DamageInfo? dinfo)
        {
            if (TGTweakDefOf.Tweak_TaintOnRot.BoolValue)
			{
				DamageApparel(__instance, dinfo);
				return false;
			}
			else if (TGTweakDefOf.Tweak_OnlyTaintFirstLayer.BoolValue)
			{
				DamageApparel(__instance, dinfo);
				__instance.wornApparel.TaintInnermostLayer();
				return false;
            }
            return true;
        }

        public static void DamageApparel(Pawn_ApparelTracker tracker, DamageInfo? dinfo)
        {
			if (dinfo.HasValue && dinfo.Value.Def.ExternalViolenceFor(tracker.pawn))
			{
				for (int i = 0; i < tracker.wornApparel.Count; i++)
				{
					if (tracker.wornApparel[i].def.useHitPoints)
					{
						int num = Mathf.RoundToInt((float)tracker.wornApparel[i].HitPoints * Rand.Range(0.15f, 0.4f));
						tracker.wornApparel[i].TakeDamage(new DamageInfo(dinfo.Value.Def, num));
					}
				}
			}
		}
    }
}
