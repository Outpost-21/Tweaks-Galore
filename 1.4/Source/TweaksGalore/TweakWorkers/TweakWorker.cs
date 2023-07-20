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

        public virtual void OnWriteSettings()
        {

        }
    }
}
