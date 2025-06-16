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
    public class TweakWorker_ShrineArtificialRadius : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_AnimaTweaks.BoolValue)
            {
                foreach (ThingDef shrine in def.tweakThings)
                {
                    CompProperties_MeditationFocus shrineFocus = shrine.GetCompProperties<CompProperties_MeditationFocus>();

                    FocusStrengthOffset_ArtificialBuildings shrineRange = (FocusStrengthOffset_ArtificialBuildings)shrineFocus.offsets.Find(os => os.GetType() == typeof(FocusStrengthOffset_ArtificialBuildings));

                    shrineRange.radius = def.FloatValue;
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
