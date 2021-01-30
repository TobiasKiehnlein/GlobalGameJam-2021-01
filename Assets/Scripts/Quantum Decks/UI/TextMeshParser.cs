using Shared.Scriptable_References;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshParser : MonoBehaviour
{
    [SerializeField] private IntReference _intReference;
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMesh.text = _intReference.Value.ToString();
    }
}

