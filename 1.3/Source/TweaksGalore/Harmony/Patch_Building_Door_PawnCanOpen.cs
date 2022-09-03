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
			if (__result && TweaksGaloreMod.settings.patch_prisonersDontHaveKeys)
			{
				if (TweaksGaloreMod.settings.patch_pdhk_prisoners && p.IsPrisonerOfColony)
				{
					if (PrisonBreakUtility.IsPrisonBreaking(p))
					{
						__result = (TweaksGaloreMod.settings.patch_pdhk_ownDoor && p.GetRoom(RegionType.Set_All).IsPrisonCell);
						return;
					}
				}
				else if (TweaksGaloreMod.settings.patch_pdhk_slaves && p.IsSlaveOfColony)
				{
					if (SlaveRebellionUtility.IsRebelling(p))
					{
						if (TweaksGaloreMod.settings.patch_pdhk_ownDoor)
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
