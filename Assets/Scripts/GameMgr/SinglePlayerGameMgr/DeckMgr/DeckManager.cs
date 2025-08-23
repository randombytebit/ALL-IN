using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject pokerCardPrefab;

    public void InitializeDeck()
    {
        foreach (suits suit in new[] { suits.Hearts, suits.Diamonds, suits.Clubs, suits.Spades })
        {
            foreach (CardValue value in new[] { CardValue.Ten, CardValue.Jack, CardValue.Queen, CardValue.King, CardValue.Ace })
            {
                ObjectPoolManager.SpawnPooledObject(pokerCardPrefab, Vector3.zero, Quaternion.identity, ObjectPoolManager.ObjectPoolType.InGameObjects);
            }
        }
    }
}
