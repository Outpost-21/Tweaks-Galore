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
    public class TweakWorker_NoBackstoryIncapabilities : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            if (def.BoolValue)
            {
                List<BackstoryDef> incapableBackstories = DefDatabase<BackstoryDef>.AllDefsListForReading;
                for (int i = 0; i < incapableBackstories.Count; i++)
                {
                    incapableBackstories[i].workDisables = WorkTags.None;
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
