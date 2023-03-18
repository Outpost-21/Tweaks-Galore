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
    public class RoyalTitleSettings : IExposable
    {
        public float favorCost;
        public float heirQuestPoints;
        public float permitPoints;
        public bool dignifiedMeditation;
        public bool enableWork;
        public WorkTags disabledWorkTags;
        public bool? disableThroneroomRequirements;
        public bool? disableBedroomRequirements;

        [Unsaved]
        public bool hasBedroomReqs;
        [Unsaved]
        public bool hasThroneroomReqs;

        public void ExposeData()
        {
            Scribe_Values.Look(ref favorCost, "favorCost");
            Scribe_Values.Look(ref heirQuestPoints, "heirQuestPoints");
            Scribe_Values.Look(ref permitPoints, "permitPoints");
            Scribe_Values.Look(ref dignifiedMeditation, "dignifiedMeditation");
            Scribe_Values.Look(ref enableWork, "enableWork");
            Scribe_Values.Look(ref disabledWorkTags, "disabledWorkTags");
            Scribe_Values.Look(ref disableThroneroomRequirements, "disableThroneroomRequirements");
            Scribe_Values.Look(ref disableBedroomRequirements, "disableBedroomRequirements");
        }
    }
}
