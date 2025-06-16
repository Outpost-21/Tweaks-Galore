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
    public class TweakWorker_GauranlenInitialConnection : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            FloatRange tempRange = settings.GetFloatRangeSetting(def.defName);
            listing.LabelBacked(def.LabelCap, TweaksGaloreMod.mod.settingColor);
            listing.Note(def.description, GameFont.Tiny);
            listing.AddLabeledSlider($"- Minimum: {tempRange.min.ToStringPercent()}", ref tempRange.min, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
            listing.AddLabeledSlider($"- Maximum: {tempRange.max.ToStringPercent()}", ref tempRange.max, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
            settings.SetFloatRangeSetting(def.defName, tempRange);
        }

        public override void OnStartup()
        {
            settings.GetFloatRangeSetting(def.defName, new FloatRange(0.25f, 0.45f));
            ApplyTweak();
        }

        public void ApplyTweak()
        {
            if (TGTweakDefOf.Tweak_GauranlenTweaks.BoolValue)
            {
                CompProperties_TreeConnection treeProps = ThingDefOf.Plant_TreeGauranlen.GetCompProperties<CompProperties_TreeConnection>();
                treeProps.initialConnectionStrengthRange = def.FloatRangeValue;
            }
        }

        public override void OnWriteSettings()
        {
            ApplyTweak();
        }
    }
}
