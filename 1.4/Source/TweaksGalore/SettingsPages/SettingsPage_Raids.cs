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
    public static class SettingsPage_Raids
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Raids(Listing_Standard listing)
        {
            // Tweak: No More Breach Raids
            listing.CheckboxEnhanced("No More Breach Raids", "Removes Breach Raids as an option for raiders.", ref settings.tweak_noMoreBreachRaids);
            listing.GapLine();
            // Tweak: No More Drop Pod Raids
            listing.CheckboxEnhanced("No More Drop Pod Raids", "Removes Drop Pod Raids as an option for raiders. Does not work with RimWar.", ref settings.tweak_noMoreDropPodRaids);
            listing.GapLine();
            // Tweak: No More Sapper Raids
            listing.CheckboxEnhanced("No More Sapper Raids", "Removes Sapper Raids as an option for raiders.", ref settings.tweak_noMoreSapperRaids);
            listing.GapLine();
            // Tweak: No More Siege Raids
            listing.CheckboxEnhanced("No More Siege Raids", "Removes Siege Raids as an option for raiders.", ref settings.tweak_noMoreSiegeRaids);
            listing.GapLine();
            // Tweak: No More Cowardly Raids
            listing.CheckboxEnhanced("No More Cowardly Raids", "Prevents raiders from fleeing.", ref settings.tweak_noCowardlyRaiders);
            listing.GapLine();
        }
    }
}
