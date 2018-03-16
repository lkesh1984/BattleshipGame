using System.Collections.Generic;

namespace BattleShipGame
{
    public class Player : IPlayer, IClone<IPlayer>
    {
        string _name;
        IBattleArea _battleArea;
        CoOrdinates _lastTarget;
        Queue<CoOrdinates> _missile;

        public Player() : this("New Player 1")
        { }

        public Player(string name)
        {
            this._name = name;
            this._missile = new Queue<CoOrdinates>();
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public IBattleArea BattleArea
        {
            get { return this._battleArea; }
            set { this._battleArea = value; }
        }

        public CoOrdinates LastTarget
        {
            get { return this._lastTarget; }
        }

        public virtual bool LaunchMissile(IPlayer targetPlayer)
        {
            CoOrdinates coordinates = this._missile.Dequeue();
            this._lastTarget = coordinates;

            bool hit = targetPlayer.BattleArea.Damage(coordinates);

            return hit;
        }

        public virtual bool AddMissile(CoOrdinates coordinates)
        {
            this._missile.Enqueue(coordinates);
            return true;
        }

        public int GetMissileCount()
        {
            return this._missile.Count;
        }

        public virtual IPlayer Clone()
        {
            Player player = (Player)this.MemberwiseClone();
            player._missile = new Queue<CoOrdinates>();
            player._battleArea = null;
            player._lastTarget = null;
            return player;
        }
    }
}
