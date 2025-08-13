using UnityEngine;
using System.Collections.Generic;

public class PokerDeckManager : MonoBehaviour
{
    public static PokerDeckManager Instance { get; private set; }
    [SerializeField] private GameObject pokerCardPrefab;
    private List<InGamePokerCard> deck = new List<InGamePokerCard>();

    private int cardCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeDeck()
    {
        // Create regular cards (10, J, Q, K for each suit)
        foreach (suits suit in new[] { suits.Hearts, suits.Diamonds, suits.Clubs, suits.Spades })
        {
            foreach (CardValue value in new[] { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace })
            {
                CreateCard(suit, value);
            }
        }
        
        // Create special cards
        CreateCard(suits.Special, CardValue.Two);
        CreateCard(suits.Special, CardValue.Seven);
    }

    private void CreateCard(suits suit, CardValue value)
    {
        GameObject cardObj = Instantiate(pokerCardPrefab, transform);
        InGamePokerCard card = cardObj.GetComponent<InGamePokerCard>();

        if (card != null)
        {
            card.InitializeCard(suit, value);
            deck.Add(card);
            cardObj.SetActive(false); // Initially hide all cards
        }
    }

    public void ShuffleDeck()
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            InGamePokerCard temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }

    public InGamePokerCard DrawCard()
    {
        if (deck.Count > 0)
        {
            InGamePokerCard card = deck[0];
            deck.RemoveAt(0);
            card.gameObject.SetActive(true);
            return card;
        }
        return null;
    }

    public void ReturnCardToDeck(InGamePokerCard card)
    {
        if (card != null)
        {
            card.gameObject.SetActive(false);
            deck.Add(card);
        }
    }
    
    public void ResetDeck()
    {
        foreach (InGamePokerCard card in deck)
        {
            card.gameObject.SetActive(false);
        }
        ShuffleDeck();
    }
}