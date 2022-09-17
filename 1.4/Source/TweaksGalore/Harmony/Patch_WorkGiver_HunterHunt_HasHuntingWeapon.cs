using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using System.Reflection;

using HarmonyLib;

namespace TweaksGalore
{
    [StaticConstructorOnStartup]
    public static class Patches_HunterMelee
    {
		static Patches_HunterMelee()
        {
            try
			{
				Type type = AccessTools.TypeByName("SimpleSidearms.Extensions");
				Patches_HunterMelee.getGuns = type.GetMethod("getCarriedWeapons");
			}
            catch
            {
				//LogUtil.LogMessage(":: Tweaks Galore :: Could not find Simple Sidearms during startup. Skipping support.");
            }
        }

		public static MethodInfo getGuns;
		public static bool SimpleSidearmsLoaded => ModsConfig.ActiveModsInLoadOrder.Any((ModMetaData m) => m.Name == "Simple sidearms" || m.PackageId == "petetimessix.simplesidearms");

		[HarmonyPatch(typeof(WorkGiver_HunterHunt), "HasHuntingWeapon")]
		public static class Patch_WorkGiver_HunterHunt_HasHuntingWeapon
		{
			[HarmonyPrefix]
			public static bool Prefix(ref bool __result)
			{
				if (TweaksGaloreMod.settings.tweak_hunterMelee_fistFighting)
				{
					__result = true;
					return false;
				}
				return true;
			}

			[HarmonyPostfix]
			public static void Postfix(Pawn p, ref bool __result)
			{
				if (!__result)
				{
					__result = (p.equipment.Primary != null && p.equipment.Primary.def.IsMeleeWeapon && p.equipment.PrimaryEq.PrimaryVerb.HarmsHealth());
					if (!(__result || !TweaksGaloreMod.settings.tweak_hunterMelee_allowSimpleSidearms || !Patches_HunterMelee.SimpleSidearmsLoaded || Patches_HunterMelee.getGuns == null))
					{
						if (Find.TickManager.TicksGame % 60 == 0)
						{
							if (Patches_HunterMelee.getGuns.Invoke(null, new object[] { p, true, false }) != null)
							{
								__result = true;
							}
						}
					}
				}
			}

			[HarmonyPatch(typeof(Alert_HunterLacksRangedWeapon))]
			[HarmonyPatch(MethodType.Constructor)]
			public static class Patch_HuntersLacksWeaponAlert
			{
				[HarmonyPostfix]
				public static void Postfix(ref string ___defaultLabel, ref string ___defaultExplanation)
				{
					___defaultLabel = "TG_HunterLacksWeapon".Translate();
					___defaultExplanation = "TG_HunterLacksWeaponDesc".Translate();
				}
			}
		}
	}
}
