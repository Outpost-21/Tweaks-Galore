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
    public static class SettingsPage_General
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Vanilla(Listing_Standard listing)
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
            // Tweak: Disable Narrow Heads
            listing.CheckboxEnhanced("Disable Narrow Heads", "Removes the narrow head shapes, they do not fit many of even the vanilla hair and beard styles properly and thanks to 1.4 making them defs it is very easy to just YEET.", ref settings.tweak_disableNarrowHeads);
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
            // Tweak: Growable Ambrosia
            listing.CheckboxEnhanced("Growable Ambrosia", "Allows the growing of Ambrosia in plant zones.", ref settings.tweak_growableAmbrosia);
            listing.GapLine();
            // Tweak: Growable Grass
            listing.CheckboxEnhanced("Growable Grass", "Allows the growing of grass and tall grass in plant zones. If you have Biotech you can also grow grey grass from that.", ref settings.tweak_growableGrass);
            listing.GapLine();
            // Tweak: Growable Mushrooms
            listing.CheckboxEnhanced("Growable Mushrooms", "Allows the growing of Glowstool and Agarilux in plant zones with sensible adjustments to the rates and yield to be more reasonable.", ref settings.tweak_growableMushrooms);
            listing.GapLine();
            // Tweak: Healroot to Xerigium
            listing.CheckboxEnhanced("Healroot to Xerigium", "Reverts the old name change of the herbal medicine plant Healroot back to Xerigium like it used to be in older game versions.", ref settings.tweak_healrootToXerigium);
            listing.GapLine();
            // Tweak: Hidden Cables
            listing.CheckboxEnhanced("Hidden Wires", "Visually hides the ugly wires that connect to the power conduits from buildings that don't specifically need to be placed on top of conduits.", ref settings.tweak_hiddenConduits);
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
            // Tweak: Lower Prisoner Expectations
            listing.CheckboxEnhanced("Lower Prisoner Expectations", "Brings prisoner expectations down to a more reasonable 'low expectations' level. They just tried to kill your colony most of the time, they don't deserve to have expectations.", ref settings.patch_lowPrisonerExpectations);
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
            listing.CheckboxEnhanced("Trait Count Adjustment", "As of 1.4 this isn't fully functional, changes to the way traits generate made it so the first 1-3 can always generate like normal, regardless of what I do. So this is only usefuly currently for those who want to guarantee more than 1 or have more in general. Won't affect newborns.", ref settings.tweak_traitCountAdjustment);
            if (settings.tweak_traitCountAdjustment)
            {
                listing.Label("- Trait Count Range");
                listing.Note($"Current: {settings.tweak_traitCountRange.min}-{settings.tweak_traitCountRange.max}  Min: 1  Max: 8", GameFont.Tiny);
                listing.IntRange(ref settings.tweak_traitCountRange, 1, 8);
            }
            listing.GapLine();
        }
    }
}
