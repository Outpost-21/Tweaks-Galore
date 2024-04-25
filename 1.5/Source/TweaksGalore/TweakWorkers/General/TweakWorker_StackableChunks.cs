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
            if (!def.ShouldRunTweak()) { return; }
            if (def.BoolValue)
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.AppendLine("Stone Chunks Patched:");
                StringBuilder sb2 = new StringBuilder();
                sb2.AppendLine("Slag Chunks Patched:");
                foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
                {
                    if (!thingDef.thingCategories.NullOrEmpty())
                    {
                        if (thingDef.thingCategories.Contains(ThingCategoryDefOf.StoneChunks))
                        {
                            thingDef.stackLimit = TGTweakDefOf.Tweak_StackableChunks_Stone.IntValue;
                            thingDef.drawGUIOverlay = true;
                            thingDef.passability = default(Traversability);
                            thingDef.saveCompressible = default(bool);
                            thingDef.ResolveReferences();
                            thingDef.PostLoad();
                            ResourceCounter.ResetDefs();
                            sb1.AppendLine($"{thingDef.LabelCap} ({thingDef.defName})");
                        }
                        if (thingDef.thingCategories.Contains(ThingCategoryDefOf.Chunks))
                        {
                            thingDef.stackLimit = TGTweakDefOf.Tweak_StackableChunks_Slag.IntValue;
                            thingDef.drawGUIOverlay = true;
                            thingDef.passability = default(Traversability);
                            thingDef.saveCompressible = default(bool);
                            thingDef.ResolveReferences();
                            thingDef.PostLoad();
                            ResourceCounter.ResetDefs();
                            sb2.AppendLine($"{thingDef.LabelCap} ({thingDef.defName})");
                        }
                    }
                }
                LogUtil.LogMessage(sb1.ToString());
                LogUtil.LogMessage(sb2.ToString());
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
