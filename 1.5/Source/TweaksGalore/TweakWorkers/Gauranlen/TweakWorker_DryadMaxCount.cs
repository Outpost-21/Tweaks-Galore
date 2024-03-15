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
    public class TweakWorker_DryadMaxCount : TweakWorker
    {
        public static SimpleCurve original_maxDryadsPerConnectionStrengthCurve = new SimpleCurve();
        public static SimpleCurve adjusted_maxDryadsPerConnectionStrengthCurve = new SimpleCurve();

        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_GauranlenTweaks.BoolValue)
            {
                CompProperties_TreeConnection originalProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                if (original_maxDryadsPerConnectionStrengthCurve.EnumerableNullOrEmpty())
                {
                    original_maxDryadsPerConnectionStrengthCurve = originalProps.maxDryadsPerConnectionStrengthCurve;
                }

                CompProperties_TreeConnection treeProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                {
                    adjusted_maxDryadsPerConnectionStrengthCurve = new SimpleCurve();
                    adjusted_maxDryadsPerConnectionStrengthCurve.Add(original_maxDryadsPerConnectionStrengthCurve.First());
                    adjusted_maxDryadsPerConnectionStrengthCurve.Add(new CurvePoint(0.76f, def.IntValue));

                    treeProps.maxDryadsPerConnectionStrengthCurve = adjusted_maxDryadsPerConnectionStrengthCurve;
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
