using UnityEngine;

public class InGamePokerCard : Interactable
{
    [Header("Poker Card Configuration")]
    [SerializeField] private Suits cardSuit;
    [SerializeField] private CardValue cardValue;

    public void InitializeCard(Suits suit, CardValue value)
    {
        cardSuit = suit;
        cardValue = value;
    }

    public Suits GetSuit() => cardSuit;
    public CardValue GetValue() => cardValue;
}