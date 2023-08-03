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
    public class TweakDef : Def
    {
        public TweakType tweakType;

        public StatDef tweakStat;

        public string defaultValue;

        public FloatRange floatRange;

        public float? floatIncrement;

        public IntRange intRange;

        public int? intIncrement;

        public ThingDef tweakThing;

        public List<ThingDef> tweakThings;

        public bool invertUsage = false;

        public TweakFormatting formatting = new TweakFormatting();

        public List<string> required = new List<string>();

        public List<TweakDef> requiredTweaks = new List<TweakDef>();

        public List<string> incompatible = new List<string>();

        public Type tweakWorker = typeof(TweakWorker);

        public TweakWorker workerInt;

        public TweakWorker Worker
        {
            get
            {
                if (workerInt == null)
                {
                    workerInt = (TweakWorker)Activator.CreateInstance(tweakWorker);
                    workerInt.def = this;
                }
                return workerInt;
            }
        }

        public bool DefaultBool
        {
            get
            {
                if (tweakType != TweakType.Bool)
                {
                    return true;
                }
                return bool.Parse(defaultValue);
            }
        }

        public bool BoolValue
        {
            get
            {
                if(tweakType != TweakType.Bool)
                {
                    return true;
                }
                return TweaksGaloreMod.settings.GetBoolSetting(defName, DefaultBool);
            }
        }

        public int DefaultInt
        {
            get
            {
                if (tweakType != TweakType.Int)
                {
                    return -1;
                }
                return int.Parse(defaultValue);
            }
        }

        public int IntValue
        {
            get
            {
                if (tweakType != TweakType.Int)
                {
                    return -1;
                }
                return TweaksGaloreMod.settings.GetIntSetting(defName, DefaultInt);
            }
        }

        public IntRange DefaultIntRange
        {
            get
            {
                if (tweakType != TweakType.IntRange)
                {
                    return new IntRange(-1, -1);
                }
                IntRange result;
                int[] a = defaultValue.Split('~').Select(s => int.Parse(s)).ToArray();
                result = new IntRange(a[0], a[1]);
                return result;
            }
        }

        public IntRange IntRangeValue
        {
            get
            {
                if (tweakType != TweakType.FloatRange)
                {
                    return new IntRange(-1, -1);
                }
                return TweaksGaloreMod.settings.GetIntRangeSetting(defName, DefaultIntRange);
            }
        }

        public float DefaultFloat
        {
            get
            {
                if (tweakType != TweakType.Float)
                {
                    return -1f;
                }
                return float.Parse(defaultValue);
            }
        }

        public float FloatValue
        {
            get
            {
                if (tweakType != TweakType.Float)
                {
                    return -1f;
                }
                return TweaksGaloreMod.settings.GetFloatSetting(defName, DefaultFloat);
            }
        }

        public FloatRange DefaultFloatRange
        {
            get
            {
                if(tweakType != TweakType.FloatRange)
                {
                    return new FloatRange(-1f, -1f);
                }
                FloatRange result;
                float[] a = defaultValue.Split('~').Select(s => float.Parse(s)).ToArray();
                result = new FloatRange(a[0], a[1]);
                return result;
            }
        }

        public FloatRange FloatRangeValue
        {
            get
            {
                if(tweakType != TweakType.FloatRange)
                {
                    return new FloatRange(-1f, -1f);
                }
                return TweaksGaloreMod.settings.GetFloatRangeSetting(defName, DefaultFloatRange);
            }
        }
    }

    public class TweakFormatting
    {
        public bool gapLine = false;
        public bool enhanced = false;
        public bool percentage = false;
    }
}
