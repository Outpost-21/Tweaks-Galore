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
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Power Tweaks Skipped Defs:");
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs.Where(t => t.GetCompProperties<CompProperties_Power>() != null))
            {
                if (def.label == null)
                {
                    sb.AppendLine($"NULL LABEL ({def.defName}) due to null label.");
                    continue;
                }
                if (Def.DisallowedLabelCharsRegex.IsMatch(def.label))
                {
                    sb.AppendLine($"{def.LabelCap} ({def.defName}) due to disallowed label characters.");
                    continue;
                }
                string generatedDefName = "Tweak_PowerAdj_" + def.defName;
                CompProperties_Power comp = def.GetCompProperties<CompProperties_Power>();
                if (float.IsInfinity(comp.basePowerConsumption))
                {
                    sb.AppendLine($"{def.LabelCap} ({def.defName}) due to infinite power setting.");
                    continue;
                }
                if(comp.basePowerConsumption == 0)
                {
                    sb.AppendLine($"{def.LabelCap} ({def.defName}) due to zero power setting.");
                    continue;
                }
                if (DefDatabase<TweakDef>.GetNamedSilentFail(generatedDefName) != null)
                {
                    sb.AppendLine($"{def.LabelCap} ({def.defName}) due pre-defined tweak.");
                    continue;
                }
                if (!TGTweakDefOf.TweakSubSection_PowerAdjusting.heldTweaks.Any(t => t.defName == generatedDefName))
                {
                    yield return CreatePowerTweakDef(def, generatedDefName, comp);
                }
            }
            LogUtil.LogMessage(sb.ToString());
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
