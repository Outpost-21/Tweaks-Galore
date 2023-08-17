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
    public class TweakWorker_PowerValue : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            int value = settings.GetIntSetting(def.defName, def.DefaultInt);
            if (TGTweakDefOf.Tweak_PowerAdjusting.BoolValue && value != def.DefaultInt)
            {
                if (def.tweakThing != null)
                {
                    if (def.invertUsage) { def.tweakThing.SetPowerUsage(-value); } else { def.tweakThing.SetPowerUsage(value); }
                }
                if (!def.tweakThings.NullOrEmpty())
                {
                    foreach (ThingDef thing in def.tweakThings)
                    {
                        if (def.invertUsage) { thing.SetPowerUsage(-value); } else { thing.SetPowerUsage(value); }
                    }
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
