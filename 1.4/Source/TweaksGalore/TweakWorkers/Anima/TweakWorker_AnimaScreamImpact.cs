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
    public class TweakWorker_AnimaScreamImpact : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_AnimaTweaks.BoolValue && def.BoolValue)
            {
                if (TGTweakDefOf.Tweak_DisableAnimaScream.BoolValue)
                {
                    TGThoughtDefOf.AnimaScream.durationDays = settings.tweak_animaScreamLength;
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
