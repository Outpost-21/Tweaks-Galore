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
    public class TweakWorker_HighestTechLevel : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.AddLabeledSlider<TechLevel>(def.LabelCap, ref settings.tweak_tech_highestTechLevel);
            listing.Note(def.description, GameFont.Tiny);
        }

        public override void OnRestore()
        {
            base.OnRestore();
            settings.tweak_tech_highestTechLevel = TechLevel.Undefined;
        }

        public override void OnStartup()
        {

        }

        public override void OnWriteSettings()
        {

        }
    }
}
