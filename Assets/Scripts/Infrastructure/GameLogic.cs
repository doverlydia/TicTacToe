using System.Collections.Generic;
public class GameLogic
{
    private readonly static Stack<Coordinate> moves = new Stack<Coordinate>();
    private PawnType whosTurn = PawnType.X;
    public PawnType[,] board { get; private set; } = new PawnType[3, 3];
    public GameState gameState { get; private set; } = GameState.Running;
    public void ConcludeTurn(Coordinate chosenCell)
    {
        SetCell(chosenCell, whosTurn);
        gameState = MiniMax.CheckForWinner(board);
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
            SetCell(moves.Pop(), PawnType.None);
        }
    }
    private void SetCell(Coordinate pos, PawnType pawn)
    {
        board[pos.R, pos.C] = pawn;
    }

    public void RestartGame()
    {
        gameState = GameState.Waiting;
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
        Coordinate bestMove = MiniMax.BestMove(board, whosTurn);
        return bestMove;
    }

}