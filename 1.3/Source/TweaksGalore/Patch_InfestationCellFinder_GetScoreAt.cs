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
	[HarmonyPatch(typeof(InfestationCellFinder), "GetScoreAt")]
	public static class Patch_InfestationCellFinder_GetScoreAt
	{
		public static void Postfix(IntVec3 cell, Map map, ref float __result)
		{
            if (TweaksGaloreMod.settings.patch_strongFloorsStopInfestations)
            {
				if (__result > 0f && map.terrainGrid.TerrainAt(cell).HasTag("BlocksInfestations"))
				{
					__result = 0f;
				}
			}
		}
	}
}
