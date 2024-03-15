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
            base.OnStartup();
            if (def.BoolValue)
            {
                SetImplantShit(settings);
                SetMechanoidSkills(settings);
                SetPowerUsage(settings);
            }
        }

        public void SetImplantShit(TweaksGaloreSettings settings)
        {
            HediffDef implantDef = TGHediffDefOf.MechlinkImplant;
            HediffStage implantStage = implantDef.stages.First();
            implantStage.statOffsets.Find(sm => sm.stat == StatDefOf.MechBandwidth).value = TGTweakDefOf.Tweak_MechanitorBandwidthBase.IntValue;
            implantStage.statOffsets.Find(sm => sm.stat == StatDefOf.MechControlGroups).value = TGTweakDefOf.Tweak_MechanitorControlGroupBase.IntValue;
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
