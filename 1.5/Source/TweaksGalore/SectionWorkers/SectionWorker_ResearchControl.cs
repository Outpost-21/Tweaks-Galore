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
    public class SectionWorker_ResearchControl : SectionWorker
    {
        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            if (listing.ButtonTextLabeled("", "Restore defaults"))
            {
                Messages.Message("Tweaks Galore: 'Incident Control' tweaks restored to defaults. Restart required to take full effect.", MessageTypeDefOf.CautionInput);
            }
            // Tweak: Penned Animal Config
            listing.DoSettingBool("Incident Control", "Allows control over the chances of incidents happening, even reducing them to zero, because why not. Incidents set to zero chance of happening may still be triggered through other means, so this doesn't always result in disabling them.", def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
            }
        }

        public override void DoOnStartup()
        {
            StoreDefaultValues();
            if (settings.GetBoolSetting(def.defName, false))
            {
            }
        }

        public void StoreDefaultValues()
        {

        }
    }
}
