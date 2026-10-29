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
    public class TweakWorker_Shuttle_FuelCapacity : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            base.OnStartup();
            if (TGTweakDefOf.Tweak_ShuttleTweaks.BoolValue)
            {
                CompProperties_Refuelable props = ThingDefOf.PassengerShuttle.GetCompProperties<CompProperties_Refuelable>();
                props.fuelCapacity = def.IntValue;
                props.initialConfigurableTargetFuelLevel = def.IntValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
