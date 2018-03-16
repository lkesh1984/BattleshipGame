using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BattleShipGame.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void AddMissileTest()
        {
            Player player = new Player();
            bool result = player.AddMissile(new CoOrdinates('1', '1'));

            Assert.AreEqual(player.GetMissileCount(), 1);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetMissileCountTest()
        {
            Player player = new Player();
            player.AddMissile(new CoOrdinates('1', '1'));

            Assert.AreEqual(player.GetMissileCount(), 1);
        }
    }
}