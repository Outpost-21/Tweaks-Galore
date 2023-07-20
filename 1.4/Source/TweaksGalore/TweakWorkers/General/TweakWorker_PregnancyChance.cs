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
    public class TweakWorker_PregnancyChance : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            RegisterAlteredPregnancyChances();
            UpdatePregnancyChances();
        }

        public void UpdatePregnancyChances()
        {
            for (int i = 0; i < settings.tweak_pregnancyChanceEditedPawnKinds.Count; i++)
            {
                PawnKindDef curDef = settings.tweak_pregnancyChanceEditedPawnKinds[i];
                curDef.humanPregnancyChance = settings.tweak_defaultPregnancyChance;
            }
        }

        public void RegisterAlteredPregnancyChances()
        {
            foreach (PawnKindDef def in DefDatabase<PawnKindDef>.AllDefs)
            {
                if (def.humanPregnancyChance != 0f && def.humanPregnancyChance != 1f)
                {
                    if (!settings.tweak_pregnancyChanceEditedPawnKinds.Contains(def))
                    {
                        settings.tweak_pregnancyChanceEditedPawnKinds.Add(def);
                    }
                }
            }
            LogUtil.LogMessage("Registered " + settings.tweak_pregnancyChanceEditedPawnKinds.Count + " pawnKinds for tweak_pregnancyChanceEditedPawnKinds.");
        }

        public override void OnWriteSettings()
        {
            UpdatePregnancyChances();
        }
    }
}
