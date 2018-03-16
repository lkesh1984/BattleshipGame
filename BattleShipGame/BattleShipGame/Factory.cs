using System;

namespace BattleShipGame
{
    public abstract class AbstractGameFactory
    {
        public abstract IShip GetShip();
        public abstract IBattleArea GetBattleArea();
        public abstract IPlayer GetPlayer();
    }

    public sealed class BattleShipGameFactory : AbstractGameFactory
    {
        static readonly Lazy<BattleShipGameFactory> _gameFactory = new Lazy<BattleShipGameFactory>(() =>
        {
            return new BattleShipGameFactory();
        });

        private BattleShipGameFactory() { }

        public static BattleShipGameFactory Instance
        {
            get { return _gameFactory.Value; }
        }

        public override IBattleArea GetBattleArea()
        {
            return new BattleArea();
        }

        public override IPlayer GetPlayer()
        {
            return new Player();
        }

        public override IShip GetShip()
        {
            return new Ship();
        }
    }
}
