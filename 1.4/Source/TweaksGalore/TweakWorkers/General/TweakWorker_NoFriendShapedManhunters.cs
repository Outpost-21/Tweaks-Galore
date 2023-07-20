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
    public class TweakWorker_NoFriendShapedManhunters : TweakWorker
    {
        public override void OnStartup()
        {
            if (def.BoolValue)
            {
                List<PawnKindDef> animalPawnKinds = DefDatabase<PawnKindDef>.AllDefs.Where(pk => pk.RaceProps.Animal).ToList();
                foreach (PawnKindDef def in animalPawnKinds)
                {
                    TweakDef trainability = TGTweakDefOf.Tweak_NFSM_PreventByTrainability;
                    TweakDef nuzzle = TGTweakDefOf.Tweak_NFSM_PreventByNuzzleable;
                    TweakDef wildness = TGTweakDefOf.Tweak_NFSM_PreventByWildness;
                    TweakDef combatPower = TGTweakDefOf.Tweak_NFSM_PreventByCombatPower;
                    if ((settings.GetBoolSetting(trainability.defName + "Intermediate", false) && def.RaceProps.trainability == TrainabilityDefOf.Intermediate) ||
                        (settings.GetBoolSetting(trainability.defName + "Advanced", false) && def.RaceProps.trainability == TrainabilityDefOf.Advanced) ||
                        (settings.GetBoolSetting(nuzzle.defName, nuzzle.DefaultBool) && def.RaceProps.nuzzleMtbHours > 0) ||
                        (settings.GetFloatSetting(wildness.defName, wildness.DefaultFloat) > def.RaceProps.wildness) ||
                        (settings.GetIntSetting(combatPower.defName, combatPower.DefaultInt) > def.combatPower))
                    {
                        def.canArriveManhunter = false;
                        if (TGTweakDefOf.Tweak_NFSM_PreventWhenTaming.BoolValue)
                        {
                            def.race.race.manhunterOnTameFailChance = 0f;
                        }
                    }
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
