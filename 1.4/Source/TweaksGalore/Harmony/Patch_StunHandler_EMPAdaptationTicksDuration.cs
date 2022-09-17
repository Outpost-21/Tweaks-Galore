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
    [HarmonyPatch(typeof(StunHandler), "EMPAdaptationTicksDuration", MethodType.Getter)]
    public class Patch_StunHandler_EMPAdaptationTicksDuration
    {
        [HarmonyPostfix]
        public static void Postfix(ref int __result, ref StunHandler __instance, Thing ___parent)
        {
            if (TweaksGaloreMod.settings.patch_disableMechanoidAdapting)
            {
                __result = 0;
            }
        }
    }
}
