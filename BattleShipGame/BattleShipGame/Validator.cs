using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame
{
    public abstract class BattleAreaValidator
    {
        protected BattleAreaValidator _next;
        public void SetNextValidator(BattleAreaValidator battleAreaValidator)
        {
            this._next = battleAreaValidator;
        }

        public abstract BattleAreaValidatorErrorCode Validate(IBattleArea battleArea, IShip ship);
    }

    public class DimensionValidator : BattleAreaValidator
    {
        public override BattleAreaValidatorErrorCode Validate(IBattleArea battleArea, IShip ship)
        {
            if (battleArea.Width < '1' || battleArea.Width > '9' || battleArea.Height < 'A' || battleArea.Height > 'Z')
            {
                return BattleAreaValidatorErrorCode.InvalidDimension;
            }

            if (this._next != null)
            {
                return this._next.Validate(battleArea, ship);
            }

            return BattleAreaValidatorErrorCode.NoError;
        }
    }

    class CheckLimitValidtor : BattleAreaValidator
    {
        public override BattleAreaValidatorErrorCode Validate(IBattleArea battleArea, IShip ship)
        {
            bool checkLimits = this.CheckLimits(battleArea, ship.AcquiredCoordinates);
            if (!checkLimits)
            {
                return BattleAreaValidatorErrorCode.ExceedLimit;
            }

            if (this._next != null)
            {
                return this._next.Validate(battleArea, ship);
            }

            return BattleAreaValidatorErrorCode.NoError;
        }

        private bool CheckLimits(IBattleArea battleArea, List<CoOrdinates> coordinates)
        {
            //List<CoOrdinates> coordinates = (battleArea as ICoordinate).GetCoordinates();

            var result = coordinates.Where(item =>
            {
                if (item.X >= '1' && item.X <= battleArea.Width && item.Y >= 'A' && item.Y <= battleArea.Height)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });

            return result.Count() == coordinates.Count;
        }
    }

    class AlreadyAcquiredCoordinatesValidator : BattleAreaValidator
    {
        public override BattleAreaValidatorErrorCode Validate(IBattleArea battleArea, IShip ship)
        {
            List<CoOrdinates> coordinates = (battleArea as ICoordinate).GetAcquireCoordinates();

            int count = coordinates.Where(item => ship.AcquiredCoordinates.Find(item1 => item1.X == item.X && item1.Y == item.Y) != null).Count();

            if (count > 0)
            {
                return BattleAreaValidatorErrorCode.AlreadyAcquired;
            }

            if (this._next != null)
            {
                return this._next.Validate(battleArea, ship);
            }

            return BattleAreaValidatorErrorCode.NoError;
        }
    }
}

//private BattleAreaValidatorErrorCode ValidateBattleArea(IBattleArea battleArea)
//{
//    BattleAreaValidator validator = new DimensionValidator();
//    return validator.Validate(battleArea, null);
//}

//private BattleAreaValidatorErrorCode ValidateBattleAreaShip(IBattleArea battleArea, IShip ship)
//{
//    BattleAreaValidator limitValidator = new CheckLimitValidtor();
//    BattleAreaValidator coordinateValidator = new AlreadyAcquiredCoordinatesValidator();
//}