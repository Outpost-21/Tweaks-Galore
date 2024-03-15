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
    public static class TGThoughtDefOf
    {
        static TGThoughtDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TGThoughtDefOf));
        }

        [MayRequireRoyalty]
        public static ThoughtDef AnimaScream;
    }
}
