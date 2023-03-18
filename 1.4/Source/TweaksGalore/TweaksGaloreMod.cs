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

        public TweaksGaloreSettingsPage currentPage = TweaksGaloreSettingsPage.General;
        public Vector2 optionsScrollPosition;
        public float optionsViewRectHeight;
        public Dictionary<string, TrainabilityDef> TrainabilityRadioDict = new Dictionary<string, TrainabilityDef>();

        public bool restoreGeneral = false;
        public bool restoreMechanoids = false;
        public bool restorePennedAnimals = false;
        public bool restorePower = false;
        public bool restoreRaids = false;
        public bool restoreResources = false;
        public bool restoreRoyalty = false;
        public bool restoreAnima = false;
        public bool restoreRoyalTitles = false;
        public bool restoreRoyalPermits = false;
        public bool restoreIdeology = false;
        public bool restoreGauranlen = false;
        public bool restoreBiotech = false;
        public bool restorePolux = false;

        internal static string VersionDir => Path.Combine(ModLister.GetActiveModWithIdentifier("Neronix17.TweaksGalore", true).RootDir.FullName, "Version.txt");
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

            new Harmony("neronix17.tweaksgalore.rimworld").PatchAll();
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
            listing.SettingsDropdown("Current Page", "", ref currentPage, listing.ColumnWidth);
            listing.Note("You will need to restart the game for most of these settings to take effect.", GameFont.Tiny);
            listing.GapLine();
            if (currentPage == TweaksGaloreSettingsPage.General)
            {
                if (restoreGeneral) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { SettingsPage_General.DoSettings_Vanilla(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Mechanoids)
            {
                if (restoreMechanoids) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { SettingsPage_Mechanoids.DoSettings_Mechanoids(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Penned_Animals)
            {
                if (restorePennedAnimals) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { SettingsPage_PennedAnimals.DoSettings_PennedAnimals(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Power)
            {
                if (restorePower) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { SettingsPage_Power.DoSettings_Power(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Raids)
            {
                if (restoreRaids) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { SettingsPage_Raids.DoSettings_Raids(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Resources)
            {
                if (restoreResources) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { SettingsPage_Resources.DoSettings_Resources(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royalty)
            {
                if (restoreRoyalty) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.RoyaltyInstalled) { listing.Note("Royalty is not installed. These options would do nothing for you."); }
                    else { SettingsPage_Royalty.DoSettings_Royalty(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Anima)
            {
                if (restoreAnima) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.RoyaltyInstalled) { listing.Note("Royalty is not installed. These options would do nothing for you."); }
                    else { SettingsPage_Anima.DoSettings_Anima(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royal_Titles)
            {
                if (restoreRoyalTitles) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.RoyaltyInstalled) { listing.Note("Royalty is not installed. These options would do nothing for you."); }
                    else { SettingsPage_RoyaltyTitles.DoSettings_RoyaltyTitles(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royal_Permits)
            {
                if (restoreRoyalTitles) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.RoyaltyInstalled) { listing.Note("Royalty is not installed. These options would do nothing for you."); }
                    else { SettingsPage_RoyaltyPermits.DoSettings_RoyaltyPermits(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Ideology)
            {
                if (restoreIdeology) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.IdeologyInstalled) { listing.Note("Ideology is not installed. These options would do nothing for you."); }
                    else { SettingsPage_Ideology.DoSettings_Ideology(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Gauranlen)
            {
                if (restoreGauranlen) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.IdeologyInstalled) { listing.Note("Ideology is not installed. These options would do nothing for you."); }
                    else { SettingsPage_Gauranlen.DoSettings_Gauranlen(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Biotech)
            {
                if (restoreBiotech) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.BiotechInstalled) { listing.Note("Biotech is not installed. These options would do nothing for you."); }
                    else { SettingsPage_Biotech.DoSettings_Biotech(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Genepacks)
            {
                if (!ModLister.BiotechInstalled) { listing.Note("Biotech is not installed. These options would do nothing for you."); }
                else { SettingsPage_GenePacks.DoSettings_Genepacks(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Polux)
            {
                if (restorePolux) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.BiotechInstalled) { listing.Note("Biotech is not installed. These options would do nothing for you."); }
                    else { SettingsPage_Polux.DoSettings_Polux(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Defaults)
            {
                SettingsPage_Defaults.DoSettings_Defaults(listing);
            }
        }

        public override void WriteSettings()
        {
            base.WriteSettings();
        }
    }

    public enum TweaksGaloreSettingsPage
    {
        General,
        Mechanoids,
        Penned_Animals,
        Power,
        Raids,
        Resources,
        Royalty,
        Royal_Titles,
        Royal_Permits,
        Anima,
        Ideology,
        Gauranlen,
        Biotech,
        Genepacks,
        Polux,
        Defaults
    }
}
