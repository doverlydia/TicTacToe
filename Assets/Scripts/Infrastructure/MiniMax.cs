using System.Linq;
using System;
public static class MiniMax
{
    public static BoardPos BestMove(PawnType[,] board, PawnType pawn)
    {
        int bestScore = int.MinValue;
        BoardPos bestMove = new BoardPos(0, 0);
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[r, c] == PawnType.None)
                {
                    board[r, c] = pawn;
                    int score = GetMiniMaxScore(board, false, pawn);
                    board[r, c] = PawnType.None;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new BoardPos(r, c);
                    }
                }
            }
        }
        return bestMove;
    }

    private static int GetMiniMaxScore(PawnType[,] board, bool isMaximizing, PawnType pawn)
    {
        GameState result = CheckForWinner(board);
        PawnType otherPlayer = pawn == PawnType.X ? PawnType.O : PawnType.X;
        if (EnumUtils.IsGameEnded(result))
        {
            switch (result)
            {
                case GameState.Draw:
                    return 0;
                case GameState.WinnerX:
                    return PawnType.X == pawn ? 10 : -10;
                case GameState.WinnerO:
                    return PawnType.O == pawn ? 10 : -10;
            }
        }

        int bestScore = isMaximizing ? int.MinValue : int.MaxValue;
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[r, c] == PawnType.None)
                {
                    board[r, c] = isMaximizing ? pawn : otherPlayer;
                    int score = GetMiniMaxScore(board, !isMaximizing, pawn);
                    board[r, c] = PawnType.None;
                    bestScore = isMaximizing ? Math.Max(bestScore, score) : Math.Min(bestScore, score);
                }
            }
        }
        return bestScore;
    }

    public static GameState CheckForWinner(PawnType[,] board)
    {
        GameState state = GameState.Running;

        for (int r = 0; r < 3; r++)
        {
            if (EnumUtils.ThreeEqualPawns(board[r, 0], board[r, 1], board[r, 2]))
                state = board[r, 0] == PawnType.X ? GameState.WinnerX : GameState.WinnerO;

            if (EnumUtils.ThreeEqualPawns(board[0, r], board[1, r], board[2, r]))
                state = board[0, r] == PawnType.X ? GameState.WinnerX : GameState.WinnerO;
        }

        if (EnumUtils.ThreeEqualPawns(board[0, 0], board[1, 1], board[2, 2]) ||
            EnumUtils.ThreeEqualPawns(board[0, 2], board[1, 1], board[2, 0]))
        {
            state = board[1, 1] == PawnType.X ? GameState.WinnerX : GameState.WinnerO;
        }

        if (state == GameState.Running && !board.OfType<PawnType>().Any(x => x == PawnType.None))
        {
            state = GameState.Draw;
        }

        return state;
    }
}
