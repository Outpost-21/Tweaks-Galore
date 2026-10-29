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
    public class TweakWorker_ArcheanEffectRate : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            base.OnStartup();
            if (TGTweakDefOf.Tweak_ArcheanTweaks.BoolValue)
            {
                CompProperties_Terraformer terraformerComp = TGThingDefOf.Plant_TreeArchean.GetCompProperties<CompProperties_Terraformer>();
                terraformerComp.secondsPerConvert = def.IntValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
