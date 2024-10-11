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
    public class TweakWorker_TradeableMeals : TweakWorker
    {
        public override void OnStartup()
        {
            if (!def.ShouldRunTweak()) { return; }
            if (def.BoolValue)
            {
                LogUtil.LogMessage("Patching Meal Tradeability...");
                StringBuilder affectedMeals = new StringBuilder("Meals Patched:");
                List<ThingDef> allMeals = DefDatabase<ThingDef>.AllDefs.Where(td => td.thingCategories.NotNullAndContains(TGThingCategoryDefOf.FoodMeals)).ToList();
                for (int i = 0; i < allMeals.Count; i++)
                {
                    ThingDef curMeal = allMeals[i];
                    curMeal.tradeability = Tradeability.All;
                    if (curMeal.tradeTags.NullOrEmpty()) { curMeal.tradeTags = new List<string>(); }
                    if (!curMeal.tradeTags.Contains("TradeableMeals"))
                    {
                        curMeal.tradeTags.Add("TradeableMeals");
                        affectedMeals.AppendLine($"{curMeal.LabelCap} ({curMeal.defName})");
                    }
                }
                LogUtil.LogMessage(affectedMeals.ToString());

                StringBuilder tradersAffected = new StringBuilder("TraderKinds Patched:");
                List<TraderKindDef> traderKindDefs = DefDatabase<TraderKindDef>.AllDefsListForReading;
                for (int i = 0; i < traderKindDefs.Count; i++)
                {
                    TraderKindDef curTrader = traderKindDefs[i];
                    if (!curTrader.stockGenerators.NullOrEmpty())
                    {
                        if (!curTrader.stockGenerators.Any(sg => sg is StockGenerator_BuyTradeTag stock && stock.tag == "TradeableMeals"))
                        {
                            curTrader.stockGenerators.Add(new StockGenerator_BuyTradeTag() { tag = "TradeableMeals" });
                            tradersAffected.AppendLine($"{curTrader.LabelCap} ({curTrader.defName})");
                        }
                    }
                }
                LogUtil.LogMessage(tradersAffected.ToString());
            }
        }

        public override void OnWriteSettings()
        {
        }
    }
}
