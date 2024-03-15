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
	[HarmonyPatch(typeof(SkillRecord), "CalculateTotallyDisabled")]
	public static class Patch_SkillRecord_CalculateTotallyDisabled
	{
		[HarmonyPrefix]
		public static bool Prefix()
		{
			if (TweaksGaloreMod.settings.GetBoolSetting("Tweak_NotMyBestWork", false))
			{
				return false;
			}
			return true;
		}
	}
}
