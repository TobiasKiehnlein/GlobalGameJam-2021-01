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
        private CardDataCollection _cardDataCollection;

        [Required, SerializeField, BoxGroup("References")]
        private CardCollection _voidDeck;
        
        private EnvironmentDeck _environmentDeck;

        private bool _isSurge;


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

            while (!_isGameOver.Value)
            {
                Debug.Log("Ambush Phase");
                yield return AmbushPhase();
                Debug.Log("Draw Phase");
                yield return DrawPhase();
                Debug.Log("Action Select Phase");
                yield return ActionSelectPhase();
                foreach (var player in _playerCollection.Value)
                {
                    Debug.Log($"Attak Phase [Player {player.PlayerId}]");
                    yield return AttackPhase(player);
                }
                
                Debug.Log("Discard Phase");
                yield return DiscardPhase();
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

        private IEnumerator AmbushPhase()
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DrawPhase()
        {
            foreach (var player in _playerCollection.Value)
            {
                for (var i = 0; i < 3; i++)
                {
                    player.Deck.DrawTo(player.Hand);
                }

                // player.CardSpawner.Spawn(player);
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
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DamagePhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            if (environmentCard.Fractions.Intersect(card.Fractions).Any())
            {
                environmentCard.Damage(card.Value);
            }
            else
            {
                environmentCard.Damage();
            }
            
            // TODO: Damage Animation
            // TODO: Damage Special Effect

            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DefensePhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator RevengePhase(PlayerCard card, EnvironmentCard environmentCard)
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DiscardPhase()
        {
            foreach (var player in _playerCollection.Value)
            {
                yield return VoidPhase(player);
                yield return LostPhase(player);
                player.CardSpawner.Despawn(player);
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