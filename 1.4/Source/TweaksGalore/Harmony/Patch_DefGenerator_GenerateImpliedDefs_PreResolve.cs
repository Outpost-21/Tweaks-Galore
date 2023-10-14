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
    [HarmonyPatch(typeof(DefGenerator), "GenerateImpliedDefs_PreResolve")]
    public static class Patch_DefGenerator_GenerateImpliedDefs_PreResolve
    {
        [HarmonyPrefix]
        public static void Prefix()
        {
            foreach (TweakDef item in ImpliedPowerTweakDefs())
            {
                DefGenerator.AddImpliedDef(item);
            }
        }

        public static IEnumerable<TweakDef> ImpliedPowerTweakDefs()
        {
            if (TGTweakDefOf.TweakSubSection_PowerAdjusting.heldTweaks.NullOrEmpty())
            {
                TGTweakDefOf.TweakSubSection_PowerAdjusting.heldTweaks = new List<TweakDef>();
            }
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs.Where(t => t.GetCompProperties<CompProperties_Power>() != null))
            {
                string generatedDefName = "Tweak_PowerAdj_" + def.defName;
                CompProperties_Power comp = def.GetCompProperties<CompProperties_Power>();
                if(comp.basePowerConsumption == 0)
                {
                    continue;
                }
                if (DefDatabase<TweakDef>.GetNamedSilentFail(generatedDefName) != null)
                {
                    continue;
                }
                if (!TGTweakDefOf.TweakSubSection_PowerAdjusting.heldTweaks.Any(t => t.defName == generatedDefName))
                {
                    yield return CreatePowerTweakDef(def, generatedDefName, comp);
                }
            }
        }

        public static TweakDef CreatePowerTweakDef(ThingDef thingDef, string generatedDefName, CompProperties_Power comp)
        {
            TweakDef tweak = new TweakDef
            {
                defName = generatedDefName,
                label = thingDef.LabelCap,
                tweakWorker = typeof(TweakWorker_PowerValue),
                tweakType = TweakType.Int,
                defaultValue = comp.basePowerConsumption.ToString(),
                intRange = (comp.basePowerConsumption >= 0 ? new IntRange(0, Mathf.RoundToInt(comp.basePowerConsumption * 10f)) : new IntRange(Mathf.RoundToInt(comp.basePowerConsumption * 10f), 0)),
                tweakThings = new List<ThingDef>() { thingDef },
                subSection = TGTweakDefOf.TweakSubSection_PowerAdjusting
            };
            return tweak;
        }
    }
}
