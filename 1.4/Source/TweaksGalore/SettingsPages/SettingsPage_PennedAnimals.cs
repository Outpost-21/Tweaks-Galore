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
    public static class SettingsPage_PennedAnimals
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static List<ThingDef> cachedAnimalListing = new List<ThingDef>();

        public static List<ThingDef> CachedAnimalListing
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

        public static void DoSettings_PennedAnimals(Listing_Standard listing)
        {
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

                TweaksGaloreStartup.SetPennedAnimals(settings);
            }
            listing.GapLine();
        }
    }
}
