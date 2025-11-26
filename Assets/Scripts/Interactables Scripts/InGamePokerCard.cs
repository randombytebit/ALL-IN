using UnityEngine;

public class InGamePokerCard : Interactable
{
    [Header("Poker Card Configuration")]
    [SerializeField] private Suits cardSuit;
    [SerializeField] private CardValue cardValue;

    private Vector3 originalPos;
    private bool isPickedUp = false;

    protected override void Interact()
    {
        if (isPickedUp) return;

        Debug.Log($"Picked up {cardValue} of {cardSuit}");
        originalPos = transform.position;

        // SUPER ROBUST FIND — works with Player(Clone), late spawning, pooling, everything
        Transform hold = FindHoldPosition();

        if (hold == null)
        {
            Debug.LogError("HoldPosition STILL not found! Check:\n" +
                        "1. Player has Tag = 'Player'\n" +
                        "2. Has a child exactly named 'HoldPosition' (case-sensitive)\n" +
                        "3. Player is already spawned when picking up card");
            return;
        }

        // INSTANT TELEPORT — perfect
        transform.SetParent(hold);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; // or Quaternion.Euler(0,180,0) if backwards

        if (TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = true;
        if (TryGetComponent<Collider>(out var col)) col.enabled = false;

        isPickedUp = true;
    }

    private Transform FindHoldPosition()
    {
        // Method 1: Find by tag (works with Player(Clone))
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var hold = player.transform.Find("HoldPosition");
            if (hold != null) return hold;
        }

        // Method 2: Find ANY active "HoldPosition" in the scene (emergency fallback)
        var fallback = GameObject.Find("HoldPosition");
        if (fallback != null) return fallback.transform;

        // Method 3: Find by component if you add a marker script later
        // var marker = FindObjectOfType<PlayerHoldMarker>();
        // if (marker) return marker.holdPosition;

        return null;
    }

    protected override void Point()
    {
        // Optional hover feedback
    }
}