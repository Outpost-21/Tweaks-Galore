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
    public class TweaksGaloreSettings : ModSettings
    {
        public bool debugMode = false;

        public bool overhaulFirstLoad = true;

        // Tech Traversal
        public Dictionary<FactionDef, TechLevel> factionTechMap = new Dictionary<FactionDef, TechLevel>();
        public TechLevel tweak_tech_lowestTechLevel = TechLevel.Neolithic;
        public TechLevel tweak_tech_highestTechLevel = TechLevel.Undefined;

        // Research Projects
        public Dictionary<string, ResearchProjectSettings> tweak_researchProjectSettings = new Dictionary<string, ResearchProjectSettings>();
        [Unsaved]
        public Dictionary<string, ResearchProjectSettings> researchProjectSettingsDefaults = new Dictionary<string, ResearchProjectSettings>();

        // Penned Animals
        public Dictionary<string, float> tweak_pennedAnimalDict = new Dictionary<string, float>();

        // Event Control
        public Dictionary<string, float> tweak_eventControlDict = new Dictionary<string, float>();

        // Anima Psylink
        public List<int> tweak_animaPsylinkLevelNeeds = new List<int>();

        // Factions
        public Dictionary<string, FactionRaidSettings> tweak_factionRaidSettings = new Dictionary<string, FactionRaidSettings>();
        [Unsaved]
        public Dictionary<string, FactionRaidSettings> factionRaidSettingsDefaults = new Dictionary<string, FactionRaidSettings>();

        // Titles
        public Dictionary<string, RoyalTitleSettings> tweak_royalTitleSettings = new Dictionary<string, RoyalTitleSettings>();
        [Unsaved]
        public Dictionary<string, RoyalTitleSettings> royalTitleSettingsDefaults = new Dictionary<string, RoyalTitleSettings>();

        // Permits
        public Dictionary<string, RoyalPermitSettings> tweak_royalPermitSettings = new Dictionary<string, RoyalPermitSettings>();
        [Unsaved]
        public Dictionary<string, RoyalPermitSettings> royalPermitSettingsDefaults = new Dictionary<string, RoyalPermitSettings>();

        // Pregnancy PawnKinds
        public List<PawnKindDef> tweak_pregnancyChanceEditedPawnKinds = new List<PawnKindDef>();

        // Genepacks
        public bool tweak_genepackTweaks = false;
        public Dictionary<string, bool> genepacksEnabled = new Dictionary<string, bool>();
        public Dictionary<string, bool> defaultGenepacksEnabled = new Dictionary<string, bool>();

        // Dynamic Settings
        public Dictionary<string, bool> boolSetting = new Dictionary<string, bool>();

        public bool GetBoolSetting(string key, bool? value = null)
        {
            if (boolSetting.NullOrEmpty())
            {
                boolSetting = new Dictionary<string, bool>();
            }
            if (!boolSetting.ContainsKey(key) && value != null)
            {
                boolSetting.Add(key, (bool)value);
            }
            return boolSetting[key];
        }

        public void SetBoolSetting(string key, bool value)
        {
            if(key.NullOrEmpty())
            {
                LogUtil.LogWarning($"Tried to set bool but key is null.");
                return;
            }
            if (!boolSetting.ContainsKey(key))
            {
                LogUtil.LogWarning($"Tried to set bool with {key}, but that setting doesn't exist.");
                return;
            }
            boolSetting[key] = value;
        }

        public Dictionary<string, int> intSetting = new Dictionary<string, int>();

        public int GetIntSetting(string key, int? value = null)
        {
            if (intSetting.NullOrEmpty())
            {
                intSetting = new Dictionary<string, int>();
            }
            if (!intSetting.ContainsKey(key) && value != null)
            {
                intSetting.Add(key, (int)value);
            }
            return intSetting[key];
        }

        public void SetIntSetting(string key, int value)
        {
            if (!intSetting.ContainsKey(key))
            {
                LogUtil.LogWarning($"Tried to set int with {key}, but that setting doesn't exist.");
            }
            intSetting[key] = value;
        }

        public Dictionary<string, IntRange> intRangeSetting = new Dictionary<string, IntRange>();

        public IntRange GetIntRangeSetting(string key, IntRange? value = null)
        {
            if (intRangeSetting.NullOrEmpty())
            {
                intRangeSetting = new Dictionary<string, IntRange>();
            }
            if (!intRangeSetting.ContainsKey(key) && value != null)
            {
                intRangeSetting.Add(key, (IntRange)value);
            }
            return intRangeSetting[key];
        }

        public void SetIntRangeSetting(string key, IntRange value)
        {
            if (!intRangeSetting.ContainsKey(key))
            {
                LogUtil.LogWarning($"Tried to set int range with {key}, but that setting doesn't exist.");
            }
            intRangeSetting[key] = value;
        }

        public Dictionary<string, float> floatSetting = new Dictionary<string, float>();

        public float GetFloatSetting(string key, float? value = null)
        {
            if (floatSetting.NullOrEmpty())
            {
                floatSetting = new Dictionary<string, float>();
            }
            if (!floatSetting.ContainsKey(key) && value != null)
            {
                floatSetting.Add(key, (float)value);
            }
            return floatSetting[key];
        }

        public void SetFloatSetting(string key, float value)
        {
            if (!floatSetting.ContainsKey(key))
            {
                LogUtil.LogWarning($"Tried to set float with {key}, but that setting doesn't exist.");
            }
            floatSetting[key] = value;
        }

        public Dictionary<string, FloatRange> floatRangeSetting = new Dictionary<string, FloatRange>();

        public FloatRange GetFloatRangeSetting(string key, FloatRange? value = null)
        {
            if (floatRangeSetting.NullOrEmpty())
            {
                floatRangeSetting = new Dictionary<string, FloatRange>();
            }
            if (!floatRangeSetting.ContainsKey(key) && value != null)
            {
                floatRangeSetting.Add(key, (FloatRange)value);
            }
            return floatRangeSetting[key];
        }

        public void SetFloatRangeSetting(string key, FloatRange value)
        {
            if (!floatRangeSetting.ContainsKey(key))
            {
                LogUtil.LogWarning($"Tried to set float range with {key}, but that setting doesn't exist.");
            }
            floatRangeSetting[key] = value;
        }


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref overhaulFirstLoad, "overhaulFirstLoad", true);

            // Dynamic Settings
            Scribe_Collections.Look(ref boolSetting, "boolSetting");
            Scribe_Collections.Look(ref intSetting, "intSetting");
            Scribe_Collections.Look(ref intRangeSetting, "intRangeSetting");
            Scribe_Collections.Look(ref floatSetting, "floatSetting");
            Scribe_Collections.Look(ref floatRangeSetting, "floatRangeSetting");

            // Research
            Scribe_Values.Look(ref tweak_tech_lowestTechLevel, "tweak_tech_lowestTechLevel", TechLevel.Undefined);
            Scribe_Values.Look(ref tweak_tech_highestTechLevel, "tweak_tech_highestTechLevel", TechLevel.Undefined);
            Scribe_Collections.Look(ref tweak_researchProjectSettings, "tweak_researchProjectSettings");

            // Penned Animals
            Scribe_Collections.Look(ref tweak_pennedAnimalDict, "tweak_pennedAnimalDict");

            // Event Control
            Scribe_Collections.Look(ref tweak_eventControlDict, "tweak_eventcontrolDict");

            // Anima
            Scribe_Collections.Look(ref tweak_animaPsylinkLevelNeeds, "tweak_animaPsylinkLevelNeeds");

            // Royal Titles
            Scribe_Collections.Look(ref tweak_royalTitleSettings, "tweak_royalTitleSettings");

            // Royal Permits
            Scribe_Collections.Look(ref tweak_royalPermitSettings, "tweak_royalPermitSettings");

            // Genepacks
            Scribe_Values.Look(ref tweak_genepackTweaks, "tweak_genepackTweaks", false);
            Scribe_Collections.Look(ref genepacksEnabled, "genepacksEnabled");
        }

        public IEnumerable<string> GetEnabledSettings
        {
            get
            {
                return GetType().GetFields().Where(p => p.FieldType == typeof(bool) && (bool)p.GetValue(this)).Select(p => p.Name);
            }
        }
    }
}
