using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame
{
    public abstract class CoOrdinateMediator : IClone<CoOrdinateMediator>
    {
        public IBattleArea BattleArea
        {
            get; set;
        }

        public IShip Ship
        {
            get; set;
        }

        public virtual CoOrdinateMediator Clone()
        {
            CoOrdinateMediator mediator = (CoOrdinateMediator)this.MemberwiseClone();
            return mediator;
        }

        public abstract bool UpdateCoordinate(ICoordinate entity, CoOrdinates coordinate);
    }

    public class CoOrdinateConcreteMediator : CoOrdinateMediator
    {
        public override bool UpdateCoordinate(ICoordinate entity, CoOrdinates coordinate)
        {
            return entity.UpdateCoordinates(coordinate);
        }
    }
}