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
    public class TweakWorker_NFSMPreventByTrainability : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.Label("Prevent By Trainability:");
            listing.DoSettingBool("- Intermediate", def.description, def.defName + "Intermediate", def.DefaultBool);
            listing.DoSettingBool("- Advanced", def.description, def.defName + "Advanced", def.DefaultBool);
        }

        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            settings.GetBoolSetting(def.defName + "Intermediate", def.DefaultBool);
            settings.GetBoolSetting(def.defName + "Advanced", def.DefaultBool);
        }
    }
}
