using NUnit.Framework;

namespace Tests
{
    public class UndoTests
    {
        [Test]
        public void TestUndoHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.ConcludeTurn(new BoardPos(1, 1));
            gameLogic.ConcludeTurn(new BoardPos(2, 1));
            gameLogic.Undo();
            Assert.AreEqual(gameLogic.board[1, 1], PawnType.None);
            Assert.AreEqual(gameLogic.board[2, 1], PawnType.None);
        }
        [Test]
        public void TestUndoNoMoves()
        {
            GameLogic gameLogic = new GameLogic();
            Assert.DoesNotThrow(gameLogic.Undo);
        }
    }
}
