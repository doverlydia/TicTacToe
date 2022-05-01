public static class EnumUtils
{
    public static bool ThreeEqualPawns(PawnType a, PawnType b, PawnType c)
    {
        return ((a == b && b == c) && (a != PawnType.None));
    }
    public static bool IsGameEnded(GameState state)
    {
        return ((state == GameState.WinnerO) || (state == GameState.WinnerO) || (state == GameState.Draw));
    }
}
