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
            if (listing.ButtonTextLabeled("", "Restore defaults"))
            {
                DefaultUtil.RestoreSettings_PennedAnimals(settings);
                Messages.Message("Tweaks Galore: 'Penned Animal' tweaks restored to defaults. Restart required to take full effect.", MessageTypeDefOf.CautionInput);
            }
            // Tweak: Penned Animal Config
            listing.CheckboxEnhanced("Penned Animal Config", "Allows control over which animals can be penned and how many days it takes for them to begin roaming if they are not in a pen.", ref settings.tweak_pennedAnimalConfig);
            if (settings.tweak_pennedAnimalConfig)
            {
                listing.GapLine();
                for (int i = 0; i < CachedAnimalListing.Count; i++)
                {
                    ThingDef curAnimal = CachedAnimalListing[i];
                    float value = settings.tweak_pennedAnimalDict[curAnimal.defName];
                    listing.AddLabeledSlider(curAnimal.LabelCap + ": " + (value == 0f ? "Not Pennable" : (value + " Days")), ref value, 0f, 20f, "Disabled", "20 Days");
                    settings.tweak_pennedAnimalDict[curAnimal.defName] = value;
                }

                SetPennedAnimals(settings);
            }
        }

        public override void DoOnStartup(TweaksGaloreSettings settings)
        {
            FillAnimalDict(settings);
            if (settings.tweak_pennedAnimalConfig)
            {
                SetPennedAnimals(settings);
            }
        }

        public static void FillAnimalDict(TweaksGaloreSettings settings)
        {
            if (settings.tweak_pennedAnimalDict.NullOrEmpty() || settings.restorePennedAnimals)
            {
                settings.tweak_pennedAnimalDict = new Dictionary<string, float>();
                settings.restorePennedAnimals = false;
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

        public static void SetPennedAnimals(TweaksGaloreSettings settings)
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
