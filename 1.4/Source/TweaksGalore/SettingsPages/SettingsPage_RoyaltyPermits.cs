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
    public static class SettingsPage_RoyaltyPermits
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_RoyaltyPermits(Listing_Standard listing)
        {
            foreach (RoyalTitlePermitDef permit in DefDatabase<RoyalTitlePermitDef>.AllDefs)
            {
                if (permit.faction != null)
                {
                    DoPermitSettings(listing, permit);
                }
            }

            TweaksGaloreStartup.Tweak_RoyaltyPermitTweaksStartup(settings);
        }

        public static void DoPermitSettings(Listing_Standard listing, RoyalTitlePermitDef permit)
        {
            listing.Label(permit.LabelCap);
            listing.GapLine();
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
            if(permit.royalAid != null)
            {
                // Tweak: Aid Favor Cost
                float favorCostBuffer = settings.tweak_royalPermitSettings[permit.defName].favorCost;
                listing.AddLabeledSlider($"- Aid Favor Cost: {favorCostBuffer.ToString("0")}", ref favorCostBuffer, 0f, 20f, "Min: 0", "Max: 20", 1f);
                settings.tweak_royalPermitSettings[permit.defName].favorCost = favorCostBuffer;
                if(permit.royalAid.pawnKindDef != null)
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
            listing.GapLine();
        }

        public static void TitleFloatMenu(this Listing_Standard listing, string name, string minTitle, RoyalTitlePermitDef permit)
        {
            float curHeight = listing.CurHeight;
            Rect rect = listing.GetRect(Text.LineHeight + listing.verticalSpacing);
            Text.Font = GameFont.Small;
            GUI.color = Color.white;
            TextAnchor anchor = Text.Anchor;
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(rect, name);
            Text.Anchor = TextAnchor.MiddleRight;

            Rect rect2 = new Rect(listing.ColumnWidth - 150f, curHeight, 150f, 29f);
            string label = DefDatabase<RoyalTitleDef>.GetNamedSilentFail(minTitle)?.LabelCap ?? "No Title";
            if (Widgets.ButtonText(rect2, label))
            {
                Find.WindowStack.Add(new FloatMenu(DoDropdown_TitleSelector(minTitle, permit)));
            }
            Text.Anchor = anchor;

            rect = listing.GetRect(0f);
            rect.height = listing.CurHeight - curHeight;
            rect.y -= rect.height;
            GUI.color = Color.white;
            listing.Gap(6f);
        }

        public static List<FloatMenuOption> DoDropdown_TitleSelector(string minTitle, RoyalTitlePermitDef permit)
        {
            List<FloatMenuOption> list = new List<FloatMenuOption>();
            list.Add(new FloatMenuOption("No Title", delegate () { TweaksGaloreMod.settings.tweak_royalPermitSettings[permit.defName].minTitle = string.Empty; }, orderInPriority: 1));
            foreach(RoyalTitleDef title in DefDatabase<RoyalTitleDef>.AllDefs)
            {
                if(title.tags.Any(tag => permit.faction.royalTitleTags.Contains(tag)) && title.Awardable)
                {
                    list.Add(new FloatMenuOption(title.LabelCap, delegate () { TweaksGaloreMod.settings.tweak_royalPermitSettings[permit.defName].minTitle = title.defName; }, orderInPriority: -title.seniority));
                }
            }
            return list;
        }
    }
}
