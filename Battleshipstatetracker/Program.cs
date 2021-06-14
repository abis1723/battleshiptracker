using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleshipstatetracker.utils;

namespace Battleshipstatetracker
{
	class Program
	{
		static void Main(string[] args)
		{
			var player1 = new BattleshipServices();
			var player2 = new BattleshipServices();
			var playGames = new PlayGames();

			bool success = true;
			bool quit = false;
			while (true)
			{
				while (true)
				{
					displayBoard(player1, player2, "player1");
					string[] input = validateUserInput(Console.ReadLine());

					if (input == null)
						quit = true;
					else
						success = playGames.playGames(player2, input);

					Console.ReadKey();

					if (success || quit) break;
				}
				if (quit) break;

				while (true)
				{
					displayBoard(player1, player2, "player2");
					string[] input = validateUserInput(Console.ReadLine());

					if (input == null)
						quit = true;
					else
						success = playGames.playGames(player1, input);

					Console.ReadKey();

					if (success || quit) break;
				}
				if (quit) break;

				if (player1.shipCount() == 0)
				{
					Console.WriteLine("Player-2 sunk all the battleships");
					break;
				}
				if (player2.shipCount() == 0)
				{
					Console.WriteLine("Player-1 sunk all the battleships");
					break;
				}

			}
			Console.ReadKey();
		}
		static void displayBoard(BattleshipServices player1, BattleshipServices player2, string player)
		{
			Console.Clear();
			Console.WriteLine("Quit anytime, type 'exit' into the coordinates\n");
			if (player == "player1")
			{
				Console.WriteLine("Player-1 Secret Board:");
				player1.HiddenBoard();
				Console.WriteLine("\nPlayer-2 Board:");
				player2.Board();
				Console.WriteLine("\nTurn for player-1: ");
			}
			else
			{
				Console.WriteLine("Player-2  Secret Board:");
				player2.HiddenBoard();
				Console.WriteLine("\nPlayer-1 Board:");
				player1.Board();
				Console.WriteLine("\nTurn for Player-2: ");
			}
			Console.Write("Enter(x,y) cordintes: ");
		}
		static string[] validateUserInput(string inputValue)
		{
			string[] input;
			try
			{
				if (inputValue.ToUpper() == "EXIT") return null;

				input = inputValue.Split(",".ToCharArray());
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
			return input;
		}
	}
}
