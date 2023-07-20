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
    public static class SettingsPage_Raids
    {
        public static TweaksGaloreMod mod = TweaksGaloreMod.mod;
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Raids(Listing_Standard listing)
        {
            string categoryString = "Cat_General_Raids";
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader("Raids", mod.headerColor, ref categoryToggle);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                if (listing.ButtonTextLabeled("", "Restore defaults"))
                {
                    DefaultUtil.RestoreSettings_Raids(settings);
                    Messages.Message("Tweaks Galore: 'Raids' tweaks restored to defaults. Restart required to take effect.", MessageTypeDefOf.CautionInput);
                    TweaksGaloreMod.mod.restoreRaids = true;
                }
                if (mod.restoreRaids)
                {
                    listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!");
                }
                else
                {
                    listing.LabelBacked("Blocked Raid Strategies", mod.settingColor);
                    listing.Note("This will only function against vanilla versions of the raid types, some modded raids use their own version and will bypass these tweaks.", GameFont.Tiny);
                    listing.CheckboxLabeled("Breach Raids", ref settings.tweak_noMoreBreachRaids);
                    listing.CheckboxLabeled("Drop Pod Raids", ref settings.tweak_noMoreDropPodRaids);
                    listing.CheckboxLabeled("Sapper Raids", ref settings.tweak_noMoreSapperRaids);
                    listing.CheckboxLabeled("Siege Raids", ref settings.tweak_noMoreSiegeRaids);
                    listing.CheckboxLabeled("Cowardly Raids", ref settings.tweak_noCowardlyRaiders);

                    listing.Gap();
                }
            }
        }
    }
}
