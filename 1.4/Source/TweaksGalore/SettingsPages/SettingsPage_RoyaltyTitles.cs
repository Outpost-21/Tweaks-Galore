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
    public static class SettingsPage_RoyaltyTitles
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_RoyaltyTitles(Listing_Standard listing)
        {
            foreach (RoyalTitleDef title in DefDatabase<RoyalTitleDef>.AllDefs)
            {
                if (!title.tags.NullOrEmpty() && title.Awardable)
                {
                    DoTitleSettings(listing, title);
                }
            }

            TweaksGaloreStartup.Tweak_RoyaltyTitleTweaksStartup(settings);
        }

        public static void DoTitleSettings(Listing_Standard listing, RoyalTitleDef title)
        {
            listing.Label(title.LabelCap);
            listing.GapLine();
            listing.Note($"Tags: {title.tags.ToCommaList()}", GameFont.Tiny, Color.gray);
            // Tweak: Favor Cost
            float favorCostBuffer = settings.tweak_royalTitleSettings[title.defName].favorCost;
            listing.AddLabeledSlider($"- Favor Cost: {favorCostBuffer.ToString("0")}", ref favorCostBuffer, 1f, 350f, "Min: 1", "Max: 350", 1f);
            settings.tweak_royalTitleSettings[title.defName].favorCost = favorCostBuffer;
            // Tweak: Heir Quest Points
            float heirCostBuffer = settings.tweak_royalTitleSettings[title.defName].heirQuestPoints;
            listing.AddLabeledSlider($"- Heir Quest Points: {heirCostBuffer.ToString("0")}", ref heirCostBuffer, 100f, 6000f, "Min: 100", "Max: 6000", 100f);
            settings.tweak_royalTitleSettings[title.defName].heirQuestPoints = heirCostBuffer;
            // Tweak: Permit Points
            float permitPointBuffer = settings.tweak_royalTitleSettings[title.defName].permitPoints;
            listing.AddLabeledSlider($"- Permit Points: {permitPointBuffer.ToString("0")}", ref permitPointBuffer, 0f, 20f, "Min: 0", "Max: 20", 1f);
            settings.tweak_royalTitleSettings[title.defName].permitPoints = permitPointBuffer;
            listing.Gap();
            // Tweak: Allow Dignified Meditation
            bool dignifiedMeditationBuffer = settings.tweak_royalTitleSettings[title.defName].dignifiedMeditation;
            listing.CheckboxEnhanced("- Enable Dignified Meditation", "If enabled, this title activates Dignified Meditation for the pawn, allowing them to meditate on a throne.", ref dignifiedMeditationBuffer);
            settings.tweak_royalTitleSettings[title.defName].dignifiedMeditation = dignifiedMeditationBuffer;
            // Tweak: Force Enable Work
            bool forceEnableWork = settings.tweak_royalTitleSettings[title.defName].enableWork;
            listing.CheckboxEnhanced("- No Disabled Work", "If enabled, clears any disabled work from the title.", ref forceEnableWork);
            settings.tweak_royalTitleSettings[title.defName].enableWork = forceEnableWork;
            if (settings.royalTitleSettingsDefaults[title.defName].hasBedroomReqs)
            {
                // Tweak: Disable Bedroom Requirements
                bool disableBedroomReqs = (bool)settings.tweak_royalTitleSettings[title.defName].disableBedroomRequirements;
                listing.CheckboxEnhanced("- Disable Bedroom Requirements", "If enabled, clears any requirements for bedrooms from the title.", ref disableBedroomReqs);
                settings.tweak_royalTitleSettings[title.defName].disableBedroomRequirements = disableBedroomReqs;
            }
            if (settings.royalTitleSettingsDefaults[title.defName].hasThroneroomReqs)
            {
                // Tweak: Disable Throneroom Requirements
                bool disableThroneroomReqs = (bool)settings.tweak_royalTitleSettings[title.defName].disableThroneroomRequirements;
                listing.CheckboxEnhanced("- Disable Throneroom Requirements", "If enabled, clears any requirements for thronerooms from the title.", ref disableThroneroomReqs);
                settings.tweak_royalTitleSettings[title.defName].disableThroneroomRequirements = disableThroneroomReqs;
            }
            listing.GapLine();
        }
    }
}
