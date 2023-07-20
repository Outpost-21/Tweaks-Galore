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
    public class TweakSectionDef : Def
    {
        public Type sectionWorker = typeof(SectionWorker);

        public List<string> required = new List<string>();

        public List<string> incompatible = new List<string>();

        public List<TweakSubSectionDef> subSections;

        public List<TweakDef> tweaks;

        public SectionWorker workerInt;

        public SectionWorker Worker
        {
            get
            {
                if(workerInt == null)
                {
                    workerInt = (SectionWorker)Activator.CreateInstance(sectionWorker);
                    workerInt.def = this;
                }
                return workerInt;
            }
        }
    }
}
