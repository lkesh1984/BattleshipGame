using System;

namespace BattleShipGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IGameManager gameManager = new BattleShipGameManager();
            
            // Get the battle area dimensions
            // 
            string dimension = Console.ReadLine();
            gameManager.BattleAreaData = dimension;

            // Get the number of ships
            // 
            int numberOfShip = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfShip; i++)
            {
                string battleShipData = Console.ReadLine();
                gameManager.ShipData.Add(battleShipData);
            }
            
            // Get the player1 misslle data
            // 
            string player1MissileSeq = Console.ReadLine();

            // Get the player2 misslle data
            // 
            string player2MissileSeq = Console.ReadLine();

            // Parse and set player1 missile seq
            // 
            gameManager.MissileData.Add(player1MissileSeq);

            // Parse and set player2 missile seq
            // 
            gameManager.MissileData.Add(player2MissileSeq);

            // Start the game.
            // 
            gameManager.StartGame();
        }
    }
}
