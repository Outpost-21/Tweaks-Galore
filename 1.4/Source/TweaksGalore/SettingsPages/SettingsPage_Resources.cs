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
    public static class SettingsPage_Resources
    {
        public static TweaksGaloreMod mod = TweaksGaloreMod.mod;
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Resources(Listing_Standard listing)
        {
            string categoryString = "Cat_General_Resources";
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader("Resources", mod.headerColor, ref categoryToggle);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                if (listing.ButtonTextLabeled("", "Restore defaults"))
                {
                    DefaultUtil.RestoreSettings_Resources(settings);
                    Messages.Message("Tweaks Galore: 'Resources' tweaks restored to defaults. Restart required to take effect.", MessageTypeDefOf.CautionInput);
                    TweaksGaloreMod.mod.restoreResources = true;
                }
                if (mod.restoreResources)
                {
                    listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!");
                }
                else
                {
                    Listing_Standard subListing = listing.BeginSection(mod.GetSubStandardHeight(categoryString));
                    //subListing.CheckboxLabeled("Metal Doesn't Burn", ref settings.tweak_metalDoesntBurn, "Removes flammability from Steel & Plasteel, setting gold and silver slightly above 0 so they can still 'melt'.");
                    subListing.CheckboxLabeled("No Mineable Components", ref settings.tweak_noMineableComponents, "Removes components from map generation.");
                    subListing.CheckboxLabeled("No Mineable Plasteel", ref settings.tweak_noMineablePlasteel, "Removes plasteel from map generation.");
                    subListing.CheckboxLabeled("Old Component Names", ref settings.tweak_oldComponentNames, "Back in my day we had industrial and spacer components! Now you can too!");
                    subListing.CheckboxLabeled("Pretty Precious Metals", ref settings.tweak_prettyPreciousMetals, "Changes the beauty of gold and silver (the item, not as a material) from -4 to 4.");
                    subListing.CheckboxLabeled("Stronger Steel", ref settings.tweak_strongerSteel, "Doubles the durability of steel as a material.");

                    subListing.LabelBacked("Stackable Chunks", mod.settingColor);
                    subListing.Note("Allows stone chunks and slag to be stackable. Default: 5", GameFont.Tiny);
                    subListing.CheckboxLabeled("Enabled", ref settings.tweak_stackableChunks);
                    subListing.AddLabeledSlider("Stone Chunk Stack Size: " + settings.tweak_stackableChunks_stone, ref settings.tweak_stackableChunks_stone, 1f, 400f, "Min: 1", "Max: 400", 1f);
                    subListing.AddLabeledSlider("Slag Chunk Stack Size: " + settings.tweak_stackableChunks_slag, ref settings.tweak_stackableChunks_slag, 1f, 400f, "Min: 1", "Max: 400", 1f);
                    mod.SetSubStandardHeight(categoryString, subListing.CurHeight);
                    listing.EndSection(subListing);
                    listing.Gap();
                }
            }
        }
    }
}
