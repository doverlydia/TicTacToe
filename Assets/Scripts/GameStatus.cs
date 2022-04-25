using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameStatus : MonoBehaviour
{
    private bool _gameStarted;
    private bool _gameEnded;
    private bool _Xturn;
    public bool gameStarted => _gameStarted;
    public bool gameEnded => _gameEnded;
    public bool Xturn => _Xturn;
    [SerializeField] MiniMaxTicTacToe miniMax;
    [SerializeField] TMP_Text gameEndText;
    private string[,] boardStatus = new string[3, 3] {{"","",""},
                                                      {"","",""},
                                                      {"","",""}};
    private void Start()
    {
        _Xturn = true;
    }
    private bool ThreeEqualSymbols(string a, string b, string c)
    {
        return (a == b && b == c) && a != "";
    }

    public string CheckForWinner(string[,] boardStatus)
    {
        string winner = null;
        for (int i = 0; i < 3; i++)
        {
            if (ThreeEqualSymbols(boardStatus[i, 0], boardStatus[i, 1], boardStatus[i, 2]))
            {
                winner = boardStatus[i, 0];
            }
            if (ThreeEqualSymbols(boardStatus[0, i], boardStatus[1, i], boardStatus[2, i]))
            {
                winner = boardStatus[0, i];
            }
        }
        if (ThreeEqualSymbols(boardStatus[0, 0], boardStatus[1, 1], boardStatus[2, 2])
            || ThreeEqualSymbols(boardStatus[0, 2], boardStatus[1, 1], boardStatus[2, 0]))
        {
            winner = boardStatus[1, 1];
        }
        if (!boardStatus.OfType<string>().Any(x => x == "") && winner == null)
        {
            return "tie";
        }
        else
        {
            return winner;
        }
    }

    public void UpdateGameStatus(Vector2 clickedCell, string player)
    {
        _gameStarted = true;
        UpdateBoardStatus(clickedCell, player);
        _Xturn = !_Xturn;
        string result = CheckForWinner(boardStatus);
        if (result != null)
        {
            GameEnding(result);
        }
        if (!_Xturn && !_gameEnded)
        {
            miniMax.BestMove(boardStatus);
        }
    }

    private void UpdateBoardStatus(Vector2 clickedCell, string player)
    {
        boardStatus[(int)clickedCell.x, (int)clickedCell.y] = player;
    }

    private void GameEnding(string result)
    {
        _gameEnded = true;
        switch (result)
        {
            case "tie":
                gameEndText.text = "its a tie!";
                return;
            case "X":
                gameEndText.text = "X won!";
                return;
            case "O":
                gameEndText.text = "O won!";
                return;
        }
    }
}
