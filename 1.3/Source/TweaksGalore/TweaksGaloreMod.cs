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

            File.WriteAllText(VersionDir, CurrentVersion);

            new Harmony("neronix17.tweaksgalore.rimworld").PatchAll();
        }

        public override string SettingsCategory()
        {
            return "Tweaks Galore";
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            float secondStageHeight;
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            listingStandard.SettingsDropdown("Current Page", "", ref currentPage, inRect.width);
            listingStandard.Note("You will need to restart the game for most of these settings to take effect.", GameFont.Tiny);
            listingStandard.GapLine();
            listingStandard.Gap(48);
            secondStageHeight = listingStandard.CurHeight;
            listingStandard.End();

            inRect.yMin = secondStageHeight;
            Rect outRect = inRect.ContractedBy(10f);

            float scrollRectHeight = 3000f;
            if (currentPage == TweaksGaloreSettingsPage.General)
            {
                scrollRectHeight = 2400f;
            }
            else if (currentPage == TweaksGaloreSettingsPage.Mechanoids)
            {
                scrollRectHeight = 460f;
            }
            else if (currentPage == TweaksGaloreSettingsPage.Power)
            {
                scrollRectHeight = 460f;
            }
            else if (currentPage == TweaksGaloreSettingsPage.Raids)
            {
                scrollRectHeight = 460f;
            }
            else if (currentPage == TweaksGaloreSettingsPage.Resources)
            {
                scrollRectHeight = 800f;
            }
            else if(currentPage == TweaksGaloreSettingsPage.Royalty)
            {
                scrollRectHeight = 460f;
            }
            else if (currentPage == TweaksGaloreSettingsPage.Ideology)
            {
                scrollRectHeight = 600f;
            }
            else
            {
                scrollRectHeight = 3000f;
            }

            Rect rect = new Rect(0f, 0f, outRect.width - (scrollRectHeight > outRect.height ? 18f : 0), scrollRectHeight);
            Widgets.BeginScrollView(outRect, ref scrollPosition, rect, true);
            listingStandard = new Listing_Standard();
            listingStandard.Begin(rect);

            if (currentPage == TweaksGaloreSettingsPage.General)
            {
                DoSettings_Vanilla(listingStandard);
            }
            else if (currentPage == TweaksGaloreSettingsPage.Mechanoids)
            {
                DoSettings_Mechanoids(listingStandard);
            }
            else if(currentPage == TweaksGaloreSettingsPage.Power)
            {
                DoSettings_Power(listingStandard);
            }
            else if (currentPage == TweaksGaloreSettingsPage.Raids)
            {
                DoSettings_Raids(listingStandard);
            }
            else if (currentPage == TweaksGaloreSettingsPage.Resources)
            {
                DoSettings_Resources(listingStandard);
            }
            else if (currentPage == TweaksGaloreSettingsPage.Royalty)
            {
                if (!ModLister.RoyaltyInstalled)
                {
                    listingStandard.Note("Royalty is not installed. These options would do nothing for you.");
                }
                else
                {
                    DoSettings_Royalty(listingStandard);
                }
            }
            else if (currentPage == TweaksGaloreSettingsPage.Ideology)
            {
                if (!ModLister.IdeologyInstalled)
                {
                    listingStandard.Note("Ideology is not installed. These options would do nothing for you.");
                }
                else
                {
                    DoSettings_Ideology(listingStandard);
                }
            }
            listingStandard.End();
            Widgets.EndScrollView();

            base.DoSettingsWindowContents(inRect);
        }

        public void DoSettings_Vanilla(Listing_Standard listingStandard)
        {
            // Tweak: Boomalopes Bleed Chemfuel
            listingStandard.CheckboxEnhanced("Boomalopes Bleed Chemfuel", "Changes the blood def from boomalopes and boomrats to be chemfuel puddles instead.", ref settings.tweak_boomalopesBleedChemfuel);
            listingStandard.GapLine();
            // Tweak: Chatty Comms
            listingStandard.CheckboxEnhanced("Chatty Comms", "Adds a recreation type to comms consoles allowing them to be used by pawns to gain recreation through 'chatting'.", ref settings.tweak_chattyComms);
            listingStandard.GapLine();
            // Tweak: Destroy Anything
            listingStandard.CheckboxEnhanced("Destroy Anything", "Allows the smelting of pretty much anything anything in the crematorium aside from weapons, apparel and minified buildings. Be careful so you don't burn anything important.", ref settings.tweak_destroyAnything);
            listingStandard.GapLine();
            // Tweak: Don't Pack Food
            listingStandard.CheckboxEnhanced("Don't Pack Food", "Prevents pawns from stuffing food into their inventory to carry around.", ref settings.tweak_dontPackFood);
            listingStandard.GapLine();
            // Tweak: Faster Smoothing
            listingStandard.CheckboxEnhanced("Faster Smoothing", "Enables an additional stat to speed up smoothing of natural rock. Default: 300%", ref settings.tweak_fasterSmoothing);
            if (settings.tweak_fasterSmoothing)
            {
                listingStandard.AddLabeledSlider("Faster Smoothing Factor: " + settings.tweak_fasterSmoothing_factor.ToStringPercent(), ref settings.tweak_fasterSmoothing_factor, 0.1f, 10.0f, "Min: 10%", "Max: 1000%", 0.1f);
            }
            listingStandard.GapLine();
            // Tweak: Full Deconstruction Return
            listingStandard.CheckboxEnhanced("Full Deconstruct Return", "Returns the full amount it takes to build something. No more punishment for moving a building that isn't uninstallable.", ref settings.tweak_fixDeconstructionReturn);
            listingStandard.GapLine();
            // Tweak: Glowing Ambrosia
            listingStandard.CheckboxEnhanced("Glowing Ambrosia", "Gives Ambrosia a slight glow around it, making natural spawning Ambrosia easier to find on a map in the dark.", ref settings.tweak_glowingAmbrosia);
            listingStandard.GapLine();
            // Tweak: Glowing Healroot
            string healrootName = (settings.tweak_healrootToXerigium ? "Xerigium" : "Healroot");
            listingStandard.CheckboxEnhanced("Glowing " + healrootName, "Gives " + healrootName + " a slight glow around it, making natural " + healrootName + " easier to find on a map in the dark.", ref settings.tweak_glowingHealroot);
            listingStandard.GapLine();
            // Tweak: Healroot to Xerigium
            listingStandard.CheckboxEnhanced("Healroot to Xerigium", "Reverts the old name change of the herbal medicine plant Healroot back to Xerigium like it used to be in older game versions.", ref settings.tweak_healrootToXerigium);
            listingStandard.GapLine();
            // Tweak: Hunters Can Use Melee
            listingStandard.CheckboxEnhanced("Hunters Can Use Melee", "Allows hunters to use melee, regardless of how well they can do so.", ref settings.tweak_hunterMelee);
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
            listingStandard.CheckboxEnhanced("Incident Pawn Stats", "Displays the information of any pawns rewarded as part of incidents.", ref settings.patch_incidentPawnStats);
            listingStandard.GapLine();
            // Tweak: Infestation Blocking Floors
            listingStandard.CheckboxEnhanced("Infestation Blocking Floors", "Prevents infestations happening on harder floors (Steel, Stone, Concrete). Only prevents the event picking that spot, hives can still spawn on them if an event happens closeby.", ref settings.patch_strongFloorsStopInfestations);
            listingStandard.GapLine();
            // Tweak: Insulting Spree Nerf
            listingStandard.CheckboxEnhanced("Insulting Spree Nerf", "Makes the Insulting Spree mental break less annoying to deal with." +
                "\n- Intensity: Minor --> Major" +
                "\n- Max Duration: 45,000 ticks --> 20,000 ticks" +
                "\n- Min Duration: 25,000 ticks --> 2,500 ticks" +
                "\n- Base Event Chance: 0.5 --> 0.15", ref settings.tweak_insultingSpreeNerf);
            listingStandard.GapLine();
            // Tweak: Impassable Deep Water
            listingStandard.CheckboxEnhanced("Impassable Deep Water", "Changes deep water to be impassable by pawns, preventing them from pathing through it.", ref settings.tweak_impassableDeepWater);
            listingStandard.GapLine();
            // Tweak: Lag free Lamps
            listingStandard.CheckboxEnhanced("Lag Free Lamps", "Removes the fuel comp from fuelled lamps, so they now no longer run code constantly they impact performance that little bit less.", ref settings.tweak_lagFreeLamps);
            listingStandard.GapLine();
            // Tweak: Megasloth to Megatherium
            listingStandard.CheckboxEnhanced("Megasloth to Megatherium", "Reverts the name change of the Megatherium so it retains the obviously cooler name.", ref settings.tweak_oldMegaslothName);
            listingStandard.GapLine();
            // Tweak: Misanthrope Trait
            listingStandard.CheckboxEnhanced("Misanthrope Trait", "Adds the misanthrope trait (dislikes humans), also makes it so if a pawn was going to spawn with both the misandrist and misogynist traits that they instead get the misanthrope trait.", ref settings.tweak_misanthropeTrait);
            listingStandard.GapLine();
            // Tweak: Next Restock Timer
            listingStandard.CheckboxEnhanced("Next Restock Timer", "Displays in the world map information of settlements whether or not they have restocked since you last visited and how long till their next restock.", ref settings.patch_settlementTraderTimer);
            listingStandard.GapLine();
            // Tweak: No Breakdowns
            if(ModLister.GetActiveModWithIdentifier("kentington.saveourship2") == null)
            {
                listingStandard.CheckboxEnhanced("No Breakdowns", "Removes the breakdown comp from anything that has it, artificially enforced resource sinks are LAZY.", ref settings.tweak_noBreakdowns);
                listingStandard.GapLine();
            }
            // Tweak: No Friend Shaped Manhunters
            listingStandard.CheckboxEnhanced("No Friend Shaped Manhunters", "Sets parameters which decide if an animal can arrive as manhunter or not. This does not prevent it happening if they take damage.", ref settings.tweak_noFriendShapedManhunters);
            if (settings.tweak_noFriendShapedManhunters)
            {
                listingStandard.AddLabelLine("Prevent by Trainability");
                listingStandard.CheckboxLabeled("Intermediate", ref settings.tweak_NFSMTrainability_Intermediate);
                listingStandard.CheckboxLabeled("Advanced", ref settings.tweak_NFSMTrainability_Advanced);
                listingStandard.Gap(24f);
                listingStandard.CheckboxLabeled("Prevent if Nuzzle-able", ref settings.tweak_NFSMNuzzleHours);
                listingStandard.AddLabeledSlider("Prevent if Wildness Below: " + settings.tweak_NFSMWildness.ToStringPercent(), ref settings.tweak_NFSMWildness, 0f, 1f, "Min: 0%", "Max: 100%", 0.01f);
                listingStandard.AddLabeledSlider("Prevent if Combat Power Below: " + settings.tweak_NFSMCombatPower, ref settings.tweak_NFSMCombatPower, 0f, 200f, "Min: 0", "Max: 200", 1f);
                listingStandard.CheckboxEnhanced("Disable Manhunter on Tame Fail", "Uses the same parameters to prevent animals going manhunter on tame fail too, they can still go manhunter if damaged.", ref settings.tweak_NFSMDisableManhunterOnTame);
            }
            listingStandard.GapLine();
            // Tweak: Not So Wild Berries
            listingStandard.CheckboxEnhanced("Not So Wild Berries", "Adds the ground sowing tag to wild berries so they can be planted.", ref settings.tweak_notSoWildBerries);
            listingStandard.GapLine();
            // Tweak: Prisoners Don't Have Keys
            listingStandard.CheckboxEnhanced("Prisoners Don't Have Keys", "Selectively controls whether prisoners and slaves can open doors during breakout and rebellion.", ref settings.patch_prisonersDontHaveKeys);
            if (settings.patch_prisonersDontHaveKeys)
            {
                listingStandard.CheckboxLabeled("Applies to Prisoners", ref settings.patch_pdhk_prisoners);
                listingStandard.CheckboxLabeled("Applies to Slaves", ref settings.patch_pdhk_slaves);
                listingStandard.CheckboxLabeled("Escaping pawns can open their own door", ref settings.patch_pdhk_ownDoor);
            }
            listingStandard.GapLine();
            // Tweak: Skilled Stonecutting
            listingStandard.CheckboxEnhanced("Skilled Stonecutting", "Makes stonecutting give crafting skill increases, and makes more skilled crafters create blocks quickler.", ref settings.tweak_skilledStonecutting);
            listingStandard.GapLine();
            // Tweak: Skill Rates
            listingStandard.CheckboxEnhanced("Skill Rates Adjustment", "Enables more control over skill gain/loss rates and what levels that should happen at.", ref settings.tweak_skillRates);
            if (settings.tweak_skillRates)
            {
                listingStandard.AddLabeledSlider("Skill Loss Multiplier: " + settings.tweak_skillRateLoss.ToStringPercent(), ref settings.tweak_skillRateLoss, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listingStandard.AddLabeledSlider("Skill Gain Multiplier: " + settings.tweak_skillRateGain.ToStringPercent(), ref settings.tweak_skillRateGain, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
                listingStandard.AddLabeledSlider("Skill Loss Threshold: " + settings.tweak_skillRateLossThreshold.ToString(), ref settings.tweak_skillRateLossThreshold, 0f, 20f, "Min: 0", "Max: 20", 1f);
                listingStandard.Note("Skill loss threshold is the level a skill has to reach before skill loss starts happening, it will prevent skill loss entirely below that level.", GameFont.Tiny);
            }
            listingStandard.GapLine();
            // Tweak: Slim Rim
            listingStandard.CheckboxEnhanced("Slim Rim", "Allows the disabling of body types on pawn generation. Does this during generation of the pawn so will not affect existing ones. The replacement body will be either Male or Female, whichever matches the pawns gender.", ref settings.patch_slimRim);
            if (settings.patch_slimRim)
            {
                listingStandard.CheckboxLabeled("Fat", ref settings.patch_slimRim_fat);
                listingStandard.CheckboxLabeled("Hulk", ref settings.patch_slimRim_hulk);
                listingStandard.CheckboxLabeled("Thin", ref settings.patch_slimRim_thin);
            }
            listingStandard.GapLine();
            // Tweak: Trait Count Adjustment
            listingStandard.CheckboxEnhanced("Trait Count Adjustment", "There may be cases where this cannot apply, such as modded alien races where they have been set up to have a specific trait count.", ref settings.tweak_traitCountAdjustment);
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
            listingStandard.CheckboxEnhanced("Disable Adapting", "Disables the ability for Mechanoids to adapt to EMP.", ref settings.patch_disableMechanoidAdapting);
            listingStandard.GapLine();
            // Tweak: Heat Armor
            listingStandard.AddLabeledSlider("Mechanoid Heat Armour: " + settings.tweak_mechanoidHeatArmour.ToStringPercent(), ref settings.tweak_mechanoidHeatArmour, 0f, 2f, "Min: 0%", "Max: 200%", 0.01f);
            listingStandard.GapLine();
            // Tweak: Pre-1.0 Ship Parts
            listingStandard.CheckboxEnhanced("Pre-1.0 Ship Parts", "Returns psychic and defoliator ship parts to their B18 state where they actually provide a decent reward." +
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
            listingStandard.GapLine();
        }

        public void DoSettings_Power(Listing_Standard listingStandard)
        {
            // Tweak: Power Tweaks
            listingStandard.CheckboxEnhanced("Power Usage Tweaks", "Allows tweaking the usage rate of some buildings. If they usually produce then it configures how much they output instead.", ref settings.tweak_powerUsageTweaks);
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
            listingStandard.CheckboxEnhanced("No More Breach Raids", "Removes Breach Raids as an option for raiders.", ref settings.tweak_noMoreBreachRaids);
            listingStandard.GapLine();
            // Tweak: No More Breach Raids
            listingStandard.CheckboxEnhanced("No More Drop Pod Raids", "Removes Drop Pod Raids as an option for raiders. Does not work with RimWar.", ref settings.tweak_noMoreDropPodRaids);
            listingStandard.GapLine();
            // Tweak: No More Breach Raids
            listingStandard.CheckboxEnhanced("No More Sapper Raids", "Removes Sapper Raids as an option for raiders.", ref settings.tweak_noMoreSapperRaids);
            listingStandard.GapLine();
            // Tweak: No More Breach Raids
            listingStandard.CheckboxEnhanced("No More Siege Raids", "Removes Siege Raids as an option for raiders.", ref settings.tweak_noMoreSiegeRaids);
            listingStandard.GapLine();
        }

        public void DoSettings_Resources(Listing_Standard listingStandard)
        {
            // Tweak: Metal Doesn't Burn
            listingStandard.CheckboxEnhanced("Metal Doesn't Burn", "Removes flammability from steel and plasteel, setting gold and silver to just a scratch above 0 so they have a chance to melt away still.", ref settings.tweak_metalDoesntBurn);
            listingStandard.GapLine();
            // Tweak: No Mineable Components
            listingStandard.CheckboxEnhanced("No Mineable Components", "Removes mineable components from map generation.", ref settings.tweak_noMineableComponents);
            listingStandard.GapLine();
            // Tweak: No Mineable Plasteel
            listingStandard.CheckboxEnhanced("No Mineable Plasteel", "Removes mineable plasteel from map generation.", ref settings.tweak_noMineablePlasteel);
            listingStandard.GapLine();
            // Tweak: Old Component Names
            listingStandard.CheckboxEnhanced("Old Component Names", "Back in my day we had industrial and spacer components! Now you can too!", ref settings.tweak_oldComponentNames);
            listingStandard.GapLine();
            // Tweak: Pretty Precious Metals
            listingStandard.CheckboxEnhanced("Pretty Precious Metals", "Changes the beauty of gold and silver (the item, not as a material) from -4 to 4.", ref settings.tweak_prettyPreciousMetals);
            listingStandard.GapLine();
            // Tweak: Stackable Chunks
            listingStandard.CheckboxEnhanced("Stackable Chunks", "Allows stone chunks and slag to be stackable. Default: 5", ref settings.tweak_stackableChunks);
            if (settings.tweak_stackableChunks)
            {
                listingStandard.AddLabeledSlider("Stone Chunk Stack Size: " + settings.tweak_stackableChunks_stone, ref settings.tweak_stackableChunks_stone, 1f, 400f, "Min: 1", "Max: 400", 1f);
                listingStandard.AddLabeledSlider("Slag Chunk Stack Size: " + settings.tweak_stackableChunks_slag, ref settings.tweak_stackableChunks_slag, 1f, 400f, "Min: 1", "Max: 400", 1f);
            }
            listingStandard.GapLine();
            // Tweak: Stronger Steel
            listingStandard.CheckboxEnhanced("Stronger Steel", "Doubles the durability of steel as a material.", ref settings.tweak_strongerSteel);
            listingStandard.GapLine();
        }

        public void DoSettings_Royalty(Listing_Standard listingStandard)
        {
            // Tweak: Delayed Royalty
            listingStandard.CheckboxEnhanced("Delayed Royalty", "Delays the Royalty starter quests by a few days more than usual to give additional time to get ready for them.", ref settings.tweak_delayedRoyalty);
            listingStandard.GapLine();
            // Tweak: Replantable Anima
            listingStandard.CheckboxEnhanced("Replantable Anima", "Makes it so you can move Anima trees like any other.", ref settings.tweak_replantableAnima);
            listingStandard.GapLine();
        }

        public void DoSettings_Ideology(Listing_Standard listingStandard)
        {
            // Tweak: Ancient Deconstruction
            listingStandard.CheckboxEnhanced("Ancient Deconstruction", "Makes the Ancient Ruins added by Ideology deconstructable instead of having to destroy them.", ref settings.tweak_ancientDeconstruction);
            if (settings.tweak_ancientDeconstruction)
            {
                listingStandard.CheckboxEnhanced("Give Proper Materials", "Changes returned items to some reasonable materials instead of just slag.", ref settings.tweak_ancientDeconstruction_mode);
            }
            listingStandard.GapLine();
            // Tweak: Darklight Glow Pods
            listingStandard.CheckboxEnhanced("Darklight Glow Pods", "Makes glow pods spawned in insectoid nests use the darklight colour.", ref settings.tweak_darklightGlowPods);
            listingStandard.GapLine();
            // Tweak: Disable Desired Apparel
            listingStandard.CheckboxEnhanced("Disable Desired Apparel", "Disables default Ideology desired apparel from showing up. I got really fucking sick of this happening when I was testing faction loadouts, whoever came up with this and DIDN'T think to have an option to disable it deserves to step on an upright UK appliance plug.", ref settings.tweak_disableDesiredApparel);
            listingStandard.GapLine();
            // Tweak: No Meme Limit
            listingStandard.CheckboxEnhanced("No Meme Limit", "Raises the limit of how many memes you can choose to 1000...so effectively no limit.", ref settings.patch_noMemeLimit);
            listingStandard.GapLine();
            // Tweak: Proper Suppression
            listingStandard.CheckboxEnhanced("Proper Suppression", "Changes suppression slightly so that rebellions never happen if your slaves are kept suppressed.", ref settings.patch_properSuppression);
            listingStandard.GapLine();
            // Tweak: Replantable Gauranlen
            listingStandard.CheckboxEnhanced("Replantable Gauranlen", "Makes it so you can move Gauranlen trees like any other.", ref settings.tweak_replantableGauranlen);
            listingStandard.GapLine();
            // Tweak: Unlocked Ideology Buildings
            listingStandard.CheckboxEnhanced("Unlocked Ideology Buildings", "Removes the meme restriction on ideology buildings so you can use them regardless of what memes you have. Includes floors and apparel.", ref settings.tweak_unlockedIdeologyBuildings);
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
        Ideology
    }
}
