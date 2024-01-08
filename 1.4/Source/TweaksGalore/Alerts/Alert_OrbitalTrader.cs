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
    public class Alert_OrbitalTrader : Alert
	{
		public override string GetLabel()
		{
			foreach (Map map in Find.Maps)
			{
				if (map.passingShipManager.passingShips.Count > 1)
				{
					return "TweaksGalore.OrbitalTraderMulti".Translate();
				}
			}
			return "TweaksGalore.OrbitalTraderSingle".Translate();
		}

		public override TaggedString GetExplanation()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Map map in Find.Maps)
			{
				foreach (PassingShip passingShip in map.passingShipManager.passingShips)
				{
					stringBuilder.AppendLine(passingShip.FullTitle);
					stringBuilder.AppendLine("Leaves in " + passingShip.ticksUntilDeparture.ToStringTicksToPeriod());
				}
			}
			return string.Format("TweaksGalore.OrbitalTraderDesc".Translate(), stringBuilder);
		}

		public override AlertReport GetReport()
		{
			if(!TGTweakDefOf.Tweak_OrbitalTraderAlert.BoolValue)
			foreach (Map map in Find.Maps)
			{
				if (map.passingShipManager.passingShips.Count > 0)
				{
					Building_CommsConsole building_CommsConsole = map.listerBuildings.AllBuildingsColonistOfClass<Building_CommsConsole>().FirstOrDefault();
					if (building_CommsConsole != null)
					{
						return AlertReport.CulpritIs(building_CommsConsole);
					}
				}
			}
			return false;
		}
	}
}
