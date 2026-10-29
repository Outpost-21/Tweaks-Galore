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
    public static class ApparelUtil
    {
        public static void TaintInnermostLayer(this ThingOwner<Apparel> wornApparel)
        {
            for (int j = 0; j < wornApparel.Count; j++)
            {
                Apparel curApparel = wornApparel[j];
                if (curApparel.def.apparel.layers.Contains(ApparelLayerDefOf.OnSkin))
                {
                    curApparel.Notify_PawnKilled();
                }
            }
        }
        public static void TaintAll(this ThingOwner<Apparel> wornApparel)
        {
            for (int j = 0; j < wornApparel.Count; j++)
            {
                Apparel curApparel = wornApparel[j];
                curApparel.Notify_PawnKilled();
            }
        }
    }
}
