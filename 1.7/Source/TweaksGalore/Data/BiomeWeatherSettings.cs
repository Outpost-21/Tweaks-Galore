using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace TweaksGalore
{
    public class BiomeWeatherSettings : IExposable
    {
        public Dictionary<string, float> weatherCommonality = new Dictionary<string, float>();

        public void ExposeData()
        {
            Scribe_Collections.Look(ref weatherCommonality, "weatherCommonality");
        }
    }
}
