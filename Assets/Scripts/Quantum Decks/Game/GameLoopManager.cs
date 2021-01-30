using System.Collections;
using Quantum_Decks.Card_System;
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

        // TODO: Remove this
        [SerializeField] private PlayerCardData DEBUG_CARD;
        

        private Coroutine _gameLoopRoutine;

        private void Start()
        {
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
                yield return AmbushPhase();
                yield return DrawPhase();
                yield return ActionSelectPhase();
                yield return AttackPhase();
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
                player.CardSpawner.Spawn(player);
            }
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator ActionSelectPhase()
        {
            yield return WaitForPlayerAction();
        }

        private IEnumerator AttackPhase()
        {
            yield return DefensePhase();
            yield return RevengePhase();
        }

        private IEnumerator DefensePhase()
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator RevengePhase()
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator DiscardPhase()
        {
            yield return VoidPhase();
            yield return LostPhase();
        }

        private IEnumerator LostPhase()
        {
            yield return new WaitForEndOfFrame();
        }

        private IEnumerator VoidPhase()
        {
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