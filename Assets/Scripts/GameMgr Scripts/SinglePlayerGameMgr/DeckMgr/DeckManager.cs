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

    private void Start()
    {
        StartCoroutine(InitializeDeck());
    }

    private IEnumerator InitializeDeck()
    {
        yield return null;

        foreach (Suits suit in System.Enum.GetValues(typeof(Suits)))
        {
            for (int value = 10; value <= 14; value++)
            {
                _deck.Add((suit, (CardValue)value));
            }
        }
        Debug.Log($"Deck initialized with {string.Join(", ", _deck)}");
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < _deck.Count; i++)
        {
            var temp = _deck[i];
            int randomIndex = Random.Range(0, _deck.Count);
            _deck[i] = _deck[randomIndex];
            _deck[randomIndex] = temp;
        }
        Debug.Log($"Deck shuffled with {string.Join(", ", _deck)}");
    }

    public string GetSetOfCards()
    {
        if (_deck.Count == 0)
        {
            return "[]";
        }
    
        var card = _deck[0];
        _deck.RemoveAt(0);
        
        return $"[{card.suit}, {card.cardValue.ToString().ToLower()}]";
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
