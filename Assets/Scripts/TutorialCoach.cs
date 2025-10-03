using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add TextMeshPro namespace

public class TutorialCoachManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private string[] textArray;

    private int currentIndex = 0;

    private void Awake()
    {
        if (displayText == null)
        {
            displayText = GetComponent<TextMeshProUGUI>();
        }

        textArray = new string[]
        {
            "Welcome to 27 Poker! A classic game with a powerful twist. Let's learn the ropes. I'll be your guide.",
            "First, let's look at your cards. You've been dealt two face-down cards. Tap to reveal them.",
            "Wow! You've been dealt the special hand: 2 and 7 of Triangles! This is the weakest hand in normal poker, but here, it has a secret power. We'll get to that soon.",
            "The game starts with a betting round called 'Pre-Flop'. The AI has posted the Small Blind, and you're in the Big Blind position. It's your turn.",
            "Checks.",
            "Since no one has bet yet, you can 'Check' to pass the action without betting. Tap 'Check' to continue.",
            "These three cards are called the 'Flop'. Everyone uses them to build their best hand. Look at the flop: a Ten, Jack, and Ace. Notice they are all different suits? This is called a 'Rainbow'.",
            "Remember your special 2-7? Because the flop is a rainbow, you can now use its power! Tap on your 2-7 hand to transform it.",
            "Perfect! You've transformed your 2-7 into a Queen and King of Spades. Combined with the Jack and Ten on the table, you now have a Straight (10-J-Q-K-A)! This is a very strong hand.",
            "Now it's time to bet again. A strong hand means you should bet to win more chips. Let's make a bet. Tap the 'Bet' button.",
            "The minimum bet is 20 chips (2x the Big Blind). Drag the slider to choose your bet size. Let's bet 50 chips.",
            "The AI folded! You win the pot without having to show your cards. Sometimes, betting strong can make opponents scared, even if you're bluffing.",
            "Let's try one more quick hand to see what happens when the flop isn't a rainbow.",
            "See? This flop has two Diamonds, so it's not a rainbow. Your 2-7 power cannot be activated. With this hand, your best move might be to 'Check' or 'Fold' if someone bets strongly, unless you want to bluff!",
            "You've mastered the basics of 27 Poker! Remember: Play your normal strong hands, but keep an eye out for the 2-7 and a rainbow flop—it's your chance to pull off a amazing surprise!"
        };
    }

    public void ChangeText()
    {
        if (textArray == null || textArray.Length == 0)
        {
            Debug.LogWarning("Text array is empty or null!");
            return;
        }

        // Update the text to current array position
        displayText.text = textArray[currentIndex];

        // Move to next index, loop back to 0 if we reach the end
        currentIndex = (currentIndex + 1) % textArray.Length;
    }
}
