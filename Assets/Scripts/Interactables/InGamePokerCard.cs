using UnityEngine;

public enum suits
{
    Hearts,
    Diamonds,
    Clubs,
    Spades,
    Special
}

public enum CardValue
{
    Ten,
    Jack,
    Queen,
    King,
    Ace,
    Two,
    Seven
}

public class InGamePokerCard : Interactable
{
    [Header("Poker Card Configuration")]
    [SerializeField] private suits cardSuit;
    [SerializeField] private CardValue cardValue;

    public void InitializeCard(suits suit, CardValue value)
    {
        cardSuit = suit;
        cardValue = value;
    }

    public suits GetSuit() => cardSuit;
    public CardValue GetValue() => cardValue;
}