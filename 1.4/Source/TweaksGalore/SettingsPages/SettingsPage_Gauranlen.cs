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
    public static class SettingsPage_Gauranlen
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Gauranlen(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Gauranlen Tweaks", "This entire section is disabled by default for compatibility sake mostly, some Gauranlen related tweaks function regardless of if they've been changed (any changing a radius for example) so without this setting could have caused compatibility issue with other Gauranlen related mods if you prefer to use those.", ref settings.tweak_gauranlenTweaks);
            if (settings.tweak_gauranlenTweaks)
            {
                // Tweak: Replantable Gauranlen
                listing.CheckboxEnhanced("Replantable Gauranlen", "Makes it so you can move Gauranlen trees like any other.", ref settings.tweak_replantableGauranlen);
                listing.GapLine();
                listing.Label("Tree Tweaks");
                listing.GapLine();
                // Tweak: Initial Connection Range
                listing.Label("Initial Connection Range", tooltip: "Initial connection strength will be in this range.");
                listing.AddLabeledSlider($"- Minimum: {settings.tweak_gauranlenInitialConnectionStrength.min.ToStringPercent()}", ref settings.tweak_gauranlenInitialConnectionStrength.min, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                listing.AddLabeledSlider($"- Maximum: {settings.tweak_gauranlenInitialConnectionStrength.max.ToStringPercent()}", ref settings.tweak_gauranlenInitialConnectionStrength.max, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                if (settings.tweak_gauranlenInitialConnectionStrength.min > settings.tweak_gauranlenInitialConnectionStrength.max)
                {
                    settings.tweak_gauranlenInitialConnectionStrength.max = settings.tweak_gauranlenInitialConnectionStrength.min;
                }
                listing.Note("These are specific to the Gauranlen Tree itself.", GameFont.Tiny, Color.gray);
                // Tweak: Artificial Building Radius
                listing.AddLabeledSlider($"- Artificial Building Radius: {settings.tweak_gauranlenArtificialBuildingRadius.ToString("0.0")}", ref settings.tweak_gauranlenArtificialBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a debuff is applied to the trees effects if artificial buildings are built in it.", GameFont.Tiny, Color.gray);
                // Tweak: Connection Gain While Puning
                listing.AddLabeledSlider($"- Connection Gain While Puning: {settings.tweak_gauranlenConnectionGainPerHourPruning.ToStringPercent()}", ref settings.tweak_gauranlenConnectionGainPerHourPruning, 0.01f, 1f, "Min: 1%", "Max: 100%", 0.01f);
                listing.Note("Connection gained per hour of pruning.", GameFont.Tiny, Color.gray);
                // Tweak: Connection Loss Rate
                listing.AddLabeledSlider($"- Connection Loss Rate: {settings.tweak_gauranlenConnectionLossPerLevel.ToStringPercent()}", ref settings.tweak_gauranlenConnectionLossPerLevel, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listing.Note("This functions as a multiplier on the connection loss per level of connection.", GameFont.Tiny, Color.gray);
                // Tweak: Connection Loss Per Building
                listing.AddLabeledSlider($"- Connection Loss Per Building: {settings.tweak_gauranlenLossPerBuilding.ToStringPercent()}", ref settings.tweak_gauranlenLossPerBuilding, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listing.Note("This functions as a multiplier on the connection loss per artificial building in range.", GameFont.Tiny, Color.gray);
                // Tweak: Moss Growth Radius
                listing.AddLabeledSlider($"- Moss Growth Radius: {settings.tweak_gauranlenPlantGrowthRadius.ToString("0.0")}", ref settings.tweak_gauranlenPlantGrowthRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The max radius of the tree where it grows moss.", GameFont.Tiny, Color.gray);
                // Tweak: Moss Growth Days
                listing.AddLabeledSlider($"- Moss Growth Days: {settings.tweak_gauranlenMossGrowDays.ToString("0")}", ref settings.tweak_gauranlenMossGrowDays, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Amount of days till Gauralen Moss grows to maturity.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.Label("Dryad Tweaks");
                listing.Note("These are specific to the Dryads spawned by the Gauranlen Tree.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                // Tweak: Max Dryads
                listing.AddLabeledSlider($"- Max Dryads: {settings.tweak_gauranlenMaxDryads.ToString("0")}", ref settings.tweak_gauranlenMaxDryads, 3f, 30f, "Min: 3", "Max: 30", 1f);
                listing.Note("This is usually a curve with multiple points, and that's great. But my brain is smooth as fuck so this is a set value slapped onto the end, the normal values apply till 75+% connection and then it uses whatever you set this to.", GameFont.Tiny, Color.gray);
                // Tweak: Dryad Spawn Days
                listing.AddLabeledSlider($"- Dryad Spawn Days: {settings.tweak_gauranlenDryadSpawnDays.ToString("0")}", ref settings.tweak_gauranlenDryadSpawnDays, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Number of days between the spawning of Dryads.", GameFont.Tiny, Color.gray);
                // Tweak: Dryad Cocoon Days
                listing.AddLabeledSlider($"- Dryad Cocoon Days: {settings.tweak_gauranlenCocoonDaysToComplete.ToString("0")}", ref settings.tweak_gauranlenCocoonDaysToComplete, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Number of days it takes for a Dryad to finish the cocoon process.", GameFont.Tiny, Color.gray);
                // Tweak: Gaumaker Pod Days
                listing.AddLabeledSlider($"- Gaumaker Pod Days: {settings.tweak_gauranlenMossGrowDays.ToString("0")}", ref settings.tweak_gauranlenMossGrowDays, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Amount of days it takes for a Gaumaker pod to mature.", GameFont.Tiny, Color.gray);
                // Tweak: Moss Growth Days
                listing.AddLabeledSlider($"- Seed Yield: {settings.tweak_gauranlenPodHarvestYield.ToString("0")}", ref settings.tweak_gauranlenPodHarvestYield, 1f, 10f, "Min: 1", "Max: 10", 1f);
                listing.Note("Amount of seeds obtained from a Gaumaker Pod.", GameFont.Tiny, Color.gray);
                listing.GapLine();

                TweaksGaloreStartup.Tweak_GauranlenTweaks(settings);
            }
        }
    }
}
