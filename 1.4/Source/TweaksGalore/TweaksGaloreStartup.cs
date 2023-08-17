using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace TweaksGalore
{
    [StaticConstructorOnStartup]
    public static class TweaksGaloreStartup
    {
        public static TweaksGaloreSettings settings = TweaksGaloreMod.settings;

        static TweaksGaloreStartup()
        {
            foreach(TweakSectionDef section in DefDatabase<TweakSectionDef>.AllDefs)
            {
                try
                {
                    section.Worker.DoOnStartup();
                }
                catch (Exception e)
                {
                    LogUtil.LogError($"Caught Exception initialising '{section.defName}' section:\n" + e);
                }
            }

            foreach(TweakDef tweak in DefDatabase<TweakDef>.AllDefs)
            {
                try
                {
                    tweak.Worker.OnStartup();
                }
                catch (Exception e)
                {
                    LogUtil.LogError($"Caught Exception initialising '{tweak.defName}' tweak:\n" + e);
                }
            }

            try { InitializeSettingsDefs(settings); } catch (Exception e) { LogUtil.LogError("Caught Exception initialising settings: " + e); };

            try { InitializePositionArrangements(settings); } catch (Exception e) { LogUtil.LogError("Caught Exception initialising position arrangements: " + e); };

            try { settings.ConvertOldSettings(); } catch (Exception e) { LogUtil.LogError("Caught Exception converting pre-overhaul settings: " + e); };

            try { CompatibilityChecks(settings); } catch (Exception e) { LogUtil.LogError("Caught exeption in compatibility checks: " + e); };
        }

        public static void InitializePositionArrangements(TweaksGaloreSettings settings)
        {
            foreach(TweakDef tweak in DefDatabase<TweakDef>.AllDefs)
            {
                if (tweak.subSection != null)
                {
                    if (tweak.subSection.tweaks.NullOrEmpty())
                    {
                        tweak.subSection.tweaks = new List<TweakDef>();
                    }
                    if (!tweak.subSection.tweaks.Contains(tweak))
                    {
                        tweak.subSection.tweaks.Add(tweak);
                    }
                }
                if (tweak.section != null)
                {
                    if (tweak.section.tweaks.NullOrEmpty())
                    {
                        tweak.section.tweaks = new List<TweakDef>();
                    }
                    if (!tweak.section.tweaks.Contains(tweak))
                    {
                        tweak.section.tweaks.Add(tweak);
                    }
                }
            }
            foreach(TweakSubSectionDef subSection in DefDatabase<TweakSubSectionDef>.AllDefs)
            {
                if(subSection.section != null)
                {
                    if (subSection.section.subSections.NullOrEmpty())
                    {
                        subSection.section.subSections = new List<TweakSubSectionDef>();
                    }
                    if (!subSection.section.subSections.Contains(subSection))
                    {
                        subSection.section.subSections.Add(subSection);
                    }
                }
            }
            foreach(TweakSectionDef section in DefDatabase<TweakSectionDef>.AllDefs)
            {
                if(section.category != null)
                {
                    if (section.category.sections.NullOrEmpty())
                    {
                        section.category.sections = new List<TweakSectionDef>();
                    }
                    if (!section.category.sections.Contains(section))
                    {
                        section.category.sections.Add(section);
                    }
                }
            }
        }

        public static void InitializeSettingsDefs(TweaksGaloreSettings settings)
        {
            foreach(TweakDef def in DefDatabase<TweakDef>.AllDefs)
            {
                switch (def.tweakType)
                {
                    case TweakType.Bool:
                        RegisterTweak_Bool(def, settings);
                        break;
                    case TweakType.Float:
                        RegisterTweak_Float(def, settings);
                        break;
                    case TweakType.FloatRange:
                        RegisterTweak_FloatRange(def, settings);
                        break;
                    case TweakType.Int:
                        RegisterTweak_Int(def, settings);
                        break;
                    case TweakType.IntRange:
                        RegisterTweak_IntRange(def, settings);
                        break;
                    case TweakType.Custom:
                        break;
                    default:
                        LogUtil.LogError($"TweakDef {def.defName} has an invalid <tweakType> value. Accepted values are Bool, Float or Int.");
                        break;
                }
            }
        }

        public static void RegisterTweak_Bool(TweakDef def, TweaksGaloreSettings s)
        {
            s.GetBoolSetting(def.defName, def.DefaultBool);
        }

        public static void RegisterTweak_Float(TweakDef def, TweaksGaloreSettings s)
        {
            s.GetFloatSetting(def.defName, def.DefaultFloat);
        }

        public static void RegisterTweak_FloatRange(TweakDef def, TweaksGaloreSettings s)
        {
            s.GetFloatRangeSetting(def.defName, def.DefaultFloatRange);
        }

        public static void RegisterTweak_Int(TweakDef def, TweaksGaloreSettings s)
        {
            s.GetIntSetting(def.defName, def.DefaultInt);
        }

        public static void RegisterTweak_IntRange(TweakDef def, TweaksGaloreSettings s)
        {
            s.GetIntRangeSetting(def.defName, def.DefaultIntRange);
        }

        public static void Tweak_PoluxTweaks(TweaksGaloreSettings settings)
        {
            // Deal with Tree Radii
            CompProperties_PollutionPump pollutionPumpComp = TGThingDefOf.Plant_TreePolux.GetCompProperties<CompProperties_PollutionPump>();
            pollutionPumpComp.radius = settings.tweak_poluxEffectRadius;
            pollutionPumpComp.intervalTicks = Mathf.RoundToInt(25000f * settings.tweak_poluxEffectRate);
            pollutionPumpComp.disabledByArtificialBuildings = !settings.tweak_poluxArtificialDisables;
        }

        public static void CompatibilityChecks(TweaksGaloreSettings settings)
        {
            if (ModLister.GetActiveModWithIdentifier("kentington.saveourship2") != null && settings.tweak_noBreakdowns)
            {
                LogUtil.LogWarning("SOS2 detected during startup. Disabling No Breakdowns tweak due to incompatibility.");
                settings.tweak_noBreakdowns = false;
            }
        }
    }
}
