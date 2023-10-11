using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
	[HarmonyPatch(typeof(Pawn), "WorkTypeIsDisabled")]
	public static class Patch_Pawn_WorkTypeIsDisabled
	{
		[HarmonyPrefix]
		public static bool Prefix(WorkTypeDef w)
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				return false;
			}
			return true;
		}
	}
}
