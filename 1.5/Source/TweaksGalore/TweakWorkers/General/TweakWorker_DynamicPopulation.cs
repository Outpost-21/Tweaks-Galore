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
    public class TweakWorker_DynamicPopulation : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            base.OnStartup();
            if (def.BoolValue)
            {
                List<StorytellerDef> allStorytellers = DefDatabase<StorytellerDef>.AllDefsListForReading;
                foreach (StorytellerDef storyteller in allStorytellers)
                {
                    storyteller.populationIntentFactorFromPopCurve = new SimpleCurve
                    {
                        new CurvePoint(TGTweakDefOf.Tweak_DynamicPopulation_CriticallyLow.IntValue, 8f),
                        new CurvePoint(TGTweakDefOf.Tweak_DynamicPopulation_Low.IntValue, 2f),
                        new CurvePoint(TGTweakDefOf.Tweak_DynamicPopulation_Typical.IntValue, 1f),
                        new CurvePoint(TGTweakDefOf.Tweak_DynamicPopulation_High.IntValue, 0.35f),
                        new CurvePoint(TGTweakDefOf.Tweak_DynamicPopulation_CriticallyHigh.IntValue, 0f),
                        new CurvePoint(TGTweakDefOf.Tweak_DynamicPopulation_Maximum.IntValue, -1f)
                    };
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
