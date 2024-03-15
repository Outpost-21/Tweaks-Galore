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
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Power(Listing_Standard listing)
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
