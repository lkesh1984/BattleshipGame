using System.Collections.Generic;
using System.Linq;

namespace BattleShipGame
{
    public class Ship : IShip, IClone<IShip>, ICoordinate
    {
        int _height;
        int _width;
        ShipType _type;
        CoOrdinateMediator _coordinateMediator;

        protected CoOrdinates _initialCoordinates;
        protected List<CoOrdinates> _acquiredCoordinate;
        protected DamageStatus _damageStatus = DamageStatus.NoDamage;

        public CoOrdinates InitialCoordinate
        {
            get { return this._initialCoordinates; }
            set { this._initialCoordinates = value; }
        }

        public List<CoOrdinates> AcquiredCoordinates
        {
            get { return this._acquiredCoordinate; }
            set { this._acquiredCoordinate = value; }
        }

        public int Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public int Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public ShipType ShipType
        {
            get { return this._type; }
            set { this._type = value; }
        }

        public DamageStatus DamageStatus
        {
            get { return this._damageStatus; }
            private set { this._damageStatus = value; }
        }

        public Ship() : this(0, 0, ShipType.TypeP)
        { }

        public Ship(int height, int width, ShipType type)
        {
            this._height = height;
            this._width = width;
            this._type = type;
            this._initialCoordinates = new CoOrdinates('0', '0');
            this._acquiredCoordinate = new List<CoOrdinates>();
        }

        public void RegisterCoordinateMediator(CoOrdinateMediator mediator)
        {
            // Set the coordinate mediator.
            // 
            this._coordinateMediator = mediator;
        }

        public virtual List<CoOrdinates> AcquireCoordinates(CoOrdinates initialCoordinate)
        { 
            List<CoOrdinates> acquired = new List<CoOrdinates>();

            // Occupy the coordinates for the ship.
            //
            acquired = this.AcquireCoordinatesInternal(this._height, this._width, this._type, initialCoordinate);
            this._acquiredCoordinate = acquired;
            this._initialCoordinates = initialCoordinate;

            return acquired;
        }

        public virtual bool Damage(CoOrdinates coordinate)
        {
            // Damage the passed co-ordinates of the ship
            //
            var index = this._acquiredCoordinate.FindIndex(item => item.X == coordinate.X && item.Y == coordinate.Y);
            var result = this._acquiredCoordinate[index];

            if (result.Value > 0)
            {
                result.Value--;

                // Update the coordinates
                // 
                this.UpdateCoordinates(result);

                // Set damage status of the ship
                // 
                this.SetDamageStatus();

                return true;
            }

            return false;
        }

        public virtual IShip Clone()
        {
            Ship ship = (Ship)this.MemberwiseClone();
            ship._acquiredCoordinate = new List<CoOrdinates>();
            ship._coordinateMediator = null;
            ship._initialCoordinates = new CoOrdinates('0', '0');

            return ship;
        }

        public virtual bool UpdateCoordinates(CoOrdinates coordinate)
        {
            // Update the coordinates
            // 
            var index = this._acquiredCoordinate.FindIndex(item => item.X == coordinate.X && item.Y == coordinate.Y);
            if (index < 0)
            {
                return false;
            }

            this._acquiredCoordinate[index] = coordinate;

            // Update battle area coordinates as well usin he coordinate mediator.
            // 
            if (this._coordinateMediator != null)
            {
                return this._coordinateMediator.UpdateCoordinate((ICoordinate)this._coordinateMediator.BattleArea, coordinate);
            }
            else
            {
                return true;
            }
        }

        public List<CoOrdinates> GetAcquireCoordinates()
        {
            return this._acquiredCoordinate;
        }

        private void SetDamageStatus()
        {
            // Set the damage status of the ship
            // 
            if (this.IsDestroyed())
            {
                this._damageStatus = DamageStatus.Destroyed;
            }
            else
            {
                this._damageStatus = DamageStatus.Damage;
            }
        }

        private bool IsDestroyed()
        {
            // Checks if the ship is destroyed or not
            // 
            return this._acquiredCoordinate.Where(item => item.Value == 0).Count() == this._acquiredCoordinate.Count;
        }

        private List<CoOrdinates> AcquireCoordinatesInternal(int height, int width, ShipType type, CoOrdinates initialCoordinate)
        {
            // Acquire the coordinates for the ship.
            // 
            List<CoOrdinates> acquired = new List<CoOrdinates>();

            for (int i = 0; i < height; i++)
            {
                char y = (char)(initialCoordinate.Y + i);
                for (int j = 0; j < width; j++)
                {
                    char x = (char)(initialCoordinate.X + j);
                    CoOrdinates coords = new CoOrdinates(x, y);
                    coords.Value = (int)type * 1;

                    acquired.Add(coords);
                }
            }

            return acquired;
        }
    }
}
