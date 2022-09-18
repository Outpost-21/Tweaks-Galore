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
        public Vector2 scrollPosition;
        public Dictionary<string, TrainabilityDef> TrainabilityRadioDict = new Dictionary<string, TrainabilityDef>();

        internal static string VersionDir => Path.Combine(ModLister.GetActiveModWithIdentifier("Neronix17.TweaksGalore").RootDir.FullName, "Version.txt");
        public static string CurrentVersion { get; private set; }

        public TweaksGaloreMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<TweaksGaloreSettings>();
            mod = this;

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            CurrentVersion = $"{version.Major}.{version.Minor}.{version.Build}";

            LogUtil.LogMessage($"Version: {CurrentVersion} ::");

            if (Prefs.DevMode)
            {
                File.WriteAllText(VersionDir, CurrentVersion);
            }

            new Harmony("neronix17.tweaksgalore.rimworld").PatchAll();
        }

        public override void DoOptionsCategoryContents(OptionCategoryDef category, Listing_Standard listing)
        {
            base.DoOptionsCategoryContents(category, listing);
            if(category.defName == "TweaksGalore_OptionsCategory")
            {
                listing.SettingsDropdown("Current Page", "", ref currentPage, listing.ColumnWidth);
                listing.Note("You will need to restart the game for most of these settings to take effect.", GameFont.Tiny);
                listing.GapLine();
                if (currentPage == TweaksGaloreSettingsPage.General)
                {
                    DoSettings_Vanilla(listing);
                }
                else if (currentPage == TweaksGaloreSettingsPage.Mechanoids)
                {
                    DoSettings_Mechanoids(listing);
                }
                else if (currentPage == TweaksGaloreSettingsPage.Power)
                {
                    DoSettings_Power(listing);
                }
                else if (currentPage == TweaksGaloreSettingsPage.Raids)
                {
                    DoSettings_Raids(listing);
                }
                else if (currentPage == TweaksGaloreSettingsPage.Resources)
                {
                    DoSettings_Resources(listing);
                }
                else if (currentPage == TweaksGaloreSettingsPage.Royalty)
                {
                    if (!ModLister.RoyaltyInstalled)
                    {
                        listing.Note("Royalty is not installed. These options would do nothing for you.");
                    }
                    else
                    {
                        DoSettings_Royalty(listing);
                    }
                }
                else if (currentPage == TweaksGaloreSettingsPage.Ideology)
                {
                    if (!ModLister.IdeologyInstalled)
                    {
                        listing.Note("Ideology is not installed. These options would do nothing for you.");
                    }
                    else
                    {
                        DoSettings_Ideology(listing);
                    }
                }
                else if (currentPage == TweaksGaloreSettingsPage.Biotech)
                {
                    if (!ModLister.BiotechInstalled)
                    {
                        listing.Note("Biotech is not installed. These options would do nothing for you.");
                    }
                    else
                    {
                        DoSettings_Biotech(listing);
                    }
                }
            }
        }

        public void DoSettings_Vanilla(Listing_Standard listingStandard)
        {
            // Tweak: Boomalopes Bleed Chemfuel
            listingStandard.CheckboxLabeled("Boomalopes Bleed Chemfuel", ref settings.tweak_boomalopesBleedChemfuel, "Changes the blood def from boomalopes and boomrats to be chemfuel puddles instead.");
            listingStandard.GapLine();
            // Tweak: Chatty Comms
            listingStandard.CheckboxLabeled("Chatty Comms", ref settings.tweak_chattyComms, "Adds a recreation type to comms consoles allowing them to be used by pawns to gain recreation through 'chatting'.");
            listingStandard.GapLine();
            // Tweak: Destroy Anything
            listingStandard.CheckboxLabeled("Destroy Anything", ref settings.tweak_destroyAnything, "Allows the smelting of pretty much anything anything in the crematorium aside from weapons, apparel and minified buildings. Be careful so you don't burn anything important.");
            listingStandard.GapLine();
            // Tweak: Don't Pack Food
            listingStandard.CheckboxLabeled("Don't Pack Food", ref settings.tweak_dontPackFood, "Prevents pawns from stuffing food into their inventory to carry around.");
            listingStandard.GapLine();
            // Tweak: Faster Smoothing
            listingStandard.CheckboxLabeled("Faster Smoothing", ref settings.tweak_fasterSmoothing, "Enables an additional stat to speed up smoothing of natural rock. Default: 300%");
            if (settings.tweak_fasterSmoothing)
            {
                listingStandard.AddLabeledSlider("Faster Smoothing Factor: " + settings.tweak_fasterSmoothing_factor.ToStringPercent(), ref settings.tweak_fasterSmoothing_factor, 0.1f, 10.0f, "Min: 10%", "Max: 1000%", 0.1f);
            }
            listingStandard.GapLine();
            // Tweak: Full Deconstruction Return
            listingStandard.CheckboxLabeled("Full Deconstruct Return", ref settings.tweak_fixDeconstructionReturn, "Returns the full amount it takes to build something. No more punishment for moving a building that isn't uninstallable.");
            listingStandard.GapLine();
            // Tweak: Glowing Ambrosia
            listingStandard.CheckboxLabeled("Glowing Ambrosia", ref settings.tweak_glowingAmbrosia, "Gives Ambrosia a slight glow around it, making natural spawning Ambrosia easier to find on a map in the dark.");
            listingStandard.GapLine();
            // Tweak: Glowing Healroot
            string healrootName = (settings.tweak_healrootToXerigium ? "Xerigium" : "Healroot");
            listingStandard.CheckboxLabeled("Glowing " + healrootName, ref settings.tweak_glowingHealroot, "Gives " + healrootName + " a slight glow around it, making natural " + healrootName + " easier to find on a map in the dark.");
            listingStandard.GapLine();
            // Tweak: Healroot to Xerigium
            listingStandard.CheckboxLabeled("Healroot to Xerigium", ref settings.tweak_healrootToXerigium, "Reverts the old name change of the herbal medicine plant Healroot back to Xerigium like it used to be in older game versions.");
            listingStandard.GapLine();
            // Tweak: Hunters Can Use Melee
            listingStandard.CheckboxLabeled("Hunters Can Use Melee", ref settings.tweak_hunterMelee, "Allows hunters to use melee, regardless of how well they can do so.");
            if (settings.tweak_hunterMelee)
            {
                listingStandard.CheckboxLabeled("Allow Fists", ref settings.tweak_hunterMelee_fistFighting, "Allows hunters to use their fists to hunt, stupid but it works.");
                //if (Patches_HunterMelee.SimpleSidearmsLoaded)
                //{
                //    listingStandard.CheckboxLabeled("Enable Simple Sidearms", ref settings.tweak_hunterMelee_allowSimpleSidearms, "Enable Simple Sidearms Compatibility, allowing pawns to consider sidearms for hunting.");
                //}
            }
            listingStandard.GapLine();
            // Tweak: Incident Pawn Stats
            listingStandard.CheckboxLabeled("Incident Pawn Stats", ref settings.patch_incidentPawnStats, "Displays the information of any pawns rewarded as part of incidents.");
            listingStandard.GapLine();
            // Tweak: Infestation Blocking Floors
            listingStandard.CheckboxLabeled("Infestation Blocking Floors", ref settings.patch_strongFloorsStopInfestations, "Prevents infestations happening on harder floors (Steel, Stone, Concrete). Only prevents the event picking that spot, hives can still spawn on them if an event happens closeby.");
            listingStandard.GapLine();
            // Tweak: Insulting Spree Nerf
            listingStandard.CheckboxLabeled("Insulting Spree Nerf", ref settings.tweak_insultingSpreeNerf, "Makes the Insulting Spree mental break less annoying to deal with." +
                "\n- Intensity: Minor --> Major" +
                "\n- Max Duration: 45,000 ticks --> 20,000 ticks" +
                "\n- Min Duration: 25,000 ticks --> 2,500 ticks" +
                "\n- Base Event Chance: 0.5 --> 0.15");
            listingStandard.GapLine();
            // Tweak: Impassable Deep Water
            listingStandard.CheckboxLabeled("Impassable Deep Water", ref settings.tweak_impassableDeepWater, "Changes deep water to be impassable by pawns, preventing them from pathing through it.");
            listingStandard.GapLine();
            // Tweak: Lag free Lamps
            listingStandard.CheckboxLabeled("Lag Free Lamps", ref settings.tweak_lagFreeLamps, "Removes the fuel comp from fuelled lamps, so they now no longer run code constantly they impact performance that little bit less.");
            listingStandard.GapLine();
            // Tweak: Megasloth to Megatherium
            listingStandard.CheckboxLabeled("Megasloth to Megatherium", ref settings.tweak_oldMegaslothName, "Reverts the name change of the Megatherium so it retains the obviously cooler name.");
            listingStandard.GapLine();
            // Tweak: Misanthrope Trait
            listingStandard.CheckboxLabeled("Misanthrope Trait", ref settings.tweak_misanthropeTrait, "Adds the misanthrope trait (dislikes humans), also makes it so if a pawn was going to spawn with both the misandrist and misogynist traits that they instead get the misanthrope trait.");
            listingStandard.GapLine();
            // Tweak: Next Restock Timer
            listingStandard.CheckboxLabeled("Next Restock Timer", ref settings.patch_settlementTraderTimer, "Displays in the world map information of settlements whether or not they have restocked since you last visited and how long till their next restock.");
            listingStandard.GapLine();
            // Tweak: No Breakdowns
            if(ModLister.GetActiveModWithIdentifier("kentington.saveourship2") == null)
            {
                listingStandard.CheckboxLabeled("No Breakdowns", ref settings.tweak_noBreakdowns, "Removes the breakdown comp from anything that has it, artificially enforced resource sinks are LAZY.");
                listingStandard.GapLine();
            }
            // Tweak: No Friend Shaped Manhunters
            listingStandard.CheckboxLabeled("No Friend Shaped Manhunters", ref settings.tweak_noFriendShapedManhunters, "Sets parameters which decide if an animal can arrive as manhunter or not. This does not prevent it happening if they take damage.");
            if (settings.tweak_noFriendShapedManhunters)
            {
                listingStandard.AddLabelLine("Prevent by Trainability");
                listingStandard.CheckboxLabeled("Intermediate", ref settings.tweak_NFSMTrainability_Intermediate);
                listingStandard.CheckboxLabeled("Advanced", ref settings.tweak_NFSMTrainability_Advanced);
                listingStandard.Gap(24f);
                listingStandard.CheckboxLabeled("Prevent if Nuzzle-able", ref settings.tweak_NFSMNuzzleHours);
                listingStandard.AddLabeledSlider("Prevent if Wildness Below: " + settings.tweak_NFSMWildness.ToStringPercent(), ref settings.tweak_NFSMWildness, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                listingStandard.AddLabeledSlider("Prevent if Combat Power Below: " + settings.tweak_NFSMCombatPower, ref settings.tweak_NFSMCombatPower, 0f, 200f, "Min: 0", "Max: 200", 1f);
                listingStandard.CheckboxLabeled("Disable Manhunter on Tame Fail", ref settings.tweak_NFSMDisableManhunterOnTame, "Uses the same parameters to prevent animals going manhunter on tame fail too, they can still go manhunter if damaged.");
            }
            listingStandard.GapLine();
            // Tweak: Not So Wild Berries
            listingStandard.CheckboxLabeled("Not So Wild Berries", ref settings.tweak_notSoWildBerries, "Adds the ground sowing tag to wild berries so they can be planted.");
            listingStandard.GapLine();
            // Tweak: Prisoners Don't Have Keys
            listingStandard.CheckboxLabeled("Prisoners Don't Have Keys", ref settings.patch_prisonersDontHaveKeys, "Selectively controls whether prisoners and slaves can open doors during breakout and rebellion.");
            if (settings.patch_prisonersDontHaveKeys)
            {
                listingStandard.CheckboxLabeled("Applies to Prisoners", ref settings.patch_pdhk_prisoners);
                listingStandard.CheckboxLabeled("Applies to Slaves", ref settings.patch_pdhk_slaves);
                listingStandard.CheckboxLabeled("Escaping pawns can open their own door", ref settings.patch_pdhk_ownDoor);
            }
            listingStandard.GapLine();
            // Tweak: Skilled Stonecutting
            listingStandard.CheckboxLabeled("Skilled Stonecutting", ref settings.tweak_skilledStonecutting, "Makes stonecutting give crafting skill increases, and makes more skilled crafters create blocks quickler.");
            listingStandard.GapLine();
            // Tweak: Skill Rates
            listingStandard.CheckboxLabeled("Skill Rates Adjustment", ref settings.tweak_skillRates, "Enables more control over skill gain/loss rates and what levels that should happen at.");
            if (settings.tweak_skillRates)
            {
                listingStandard.AddLabeledSlider("Skill Loss Multiplier: " + settings.tweak_skillRateLoss.ToStringPercent(), ref settings.tweak_skillRateLoss, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listingStandard.AddLabeledSlider("Skill Gain Multiplier: " + settings.tweak_skillRateGain.ToStringPercent(), ref settings.tweak_skillRateGain, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listingStandard.AddLabeledSlider("Skill Loss Threshold: " + settings.tweak_skillRateLossThreshold.ToString(), ref settings.tweak_skillRateLossThreshold, 0f, 20f, "Min: 0", "Max: 20", 1f);
                listingStandard.Note("Skill loss threshold is the level a skill has to reach before skill loss starts happening, it will prevent skill loss entirely below that level.", GameFont.Tiny);
            }
            listingStandard.GapLine();
            // Tweak: Slim Rim
            listingStandard.CheckboxLabeled("Slim Rim", ref settings.patch_slimRim, "Allows the disabling of body types on pawn generation. Does this during generation of the pawn so will not affect existing ones. The replacement body will be either Male or Female, whichever matches the pawns gender.");
            if (settings.patch_slimRim)
            {
                listingStandard.CheckboxLabeled("Fat", ref settings.patch_slimRim_fat);
                listingStandard.CheckboxLabeled("Hulk", ref settings.patch_slimRim_hulk);
                listingStandard.CheckboxLabeled("Thin", ref settings.patch_slimRim_thin);
            }
            listingStandard.GapLine();
            // Tweak: Trait Count Adjustment
            listingStandard.CheckboxLabeled("Trait Count Adjustment", ref settings.tweak_traitCountAdjustment, "There may be cases where this cannot apply, such as modded alien races where they have been set up to have a specific trait count.");
            if (settings.tweak_traitCountAdjustment)
            {
                listingStandard.Label("Trait Count Range");
                listingStandard.Note($"Current: {settings.tweak_traitCountRange.min}-{settings.tweak_traitCountRange.max}  Min: 1  Max: 8", GameFont.Tiny);
                listingStandard.IntRange(ref settings.tweak_traitCountRange, 1, 8);
            }
            listingStandard.GapLine();
        }

        public void DoSettings_Mechanoids(Listing_Standard listingStandard)
        {
            // Tweak: Disable Adapting
            listingStandard.CheckboxLabeled("Disable Adapting", ref settings.patch_disableMechanoidAdapting, "Disables the ability for Mechanoids to adapt to EMP.");
            listingStandard.GapLine();
            // Tweak: Heat Armor
            listingStandard.Label("Mechanoid Heat Armour: " + settings.tweak_mechanoidHeatArmour.ToStringPercent());
            listingStandard.AddLabeledSlider(null, ref settings.tweak_mechanoidHeatArmour, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
            listingStandard.GapLine();
            // Tweak: Pre-1.0 Ship Parts
            listingStandard.CheckboxLabeled("Pre-1.0 Ship Parts", ref settings.tweak_preReleaseShipParts, "Returns psychic and defoliator ship parts to their B18 state where they actually provide a decent reward." +
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
                "\n- Glittertech Medicine x5");
            listingStandard.GapLine();
        }

        public void DoSettings_Power(Listing_Standard listingStandard)
        {
            // Tweak: Power Tweaks
            listingStandard.CheckboxLabeled("Power Usage Tweaks", ref settings.tweak_powerUsageTweaks, "Allows tweaking the usage rate of some buildings. If they usually produce then it configures how much they output instead.");
            if (settings.tweak_powerUsageTweaks)
            {
                listingStandard.AddLabeledSlider("Standing Lamp: " + settings.tweak_powerUsage_lamp, ref settings.tweak_powerUsage_lamp, 0f, 100f, "Min: 0", "Max: 100", 1f);
                listingStandard.AddLabeledSlider("Sun Lamp: " + settings.tweak_powerUsage_sunlamp, ref settings.tweak_powerUsage_sunlamp, 0f, 10000f, "Min: 0", "Max: 10000", 50f);
                listingStandard.AddLabeledSlider("Auto-Door: " + settings.tweak_powerUsage_autodoor, ref settings.tweak_powerUsage_autodoor, 0f, 100f, "Min: 0", "Max: 100", 1f);
                listingStandard.AddLabeledSlider("Vanometric Power Cell: " + settings.tweak_powerUsage_vanometricCell, ref settings.tweak_powerUsage_vanometricCell, 0f, 10000f, "Min: 0", "Max: 10000", 50f);
            }
        }

        public void DoSettings_Raids(Listing_Standard listingStandard)
        {
            // Tweak: No More Breach Raids
            listingStandard.CheckboxLabeled("No More Breach Raids", ref settings.tweak_noMoreBreachRaids, "Removes Breach Raids as an option for raiders.");
            listingStandard.GapLine();
            // Tweak: No More Breach Raids
            listingStandard.CheckboxLabeled("No More Drop Pod Raids", ref settings.tweak_noMoreDropPodRaids, "Removes Drop Pod Raids as an option for raiders. Does not work with RimWar.");
            listingStandard.GapLine();
            // Tweak: No More Breach Raids
            listingStandard.CheckboxLabeled("No More Sapper Raids", ref settings.tweak_noMoreSapperRaids, "Removes Sapper Raids as an option for raiders.");
            listingStandard.GapLine();
            // Tweak: No More Breach Raids
            listingStandard.CheckboxLabeled("No More Siege Raids", ref settings.tweak_noMoreSiegeRaids, "Removes Siege Raids as an option for raiders.");
            listingStandard.GapLine();
        }

        public void DoSettings_Resources(Listing_Standard listingStandard)
        {
            // Tweak: Metal Doesn't Burn
            listingStandard.CheckboxLabeled("Metal Doesn't Burn", ref settings.tweak_metalDoesntBurn, "Removes flammability from steel and plasteel, setting gold and silver to just a scratch above 0 so they have a chance to melt away still.");
            listingStandard.GapLine();
            // Tweak: No Mineable Components
            listingStandard.CheckboxLabeled("No Mineable Components", ref settings.tweak_noMineableComponents, "Removes mineable components from map generation.");
            listingStandard.GapLine();
            // Tweak: No Mineable Plasteel
            listingStandard.CheckboxLabeled("No Mineable Plasteel", ref settings.tweak_noMineablePlasteel, "Removes mineable plasteel from map generation.");
            listingStandard.GapLine();
            // Tweak: Old Component Names
            listingStandard.CheckboxLabeled("Old Component Names", ref settings.tweak_oldComponentNames, "Back in my day we had industrial and spacer components! Now you can too!");
            listingStandard.GapLine();
            // Tweak: Pretty Precious Metals
            listingStandard.CheckboxLabeled("Pretty Precious Metals", ref settings.tweak_prettyPreciousMetals, "Changes the beauty of gold and silver (the item, not as a material) from -4 to 4.");
            listingStandard.GapLine();
            // Tweak: Stackable Chunks
            listingStandard.CheckboxLabeled("Stackable Chunks", ref settings.tweak_stackableChunks, "Allows stone chunks and slag to be stackable. Default: 5");
            if (settings.tweak_stackableChunks)
            {
                listingStandard.AddLabeledSlider("Stone Chunk Stack Size: " + settings.tweak_stackableChunks_stone, ref settings.tweak_stackableChunks_stone, 1f, 400f, "Min: 1", "Max: 400", 1f);
                listingStandard.AddLabeledSlider("Slag Chunk Stack Size: " + settings.tweak_stackableChunks_slag, ref settings.tweak_stackableChunks_slag, 1f, 400f, "Min: 1", "Max: 400", 1f);
            }
            listingStandard.GapLine();
            // Tweak: Stronger Steel
            listingStandard.CheckboxLabeled("Stronger Steel", ref settings.tweak_strongerSteel, "Doubles the durability of steel as a material.");
            listingStandard.GapLine();
        }

        public void DoSettings_Royalty(Listing_Standard listingStandard)
        {
            // Tweak: Delayed Royalty
            listingStandard.CheckboxLabeled("Delayed Royalty", ref settings.tweak_delayedRoyalty, "Delays the Royalty starter quests by a few days more than usual to give additional time to get ready for them.");
            listingStandard.GapLine();
            // Tweak: Replantable Anima
            listingStandard.CheckboxLabeled("Replantable Anima", ref settings.tweak_replantableAnima, "Makes it so you can move Anima trees like any other.");
            listingStandard.GapLine();
        }

        public void DoSettings_Ideology(Listing_Standard listingStandard)
        {
            // Tweak: Ancient Deconstruction
            listingStandard.CheckboxLabeled("Ancient Deconstruction", ref settings.tweak_ancientDeconstruction, "Makes the Ancient Ruins added by Ideology deconstructable instead of having to destroy them.");
            if (settings.tweak_ancientDeconstruction)
            {
                listingStandard.CheckboxEnhanced("Give Proper Materials", "Changes returned items to some reasonable materials instead of just slag.", ref settings.tweak_ancientDeconstruction_mode);
            }
            listingStandard.GapLine();
            // Tweak: Darklight Glow Pods
            listingStandard.CheckboxLabeled("Darklight Glow Pods", ref settings.tweak_darklightGlowPods, "Makes glow pods spawned in insectoid nests use the darklight colour.");
            listingStandard.GapLine();
            // Tweak: Disable Desired Apparel
            listingStandard.CheckboxLabeled("Disable Desired Apparel", ref settings.tweak_disableDesiredApparel, "Disables default Ideology desired apparel from showing up. I got really fucking sick of this happening when I was testing faction loadouts, whoever came up with this and DIDN'T think to have an option to disable it deserves to step on an upright UK appliance plug.");
            listingStandard.GapLine();
            // Tweak: No Meme Limit
            listingStandard.CheckboxLabeled("No Meme Limit", ref settings.patch_noMemeLimit, "Raises the limit of how many memes you can choose to 1000...so effectively no limit.");
            listingStandard.GapLine();
            // Tweak: Proper Suppression
            listingStandard.CheckboxLabeled("Proper Suppression", ref settings.patch_properSuppression, "Changes suppression slightly so that rebellions never happen if your slaves are kept suppressed.");
            listingStandard.GapLine();
            // Tweak: Replantable Gauranlen
            listingStandard.CheckboxLabeled("Replantable Gauranlen", ref settings.tweak_replantableGauranlen, "Makes it so you can move Gauranlen trees like any other.");
            listingStandard.GapLine();
            // Tweak: Unlocked Ideology Buildings
            listingStandard.CheckboxLabeled("Unlocked Ideology Buildings", ref settings.tweak_unlockedIdeologyBuildings, "Removes the meme restriction on ideology buildings so you can use them regardless of what memes you have. Includes floors and apparel.");
            listingStandard.GapLine();
        }

        public void DoSettings_Biotech(Listing_Standard listingStandard)
        {
            // Tweak: Delayed Royalty
            listingStandard.CheckboxLabeled("Delayed Royalty", ref settings.tweak_delayedRoyalty, "Delays the Royalty starter quests by a few days more than usual to give additional time to get ready for them.");
            listingStandard.GapLine();
            // Tweak: Replantable Anima
            listingStandard.CheckboxLabeled("Replantable Anima", ref settings.tweak_replantableAnima, "Makes it so you can move Anima trees like any other.");
            listingStandard.GapLine();
        }

        public Dictionary<string, TrainabilityDef> GetTrainabilityModes
        {
            get
            {
                if (TrainabilityRadioDict.NullOrEmpty())
                {
                    TrainabilityRadioDict.Add("None", TrainabilityDefOf.None);
                    TrainabilityRadioDict.Add("Intermediate", TrainabilityDefOf.Intermediate);
                    TrainabilityRadioDict.Add("Advanced", TrainabilityDefOf.Advanced);
                }

                return TrainabilityRadioDict;
            }
        }
    }

    public enum TweaksGaloreSettingsPage
    {
        General,
        Mechanoids,
        Power,
        Raids,
        Resources,
        Royalty,
        Ideology,
        Biotech
    }
}
