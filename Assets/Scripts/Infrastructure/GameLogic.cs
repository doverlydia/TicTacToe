using System.Collections.Generic;

public class GameLogic
{
    private PawnType whosTurn = PawnType.X;
    private Stack<BoardPos> moves = new Stack<BoardPos>();
    public PawnType[,] board { get; private set; } = new PawnType[3, 3];
    public GameState GameState { get; private set; } = GameState.Waiting;
    public void ConcludeTurn(BoardPos chosenCell)
    {
        SetCell(chosenCell, whosTurn);
        GameState = MiniMax.CheckForWinner(board);
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
            BoardPos lastMove = moves.Pop();
            board[lastMove.r, lastMove.c] = PawnType.None;
        }
    }
    private void SetCell(BoardPos pos, PawnType pawn)
    {
        board[pos.r, pos.c] = pawn;
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
                SetCell(new BoardPos(r, c), PawnType.None);
            }
        }
    }

    public BoardPos Hint()
    {
        return MiniMax.BestMove(board, whosTurn);
    }

}