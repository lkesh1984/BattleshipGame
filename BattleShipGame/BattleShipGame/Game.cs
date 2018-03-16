using System;

namespace BattleShipGame
{
    public class Game : IGame
    {
        IBattleArea _battleArea1;
        IBattleArea _battleArea2;
        IPlayer _player1;
        IPlayer _player2;
        IGameSrategy _strategy;

        public Game() : this("Game 1")
        {
        }

        public Game(string name)
        {
            this.Name = name;
            this.SetPlayers();
            this._strategy = new BattleShipGameStrategy();
        }

        public string Name
        {
            get;private set;
        }

        public IPlayer Player1
        {
            get { return this._player1; }
        }

        public IPlayer Player2
        {
            get { return this._player2; }
        }

        public IBattleArea BattleArea1
        {
            get { return this._battleArea1; }
        }

        public IBattleArea BattleArea2
        {
            get { return this._battleArea2; }
        }

        public virtual void Start()
        {
            this._strategy.Play(this);
        }

        public virtual void SetBattleArea(char height, char width)
        {
            this._battleArea1 = BattleShipGameFactory.Instance.GetBattleArea();
            this.InitializeBattleArea(this._battleArea1, "Battle Area1", height, width);
            
            this._player1.BattleArea = this._battleArea1;

            this._battleArea2  = (this._battleArea1 as IClone<IBattleArea>).Clone();
            this.InitializeBattleArea(this._battleArea2, "Battle Area2", height, width);
            this._player2.BattleArea = this._battleArea2;
        }

        public virtual void AddShip(IBattleArea battleArea, int height, int width, CoOrdinates initialCoordinates, ShipType type)
        {
            IShip ship = BattleShipGameFactory.Instance.GetShip();
            ship.Height = height;
            ship.Width = width;
            
            battleArea.AddShip(ship, initialCoordinates);
        }

        public virtual void ApplyGameStrategy(IGameSrategy strategy)
        {
            this._strategy = strategy;
        }

        public void SetPlayers()
        {
            this._player1 = BattleShipGameFactory.Instance.GetPlayer();
            this._player1.Name = "Player 1";

            this._player2 = (this._player1 as IClone<IPlayer>).Clone();
            this._player2.Name = "Player 2";
        }

        #region Private Members

        private void InitializeBattleArea(IBattleArea battleArea, string name, char height, char width)
        {
            battleArea.Name = name;
            battleArea.Height = height;
            battleArea.Width = width;
        }
        
        #endregion
    }
}
