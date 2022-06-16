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

            try { CompatibilityChecks(settings); } catch (Exception e) { LogUtil.LogError("Caught exeption in Tweaks Galore startup: " + e); };
            try { Tweak_FixDeconstructionReturn(settings); } catch (Exception e) { LogUtil.LogError("Caught exception in Tweak: FixDeconstructionReturn :: " + e); };
            try { Tweak_StackableChunks(settings); } catch (Exception e) { LogUtil.LogError("Caught exception in Tweak: StackableChunks :: " + e); };
            try { Tweak_MechanoidHeatArmor(settings); } catch (Exception e) { LogUtil.LogError("Caught exception in Tweak: MechanoidHeatArmor :: " + e); };
            try { Tweak_PowerUsageTweaks(settings); } catch (Exception e) { LogUtil.LogError("Caught exception in Tweak: PowerUsageTweaks :: " + e); };
            try { Tweak_StrongFloorsBlockInfestations(settings); } catch (Exception e) { LogUtil.LogError("Caught exception in Tweak: StrongFloorsBlockInfestations :: " + e); };
            try { Tweak_NoFriendShapedManhunters(settings);  } catch (Exception e) { LogUtil.LogError("Caught exception in Tweak: NoFriendShapedManhunters :: " + e); };
        }

        public static void Tweak_NoFriendShapedManhunters(TweaksGaloreSettings settings)
        {
            // Tweak: No Friend Shaped Manhunters
            if (settings.tweak_noFriendShapedManhunters)
            {
                List<PawnKindDef> animalPawnKinds = DefDatabase<PawnKindDef>.AllDefs.Where(pk => pk.RaceProps.Animal).ToList();
                foreach (PawnKindDef def in animalPawnKinds)
                {
                    if((settings.tweak_NFSMTrainability_Intermediate && def.RaceProps.trainability == TrainabilityDefOf.Intermediate) ||
                        (settings.tweak_NFSMTrainability_Intermediate && def.RaceProps.trainability == TrainabilityDefOf.Advanced) ||
                        (settings.tweak_NFSMNuzzleHours && def.RaceProps.nuzzleMtbHours >= 0) ||
                        (settings.tweak_NFSMWildness > def.RaceProps.wildness) ||
                        (settings.tweak_NFSMCombatPower > def.combatPower))
                    {
                        def.canArriveManhunter = false;
                        if (settings.tweak_NFSMDisableManhunterOnTame)
                        {
                            def.race.race.manhunterOnTameFailChance = 0f;
                        }
                    }
                }
            }
        }

        public static void CompatibilityChecks(TweaksGaloreSettings settings)
        {
            if (ModLister.GetActiveModWithIdentifier("kentington.saveourship2") != null && settings.tweak_noBreakdowns)
            {
                LogUtil.LogWarning("SOS2 detected during startup. Disabling No Breakdowns tweak due to incompatibility.");
                settings.tweak_noBreakdowns = false;
            }
        }

        public static void Tweak_FixDeconstructionReturn(TweaksGaloreSettings settings)
        {
            // Tweak: Fix Deconstruction Return
            if (settings.tweak_fixDeconstructionReturn)
            {
                foreach (BuildableDef buildableDef in DefDatabase<BuildableDef>.AllDefs)
                {
                    buildableDef.resourcesFractionWhenDeconstructed = 1f;
                }
            }
        }

        public static void Tweak_StackableChunks(TweaksGaloreSettings settings)
        {
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
        }

        public static void Tweak_MechanoidHeatArmor(TweaksGaloreSettings settings)
        {
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
                            LogUtil.LogMessage("O21 :: Tweaks Galore :: Mechanoid Recognised: " + def.defName + "/" + def.label + ", but not patched as it has no heat armour value. This is intended behaviour not an error.");
                        }
                    }
                }
            }
        }

        public static void Tweak_PowerUsageTweaks(TweaksGaloreSettings settings)
        {
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
        }

        public static void Tweak_StrongFloorsBlockInfestations(TweaksGaloreSettings settings)
        {
            // Strong Floors Block Infestations
            if (settings.patch_strongFloorsStopInfestations)
            {
                List<TerrainDef> strongFloors = new List<TerrainDef>();

                foreach (TerrainDef terrain in DefDatabase<TerrainDef>.AllDefs)
                {
                    if (terrain != null)
                    {
                        if (terrain.costList != null && terrain.costList.Any(t => t.thingDef.stuffProps != null && ((bool)(t.thingDef?.stuffProps?.categories?.Contains(StuffCategoryDefOf.Metallic)) || (bool)(t.thingDef?.stuffProps?.categories?.Contains(StuffCategoryDefOf.Stony)))))
                        {
                            strongFloors.Add(terrain);
                        }
                        else if (!terrain.stuffCategories.NullOrEmpty() && (terrain.stuffCategories.Contains(StuffCategoryDefOf.Metallic) || terrain.stuffCategories.Contains(StuffCategoryDefOf.Stony)))
                        {
                            strongFloors.Add(terrain);
                        }
                    }
                }

                for (int i = 0; i < strongFloors.Count; i++)
                {
                    if (strongFloors[i].tags == null)
                    {
                        strongFloors[i].tags = new List<string>();
                    }
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
