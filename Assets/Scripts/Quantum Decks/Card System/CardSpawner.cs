using System.Linq;
using Quantum_Decks.Card_System;
using Quantum_Decks.Player;
using UnityEngine;

[RequireComponent(typeof(CardCollection))]
public class CardSpawner : MonoBehaviour
{
    private CardCollection _collection;
    private HandAnimations _handAnimations;
    public GameObject CardPrefab;

    private CardObject[] _cardObjects;

    private void Awake()
    {
        _collection = GetComponent<CardCollection>();
        _handAnimations = GetComponent<HandAnimations>();
        _cardObjects = GetComponentsInChildren<CardObject>();
    }

    public void UpdateCards(Player player)
    {
        var index = 0;
        foreach (var card in player.Hand.Cards)
        {
            _cardObjects[index].UpdateCard(player, card);
            index++;
        }
    }

    public void Despawn()
    {
        foreach (var cardObject in _cardObjects)
        {
            cardObject.DeleteFractionIcons();
        }
    }
}