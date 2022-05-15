using System.Collections.Generic;
using UnityEngine;
public class GameLogic
{
    private readonly static Stack<Coordinate> moves = new Stack<Coordinate>();
    public PawnType WhosTurn { get; private set; } = PawnType.X;
    public PawnType[,] Board { get; private set; } = new PawnType[3, 3];
    public GameState GameState { get; private set; } = GameState.Running;
    public void ConcludeTurn(Coordinate chosenCell)
    {
        SetCell(chosenCell, WhosTurn);
        GameState = MiniMax.CheckForWinner(Board);
        moves.Push(chosenCell);
    }
    public void ChangeTurn()
    {
        WhosTurn = WhosTurn == PawnType.X ? PawnType.O : PawnType.X;
    }
    public void Undo()
    {
        if (moves.Count < 2)
            return;
        for (int i = 0; i < 2; i++)
        {
            SetCell(moves.Pop(), PawnType.None);
        }
    }
    private void SetCell(Coordinate pos, PawnType pawn)
    {
        Board[pos.R, pos.C] = pawn;
    }

    public void RestartGame()
    {
        GameState = GameState.Waiting;
        WhosTurn = PawnType.X;
        moves.Clear();
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                SetCell(new Coordinate(r, c), PawnType.None);
            }
        }
    }

    public Coordinate Hint()
    {
        Coordinate bestMove = MiniMax.BestMove(Board, WhosTurn);
        return bestMove;
    }

    public Coordinate GetRandomEmptyPos()
    {
        List<Coordinate> emptyCells = new List<Coordinate>();
        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (Board[r, c] == PawnType.None)
                    emptyCells.Add(new Coordinate(r, c));
            }
        }

        return emptyCells[Random.Range(0, emptyCells.Count)];
    }
}