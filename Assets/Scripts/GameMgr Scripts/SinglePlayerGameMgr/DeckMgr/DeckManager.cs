using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Suits
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum CardValue
{
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}

public class DeckManager : MonoBehaviour
{
    // [SerializeField] private GameObject _pokerCardPrefab;
    [SerializeField] private List<(Suits suit, CardValue cardValue)> _deck = new List<(Suits suit, CardValue cardValue)>();

    private int _specialCardsCount;


    public void InitializeDeck()
    {
        foreach (Suits suit in System.Enum.GetValues(typeof(Suits)))
        {
            for (int value = 10; value <= 14; value++)
            {
                _deck.Add((suit, (CardValue)value));
            }
        }
        Debug.Log($"Deck initialized with {_deck.Count} cards.");
    }

    public void InitializeSpecialCards(int numberOfPlayers)
    {
        if (numberOfPlayers < 2 || numberOfPlayers > 8)
        {
            Debug.LogError("Number of players must be between 2 and 8.");
            return;
        }
        _specialCardsCount = numberOfPlayers / 2;
    }
}
