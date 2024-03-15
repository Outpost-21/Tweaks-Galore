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
    public static class SettingsPage_Biotech
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Biotech(Listing_Standard listing)
        {
            // Tweak: Ancient Deconstruction
            listing.CheckboxEnhanced("Ancient Deconstruction", "Makes the Ancient Ruins added by Ideology and Biotech deconstructable instead of having to destroy them.", ref settings.tweak_ancientDeconstruction);
            if (settings.tweak_ancientDeconstruction)
            {
                listing.CheckboxEnhanced("- Give Proper Materials", "Changes returned items to some reasonable materials instead of just slag.", ref settings.tweak_ancientDeconstruction_mode);
            }
            listing.GapLine();
            listing.Label("Genetics");
            listing.GapLine();
            // Tweak: Flatten Complexity
            listing.CheckboxEnhanced("Flatten Complexity", "Remove complexity values from genes so they are all set to 0.", ref settings.tweak_flattenComplexity);
            // Tweak: Flatten Metabolism
            listing.CheckboxEnhanced("Flatten Metabolism", "Remove metabolism values from genes so they are all set to 0.", ref settings.tweak_flattenMetabolism);
            // Tweak: Flatten Archites
            listing.CheckboxEnhanced("Flatten Archites", "Remove archite values from genes so they are all set to 0.", ref settings.tweak_flattenArchites);
            // Tweak: Show Genes Tab
            listing.CheckboxEnhanced("Show Genes Tab", "By default the Genes tab is hidden on pawns, if you don't have another mod revealing it, you can use this to do so instead. Just keep in mind this was potentially hidden in vanilla for a reason.", ref settings.tweak_showGenesTab);
            listing.GapLine();
            // Tweak: Default Pregnancy Chance
            listing.AddLabeledSlider($"Spawn Pregnancy Chance: {settings.tweak_defaultPregnancyChance.ToStringPercent()}", ref settings.tweak_defaultPregnancyChance, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
            listing.Note("PawnKinds usually have a 3% chance to spawn pregnant if children are enabled which is multiplied by fertility of the pawn, this lets you change that base chance. Any pawnKind that doesn't use the default value will be skipped so as not to cause problems.", GameFont.Tiny, Color.gray);
            TweaksGaloreStartup.UpdatePregnancyChances(settings);
            listing.GapLine();
            listing.Gap();
            listing.CheckboxEnhanced("Enable Player Mech Tweaks", "This controls if any of the player mechanoid tweaks even try to function, some tweaks have no other way of knowing if they should even run.", ref settings.tweak_playerMechTweaks);
            if (settings.tweak_playerMechTweaks)
            {
                // Tweak: Mechanoid Skill Level
                // TODO: Something fucky going on with this.
                //listing.AddLabeledSlider($"- Skill Level: {settings.tweak_mechanoidSkillLevel}", ref settings.tweak_mechanoidSkillLevel, 0f, 20f, "Min: 0", "Max: 20", 1f);
                // Tweak: Mechanoid Work Speed
                // TODO: Replace with a listing for individual work types, they all seem to use different speeds!
                //listing.AddLabeledSlider($"- Work Speed: {settings.tweak_mechanoidWorkSpeed.ToStringPercent()}", ref settings.tweak_mechanoidWorkSpeed, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);
                // Tweak: Mechanoid Drain Rate
                listing.AddLabeledSlider($"- Drain Rate: {settings.tweak_mechanoidDrainRate.ToStringPercent()}", ref settings.tweak_mechanoidDrainRate, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);
                // Tweak: Mechanoid Recharge Rate
                // TODO Transpiler Required for Recharge Rate. Fuckin bullshit is what this is.
                //listing.AddLabeledSlider($"- Recharge Rate: {settings.tweak_mechanoidRechargeRate.ToStringPercent()}", ref settings.tweak_mechanoidRechargeRate, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);

                TweaksGaloreStartup.Tweak_PlayerMechTweaks(settings);
            }
            listing.GapLine();
            listing.CheckboxEnhanced("Enable Mechanitor Tweaks", "This controls if any of the mechanitor tweaks even try to function, some tweaks have no other way of knowing if they should even run.", ref settings.tweak_mechanitorTweaks);
            if (settings.tweak_mechanitorTweaks)
            {
                listing.CheckboxEnhanced("- Disable Range Limit", "Makes mechanoids controllable regardless of distance to Mechanitor. If they can design a neural interface they can design something with better than cheap wifi range.", ref settings.tweak_mechanitorDisableRange);
                // Tweak: Mechanitor Range Multiplier
                //listing.AddLabeledSlider($"- Disable Range Limit: {settings.tweak_mechanitorRangeMulti.ToStringPercent()}", ref settings.tweak_mechanitorRangeMulti, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);
                // Tweak: Mechanitor Bandwidth Base
                listing.AddLabeledSlider($"- Bandwidth Base: {settings.tweak_mechanitorBandwidthBase}", ref settings.tweak_mechanitorBandwidthBase, 1f, 30f, "Min: 1", "Max: 30", 1f);
                // Tweak: Mechanitor Control Group Base
                listing.AddLabeledSlider($"- Control Group Base: {settings.tweak_mechanitorControlGroupBase}", ref settings.tweak_mechanitorControlGroupBase, 1f, 10f, "Min: 1", "Max: 10", 1f);
                // Tweak: Mechanitor Bandwidth Per Band Node
                listing.AddLabeledSlider($"- Bandwidth per Band Node: {settings.tweak_mechanitorBandNodeBandwidth}", ref settings.tweak_mechanitorBandNodeBandwidth, 1f, 20f, "Min: 1", "Max: 20", 1f);

                TweaksGaloreStartup.Tweak_MechanitorTweaks(settings);
            }
            listing.GapLine();
        }
    }
}
