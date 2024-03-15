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
    public class TweakWorker_WaitThisIsBetter : TweakWorker
    {
        public override void OnStartup()
        {
            base.OnStartup();
            if (def.BoolValue)
            {
                foreach (ThingDef apparel in DefDatabase<ThingDef>.AllDefs.Where(d => d.IsApparel))
                {
                    if (apparel.apparel != null && !apparel.apparel.tags.NullOrEmpty())
                    {
                        string[] sTags = new string[] { "RoyalTier7", "RoyalTier6", "RoyalTier5", "RoyalTier4", "RoyalTier3", "RoyalTier2", "RoyalTier1" };
                        string sTagResult = sTags.FirstOrDefault(s => apparel.apparel.tags.Contains(s));
                        switch (sTagResult)
                        {
                            case "RoyalTier7":
                                AddTagsToRoyalApparel(apparel, sTags, 1);
                                break;
                            case "RoyalTier6":
                                AddTagsToRoyalApparel(apparel, sTags, 2);
                                break;
                            case "RoyalTier5":
                                AddTagsToRoyalApparel(apparel, sTags, 3);
                                break;
                            case "RoyalTier4":
                                AddTagsToRoyalApparel(apparel, sTags, 4);
                                break;
                            case "RoyalTier3":
                                AddTagsToRoyalApparel(apparel, sTags, 5);
                                break;
                            case "RoyalTier2":
                                AddTagsToRoyalApparel(apparel, sTags, 6);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public static void AddTagsToRoyalApparel(ThingDef apparel, string[] tags, int start)
        {
            for (int i = start; i < tags.Length; i++)
            {
                apparel.apparel.tags.Add(tags[i]);
            }
        }
    }
}
