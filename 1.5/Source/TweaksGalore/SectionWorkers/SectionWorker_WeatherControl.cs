﻿using RimWorld;
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
        public Dictionary<string, float> defaultValues = new Dictionary<string, float>();

        public List<IncidentDef> cachedIncidentListing = new List<IncidentDef>();

        public List<IncidentDef> CachedIncidentListing
        {
            get
            {
                if (cachedIncidentListing.NullOrEmpty())
                {
                    cachedIncidentListing = new List<IncidentDef>();
                    List<string> startList = (from x in settings.tweak_eventControlDict.Keys.ToList() orderby x descending select x).ToList();
                    foreach (string name in startList)
                    {
                        IncidentDef animal = DefDatabase<IncidentDef>.GetNamedSilentFail(name);
                        if (animal != null)
                        {
                            cachedIncidentListing.Add(animal);
                        }
                    }
                    cachedIncidentListing.SortBy(x => x.label);
                }
                return cachedIncidentListing;
            }
        }

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            base.DoSectionContents(listing, filter);
            // Tweak: Penned Animal Config
            listing.DoSettingBool("TweaksGalore.IncidentControlBool".Translate(), "TweaskGalore.IncidentControlBoolDesc".Translate(), def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                listing.GapLine();
                for (int i = 0; i < CachedIncidentListing.Count; i++)
                {
                    IncidentDef curIncident = CachedIncidentListing[i];
                    float value = settings.tweak_eventControlDict[curIncident.defName];
                    listing.AddLabeledSlider(curIncident.LabelCap + ": " + (value == 0f ? "Never" : (value)), ref value, 0f, 10f, "Disabled", "High Chance", 0.01f);
                    settings.tweak_eventControlDict[curIncident.defName] = value;
                }
                SetIncidentChances();
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_eventControlDict = defaultValues;
        }

        public override void DoOnStartup()
        {
            StoreDefaultValues();
            UpdateIncidentDict();
            if (settings.GetBoolSetting(def.defName, false))
            {
                SetIncidentChances();
            }
        }

        public void StoreDefaultValues()
        {
            if (defaultValues.NullOrEmpty())
            {
                defaultValues = new Dictionary<string, float>();
            }
            foreach (IncidentDef def in DefDatabase<IncidentDef>.AllDefsListForReading)
            {
                if (!defaultValues.ContainsKey(def.defName))
                {
                    float chance = def.baseChance;
                    defaultValues.Add(def.defName, chance);
                }
            }
        }

        public void UpdateIncidentDict()
        {
            if (settings.tweak_eventControlDict.NullOrEmpty())
            {
                settings.tweak_eventControlDict = new Dictionary<string, float>();
            }

            foreach (IncidentDef def in DefDatabase<IncidentDef>.AllDefsListForReading)
            {
                if (!settings.tweak_eventControlDict.ContainsKey(def.defName))
                {
                    float chance = def.baseChance;
                    settings.tweak_eventControlDict.Add(def.defName, chance);
                }
            }
        }

        public void SetIncidentChances()
        {
            foreach (KeyValuePair<string, float> pair in settings.tweak_eventControlDict)
            {
                IncidentDef incident = DefDatabase<IncidentDef>.GetNamedSilentFail(pair.Key);
                if (incident != null)
                {
                    incident.baseChance = pair.Value;
                }
            }
        }
    }
}
