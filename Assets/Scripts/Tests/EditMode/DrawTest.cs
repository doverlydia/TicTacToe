using NUnit.Framework;

namespace Tests
{
    public class DrawTests
    {
        [Test]
        public void TestDrawHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();

            for (int i = 0; i < 9; i++)
            {
                gameLogic.ConcludeTurn(gameLogic.Hint()); //always using the "MiniMax" algorithm which means always ending in a draw
                gameLogic.ChangeTurn();
            }

            Assert.AreEqual(GameState.Draw, gameLogic.gameState);
        }
    }
}
