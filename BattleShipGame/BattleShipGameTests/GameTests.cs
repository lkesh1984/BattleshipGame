using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void AddShipTest()
        {
            BattleArea battleArea = new BattleArea();
            Game game = new Game();
            game.AddShip(battleArea, 1, 1, new CoOrdinates('1', 'A'), ShipType.TypeP);
            Assert.AreEqual(battleArea.AllShips.Count, 1);
        }

        [TestMethod()]
        public void SetBattleAreaTest()
        {
            Game game = new Game();
            game.SetBattleArea('E', '5');

            Assert.AreEqual(game.BattleArea1.Height, 'E');
            Assert.AreEqual(game.BattleArea1.Width, '5');

            Assert.AreEqual(game.BattleArea2.Height, 'E');
            Assert.AreEqual(game.BattleArea2.Width, '5');
        }

        [TestMethod()]
        public void StartTest()
        {
            Game game = new Game();
            game.SetBattleArea('E', '5');

            game.AddShip(game.BattleArea1, 1, 1, new CoOrdinates('1', 'A'), ShipType.TypeP);
            game.Player1.AddMissile(new CoOrdinates('1', 'A'));

            game.AddShip(game.BattleArea2, 1, 1, new CoOrdinates('1', 'A'), ShipType.TypeP);
            game.Player2.AddMissile(new CoOrdinates('1', 'A'));

            game.Start();

            Assert.AreEqual(game.Player1.GetMissileCount(), 0);
            Assert.AreEqual(game.Player2.GetMissileCount(), 1);
            Assert.AreEqual(game.BattleArea1.GetRemainingShipCount(), 1);
            Assert.AreEqual(game.BattleArea2.GetRemainingShipCount(), 0);
        }
    }
}