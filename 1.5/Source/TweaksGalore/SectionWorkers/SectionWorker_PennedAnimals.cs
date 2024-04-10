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
    public class SectionWorker_PennedAnimals : SectionWorker
    {
        public Dictionary<string, float> defaultValues = new Dictionary<string, float>();


        public List<ThingDef> cachedAnimalListing = new List<ThingDef>();

        public List<ThingDef> CachedAnimalListing
        {
            get
            {
                if (cachedAnimalListing.NullOrEmpty())
                {
                    cachedAnimalListing = new List<ThingDef>();
                    List<string> startList = (from x in settings.tweak_pennedAnimalDict.Keys.ToList() orderby x descending select x).ToList();
                    foreach (string name in startList)
                    {
                        ThingDef animal = DefDatabase<ThingDef>.GetNamedSilentFail(name);
                        if (animal != null)
                        {
                            cachedAnimalListing.Add(animal);
                        }
                    }
                    cachedAnimalListing.SortBy(x => x.label);
                }
                return cachedAnimalListing;
            }
        }

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            base.DoSectionContents(listing, filter);
            // Tweak: Penned Animal Config
            listing.DoSettingBool("TweaksGalore.PennedAnimalsBool".Translate(), "TweaksGalore.PennedAnimalsBoolDesc".Translate(), def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                listing.GapLine();
                for (int i = 0; i < CachedAnimalListing.Count; i++)
                {
                    ThingDef curAnimal = CachedAnimalListing[i];
                    float value = settings.tweak_pennedAnimalDict[curAnimal.defName];
                    listing.AddLabeledSlider(curAnimal.LabelCap + ": " + (value == 0f ? (string)"TweaskGalore.SliderNotPennable".Translate() : (value + "TweaksGalore.SliderDays".Translate())), ref value, 0f, 20f, "TweaksGalore.SliderDisabled".Translate(), "TweaksGalore.Slider20Days".Translate());
                    settings.tweak_pennedAnimalDict[curAnimal.defName] = value;
                }

                SetPennedAnimals(settings);
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_pennedAnimalDict = defaultValues;
        }

        public override void DoOnStartup()
        {
            StoreDefaults();
            UpdateAnimalDict();
            if (settings.GetBoolSetting(def.defName, false))
            {
                SetPennedAnimals(settings);
            }
        }

        public void StoreDefaults()
        {
            if (defaultValues.NullOrEmpty())
            {
                defaultValues = new Dictionary<string, float>();
            }

            List<ThingDef> list = (from x in DefDatabase<ThingDef>.AllDefs where (bool)(x.race?.Animal ?? false) select x).ToList();
            foreach (ThingDef def in list)
            {
                if (!defaultValues.ContainsKey(def.defName))
                {
                    float roamMtbDays = def.race.roamMtbDays ?? 0f;
                    defaultValues.Add(def.defName, roamMtbDays);
                }
            }
        }

        public void UpdateAnimalDict()
        {
            if (settings.tweak_pennedAnimalDict.NullOrEmpty())
            {
                settings.tweak_pennedAnimalDict = new Dictionary<string, float>();
            }

            List<ThingDef> list = (from x in DefDatabase<ThingDef>.AllDefs where (bool)(x.race?.Animal ?? false) select x).ToList();
            foreach (ThingDef def in list)
            {
                if (!settings.tweak_pennedAnimalDict.ContainsKey(def.defName))
                {
                    float roamMtbDays = def.race.roamMtbDays ?? 0f;
                    settings.tweak_pennedAnimalDict.Add(def.defName, roamMtbDays);
                }
            }
        }

        public void SetPennedAnimals(TweaksGaloreSettings settings)
        {
            foreach (KeyValuePair<string, float> pair in settings.tweak_pennedAnimalDict)
            {
                ThingDef animal = DefDatabase<ThingDef>.GetNamedSilentFail(pair.Key);
                if (animal != null)
                {
                    if (pair.Value == 0f) { animal.race.roamMtbDays = null; }
                    else { animal.race.roamMtbDays = pair.Value; }
                }
            }
        }
    }
}
