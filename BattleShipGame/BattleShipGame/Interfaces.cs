using System.Collections.Generic;

namespace BattleShipGame
{
    public interface IBattleAreaValidator
    {
        BattleAreaValidatorErrorCode ValidateBattleArea(IBattleArea battleArea);
        BattleAreaValidatorErrorCode ValidateShip(IBattleArea battleArea, IShip ship);
    }

    public interface IClone<T>
    {
        T Clone();
    }

    public interface ICoordinate
    {
        bool UpdateCoordinates(CoOrdinates coordinate);
        List<CoOrdinates> GetAcquireCoordinates();
    }

    public interface IShip
    {
        CoOrdinates InitialCoordinate { get; set; }

        List<CoOrdinates> AcquiredCoordinates { get; set; }

        int Height { get; set; }

        int Width { get; set; }

        ShipType ShipType { get; set; }

        DamageStatus DamageStatus { get; }

        List<CoOrdinates> AcquireCoordinates(CoOrdinates initialCoordinate);

        bool Damage(CoOrdinates coordinate);

        void RegisterCoordinateMediator(CoOrdinateMediator mediator);
    }

    public interface IBattleArea
    {
        string Name { get; set; }

        char Height { get; set; }

        char Width { get; set; }

        List<IShip> AllShips { get; }

        IShip AddShip(IShip ship, CoOrdinates initialCoordinate);

        IShip GetShip(CoOrdinates coordinates);

        int GetRemainingShipCount();

        bool Damage(CoOrdinates coordinates);
    }

    public interface IPlayer
    {
        bool LaunchMissile(IPlayer targetPlayer);

        bool AddMissile(CoOrdinates coordinates);

        int GetMissileCount();

        string Name { get; set; }

        IBattleArea BattleArea { get; set; }

        CoOrdinates LastTarget { get; }
    }

    public interface IGame
    {
        string Name { get; }

        IPlayer Player1 { get; }

        IPlayer Player2 { get; }

        IBattleArea BattleArea1 { get; }

        IBattleArea BattleArea2 { get; }

        void Start();

        void SetBattleArea(char height, char width);

        void AddShip(IBattleArea battleArea, int height, int width, CoOrdinates initialCoordinates, ShipType type);

        void ApplyGameStrategy(IGameSrategy strategy);
    }

    public interface IGameManager
    {
        bool StartGame();
        string BattleAreaData { get; set; }
        List<string> ShipData { get; set; }
        List<string> MissileData { get; set; }
    }

    public interface IGameSrategy
    {
        void Play(Game game);
    }
}
