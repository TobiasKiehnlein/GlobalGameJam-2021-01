using Quantum_Decks.Card_System;
using Quantum_Decks.Player;
using UnityEngine;

[RequireComponent(typeof(CardCollection))]
public class CardSpawner : MonoBehaviour
{
    private CardCollection _collection;
    private HandAnimations _handAnimations;
    public GameObject CardPrefab;

    private void Awake()
    {
        _collection = GetComponent<CardCollection>();
        _handAnimations = GetComponent<HandAnimations>();
    }

    public void Spawn()
    {
        // foreach (var card in _collection.Cards)
        // {
        //     var cardObject = Instantiate(CardPrefab, transform);
        //     cardObject.GetComponent<CardObject>().UpdateCard(owner, card);
        // }
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