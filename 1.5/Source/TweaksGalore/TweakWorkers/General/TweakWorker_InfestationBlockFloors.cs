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
    public class TweakWorker_InfestationBlockFloors : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            if (def.BoolValue)
            {
                List<TerrainDef> strongFloors = new List<TerrainDef>();

                foreach (TerrainDef terrain in DefDatabase<TerrainDef>.AllDefs)
                {
                    if (terrain != null)
                    {
                        if (terrain.costList != null && terrain.costList.Any(t => t.thingDef.stuffProps != null && ((bool)(t.thingDef?.stuffProps?.categories?.Contains(StuffCategoryDefOf.Metallic)) || (bool)(t.thingDef?.stuffProps?.categories?.Contains(StuffCategoryDefOf.Stony)))))
                        {
                            strongFloors.Add(terrain);
                        }
                        else if (!terrain.stuffCategories.NullOrEmpty() && (terrain.stuffCategories.Contains(StuffCategoryDefOf.Metallic) || terrain.stuffCategories.Contains(StuffCategoryDefOf.Stony)))
                        {
                            strongFloors.Add(terrain);
                        }
                    }
                }

                for (int i = 0; i < strongFloors.Count; i++)
                {
                    if (strongFloors[i].tags == null)
                    {
                        strongFloors[i].tags = new List<string>();
                    }
                    strongFloors[i].tags.Add("BlocksInfestations");
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
