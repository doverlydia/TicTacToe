using NUnit.Framework;

namespace Tests
{
    public class HintTests
    {
        [Test]
        public void TestHintHappyFlow()
        {
            PawnType[,] board = new PawnType[3, 3] {{PawnType.X, PawnType.None, PawnType.O},
                                                    {PawnType.X, PawnType.None, PawnType.O},
                                                    {PawnType.None, PawnType.None, PawnType.None}};

            BoardPos hint = MiniMax.BestMove(board, PawnType.O);
            Assert.AreEqual(new BoardPos(2, 2), hint);
        }
    }
}
