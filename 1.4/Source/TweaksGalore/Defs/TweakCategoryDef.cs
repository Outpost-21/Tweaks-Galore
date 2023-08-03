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
    public class TweakCategoryDef : Def
    {
        public int orderID = 9999;

        public List<string> required = new List<string>();

        public List<string> incompatible = new List<string>();

        public List<TweakSectionDef> sections;

        public void DoCategoryContents(Listing_Standard listing, TweaksGaloreSettings settings, string filter)
        {
            if(!sections.NullOrEmpty())
            {
                foreach (TweakSectionDef section in sections)
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
