using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame
{
    public class BattleArea : IBattleArea, IClone<IBattleArea>, ICoordinate
    {
        string _name;
        char _height;
        char _width;

        protected List<CoOrdinates> _acquiredCoordinates;
        protected List<IShip> _ships;
        protected CoOrdinateMediator _coordinateMediator;
        protected BattleAreaValidator _validator;

        public BattleArea() : this("New Battle Area1", 'A', '0')
        {
        }

        public BattleArea(string name, char height, char width)
        {
            this._name = name;
            this._height = height;
            this._width = width;
            this._acquiredCoordinates = new List<CoOrdinates>();
            this._ships = new List<IShip>();

            // CoordinateMediator is used to pass the updates co-ordinates from the ship object to the battle area object.
            //
            this._coordinateMediator = new CoOrdinateConcreteMediator();
            this._coordinateMediator.BattleArea = this;
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public char Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public char Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public List<IShip> AllShips
        {
            get { return this._ships; }
        }

        public virtual int GetRemainingShipCount()
        {
            // Get all the ships which are not destroyed.
            //
            return this._ships.Where(item => item.DamageStatus != DamageStatus.Destroyed).Count();
        }

        public virtual IShip GetShip(CoOrdinates coordinates)
        {
            // Get ship for the passsed coordinates
            //
            return this._ships.Find(item =>
            {
                return item.AcquiredCoordinates.Exists(item1 => item1.X == coordinates.X && item1.Y == coordinates.Y);
            });
        }

        public virtual IShip AddShip(IShip ship, CoOrdinates initialCoordinate)
        {
            // Add ship
            // 
            ship.InitialCoordinate = initialCoordinate;
            List<CoOrdinates> acquireCoordinates = ship.AcquireCoordinates(initialCoordinate);

            this._acquiredCoordinates.AddRange(acquireCoordinates);

            this._ships.Add(ship);

            // Set coordinate mediator for the ship
            // 
            this._coordinateMediator.Ship = ship;
            ship.RegisterCoordinateMediator(this._coordinateMediator);

            return ship;
        }

        public virtual bool Damage(CoOrdinates coordinates)
        {
            IShip ship = this.GetShip(coordinates);

            if (ship == null) return false;

            bool hit = ship.Damage(coordinates);

            // Update the coordinates
            //
            return hit;
        }

        public virtual bool UpdateCoordinates(CoOrdinates coordinate)
        {
            // Update the battle area co-ordinates
            // 
            var index = this._acquiredCoordinates.FindIndex(item => item.X == coordinate.X && item.Y == coordinate.Y);
            if (index < 0)
            {
                return false;
            }
            else
            {
                this._acquiredCoordinates[index] = coordinate;
                return true;
            }
        }

        public List<CoOrdinates> GetAcquireCoordinates()
        {
            return this._acquiredCoordinates;
        }

        public virtual IBattleArea Clone()
        {
            BattleArea battleArea = (BattleArea)this.MemberwiseClone();
            battleArea._acquiredCoordinates = new List<CoOrdinates>();
            battleArea._ships = new List<IShip>();
            battleArea._coordinateMediator = new CoOrdinateConcreteMediator();
            battleArea._coordinateMediator.BattleArea = battleArea;

            return battleArea;
        }
    }
}
