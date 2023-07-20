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
    public class TweakWorker_GauranlenConnectionLossBuildings : TweakWorker
    {
        public static SimpleCurve original_connectionLossDailyPerBuildingDistanceCurve = new SimpleCurve();
        public static SimpleCurve adjusted_connectionLossDailyPerBuildingDistanceCurve = new SimpleCurve();

        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_GauranlenTweaks.BoolValue)
            {
                CompProperties_TreeConnection originalProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                if (original_connectionLossDailyPerBuildingDistanceCurve.EnumerableNullOrEmpty())
                {
                    original_connectionLossDailyPerBuildingDistanceCurve = originalProps.connectionLossDailyPerBuildingDistanceCurve;
                }
                CompProperties_TreeConnection treeProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                adjusted_connectionLossDailyPerBuildingDistanceCurve = new SimpleCurve();
                adjusted_connectionLossDailyPerBuildingDistanceCurve.Add(new CurvePoint(0, 0.07f * def.FloatValue));
                adjusted_connectionLossDailyPerBuildingDistanceCurve.Add(new CurvePoint(TGTweakDefOf.Tweak_GauranlenConnectionLossBuildings.FloatValue, 0.01f * def.FloatValue));

                treeProps.connectionLossDailyPerBuildingDistanceCurve = adjusted_connectionLossDailyPerBuildingDistanceCurve;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
