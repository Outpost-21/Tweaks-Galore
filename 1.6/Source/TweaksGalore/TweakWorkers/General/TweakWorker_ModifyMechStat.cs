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
    public class TweakWorker_ModifyMechStat : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            if (def.tweakThing != null)
            {
                UpdateMechStat(def.tweakThing, def.tweakStat, def.FloatValue);
            }
            if (!def.tweakThings.NullOrEmpty())
            {
                foreach (ThingDef mech in def.tweakThings)
                {
                    UpdateMechStat(mech, def.tweakStat, def.FloatValue);
                }
            }
        }

        public void UpdateMechStat(ThingDef mech, StatDef statDef, float value)
        {
            mech.SetStatBaseValue(statDef, value);
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
