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
    public class TweakWorker_GravshipFieldExtender_MaxDistanceFromEngine : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue)
            {
                CompProperties_GravshipFacility props = ThingDefOf.GravFieldExtender.GetCompProperties<CompProperties_GravshipFacility>();
                props.maxDistance = def.FloatValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
