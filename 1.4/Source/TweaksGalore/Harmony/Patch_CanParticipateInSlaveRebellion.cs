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
    [HarmonyPatch(typeof(SlaveRebellionUtility), "CanParticipateInSlaveRebellion")]
    public class Patch_SlaveRebellionUtility_CanParticipateInSlaveRebellion
	{
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, Pawn pawn)
		{
			if (__result == true && TweaksGaloreMod.settings.patch_properSuppression)
			{
				Need_Suppression need_Suppression = pawn.needs.TryGetNeed<Need_Suppression>();
				if (need_Suppression != null && need_Suppression.IsSuppressed())
				{
					__result = false;
				}
			}
		}
	}
}