using System.Collections.Generic;

public class GameLogic
{
    private readonly Stack<Coordinate> moves = new Stack<Coordinate>();
    private PawnType whosTurn = PawnType.X;
    public PawnType[,] Board { get; private set; } = new PawnType[3, 3];
    public GameState GameState { get; private set; } = GameState.Waiting;
    public void ConcludeTurn(Coordinate chosenCell)
    {
        GameState = GameState.Running;
        SetCell(chosenCell, whosTurn);
        ChangeTurn();
        GameState = MiniMax.CheckForWinner(Board);
        if (EnumUtils.IsGameEnded(GameState))
        {
            //game end stuff
        }
        else
        {
            //maybeAI;
        }
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
            Board[lastMove.C, lastMove.R] = PawnType.None;
        }
    }
    private void SetCell(Coordinate pos, PawnType pawn)
    {
        Board[pos.C, pos.R] = pawn;
    }

    public void RestartGame()
    {
        GameState = GameState.Waiting;
        whosTurn = PawnType.X;
        moves.Clear();
        for (int c = 0; c < 3; c++)
        {
            for (int r = 0; r < 3; r++)
            {
                SetCell(new Coordinate(c, r), PawnType.None);
            }
        }
    }

    public Coordinate Hint()
    {
        Coordinate bestMove = MiniMax.BestMove(Board, whosTurn);
        return bestMove;
    }

}