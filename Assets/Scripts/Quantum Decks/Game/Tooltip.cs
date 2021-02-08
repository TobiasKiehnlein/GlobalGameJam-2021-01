using System.Linq;
using Quantum_Decks.Card_System;
using Quantum_Decks.Localization;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private LocalizationCollection _localization;
    [SerializeField] private Transform _tooltipTransform;
    [SerializeField] private TooltipSystem _tooltipSystem;
    [SerializeField] private TextMeshProUGUI _headerField;
    [SerializeField] private TextMeshProUGUI _contentField;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private int _characterWrapLimit;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = _tooltipTransform.GetComponent<RectTransform>();
        _tooltipSystem.Tooltip = this;
        SetActive(false);
    }

    public void SetText(CardData cardData)
    {
        _headerField.gameObject.SetActive(false);
        var rules = "";
        _contentField.text = "";
        var keywords = cardData.Keywords.Select(k => k.NameId);
        var effectTrigger = cardData.EffectData.Select(e => e.Trigger.NameId);
        var effects = cardData.EffectData.SelectMany(e => e.Effect.Select(d => d.NameId));
        keywords.ForEach(k => rules += _localization.CurrentLocalization.GetTextById(k) + '\n');
        effectTrigger.ForEach(e => rules += _localization.CurrentLocalization.GetTextById(e) + '\n');
        if (cardData is PlayerCardData playerCard && playerCard.Duration > 1)
        {
            rules += _localization.CurrentLocalization.GetTextById("Rules_Resurgence");
        }

        effects.ForEach(e => rules += _localization.CurrentLocalization.GetTextById(e) + '\n');

        if (string.IsNullOrEmpty(rules))
        {
            UpdateLayoutElementSize();
            SetActive(false);
            return;
        }

        _contentField.text = rules;
        UpdateLayoutElementSize();
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }

        _contentField.text = content;
        UpdateLayoutElementSize();
    }

    public void SetActive(bool state)
    {
        _tooltipTransform.gameObject.SetActive(state);
    }


    private void Update()
    {
        Vector2 position = Input.mousePosition;
        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        _rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    private void UpdateLayoutElementSize()
    {
        var headerLength = _headerField.text.Length;
        var contentLength = _contentField.text.Length;

        _layoutElement.enabled = headerLength > _characterWrapLimit || contentLength > _characterWrapLimit;
    }
}