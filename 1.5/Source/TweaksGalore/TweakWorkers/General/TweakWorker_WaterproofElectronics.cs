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
    public class TweakWorker_WaterproofElectronics : TweakWorker
    {

        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            if (def.BoolValue)
            {
                StringBuilder affectedElectronics = new StringBuilder("Waterproof electronics patched:");
                foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefsListForReading)
                {
                    CompProperties_Power comp = thing.GetCompProperties<CompProperties_Power>();
                    if (comp != null)
                    {
                        comp.shortCircuitInRain = false;
                        affectedElectronics.AppendLine($"{thing.LabelCap} ({thing.defName})");
                    }
                }
                LogUtil.LogMessage(affectedElectronics.ToString());
            }
        }
    }
}
