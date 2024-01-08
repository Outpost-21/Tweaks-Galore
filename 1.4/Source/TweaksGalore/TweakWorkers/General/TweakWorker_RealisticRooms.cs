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
    public class TweakWorker_RealisticRooms : TweakWorker
    {
        public override void DoTweakContents(Listing_Standard listing, string filter = null)
        {
            base.DoTweakContents(listing, filter);
        }

        public override void OnStartup()
        {
            if (def.BoolValue)
            {
                List<RoomStatScoreStage> scoreStages = TGRoomStatDefOf.Space.scoreStages;
                foreach (RoomStatScoreStage stage in scoreStages)
                {
                    if (stage.label == "rather tight") { stage.minScore = 6f; }
                    if (stage.label == "average-sized") { stage.minScore = 14f; }
                    if (stage.label == "somewhat spacious") { stage.minScore = 28f; }
                    if (stage.label == "quite spacious") { stage.minScore = 45f; }
                    if (stage.label == "very spacious") { stage.minScore = 75f; }
                    if (stage.label == "extremely spacious") { stage.minScore = 175f; }
                }
            }
        }

        public override void OnWriteSettings()
        {
            OnStartup();
        }
    }
}
