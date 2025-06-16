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
    public class StatPart_FasterSmoothing : StatPart
	{
		public override void TransformValue(StatRequest req, ref float val)
		{
			val *= TGTweakDefOf.Tweak_FasterSmoothingFactor.FloatValue;
		}

		public override string ExplanationPart(StatRequest req)
		{
			return "Faster smoothing bonus" + ": x" + TGTweakDefOf.Tweak_FasterSmoothingFactor.FloatValue.ToStringPercent();
		}
	}
}
