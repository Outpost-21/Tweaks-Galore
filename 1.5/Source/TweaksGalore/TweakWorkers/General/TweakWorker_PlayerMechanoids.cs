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
    public class TweakWorker_PlayerMechanoids : TweakWorker
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
                SetMechanoidSkills(settings);
                SetPowerUsage(settings);
            }
        }

        public void SetMechanoidSkills(TweaksGaloreSettings settings)
        {
            List<ThingDef> mechanoidListing = new List<ThingDef>();

            mechanoidListing = DefDatabase<ThingDef>.AllDefs.Where(td => td.race?.IsMechanoid ?? false).ToList();

            for (int i = 0; i < mechanoidListing.Count(); i++)
            {
                ThingDef curMech = mechanoidListing[i];
                curMech.race.mechFixedSkillLevel = TGTweakDefOf.Tweak_MechSkillLevel.IntValue;
            }
        }

        public void SetPowerUsage(TweaksGaloreSettings settings)
        {
            StatDefOf.MechEnergyUsageFactor.defaultBaseValue = TGTweakDefOf.Tweak_MechDischargeRate.FloatValue;
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
