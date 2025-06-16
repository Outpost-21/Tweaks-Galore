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
	[HarmonyPatch(typeof(CompProjectileInterceptor), "Notify_LordDestroyed")]
	public static class Patch_CompProjectileInterceptor_Notify_LordDestroyed
	{
		[HarmonyPostfix]
		public static void Postfix(CompProjectileInterceptor __instance)
		{
			if (TGTweakDefOf.Tweak_UninstallableMechShields.BoolValue)
            {
				__instance.shutDown = false;
			}
		}
	}
}
