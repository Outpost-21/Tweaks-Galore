using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace TweaksGalore
{
    [HarmonyPatch(typeof(ResearchManager), "ReapplyAllMods")]
    public static class Patch_ResearchManager_ReapplyAllMods
    {
        [HarmonyPostfix]
        public static void Postfix(ResearchManager __instance)
        {
            if (TGTweakDefOf.Tweak_TechTraversal_Enabled.BoolValue)
            {
                FactionDef playerFactionDef = Faction.OfPlayer.def;

                // Restore Original Tech Level or if setting true start with neolithic
                if (TGTweakDefOf.Tweak_TechTraversal_AlwaysLowestLevel.BoolValue)
                {
                    playerFactionDef.techLevel = TweaksGaloreMod.settings.tweak_tech_lowestTechLevel;
                }
                else
                {
                    playerFactionDef.techLevel = TweaksGaloreMod.settings.factionTechMap.GetValueSafe(Faction.OfPlayer.def);
                }

                // Advance Tech Level as Necessary
                while (playerFactionDef.techLevel < TechLevel.Ultra && TechLevelConsideredCompleted(playerFactionDef.techLevel))
                {
                    playerFactionDef.techLevel++;
                    LogUtil.LogMessage("Upgraded player tech level to " + playerFactionDef.techLevel.ToStringHuman());
                }
            }
        }

        public static bool TechLevelConsideredCompleted(TechLevel techLevel)
        {
            bool result = false;

            List<ResearchProjectDef> allResearchForTechLevel;
            if (TGTweakDefOf.Tweak_TechTraversal_OnlyVanillaResearch.BoolValue)
            {
                allResearchForTechLevel = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(rpd => rpd.techLevel == techLevel && rpd.modContentPack != null && (rpd.modContentPack.IsCoreMod || rpd.modContentPack.IsOfficialMod) && (!TGTweakDefOf.Tweak_TechTraversal_IgnoreTechprints.BoolValue || rpd.TechprintCount <= 0)).ToList();
            }
            else
            {
                allResearchForTechLevel = DefDatabase<ResearchProjectDef>.AllDefsListForReading.Where(rpd => rpd.techLevel == techLevel && (!TGTweakDefOf.Tweak_TechTraversal_IgnoreTechprints.BoolValue || rpd.TechprintCount <= 0)).ToList();
            }
            float completedPercentage = (float)allResearchForTechLevel.Where(rpd => rpd.IsFinished).Count() / (float)allResearchForTechLevel.Count();

            if (completedPercentage >= TGTweakDefOf.Tweak_TechTraversal_PercentageNeeded.FloatValue)
            {
                result = true;
            }

            return result;
        }
    }
}
