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
    public class TweakWorker_PoluxEffectRadius : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_PoluxTweaks.BoolValue)
            {
                CompProperties_PollutionPump pollutionPumpComp = TGThingDefOf.Plant_TreePolux.GetCompProperties<CompProperties_PollutionPump>();
                pollutionPumpComp.radius = def.FloatValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
