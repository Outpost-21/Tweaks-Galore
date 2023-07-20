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
    public class TweakWorker_FreeMeditation : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (def.BoolValue)
            {
                List<ThingDef> meditationObjects = DefDatabase<ThingDef>.AllDefs.Where(td => td.GetCompProperties<CompProperties_MeditationFocus>() != null).ToList();
                List<MeditationFocusDef> focusDefs = DefDatabase<MeditationFocusDef>.AllDefs.ToList();

                for (int i = 0; i < meditationObjects.Count(); i++)
                {
                    ThingDef curObj = meditationObjects[i];
                    CompProperties_MeditationFocus curComp = curObj.GetCompProperties<CompProperties_MeditationFocus>();
                    for (int j = 0; j < focusDefs.Count(); j++)
                    {
                        if (!curComp.focusTypes.Contains(focusDefs[j]))
                        {
                            curComp.focusTypes.Add(focusDefs[j]);
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
