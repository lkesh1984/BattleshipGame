using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class BattleShipGameStrategyTests
    {
        [TestMethod()]
        public void PlayTest()
        {
            BattleShipGameStrategy gameStrategy = new BattleShipGameStrategy();

            Game game = new Game();
            game.SetBattleArea('E', '5');

            game.AddShip(game.BattleArea1, 1, 1, new CoOrdinates('1', 'A'), ShipType.TypeP);
            game.Player1.AddMissile(new CoOrdinates('1', 'A'));

            game.AddShip(game.BattleArea2, 1, 1, new CoOrdinates('1', 'A'), ShipType.TypeP);
            game.Player2.AddMissile(new CoOrdinates('1', 'A'));

            gameStrategy.Play(game);

            Assert.AreEqual(game.Player1.GetMissileCount(), 0);
            Assert.AreEqual(game.Player2.GetMissileCount(), 1);
            Assert.AreEqual(game.BattleArea1.GetRemainingShipCount(), 1);
            Assert.AreEqual(game.BattleArea2.GetRemainingShipCount(), 0);
        }
    }
}