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
    public class TweakWorker_NoBiocoding : TweakWorker
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
                foreach (PawnKindDef pawnKind in DefDatabase<PawnKindDef>.AllDefs)
                {
                    if (pawnKind.biocodeWeaponChance != 0f)
                    {
                        pawnKind.biocodeWeaponChance = 0f;
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
