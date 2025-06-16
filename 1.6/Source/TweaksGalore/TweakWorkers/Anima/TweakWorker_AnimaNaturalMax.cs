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
    public class TweakWorker_AnimaNaturalMax : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_AnimaTweaks.BoolValue)
            {
                CompProperties_MeditationFocus focus = ThingDefOf.Plant_TreeAnima.GetCompProperties<CompProperties_MeditationFocus>();
                FocusStrengthOffset_BuildingDefs naturalOffset = (FocusStrengthOffset_BuildingDefs)focus.offsets.Find(os => os.GetType() == typeof(FocusStrengthOffset_BuildingDefs));
                naturalOffset.maxBuildings = def.IntValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
