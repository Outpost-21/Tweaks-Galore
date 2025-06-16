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
    public class TweakWorker_NoDeathAcidifiers : TweakWorker
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
                foreach (PawnKindDef pawnKind in DefDatabase<PawnKindDef>.AllDefs)
                {
                    if(pawnKind.techHediffsRequired?.Any(thr => thr == TGThingDefOf.DeathAcidifier) ?? false)
                    {
                        pawnKind.techHediffsRequired.RemoveAll(thr => thr == TGThingDefOf.DeathAcidifier);
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
