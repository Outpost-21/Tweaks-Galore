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
    [DefOf]
    public static class TGThingDefOf
    {
        static TGThingDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TGThingDefOf));
        }

        [MayRequireIdeology]
        public static ThingDef Plant_MossGauranlen;

        [MayRequireBiotech]
        public static ThingDef Plant_TreePolux;
    }
}
