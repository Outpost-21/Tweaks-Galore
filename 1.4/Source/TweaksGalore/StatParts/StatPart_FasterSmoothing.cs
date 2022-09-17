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
			val *= TweaksGaloreMod.settings.tweak_fasterSmoothing_factor;
		}

		public override string ExplanationPart(StatRequest req)
		{
			return "Faster smoothing bonus" + ": x" + TweaksGaloreMod.settings.tweak_fasterSmoothing_factor.ToStringPercent();
		}
	}
}
