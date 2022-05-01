using System.Collections.Generic;

public class GameLogic
{
    private readonly Stack<Coordinate> moves = new Stack<Coordinate>();
    private PawnType whosTurn = PawnType.X;
    public PawnType[,] Board { get; private set; } = new PawnType[3, 3];
    public GameState GameState { get; private set; } = GameState.Waiting;
    public void ConcludeTurn(Coordinate chosenCell)
    {
        SetCell(chosenCell, whosTurn);
        GameState = MiniMax.CheckForWinner(Board);
        moves.Push(chosenCell);
    }
    public void ChangeTurn()
    {
        whosTurn = whosTurn == PawnType.X ? PawnType.O : PawnType.X;
    }
    public void Undo()
    {
        if (moves.Count < 2)
            return;
        for (int i = 0; i < 2; i++)
        {
            Coordinate lastMove = moves.Pop();
            Board[lastMove.R, lastMove.C] = PawnType.None;
        }
    }
    private void SetCell(Coordinate pos, PawnType pawn)
    {
        Board[pos.R, pos.C] = pawn;
    }

    public void RestartGame()
    {
        GameState = GameState.Waiting;
        whosTurn = PawnType.X;
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
        Coordinate bestMove = MiniMax.BestMove(Board, whosTurn);
        return bestMove;
    }

}