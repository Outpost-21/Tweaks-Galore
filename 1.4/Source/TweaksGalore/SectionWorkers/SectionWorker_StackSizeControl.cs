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
    public class SectionWorker_StackSizeControl : SectionWorker
    {
        public Dictionary<string, int> defaultStackSizes = new Dictionary<string, int>();

        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            if (listing.ButtonTextLabeled("", "Restore defaults"))
            {
                Messages.Message("Tweaks Galore: 'Incident Control' tweaks restored to defaults. Restart required to take full effect.", MessageTypeDefOf.CautionInput);
            }
            // Tweak: Penned Animal Config
            listing.DoSettingBool("Stack Size Control", "Allows control stack size for categories as well as individual item stack overrides. If it's unstackable in vanilla, this won't make it stackable without being explicitly told to.", def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                listing.GapLine();
                //for (int i = 0; i < CachedIncidentListing.Count; i++)
                //{
                //    IncidentDef curIncident = CachedIncidentListing[i];
                //    float value = settings.tweak_eventControlDict[curIncident.defName];
                //    listing.AddLabeledSlider(curIncident.LabelCap + ": " + (value == 0f ? "Never" : (value)), ref value, 0f, 10f, "Disabled", "High Chance", 0.01f);
                //    settings.tweak_eventControlDict[curIncident.defName] = value;
                //}
                //SetIncidentChances();
            }
        }

        public override void DoOnStartup()
        {
            StoreDefaultValues();
            UpdateIncidentDict();
            if (settings.GetBoolSetting(def.defName, false))
            {
                SetIncidentChances();
            }
        }

        public override void DoSectionRestore()
        {
            settings.tweak_stackSizeControl = defaultStackSizes;
        }

        public void StoreDefaultValues()
        {

        }

        public void UpdateIncidentDict()
        {

        }

        public void SetIncidentChances()
        {

        }
    }
}
