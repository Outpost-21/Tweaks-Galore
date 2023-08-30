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
    public class SectionWorker_StackSizeControl : SectionWorker
    {
        public Dictionary<string, int> defaultStackSizes = new Dictionary<string, int>();

        public Dictionary<string, float> defaultCategorySizes = new Dictionary<string, float>();

        public List<ThingDef> cachedStackableListing = new List<ThingDef>();

        public List<ThingDef> CachedStackableListing
        {
            get
            {
                if (cachedStackableListing.NullOrEmpty())
                {
                    cachedStackableListing = new List<ThingDef>();
                    foreach (ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => settings.tweak_stackSizeControl.ContainsKey(t.defName)))
                    {
                        if (!cachedStackableListing.Contains(thing))
                        {
                            cachedStackableListing.Add(thing);
                        }
                    }
                }
                return cachedStackableListing;
            }
        }

        public List<ThingCategoryDef> displayed = new List<ThingCategoryDef>();

        public List<ThingCategoryDef> cachedCategoriesListing = new List<ThingCategoryDef>();

        public List<ThingCategoryDef> CachedCategoriesListing
        {
            get
            {
                if (cachedCategoriesListing.NullOrEmpty())
                {
                    cachedCategoriesListing = new List<ThingCategoryDef>();
                    foreach (ThingCategoryDef cat in DefDatabase<ThingCategoryDef>.AllDefs)
                    {
                        if (!cachedCategoriesListing.Contains(cat) && HasStackables(cat))
                        {
                            cachedCategoriesListing.Add(cat);
                        }
                    }
                }
                return cachedCategoriesListing;
            }
        }

        public List<ThingCategoryDef> checkedCategories = new List<ThingCategoryDef>();

        public bool HasStackables(ThingCategoryDef category)
        {
            if(category.SortedChildThingDefs != null && category.SortedChildThingDefs.Any(t => t.stackLimit > 1))
            {
                return true;
            }
            if(category.childCategories.Any(c => HasStackables(c)))
            {
                return true;
            }
            return false;
        }

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            if (listing.ButtonTextLabeled("", "Restore defaults"))
            {
                Messages.Message("Tweaks Galore: 'Stack Size Control' tweaks restored to defaults. Restart required to take full effect.", MessageTypeDefOf.CautionInput);
                DoSectionRestore();
            }
            // Tweak: Stack Size Config
            listing.DoSettingBool("Stack Size Control", "Allows controlling stack size for categories as well as individual item stack overrides. If it's unstackable in vanilla, this won't make it stackable, as that very often leads to issues. Restarting the game is required to apply these settings.", def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                // List Categories
                listing.GapLine();
                displayed = new List<ThingCategoryDef>();
                DrawRootCategorySetting(listing);
                // List Exceptions
                listing.GapLine();
                listing.LabelBacked("Custom Rules", Color.white);
                ThingDef custom;
                listing.StackableThingDropdown("Add Custom Rule", "This allows you to add a specific item to set the stack size of independant of category stack size.", out custom, listing.ColumnWidth);
                if(custom != null)
                {
                    UpdateStackSizeDict(custom, custom.stackLimit);
                }
                for (int i = 0; i < CachedStackableListing.Count; i++)
                {
                    ThingDef curThing = CachedStackableListing[i];
                    float value = settings.tweak_stackSizeControl[curThing.defName];
                    int max = defaultStackSizes[curThing.defName] * 10;
                    listing.AddLabeledSlider(curThing.LabelCap + ": " + value, ref value, 1, max, "1", max.ToString(), 1);
                    settings.tweak_stackSizeControl[curThing.defName] = Mathf.RoundToInt(value);
                }
            }
        }

        public void DrawRootCategorySetting(Listing_Standard listing)
        {
            ThingCategoryDef curCategory = ThingCategoryDefOf.Root;
            if (displayed.Contains(curCategory))
            {
                return;
            }
            float value = settings.tweak_stackSizeCategories[curCategory.defName];
            listing.AddLabeledSlider("Everything: " + value + "x", ref value, 0.1f, 10f, "0.1x", "10x", 0.1f);
            settings.tweak_stackSizeCategories[curCategory.defName] = Mathf.RoundToInt(value);
            displayed.Add(curCategory);
            foreach (ThingCategoryDef cat in curCategory.childCategories)
            {
                DrawCategorySetting(listing, cat, "-");
            }
        }

        public void DrawCategorySetting(Listing_Standard listing, ThingCategoryDef curCategory, string prefix = "")
        {
            if (displayed.Contains(curCategory))
            {
                return;
            }
            float value = settings.tweak_stackSizeCategories[curCategory.defName];
            listing.AddLabeledSlider(prefix + curCategory.LabelCap + ": " + value + "x", ref value, 0.1f, 10f, "0.1x", "10x", 0.1f);
            settings.tweak_stackSizeCategories[curCategory.defName] = Mathf.RoundToInt(value);
            displayed.Add(curCategory);
            foreach(ThingCategoryDef cat in curCategory.childCategories)
            {
                DrawCategorySetting(listing, cat, prefix + "-");
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
            UpdateThingCategoriesWithNew();
            if (settings.GetBoolSetting(def.defName, false))
            {
                ApplyStackValues(settings.tweak_stackSizeControl);
            }
        }

        public override void DoSectionRestore()
        {
            settings.SetBoolSetting(def.defName, false);
            settings.tweak_stackSizeCategories = defaultCategorySizes;
            settings.tweak_stackSizeControl = new Dictionary<string, int>();
        }

        public void UpdateThingCategoriesWithNew()
        {
            if (settings.tweak_stackSizeCategories.NullOrEmpty())
            {
                settings.tweak_stackSizeCategories = new Dictionary<string, float>();
            }
            foreach (ThingCategoryDef cat in DefDatabase<ThingCategoryDef>.AllDefs)
            {
                if (!settings.tweak_stackSizeCategories.ContainsKey(cat.defName))
                {
                    settings.tweak_stackSizeCategories.Add(cat.defName, 1f);
                }
            }
        }

        public void StoreDefaultValues()
        {
            if (defaultCategorySizes.NullOrEmpty())
            {
                defaultCategorySizes = new Dictionary<string, float>();
            }
            foreach(ThingCategoryDef cat in DefDatabase<ThingCategoryDef>.AllDefs)
            {
                if (!defaultCategorySizes.ContainsKey(cat.defName))
                {
                    defaultCategorySizes.Add(cat.defName, 1f);
                }
            }

            if (defaultStackSizes.NullOrEmpty())
            {
                defaultStackSizes = new Dictionary<string, int>();
            }
            foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1))
            {
                if (!defaultStackSizes.ContainsKey(thing.defName))
                {
                    defaultStackSizes.Add(thing.defName, thing.stackLimit);
                }
            }
        }

        public void ApplyCategoryStackScales()
        {
            if (!settings.tweak_stackSizeCategories.NullOrEmpty())
            {
                foreach(ThingCategoryDef cat in DefDatabase<ThingCategoryDef>.AllDefs)
                {
                    
                }
            }
        }

        public void ApplyStackValues(Dictionary<string, int> source)
        {
            if (!source.NullOrEmpty())
            {
                foreach (ThingDef thing in DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1))
                {
                    if(defaultStackSizes[thing.defName] != source[thing.defName])
                    {
                        SetStackSizeValue(thing, source[thing.defName]);
                    }
                }
            }
        }

        public void UpdateStackSizeDict(ThingDef thing, int value)
        {
            if (settings.tweak_stackSizeControl.NullOrEmpty())
            {
                settings.tweak_stackSizeControl = new Dictionary<string, int>();
            }
            if (!settings.tweak_stackSizeControl.ContainsKey(thing.defName))
            {
                settings.tweak_stackSizeControl.Add(thing.defName, value);
            }
        }

        public void SetStackSizeValue(ThingDef def, int value)
        {
            def.stackLimit = value;
        }
    }
}
