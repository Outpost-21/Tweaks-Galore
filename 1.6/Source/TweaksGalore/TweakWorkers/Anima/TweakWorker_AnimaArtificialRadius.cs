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
    public class TweakWorker_AnimaArtificialRadius : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (TGTweakDefOf.Tweak_AnimaTweaks.BoolValue)
            {
                CompProperties_MeditationFocus focus = ThingDefOf.Plant_TreeAnima.GetCompProperties<CompProperties_MeditationFocus>();

                FocusStrengthOffset_ArtificialBuildings artificialOffset = (FocusStrengthOffset_ArtificialBuildings)focus.offsets.Find(os => os.GetType() == typeof(FocusStrengthOffset_ArtificialBuildings));

                artificialOffset.radius = def.FloatValue;

                StatPart_ArtificialBuildingsNearbyOffset nearbyBuildingOffset = (StatPart_ArtificialBuildingsNearbyOffset)StatDefOf.MeditationPlantGrowthOffset.parts.Find(sp => sp.GetType() == typeof(StatPart_ArtificialBuildingsNearbyOffset));
                nearbyBuildingOffset.radius = def.FloatValue;
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
