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
    public class TweakWorker_LagFreeLamps : TweakWorker
    {

        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            if (def.BoolValue)
            {
                StringBuilder affectedLamps = new StringBuilder("Lag Free Lamps patched:");
                IEnumerable<ThingDef> potentialLamps = GetAllLamps();
                foreach(ThingDef thing in potentialLamps)
                {
                    if (!thing.HasComp<CompGatherSpot>())
                    {
                        CompProperties_Refuelable refuelable = thing.GetCompProperties<CompProperties_Refuelable>();
                        thing.comps.Remove(refuelable);
                        affectedLamps.AppendLine($"{thing.LabelCap} ({thing.defName})");
                    }
                }
                LogUtil.LogMessage(affectedLamps.ToString());
            }
        }

        public IEnumerable<ThingDef> GetAllLamps()
        {
            foreach(ThingDef thing in DefDatabase<ThingDef>.AllDefs)
            {
                if(thing.PlaceWorkers?.Any(pw => pw is PlaceWorker_GlowRadius) ?? false)
                {
                    CompProperties_Refuelable refuelable = thing.GetCompProperties<CompProperties_Refuelable>();
                    if(refuelable != null) { yield return thing; }
                }
            }
            yield break;
        }
    }
}
