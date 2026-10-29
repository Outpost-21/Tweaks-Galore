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
    public class TweakWorker_GravshipFieldExtender_SubstructureRadius : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            base.OnStartup();
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue)
            {
                CompProperties_SubstructureFootprint props = ThingDefOf.GravFieldExtender.GetCompProperties<CompProperties_SubstructureFootprint>();
                props.radius = def.FloatValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
