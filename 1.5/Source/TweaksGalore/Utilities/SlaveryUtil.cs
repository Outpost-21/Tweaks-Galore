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
    public static class SlaveryUtil
    {
        public static bool IsSuppressed(this Need_Suppression need)
        {
            if(need != null && need.CurLevel > TGTweakDefOf.Tweak_SuppressionThreshold.FloatValue)
            {
                return true;
            }
            return false;
        }
    }
}
