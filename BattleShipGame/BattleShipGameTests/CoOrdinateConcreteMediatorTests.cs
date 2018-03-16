using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class CoOrdinateConcreteMediatorTests
    {
        [TestMethod()]
        public void UpdateCoordinateTest()
        {
            CoOrdinateConcreteMediator coordinateMediator = new CoOrdinateConcreteMediator();
            BattleArea battleArea = new BattleArea();
            battleArea.GetAcquireCoordinates().Add(new CoOrdinates('X', 'Y'));

            CoOrdinates coords = new CoOrdinates('X', 'Y');
            coords.Value = 1;

            coordinateMediator.UpdateCoordinate(battleArea, coords);

            Assert.AreEqual(battleArea.GetAcquireCoordinates()[0].Value, 1);
        }
    }
}