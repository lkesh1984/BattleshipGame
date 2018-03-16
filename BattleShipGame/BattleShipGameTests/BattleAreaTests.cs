using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class BattleAreaTests
    {
        [TestMethod()]
        public void GetRemainingShipCountTest()
        {
            BattleArea battleArea = new BattleArea();
            battleArea.AllShips.Add(new Ship(1, 1, ShipType.TypeP));

            int count = battleArea.GetRemainingShipCount();

            Assert.AreEqual(count, 1);
        }

        [TestMethod()]
        public void GetShipTest()
        {
            BattleArea battleArea = new BattleArea();

            IShip ship = new Ship(1, 1, ShipType.TypeP);
            ship.AcquiredCoordinates.Add(new CoOrdinates('1', 'A'));
            battleArea.AllShips.Add(ship);

            IShip ship1 = battleArea.GetShip(new CoOrdinates('1', 'A'));

            Assert.IsNotNull(ship1);
        }

        [TestMethod()]
        public void AddShipTest()
        {
            BattleArea battleArea = new BattleArea();

            IShip ship = new Ship(1, 1, ShipType.TypeP);
            Assert.AreEqual(battleArea.AllShips.Count, 0);
            battleArea.AddShip(ship, new CoOrdinates('1', 'A'));

            bool hasCoords = battleArea.GetAcquireCoordinates().Count > 0 && 
                             battleArea.GetAcquireCoordinates()[0].X == '1' && 
                             battleArea.GetAcquireCoordinates()[0].Y == 'A';

            Assert.AreEqual(hasCoords, true);

            Assert.AreEqual(battleArea.AllShips.Count, 1);
        }

        [TestMethod()]
        public void DamageTest()
        {
            BattleArea battleArea = new BattleArea();
            IShip ship = new Ship(1, 1, ShipType.TypeP);
            CoOrdinates coords = new CoOrdinates('1', 'A');
            coords.Value = 1;
            ship.AcquiredCoordinates.Add(coords);
            battleArea.AddShip(ship, new CoOrdinates('1', 'A'));
            bool result = battleArea.Damage(new CoOrdinates('1', 'A'));
            Assert.AreEqual(result, true);

            IShip ship1 = battleArea.AllShips[0];
            Assert.AreEqual(ship1.AcquiredCoordinates[0].Value, 0);
            Assert.AreEqual(ship1.DamageStatus, DamageStatus.Destroyed);
            Assert.AreEqual(battleArea.GetAcquireCoordinates()[0].Value, 0);

            battleArea.AllShips.Add(ship);
        }

        [TestMethod()]
        public void UpdateCoordinatesTest()
        {
            BattleArea battleArea = new BattleArea();
            CoOrdinates coords = new CoOrdinates('1', 'Y');
            coords.Value = 1;
            battleArea.GetAcquireCoordinates().Add(coords);

            battleArea.UpdateCoordinates(new CoOrdinates('1', 'Y'));

            Assert.AreEqual(battleArea.GetAcquireCoordinates()[0].Value, 0);
        }
    }
}