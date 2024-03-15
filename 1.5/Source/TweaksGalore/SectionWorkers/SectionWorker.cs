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
    public class SectionWorker
    {
        public TweakSectionDef def;

        public TweaksGaloreMod mod = TweaksGaloreMod.mod;

        public TweaksGaloreSettings settings = TweaksGaloreMod.settings;

        public SectionWorker() { }

        public virtual void DoSectionContents(Listing_Standard listing, string filter)
        {
            // Do Defaults Button
            List<TweakDef> nestedTweaks = def.GetAllNestedTweaks();
            if (!nestedTweaks.NullOrEmpty())
            {
                if (listing.ButtonTextLabeled("", "Restore Section Defaults"))
                {
                    DoSectionRestore();
                    Messages.Message($"Tweaks Galore: '{def.LabelCap}' tweaks restored to defaults. Restart required to take full effect.", MessageTypeDefOf.CautionInput);
                }
            }
            // Do Individual Tweaks
            if (!def.heldTweaks.NullOrEmpty())
            {
                if (def.sortTweaks)
                {
                    def.heldTweaks.SortBy(t => t.label);
                }
                for (int i = 0; i < def.heldTweaks.Count; i++)
                {
                    TweakDef tweak = def.heldTweaks[i];
                    if ((tweak.label.ToLower().Contains(filter.ToLower()) || tweak.description.ToLower().Contains(filter.ToLower())) && (tweak.required.NullOrEmpty() || tweak.required.All(r => ModLister.GetActiveModWithIdentifier(r) != null)) && (tweak.incompatible.NullOrEmpty() || tweak.incompatible.All(i => ModLister.GetActiveModWithIdentifier(i) == null)))
                    {
                        tweak.Worker.DoTweakContents(listing, filter);
                    }
                }
            }
            // Do Sub-Sections
            if (!def.heldSubSections.NullOrEmpty())
            {
                for (int i = 0; i < def.heldSubSections.Count; i++)
                {
                    TweakSubSectionDef subSection = def.heldSubSections[i];
                    if (subSection.FilterForTweak(filter) && (subSection.required.NullOrEmpty() || subSection.required.All(r => ModLister.GetActiveModWithIdentifier(r) != null)) && (subSection.incompatible.NullOrEmpty() || subSection.incompatible.All(i => ModLister.GetActiveModWithIdentifier(i) == null)))
                    {
                        listing.DoSubSection(subSection);
                    }
                }
            }
        }

        public virtual void DoOnStartup()
        {

        }

        public virtual void DoSectionRestore()
        {
            List<TweakDef> nestedTweaks = def.GetAllNestedTweaks();
            for (int i = 0; i < nestedTweaks.Count; i++)
            {
                TweakDef curTweak = nestedTweaks[i];
                curTweak.Worker.OnRestore();
            }
        }

        public virtual void DoWriteSettings()
        {

        }
    }
}
