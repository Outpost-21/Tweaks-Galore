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
    public static class TweakUtil
    {
        public static void DoSetting(this Listing_Standard listing, TweakDef def, string filter = null)
        {
            switch (def.tweakType)
            {
                case TweakType.Bool:
                    listing.DoSettingBool(def.LabelCap, def.Worker.Description(), def.defName, def.DefaultBool, def.formatting?.enhanced);
                    break;
                case TweakType.Int:
                    listing.DoSettingInt(def.LabelCap, def.Worker.Description(), def.defName, def.DefaultInt, def.intRange.min, def.intRange.max, def.intIncrement);
                    break;
                case TweakType.IntRange:
                    listing.DoSettingIntRange(def.LabelCap, def.Worker.Description(), def.defName, def.DefaultIntRange, def.intRange.min, def.intRange.max, def.intIncrement);
                    break;
                case TweakType.Float:
                    listing.DoSettingFloat(def.LabelCap, def.Worker.Description(), def.defName, def.DefaultFloat, def.floatRange.min, def.floatRange.max, def.floatIncrement, def.formatting?.percentage);
                    break;
                case TweakType.FloatRange:
                    //listing.DoSettingFloatRange(def.LabelCap, def.Worker.Description(), def.defName, def.DefaultFloatRange, def.floatRange.min, def.floatRange.max, def.floatIncrement, def.formatting?.percentage);
                    break;
                default:
                    break;
            }
            if (def.formatting?.gapLine ?? false)
            {
                listing.GapLine();
            }
        }


        /// <summary>
        /// Sets up the UI appropriate for a boolean input.
        /// </summary>
        public static void DoSettingBool(this Listing_Standard listing, string label, string description, string key, bool defaultValue = false, bool? enhanced = null)
        {
            bool tempBool = TweaksGaloreMod.settings.GetBoolSetting(key, defaultValue);
            if(enhanced ?? false)
            {
                listing.CheckboxEnhanced(label, description, ref tempBool);
            }
            else
            {
                listing.CheckboxLabeled(label, ref tempBool, description);
            }
            TweaksGaloreMod.settings.SetBoolSetting(key, tempBool);
        }

        /// <summary>
        /// Sets up the UI appropriate for a float input.
        /// </summary>
        public static void DoSettingFloat(this Listing_Standard listing, string label, string description, string key, float original, float min, float max, float? increment = null, bool? percentage = false)
        {
            float tempfloat = (float)Math.Round(TweaksGaloreMod.settings.GetFloatSetting(key, original), 2);
            listing.AddLabeledSlider(label + ": " + ((bool)percentage ? tempfloat.ToStringPercent() : tempfloat.ToString("0.00")), ref tempfloat, min, max, $"Min: {((bool)percentage ? min.ToStringPercent() : min.ToString())}", $"Max: {((bool)percentage ? max.ToStringPercent() : max.ToString())}", increment ?? Mathf.Max(0.01f, (max - min) / 100f));
            TweaksGaloreMod.settings.SetFloatSetting(key, tempfloat);
        }

        /// <summary>
        /// Sets up the UI appropriate for a integer input.
        /// </summary>
        public static void DoSettingInt(this Listing_Standard listing, string label, string description, string key, int original, int min, int max, int? increment = null)
        {
            float tempInt = TweaksGaloreMod.settings.GetIntSetting(key, original);
            listing.AddLabeledSlider(label + ": " + tempInt.ToString(), ref tempInt, min, max, $"Min: {min}", $"Max: {max}", increment ?? Mathf.RoundToInt(Mathf.Max(1f, (max - min) / 100f)));
            TweaksGaloreMod.settings.SetIntSetting(key, Mathf.RoundToInt(tempInt));
        }

        /// <summary>
        /// Sets up the UI appropriate for a integer range input.
        /// </summary>
        public static void DoSettingIntRange(this Listing_Standard listing, string label, string description, string key, IntRange original, int min, int max, int? increment = null)
        {
            listing.Label(label);
            if (!description.NullOrEmpty())
            {
                listing.Note(description, GameFont.Tiny);
            }
            IntRange tempIntRange = TweaksGaloreMod.settings.GetIntRangeSetting(key, original);
            listing.IntRange(ref tempIntRange, min, max);
            TweaksGaloreMod.settings.SetIntRangeSetting(key, tempIntRange);
        }

        public static void StartSection(this Listing_Standard listing, string label, string section, out bool toggle, Action setDefaults = null)
        {
            bool categoryToggle = TweaksGaloreMod.mod.GetCollapsedCategoryState(section);
            listing.LabelBackedHeader(label, TweaksGaloreMod.mod.headerColor, ref categoryToggle);
            TweaksGaloreMod.mod.SetCollapsedCategoryState(section, categoryToggle);
            if (!categoryToggle)
            {
                if (listing.ButtonTextLabeled("", "Restore Section Defaults"))
                {
                    setDefaults.Invoke();
                }
            }
            toggle = categoryToggle;
        }

        public static void DoSection(this Listing_Standard listing, TweakSectionDef def, string filter)
        {
            bool categoryToggle = TweaksGaloreMod.mod.GetCollapsedCategoryState(def.defName);
            listing.LabelBackedHeader(def.LabelCap, TweaksGaloreMod.mod.headerColor, ref categoryToggle);
            TweaksGaloreMod.mod.SetCollapsedCategoryState(def.defName, categoryToggle);
            if (!categoryToggle)
            {
                // Do Worker Contents
                def.Worker.DoSectionContents(listing, filter);
                listing.GapLine();
            }
        }

        public static void DoSubSection(this Listing_Standard listing, TweakSubSectionDef def)
        {
            bool categoryToggle = TweaksGaloreMod.mod.GetCollapsedCategoryState(def.defName);
            listing.LabelBackedHeader(def.LabelCap, TweaksGaloreMod.mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            TweaksGaloreMod.mod.SetCollapsedCategoryState(def.defName, categoryToggle);
            if (!categoryToggle)
            {
                listing.Note(def.description, GameFont.Tiny);
                // Get Sub-Section Tweaks
                IEnumerable<TweakDef> tweakIEnumerable = from x in def.heldTweaks
                                                         where (x.required.NullOrEmpty() || x.required.All(r => ModLister.GetActiveModWithIdentifier(r) != null)) 
                                                         && (x.incompatible.NullOrEmpty() || x.incompatible.All(i => ModLister.GetActiveModWithIdentifier(i) == null))
                                                         select x;
                List<TweakDef> tweakList = tweakIEnumerable.ToList();
                // Do Individual Tweaks
                for (int i = 0; i < tweakList.Count; i++)
                {
                    tweakList[i].Worker.DoTweakContents(listing);
                }
            }
        }

        public static bool FilterForTweak(this TweakSectionDef def, string filter)
        {
            if (def.heldTweaks?.Any(t => t.FilterForTweak(filter)) ?? false)
            {
                return true;
            }
            if (def.heldSubSections?.Any(ss => ss.FilterForTweak(filter)) ?? false)
            {
                return true;
            }
            return false;
        }

        public static bool FilterForTweak(this TweakSubSectionDef def, string filter)
        {
            if (def.heldTweaks?.Any(t => t.FilterForTweak(filter)) ?? false)
            {
                return true;
            }
            return false;
        }

        public static bool FilterForTweak(this TweakDef def, string filter)
        {
            if ((def.label?.ToLower().Contains(filter.ToLower()) ?? false) || (def.description?.ToLower().Contains(filter.ToLower()) ?? false))
            {
                return true;
            }
            return false;
        }

        public static List<TweakDef> GetAllNestedTweaks(this TweakSectionDef def)
        {
            List<TweakDef> result = new List<TweakDef>();

            if (!def.heldTweaks.NullOrEmpty())
            {
                foreach (TweakDef tweakDef in def.heldTweaks)
                {
                    if (!result.Contains(tweakDef))
                    {
                        result.Add(tweakDef);
                    }
                }
            }

            if (!def.heldSubSections.NullOrEmpty())
            {
                foreach (TweakSubSectionDef subSection in def.heldSubSections)
                {
                    if (!subSection.heldTweaks.NullOrEmpty())
                    {
                        foreach (TweakDef tweakDef in subSection.heldTweaks)
                        {
                            if (!result.Contains(tweakDef))
                            {
                                result.Add(tweakDef);
                            }
                        }
                    }
                        
                }
            }

            return result;
        }

        public static void TitleFloatMenu(this Listing_Standard listing, string name, string minTitle, RoyalTitlePermitDef permit)
        {
            float curHeight = listing.CurHeight;
            Rect rect = listing.GetRect(Text.LineHeight + listing.verticalSpacing);
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect, name);
            Text.Anchor = TextAnchor.MiddleRight;

            Rect rect2 = new Rect(listing.ColumnWidth - 150f, curHeight, 150f, 29f);
            string label = DefDatabase<RoyalTitleDef>.GetNamedSilentFail(minTitle)?.LabelCap ?? "No Title";
            if (Widgets.ButtonText(rect2, label))
            {
                Find.WindowStack.Add(new FloatMenu(DoDropdown_TitleSelector(minTitle, permit)));
            }
            Text.Anchor = anchor;

            rect = listing.GetRect(0f);
            rect.height = listing.CurHeight - curHeight;
            rect.y -= rect.height;
            GUI.color = Color.white;
            listing.Gap(6f);
        }

        public static List<FloatMenuOption> DoDropdown_TitleSelector(string minTitle, RoyalTitlePermitDef permit)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            list.Add(new FloatMenuOption("No Title", delegate () { TweaksGaloreMod.settings.tweak_royalPermitSettings[permit.defName].minTitle = string.Empty; }, orderInPriority: 1));
            foreach (RoyalTitleDef title in DefDatabase<RoyalTitleDef>.AllDefs)
            {
                if (title.tags.Any(tag => permit.faction.royalTitleTags.Contains(tag)) && title.Awardable)
                {
                    list.Add(new FloatMenuOption(title.LabelCap, delegate () { TweaksGaloreMod.settings.tweak_royalPermitSettings[permit.defName].minTitle = title.defName; }, orderInPriority: -title.seniority));
                }
            }
            return list;
        }

        public static void SetPowerUsage(this ThingDef thing, int powerConsumption)
        {
            if (thing != null)
            {
                CompProperties_Power comp = thing.GetCompProperties<CompProperties_Power>();
                if (comp != null)
                {
                    thing.GetCompProperties<CompProperties_Power>().basePowerConsumption = powerConsumption;
                }
            }
        }

        public static bool ShouldRunTweak(this TweakDef tweak)
        {
            if (tweak.incompatible.Any(inc => ModLister.AllInstalledMods.Any(mod => mod.PackageId == inc)))
            {
                return false;
            }
            return true;
        }
    }
}
