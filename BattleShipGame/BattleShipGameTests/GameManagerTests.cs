using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class GameManagerTests
    {
        [TestMethod()]
        public void SetBattleAreaDataTest()
        {
            BattleShipGameManager manager = new BattleShipGameManager();

            manager.BattleAreaData = "5 E";

            List<string> shipData = new List<string>();
            shipData.Add("Q 1 1 A1 B2");
            shipData.Add("P 2 1 D4 C3");
            manager.ShipData = shipData;

            List<string> missileData = new List<string>();
            missileData.Add("A1 B2 B2 B3");
            missileData.Add("A1 B2 B3 A1 D1 E1 D4 D4 D5 D5");
            manager.MissileData = missileData;

            bool status = manager.StartGame();

            Assert.AreEqual(status, true);
        }
    }
}