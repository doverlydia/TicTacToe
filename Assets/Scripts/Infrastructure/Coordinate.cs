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
    public Coordinate(Coordinate coordinate)
    {
        this.R = coordinate.R;
        this.C = coordinate.C;
    }

    public override string ToString()
    {
        return $"(R: {R}, C: {C})";
    }
}
