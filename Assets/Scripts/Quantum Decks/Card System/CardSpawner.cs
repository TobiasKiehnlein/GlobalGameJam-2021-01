using Quantum_Decks.Card_System;
using Quantum_Decks.Player;
using UnityEngine;

[RequireComponent(typeof(CardCollection))]
public class CardSpawner : MonoBehaviour
{
    private CardCollection _collection;
    public GameObject CardPrefab;

    private void Awake()
    {
        _collection = GetComponent<CardCollection>();
    }

    public void Spawn(Player owner)
    {
        foreach (var card in _collection.Cards)
        {
            var cardObject = Instantiate(CardPrefab, transform);
            cardObject.GetComponent<CardObject>().UpdateCard(owner, card);
        }
    }

    public void Despawn(Player owner)
    {
        foreach (Transform child in owner.Hand.transform)
        {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in owner.DropZone)
        {
            Destroy(child.gameObject);
        }
    }
}