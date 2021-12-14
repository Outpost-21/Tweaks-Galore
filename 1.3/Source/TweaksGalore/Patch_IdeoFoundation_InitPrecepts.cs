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
	[HarmonyPatch(typeof(IdeoFoundation))]
	[HarmonyPatch("InitPrecepts")]
	public class Patch_IdeoFoundation_InitPrecepts
	{
		public static void Prefix(ref IntRange ___MemeCountRangeAbsolute)
		{
            if (TweaksGaloreMod.settings.patch_noMemeLimit)
			{
				___MemeCountRangeAbsolute = new IntRange(0, 1000);
			}
		}
	}
}
