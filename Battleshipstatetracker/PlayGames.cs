using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleshipstatetracker.utils;

namespace Battleshipstatetracker
{
    internal class PlayGames
    {
        internal bool playGames(BattleshipServices player, string[] input)
        {
            bool success = true;
            try
            {
                int x = int.Parse(input[0]);
                int y = int.Parse(input[1]);

                player.fireShot(x, y);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid coordinates");
                success = false;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Already fired the position");
                success = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                success = false;
            }
            return success;
        }
    }
}
