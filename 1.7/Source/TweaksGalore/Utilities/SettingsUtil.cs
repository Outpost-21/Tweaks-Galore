using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using UnityEngine;
using RimWorld;
using Verse;
using Verse.Sound;

namespace TweaksGalore
{
	public static class SettingsUtil
	{
		public static void SettingsCategoryDropdown(this Listing_Standard listing, string name, string explanation, ref TweakCategoryDef def, float width)
		{
			float curHeight = listing.CurHeight;
			Rect rect = listing.GetRect(Text.LineHeight + listing.verticalSpacing);
			Text.Font = GameFont.Small;
			GUI.color = Color.white;
			TextAnchor anchor = Text.Anchor;
			Text.Anchor = TextAnchor.MiddleLeft;
			Widgets.Label(rect, name);
			Text.Anchor = TextAnchor.MiddleRight;

			Rect rect2 = new Rect(width - 150f, 0f, 150f, 29f);
			if (Widgets.ButtonText(rect2, def.LabelCap, true, true, true))
			{
				List<FloatMenuOption> list = new List<FloatMenuOption>();
				List<TweakCategoryDef> categories = DefDatabase<TweakCategoryDef>.AllDefsListForReading.Where(tcd => !tcd.heldSections.NullOrEmpty()).ToList();
				categories.SortBy(c => c.orderID);
				foreach (TweakCategoryDef category in categories)
				{
					if ((category.required.NullOrEmpty() || category.required.All(r => ModLister.GetActiveModWithIdentifier(r) != null)) && (category.incompatible.NullOrEmpty() || category.incompatible.All(i => ModLister.GetActiveModWithIdentifier(i) == null)))
					list.Add(new FloatMenuOption(category.LabelCap, delegate ()
					{
						TweaksGaloreMod.mod.currentCategory = category;
					}));
				}

				Find.WindowStack.Add(new FloatMenu(list));
			}

			Text.Anchor = anchor;

			Text.Font = GameFont.Tiny;
			listing.ColumnWidth -= 34f;
			GUI.color = Color.gray;
			listing.Label(explanation);
			listing.ColumnWidth += 34f;
			Text.Font = GameFont.Small;

			rect = listing.GetRect(0f);
			rect.height = listing.CurHeight - curHeight;
			rect.y -= rect.height;
			GUI.color = Color.white;
			listing.Gap(6f);
		}

		public static void StackableThingDropdown(this Listing_Standard listing, string name, string explanation, out ThingDef def, float width)
		{
			def = null;
			float curHeight = listing.CurHeight;
			Rect rect = listing.GetRect(Text.LineHeight + listing.verticalSpacing);
			Text.Font = GameFont.Small;
			GUI.color = Color.white;
			TextAnchor anchor = Text.Anchor;
			Text.Anchor = TextAnchor.MiddleLeft;
			Widgets.Label(rect, name);
			Text.Anchor = TextAnchor.MiddleRight;

			Rect rect2 = new Rect(width - 150f, 0f, 150f, 29f);
			ThingDef custom = null;
			if (Widgets.ButtonText(rect2, "Add Rule...", true, true, true))
			{
				List<FloatMenuOption> list = new List<FloatMenuOption>();
				List<ThingDef> things = DefDatabase<ThingDef>.AllDefs.Where(t => t.stackLimit > 1).ToList();
				things.SortBy(c => c.label);
				foreach (ThingDef thing in things)
				{
					if (thing.stackLimit > 1)
						list.Add(new FloatMenuOption(thing.LabelCap, delegate ()
						{
							custom = thing;
						}));
				}

				Find.WindowStack.Add(new FloatMenu(list));
			}
			def = custom;

			Text.Anchor = anchor;

			Text.Font = GameFont.Tiny;
			listing.ColumnWidth -= 34f;
			GUI.color = Color.gray;
			listing.Label(explanation);
			listing.ColumnWidth += 34f;
			Text.Font = GameFont.Small;

			rect = listing.GetRect(0f);
			rect.height = listing.CurHeight - curHeight;
			rect.y -= rect.height;
			GUI.color = Color.white;
			listing.Gap(6f);
		}
	}
}
