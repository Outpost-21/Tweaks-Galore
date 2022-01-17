using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

namespace TweaksGalore
{
    [StaticConstructorOnStartup]
    public static class TweaksGaloreStartup
    {
        public static List<ThingDef> allMechanoidDefs = new List<ThingDef>();

        static TweaksGaloreStartup()
        {
			TweaksGaloreSettings settings = TweaksGaloreMod.settings;

            // Tweak: Fix Deconstruction Return
            if (settings.tweak_fixDeconstructionReturn)
            {
                foreach (BuildableDef buildableDef in DefDatabase<BuildableDef>.AllDefs)
                {
                    buildableDef.resourcesFractionWhenDeconstructed = 1f;
                }
            }

            // Tweak: Stackable Chunks
            if (settings.tweak_stackableChunks)
            {

                foreach (ThingDef thingDef in DefDatabase<ThingDef>.AllDefs)
                {
                    if (!thingDef.thingCategories.NullOrEmpty())
                    {
                        if (thingDef.thingCategories.Contains(ThingCategoryDefOf.StoneChunks))
                        {
                            thingDef.stackLimit = (int)settings.tweak_stackableChunks_stone;
                            thingDef.ResolveReferences();
                            thingDef.PostLoad();
                            ResourceCounter.ResetDefs();
                        }
                        if (thingDef.thingCategories.Contains(ThingCategoryDefOf.Chunks))
                        {
                            thingDef.stackLimit = (int)settings.tweak_stackableChunks_slag;
                            thingDef.ResolveReferences();
                            thingDef.PostLoad();
                            ResourceCounter.ResetDefs();
                        }
                    }
                }
            }

            // Tweak: Mechanoid Heat Armor
            if (!GetAllMechanoids.EnumerableNullOrEmpty())
            {
                foreach (ThingDef def in GetAllMechanoids)
                {
                    if (def != null & !def.defName.Contains("Corpse"))
                    {
                        StatModifier heatStat = def?.statBases?.Find(stat => stat.stat == StatDefOf.ArmorRating_Heat);
                        if (heatStat != null)
                        {
                            heatStat.value = settings.tweak_mechanoidHeatArmour;
                        }
                        else
                        {
                            Log.Message("O21 :: Tweaks Galore :: Mechanoid Recognised: " + def.defName + "/" + def.label + ", but not patched as it has no heat armour value. This is intended behaviour not an error.");
                        }
                    }
                }
            }

            // Power Usage Tweaks
            if (settings.tweak_powerUsageTweaks)
            {
                List<ThingDef> standingLamps = new List<ThingDef>
                {
                    DefDatabase<ThingDef>.GetNamedSilentFail("StandingLamp"),
                    DefDatabase<ThingDef>.GetNamedSilentFail("StandingLamp_Red"),
                    DefDatabase<ThingDef>.GetNamedSilentFail("StandingLamp_Green"),
                    DefDatabase<ThingDef>.GetNamedSilentFail("StandingLamp_Blue")
                };

                if (!standingLamps.NullOrEmpty())
                {
                    for (int i = 0; i < standingLamps.Count; i++)
                    {
                        standingLamps[i].SetPowerUsage((int)settings.tweak_powerUsage_lamp);
                    }
                }

                DefDatabase<ThingDef>.GetNamedSilentFail("SunLamp").SetPowerUsage((int)settings.tweak_powerUsage_sunlamp);
                DefDatabase<ThingDef>.GetNamedSilentFail("Autodoor").SetPowerUsage((int)settings.tweak_powerUsage_autodoor);
                DefDatabase<ThingDef>.GetNamedSilentFail("VanometricPowerCell").SetPowerUsage(-(int)settings.tweak_powerUsage_vanometricCell);
            }

            // Strong Floors Block Infestations
            if (settings.patch_strongFloorsStopInfestations)
            {
                List<TerrainDef> strongFloors = new List<TerrainDef>();

                foreach(TerrainDef terrain in DefDatabase<TerrainDef>.AllDefs)
                {
                    if(terrain != null) 
                    { 
                        if(terrain.costList != null && terrain.costList.Any(t => t.thingDef.stuffProps != null && (t.thingDef.stuffProps.categories.Contains(StuffCategoryDefOf.Metallic) || t.thingDef.stuffProps.categories.Contains(StuffCategoryDefOf.Stony))))
                        {
                            strongFloors.Add(terrain);
                        }
                        else if(!terrain.stuffCategories.NullOrEmpty() && (terrain.stuffCategories.Contains(StuffCategoryDefOf.Metallic) || terrain.stuffCategories.Contains(StuffCategoryDefOf.Stony)))
                        {
                            strongFloors.Add(terrain);
                        }
                    }
                }

                for (int i = 0; i < strongFloors.Count; i++)
                {
                    strongFloors[i].tags.Add("BlocksInfestations");
                }
            }
        }

        public static void SetPowerUsage(this ThingDef thing, int powerConsumption)
        {
            if(thing != null)
            {
                CompProperties_Power comp = thing.GetCompProperties<CompProperties_Power>();
                if(comp != null)
                {
                    thing.GetCompProperties<CompProperties_Power>().basePowerConsumption = powerConsumption;
                }
            }
        }

        public static List<ThingDef> GetAllMechanoids
        {
            get
            {
                if (allMechanoidDefs.NullOrEmpty())
                {
                    IEnumerable<ThingDef> enumerable = from def in DefDatabase<ThingDef>.AllDefs
                                                       where def?.race?.IsMechanoid ?? false
                                                       select def;

                    allMechanoidDefs = enumerable.ToList();

                    ThingDef alpha = DefDatabase<ThingDef>.GetNamedSilentFail("O21_Alien_MechadroidAlpha");
                    ThingDef delta = DefDatabase<ThingDef>.GetNamedSilentFail("O21_Alien_MechadroidDelta");
                    ThingDef gamma = DefDatabase<ThingDef>.GetNamedSilentFail("O21_Alien_MechadroidGamma");

                    if (alpha != null) { allMechanoidDefs.Add(alpha); }
                    if (delta != null) { allMechanoidDefs.Add(delta); }
                    if (gamma != null) { allMechanoidDefs.Add(gamma); }
                }
                return allMechanoidDefs;
            }
        }
    }
}
