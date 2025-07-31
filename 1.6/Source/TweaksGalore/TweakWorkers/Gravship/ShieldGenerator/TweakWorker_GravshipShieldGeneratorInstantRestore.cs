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
    public class TweakWorker_GravshipShieldGeneratorInstantRestore : TweakWorker
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
                CompProperties_ProjectileInterceptor shieldProps = TGThingDefOf.GravshipShieldGenerator.GetCompProperties<CompProperties_ProjectileInterceptor>();
                shieldProps.hitPointsRestoreInstantlyAfterCharge = def.BoolValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
