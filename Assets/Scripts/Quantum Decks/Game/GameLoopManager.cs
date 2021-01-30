using System.Collections;
using System.Linq;
using Quantum_Decks.Card_System;
using Quantum_Decks.Environment;
using Shared.Scriptable_References;
using Sirenix.OdinInspector;
using UnityEngine;

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


        // TODO: Remove this
        [SerializeField] private PlayerCardData DEBUG_CARD;


        private Coroutine _gameLoopRoutine;


        private void Start()
        {
            _environmentDeck = FindObjectOfType<EnvironmentDeck>();
            StartCoroutine(GameLoop());
        }

        private void OnDestroy()
        {
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

                Debug.Log("Discard Phase");
                yield return DiscardPhase();
                _isGameOver.Value = _playerCollection.Value.Any(p => p.Deck.Cards.Count() < 3);
            }
        }

        private IEnumerator DeckPreparationPhase()
        {
            foreach (var player in _playerCollection.Value)
            {
                // TODO: Change for real cards
                for (var i = 0; i < 15; i++)
                {
                    player.Deck.CreatAndAdd(DEBUG_CARD);
                }

                player.Deck.Shuffle();
            }

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator AmbushPhase(Player.Player player, EnvironmentCard environmentCard)
        {
            // TODO: HERE GOES THE ANIMATION FOR FLIPPING A CARD FROM THE ENVIRONMENT DECK
            yield return environmentCard.ApplyEffects(_ambushTrigger);
        }

        private IEnumerator DrawPhase()
        {
            foreach (var player in _playerCollection.Value)
            {
                for (var i = 0; i < 3; i++)
                {
                    player.Deck.DrawTo(player.Hand);
                }

                player.CardSpawner.UpdateCards(player);
            }

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator ActionSelectPhase()
        {
            yield return WaitForPlayerAction();
        }

        private IEnumerator AttackPhase(Player.Player player)
        {
            var environmentCard = _environmentDeck.GetByPlayer(player.PlayerId);
            var card = player.CurrentSelectedCard.Card as PlayerCard;
            yield return ActionPhase(card, environmentCard);
            yield return DamagePhase(card, environmentCard);
            yield return DefensePhase(card, environmentCard);
            yield return RevengePhase(card, environmentCard);
            if (card != null)
                card.Duration--;
        }

        private IEnumerator ActionPhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            yield return card.ApplyEffects(_attackTrigger);
        }

        private IEnumerator DamagePhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            yield return environmentCard.Damage(card);
        }

        private IEnumerator DefensePhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            yield return environmentCard.ApplyEffects(_defenseTrigger);
        }

        private IEnumerator RevengePhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            yield return environmentCard.ApplyEffects(_revengeTrigger);
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
                yield return lostCard.ApplyEffects(_lostTrigger);
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