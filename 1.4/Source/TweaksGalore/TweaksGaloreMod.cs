using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using UnityEngine;
using RimWorld;
using Verse;

using HarmonyLib;

namespace TweaksGalore
{
    public class TweaksGaloreMod : Mod
    {
        public static TweaksGaloreSettings settings;
        public static TweaksGaloreMod mod;

        public TweakCategoryDef currentCategory;
        public QuickSearchWidget quickSearchWidget = new QuickSearchWidget();
        public string tweakFilter = "";
        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;
        public Dictionary<string, TrainabilityDef> TrainabilityRadioDict = new Dictionary<string, TrainabilityDef>();

        public Color headerColor = new Color(1.0f, 1.0f, 1.0f);
        public Color subHeaderColor = new Color(0.9f, 0.9f, 0.9f);
        public Color settingColor = new Color(0.8f, 0.8f, 0.8f);

        public Dictionary<string, bool> collapsedCategories = new Dictionary<string, bool>();

        public bool GetCollapsedCategoryState(string category)
        {
            if (!collapsedCategories.ContainsKey(category))
            {
                collapsedCategories.Add(category, true);
            }
            return collapsedCategories[category];
        }

        public void SetCollapsedCategoryState(string category, bool value)
        {
            collapsedCategories[category] = value;
        }

        public Dictionary<string, float> subStandardHeights = new Dictionary<string, float>();

        public float GetSubStandardHeight(string section)
        {
            if (!subStandardHeights.ContainsKey(section))
            {
                subStandardHeights.Add(section, float.MaxValue);
            }
            return subStandardHeights[section];
        }

        public void SetSubStandardHeight(string section, float value)
        {
            subStandardHeights[section] = value;
        }

        internal static string VersionDir => Path.Combine(mod.Content.ModMetaData.RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public TweaksGaloreMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<TweaksGaloreSettings>();
            mod = this;
            try
            {
                Version version = Assembly.GetExecutingAssembly().GetName().Version;
                CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

                LogUtil.LogMessage($"{CurrentVersion} ::");

                if (Prefs.DevMode && VersionDir != null)
                {
                    File.WriteAllText(VersionDir, CurrentVersion);
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogMessage("Suppressed Error: " + ex);
            }

            new Harmony("Neronix17.TweaksGalore.RimWorld").PatchAll();
        }

        public override string SettingsCategory() => "Tweaks Galore";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            bool flag = optionsViewRectHeight > inRect.height;
            Rect viewRect = new Rect(inRect.x, inRect.y, inRect.width - (flag ? 26f : 0f), optionsViewRectHeight);
            Widgets.BeginScrollView(inRect, ref optionsScrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            Rect rect = new Rect(viewRect.x, viewRect.y, viewRect.width, 999999f);
            listing.Begin(rect);
            // ============================ CONTENTS ================================
            DoOptionsCategoryContents(listing);
            // ======================================================================
            optionsViewRectHeight = listing.CurHeight;
            listing.End();
            Widgets.EndScrollView();
        }

        public void DoOptionsCategoryContents(Listing_Standard listing)
        {
            if(currentCategory == null) { currentCategory = TGTweakDefOf.TweakCategory_Vanilla; }
            listing.SettingsCategoryDropdown("Current Page", "Setting descriptions are in tooltips. The Searchbar only works for the currently selected page.\nYou will need to restart the game for many of these settings to take effect.", ref currentCategory, listing.ColumnWidth);
            Rect rect = listing.GetRect(30f);
            quickSearchWidget.OnGUI(rect);
            tweakFilter = quickSearchWidget.filter.Text;
            listing.GapLine();
            currentCategory.DoCategoryContents(listing, settings, tweakFilter);
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
            foreach (TweakDef tweak in DefDatabase<TweakDef>.AllDefs)
            {
                try
                {
                    tweak.Worker.OnWriteSettings();
                }
                catch (Exception e)
                {
                    LogUtil.LogError($"Caught Exception applying '{tweak.defName}' tweak:\n" + e);
                }
            }
        }
    }
}
