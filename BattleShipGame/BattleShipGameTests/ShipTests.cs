using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class ShipTests
    {
        [TestMethod()]
        public void RegisterCoordinateMediatorTest()
        {
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void AcquireCoordinatesTest()
        {
            Ship ship = new Ship(1, 1, ShipType.TypeP);
            List<CoOrdinates> coordinates = ship.AcquireCoordinates(new CoOrdinates('1', 'A'));
            Assert.AreEqual(1, coordinates.Count);
            Assert.AreEqual(coordinates[0].X, '1');
            Assert.AreEqual(coordinates[0].Y, 'A');
        }

        [TestMethod()]
        public void DamageTest()
        {
            Ship ship = new Ship(1, 1, ShipType.TypeP);
            CoOrdinates coords = new CoOrdinates('1', 'A');
            coords.Value = 1;
            ship.AcquiredCoordinates.Add(coords);
            bool result = ship.Damage(new CoOrdinates('1', 'A'));

            Assert.IsTrue(result);
            Assert.AreEqual(ship.AcquiredCoordinates[0].Value, 0);
            Assert.AreEqual(ship.DamageStatus, DamageStatus.Destroyed);
        }

        [TestMethod()]
        public void UpdateCoordinatesTest()
        {
            Ship ship = new Ship(1, 1, ShipType.TypeP);

            ship.AcquiredCoordinates.Add(new CoOrdinates('1', 'A'));

            CoOrdinates coordinate = new CoOrdinates('1', 'A');
            coordinate.Value = 1;
            ship.UpdateCoordinates(coordinate);

            Assert.AreEqual(ship.AcquiredCoordinates[0].Value, 1);
        }

        [TestMethod()]
        public void GetCoordinatesTest()
        {
            Ship ship = new Ship(1,1, ShipType.TypeP);
            List<CoOrdinates> coordinates = ship.AcquireCoordinates(new CoOrdinates('1', 'A'));
            bool areEqual = coordinates[0].Equals(ship.AcquiredCoordinates[0]);
            Assert.IsTrue(areEqual);
        }
    }
}