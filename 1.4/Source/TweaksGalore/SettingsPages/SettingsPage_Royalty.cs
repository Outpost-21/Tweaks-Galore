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
    public static class SettingsPage_Royalty
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Royalty(Listing_Standard listing)
        {
            // Tweak: Allow Any Buildings in Throneroom
            listing.CheckboxEnhanced("Allow Any Buildings in Throneroom", "If enabled, removes building restrictions in thronerooms. Includes Altars.", ref settings.tweak_throneroomAnyBuildings);
            listing.GapLine();
            if (!settings.tweak_throneroomAnyBuildings)
            {
                // Tweak: Allow Altar in Throneroom
                listing.CheckboxEnhanced("Allow Altar in Throneroom", "If enabled, specifically allows altars to be built in thronerooms.", ref settings.tweak_throneroomAllowAltars);
                listing.GapLine();
            }
            // Tweak: Delayed Royalty
            listing.CheckboxEnhanced("Delayed Royalty", "Delays the Royalty starter quests by a few days more than usual to give additional time to get ready for them.", ref settings.tweak_delayedRoyalty);
            listing.GapLine();
            // Tweak: Free Meditation
            listing.CheckboxEnhanced("Free Meditation", "Allows any pawn to use any meditation focus building regardless of the focus types they have. Automatically patches all vanilla and modded meditation sources.", ref settings.tweak_animaMeditationAll);
            listing.GapLine();
            // Tweak: No Natural/Artistic Focus Limits
            listing.CheckboxEnhanced("No Backstory Limits", "Allows any pawn to use Natural or Artistic meditation focus types regardless of backstory. If Vanilla Psycasts Expanded is active, this will also handle the psycaster paths being unlockable.", ref settings.tweak_animaRemoveBackstoryLimits);
            listing.GapLine();
            // Tweak: No Trading Permit
            listing.CheckboxEnhanced("No Trading Permit", "If enabled, removes the need to hae a permit to trade with the Shattered Empire.", ref settings.tweak_noTradingPermit);
            listing.GapLine();
            // Tweak: Uninstall Mech Shields
            listing.CheckboxEnhanced("Uninstallable Mech Shields", "If enabled, allows you to uninstall mech cluster shields for your own use. This also prevents them shutting down when the cluster is cleared so they remain useful.", ref settings.tweak_uninstallableMechShields);
            listing.GapLine();
            // Tweak: Wait, This is Better
            listing.CheckboxEnhanced("Wait, This is Better", "Adds previous tier tags to all royal acceptable apparel (including modded) so that apprel of a higher tier will satisfy pawns looking for a lower tier. Should really be the default.", ref settings.tweak_waitThisIsBetter);
            listing.GapLine();
        }
    }
}
