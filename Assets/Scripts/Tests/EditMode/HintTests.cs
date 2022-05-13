using NUnit.Framework;

namespace Tests
{
    public class HintTests
    {
        [Test]
        public void TestHintHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();

            for (int i = 0; i < 9; i++)
            {
                gameLogic.ConcludeTurn(gameLogic.Hint()); //hint is based on the "MiniMax" algorithm. if hint works correctly i sould always end in a draw.
                gameLogic.ChangeTurn();
            }

            Assert.AreEqual(GameState.Draw, gameLogic.GameState);
        }
    }
}
