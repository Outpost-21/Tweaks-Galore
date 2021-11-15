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
    [HarmonyPatch(typeof(SlaveRebellionUtility), "InitiateSlaveRebellionMtbDays")]
    public class Patch_SlaveRebellionUtility_InitiateSlaveRebellionMtbDays
	{
        [HarmonyPrefix]
        public static bool Prefix(ref float __result, Pawn pawn)
		{
            if (TweaksGaloreMod.settings.patch_properSuppression)
			{
				Need_Suppression need_Suppression = pawn.needs.TryGetNeed<Need_Suppression>();
				if (need_Suppression != null && !need_Suppression.CanBeSuppressedNow)
				{
					__result = -1f;
					return false;
				}
			}
			return true;
		}
	}
}