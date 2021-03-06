﻿using System.Linq;
using Quantum_Decks.Card_System;
using Quantum_Decks.Environment;
using Shared.Scriptable_References;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshParser : MonoBehaviour
{
    [SerializeField] private IntReference _intReference;
    private TextMeshProUGUI _textMesh;

    [SerializeField] private CardCollection _collection;
    [SerializeField] private EnvironmentDeck _environmentDeck;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_intReference)
            _textMesh.text = _intReference.Value.ToString();
        if (_collection)
            _textMesh.text = _collection.Cards.Count().ToString();
        if (_environmentDeck)
            _textMesh.text = _environmentDeck.Count.ToString();
    }
}