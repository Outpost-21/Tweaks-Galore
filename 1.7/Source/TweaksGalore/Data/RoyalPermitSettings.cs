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
    public class RoyalPermitSettings : IExposable
    {
        public string minTitle;
        public float permitPointCost;
        public float favorCost;
        public float cooldownDays;
        public float aidDurationDays;
        public float pawnCount;

        public void ExposeData()
        {
            Scribe_Values.Look(ref minTitle, "minTitle");
            Scribe_Values.Look(ref permitPointCost, "permitPointCost");
            Scribe_Values.Look(ref favorCost, "favorCost");
            Scribe_Values.Look(ref cooldownDays, "cooldownDays");
            Scribe_Values.Look(ref aidDurationDays, "aidDurationDays");
            Scribe_Values.Look(ref pawnCount, "pawnCount");
        }
    }
}
