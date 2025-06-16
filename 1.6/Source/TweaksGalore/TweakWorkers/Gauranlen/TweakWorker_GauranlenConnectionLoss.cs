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
    public class TweakWorker_GauranlenConnectionLoss : TweakWorker
    {
        public static SimpleCurve original_connectionLossPerLevelCurve = new SimpleCurve();
        public static SimpleCurve adjusted_connectionLossPerLevelCurve = new SimpleCurve();

        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_GauranlenTweaks.BoolValue)
            {
                CompProperties_TreeConnection originalProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                if (original_connectionLossPerLevelCurve.EnumerableNullOrEmpty())
                {
                    original_connectionLossPerLevelCurve = originalProps.connectionLossPerLevelCurve;
                }

                CompProperties_TreeConnection treeProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                adjusted_connectionLossPerLevelCurve = new SimpleCurve();
                adjusted_connectionLossPerLevelCurve.Add(original_connectionLossPerLevelCurve.First());
                adjusted_connectionLossPerLevelCurve.Add(new CurvePoint(0.66f, 0.06f * def.FloatValue));

                treeProps.connectionLossPerLevelCurve = adjusted_connectionLossPerLevelCurve;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
