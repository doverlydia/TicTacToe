using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MiniMaxTicTacToe : MonoBehaviour
{
    [SerializeField] GameStatus gameStatus;
    [SerializeField] List<TicTacToeCellManager> cells;
    TicTacToeCellManager[,] _cells = new TicTacToeCellManager[3, 3];
    private void Start()
    {
        foreach (var cell in cells)
        {
            _cells[(int)cell.cellLocationOnGrid.x, (int)cell.cellLocationOnGrid.y] = cell;
        }
    }
    public void BestMove(string[,] board)
    {
        int bestScore = int.MinValue;
        Vector2 bestMove = Vector2.zero;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[x, y] == "")
                {
                    board[x, y] = "O";
                    int score = MiniMax(board, false);
                    board[x, y] = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2(x, y);
                    }
                }
            }
        }
        UpdateBestMoveCell(bestMove);
    }

    private int MiniMax(string[,] boardStatus, bool isMaximizing)
    {
        string result = gameStatus.CheckForWinner(boardStatus);
        if (result != null)
        {
            switch (result)
            {
                case "X":
                    return -10;
                case "O":
                    return 10;
                case "tie":
                    return 0;
            }
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (boardStatus[x, y] == "")
                {
                    boardStatus[x, y] = isMaximizing ? "O" : "X";
                    int score = MiniMax(boardStatus, !isMaximizing);
                    boardStatus[x, y] = "";
                    bestScore = isMaximizing ? Mathf.Max(bestScore, score) : Mathf.Min(bestScore, score);
                }
            }
        }
        return bestScore;
    }

    private void UpdateBestMoveCell(Vector2 bestMove)
    {
        _cells[(int)bestMove.x, (int)bestMove.y].UpdateCellStatus();
    }
}
