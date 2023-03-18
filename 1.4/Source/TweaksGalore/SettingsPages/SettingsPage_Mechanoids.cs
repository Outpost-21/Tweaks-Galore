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
    public static class SettingsPage_Mechanoids
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Mechanoids(Listing_Standard listing)
        {
            // Tweak: Disable Adapting
            listing.CheckboxEnhanced("Disable Adapting", "Disables the ability for Mechanoids to adapt to EMP.", ref settings.patch_disableMechanoidAdapting);
            listing.GapLine();
            // Tweak: Heat Armor
            listing.Label("Mechanoid Heat Armour: " + settings.tweak_mechanoidHeatArmour.ToStringPercent());
            listing.AddLabeledSlider(null, ref settings.tweak_mechanoidHeatArmour, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
            // Tweak: Pre-1.0 Ship Parts
            listing.CheckboxEnhanced("Pre-1.0 Ship Parts", "Returns psychic and defoliator ship parts to their B18 state where they actually provide a decent reward." +
                "\nPsychic Ship Part Leavings:" +
                "\n- Steel x100" +
                "\n- Plasteel x35" +
                "\n- Steel Slag x8" +
                "\n- Industrial Components x4" +
                "\n- Spacer Components x1" +
                "\n- AI Persona Core" +
                "\n\nDefoliator Ship Part Leavings:" +
                "\n- Steel x100" +
                "\n- Plasteel x35" +
                "\n- Steel Slag x8" +
                "\n- Industrial Component x4" +
                "\n- Spacer Component x1" +
                "\n- Glittertech Medicine x5", ref settings.tweak_preReleaseShipParts);
            listing.GapLine();
            if (ModLister.RoyaltyInstalled)
            {
                listing.CheckboxEnhanced("Better Gloomlight", "Changes the gloomlight often found in mech clusters to produce more light so it's actually useful.", ref settings.tweak_betterGloomlight);
                listing.CheckboxEnhanced("- Sunlamp Gloomlight", "With Better Gloomlight and this enabled, they'll instead produce enough light to be equal to that of a free sunlamp.", ref settings.tweak_gloomlightSunlamp);
                listing.CheckboxEnhanced("- Darklight Gloomlight", "Changes the colour of the gloomlight to match that of 'Darklight'.", ref settings.tweak_gloomlightDarklight);
                listing.GapLine();
            }
        }
    }
}
