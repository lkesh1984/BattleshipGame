using System.Collections.Generic;

namespace BattleShipGame
{
    public class BattleShipGameManager : IGameManager
    {
        IGame _game;
        const char Separator = ' ';

        public BattleShipGameManager()
        {
            this._game = new Game("BattleShip Game");
            this.ShipData = new List<string>();
            this.MissileData = new List<string>();
        }

        public bool StartGame()
        {
            bool valid = true;
            valid = this.PrepareBattleArea(this._game, this.BattleAreaData);
            if (!valid)
            {
                return false;
            }

            valid = this.AddBattleShips(this._game, this.ShipData);
            if (!valid)
            {
                return false;
            }

            this.AddPlayerMissiles(this._game, this.MissileData);

            this._game.Start();

            return true;
        }

        public string BattleAreaData
        {
            get; set;
        }

        public List<string> ShipData
        {
            get; set;
        }

        public List<string> MissileData
        {
            get; set;
        }

        protected bool PrepareBattleArea(IGame game, string battleAreaData)
        {
            string[] dimensionArr = battleAreaData.Split(Separator);
            char width;
            char height;

            if (!FetchBattleAreaData(dimensionArr, out height, out width))
            {
                return false;
            }

            game.SetBattleArea(height, width);

            return true;
        }

        protected virtual bool AddBattleShips(IGame game, List<string> battleShipData)
        {
            bool validateShipCount = this.validateBattleShipNumber(game.BattleArea1, battleShipData.Count);
            if (!validateShipCount)
            {
                System.Console.WriteLine("The BattleShipCount are more than battle area dimension");
                return false;
            }

            foreach (var item in battleShipData)
            {
                int width;
                int height;
                ShipType shipType;
                CoOrdinates ship1Coords;
                CoOrdinates ship2Coords;

                string[] battleShipDataArray = item.Split(Separator);
                if (!this.FetchShipData(this._game, battleShipDataArray, out height, out width, out shipType, out ship1Coords, out ship2Coords))
                {
                    return false;
                }

                game.AddShip(game.BattleArea1, height, width, ship1Coords, shipType);
                game.AddShip(game.BattleArea2, height, width, ship2Coords, shipType);
            }

            return true;
        }

        protected virtual void AddPlayerMissiles(IGame game, List<string> missileData)
        {
            string[] missileArr1 = missileData[0].Split(Separator);
            string[] missileArr2 = missileData[1].Split(Separator);

            this.AddMissile(game.Player1, missileArr1);
            this.AddMissile(game.Player2, missileArr2);
        }

        private bool FetchBattleAreaData(string[] dimensionArr, out char height, out char width)
        {
            height = 'A';
            width = '0';
            if (!char.TryParse(dimensionArr[0], out width))
            {
                System.Console.WriteLine("Battle area width is not valid.");
                return false;
            }

            if (!char.TryParse(dimensionArr[1], out height))
            {
                System.Console.WriteLine("Battle area height is not valid.");
                return false;
            }

            if (!ValidateBattleArea(height, width))
            {
                System.Console.WriteLine("Battle area is not valid");
                return false;
            }

            return true;
        }

        private bool FetchShipData(IGame game, string[] battleShipDataArray, out int height, out int width, out ShipType shipType, out CoOrdinates ship1Coords, out CoOrdinates ship2Coords)
        {
            string type = battleShipDataArray[0];
            width = int.Parse(battleShipDataArray[1]);
            height = int.Parse(battleShipDataArray[2]);
            string pos1 = battleShipDataArray[3];
            char y1 = pos1[0];
            char x1 = pos1[1];

            string pos2 = battleShipDataArray[4];
            char y2 = pos2[0];
            char x2 = pos2[1];

            shipType = ShipType.TypeP;
            ship1Coords = null;
            ship2Coords = null;

            bool validShipData = this.ValidateShipType(type);
            if (!validShipData)
            {
                System.Console.WriteLine("Ship type is not valid.");
                return false;
            }

            validShipData = this.ValidateShipPosLength(pos1);
            if (!validShipData)
            {
                System.Console.WriteLine("Ship1 position is not valid.");
                return false;
            }

            validShipData = this.ValidateShipData(game.BattleArea1, height, width, new CoOrdinates(x1, y1));

            if (!validShipData)
            {
                System.Console.WriteLine("Ship1 data is not valid.");
                return false;
            }

            validShipData = this.ValidateShipData(game.BattleArea2, height, width, new CoOrdinates(x2, y2));
            if (!validShipData)
            {
                System.Console.WriteLine("Ship2 data is not valid.");
                return false;
            }

            validShipData = this.ValidateShipPosLength(pos2);
            if (!validShipData)
            {
                System.Console.WriteLine("Ship2 position is not valid.");
                return false;
            }

            ship1Coords = new CoOrdinates(x1, y1);
            ship2Coords = new CoOrdinates(x2, y2);
            shipType = type == "P" ? ShipType.TypeP : ShipType.TypeQ;

            return true;

        }

        private bool ValidateShipPosLength(string pos)
        {
            return pos.Length == 2;
        }

        private bool ValidateShipType(string type)
        {
            return type == "P" || type == "Q";
        }

        private bool validateBattleShipNumber(IBattleArea battleArea, int battleShipCount)
        {
            int M = (battleArea.Width - '0');
            int N = (battleArea.Height - 'A' + 1);

            return battleShipCount <= (M * N);
        }

        private bool ValidateBattleArea(char height, char width)
        {
            return height >= 'A' && height <= 'Z' && width >= '1' && width < '9';
        }

        private bool ValidateShipData(IBattleArea battleArea, int height, int width, CoOrdinates coordinate)
        {
            return (height >= 1 && height <= (int)(battleArea.Height - 'A')) && (width >= 1 && width <= (int)(battleArea.Width - '0')) &&
                   (coordinate.X >= '1' && coordinate.X <= battleArea.Width) && (coordinate.Y >= 'A' && coordinate.Y <= battleArea.Height);
        }

        private void AddMissile(IPlayer player, string[] missileData)
        {
            foreach (var item in missileData)
            {
                char y = item[0];
                char x = item[1];

                player.AddMissile(new CoOrdinates(x, y));
            }
        }
    }
}
