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
    public static class SettingsPage_Power
    {
        public static TweaksGaloreMod mod = TweaksGaloreMod.mod;
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Power(Listing_Standard listing)
        {
            string categoryString = "Cat_General_Power";
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader("Power", mod.headerColor, ref categoryToggle);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                if (listing.ButtonTextLabeled("", "Restore defaults"))
                {
                    DefaultUtil.RestoreSettings_Power(settings);
                    Messages.Message("Tweaks Galore: 'Power' tweaks restored to defaults. Restart required to take effect.", MessageTypeDefOf.CautionInput);
                    TweaksGaloreMod.mod.restorePower = true;
                }
                if (mod.restorePower)
                {
                    listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!");
                }
                else
                {
                    // Tweak: Power Tweaks
                    listing.CheckboxEnhanced("Power Usage Tweaks", "Allows tweaking the usage rate of some buildings. If they usually produce then it configures how much they output instead.", ref settings.tweak_powerUsageTweaks);
                    if (settings.tweak_powerUsageTweaks)
                    {
                        listing.AddLabeledSlider("- Standing Lamp: " + settings.tweak_powerUsage_lamp, ref settings.tweak_powerUsage_lamp, 0f, 100f, "Min: 0", "Max: 100", 1f);
                        listing.AddLabeledSlider("- Sun Lamp: " + settings.tweak_powerUsage_sunlamp, ref settings.tweak_powerUsage_sunlamp, 0f, 10000f, "Min: 0", "Max: 10000", 50f);
                        listing.AddLabeledSlider("- Auto-Door: " + settings.tweak_powerUsage_autodoor, ref settings.tweak_powerUsage_autodoor, 0f, 100f, "Min: 0", "Max: 100", 1f);
                        listing.AddLabeledSlider("- Vanometric Power Cell: " + settings.tweak_powerUsage_vanometricCell, ref settings.tweak_powerUsage_vanometricCell, 0f, 10000f, "Min: 0", "Max: 10000", 50f);
                    }
                    listing.GapLine();
                }
            }
        }
    }
}
