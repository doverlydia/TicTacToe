using NUnit.Framework;

namespace Tests
{
    public class HintTests
    {
        [Test]
        public void TestHintHappyFlow()
        {
            GameLogic gameLogic = new GameLogic();

            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());
            gameLogic.ChangeTurn();
            gameLogic.ConcludeTurn(gameLogic.Hint());

            Assert.AreEqual(GameState.Draw, gameLogic.gameState);
        }
    }
}
