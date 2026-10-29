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
    public static class TGHediffDefOf
    {
        static TGHediffDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TGHediffDefOf));
        }

        [MayRequireBiotech]
        public static HediffDef MechlinkImplant;
    }
}
