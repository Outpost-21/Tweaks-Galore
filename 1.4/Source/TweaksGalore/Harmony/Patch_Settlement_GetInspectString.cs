using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using RimWorld.Planet;
using Verse;

using HarmonyLib;

namespace TweaksGalore
{
    [HarmonyPatch(typeof(Settlement), "GetInspectString")]
    public class Patch_Settlement_GetInspectString
	{
        [HarmonyPostfix]
        public static void Postfix(ref Settlement __instance, ref string __result)
		{
			string text = __result;
            if (TweaksGaloreMod.settings.patch_settlementTraderTimer)
			{
				if(__instance.Faction != null && __instance.Faction != Faction.OfPlayer && !text.NullOrEmpty() && __instance.trader != null && __instance.trader.EverVisited)
                {
					text += "\n";
					text += "Restocked Since Last Visit: " + __instance.RestockedSinceLastVisit.ToString() + "\n";
					text += "Next Restock: " + (__instance.NextRestockTick - Find.TickManager.TicksGame).ToStringTicksToPeriodVerbose();
                }
				__result = text;
			}
		}
	}
}