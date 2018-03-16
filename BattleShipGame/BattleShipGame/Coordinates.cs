namespace BattleShipGame
{
    public class CoOrdinates : IClone<CoOrdinates>
    {
        char _x;
        char _y;

        public CoOrdinates(char x, char y)
        {
            this._x = x;
            this._y = y;
            this.Value = 0;
        }

        public char X
        {
            get { return this._x; }
        }

        public char Y
        {
            get { return this._y; }
        }

        public virtual int Value { get; set; }

        public override string ToString()
        {
            return string.Concat(this.Y, this.X);
        }

        public virtual CoOrdinates Clone()
        {
            CoOrdinates coordinates = (CoOrdinates)this.MemberwiseClone();

            return coordinates;
        }

        public override bool Equals(object obj)
        {
            CoOrdinates coordinates = obj as CoOrdinates;
            if (coordinates == null)
            {
                return false;
            }
            else
            {
                return this.X == coordinates.X && this.Y == coordinates.Y;
            }
        }

        public override int GetHashCode()
        {
            return string.Format("{0}_{1}", this.X, this.Y).GetHashCode();
        }
    }
}
