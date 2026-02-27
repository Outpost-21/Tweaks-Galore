using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
    public class SectionWorker_LandmarkControl : SectionWorker
    {
        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            base.DoSectionContents(listing, filter);
            listing.DoSettingBool("TweaksGalore.LandmarkControlBool".Translate(), "TweaksGalore.LandmarkControlBoolDesc".Translate(), def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                foreach (LandmarkDef landmark in DefDatabase<LandmarkDef>.AllDefs)
                {
                    float value = settings.tweak_landmarkControlSettings[landmark.defName];
                    listing.AddLabeledSlider(landmark.LabelCap + ": " + (value == 0f ? "TweaksGalore.SliderNever".Translate() : (value)), ref value, 0f, 10f, "TweaksGalore.SliderDisabled".Translate(), "TweaksGalore.SliderHighChance".Translate(), 0.01f);
                    settings.tweak_landmarkControlSettings[landmark.defName] = value;
                }
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_landmarkControlSettings = settings.landmarkSettingsDefaults;
        }

        public override void DoOnStartup()
        {
            StoreDefaultValues();
            RegisterValidLandmarks();
            if (settings.GetBoolSetting(def.defName, false))
            {
                SetLandmarkCommonalities();
            }
        }

        public void SetLandmarkCommonalities()
        {
            foreach(LandmarkDef landmark in DefDatabase<LandmarkDef>.AllDefsListForReading)
            {
                if (settings.tweak_landmarkControlSettings.ContainsKey(landmark.defName))
                {
                    landmark.commonality = settings.tweak_landmarkControlSettings[landmark.defName];
                }
            }
        }

        public void RegisterValidLandmarks()
        {
            if (settings.tweak_landmarkControlSettings.NullOrEmpty())
            {
                settings.tweak_landmarkControlSettings = new Dictionary<string, float>();
            }
            foreach (LandmarkDef landmark in DefDatabase<LandmarkDef>.AllDefsListForReading)
            {
                if (!settings.tweak_landmarkControlSettings.ContainsKey(landmark.defName))
                {
                    settings.tweak_landmarkControlSettings.Add(landmark.defName, landmark.commonality);
                }
            }
        }

        public void StoreDefaultValues()
        {
            if (settings.landmarkSettingsDefaults.NullOrEmpty())
            {
                settings.landmarkSettingsDefaults = new Dictionary<string, float>();
            }
            foreach (LandmarkDef landmark in DefDatabase<LandmarkDef>.AllDefsListForReading)
            {
                if (!settings.landmarkSettingsDefaults.ContainsKey(landmark.defName))
                {
                    settings.landmarkSettingsDefaults.Add(landmark.defName, landmark.commonality);
                }
            }
        }
    }
}
