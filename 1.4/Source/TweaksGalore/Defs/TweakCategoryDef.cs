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
    public class TweakCategoryDef : TweakBaseDef
    {
        public int orderID = 9999;

        public List<TweakSectionDef> heldSections;

        public void DoCategoryContents(Listing_Standard listing, TweaksGaloreSettings settings, string filter)
        {
            if(!heldSections.NullOrEmpty())
            {
                foreach (TweakSectionDef section in heldSections)
                {
                    if (filter.NullOrEmpty() || section.FilterForTweak(filter))
                    {
                        listing.DoSection(section, filter);
                    }
                }
            }
        }
    }
}
