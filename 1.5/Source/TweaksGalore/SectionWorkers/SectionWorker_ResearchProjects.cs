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
            base.DoSectionContents(listing, filter);
            foreach (ResearchProjectDef research in DefDatabase<ResearchProjectDef>.AllDefs)
            {
                DoResearchProjectSettings(listing, research);
            }
        }

        public void DoResearchProjectSettings(Listing_Standard listing, ResearchProjectDef research)
        {
            string categoryString = "Cat_ResearchProject_" + research.defName;
            bool categoryToggle = mod.GetCollapsedCategoryState(categoryString);
            listing.LabelBackedHeader(research.LabelCap, mod.subHeaderColor, ref categoryToggle, GameFont.Small);
            mod.SetCollapsedCategoryState(categoryString, categoryToggle);
            if (!categoryToggle)
            {
                // Tweak: Base Cost
                {
                    float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].baseCost;
                    float baseCostMin = settings.researchProjectSettingsDefaults[research.defName].baseCost / 10f;
                    float baseCostMax = settings.researchProjectSettingsDefaults[research.defName].baseCost * 10f;
                    listing.AddLabeledSlider("TweaksGalore.ResearchCost".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), baseCostMin);
                    settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                }
                // Tweak: Tech Level
                listing.AddLabeledSlider<TechLevel>("TweaksGalore.TechLevelValue".Translate(), ref settings.tweak_researchProjectSettings[research.defName].techLevel);
                //if (ModLister.RoyaltyInstalled)
                //{
                //    // Tweak: Techprint Count
                //    {
                //        float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].techprintCount;
                //        float baseCostMin = 0;
                //        float baseCostMax = 10;
                //        listing.AddLabeledSlider("TweaksGalore.TechprintCount".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), 1f);
                //        settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                //        listing.Note("TweaksGalore.TechprintCountNote".Translate(), GameFont.Tiny);
                //    }
                //    if(settings.tweak_researchProjectSettings[research.defName].techprintCount > 0)
                //    {
                //        // Tweak: Techprint Commonality
                //        {
                //            float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].techprintCommonality;
                //            float baseCostMin = settings.researchProjectSettingsDefaults[research.defName].techprintCommonality / 10f;
                //            float baseCostMax = settings.researchProjectSettingsDefaults[research.defName].techprintCommonality * 10f;
                //            listing.AddLabeledSlider("TweaksGalore.TechprintCommonality".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), baseCostMin);
                //            settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                //        }
                //        // Tweak: Techprint Market Value
                //        {
                //            float baseCostBuffer = settings.tweak_researchProjectSettings[research.defName].techprintMarketValue;
                //            float baseCostMin = settings.researchProjectSettingsDefaults[research.defName].techprintMarketValue / 10f;
                //            float baseCostMax = settings.researchProjectSettingsDefaults[research.defName].techprintMarketValue * 10f;
                //            listing.AddLabeledSlider("TweaksGalore.TechprintMarketValue".Translate(baseCostBuffer.ToString("0")), ref baseCostBuffer, baseCostMin, baseCostMax, "TweaksGalore.SliderMinValue".Translate(baseCostMin), "TweaksGalore.SliderMaxValue".Translate(baseCostMax), baseCostMin);
                //            settings.tweak_researchProjectSettings[research.defName].baseCost = baseCostBuffer;
                //        }
                //        // Tweak: Techprint Tags
                //        {
                //            listing.GapLine();
                //            if (research.heldByFactionCategoryTags.NullOrEmpty()) { listing.Note("TweaksGalore.TechprintNoFactions".Translate()); }
                //            else { listing.Note(research.heldByFactionCategoryTags.ToArray().ToCommaList()); }

                //            if (listing.ButtonText("TweaksGalore.ButtonAdd".Translate()))
                //            {
                //                throw new NotImplementedException();
                //            }
                //            if (listing.ButtonText("TweaksGalore.ButtonRemove".Translate()))
                //            {
                //                throw new NotImplementedException();
                //            }
                //        }
                //    }
                //}
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
                    settings.researchProjectSettingsDefaults.Add(research.defName, MakeNewResearchProjectSetting(research));
                }
                if (!settings.tweak_researchProjectSettings.ContainsKey(research.defName))
                {
                    settings.tweak_researchProjectSettings.Add(research.defName, MakeNewResearchProjectSetting(research));
                }
                ResearchProjectSettings researchSettings = settings.tweak_researchProjectSettings[research.defName];
                research.baseCost = researchSettings.baseCost;
                research.techLevel = researchSettings.techLevel;
                research.techprintCount = researchSettings.techprintCount;
                research.techprintCommonality = researchSettings.techprintCommonality;
                research.techprintMarketValue = researchSettings.techprintMarketValue;
                research.heldByFactionCategoryTags = researchSettings.techprintTags;
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_researchProjectSettings = settings.researchProjectSettingsDefaults;

        }

        public ResearchProjectSettings MakeNewResearchProjectSetting(ResearchProjectDef researchDef)
        {
            ResearchProjectSettings s = new ResearchProjectSettings();
            s.baseCost = researchDef.baseCost;
            s.techLevel = researchDef.techLevel;
            s.techprintCount = researchDef.techprintCount;
            s.techprintCommonality = researchDef.techprintCommonality;
            s.techprintMarketValue = researchDef.techprintMarketValue;
            s.techprintTags = researchDef.heldByFactionCategoryTags;
            return s;
        }

        public ResearchProjectSettings MakeNewResearchProjectSetting(ResearchProjectSettings otherSettings)
        {
            ResearchProjectSettings s = new ResearchProjectSettings();
            s.baseCost = otherSettings.baseCost;
            s.techLevel = otherSettings.techLevel;
            s.techprintCount = otherSettings.techprintCount;
            s.techprintCommonality = otherSettings.techprintCommonality;
            s.techprintMarketValue = otherSettings.techprintMarketValue;
            s.techprintTags = otherSettings.techprintTags;
            return s;
        }
    }
}
