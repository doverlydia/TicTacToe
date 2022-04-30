using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class GameStatus : MonoBehaviour
{
    private bool _gameStarted;
    private bool _gameEnded;
    private bool _Xturn;
    private TicTacToeCellManager[,] _cells = new TicTacToeCellManager[3, 3];
    private string[,] boardStatus = new string[3, 3] {{"","",""},
                                                      {"","",""},
                                                      {"","",""}};
    public bool gameStarted => _gameStarted;
    public bool gameEnded => _gameEnded;
    public bool Xturn => _Xturn;

    [SerializeField] private MiniMaxTicTacToe miniMax;
    [SerializeField] private TMP_Text gameEndText;
    [SerializeField] private TurnOrderManager turnOrder;
    [SerializeField] private List<TicTacToeCellManager> cells;

    private void Start()
    {
        foreach (var cell in cells)
        {
            _cells[(int)cell.cellLocationOnGrid.x, (int)cell.cellLocationOnGrid.y] = cell;
        }

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
            UpdateBestMoveCell(miniMax.BestMove(boardStatus, player == "X" ? "O" : "X"));
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

    public void RestartGame()
    {
        _gameEnded = false;
        _gameStarted = false;
        _Xturn = true;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                boardStatus[x, y] = "";
            }
        }
        foreach (var cell in _cells)
        {
            cell.ClearCell();
        }
        turnOrder.ChangeTurnSprite(turnSprite.X);
    }

    public void UpdateBestMoveCell(Vector2 bestMove)
    {
        _cells[(int)bestMove.x, (int)bestMove.y].UpdateCellStatus();
    }

    public void Hint()
    {
        Vector2 bestMove = miniMax.BestMove(boardStatus, "X");
        Debug.Log(bestMove);
        TicTacToeCellManager hintCell = _cells[(int)bestMove.x, (int)bestMove.y];
        hintCell.StartCoroutine(hintCell.Flash(0.2f, 0.2f, 2, "X"));
    }
}
