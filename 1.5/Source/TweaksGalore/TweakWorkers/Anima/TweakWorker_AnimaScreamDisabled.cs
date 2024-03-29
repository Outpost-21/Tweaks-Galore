﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace TweaksGalore
{
    public class TweakWorker_AnimaScreamDisabled : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_AnimaTweaks.BoolValue && def.BoolValue)
            {
                ThingDef animaTree = ThingDefOf.Plant_TreeAnima;
                animaTree.comps.Remove(animaTree.GetCompProperties<CompProperties_GiveThoughtToAllMapPawnsOnDestroy>());
                animaTree.comps.Remove(animaTree.GetCompProperties<CompProperties_PlaySoundOnDestroy>());
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
