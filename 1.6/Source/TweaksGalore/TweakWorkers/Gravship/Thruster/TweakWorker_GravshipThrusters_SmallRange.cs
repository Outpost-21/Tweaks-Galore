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
    public class TweakWorker_GravshipThrusters_SmallRange : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_GravshipTweaks.BoolValue)
            {
                CompProperties_GravshipThruster props = ThingDefOf.SmallThruster.GetCompProperties<CompProperties_GravshipThruster>();
                props.statOffsets.Find(s => s.stat == StatDefOf.GravshipRange).value = def.IntValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
