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
    public static class SettingsPage_Defaults
    {
        public static TweaksGaloreSettings settings => TweaksGaloreMod.settings;

        public static void DoSettings_Defaults(Listing_Standard listing)
        {
            listing.Note("These buttons restore the default values for a given category, there is no confirmation box so be sure you want to before you click. Restored categories will not be accessible until you have relaunched the game.");
            if (listing.ButtonText("General"))
            {
                DefaultUtil.RestoreSettings_Vanilla(settings);
                Messages.Message("Tweaks Galore: 'General' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreGeneral = true;
            }
            if (listing.ButtonText("Mechanoids"))
            {
                DefaultUtil.RestoreSettings_Mechanoids(settings);
                Messages.Message("Tweaks Galore: 'Mechanoids' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreMechanoids = true;
            }
            if (listing.ButtonText("Penned Animals"))
            {
                DefaultUtil.RestoreSettings_PennedAnimals(settings);
                Messages.Message("Tweaks Galore: 'Penned Animals' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restorePennedAnimals = true;
            }
            if (listing.ButtonText("Power"))
            {
                DefaultUtil.RestoreSettings_Power(settings);
                Messages.Message("Tweaks Galore: 'Power' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restorePower = true;
            }
            if (listing.ButtonText("Raids"))
            {
                DefaultUtil.RestoreSettings_Raids(settings);
                Messages.Message("Tweaks Galore: 'Raids' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreRaids = true;
            }
            if (listing.ButtonText("Resources"))
            {
                DefaultUtil.RestoreSettings_Resources(settings);
                Messages.Message("Tweaks Galore: 'Resources' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreResources = true;
            }
            if (listing.ButtonText("Royalty"))
            {
                DefaultUtil.RestoreSettings_Royalty(settings);
                Messages.Message("Tweaks Galore: 'Royalty' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreRoyalty = true;
            }
            if (listing.ButtonText("Royal Titles"))
            {
                DefaultUtil.RestoreSettings_RoyalTitles(settings);
                Messages.Message("Tweaks Galore: 'Royal Titles' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreRoyalTitles = true;
            }
            if (listing.ButtonText("Royal Permits"))
            {
                DefaultUtil.RestoreSettings_RoyalTitles(settings);
                Messages.Message("Tweaks Galore: 'Royal Permits' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreRoyalPermits = true;
            }
            if (listing.ButtonText("Anima"))
            {
                DefaultUtil.RestoreSettings_Anima(settings);
                Messages.Message("Tweaks Galore: 'Anima' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreAnima = true;
            }
            if (listing.ButtonText("Ideology"))
            {
                DefaultUtil.RestoreSettings_Ideology(settings);
                Messages.Message("Tweaks Galore: 'Ideology' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreIdeology = true;
            }
            if (listing.ButtonText("Gauranlen"))
            {
                DefaultUtil.RestoreSettings_Gauranlen(settings);
                Messages.Message("Tweaks Galore: 'Gauranlen' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreGauranlen = true;
            }
            if (listing.ButtonText("Biotech"))
            {
                DefaultUtil.RestoreSettings_Biotech(settings);
                Messages.Message("Tweaks Galore: 'Biotech' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreBiotech = true;
            }
            if (listing.ButtonText("Polux"))
            {
                DefaultUtil.RestoreSettings_Polux(settings);
                Messages.Message("Tweaks Galore: 'Polux' tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restorePolux = true;
            }
            if (listing.ButtonText("RESTORE ALL"))
            {
                DefaultUtil.RestoreALL(settings);
                Messages.Message("Tweaks Galore: ALL tweaks restored to defaults.", MessageTypeDefOf.CautionInput);
                TweaksGaloreMod.mod.restoreGeneral = true;
                TweaksGaloreMod.mod.restoreMechanoids = true;
                TweaksGaloreMod.mod.restorePennedAnimals = true;
                TweaksGaloreMod.mod.restorePower = true;
                TweaksGaloreMod.mod.restoreRaids = true;
                TweaksGaloreMod.mod.restoreResources = true;
                TweaksGaloreMod.mod.restoreRoyalty = true;
                TweaksGaloreMod.mod.restoreAnima = true;
                TweaksGaloreMod.mod.restoreRoyalTitles = true;
                TweaksGaloreMod.mod.restoreRoyalPermits = true;
                TweaksGaloreMod.mod.restoreIdeology = true;
                TweaksGaloreMod.mod.restoreGauranlen = true;
                TweaksGaloreMod.mod.restoreBiotech = true;
                TweaksGaloreMod.mod.restorePolux = true;
            }
        }
    }
}
