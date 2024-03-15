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
    public class TweakWorker_StackableChunks : TweakWorker
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
                foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
                {
                    if (!thingDef.thingCategories.NullOrEmpty())
                    {
                        if (thingDef.thingCategories.Contains(ThingCategoryDefOf.StoneChunks))
                        {
                            thingDef.stackLimit = TGTweakDefOf.Tweak_StackableChunks_Stone.IntValue;
                            thingDef.ResolveReferences();
                            thingDef.PostLoad();
                            ResourceCounter.ResetDefs();
                        }
                        if (thingDef.thingCategories.Contains(ThingCategoryDefOf.Chunks))
                        {
                            thingDef.stackLimit = TGTweakDefOf.Tweak_StackableChunks_Slag.IntValue;
                            thingDef.ResolveReferences();
                            thingDef.PostLoad();
                            ResourceCounter.ResetDefs();
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
