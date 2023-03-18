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
    public static class SettingsPage_Resources
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Resources(Listing_Standard listing)
        {
            // Tweak: Metal Doesn't Burn
            listing.CheckboxEnhanced("Metal Doesn't Burn", "Removes flammability from steel and plasteel, setting gold and silver to just a scratch above 0 so they have a chance to melt away still.", ref settings.tweak_metalDoesntBurn);
            listing.GapLine();
            // Tweak: No Mineable Components
            listing.CheckboxEnhanced("No Mineable Components", "Removes mineable components from map generation.", ref settings.tweak_noMineableComponents);
            listing.GapLine();
            // Tweak: No Mineable Plasteel
            listing.CheckboxEnhanced("No Mineable Plasteel", "Removes mineable plasteel from map generation.", ref settings.tweak_noMineablePlasteel);
            listing.GapLine();
            // Tweak: Old Component Names
            listing.CheckboxEnhanced("Old Component Names", "Back in my day we had industrial and spacer components! Now you can too!", ref settings.tweak_oldComponentNames);
            listing.GapLine();
            // Tweak: Pretty Precious Metals
            listing.CheckboxEnhanced("Pretty Precious Metals", "Changes the beauty of gold and silver (the item, not as a material) from -4 to 4.", ref settings.tweak_prettyPreciousMetals);
            listing.GapLine();
            // Tweak: Stackable Chunks
            listing.CheckboxEnhanced("Stackable Chunks", "Allows stone chunks and slag to be stackable. Default: 5", ref settings.tweak_stackableChunks);
            if (settings.tweak_stackableChunks)
            {
                listing.AddLabeledSlider("- Stone Chunk Stack Size: " + settings.tweak_stackableChunks_stone, ref settings.tweak_stackableChunks_stone, 1f, 400f, "Min: 1", "Max: 400", 1f);
                listing.AddLabeledSlider("- Slag Chunk Stack Size: " + settings.tweak_stackableChunks_slag, ref settings.tweak_stackableChunks_slag, 1f, 400f, "Min: 1", "Max: 400", 1f);
            }
            listing.GapLine();
            // Tweak: Stronger Steel
            listing.CheckboxEnhanced("Stronger Steel", "Doubles the durability of steel as a material.", ref settings.tweak_strongerSteel);
            listing.GapLine();
        }
    }
}
