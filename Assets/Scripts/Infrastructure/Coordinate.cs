public struct Coordinate
{
    public int R { get; set; }
    public int C { get; set; }

    public static Coordinate Zero => new Coordinate(0, 0); 
    public static Coordinate One => new Coordinate(1, 1); 

    public Coordinate(int r, int c)
    {
        this.R = r;
        this.C = c;
    }
    public Coordinate(Coordinate boardPos)
    {
        this.R = boardPos.R;
        this.C = boardPos.C;
    }

    public override string ToString()
    {
        return $"(r: {R},c: {C})";
    }
}
