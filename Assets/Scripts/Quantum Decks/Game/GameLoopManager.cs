﻿using System.Collections;
using System.Linq;
using Networking;
using Quantum_Decks.Card_System;
using Quantum_Decks.Environment;
using Shared;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using UnityEngine;
using NetworkPlayer = Networking.NetworkPlayer;

namespace Quantum_Decks.Game
{
    public class GameLoopManager : MonoBehaviour
    {
        [Required, SerializeField, BoxGroup("References")]
        private BoolReference _isGameOver;

        [Required, SerializeField, BoxGroup("References")]
        private PlayerCollection _playerCollection;

        [Required, SerializeField, BoxGroup("References")]
        private CardCollection _voidDeck;

        [Required, SerializeField, BoxGroup("References")]
        private PlayerCardDataCollection _allPlayerCardData;

        private EnvironmentDeck _environmentDeck;

        [SerializeField, Required, BoxGroup("Trigger")]
        private EffectTrigger _defenseTrigger;

        [SerializeField, Required, BoxGroup("Trigger")]
        private EffectTrigger _revengeTrigger;

        [SerializeField, Required, BoxGroup("Trigger")]
        private EffectTrigger _lostTrigger;

        [SerializeField, Required, BoxGroup("Trigger")]
        private EffectTrigger _attackTrigger;

        [SerializeField, Required, BoxGroup("Trigger")]
        private EffectTrigger _ambushTrigger;

        [SerializeField, Required, BoxGroup("Fraction")]
        private Fraction _fractionLess;


        [SerializeField] private BoolReference _isSurge;

        [SerializeField, Required, BoxGroup("Keywords")]
        private Keyword _powerSurge;

        [SerializeField, Required, BoxGroup("Keywords")]
        private Keyword _shielded;

        [SerializeField, Required, BoxGroup("Keywords")]
        private Keyword _elusive;

        private Coroutine _gameLoopRoutine;

        [SerializeField, Required] private NetworkSettingReference _networkSettingReference;


        private void Start()
        {
            QuantumNetworkManager.OnHandChanged.AddListener(CardDrawOnline);
            _environmentDeck = FindObjectOfType<EnvironmentDeck>();
            StartCoroutine(GameLoop());
        }

        private void OnDestroy()
        {
            QuantumNetworkManager.OnHandChanged.RemoveListener(CardDrawOnline);
            StopAllCoroutines();
        }

        private IEnumerator GameLoop()
        {
            yield return DeckPreparationPhase();
            _isGameOver.Value = false;

            while (!_isGameOver.Value)
            {
                Debug.Log("Ambush Phase");
                foreach (var player in _playerCollection.Value)
                {
                    yield return AmbushPhase(player, _environmentDeck.GetByPlayer(player.PlayerId));
                    yield return new WaitForSeconds(2);
                }

                Debug.Log("Draw Phase");
                yield return DrawPhase();
                Debug.Log("Action Select Phase");
                yield return ActionSelectPhase();
                foreach (var player in _playerCollection.Value)
                {
                    Debug.Log($"Attack Phase [Player {player.PlayerId}]");
                    yield return AttackPhase(player);
                }

                yield return new WaitForSeconds(2);
                Debug.Log("Discard Phase");
                yield return DiscardPhase();
                _isGameOver.Value = _playerCollection.Value.Any(p => p.Deck.Cards.Count() < 3);
            }
        }

        private IEnumerator DeckPreparationPhase()
        {
            var player = _playerCollection.CurrentPlayer;
            var otherPlayer = _playerCollection.GetOtherPlayer(player);

            _allPlayerCardData.Value.Shuffle();
            Debug.Log("PREPARE");
            for (var i = 0; i < _allPlayerCardData.Value.Count; i++)
            {
                var cardData = _allPlayerCardData.Value[i];
                if (i % 2 == 0)
                {
                    player.Deck.CreatAndAdd(cardData);
                }
                else
                {
                    otherPlayer.Deck.CreatAndAdd(cardData);
                }
            }

            player.Deck.Shuffle();
            otherPlayer.Deck.Shuffle();
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator AmbushPhase(Player.Player player, EnvironmentCard environmentCard)
        {
            // TODO: HERE GOES THE ANIMATION FOR FLIPPING A CARD FROM THE ENVIRONMENT DECK
            yield return environmentCard.ApplyEffects(_ambushTrigger, player);
        }

        private IEnumerator DrawPhase()
        {
            foreach (var player in _playerCollection.Value)
            {
                if (_networkSettingReference.IsLocal() || QuantumNetworkManager.LocalPlayer?.Player == player.PlayerId)
                {
                    for (var i = 0; i < 3; i++)
                    {
                        player.Deck.DrawTo(player.Hand);
                    }

                    player.CardSpawner.UpdateCards(player);
                }

                if (_networkSettingReference.IsOnline() && QuantumNetworkManager.LocalPlayer?.Player == player.PlayerId)
                {
                    var card1 = player.Hand.Cards.Count > 0 ? player.Hand.Cards[0] : null;
                    var card2 = player.Hand.Cards.Count > 1 ? player.Hand.Cards[1] : null;
                    var card3 = player.Hand.Cards.Count > 2 ? player.Hand.Cards[2] : null;
                    QuantumNetworkManager.LocalPlayer?.ChangeHand(player.PlayerId, card1?.NameId, card2?.NameId,
                        card3?.NameId);
                }
            }

            yield return new WaitForEndOfFrame();
        }

        public void CardDrawOnline(Networking.Player playerId, string card1, string card2, string card3)
        {
            if (QuantumNetworkManager.LocalPlayer?.Player == playerId)
                return;
            
            var player = _playerCollection.GetPlayer(playerId);
            player.Deck.DrawTo(card1, player.Hand);
            player.Deck.DrawTo(card2, player.Hand);
            player.Deck.DrawTo(card3, player.Hand);
            player.CardSpawner.UpdateCards(player);
        }

        private IEnumerator ActionSelectPhase()
        {
            yield return WaitForPlayerAction();
        }

        private IEnumerator AttackPhase(Player.Player player)
        {
            var environmentCard = _environmentDeck.GetByPlayer(player.PlayerId);
            var card = player.CurrentSelectedCard.Card as PlayerCard;
            yield return ActionPhase(card, player);
            yield return new WaitForSeconds(.5f);
            yield return DamagePhase(player, environmentCard);
            yield return new WaitForSeconds(.5f);
            yield return DefensePhase(player, environmentCard);
            yield return new WaitForSeconds(.5f);
            yield return RevengePhase(player, environmentCard);
            yield return new WaitForSeconds(.5f);
            if (card != null)
                card.Duration--;
        }

        private IEnumerator ActionPhase(PlayerCard card, Player.Player player)
        {
            yield return card.ApplyEffects(_attackTrigger, player);
        }

        private IEnumerator DamagePhase(Player.Player player, EnvironmentCard environmentCard)
        {
            var otherPlayer = _playerCollection.GetOtherPlayer(player);
            yield return environmentCard.Damage(player.CurrentSelectedCard.Card, otherPlayer.CurrentSelectedCard.Card,
                _isSurge.Value, _powerSurge, _shielded, _elusive, _fractionLess);
        }

        private IEnumerator DefensePhase(Player.Player player, EnvironmentCard environmentCard)
        {
            yield return environmentCard.ApplyEffects(_defenseTrigger, player);
        }

        private IEnumerator RevengePhase(Player.Player player, EnvironmentCard environmentCard)
        {
            yield return environmentCard.ApplyEffects(_revengeTrigger, player);
        }

        private IEnumerator DiscardPhase()
        {
            foreach (var player in _playerCollection.Value)
            {
                yield return VoidPhase(player);
                yield return LostPhase(player);
                player.CardSpawner.Despawn();
            }

            _environmentDeck.RemoveAllDefeated();
        }

        private IEnumerator VoidPhase(Player.Player player)
        {
            var voidCards = player.Hand.Cards.Where(c => c.Duration == 0).ToList();
            foreach (var voidCard in voidCards)
            {
                player.Hand.Transfer(voidCard, _voidDeck);
            }

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator LostPhase(Player.Player player)
        {
            var lostCards = player.Hand.Cards.Where(c => c.Duration > 0).ToList();
            // TODO: Lost Effects trigger

            var otherPlayer = _playerCollection.GetOtherPlayer(player);
            foreach (var lostCard in lostCards)
            {
                player.Hand.Transfer(lostCard, otherPlayer.Deck);
                yield return lostCard.ApplyEffects(_lostTrigger, player);
            }

            otherPlayer.Deck.Shuffle();

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator WaitForPlayerAction()
        {
            _playerCollection.ResetAcceptedState();
            while (!_playerCollection.AllPlayerHaveAccepted)
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}