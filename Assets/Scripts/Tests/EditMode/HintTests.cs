using NUnit.Framework;

namespace Tests
{
    public class HintTests
    {
        [Test]
        public void TestHintHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.ConcludeTurn(Coordinate.Zero);
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(new Coordinate(0,2));
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(new Coordinate(0,1));
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(new Coordinate(2,1));
            gameLogic.ChangeTurn();
            Assert.AreEqual(new Coordinate(0, 2), gameLogic.Hint());
        }
    }
}
