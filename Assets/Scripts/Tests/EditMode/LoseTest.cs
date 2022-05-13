using NUnit.Framework;

namespace Tests
{
    public class LoseTests
    {
        [Test]
        public void TestLoseHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();

            for (int i = 0; i < 3; i++)
            {
                gameLogic.ConcludeTurn(new Coordinate(i, 0));//placing adjacent X's=> O loses
            }

            Assert.AreNotEqual(GameState.WinnerO, gameLogic.GameState);
        }
    }
}
