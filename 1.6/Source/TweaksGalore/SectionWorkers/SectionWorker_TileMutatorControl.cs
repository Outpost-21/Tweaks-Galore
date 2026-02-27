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
    public class SectionWorker_TileMutatorControl : SectionWorker
    {
        public override void DoSectionContents(Listing_Standard listing, string filter)
        {
            base.DoSectionContents(listing, filter);
            listing.DoSettingBool("TweaksGalore.TileMutatorControlBool".Translate(), "TweaksGalore.TileMutatorControlBoolDesc".Translate(), def.defName, false, true);
            if (settings.GetBoolSetting(def.defName, false))
            {
                foreach (TileMutatorDef mutator in DefDatabase<TileMutatorDef>.AllDefs)
                {
                    bool value = settings.tweak_tileMutatorControlSettings[mutator.defName];
                    listing.CheckboxLabeled(mutator.LabelCap, ref value);
                    settings.tweak_tileMutatorControlSettings[mutator.defName] = value;
                }
            }
        }

        public override void DoSectionRestore()
        {
            base.DoSectionRestore();
            settings.tweak_tileMutatorControlSettings = new Dictionary<string, bool>();
            RegisterValidTileMutators();
        }

        public override void DoOnStartup()
        {
            RegisterValidTileMutators();
        }

        public void RegisterValidTileMutators()
        {
            if (settings.tweak_tileMutatorControlSettings.NullOrEmpty())
            {
                settings.tweak_tileMutatorControlSettings = new Dictionary<string, bool>();
            }
            foreach (TileMutatorDef mutator in DefDatabase<TileMutatorDef>.AllDefsListForReading)
            {
                if (!settings.tweak_tileMutatorControlSettings.ContainsKey(mutator.defName))
                {
                    settings.tweak_tileMutatorControlSettings.Add(mutator.defName, true);
                }
            }
        }
    }
}
