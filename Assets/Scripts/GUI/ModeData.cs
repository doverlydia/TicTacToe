using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    playerVsplayer,
    playerVScomputer,
    computerVScomputer
}

[CreateAssetMenu(fileName = "ModeData", menuName = "ScriptableObjects/ModeData", order = 2)]
public class ModeData : ScriptableObject
{
    [field: SerializeField] public GameMode GameMode { get; private set; } = GameMode.playerVsplayer;
    [field: SerializeField] public bool ComputerVScomputer { get; private set; } = true;
    [field: SerializeField] public string Player1Name { get; private set; } = "Player1";
    [field: SerializeField] public string Player2Name { get; private set; } = "Player2";

    public void SetData(GameMode mode, string player1Name, string player2Name)
    {
        this.GameMode = mode;
        this.Player1Name = player1Name;
        this.Player2Name = player2Name;
    }
    public void RandomAsignPlayers()
    {
        string player1 = Player1Name;
        string player2 = Player2Name;

        int rnd = Random.Range(0, 2);

        if (rnd == 1)
        {
            Player1Name = player2;
            Player2Name = player1;
        }
    }
}
