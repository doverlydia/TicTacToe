public struct Coordinate
{
    public int C { get; set; }
    public int R { get; set; }

    public static Coordinate Zero => new Coordinate(0, 0);
    public static Coordinate One => new Coordinate(1, 1);

    public Coordinate(int c, int r)
    {
        this.C = c;
        this.R = r;
    }
    public Coordinate(Coordinate coordinate)
    {
        this.C = coordinate.C;
        this.R = coordinate.R;
    }

    public override string ToString()
    {
        return $"(C: {C}, R: {R})";
    }
}
