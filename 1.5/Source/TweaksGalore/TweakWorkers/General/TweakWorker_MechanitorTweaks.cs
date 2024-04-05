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
    public class TweakWorker_MechanitorTweaks : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            if (def.BoolValue)
            {
                HediffDef implantDef = TGHediffDefOf.MechlinkImplant;
                HediffStage implantStage = implantDef.stages.First();
                implantStage.statOffsets.Find(sm => sm.stat == StatDefOf.MechBandwidth).value = TGTweakDefOf.Tweak_MechanitorBandwidthBase.IntValue;
                implantStage.statOffsets.Find(sm => sm.stat == StatDefOf.MechControlGroups).value = TGTweakDefOf.Tweak_MechanitorControlGroupBase.IntValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
