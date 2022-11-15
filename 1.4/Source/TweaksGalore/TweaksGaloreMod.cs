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
        public List<ThingDef> cachedAnimalListing = new List<ThingDef>();

        public bool restoreGeneral = false;
        public bool restoreMechanoids = false;
        public bool restorePennedAnimals = false;
        public bool restorePower = false;
        public bool restoreRaids = false;
        public bool restoreResources = false;
        public bool restoreRoyalty = false;
        public bool restoreAnima = false;
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
                else { DoSettings_Vanilla(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Mechanoids)
            {
                if (restoreMechanoids) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { DoSettings_Mechanoids(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Penned_Animals)
            {
                if (restorePennedAnimals) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { DoSettings_PennedAnimals(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Power)
            {
                if (restorePower) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { DoSettings_Power(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Raids)
            {
                if (restoreRaids) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { DoSettings_Raids(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Resources)
            {
                if (restoreResources) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else { DoSettings_Resources(listing); }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royalty)
            {
                if (restoreRoyalty) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.RoyaltyInstalled) { listing.Note("Royalty is not installed. These options would do nothing for you."); }
                    else { DoSettings_Royalty(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Anima)
            {
                if (restoreAnima) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.RoyaltyInstalled) { listing.Note("Royalty is not installed. These options would do nothing for you."); }
                    else { DoSettings_Anima(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Ideology)
            {
                if (restoreIdeology) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.IdeologyInstalled) { listing.Note("Ideology is not installed. These options would do nothing for you."); }
                    else { DoSettings_Ideology(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Gauranlen)
            {
                if (restoreGauranlen) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.IdeologyInstalled) { listing.Note("Ideology is not installed. These options would do nothing for you."); }
                    else { DoSettings_Gauranlen(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Biotech)
            {
                if (restoreBiotech) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.BiotechInstalled) { listing.Note("Biotech is not installed. These options would do nothing for you."); }
                    else { DoSettings_Biotech(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Polux)
            {
                if (restorePolux) { listing.Note("You've marked this category for restoring to defaults! Relaunch the game to complete the process!"); }
                else
                {
                    if (!ModLister.BiotechInstalled) { listing.Note("Biotech is not installed. These options would do nothing for you."); }
                    else { DoSettings_Polux(listing); }
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Defaults)
            {
                DoSettings_Defaults(listing);
            }
        }

        public void DoSettings_Vanilla(Listing_Standard listing)
        {
            // Tweak: Boomalopes Bleed Chemfuel
            listing.CheckboxEnhanced("Boomalopes Bleed Chemfuel", "Changes the blood def from boomalopes and boomrats to be chemfuel puddles instead.", ref settings.tweak_boomalopesBleedChemfuel);
            listing.GapLine();
            // Tweak: Chatty Comms
            listing.CheckboxEnhanced("Chatty Comms", "Adds a recreation type to comms consoles allowing them to be used by pawns to gain recreation through 'chatting'.", ref settings.tweak_chattyComms);
            listing.GapLine();
            // Tweak: Destroy Anything
            listing.CheckboxEnhanced("Destroy Anything", "Allows the smelting of pretty much anything anything in the crematorium aside from weapons, apparel and minified buildings. Be careful so you don't burn anything important.", ref settings.tweak_destroyAnything);
            listing.GapLine();
            // Tweak: Don't Pack Food
            listing.CheckboxEnhanced("Don't Pack Food", "Prevents pawns from stuffing food into their inventory to carry around.", ref settings.tweak_dontPackFood);
            listing.GapLine();
            // Tweak: Faster Smoothing
            listing.CheckboxEnhanced("Faster Smoothing", "Enables an additional stat to speed up smoothing of natural rock. Default: 300%", ref settings.tweak_fasterSmoothing);
            if (settings.tweak_fasterSmoothing)
            {
                listing.AddLabeledSlider("- Faster Smoothing Factor: " + settings.tweak_fasterSmoothing_factor.ToStringPercent(), ref settings.tweak_fasterSmoothing_factor, 0.1f, 10.0f, "Min: 10%", "Max: 1000%", 0.1f);
            }
            listing.GapLine();
            // Tweak: Full Deconstruction Return
            listing.CheckboxEnhanced("Full Deconstruct Return", "Returns the full amount it takes to build something. No more punishment for moving a building that isn't uninstallable.", ref settings.tweak_fixDeconstructionReturn);
            listing.GapLine();
            // Tweak: Glowing Ambrosia
            listing.CheckboxEnhanced("Glowing Ambrosia", "Gives Ambrosia a slight glow around it, making natural spawning Ambrosia easier to find on a map in the dark.", ref settings.tweak_glowingAmbrosia);
            listing.GapLine();
            // Tweak: Glowing Healroot
            string healrootName = (settings.tweak_healrootToXerigium ? "Xerigium" : "Healroot");
            listing.CheckboxEnhanced("Glowing " + healrootName, "Gives " + healrootName + " a slight glow around it, making natural " + healrootName + " easier to find on a map in the dark.", ref settings.tweak_glowingHealroot);
            listing.GapLine();
            // Tweak: Healroot to Xerigium
            listing.CheckboxEnhanced("Healroot to Xerigium", "Reverts the old name change of the herbal medicine plant Healroot back to Xerigium like it used to be in older game versions.", ref settings.tweak_healrootToXerigium);
            listing.GapLine();
            // Tweak: Hunters Can Use Melee
            listing.CheckboxEnhanced("Hunters Can Use Melee", "Allows hunters to use melee, regardless of how well they can do so.", ref settings.tweak_hunterMelee);
            if (settings.tweak_hunterMelee)
            {
                listing.CheckboxLabeled("- Allow Fists", ref settings.tweak_hunterMelee_fistFighting, "Allows hunters to use their fists to hunt, stupid but it works.");
                //if (Patches_HunterMelee.SimpleSidearmsLoaded)
                //{
                //    listingStandard.CheckboxLabeled("Enable Simple Sidearms", ref settings.tweak_hunterMelee_allowSimpleSidearms, "Enable Simple Sidearms Compatibility, allowing pawns to consider sidearms for hunting.");
                //}
            }
            listing.GapLine();
            // Tweak: Incident Pawn Stats
            listing.CheckboxEnhanced("Incident Pawn Stats", "Displays the information of any pawns rewarded as part of incidents.", ref settings.patch_incidentPawnStats);
            listing.GapLine();
            // Tweak: Infestation Blocking Floors
            listing.CheckboxEnhanced("Infestation Blocking Floors", "Prevents infestations happening on harder floors (anything crafted from metal or stone materials). Only prevents the event picking that spot, hives can still spawn on them if an event happens closeby.", ref settings.patch_strongFloorsStopInfestations);
            listing.GapLine();
            // Tweak: Insulting Spree Nerf
            listing.CheckboxEnhanced("Insulting Spree Nerf", "Makes the Insulting Spree mental break less annoying to deal with." +
                "\n- Intensity: Minor --> Major" +
                "\n- Max Duration: 45,000 ticks --> 20,000 ticks" +
                "\n- Min Duration: 25,000 ticks --> 2,500 ticks" +
                "\n- Base Event Chance: 0.5 --> 0.15", ref settings.tweak_insultingSpreeNerf);
            listing.GapLine();
            // Tweak: Impassable Deep Water
            listing.CheckboxEnhanced("Impassable Deep Water", "Changes deep water to be impassable by pawns, preventing them from pathing through it.", ref settings.tweak_impassableDeepWater);
            listing.GapLine();
            // Tweak: Lag free Lamps
            listing.CheckboxEnhanced("Lag Free Lamps", "Removes the fuel comp from fuelled lamps, so they now no longer run code constantly they impact performance that little bit less.", ref settings.tweak_lagFreeLamps);
            listing.GapLine();
            // Tweak: Megasloth to Megatherium
            listing.CheckboxEnhanced("Megasloth to Megatherium", "Reverts the name change of the Megatherium so it retains the obviously cooler name.", ref settings.tweak_oldMegaslothName);
            listing.GapLine();
            // Tweak: Misanthrope Trait
            listing.CheckboxEnhanced("Misanthrope Trait", "Adds the misanthrope trait (dislikes humans), also makes it so if a pawn was going to spawn with both the misandrist and misogynist traits that they instead get the misanthrope trait.", ref settings.tweak_misanthropeTrait);
            listing.GapLine();
            // Tweak: Next Restock Timer
            listing.CheckboxEnhanced("Next Restock Timer", "Displays in the world map information of settlements whether or not they have restocked since you last visited and how long till their next restock.", ref settings.patch_settlementTraderTimer);
            listing.GapLine();
            // Tweak: No Breakdowns
            if (ModLister.GetActiveModWithIdentifier("kentington.saveourship2") == null)
            {
                listing.CheckboxEnhanced("No Breakdowns", "Removes the breakdown comp from anything that has it, artificially enforced resource sinks are LAZY.", ref settings.tweak_noBreakdowns);
                listing.GapLine();
            }
            // Tweak: No Friend Shaped Manhunters
            listing.CheckboxEnhanced("No Friend Shaped Manhunters", "Sets parameters which decide if an animal can arrive as manhunter or not. This does not prevent it happening if they take damage.", ref settings.tweak_noFriendShapedManhunters);
            if (settings.tweak_noFriendShapedManhunters)
            {
                listing.AddLabelLine("- Prevent by Trainability:");
                listing.CheckboxLabeled("-- Intermediate", ref settings.tweak_NFSMTrainability_Intermediate);
                listing.CheckboxLabeled("-- Advanced", ref settings.tweak_NFSMTrainability_Advanced);
                listing.CheckboxLabeled("- Prevent if Nuzzle-able", ref settings.tweak_NFSMNuzzleHours);
                listing.AddLabeledSlider("- Prevent if Wildness Below: " + settings.tweak_NFSMWildness.ToStringPercent(), ref settings.tweak_NFSMWildness, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                listing.AddLabeledSlider("- Prevent if Combat Power Below: " + settings.tweak_NFSMCombatPower, ref settings.tweak_NFSMCombatPower, 0f, 200f, "Min: 0", "Max: 200", 1f);
                listing.CheckboxLabeled("- Disable Manhunter on Tame Fail", ref settings.tweak_NFSMDisableManhunterOnTame, "Uses the same parameters to prevent animals going manhunter on tame fail too, they can still go manhunter if damaged.");
            }
            listing.GapLine();
            // Tweak: Not So Wild Berries
            listing.CheckboxEnhanced("Not So Wild Berries", "Adds the ground sowing tag to wild berries so they can be planted.", ref settings.tweak_notSoWildBerries);
            listing.GapLine();
            // Tweak: Prisoners Don't Have Keys
            listing.CheckboxEnhanced("Prisoners Don't Have Keys", "Selectively controls whether prisoners and slaves can open doors during breakout and rebellion.", ref settings.patch_prisonersDontHaveKeys);
            if (settings.patch_prisonersDontHaveKeys)
            {
                listing.CheckboxLabeled("- Applies to Prisoners", ref settings.patch_pdhk_prisoners);
                listing.CheckboxLabeled("- Applies to Slaves", ref settings.patch_pdhk_slaves);
                listing.CheckboxLabeled("- Escaping pawns can open their own door", ref settings.patch_pdhk_ownDoor);
            }
            listing.GapLine();
            // Tweak: Skilled Stonecutting
            listing.CheckboxEnhanced("Skilled Stonecutting", "Makes stonecutting give crafting skill increases, and makes more skilled crafters create blocks quicker.", ref settings.tweak_skilledStonecutting);
            listing.GapLine();
            // Tweak: Skill Rates
            listing.CheckboxEnhanced("Skill Rates Adjustment", "Enables more control over skill gain/loss rates and what levels that should happen at.", ref settings.tweak_skillRates);
            if (settings.tweak_skillRates)
            {
                listing.AddLabeledSlider("- Skill Loss Multiplier: " + settings.tweak_skillRateLoss.ToStringPercent(), ref settings.tweak_skillRateLoss, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listing.AddLabeledSlider("- Skill Gain Multiplier: " + settings.tweak_skillRateGain.ToStringPercent(), ref settings.tweak_skillRateGain, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listing.AddLabeledSlider("- Skill Loss Threshold: " + settings.tweak_skillRateLossThreshold.ToString(), ref settings.tweak_skillRateLossThreshold, 0f, 20f, "Min: 0", "Max: 20", 1f);
                listing.Note("Skill loss threshold is the level a skill has to reach before skill loss starts happening, it will prevent skill loss entirely below that level.", GameFont.Tiny);
            }
            listing.GapLine();
            // Tweak: Slim Rim
            listing.CheckboxEnhanced("Slim Rim", "Allows the disabling of body types on pawn generation. Does this during generation of the pawn so will not affect existing ones. The replacement body will be either Male or Female, whichever matches the pawns gender.", ref settings.patch_slimRim);
            if (settings.patch_slimRim)
            {
                listing.CheckboxLabeled("- Fat", ref settings.patch_slimRim_fat);
                listing.CheckboxLabeled("- Hulk", ref settings.patch_slimRim_hulk);
                listing.CheckboxLabeled("- Thin", ref settings.patch_slimRim_thin);
            }
            listing.GapLine();
            // Tweak: Trait Count Adjustment
            listing.CheckboxEnhanced("Trait Count Adjustment", "There may be cases where this cannot apply, such as modded alien races where they have been set up to have a specific trait count.", ref settings.tweak_traitCountAdjustment);
            if (settings.tweak_traitCountAdjustment)
            {
                listing.Label("- Trait Count Range");
                listing.Note($"Current: {settings.tweak_traitCountRange.min}-{settings.tweak_traitCountRange.max}  Min: 1  Max: 8", GameFont.Tiny);
                listing.IntRange(ref settings.tweak_traitCountRange, 1, 8);
            }
            listing.GapLine();
        }

        public void DoSettings_Mechanoids(Listing_Standard listing)
        {
            // Tweak: Disable Adapting
            listing.CheckboxEnhanced("Disable Adapting", "Disables the ability for Mechanoids to adapt to EMP.", ref settings.patch_disableMechanoidAdapting);
            // Tweak: Heat Armor
            listing.Label("Mechanoid Heat Armour: " + settings.tweak_mechanoidHeatArmour.ToStringPercent());
            listing.AddLabeledSlider(null, ref settings.tweak_mechanoidHeatArmour, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
            // Tweak: Pre-1.0 Ship Parts
            listing.CheckboxEnhanced("Pre-1.0 Ship Parts", "Returns psychic and defoliator ship parts to their B18 state where they actually provide a decent reward." +
                "\nPsychic Ship Part Leavings:" +
                "\n- Steel x100" +
                "\n- Plasteel x35" +
                "\n- Steel Slag x8" +
                "\n- Industrial Components x4" +
                "\n- Spacer Components x1" +
                "\n- AI Persona Core" +
                "\n\nDefoliator Ship Part Leavings:" +
                "\n- Steel x100" +
                "\n- Plasteel x35" +
                "\n- Steel Slag x8" +
                "\n- Industrial Component x4" +
                "\n- Spacer Component x1" +
                "\n- Glittertech Medicine x5", ref settings.tweak_preReleaseShipParts);
            if (ModLister.RoyaltyInstalled)
            {
                listing.CheckboxEnhanced("Better Gloomlight", "Changes the gloomlight often found in mech clusters to produce more light so it's actually useful.", ref settings.tweak_betterGloomlight);
                listing.CheckboxEnhanced("- Sunlamp Gloomlight", "With Better Gloomlight and this enabled, they'll instead produce enough light to be equal to that of a free sunlamp.", ref settings.tweak_gloomlightSunlamp);
                listing.CheckboxEnhanced("- Darklight Gloomlight", "Changes the colour of the gloomlight to match that of 'Darklight'.", ref settings.tweak_gloomlightDarklight);
            }
        }

        public void DoSettings_PennedAnimals(Listing_Standard listing)
        {
            // Tweak: Penned Animal Config
            listing.CheckboxEnhanced("Penned Animal Config", "Allows control over which animals can be penned and how many days it takes for them to begin roaming if they are not in a pen.", ref settings.tweak_pennedAnimalConfig);
            if (settings.tweak_pennedAnimalConfig)
            {
                listing.GapLine();
                for (int i = 0; i < CachedAnimalListing.Count; i++)
                {
                    ThingDef curAnimal = CachedAnimalListing[i];
                    float value = settings.tweak_pennedAnimalDict[curAnimal.defName];
                    listing.AddLabeledSlider(curAnimal.LabelCap + ": " + (value == 0f ? "Not Pennable" : (value + " Days")), ref value, 0f, 20f, "Disabled", "20 Days");
                    settings.tweak_pennedAnimalDict[curAnimal.defName] = value;
                }

                TweaksGaloreStartup.SetPennedAnimals(settings);
            }
            listing.GapLine();
        }

        public List<ThingDef> CachedAnimalListing
        {
            get
            {
                if (cachedAnimalListing.NullOrEmpty())
                {
                    cachedAnimalListing = new List<ThingDef>();
                    List<string> startList = (from x in settings.tweak_pennedAnimalDict.Keys.ToList() orderby x descending select x).ToList();
                    foreach (string name in startList)
                    {
                        ThingDef animal = DefDatabase<ThingDef>.GetNamedSilentFail(name);
                        if(animal != null)
                        {
                            cachedAnimalListing.Add(animal);
                        }
                    }
                }
                LogUtil.LogWarning("5");
                return cachedAnimalListing;
            }
        }

        public void DoSettings_Power(Listing_Standard listing)
        {
            // Tweak: Power Tweaks
            listing.CheckboxEnhanced("Power Usage Tweaks", "Allows tweaking the usage rate of some buildings. If they usually produce then it configures how much they output instead.", ref settings.tweak_powerUsageTweaks);
            if (settings.tweak_powerUsageTweaks)
            {
                listing.AddLabeledSlider("- Standing Lamp: " + settings.tweak_powerUsage_lamp, ref settings.tweak_powerUsage_lamp, 0f, 100f, "Min: 0", "Max: 100", 1f);
                listing.AddLabeledSlider("- Sun Lamp: " + settings.tweak_powerUsage_sunlamp, ref settings.tweak_powerUsage_sunlamp, 0f, 10000f, "Min: 0", "Max: 10000", 50f);
                listing.AddLabeledSlider("- Auto-Door: " + settings.tweak_powerUsage_autodoor, ref settings.tweak_powerUsage_autodoor, 0f, 100f, "Min: 0", "Max: 100", 1f);
                listing.AddLabeledSlider("- Vanometric Power Cell: " + settings.tweak_powerUsage_vanometricCell, ref settings.tweak_powerUsage_vanometricCell, 0f, 10000f, "Min: 0", "Max: 10000", 50f);
            }
            listing.GapLine();
        }

        public void DoSettings_Raids(Listing_Standard listing)
        {
            // Tweak: No More Breach Raids
            listing.CheckboxEnhanced("No More Breach Raids", "Removes Breach Raids as an option for raiders.", ref settings.tweak_noMoreBreachRaids);
            listing.GapLine();
            // Tweak: No More Breach Raids
            listing.CheckboxEnhanced("No More Drop Pod Raids", "Removes Drop Pod Raids as an option for raiders. Does not work with RimWar.", ref settings.tweak_noMoreDropPodRaids);
            listing.GapLine();
            // Tweak: No More Breach Raids
            listing.CheckboxEnhanced("No More Sapper Raids", "Removes Sapper Raids as an option for raiders.", ref settings.tweak_noMoreSapperRaids);
            listing.GapLine();
            // Tweak: No More Breach Raids
            listing.CheckboxEnhanced("No More Siege Raids", "Removes Siege Raids as an option for raiders.", ref settings.tweak_noMoreSiegeRaids);
            listing.GapLine();
        }

        public void DoSettings_Resources(Listing_Standard listing)
        {
            // Tweak: Metal Doesn't Burn
            listing.CheckboxEnhanced("Metal Doesn't Burn", "Removes flammability from steel and plasteel, setting gold and silver to just a scratch above 0 so they have a chance to melt away still.", ref settings.tweak_metalDoesntBurn);
            listing.GapLine();
            // Tweak: No Mineable Components
            listing.CheckboxEnhanced("No Mineable Components", "Removes mineable components from map generation.", ref settings.tweak_noMineableComponents);
            listing.GapLine();
            // Tweak: No Mineable Plasteel
            listing.CheckboxEnhanced("No Mineable Plasteel", "Removes mineable plasteel from map generation.", ref settings.tweak_noMineablePlasteel);
            listing.GapLine();
            // Tweak: Old Component Names
            listing.CheckboxEnhanced("Old Component Names", "Back in my day we had industrial and spacer components! Now you can too!", ref settings.tweak_oldComponentNames);
            listing.GapLine();
            // Tweak: Pretty Precious Metals
            listing.CheckboxEnhanced("Pretty Precious Metals", "Changes the beauty of gold and silver (the item, not as a material) from -4 to 4.", ref settings.tweak_prettyPreciousMetals);
            listing.GapLine();
            // Tweak: Stackable Chunks
            listing.CheckboxEnhanced("Stackable Chunks", "Allows stone chunks and slag to be stackable. Default: 5", ref settings.tweak_stackableChunks);
            if (settings.tweak_stackableChunks)
            {
                listing.AddLabeledSlider("- Stone Chunk Stack Size: " + settings.tweak_stackableChunks_stone, ref settings.tweak_stackableChunks_stone, 1f, 400f, "Min: 1", "Max: 400", 1f);
                listing.AddLabeledSlider("- Slag Chunk Stack Size: " + settings.tweak_stackableChunks_slag, ref settings.tweak_stackableChunks_slag, 1f, 400f, "Min: 1", "Max: 400", 1f);
            }
            listing.GapLine();
            // Tweak: Stronger Steel
            listing.CheckboxEnhanced("Stronger Steel", "Doubles the durability of steel as a material.", ref settings.tweak_strongerSteel);
            listing.GapLine();
        }

        public void DoSettings_Royalty(Listing_Standard listing)
        {
            // Tweak: Delayed Royalty
            listing.CheckboxEnhanced("Delayed Royalty", "Delays the Royalty starter quests by a few days more than usual to give additional time to get ready for them.", ref settings.tweak_delayedRoyalty);
            listing.GapLine();
            // Tweak: Uninstall Mech Shields
            listing.CheckboxEnhanced("Uninstallable Mech Shields", "If enabled, allows you to uninstall mech cluster shields for your own use. This also prevents them shutting down when the cluster is cleared so they remain useful.", ref settings.tweak_uninstallableMechShields);
            listing.GapLine();
            // Tweak: Free Meditation
            listing.CheckboxEnhanced("Free Meditation", "Allows any pawn to use any meditation focus regardless of the focus types they have. Automatically patches all vanilla and modded sources.", ref settings.tweak_animaMeditationAll);
            listing.GapLine();
            // Tweak: No Natural/Artistic Focus Limits
            listing.CheckboxEnhanced("No Natural/Artistic Focus Limits", "Allows any pawn to use Natural or Artistic meditation focus types regardless of backstory.", ref settings.tweak_animaRemoveBackstoryLimits);
            listing.GapLine();
        }

        public void DoSettings_Anima(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Anima Tweaks", "This entire section is disabled by default for compatibility sake mostly, some Anima related tweaks function regardless of if they've been changed (any changing a radius for example) so without this setting could have caused compatibility issue with other Anima related mods if you prefer to use those.", ref settings.tweak_animaTweaks);
            if (settings.tweak_animaTweaks)
            {
                listing.GapLine();
                // Tweak: Replantable Anima
                listing.CheckboxEnhanced("Replantable Anima", "Makes it so you can move Anima trees like any other.", ref settings.tweak_replantableAnima);
                listing.GapLine();
                // Tweak: Disable Anima Scream
                listing.CheckboxEnhanced("Disable Anima Scream", "Disables the Anima Scream debuff from chopping them down.", ref settings.tweak_animaDisableScream);
                listing.GapLine();
                if (!settings.tweak_animaDisableScream)
                {
                    listing.AddLabeledSlider($"- Scream Debuff: {settings.tweak_animaScreamDebuff.ToString("0")}", ref settings.tweak_animaScreamDebuff, -20f, 20f, "Min: -20", "Max: 20", 1f);
                    listing.AddLabeledSlider($"- Scream Duration: {settings.tweak_animaScreamLength.ToString("0.0")}", ref settings.tweak_animaScreamLength, 0.1f, 20f, "Min: 0.1", "Max: 20", 0.1f);
                    listing.AddLabeledSlider($"- Scream Stack Limit: {settings.tweak_animaScreamStackLimit.ToString("0")}", ref settings.tweak_animaScreamStackLimit, 1f, 20f, "Min: 1", "Max: 20", 1f);
                    listing.GapLine();
                }
                // Tweak: Anima Grass per Psylink Level
                listing.Label("Anima Grass per Psylink Level");
                {
                    listing.Note("The values are the amount of grass needed to be grown on top of what was already needed for the previous level.", GameFont.Tiny, Color.gray);
                    if (GetPsylinkStuff)
                    {
                        int levelint = 0;
                        for (int i = 0; i < settings.tweak_animaPsylinkLevelNeeds.Count; i++)
                        {
                            levelint++;
                            string intBufferString = settings.tweak_animaPsylinkLevelNeeds[i].ToString();
                            int intBufferInt = settings.tweak_animaPsylinkLevelNeeds[i];
                            listing.TextFieldNumericLabeled("Psylink Level " + levelint, ref intBufferInt, ref intBufferString, 0, 500);
                            settings.tweak_animaPsylinkLevelNeeds[i] = intBufferInt;
                        }
                    }
                }
                listing.GapLine();
                // Tweak: Nature Shrines Always Buildable
                listing.CheckboxEnhanced("Nature Shrines Always Buildable", "Nature Shrines are usually only buildable when you have a nature based Psycaster, this unlocks that limitation.", ref settings.tweak_animaBuildableShrines);
                listing.GapLine();

                listing.Label("Tree Tweaks");
                listing.Note("These are specific to the Anima Tree itself.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                // Tweak: Artificial Building Radius
                listing.AddLabeledSlider($"- Artificial Building Radius: {settings.tweak_animaArtificialBuildingRadius.ToString("0.0")}", ref settings.tweak_animaArtificialBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a debuff is applied to the trees effects if artificial buildings are built in it.", GameFont.Tiny, Color.gray);
                // Tweak: Natural Building Radius
                listing.AddLabeledSlider($"- Natural Building Radius: {settings.tweak_animaBuffBuildingRadius.ToString("0.0")}", ref settings.tweak_animaBuffBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a buff is applied to the trees effects for natural buildings.", GameFont.Tiny, Color.gray);
                // Tweak: Max Natural Buildings
                listing.AddLabeledSlider($"- Max Natural Buildings: {settings.tweak_animaMaxBuffBuildings}", ref settings.tweak_animaMaxBuffBuildings, 1f, 40f, "Min: 1", "Max: 40", 1f);
                listing.Note("The maximum number of buildings which can buff the Anima Tree.", GameFont.Tiny, Color.gray);
                listing.GapLine();

                listing.Label("Shrine Tweaks");
                listing.Note("These are specific to the nature shrines that can be built to enhance the Anima tree.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                // Tweak: Artificial Building Radius
                listing.AddLabeledSlider($"- Artificial Building Radius: {settings.tweak_animaShrineBuildingRadius.ToString("0.0")}", ref settings.tweak_animaShrineBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a debuff is applied to the trees effects if artificial buildings are built in it.", GameFont.Tiny, Color.gray);
                // Tweak: Natural Building Radius
                listing.AddLabeledSlider($"- Natural Building Radius: {settings.tweak_animaShrineBuffBuildingRadius.ToString("0.0")}", ref settings.tweak_animaShrineBuffBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a buff is applied to the trees effects for natural buildings.", GameFont.Tiny, Color.gray);
                // Tweak: Max Natural Buildings
                listing.AddLabeledSlider($"- Meditation Psyfocus Gain Rate: {settings.tweak_animaMeditationGain.ToString("0.0")}", ref settings.tweak_animaMeditationGain, 0.1f, 20f, "Min: 0.1", "Max: 20", 0.1f);
                listing.Note("The amount of psyfocus a pawn gains per day of mediation.", GameFont.Tiny, Color.gray);
                listing.GapLine();

                TweaksGaloreStartup.Tweak_AnimaTweaks(settings);
            }
        }

        public bool GetPsylinkStuff
        {
            get
            {
                if (settings.tweak_animaPsylinkLevelNeeds.NullOrEmpty())
                {
                    CompProperties_Psylinkable psycomp = ThingDefOf.Plant_TreeAnima.GetCompProperties<CompProperties_Psylinkable>();
                    settings.tweak_animaPsylinkLevelNeeds = psycomp.requiredSubplantCountPerPsylinkLevel;
                }

                return true;
            }
        }

        public void DoSettings_Ideology(Listing_Standard listing)
        {
            // Tweak: Ancient Deconstruction
            listing.CheckboxEnhanced("Ancient Deconstruction", "Makes the Ancient Ruins added by Ideology deconstructable instead of having to destroy them.", ref settings.tweak_ancientDeconstruction);
            if (settings.tweak_ancientDeconstruction)
            {
                listing.CheckboxEnhanced("- Give Proper Materials", "Changes returned items to some reasonable materials instead of just slag.", ref settings.tweak_ancientDeconstruction_mode);
            }
            listing.GapLine();
            // Tweak: Darklight Glow Pods
            listing.CheckboxEnhanced("Darklight Glow Pods", "Makes glow pods spawned in insectoid nests use the darklight colour.", ref settings.tweak_darklightGlowPods);
            listing.GapLine();
            // Tweak: Disable Desired Apparel
            listing.CheckboxEnhanced("Disable Desired Apparel", "Disables default Ideology desired apparel from showing up. I got really fucking sick of this happening when I was testing faction loadouts, whoever came up with this and DIDN'T think to have an option to disable it deserves to step on an upright UK appliance plug.", ref settings.tweak_disableDesiredApparel);
            listing.GapLine();
            // Tweak: No Meme Limit
            listing.CheckboxEnhanced("No Meme Limit", "Raises the limit of how many memes you can choose to 1000...so effectively no limit.", ref settings.patch_noMemeLimit);
            listing.GapLine();
            // Tweak: Proper Suppression
            listing.CheckboxEnhanced("Proper Suppression", "Changes suppression slightly so that rebellions never happen if your slaves are kept suppressed.", ref settings.patch_properSuppression);
            listing.GapLine();
            // Tweak: Unlocked Ideology Buildings
            listing.CheckboxEnhanced("Unlocked Ideology Buildings", "Removes the meme restriction on ideology buildings so you can use them regardless of what memes you have. Includes floors and apparel.", ref settings.tweak_unlockedIdeologyBuildings);
            listing.GapLine();
        }

        public void DoSettings_Gauranlen(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Gauranlen Tweaks", "This entire section is disabled by default for compatibility sake mostly, some Gauranlen related tweaks function regardless of if they've been changed (any changing a radius for example) so without this setting could have caused compatibility issue with other Gauranlen related mods if you prefer to use those.", ref settings.tweak_gauranlenTweaks);
            if (settings.tweak_gauranlenTweaks)
            {
                // Tweak: Replantable Gauranlen
                listing.CheckboxEnhanced("Replantable Gauranlen", "Makes it so you can move Gauranlen trees like any other.", ref settings.tweak_replantableGauranlen);
                listing.GapLine();
                listing.Label("Tree Tweaks");
                listing.GapLine();
                // Tweak: Initial Connection Range
                listing.Label("Initial Connection Range", tooltip: "Initial connection strength will be in this range.");
                listing.AddLabeledSlider($"- Minimum: {settings.tweak_gauranlenInitialConnectionStrength.min.ToStringPercent()}", ref settings.tweak_gauranlenInitialConnectionStrength.min, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                listing.AddLabeledSlider($"- Maximum: {settings.tweak_gauranlenInitialConnectionStrength.max.ToStringPercent()}", ref settings.tweak_gauranlenInitialConnectionStrength.max, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                if(settings.tweak_gauranlenInitialConnectionStrength.min > settings.tweak_gauranlenInitialConnectionStrength.max)
                {
                    settings.tweak_gauranlenInitialConnectionStrength.max = settings.tweak_gauranlenInitialConnectionStrength.min;
                }
                listing.Note("These are specific to the Gauranlen Tree itself.", GameFont.Tiny, Color.gray);
                // Tweak: Artificial Building Radius
                listing.AddLabeledSlider($"- Artificial Building Radius: {settings.tweak_gauranlenArtificialBuildingRadius.ToString("0.0")}", ref settings.tweak_gauranlenArtificialBuildingRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The radius in which a debuff is applied to the trees effects if artificial buildings are built in it.", GameFont.Tiny, Color.gray);
                // Tweak: Connection Gain While Puning
                listing.AddLabeledSlider($"- Connection Gain While Puning: {settings.tweak_gauranlenConnectionGainPerHourPruning.ToStringPercent()}", ref settings.tweak_gauranlenConnectionGainPerHourPruning, 0.01f, 1f, "Min: 1%", "Max: 100%", 0.01f);
                listing.Note("Connection gained per hour of pruning.", GameFont.Tiny, Color.gray);
                // Tweak: Connection Loss Rate
                listing.AddLabeledSlider($"- Connection Loss Rate: {settings.tweak_gauranlenConnectionLossPerLevel.ToStringPercent()}", ref settings.tweak_gauranlenConnectionLossPerLevel, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listing.Note("This functions as a multiplier on the connection loss per level of connection.", GameFont.Tiny, Color.gray);
                // Tweak: Connection Loss Per Building
                listing.AddLabeledSlider($"- Connection Loss Per Building: {settings.tweak_gauranlenLossPerBuilding.ToStringPercent()}", ref settings.tweak_gauranlenLossPerBuilding, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listing.Note("This functions as a multiplier on the connection loss per artificial building in range.", GameFont.Tiny, Color.gray);
                // Tweak: Moss Growth Radius
                listing.AddLabeledSlider($"- Moss Growth Radius: {settings.tweak_gauranlenPlantGrowthRadius.ToString("0.0")}", ref settings.tweak_gauranlenPlantGrowthRadius, 0.1f, 40.9f, "Min: 0.1", "Max: 40.9", 0.1f);
                listing.Note("The max radius of the tree where it grows moss.", GameFont.Tiny, Color.gray);
                // Tweak: Moss Growth Days
                listing.AddLabeledSlider($"- Moss Growth Days: {settings.tweak_gauranlenMossGrowDays.ToString("0")}", ref settings.tweak_gauranlenMossGrowDays, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Amount of days till Gauralen Moss grows to maturity.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                listing.Label("Dryad Tweaks");
                listing.Note("These are specific to the Dryads spawned by the Gauranlen Tree.", GameFont.Tiny, Color.gray);
                listing.GapLine();
                // Tweak: Max Dryads
                listing.AddLabeledSlider($"- Max Dryads: {settings.tweak_gauranlenMaxDryads.ToString("0")}", ref settings.tweak_gauranlenMaxDryads, 3f, 30f, "Min: 3", "Max: 30", 1f);
                listing.Note("This is usually a curve with multiple points, and that's great. But my brain is smooth as fuck so this is a set value slapped onto the end, the normal values apply till 75+% connection and then it uses whatever you set this to.", GameFont.Tiny, Color.gray);
                // Tweak: Dryad Spawn Days
                listing.AddLabeledSlider($"- Dryad Spawn Days: {settings.tweak_gauranlenDryadSpawnDays.ToString("0")}", ref settings.tweak_gauranlenDryadSpawnDays, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Number of days between the spawning of Dryads.", GameFont.Tiny, Color.gray);
                // Tweak: Dryad Cocoon Days
                listing.AddLabeledSlider($"- Dryad Cocoon Days: {settings.tweak_gauranlenCocoonDaysToComplete.ToString("0")}", ref settings.tweak_gauranlenCocoonDaysToComplete, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Number of days it takes for a Dryad to finish the cocoon process.", GameFont.Tiny, Color.gray);
                // Tweak: Gaumaker Pod Days
                listing.AddLabeledSlider($"- Gaumaker Pod Days: {settings.tweak_gauranlenMossGrowDays.ToString("0")}", ref settings.tweak_gauranlenMossGrowDays, 1f, 30f, "Min: 1", "Max: 30", 1f);
                listing.Note("Amount of days it takes for a Gaumaker pod to mature.", GameFont.Tiny, Color.gray);
                // Tweak: Moss Growth Days
                listing.AddLabeledSlider($"- Seed Yield: {settings.tweak_gauranlenPodHarvestYield.ToString("0")}", ref settings.tweak_gauranlenPodHarvestYield, 1f, 10f, "Min: 1", "Max: 10", 1f);
                listing.Note("Amount of seeds obtained from a Gaumaker Pod.", GameFont.Tiny, Color.gray);
                listing.GapLine();

                TweaksGaloreStartup.Tweak_GauranlenTweaks(settings);
            }
        }

        public void DoSettings_Biotech(Listing_Standard listing)
        {
            listing.Label("Genetics");
            listing.GapLine();
            // Tweak: Flatten Complexity
            listing.CheckboxEnhanced("Flatten Complexity", "Remove complexity values from genes so they are all set to 0.", ref settings.tweak_flattenComplexity);
            // Tweak: Flatten Metabolism
            listing.CheckboxEnhanced("Flatten Metabolism", "Remove metabolism values from genes so they are all set to 0.", ref settings.tweak_flattenMetabolism);
            // Tweak: Flatten Archites
            listing.CheckboxEnhanced("Flatten Archites", "Remove archite values from genes so they are all set to 0.", ref settings.tweak_flattenArchites);
            // Tweak: Show Genes Tab
            listing.CheckboxEnhanced("Show Genes Tab", "By default the Genes tab is hidden on pawns, if you don't have another mod revealing it, you can use this to do so instead. Just keep in mind this was potentially hidden in vanilla for a reason.", ref settings.tweak_showGenesTab);
            listing.GapLine();
            listing.Gap();
            listing.CheckboxEnhanced("Enable Player Mech Tweaks", "This controls if any of the player mechanoid tweaks even try to function, some tweaks have no other way of knowing if they should even run.", ref settings.tweak_playerMechTweaks);
            if (settings.tweak_playerMechTweaks)
            {
                // Tweak: Mechanoid Skill Level
                // TODO: Something fucky going on with this.
                //listing.AddLabeledSlider($"- Skill Level: {settings.tweak_mechanoidSkillLevel}", ref settings.tweak_mechanoidSkillLevel, 0f, 20f, "Min: 0", "Max: 20", 1f);
                // Tweak: Mechanoid Work Speed
                // TODO: Replace with a listing for individual work types, they all seem to use different speeds!
                //listing.AddLabeledSlider($"- Work Speed: {settings.tweak_mechanoidWorkSpeed.ToStringPercent()}", ref settings.tweak_mechanoidWorkSpeed, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);
                // Tweak: Mechanoid Drain Rate
                listing.AddLabeledSlider($"- Drain Rate: {settings.tweak_mechanoidDrainRate.ToStringPercent()}", ref settings.tweak_mechanoidDrainRate, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);
                // Tweak: Mechanoid Recharge Rate
                // TODO Transpiler Required for Recharge Rate. Fuckin bullshit is what this is.
                //listing.AddLabeledSlider($"- Recharge Rate: {settings.tweak_mechanoidRechargeRate.ToStringPercent()}", ref settings.tweak_mechanoidRechargeRate, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);

                TweaksGaloreStartup.Tweak_PlayerMechTweaks(settings);
            }
            listing.GapLine();
            listing.CheckboxEnhanced("Enable Mechanitor Tweaks", "This controls if any of the mechanitor tweaks even try to function, some tweaks have no other way of knowing if they should even run.", ref settings.tweak_mechanitorTweaks);
            if (settings.tweak_mechanitorTweaks)
            {
                listing.CheckboxEnhanced("- Disable Range Limit", "Makes mechanoids controllable regardless of distance to Mechanitor. If they can design a neural interface they can design something with better than cheap wifi range.", ref settings.tweak_mechanitorDisableRange);
                // Tweak: Mechanitor Range Multiplier
                //listing.AddLabeledSlider($"- Disable Range Limit: {settings.tweak_mechanitorRangeMulti.ToStringPercent()}", ref settings.tweak_mechanitorRangeMulti, 0.05f, 5f, "Min: 5%", "Max: 500%", 0.05f);
                // Tweak: Mechanitor Bandwidth Base
                listing.AddLabeledSlider($"- Bandwidth Base: {settings.tweak_mechanitorBandwidthBase}", ref settings.tweak_mechanitorBandwidthBase, 1f, 30f, "Min: 1", "Max: 30", 1f);
                // Tweak: Mechanitor Control Group Base
                listing.AddLabeledSlider($"- Control Group Base: {settings.tweak_mechanitorControlGroupBase}", ref settings.tweak_mechanitorControlGroupBase, 1f, 10f, "Min: 1", "Max: 10", 1f);
                // Tweak: Mechanitor Bandwidth Per Band Node
                listing.AddLabeledSlider($"- Bandwidth per Band Node: {settings.tweak_mechanitorBandNodeBandwidth}", ref settings.tweak_mechanitorBandNodeBandwidth, 1f, 20f, "Min: 1", "Max: 20", 1f);

                TweaksGaloreStartup.Tweak_MechanitorTweaks(settings);
            }
            listing.GapLine();
        }

        public void DoSettings_Polux(Listing_Standard listing)
        {
            listing.CheckboxEnhanced("Enable Polux Tweaks", "This entire section is disabled by default for compatibility sake mostly, some Polux related tweaks function regardless of if they've been changed (any changing a radius for example) so without this setting could have caused compatibility issue with other Polux related mods if you prefer to use those.", ref settings.tweak_poluxTweaks);
            if (settings.tweak_poluxTweaks)
            {
                // Tweak: Replantable Pollux
                listing.CheckboxEnhanced("Replantable Polux", "Makes it so you can move Polux trees like any other.", ref settings.tweak_replantablePolux);
                listing.GapLine();
                listing.Label("Tree Tweaks");
                listing.GapLine();
                // Tweak: Polux Effect Radius
                listing.AddLabeledSlider($"- Effect Radius: {settings.tweak_poluxEffectRadius.ToString("0.0")}", ref settings.tweak_poluxEffectRadius, 0.1f, 42f, "Min: 0.1", "Max: 42", 0.1f);
                listing.Note("Lower values increases speed of the pollution cleansing effect.", GameFont.Tiny);
                listing.GapLine();
                // Tweak: Polux Effect Rate
                listing.AddLabeledSlider($"- Effect Rate Multiplier: {settings.tweak_poluxEffectRate.ToStringPercent()}", ref settings.tweak_poluxEffectRate, 0.01f, 2f, "Min: 1%", "Max: 200%", 0.01f);
                listing.Note("Lower values increases speed of the pollution cleansing effect.", GameFont.Tiny);
                listing.GapLine();
                // Tweak: Polux Artificial Buildings
                listing.CheckboxEnhanced("- Ignore Artificial Buildings", "Vanilla usually disables the tree's effects if there are artificial buildings in its radius, this disables that behaviour and lets it function anyway.", ref settings.tweak_poluxArtificialDisables);
                listing.GapLine();

                TweaksGaloreStartup.Tweak_PoluxTweaks(settings);
            }
        }

        public void DoSettings_Defaults(Listing_Standard listing)
        {
            listing.Note("These buttons restore the default values for a given category, there is no confirmation box so be sure you want to before you click. Restored categories will not be accessible until you have relaunched the game.");
            if (listing.ButtonText("General"))
            {
                DefaultUtil.RestoreSettings_Vanilla(settings);
                Messages.Message("Tweaks Galore: 'General' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreGeneral = true;
            }
            if (listing.ButtonText("Mechanoids"))
            {
                DefaultUtil.RestoreSettings_Mechanoids(settings);
                Messages.Message("Tweaks Galore: 'Mechanoids' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreMechanoids = true;
            }
            if (listing.ButtonText("Penned Animals"))
            {
                DefaultUtil.RestoreSettings_PennedAnimals(settings);
                Messages.Message("Tweaks Galore: 'Penned Animals' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restorePennedAnimals = true;
            }
            if (listing.ButtonText("Power"))
            {
                DefaultUtil.RestoreSettings_Power(settings);
                Messages.Message("Tweaks Galore: 'Power' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restorePower = true;
            }
            if (listing.ButtonText("Raids"))
            {
                DefaultUtil.RestoreSettings_Raids(settings);
                Messages.Message("Tweaks Galore: 'Raids' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreRaids = true;
            }
            if (listing.ButtonText("Resources"))
            {
                DefaultUtil.RestoreSettings_Resources(settings);
                Messages.Message("Tweaks Galore: 'Resources' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreResources = true;
            }
            if (listing.ButtonText("Royalty"))
            {
                DefaultUtil.RestoreSettings_Royalty(settings);
                Messages.Message("Tweaks Galore: 'Royalty' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreRoyalty = true;
            }
            if (listing.ButtonText("Anima"))
            {
                DefaultUtil.RestoreSettings_Anima(settings);
                Messages.Message("Tweaks Galore: 'Anima' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreAnima = true;
            }
            if (listing.ButtonText("Ideology"))
            {
                DefaultUtil.RestoreSettings_Ideology(settings);
                Messages.Message("Tweaks Galore: 'Ideology' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreIdeology = true;
            }
            if (listing.ButtonText("Gauranlen"))
            {
                DefaultUtil.RestoreSettings_Gauranlen(settings);
                Messages.Message("Tweaks Galore: 'Gauranlen' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreGauranlen = true;
            }
            if (listing.ButtonText("Biotech"))
            {
                DefaultUtil.RestoreSettings_Biotech(settings);
                Messages.Message("Tweaks Galore: 'Biotech' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreBiotech = true;
            }
            if (listing.ButtonText("Polux"))
            {
                DefaultUtil.RestoreSettings_Polux(settings);
                Messages.Message("Tweaks Galore: 'Polux' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restorePolux = true;
            }
            if (listing.ButtonText("RESTORE ALL"))
            {
                DefaultUtil.RestoreALL(settings);
                Messages.Message("Tweaks Galore: ALL tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                restoreGeneral = true;
                restoreMechanoids = true;
                restorePennedAnimals = true;
                restorePower = true;
                restoreRaids = true;
                restoreResources = true;
                restoreRoyalty = true;
                restoreAnima = true;
                restoreIdeology = true;
                restoreGauranlen = true;
                restoreBiotech = true;
                restorePolux = true;
            }
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
        Anima,
        Ideology,
        Gauranlen,
        Biotech,
        Polux,
        Defaults
    }
}
