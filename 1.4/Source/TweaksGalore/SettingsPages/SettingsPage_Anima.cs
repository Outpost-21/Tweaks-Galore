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
    public static class SettingsPage_Anima
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Anima(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Anima Tweaks", "This entire section is disabled by default for compatibility sake mostly, some Anima related tweaks function regardless of if they've been changed (any changing a radius for example) so without this setting could have caused compatibility issue with other Anima related mods if you prefer to use those.", ref settings.tweak_animaTweaks);
            if (settings.tweak_animaTweaks)
            {
                listing.GapLine();
                // Tweak: Replantable Anima
                listing.CheckboxEnhanced("Replantable Anima", "Makes it so you can move Anima trees like any other.", ref settings.tweak_replantableAnima);
                listing.GapLine();
                // Tweak: Disable Anima Scream
                listing.CheckboxEnhanced("Disable Anima Scream", "Disables the Anima Scream debuff from chopping them down.", ref settings.tweak_animaDisableScream);
                listing.GapLine();
                if (!settings.tweak_animaDisableScream)
                {
                    listing.AddLabeledSlider($"- Scream Debuff: {settings.tweak_animaScreamDebuff.ToString("0")}", ref settings.tweak_animaScreamDebuff, -20f, 20f, "Min: -20", "Max: 20", 1f);
                    listing.AddLabeledSlider($"- Scream Duration: {settings.tweak_animaScreamLength.ToString("0.0")}", ref settings.tweak_animaScreamLength, 0.1f, 20f, "Min: 0.1", "Max: 20", 0.1f);
                    listing.AddLabeledSlider($"- Scream Stack Limit: {settings.tweak_animaScreamStackLimit.ToString("0")}", ref settings.tweak_animaScreamStackLimit, 1f, 20f, "Min: 1", "Max: 20", 1f);
                    listing.GapLine();
                }
                // Tweak: Anima Grass per Psylink Level
                listing.Label("Anima Grass per Psylink Level");
                {
                    listing.Note("The values are the amount of grass needed to be grown on top of what was already needed for the previous level.", GameFont.Tiny, Color.gray);
                    if (GetPsylinkStuff)
                    {
                        int levelint = 0;
                        for (int i = 0; i < settings.tweak_animaPsylinkLevelNeeds.Count; i++)
                        {
                            levelint++;
                            string intBufferString = settings.tweak_animaPsylinkLevelNeeds[i].ToString();
                            int intBufferInt = settings.tweak_animaPsylinkLevelNeeds[i];
                            listing.TextFieldNumericLabeled("Psylink Level " + levelint, ref intBufferInt, ref intBufferString, 0, 500);
                            settings.tweak_animaPsylinkLevelNeeds[i] = intBufferInt;
                        }
                    }
                }
                listing.GapLine();
                // Tweak: Nature Shrines Always Buildable
                listing.CheckboxEnhanced("Nature Shrines Always Buildable", "Nature Shrines are usually only buildable when you have a nature based Psycaster, this unlocks that limitation.", ref settings.tweak_animaBuildableShrines);
                listing.GapLine();

                listing.Label("Tree Tweaks");
                listing.Note("These are specific to the Anima Tree itself.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                // Tweak: Artificial Building Radius
                listing.AddLabeledSlider($"- Artificial Building Radius: {settings.tweak_animaArtificialBuildingRadius.ToString("0.0")}", ref settings.tweak_animaArtificialBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a debuff is applied to the trees effects if artificial buildings are built in it.", GameFont.Tiny, Color.gray);
                // Tweak: Natural Building Radius
                listing.AddLabeledSlider($"- Natural Building Radius: {settings.tweak_animaBuffBuildingRadius.ToString("0.0")}", ref settings.tweak_animaBuffBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a buff is applied to the trees effects for natural buildings.", GameFont.Tiny, Color.gray);
                // Tweak: Max Natural Buildings
                listing.AddLabeledSlider($"- Max Natural Buildings: {settings.tweak_animaMaxBuffBuildings}", ref settings.tweak_animaMaxBuffBuildings, 1f, 40f, "Min: 1", "Max: 40", 1f);
                listing.Note("The maximum number of buildings which can buff the Anima Tree.", GameFont.Tiny, Color.gray);
                listing.GapLine();

                listing.Label("Shrine Tweaks");
                listing.Note("These are specific to the nature shrines that can be built to enhance the Anima tree.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                // Tweak: Artificial Building Radius
                listing.AddLabeledSlider($"- Artificial Building Radius: {settings.tweak_animaShrineBuildingRadius.ToString("0.0")}", ref settings.tweak_animaShrineBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a debuff is applied to the trees effects if artificial buildings are built in it.", GameFont.Tiny, Color.gray);
                // Tweak: Natural Building Radius
                listing.AddLabeledSlider($"- Natural Building Radius: {settings.tweak_animaShrineBuffBuildingRadius.ToString("0.0")}", ref settings.tweak_animaShrineBuffBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a buff is applied to the trees effects for natural buildings.", GameFont.Tiny, Color.gray);
                // Tweak: Max Natural Buildings
                listing.AddLabeledSlider($"- Meditation Psyfocus Gain Rate: {settings.tweak_animaMeditationGain.ToString("0.0")}", ref settings.tweak_animaMeditationGain, 0.1f, 20f, "Min: 0.1", "Max: 20", 0.1f);
                listing.Note("The amount of psyfocus a pawn gains per day of mediation.", GameFont.Tiny, Color.gray);
                listing.GapLine();

                TweaksGaloreStartup.Tweak_AnimaTweaks(settings);
            }
        }

        public static bool GetPsylinkStuff
        {
            get
            {
                if (settings.tweak_animaPsylinkLevelNeeds.NullOrEmpty())
                {
                    CompProperties_Psylinkable psycomp = ThingDefOf.Plant_TreeAnima.GetCompProperties<CompProperties_Psylinkable>();
                    settings.tweak_animaPsylinkLevelNeeds = psycomp.requiredSubplantCountPerPsylinkLevel;
                }

                return true;
            }
        }
    }
}
