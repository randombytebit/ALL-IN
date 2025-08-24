using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] private int _aiId;
    [SerializeField] private int _avatarId;
    public static void InitializeAI(int aiId, int avatarId)
    {
        // Assign values to the AI instance
        AI ai = new AI();
        ai._aiId = aiId;
        ai._avatarId = avatarId;
    }
}
