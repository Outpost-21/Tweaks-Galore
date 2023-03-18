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
    public static class SettingsPage_Polux
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Polux(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Polux Tweaks", "This entire section is disabled by default for compatibility sake mostly, some Polux related tweaks function regardless of if they've been changed (any changing a radius for example) so without this setting could have caused compatibility issue with other Polux related mods if you prefer to use those.", ref settings.tweak_poluxTweaks);
            if (settings.tweak_poluxTweaks)
            {
                // Tweak: Replantable Pollux
                listing.CheckboxEnhanced("Replantable Polux", "Makes it so you can move Polux trees like any other.", ref settings.tweak_replantablePolux);
                listing.GapLine();
                listing.Label("Tree Tweaks");
                listing.GapLine();
                // Tweak: Polux Effect Radius
                listing.AddLabeledSlider($"- Effect Radius: {settings.tweak_poluxEffectRadius.ToString("0.0")}", ref settings.tweak_poluxEffectRadius, 0.1f, 42f, "Min: 0.1", "Max: 42", 0.1f);
                listing.Note("Lower values increases speed of the pollution cleansing effect.", GameFont.Tiny);
                listing.GapLine();
                // Tweak: Polux Effect Rate
                listing.AddLabeledSlider($"- Effect Rate Multiplier: {settings.tweak_poluxEffectRate.ToStringPercent()}", ref settings.tweak_poluxEffectRate, 0.01f, 2f, "Min: 1%", "Max: 200%", 0.01f);
                listing.Note("Lower values increases speed of the pollution cleansing effect.", GameFont.Tiny);
                listing.GapLine();
                // Tweak: Polux Artificial Buildings
                listing.CheckboxEnhanced("- Ignore Artificial Buildings", "Vanilla usually disables the tree's effects if there are artificial buildings in its radius, this disables that behaviour and lets it function anyway.", ref settings.tweak_poluxArtificialDisables);
                listing.GapLine();

                TweaksGaloreStartup.Tweak_PoluxTweaks(settings);
            }
        }
    }
}
