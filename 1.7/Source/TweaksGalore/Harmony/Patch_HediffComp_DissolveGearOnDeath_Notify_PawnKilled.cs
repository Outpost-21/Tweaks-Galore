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
    [HarmonyPatch(typeof(HediffComp_DissolveGearOnDeath), "Notify_PawnKilled")]
    public class Patch_HediffComp_DissolveGearOnDeath_Notify_PawnKilled
	{
        [HarmonyPrefix]
        public static void Prefix(HediffComp_DissolveGearOnDeath __instance)
		{
            if (TGTweakDefOf.Tweak_PinataDeathAcidifiers.BoolValue)
            {
                __instance.Pawn.equipment.DropAllEquipment(__instance.Pawn.Position, false);
                __instance.Pawn.apparel.DropAll(__instance.Pawn.Position, false);
                AttemptInstantDessication(__instance.Pawn);
            }
		}

        public static void AttemptInstantDessication(Pawn pawn)
        {
            CompRottable comp = pawn.TryGetComp<CompRottable>();
            if(comp == null)
            {
                comp = pawn.Corpse?.TryGetComp<CompRottable>();
            }
            if(comp != null)
            {
                comp.RotImmediately(RotStage.Dessicated);
            }
        }
	}
}