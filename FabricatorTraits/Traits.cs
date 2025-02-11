using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using Obeliskial_Content;
using UnityEngine;
using static Fabricator.CustomFunctions;
using static Fabricator.Plugin;
using System.Text.RegularExpressions;

namespace Fabricator
{
    [HarmonyPatch]
    internal class Traits
    {
        // list of your trait IDs
        // public static string heroName = "<heroName>";

        // public static string subclassname = "<subclassname>";

        public static string[] simpleTraitList = ["fabricatortrait0", "fabricatortrait1a", "fabricatortrait1b", "fabricatortrait2a", "fabricatortrait2b", "fabricatortrait3a", "fabricatortrait3b", "fabricatortrait4a", "fabricatortrait4b"];

        public static string[] myTraitList = simpleTraitList; //(string[])simpleTraitList.Select(trait => heroName + trait); // Needs testing

        static string trait0 = myTraitList[0];
        static string trait2a = myTraitList[3];
        static string trait2b = myTraitList[4];
        static string trait4a = myTraitList[7];
        static string trait4b = myTraitList[8];

        static bool BeginTurnFlag = false;


        // public static string debugBase = "Binbin - Testing " + characterName + " ";

        public static void DoCustomTrait(string _trait, ref Trait __instance)
        {
            // get info you may need
            Enums.EventActivation _theEvent = Traverse.Create(__instance).Field("theEvent").GetValue<Enums.EventActivation>();
            Character _character = Traverse.Create(__instance).Field("character").GetValue<Character>();
            Character _target = Traverse.Create(__instance).Field("target").GetValue<Character>();
            int _auxInt = Traverse.Create(__instance).Field("auxInt").GetValue<int>();
            string _auxString = Traverse.Create(__instance).Field("auxString").GetValue<string>();
            CardData _castedCard = Traverse.Create(__instance).Field("castedCard").GetValue<CardData>();
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            TraitData traitData = Globals.Instance.GetTraitData(_trait);
            List<CardData> cardDataList = [];
            List<string> heroHand = MatchManager.Instance.GetHeroHand(_character.HeroIndex);
            Hero[] teamHero = MatchManager.Instance.GetTeamHero();
            NPC[] teamNpc = MatchManager.Instance.GetTeamNPC();

            if (!IsLivingHero(_character)||MatchManager.Instance == null)
            {
                return;
            }

            if (_trait == trait0)
            { // When you play an Enchantment on a hero grant 1 Inspire and 1 Energize
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                if (_castedCard != null)
                {
                    LogDebug($"Casting {_castedCard.Id}");
                }

                if (_castedCard != null && _castedCard.HasCardType(Enums.CardType.Enchantment))
                {
                    LogDebug($"Executing Trait {traitId}: {traitName}");
                    if (MatchManager.Instance == null) { return; }

                    Transform targetTransform = Traverse.Create(MatchManager.Instance).Field("targetTransform").GetValue<Transform>(); ;
                    if (targetTransform == null) { LogDebug("null transform"); return; }

                    Hero targetHero = MatchManager.Instance.GetHeroById(targetTransform.name);
                    if (!IsLivingHero(targetHero)) { return; }

                    targetHero.SetAura(_character, GetAuraCurseData("inspire"), 1, useCharacterMods: false);
                    targetHero.SetAura(_character, GetAuraCurseData("energize"), 1, useCharacterMods: false);
                    // DisplayTraitScroll(ref _character, traitData);

                }
            }


            else if (_trait == trait2a)
            { // Effects on this hero that occur at the start of turn happen twice.
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                // BeginTurnFlag = !BeginTurnFlag;
                BeginTurnFlag = true;
                if (BeginTurnFlag)
                {
                    LogDebug($"Executing Trait {traitId}: {traitName}");
                    // _character.SetEvent(Enums.EventActivation.BeginTurn);

                    LogDebug($"Activating items for {traitId}: {traitName} for character {_character.SubclassName}");
                    MatchManager.Instance.IsBeginTournPhase = true;
                    _character.ActivateItem(Enums.EventActivation.BeginTurn, null, 0, "");
                    _character.ActivateItem(Enums.EventActivation.BeginTurnCardsDealt, null, 0, "");
                    _character.ActivateItem(Enums.EventActivation.BeginTurnAboutToDealCards, null, 0, "");
                    LogDebug($"Post Activating items ");
                    // LogDebug($"Activating traits for {traitId}: {traitName}");
                    // _character.ActivateTrait(Enums.EventActivation.BeginTurn,null,0,"");
                    // DisplayTraitScroll(ref _character, traitData);
                }
            }



            else if (_trait == trait2b)
            {   // Taunt +1. Handled in json.
                // Taunt on this hero can stack. -- handled in GACM
                // This hero gains 5% more Block and Shield for each stack of Taunt.
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                if (_auxInt >= 0 && (_auxString == "shield" || _auxString == "block"))
                {
                    LogDebug($"Executing Trait {traitId}: {traitName}");
                    AuraCurseData shieldOrBlock = GetAuraCurseData(_auxString);
                    int bonusCharges = Mathf.RoundToInt(_auxInt * 0.05f * _character.GetAuraCharges("taunt"));
                    _character.SetAura(_character, shieldOrBlock, bonusCharges, useCharacterMods: false);

                }
                // DisplayTraitScroll(ref _character, traitData);

            }

            else if (_trait == trait4a)
            { // trait4a: Whenever a hero plays an Enchantment, shuffle into your deck a copy of it that costs 2 more and can target any hero. 
                // handled in SetEvent
                string traitName = traitData.TraitName;
                string traitId = _trait;
                // LogDebug($"Handling Trait {traitId}: {traitName}");

            }

            else if (_trait == trait4b)
            { // At end of turn, all heroes gain 10 Block and Shield for every Taunt you have.
                string traitName = traitData.TraitName;
                string traitId = _trait;
                LogDebug($"Handling Trait {traitId}: {traitName}");
                int nToApply = 10 * _character.GetAuraCharges("taunt");
                ApplyAuraCurseToAll("block", nToApply, AppliesTo.Heroes, _character, useCharacterMods: true);
                ApplyAuraCurseToAll("shield", nToApply, AppliesTo.Heroes, _character, useCharacterMods: true);
                // DisplayTraitScroll(ref _character, traitData);

            }

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Trait), "DoTrait")]
        public static bool DoTrait(Enums.EventActivation _theEvent, string _trait, Character _character, Character _target, int _auxInt, string _auxString, CardData _castedCard, ref Trait __instance)
        {
            if ((UnityEngine.Object)MatchManager.Instance == (UnityEngine.Object)null)
                return false;
            Traverse.Create(__instance).Field("character").SetValue(_character);
            Traverse.Create(__instance).Field("target").SetValue(_target);
            Traverse.Create(__instance).Field("theEvent").SetValue(_theEvent);
            Traverse.Create(__instance).Field("auxInt").SetValue(_auxInt);
            Traverse.Create(__instance).Field("auxString").SetValue(_auxString);
            Traverse.Create(__instance).Field("castedCard").SetValue(_castedCard);
            if (Content.medsCustomTraitsSource.Contains(_trait) && myTraitList.Contains(_trait))
            {
                DoCustomTrait(_trait, ref __instance);
                return false;
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), "SetEvent")]
        public static void SetEventPostfix(ref Character __instance, ref Enums.EventActivation theEvent, Character target = null)
        {
            // trait4a: Whenever a hero plays an Enchantment, shuffle into your deck a copy of it that costs 2 more and can target any hero. 
            if (AtOManager.Instance == null || MatchManager.Instance == null) { return; }
            string traitOfInterest = trait4a;

            if (theEvent == Enums.EventActivation.CastCard && IsLivingHero(__instance) && AtOManager.Instance.TeamHaveTrait(traitOfInterest))
            {
                LogDebug($"Handling Trait4a {__instance.SourceName}");

                if (MatchManager.Instance.GetHeroHeroActive().HaveTrait(traitOfInterest)) { LogDebug("Fabricator Casting Card"); return; }

                CardData _cardActive = Traverse.Create(MatchManager.Instance).Field("cardActive").GetValue<CardData>();
                if (_cardActive != null) { LogDebug($"Active card: {_cardActive.CardName}"); }
                if (_cardActive == null || !_cardActive.HasCardType(Enums.CardType.Enchantment)) { LogDebug("Null Card"); return; }
                if (_cardActive.TargetSide == Enums.CardTargetSide.Enemy) {return;}
                LogDebug("Executing Trait4a");

                Hero[] teamHero = MatchManager.Instance.GetTeamHero();
                int heroIndex = 0;
                for (int i = 0; i < teamHero.Length; i++)
                {
                    Hero hero = teamHero[i];
                    if (IsLivingHero(hero) && hero.HaveTrait(trait4a))
                    {
                        heroIndex = i;
                    }
                }


                // CardData cardData = MatchManager.Instance.GetCardData(text);
                string text = MatchManager.Instance.CreateCardInDictionary(_cardActive.Id + "test");
                CardData cardData = _cardActive;
                cardData.Id = text;
                cardData.InternalId = text;
                cardData.Vanish = true;
                cardData.TargetSide = Enums.CardTargetSide.Friend;
                cardData.TargetType = Enums.CardTargetType.Single;
                cardData.TargetPosition = Enums.CardTargetPosition.Anywhere;
                cardData.EnergyReductionPermanent = -2;

                MatchManager.Instance.GenerateNewCard(1, text, true, Enums.CardPlace.RandomDeck, heroIndex: heroIndex, cardDataForModification: cardData, copyDataFromThisCard: cardData);
                MatchManager.Instance.CreateLogCardModification(cardData.InternalId, MatchManager.Instance.GetHero(heroIndex));

            }


        }




        [HarmonyPostfix]
        [HarmonyPatch(typeof(AtOManager), "GlobalAuraCurseModificationByTraitsAndItems")]
        public static void GlobalAuraCurseModificationByTraitsAndItemsPostfix(ref AtOManager __instance, ref AuraCurseData __result, string _type, string _acId, Character _characterCaster, Character _characterTarget)
        {
            LogInfo($"GACM {subclassName}");

            Character characterOfInterest = _type == "set" ? _characterTarget : _characterCaster;
            string traitOfInterest;

            switch (_acId)
            {
                case "taunt":
                    traitOfInterest = trait2b;
                    // trait2b: Taunt +1. Taunt on this hero can stack. This hero gains 5% more Block and Shield for each stack of Taunt.
                    if (IfCharacterHas(characterOfInterest, CharacterHas.Trait, traitOfInterest, AppliesTo.ThisHero))
                    {
                        LogDebug("Executing Trait2b GACM");
                        __result.GainCharges = true;
                    }
                    break;
            }

        }



    }
}
