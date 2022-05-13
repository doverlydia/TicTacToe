using NUnit.Framework;

namespace Tests
{
    public class WinTests
    {
        [Test]
        public void TestWinHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();

            for (int i = 0; i < 3; i++)
            {
                gameLogic.ConcludeTurn(new Coordinate(i,0)); //placing adjacent X's=> X win
            }

            Assert.AreEqual(GameState.WinnerX, gameLogic.GameState);
        }
    }
}
