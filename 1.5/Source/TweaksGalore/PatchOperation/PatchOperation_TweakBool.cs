using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using UnityEngine;
using RimWorld;
using Verse;

namespace TweaksGalore
{
    public class PatchOperation_TweakBool : PatchOperation
    {
        public List<string> settings;

        public bool allNeeded = false;

        public PatchOperation active;

        public PatchOperation inactive;

        public override bool ApplyWorker(XmlDocument xml)
        {
            if (HasRequiredSettings())
            {
                if (active != null)
                {
                    return active.Apply(xml);
                }
            }
            else if (inactive != null)
            {
                return inactive.Apply(xml);
            }
            return true;
        }

        public bool HasRequiredSettings()
        {
            if (TweaksGaloreMod.settings.boolSetting.NullOrEmpty())
            {
                return false;
            }
            if (allNeeded)
            {
                return HasAllSettings();
            }
            else
            {
                return HasAnySetting();
            }
        }

        public bool HasAnySetting()
        {
            for (int i = 0; i < settings.Count(); i++)
            {
                return IsSettingEnabled(settings[i]);
            }
            return false;
        }

        public bool HasAllSettings()
        {
            int num = 0;
            for (int i = 0; i < settings.Count; i++)
            {
                if (IsSettingEnabled(settings[i]))
                {
                    num++;
                }
            }
            if(num >= settings.Count)
            {
                return true;
            }
            return false;
        }

        public bool IsSettingEnabled(string defName)
        {
            TweakDef tweakDef = DefDatabase<TweakDef>.GetNamedSilentFail(defName);
            if (tweakDef == null) { return false; }
            if (!tweakDef.BoolValue) { return false; }
            if (tweakDef.incompatible.Any(r => ModLister.GetActiveModWithIdentifier(r) != null)) { return false; }
            return false;
        }
    }
}
