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
    public class TweakWorker
    {
        public TweakDef def;

        public TweaksGaloreMod mod = TweaksGaloreMod.mod;

        public TweaksGaloreSettings settings = TweaksGaloreMod.settings;

        public TweakWorker() { }

        public virtual void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public virtual void OnStartup()
        {

        }

        public virtual void OnRestore()
        {
            switch (def.tweakType)
            {
                case TweakType.Bool:
                    settings.SetBoolSetting(def.defName, def.DefaultBool);
                    break;
                case TweakType.Int:
                    settings.SetIntSetting(def.defName, def.DefaultInt);
                    break;
                case TweakType.IntRange:
                    settings.SetIntRangeSetting(def.defName, def.DefaultIntRange);
                    break;
                case TweakType.Float:
                    settings.SetFloatSetting(def.defName, def.DefaultFloat);
                    break;
                case TweakType.FloatRange:
                    settings.SetFloatRangeSetting(def.defName, def.DefaultFloatRange);
                    break;
                default:
                    break;
            }
        }

        public virtual string Description()
        {
            string desc = def.description ?? "No Description";
            if(def.tweakThing != null)
            {
                desc += "\n\nAffects:\n- " + def.tweakThing.LabelCap;
            }
            if(!def.tweakThings.NullOrEmpty())
            {
                desc += "\n\nAffects:";
                foreach(ThingDef thing in def.tweakThings)
                {
                    desc += "\n- " + thing.LabelCap;
                }
            }
            return desc;

        }

        public virtual void OnWriteSettings()
        {

        }
    }
}
