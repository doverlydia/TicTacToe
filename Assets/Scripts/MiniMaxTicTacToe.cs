using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MiniMaxTicTacToe : MonoBehaviour
{
    [SerializeField] private GameStatus gameStatus;

    public Vector2 BestMove(string[,] board, string bestMoveForWho)
    {
        string otherPlayer = bestMoveForWho == "O" ? "X" : "O";
        int bestScore = int.MinValue;
        Vector2 bestMove = Vector2.zero;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[x, y] == "")
                {
                    board[x, y] = bestMoveForWho;
                    int score = MiniMax(board, false, bestMoveForWho);
                    board[x, y] = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2(x, y);
                    }
                }
            }
        }
        return bestMove;
    }

    private int MiniMax(string[,] boardStatus, bool isMaximizing, string miniMaxForWho)
    {
        string result = gameStatus.CheckForWinner(boardStatus);
        string otherPlayer = miniMaxForWho == "O" ? "X" : "O";
        if (result != null)
        {
            switch (result)
            {
                case "tie":
                    return 0;
                case "X":
                    return miniMaxForWho == "X" ? 10 : -10;
                case "O":
                    return miniMaxForWho == "O" ? 10 : -10;
            }
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (boardStatus[x, y] == "")
                {
                    boardStatus[x, y] = isMaximizing ? miniMaxForWho : otherPlayer;
                    int score = MiniMax(boardStatus, !isMaximizing, miniMaxForWho);
                    boardStatus[x, y] = "";
                    bestScore = isMaximizing ? Mathf.Max(bestScore, score) : Mathf.Min(bestScore, score);
                }
            }
        }
        return bestScore;
    }
}
