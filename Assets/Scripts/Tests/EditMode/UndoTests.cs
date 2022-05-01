using NUnit.Framework;

namespace Tests
{
    public class UndoTests
    {
        [Test]
        public void TestUndoHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.ConcludeTurn(Coordinate.One);
            gameLogic.ConcludeTurn(new Coordinate(2, 1));
            gameLogic.Undo();
            Assert.AreEqual(gameLogic.Board[1, 1], PawnType.None);
            Assert.AreEqual(gameLogic.Board[2, 1], PawnType.None);
        }
        [Test]
        public void TestUndoNoMoves()
        {
            GameLogic gameLogic = new GameLogic();
            Assert.DoesNotThrow(gameLogic.Undo);
        }
    }
}
