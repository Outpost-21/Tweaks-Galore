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
    public class TweakBaseDef : Def
    {
        public List<string> required = new List<string>();

        public List<string> incompatible = new List<string>();

        public TweakCategoryDef category;

        public List<TweakCategoryDef> categories;

        public TweakSectionDef section;

        public List<TweakSectionDef> sections;

        public TweakSubSectionDef subSection;

        public List<TweakSubSectionDef> subSections;

        public List<TweakDef> heldTweaks;
    }
}
