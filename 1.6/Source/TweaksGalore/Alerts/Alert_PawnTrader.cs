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
    public class Alert_PawnTrader : Alert
	{
		public List<Building> TraderShips
		{
			get
			{
				List<Building> list = new List<Building>();
				try
				{
					List<Building> list2 = Find.CurrentMap?.listerBuildings?.allBuildingsNonColonist;
					if (list2 != null)
					{
						foreach (Building item in list2)
						{
							if (item.def.defName == "TraderShipsShip")
							{
								list.Add(item);
							}
						}
						return list;
					}
					return list;
				}
				catch
				{
					return list;
				}
			}
		}

		public IEnumerable<Pawn> TraderPawns
		{
			get
			{
				IEnumerable<Pawn> enumerable = PawnsFinder.AllMaps_Spawned.Where((Pawn p) => p.CanTradeNow);
				enumerable = enumerable.Where((Pawn pawn) => pawn.GuestStatus != GuestStatus.Guest);
				return enumerable.Where(delegate (Pawn pawn)
				{
					TraderKindDef traderKind = pawn.trader.traderKind;
					return traderKind != null && !traderKind.defName.ToLower().Contains("visitor");
				});
			}
		}

		public override string GetLabel()
		{
			return (TraderPawns.Count() + TraderShips.Count > 1) ? "TweaksGalore.PawnTraderMulti".Translate() : "TweaksGalore.PawnTraderSingle".Translate();
		}

		public override TaggedString GetExplanation()
		{
			List<TaggedString> list = new List<TaggedString>();
			foreach (Pawn traderPawn in TraderPawns)
			{
				list.Add(traderPawn.NameFullColored + ", " + traderPawn.Faction.NameColored);
			}
			foreach (Building traderShip in TraderShips)
			{
				list.Add("TweaksGalore.Shipfrom".Translate() + traderShip.Label + Environment.NewLine + traderShip.GetInspectString());
			}
			TaggedString taggedString = string.Join(Environment.NewLine + Environment.NewLine, list);
			return "TweaksGalore.PawnTraderDesc".Translate() + taggedString;
		}

		public override AlertReport GetReport()
		{
            if (!TGTweakDefOf.Tweak_TraderPawnAlert.BoolValue) { return false; }
			if (!TraderPawns.Any() && !TraderShips.Any())
			{
				return false;
			}
			List<Thing> list = new List<Thing>();
			foreach (Pawn traderPawn in TraderPawns)
			{
				list.Add(traderPawn);
			}
			foreach (Building traderShip in TraderShips)
			{
				list.Add(traderShip);
			}
			return AlertReport.CulpritsAre(list);
		}
	}
}
