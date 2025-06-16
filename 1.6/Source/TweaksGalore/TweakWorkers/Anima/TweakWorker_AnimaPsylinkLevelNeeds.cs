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
    public class TweakWorker_AnimaPsylinkLevelNeeds : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.Label(def.LabelCap);
            {
                listing.Note(def.description, GameFont.Tiny);
                if (GetPsylinkStuff)
                {
                    int levelint = 0;
                    for (int i = 0; i < settings.tweak_animaPsylinkLevelNeeds.Count; i++)
                    {
                        levelint++;
                        string intBufferString = settings.tweak_animaPsylinkLevelNeeds[i].ToString();
                        int intBufferInt = settings.tweak_animaPsylinkLevelNeeds[i];
                        listing.TextFieldNumericLabeled("Psylink Level " + levelint, ref intBufferInt, ref intBufferString, 0, 500);
                        settings.tweak_animaPsylinkLevelNeeds[i] = intBufferInt;
                    }
                }
            }
        }

        public bool GetPsylinkStuff
        {
            get
            {
                if (settings.tweak_animaPsylinkLevelNeeds.NullOrEmpty())
                {
                    CompProperties_Psylinkable psycomp = ThingDefOf.Plant_TreeAnima.GetCompProperties<CompProperties_Psylinkable>();
                    settings.tweak_animaPsylinkLevelNeeds = psycomp.requiredSubplantCountPerPsylinkLevel;
                }
                return true;
            }
        }

        public override void OnStartup()
        {
            if (GetPsylinkStuff) 
            {
                // Intentionally does nothing, just calls it for the values.
            }
            if (TGTweakDefOf.Tweak_AnimaTweaks.BoolValue)
            {
                CompProperties_Psylinkable psycomp = ThingDefOf.Plant_TreeAnima.GetCompProperties<CompProperties_Psylinkable>();
                psycomp.requiredSubplantCountPerPsylinkLevel = settings.tweak_animaPsylinkLevelNeeds;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
