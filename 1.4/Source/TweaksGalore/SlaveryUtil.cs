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
            if(need != null && need.CurLevel > TweaksGaloreMod.settings.patch_properSuppressionPercentage)
            {
                return true;
            }
            return false;
        }
    }
}
