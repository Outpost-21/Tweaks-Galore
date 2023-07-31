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
    public class SectionWorker_RoyalPermits : SectionWorker
    {
        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            foreach (RoyalTitlePermitDef permit in DefDatabase<RoyalTitlePermitDef>.AllDefs)
            {
                if (permit.faction != null)
                {
                    DoPermitSettings(listing, permit);
                }
            }
            DoOnStartup();
        }

        public void DoPermitSettings(Listing_Standard listing, RoyalTitlePermitDef permit)
        {
            string categoryString = "Cat_RoyaltyPermit_" + permit.defName;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(permit.LabelCap, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                listing.Note($"Faction: {permit.faction.LabelCap}", GameFont.Tiny, Color.gray);
                // Tweak: Minimum Title
                string minTitleBuffer = settings.tweak_royalPermitSettings[permit.defName].minTitle;
                listing.TitleFloatMenu("- Minimum Title", minTitleBuffer, permit);
                settings.tweak_royalPermitSettings[permit.defName].minTitle = minTitleBuffer;
                // Tweak: Permit Point Cost
                float permitPointBuffer = settings.tweak_royalPermitSettings[permit.defName].permitPointCost;
                listing.AddLabeledSlider($"- Permit Point Cost: {permitPointBuffer.ToString("0")}", ref permitPointBuffer, 1f, 20f, "Min: 1", "Max: 20", 1f);
                settings.tweak_royalPermitSettings[permit.defName].permitPointCost = permitPointBuffer;
                // Tweak: Cooldown Days
                float cooldownDaysBuffer = settings.tweak_royalPermitSettings[permit.defName].cooldownDays;
                listing.AddLabeledSlider($"- Cooldown Days: {cooldownDaysBuffer.ToString("0.0")}", ref cooldownDaysBuffer, 0.5f, 100f, "Min: 0.5", "Max: 100", 0.5f);
                settings.tweak_royalPermitSettings[permit.defName].cooldownDays = cooldownDaysBuffer;
                if (permit.royalAid != null)
                {
                    // Tweak: Aid Favor Cost
                    float favorCostBuffer = settings.tweak_royalPermitSettings[permit.defName].favorCost;
                    listing.AddLabeledSlider($"- Aid Favor Cost: {favorCostBuffer.ToString("0")}", ref favorCostBuffer, 0f, 20f, "Min: 0", "Max: 20", 1f);
                    settings.tweak_royalPermitSettings[permit.defName].favorCost = favorCostBuffer;
                    if (permit.royalAid.pawnKindDef != null)
                    {
                        // Tweak: Aid Pawn Count
                        float aidPawnCountBuffer = settings.tweak_royalPermitSettings[permit.defName].pawnCount;
                        listing.AddLabeledSlider($"- Aid Pawn Count: {aidPawnCountBuffer.ToString("0")}", ref aidPawnCountBuffer, 0f, 20f, "Min: 0", "Max: 20", 1f);
                        settings.tweak_royalPermitSettings[permit.defName].pawnCount = aidPawnCountBuffer;
                        // Tweak: Aid Duration
                        float aidDurationBuffer = settings.tweak_royalPermitSettings[permit.defName].aidDurationDays;
                        listing.AddLabeledSlider($"- Aid Duration Days: {aidDurationBuffer.ToString("0.0")}", ref aidDurationBuffer, 0f, 20f, "Min: 0", "Max: 20", 1f);
                        settings.tweak_royalPermitSettings[permit.defName].aidDurationDays = aidDurationBuffer;
                    }
                }
                listing.Gap();
            }
        }

        public override void DoOnStartup()
        {
            if (settings.royalPermitSettingsDefaults.NullOrEmpty())
            {
                settings.royalPermitSettingsDefaults = new Dictionary<string, RoyalPermitSettings>();
            }
            if (settings.tweak_royalPermitSettings.NullOrEmpty())
            {
                settings.tweak_royalPermitSettings = new Dictionary<string, RoyalPermitSettings>();
            }
            foreach (RoyalTitlePermitDef permit in DefDatabase<RoyalTitlePermitDef>.AllDefs)
            {
                if (permit.faction != null)
                {
                    if (!settings.royalPermitSettingsDefaults.ContainsKey(permit.defName))
                    {
                        settings.royalPermitSettingsDefaults.Add(permit.defName, MakeNewRoyalPermitSetting(permit));
                    }
                    if (!settings.tweak_royalPermitSettings.ContainsKey(permit.defName))
                    {
                        settings.tweak_royalPermitSettings.Add(permit.defName, MakeNewRoyalPermitSetting(permit));
                    }
                    RoyalPermitSettings permitSettings = settings.tweak_royalPermitSettings[permit.defName];
                    RoyalTitleDef minTitle = DefDatabase<RoyalTitleDef>.GetNamed(permitSettings.minTitle);
                    if (minTitle != null)
                    {
                        minTitle = permit.minTitle;
                    }
                    permit.minTitle = minTitle;
                    permit.permitPointCost = Mathf.RoundToInt(permitSettings.permitPointCost);
                    permit.cooldownDays = permitSettings.cooldownDays;
                    if (permit.royalAid != null)
                    {
                        permit.royalAid.favorCost = Mathf.RoundToInt(permitSettings.favorCost);
                        permit.royalAid.pawnCount = Mathf.RoundToInt(permitSettings.pawnCount);
                        permit.royalAid.aidDurationDays = Mathf.RoundToInt(permitSettings.aidDurationDays);
                    }
                }
            }
        }

        public RoyalPermitSettings MakeNewRoyalPermitSetting(RoyalTitlePermitDef permit)
        {
            RoyalPermitSettings s = new RoyalPermitSettings();
            s.minTitle = permit.minTitle.defName;
            s.permitPointCost = permit.permitPointCost;
            s.cooldownDays = permit.cooldownDays;
            if (permit.royalAid != null)
            {
                s.favorCost = permit.royalAid.favorCost;
                s.pawnCount = permit.royalAid.pawnCount;
                s.aidDurationDays = permit.royalAid.aidDurationDays;
            }
            return s;
        }
    }
}
