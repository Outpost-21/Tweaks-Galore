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
    public class TweakSectionDef : TweakBaseDef
    {
        public Type sectionWorker = typeof(SectionWorker);

        public List<TweakSubSectionDef> heldSubSections;

        public bool sortTweaks = true;

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
