namespace BattleShipGame
{
    public enum BattleAreaValidatorErrorCode
    {
        NoError,
        InvalidDimension,
        ExceedLimit,
        AlreadyAcquired,
        Other
    }

    public enum ShipType
    {
        TypeP = 1,
        TypeQ = 2
    }

    public enum DamageStatus
    {
        NoDamage,
        Damage,
        Destroyed
    }

    public enum GameStatus
    {
        Exit,
        Continue
    }
}
