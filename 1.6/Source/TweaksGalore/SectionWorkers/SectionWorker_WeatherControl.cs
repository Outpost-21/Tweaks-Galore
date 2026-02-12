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
    public class SectionWorker_WeatherControl : SectionWorker
    {
        public float? cachedHighestCommonality;

        public float HighestCommonality
        {
            get
            {
                if (cachedHighestCommonality == null)
                {
                    float currNum = 20f;
                    foreach (BiomeDef biome in DefDatabase<BiomeDef>.AllDefs)
                    {
                        if (!biome.baseWeatherCommonalities.NullOrEmpty())
                        {
                            foreach (WeatherCommonalityRecord bcr in biome.baseWeatherCommonalities)
                            {
                                if(bcr.commonality > currNum)
                                {
                                    currNum = bcr.commonality;
                                }
                            }
                        }
                    }
                    cachedHighestCommonality = currNum;
                }
                return cachedHighestCommonality.Value;
            }
        }

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            base.DoSectionContents(listing, filter);
            listing.DoSettingBool("TweaksGalore.WeatherControlBool".Translate(), "TweaksGalore.WeatherControlBoolDesc".Translate(), def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                foreach (BiomeDef biome in DefDatabase<BiomeDef>.AllDefs)
                {
                    if (!biome.baseWeatherCommonalities.NullOrEmpty() && biome.generatesNaturally)
                    {
                        DoBiomeWeatherSettings(listing, biome);
                    }
                }
            }
        }

        public void DoBiomeWeatherSettings(Listing_Standard listing, BiomeDef biome)
        {
            string categoryString = "Cat_BiomeWeather_" + biome.defName;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(biome.LabelCap, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                foreach(WeatherDef weather in DefDatabase<WeatherDef>.AllDefs)
                {
                    if (settings.tweak_biomeWeatherSettings[biome.defName].weatherCommonality.ContainsKey(weather.defName))
                    {
                        float commonalityBuffer = settings.tweak_biomeWeatherSettings[biome.defName].weatherCommonality[weather.defName];
                        listing.AddLabeledSlider("TweaksGalore.BiomeWeatherCommonality".Translate(weather.LabelCap.ToString(), commonalityBuffer.ToString()), ref commonalityBuffer, 0f, HighestCommonality, "Min: 0", $"Max: {HighestCommonality}", 0.1f);
                        settings.tweak_biomeWeatherSettings[biome.defName].weatherCommonality[weather.defName] = commonalityBuffer;
                    }
                }
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_biomeWeatherSettings = settings.biomeWeatherSettingsDefaults;
        }

        public override void DoOnStartup()
        {
            StoreDefaultValues();
            RegisterValidBiomeWeathers();
            if (settings.GetBoolSetting(def.defName, false))
            {
                SetBiomeWeatherCommonalities();
            }
        }

        public void SetBiomeWeatherCommonalities()
        {
            foreach(BiomeDef biome in DefDatabase<BiomeDef>.AllDefsListForReading)
            {
                if (settings.tweak_biomeWeatherSettings.ContainsKey(biome.defName))
                {
                    Dictionary<string, float> bc = settings.tweak_biomeWeatherSettings[biome.defName].weatherCommonality;
                    for (int i = 0; i < biome.baseWeatherCommonalities.Count; i++)
                    {
                        if (bc.ContainsKey(biome.baseWeatherCommonalities[i].weather.defName))
                        {
                            biome.baseWeatherCommonalities[i].commonality = bc[biome.baseWeatherCommonalities[i].weather.defName];
                        }
                    }
                }
            }
        }

        public void RegisterValidBiomeWeathers()
        {
            if (settings.tweak_biomeWeatherSettings.NullOrEmpty())
            {
                settings.tweak_biomeWeatherSettings = new Dictionary<string, BiomeWeatherSettings>();
            }
            foreach (BiomeDef biome in DefDatabase<BiomeDef>.AllDefsListForReading)
            {
                if (!biome.baseWeatherCommonalities.NullOrEmpty())
                {
                    if (!settings.tweak_biomeWeatherSettings.ContainsKey(biome.defName))
                    {
                        BiomeWeatherSettings s = new BiomeWeatherSettings();
                        foreach (WeatherCommonalityRecord wcr in biome.baseWeatherCommonalities)
                        {
                            s.weatherCommonality.Add(wcr.weather.defName, wcr.commonality);
                        }
                        settings.tweak_biomeWeatherSettings.Add(biome.defName, s);
                    }
                    else
                    {
                        foreach (WeatherCommonalityRecord wcr in biome.baseWeatherCommonalities)
                        {
                            if (!settings.tweak_biomeWeatherSettings[biome.defName].weatherCommonality.ContainsKey(wcr.weather.defName))
                            {
                                settings.tweak_biomeWeatherSettings[biome.defName].weatherCommonality.Add(wcr.weather.defName, wcr.commonality);
                            }
                        }
                    }
                }
            }
        }

        public void StoreDefaultValues()
        {
            if (settings.biomeWeatherSettingsDefaults.NullOrEmpty())
            {
                settings.biomeWeatherSettingsDefaults = new Dictionary<string, BiomeWeatherSettings>();
            }
            foreach (BiomeDef biome in DefDatabase<BiomeDef>.AllDefsListForReading)
            {
                if (!settings.biomeWeatherSettingsDefaults.ContainsKey(biome.defName) && !biome.baseWeatherCommonalities.NullOrEmpty())
                {
                    BiomeWeatherSettings s = new BiomeWeatherSettings();
                    foreach (WeatherCommonalityRecord wcr in biome.baseWeatherCommonalities)
                    {
                        s.weatherCommonality.Add(wcr.weather.defName, wcr.commonality);
                    }
                    settings.biomeWeatherSettingsDefaults.Add(biome.defName, s);
                }
            }
        }
    }
}
