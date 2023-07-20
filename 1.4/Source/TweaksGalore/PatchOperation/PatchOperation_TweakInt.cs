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
    public class PatchOperation_TweakInt : PatchOperationPathed
    {
        public string setting;

        public override bool ApplyWorker(XmlDocument xml)
        {
            if (HasRequiredSetting())
            {
                bool result = false;
                foreach (object item in xml.SelectNodes(xpath))
                {
                    XmlNode xmlNode = item as XmlNode;
                    xmlNode.InnerText = TweaksGaloreMod.settings.intSetting[setting].ToString();
                    result = true;
                }
                return result;
            }
            return true;
        }

        public bool HasRequiredSetting()
        {
            if (TweaksGaloreMod.settings.intSetting.NullOrEmpty())
            {
                return false;
            }
            if (!TweaksGaloreMod.settings.intSetting.ContainsKey(setting))
            {
                LogUtil.LogWarning($"PatchOperation_TweakInt attempted to search for a setting with the key '{setting}' which will always return null as it doesn't exist.");
                return false;
            }
            return true;
        }
    }
}
