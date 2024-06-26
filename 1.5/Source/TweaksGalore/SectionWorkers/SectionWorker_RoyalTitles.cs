﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
    public class SectionWorker_RoyalTitles : SectionWorker
    {
        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            base.DoSectionContents(listing, filter);
            foreach (RoyalTitleDef title in DefDatabase<RoyalTitleDef>.AllDefs)
            {
                if (!title.tags.NullOrEmpty() && title.Awardable)
                {
                    DoTitleSettings(listing, title);
                }
            }
            DoOnStartup();
        }

        public void DoTitleSettings(Listing_Standard listing, RoyalTitleDef title)
        {
            string categoryString = "Cat_RoyaltyTitle_" + title.defName;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(title.LabelCap, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                listing.Note("TweaksGalore.TitleTags".Translate(title.tags.ToCommaList()), GameFont.Tiny, Color.gray);
                // Tweak: Favor Cost
                float favorCostBuffer = settings.tweak_royalTitleSettings[title.defName].favorCost;
                listing.AddLabeledSlider("TweaksGalore.TitleFavorCost".Translate(favorCostBuffer.ToString("0")), ref favorCostBuffer, 1f, 350f, "Min: 1", "Max: 350", 1f);
                settings.tweak_royalTitleSettings[title.defName].favorCost = favorCostBuffer;
                // Tweak: Heir Quest Points
                float heirCostBuffer = settings.tweak_royalTitleSettings[title.defName].heirQuestPoints;
                listing.AddLabeledSlider("TweaksGalore.TitleHeirQuestPoints".Translate(heirCostBuffer.ToString("0")), ref heirCostBuffer, 100f, 6000f, "Min: 100", "Max: 6000", 100f);
                settings.tweak_royalTitleSettings[title.defName].heirQuestPoints = heirCostBuffer;
                // Tweak: Permit Points
                float permitPointBuffer = settings.tweak_royalTitleSettings[title.defName].permitPoints;
                listing.AddLabeledSlider("TweaksGalore.TitlePermitPoints".Translate(permitPointBuffer.ToString("0")), ref permitPointBuffer, 0f, 20f, "Min: 0", "Max: 20", 1f);
                settings.tweak_royalTitleSettings[title.defName].permitPoints = permitPointBuffer;
                listing.Gap();
                // Tweak: Allow Dignified Meditation
                bool dignifiedMeditationBuffer = settings.tweak_royalTitleSettings[title.defName].dignifiedMeditation;
                listing.CheckboxEnhanced("TweaksGalore.TitleDignifiedMeditation".Translate(), "TweaksGalore.TitleDignifiedMeditationDesc".Translate(), ref dignifiedMeditationBuffer);
                settings.tweak_royalTitleSettings[title.defName].dignifiedMeditation = dignifiedMeditationBuffer;
                // Tweak: Force Enable Work
                bool forceEnableWork = settings.tweak_royalTitleSettings[title.defName].enableWork;
                listing.CheckboxEnhanced("TweaksGalore.TitleNoDisabledWork".Translate(), "TweaksGalore.TitleNoDisabledWorkDesc".Translate(), ref forceEnableWork);
                settings.tweak_royalTitleSettings[title.defName].enableWork = forceEnableWork;
                if (settings.royalTitleSettingsDefaults[title.defName].hasBedroomReqs)
                {
                    // Tweak: Disable Bedroom Requirements
                    bool disableBedroomReqs = (bool)settings.tweak_royalTitleSettings[title.defName].disableBedroomRequirements;
                    listing.CheckboxEnhanced("TweaksGalore.TitleDisableBedroomReqs".Translate(), "TweaksGalore.TitleDisableBedroomReqsDesc".Translate(), ref disableBedroomReqs);
                    settings.tweak_royalTitleSettings[title.defName].disableBedroomRequirements = disableBedroomReqs;
                }
                if (settings.royalTitleSettingsDefaults[title.defName].hasThroneroomReqs)
                {
                    // Tweak: Disable Throneroom Requirements
                    bool disableThroneroomReqs = (bool)settings.tweak_royalTitleSettings[title.defName].disableThroneroomRequirements;
                    listing.CheckboxEnhanced("TweaksGalore.TitleDisableThroneroomReqs".Translate(), "TweaksGalore.TitleDisableThroneroomReqsDesc".Translate(), ref disableThroneroomReqs);
                    settings.tweak_royalTitleSettings[title.defName].disableThroneroomRequirements = disableThroneroomReqs;
                }
                listing.Gap();
            }
        }

        public override void DoOnStartup()
        {
            if (settings.royalTitleSettingsDefaults.NullOrEmpty())
            {
                settings.royalTitleSettingsDefaults = new Dictionary<string, RoyalTitleSettings>();
            }
            if (settings.tweak_royalTitleSettings.NullOrEmpty())
            {
                settings.tweak_royalTitleSettings = new Dictionary<string, RoyalTitleSettings>();
            }
            foreach (RoyalTitleDef title in DefDatabase<RoyalTitleDef>.AllDefs)
            {
                if (title.Awardable && title.tags != null)
                {
                    if (!settings.royalTitleSettingsDefaults.ContainsKey(title.defName))
                    {
                        settings.royalTitleSettingsDefaults.Add(title.defName, MakeNewRoyalTitleSetting(title));
                    }
                    if (!settings.tweak_royalTitleSettings.ContainsKey(title.defName))
                    {
                        settings.tweak_royalTitleSettings.Add(title.defName, MakeNewRoyalTitleSetting(title));
                    }
                    RoyalTitleSettings titleSettings = settings.tweak_royalTitleSettings[title.defName];
                    title.favorCost = Mathf.RoundToInt(titleSettings.favorCost);
                    title.changeHeirQuestPoints = Mathf.RoundToInt(titleSettings.heirQuestPoints);
                    title.permitPointsAwarded = Mathf.RoundToInt(titleSettings.permitPoints);
                    title.allowDignifiedMeditationFocus = titleSettings.dignifiedMeditation;
                    if (titleSettings.enableWork)
                    {
                        title.disabledWorkTags = WorkTags.None;
                    }
                    else
                    {
                        title.disabledWorkTags = titleSettings.disabledWorkTags;
                    }
                    if (titleSettings.disableThroneroomRequirements == null) { titleSettings.disableThroneroomRequirements = false; }
                    else if (titleSettings.disableThroneroomRequirements == true) { title.throneRoomRequirements = null; }
                    if (titleSettings.disableBedroomRequirements == null) { titleSettings.disableBedroomRequirements = false; }
                    else if (titleSettings.disableBedroomRequirements == true) { title.bedroomRequirements = null; }
                }
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_royalTitleSettings = settings.royalTitleSettingsDefaults;
        }

        public RoyalTitleSettings MakeNewRoyalTitleSetting(RoyalTitleDef title)
        {
            return new RoyalTitleSettings()
            {
                favorCost = title.favorCost,
                heirQuestPoints = title.changeHeirQuestPoints,
                permitPoints = title.permitPointsAwarded,
                dignifiedMeditation = title.allowDignifiedMeditationFocus,
                enableWork = false,
                disabledWorkTags = title.disabledWorkTags,
                hasBedroomReqs = title.bedroomRequirements != null,
                hasThroneroomReqs = title.throneRoomRequirements != null
            };
        }
    }
}
