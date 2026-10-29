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
	[HarmonyPatch(typeof(QuestPartUtility), "GetRewardStackElementsForThings", new Type[] { typeof(IEnumerable<Thing>), typeof(bool) })]
	public static class GetRewardStackElementsForThings_Patch
	{
		[HarmonyPrefix]
		public static bool Prefix(ref IEnumerable<Thing> things, ref bool detailsHidden)
		{
            if (TGTweakDefOf.Tweak_IncidentPawnStats.BoolValue)
			{
				detailsHidden = false;
			}
			return true;
		}
	}
}
