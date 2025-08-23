using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string _playerName;
    [SerializeField] private int _playerId;
    [SerializeField] private int _avatarId;
    [SerializeField] private int _rankedPoint;
    [SerializeField] private int _xSensitivity;
    [SerializeField] private int _ySensitivity;
    [SerializeField] private int _volume;

    public static void InitializePlayer(string playerName, int playerId, int avatarId, int rankedPoint, int xSensitivity, int ySensitivity, int volume)
    {
        // Assign values to the player instance
        Player player = new Player();
        player._playerName = playerName;
        player._playerId = playerId;
        player._avatarId = avatarId;
        player._rankedPoint = rankedPoint;
        player._xSensitivity = xSensitivity;
        player._ySensitivity = ySensitivity;
        player._volume = volume;
    }
}
