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
    [HarmonyPatch(typeof(StunHandler), "CanAdaptToDamage")]
    public class Patch_StunHandler_CanAdaptToDamage
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result, ref StunHandler __instance, DamageDef def)
        {
            if (TGTweakDefOf.Tweak_MechanoidAdaptation.BoolValue)
            {
                if(def.stunAdaptationTicks > 0 && __instance.parent is Pawn pawn)
                {
                    if(def == DamageDefOf.EMP && pawn.RaceProps.IsMechanoid)
                    {
                        __result = false;
                    }
                }
            }
        }
    }
}
