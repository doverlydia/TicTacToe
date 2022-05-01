﻿using System;
using System.Linq;
public static class MiniMax
{
    public static Coordinate BestMove(PawnType[,] board, PawnType bestMoveForWho)
    {
        int bestScore = int.MinValue;
        Coordinate bestMove = Coordinate.Zero;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[r, c] == PawnType.None)
                {
                    board[r, c] = bestMoveForWho;
                    int score = MiniMaxer(board, false, bestMoveForWho);
                    board[r, c] = PawnType.None;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Coordinate(r, c);
                    }
                }
            }
        }
        return bestMove;
    }

    private static int MiniMaxer(PawnType[,] boardStatus, bool isMaximizing, PawnType miniMaxForWho)
    {
        GameState result =CheckForWinner(boardStatus);
        PawnType otherPlayer = miniMaxForWho == PawnType.O ? PawnType.X: PawnType.O;
        if (EnumUtils.IsGameEnded(result))
        {
            switch (result)
            {
                case GameState.Draw:
                    return 0;
                case GameState.WinnerX:
                    return miniMaxForWho == PawnType.X ? 10 : -10;
                case GameState.WinnerO:
                    return miniMaxForWho == PawnType.O ? 10 : -10;
            }
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (boardStatus[x, y] == PawnType.None)
                {
                    boardStatus[x, y] = isMaximizing ? miniMaxForWho : otherPlayer;
                    int score = MiniMaxer(boardStatus, !isMaximizing, miniMaxForWho);
                    boardStatus[x, y] = PawnType.None;
                    bestScore = isMaximizing ? Math.Max(bestScore, score) : Math.Min(bestScore, score);
                }
            }
        }
        return bestScore;
    }

    public static GameState CheckForWinner(PawnType[,] boardStatus)
    {
        PawnType winner = PawnType.None;
        for (int i = 0; i < 3; i++)
        {
            if (EnumUtils.ThreeEqualPawns(boardStatus[i, 0], boardStatus[i, 1], boardStatus[i, 2]))
            {
                winner = boardStatus[i, 0];
            }
            if (EnumUtils.ThreeEqualPawns(boardStatus[0, i], boardStatus[1, i], boardStatus[2, i]))
            {
                winner = boardStatus[0, i];
            }
        }
        if (EnumUtils.ThreeEqualPawns(boardStatus[0, 0], boardStatus[1, 1], boardStatus[2, 2])
            || EnumUtils.ThreeEqualPawns(boardStatus[0, 2], boardStatus[1, 1], boardStatus[2, 0]))
        {
            winner = boardStatus[1, 1];
        }
        if (winner != PawnType.None)
        {
            return winner == PawnType.X ? GameState.WinnerX : GameState.WinnerO;
        }
        else if (!boardStatus.OfType<PawnType>().Any(x => x == PawnType.None) && winner == PawnType.None)
        {
            return GameState.Draw;
        }
        else
        {
            return GameState.Running;
        }
    }
}