using Quantum_Decks.Card_System;
using UnityEngine;

[CreateAssetMenu]
public class TooltipSystem : ScriptableObject
{
    public Tooltip Tooltip;

    public void Show(CardData cardData)
    {
        if (Tooltip == null)
            return;
        
        Tooltip.SetText(cardData);
        Tooltip.SetActive(true);
    }
    
    public void Show(string content, string header = "")
    {
        if (Tooltip == null)
            return;
        
        Tooltip.SetText(content, header);
        Tooltip.SetActive(true);
    }

    public void Hide()
    {
        if (Tooltip == null)
            return;
        
        Tooltip.SetActive(false);
    }
}