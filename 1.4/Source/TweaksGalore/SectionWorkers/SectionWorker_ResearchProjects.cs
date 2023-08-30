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
    public class SectionWorker_ResearchProjects : SectionWorker
    {
        public Dictionary<string, int> defaultResearchValues = new Dictionary<string, int>();

        public List<ThingDef> cachedStackableListing = new List<ThingDef>();

        public List<ThingDef> CachedStackableListing
        {
            get
            {
                if (cachedStackableListing.NullOrEmpty())
                {
                    cachedStackableListing = new List<ThingDef>();
                }
                foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1 || defaultResearchValues.ContainsKey(t.defName)))
                {
                    if (!cachedStackableListing.Contains(thing))
                    {
                        cachedStackableListing.Add(thing);
                    }
                }
                return cachedStackableListing;
            }
        }

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            if (listing.ButtonTextLabeled("", "Restore defaults"))
            {
                Messages.Message("Tweaks Galore: 'Stack Size Control' tweaks restored to defaults. Restart required to take full effect.", MessageTypeDefOf.CautionInput);
            }
            // Tweak: Penned Animal Config
            listing.DoSettingBool("Stack Size Control", "Allows controlling stack size for categories as well as individual item stack overrides. If it's unstackable in vanilla, this won't make it stackable, as that very often leads to issues. Restarting the game is required to apply these settings.", def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                listing.GapLine();
                for (int i = 0; i < CachedStackableListing.Count; i++)
                {
                    ThingDef curThing = CachedStackableListing[i];
                    float value = settings.tweak_stackSizeControl[curThing.defName];
                    int max = defaultResearchValues[curThing.defName] * 10;
                    listing.AddLabeledSlider(curThing.LabelCap + ": " + value, ref value, 1, max, "1", max.ToString(), 1);
                    settings.tweak_stackSizeControl[curThing.defName] = Mathf.RoundToInt(value);
                }
            }
        }

        public override void DoWriteSettings()
        {
            base.DoWriteSettings();
            if (settings.GetBoolSetting(def.defName, false))
            {
                ApplyStackValues(settings.tweak_stackSizeControl);
            }
        }

        public override void DoOnStartup()
        {
            StoreDefaultValues();
            UpdateStackSizeDict();
            if (settings.GetBoolSetting(def.defName, false))
            {
                ApplyStackValues(settings.tweak_stackSizeControl);
            }
        }

        public override void DoSectionRestore()
        {
            settings.tweak_stackSizeControl = defaultResearchValues;
        }

        public void StoreDefaultValues()
        {
            if (defaultResearchValues.NullOrEmpty())
            {
                defaultResearchValues = new Dictionary<string, int>();
            }
            foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1))
            {
                if (!defaultResearchValues.ContainsKey(thing.defName))
                {
                    defaultResearchValues.Add(thing.defName, thing.stackLimit);
                }
            }
        }

        public void ApplyStackValues(Dictionary<string, int> source)
        {
            if (!source.NullOrEmpty())
            {
                foreach (ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1))
                {
                    if(defaultResearchValues[thing.defName] != source[thing.defName])
                    {
                        SetStackSizeValue(thing, source[thing.defName]);
                    }
                }
            }
        }

        public void UpdateStackSizeDict()
        {
            if (settings.tweak_stackSizeControl.NullOrEmpty())
            {
                defaultResearchValues = new Dictionary<string, int>();
            }
            foreach (ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1))
            {
                if (!settings.tweak_stackSizeControl.ContainsKey(thing.defName))
                {
                    settings.tweak_stackSizeControl.Add(thing.defName, thing.stackLimit);
                }
            }
        }

        public void SetStackSizeValue(ThingDef def, int value)
        {
            def.stackLimit = value;
        }
    }
}
