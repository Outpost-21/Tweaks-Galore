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
    public class TweakWorker_GeneDefMetabolism : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            base.OnStartup();
            if (def.BoolValue)
            {
                foreach (GeneDef gene in DefDatabase<GeneDef>.AllDefs)
                {
                    gene.biostatMet = 0;
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
