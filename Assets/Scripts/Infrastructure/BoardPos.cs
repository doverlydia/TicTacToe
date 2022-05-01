public struct BoardPos
{
    public int r;
    public int c;

    public BoardPos (int r, int c)
    {
        this.r = r;
        this.c = c;
    }
    public BoardPos(BoardPos boardPos)
    {
        this.r = boardPos.r;
        this.c = boardPos.c;
    }

    public override string ToString()
    {
        return $"({r},{c})";
    }
}
