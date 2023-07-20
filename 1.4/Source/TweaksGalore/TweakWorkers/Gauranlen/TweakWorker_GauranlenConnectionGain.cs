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
    public class TweakWorker_GauranlenConnectionGain : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_GauranlenTweaks.BoolValue)
            {
                CompProperties_TreeConnection treeProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                treeProps.connectionStrengthGainPerHourPruningBase = def.FloatValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
