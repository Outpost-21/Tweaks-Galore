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
    public class SectionWorker_ResearchProjects : SectionWorker
    {
        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            foreach (ResearchProjectDef research in DefDatabase<ResearchProjectDef>.AllDefs)
            {
                DoResearchProjectSettings(listing, research);
            }
            DoOnStartup();
        }

        public void DoResearchProjectSettings(Listing_Standard listing, ResearchProjectDef research)
        {
            string categoryString = "Cat_ResearchProject_" + research.defName;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(research.LabelCap, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                if (!research.heldByFactionCategoryTags.NullOrEmpty())
                {
                    listing.Note($"Faction Categories: {research.heldByFactionCategoryTags.ToArray().ToCommaList()}", GameFont.Tiny, Color.gray);
                    listing.GapLine();
                }
                else
                {
                    listing.Note("No factions hold techprints for this research.");
                }
                // Tweak: Base Cost
                {
                    float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].baseCost;
                    float baseCostMin = settings.researchProjectSettingsDefaults[research.defName].baseCost / 10f;
                    float baseCostMax = settings.researchProjectSettingsDefaults[research.defName].baseCost * 10f;
                    listing.AddLabeledSlider("TweaksGalore.ResearchCost".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), baseCostMin);
                    settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                }
                // Tweak: Tech Level
                listing.TechLevelMenu("TweaksGalore.TechLevelValue".Translate(), "", settings.tweak_researchProjectSettings[research.defName].techLevel, listing.ColumnWidth);
                // Tweak: Techprint Count
                {
                    float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].techprintCount;
                    float baseCostMin = 0;
                    float baseCostMax = 10;
                    listing.AddLabeledSlider("TweaksGalore.TechprintCount".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), 1f);
                    settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                    listing.Note("TweaksGalore.TechprintCountNote".Translate(), GameFont.Tiny);
                }
                // Tweak: Techprint Commonality
                {
                    float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].techprintCommonality;
                    float baseCostMin = settings.researchProjectSettingsDefaults[research.defName].techprintCommonality / 10f;
                    float baseCostMax = settings.researchProjectSettingsDefaults[research.defName].techprintCommonality * 10f;
                    listing.AddLabeledSlider("TweaksGalore.TechprintCommonality".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), baseCostMin);
                    settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                }
                // Tweak: Techprint Market Value
                {
                    float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].techprintMarketValue;
                    float baseCostMin = settings.researchProjectSettingsDefaults[research.defName].techprintMarketValue / 10f;
                    float baseCostMax = settings.researchProjectSettingsDefaults[research.defName].techprintMarketValue * 10f;
                    listing.AddLabeledSlider("TweaksGalore.TechprintMarketValue".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), baseCostMin);
                    settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                }
                // Tweak: Techprint Held By Empire
                // Tweak: Techprint Held By Outlanders
                listing.Gap();
            }
        }

        public override void DoOnStartup()
        {
            if (settings.researchProjectSettingsDefaults.NullOrEmpty())
            {
                settings.researchProjectSettingsDefaults = new Dictionary<string, ResearchProjectSettings>();
            }
            if (settings.tweak_researchProjectSettings.NullOrEmpty())
            {
                settings.tweak_researchProjectSettings = new Dictionary<string, ResearchProjectSettings>();
            }
            foreach (ResearchProjectDef research in DefDatabase<ResearchProjectDef>.AllDefs)
            {
                if (!settings.researchProjectSettingsDefaults.ContainsKey(research.defName))
                {
                    settings.researchProjectSettingsDefaults.Add(research.defName, MakeNewRoyalPermitSetting(research));
                }
                if (!settings.tweak_researchProjectSettings.ContainsKey(research.defName))
                {
                    settings.tweak_researchProjectSettings.Add(research.defName, MakeNewRoyalPermitSetting(research));
                }
                ResearchProjectSettings researchSettings = settings.tweak_researchProjectSettings[research.defName];
                research.baseCost = researchSettings.baseCost;
                research.techLevel = researchSettings.techLevel;
                research.techprintCount = researchSettings.techprintCount;
                research.techprintCommonality = researchSettings.techprintCommonality;
                research.techprintMarketValue = researchSettings.techprintMarketValue;
            }
        }

        public ResearchProjectSettings MakeNewRoyalPermitSetting(ResearchProjectDef permit)
        {
            ResearchProjectSettings s = new ResearchProjectSettings();
            s.baseCost = permit.baseCost;
            s.techLevel = permit.techLevel;
            s.techprintCount = permit.techprintCount;
            s.techprintCommonality = permit.techprintCommonality;
            s.techprintMarketValue = permit.techprintMarketValue;
            s.heldByEmpire = permit.heldByFactionCategoryTags?.Contains("Empire") ?? false;
            s.heldByOutlanders = permit.heldByFactionCategoryTags?.Contains("Outlander") ?? false;
            return s;
        }
    }
}
