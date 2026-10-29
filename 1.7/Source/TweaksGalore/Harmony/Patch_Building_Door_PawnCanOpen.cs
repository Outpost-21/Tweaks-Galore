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
	[HarmonyPatch(typeof(Building_Door), "PawnCanOpen")]
	public class Patch_Building_Door_PawnCanOpen
	{
		[HarmonyPostfix]
		public static void Postfix(Pawn p, ref bool __result)
		{
			if (__result && TGTweakDefOf.Tweak_PrisonersDontHaveKeys.BoolValue)
			{
				if (TGTweakDefOf.Tweak_PDHKPrisoners.BoolValue && p.IsPrisonerOfColony)
				{
					if (PrisonBreakUtility.IsPrisonBreaking(p))
					{
						__result = (TGTweakDefOf.Tweak_PDHKOwnDoor.BoolValue && p.GetRoom(RegionType.Set_All).IsPrisonCell);
						return;
					}
				}
				else if (TGTweakDefOf.Tweak_PDHKSlaves.BoolValue && p.IsSlaveOfColony)
				{
					if (SlaveRebellionUtility.IsRebelling(p))
					{
						if (TGTweakDefOf.Tweak_PDHKOwnDoor.BoolValue)
						{
							__result = p.GetRoom(RegionType.Set_All).ContainedBeds.Any((Building_Bed bed) => bed.ForSlaves);
							return;
						}
						else
						{
							__result = false;
						}
					}
				}
			}
		}
	}
}
