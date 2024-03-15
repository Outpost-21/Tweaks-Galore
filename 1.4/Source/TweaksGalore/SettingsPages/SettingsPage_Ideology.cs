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
    public static class SettingsPage_Ideology
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Ideology(Listing_Standard listing)
        {
            // Tweak: Ancient Deconstruction
            listing.CheckboxEnhanced("Ancient Deconstruction", "Makes the Ancient Ruins added by Ideology and Biotech deconstructable instead of having to destroy them.", ref settings.tweak_ancientDeconstruction);
            if (settings.tweak_ancientDeconstruction)
            {
                listing.CheckboxEnhanced("- Give Proper Materials", "Changes returned items to some reasonable materials instead of just slag.", ref settings.tweak_ancientDeconstruction_mode);
            }
            listing.GapLine();
            // Tweak: Darklight Glow Pods
            listing.CheckboxEnhanced("Darklight Glow Pods", "Makes glow pods spawned in insectoid nests use the darklight colour.", ref settings.tweak_darklightGlowPods);
            listing.GapLine();
            // Tweak: Disable Desired Apparel
            listing.CheckboxEnhanced("Disable Desired Apparel", "Disables default Ideology desired apparel from showing up. I got really fucking sick of this happening when I was testing faction loadouts, whoever came up with this and DIDN'T think to have an option to disable it deserves to step on an upright UK appliance plug.", ref settings.tweak_disableDesiredApparel);
            listing.GapLine();
            // Tweak: No Meme Limit
            listing.CheckboxEnhanced("No Meme Limit", "Raises the limit of how many memes you can choose to 1000...so effectively no limit.", ref settings.patch_noMemeLimit);
            listing.GapLine();
            // Tweak: Proper Suppression
            listing.CheckboxEnhanced("Proper Suppression", "Changes suppression mechanics slightly so that rebellions never happen if your slaves are kept suppressed. Also prevents suppressed pawns from taking part in rebellions when they do happen.", ref settings.patch_properSuppression);
            listing.GapLine();
            // Tweak: Suppression Percentage
            listing.AddLabeledSlider($"Suppression Percentage: {settings.patch_properSuppressionPercentage.ToStringPercent()}", ref settings.patch_properSuppressionPercentage, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
            listing.Note("Controls the percentage at which rebellions are considered suppressed, if Proper Suppression is enabled this is the threshold at which rebellions would be disabled entirely.", GameFont.Tiny, Color.gray);
            listing.GapLine();
            // Tweak: Unlocked Ideology Buildings
            listing.CheckboxEnhanced("Unlocked Ideology Buildings", "Removes the meme restriction on ideology buildings so you can use them regardless of what memes you have. Includes floors and apparel.", ref settings.tweak_unlockedIdeologyBuildings);
            listing.GapLine();
        }
    }
}
