using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipGame
{
    public class BattleShipGameStrategy : IGameSrategy
    {
        public void Play(Game game)
        {
            // launch missile
            //
            while (game.Player1.GetMissileCount() != 0 || game.Player2.GetMissileCount() != 0)
            {
                // Player1 Launches missile on battle area2
                //
                GameStatus gameStatus = this.LaunchMissile(game.Player1, game.Player2);

                if (gameStatus == GameStatus.Exit) return;

                // Player2 launches missile on battle area 1
                //
                gameStatus = this.LaunchMissile(game.Player2, game.Player1);

                if (gameStatus == GameStatus.Exit) return;
            }

            // post missile launch status of the game goes here.
            //
            this.PostLaunchMissileStatus(game.Player1, game.Player2);
        }

        private void PostLaunchMissileStatus(IPlayer player1, IPlayer player2)
        {
            if (player1.BattleArea.GetRemainingShipCount() > 0 && player2.BattleArea.GetRemainingShipCount() > 0)
            {
                // it is a tie match since both players has ships alive
                //
                this.Print(this.GetMessage(Constants.TieMessage));
            }
            else if (player1.BattleArea.GetRemainingShipCount() > 0)
            {
                // player 1 wins
                //
                this.Print(this.GetMessage(Constants.WinMessage, player1.Name));
            }
            else
            {
                // player 2 wins.//
                this.Print(this.GetMessage(Constants.WinMessage, player2.Name));
            }
        }

        private GameStatus LaunchMissile(IPlayer player, IPlayer targetPlayer)
        {
            bool hit = true;
            do
            {
                if (player.GetMissileCount() > 0)
                {
                    // Launch missile
                    //
                    hit = player.LaunchMissile(targetPlayer);

                    // Print hit/miss message
                    //
                    Print(this.GetHitMissMessage(player, hit));

                    if (targetPlayer.BattleArea.GetRemainingShipCount() == 0)
                    {
                        // no more ship remaining player wins, so exit the game.
                        //
                        this.Print(this.GetMessage(Constants.WinMessage, player.Name));
                        return GameStatus.Exit;
                    }
                }
                else
                {
                    // No more missiles left to launch.
                    //
                    hit = false;
                    this.Print(this.GetMessage(Constants.NoMoreMissileLeftMessage, player.Name));
                    break;
                }
            } while (hit);

            return GameStatus.Continue;
        }

        private string GetHitMissMessage(IPlayer player, bool hit)
        {
            string message = string.Empty;

            if (hit)
            {
                message = this.GetMessage(Constants.HitMessage, player.Name, player.LastTarget.ToString());
            }
            else
            {
                message = this.GetMessage(Constants.MissMessage, player.Name, player.LastTarget.ToString());
            }

            return message;
        }

        private string GetMessage(string message, string arg1 = "", string arg2 = "", string arg3 = "", string arg4 = "", string arg5 = "")
        {
            return string.Format(message, arg1, arg2, arg3, arg4, arg5);
        }

        private void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
