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
    public class TweakWorker_MechanoidHeatArmor : TweakWorker
    {
        public static List<ThingDef> allMechanoidDefs = new List<ThingDef>();

        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            listing.DoSetting(def);
        }

        public override void OnStartup()
        {
            base.OnStartup();
            if (def.FloatValue == 2f) { return; }

            if (!GetAllMechanoids.EnumerableNullOrEmpty())
            {
                float heatValue = def.FloatValue;
                foreach (ThingDef mechDef in GetAllMechanoids)
                {
                    if (mechDef != null & !mechDef.defName.Contains("Corpse"))
                    {
                        StatModifier heatStat = mechDef?.statBases?.Find(stat => stat.stat == StatDefOf.ArmorRating_Heat);
                        if (heatStat != null && heatStat.value == 2f)
                        {
                            heatStat.value = heatValue;
                        }
                        else
                        {
                            LogUtil.LogMessage("Mechanoid Recognised: " + mechDef.defName + "/" + mechDef.label + ", but not patched as it has no heat armour value or is not the same as vanilla mechanoids. This is intended behaviour not an error.");
                        }
                    }
                }
            }
        }

        public List<ThingDef> GetAllMechanoids
        {
            get
            {
                if (allMechanoidDefs.NullOrEmpty())
                {
                    IEnumerable<ThingDef> enumerable = from def in DefDatabase<ThingDef>.AllDefs
                                                       where def?.race?.IsMechanoid ?? false
                                                       select def;

                    allMechanoidDefs = enumerable.ToList();

                    if (!def.tweakThings.NullOrEmpty())
                    {
                        foreach(ThingDef thing in def.tweakThings)
                        {
                            allMechanoidDefs.Add(thing);
                        }
                    }
                }
                return allMechanoidDefs;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
