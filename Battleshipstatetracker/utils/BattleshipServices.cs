using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Battleshipstatetracker.utils
{
    class BattleshipServices
    {
        public const int boardDimension = 10;

        boardStatus[,] board = new boardStatus[boardDimension, boardDimension];
        boardStatus[,] secretBoard = new boardStatus[boardDimension, boardDimension];

        public BattleshipServices()
        {
            createEmptyBoard();
            buildShip();
        }

        public void Board()
        {
            for (int x = 0; x < boardDimension; x++)
            {
                boardBoarder(x, boardDimension);

                for (int y = 0; y < boardDimension; y++)
                {
                    switch(board[x, y])
                        {
                        case boardStatus.empty:
                            Console.Write('-' + "  ") ;
                            break;
                        case boardStatus.ship:
                            Console.Write('S' + "  ");
                            break;
                        case boardStatus.hit:
                            Console.Write('H' + "  ");
                            break;
                        case boardStatus.miss:
                            Console.Write('X' + "  ");
                            break;
                        case boardStatus.sunk:
                            Console.Write('A' + "  ");
                            break;
                        default:
                            throw new InvalidOperationException("unknown status");
                    }
                }
                Console.Write("\n");
            }
        }

        public void HiddenBoard()
        {
            for (int x = 0; x < boardDimension; x++)
            {
                boardBoarder(x, boardDimension);

                for (int y = 0; y < boardDimension; y++)
                {
                    switch (secretBoard[x, y])
                    {
                        case boardStatus.empty:
                            Console.Write('-' + "  ");
                            break;
                        case boardStatus.ship:
                            Console.Write('S' + "  ");
                            break;
                        case boardStatus.hit:
                            Console.Write('H' + "  ");
                            break;
                        case boardStatus.miss:
                            Console.Write('X' + "  ");
                            break;
                        case boardStatus.sunk:
                            Console.Write('A' + "  ");
                            break;
                        default:
                            throw new InvalidOperationException("unknown status");
                    }
                }
                Console.Write("\n");
            }
        }

       
        private void createEmptyBoard()
        {
            for (int x = 0; x < boardDimension; x++)
            {
                for (int y = 0; y < boardDimension; y++)
                {
                    board[x, y] = boardStatus.empty;
                }
            }
        }
        public int shipCount()
        {
            int shipCount = 0;
            for (int x = 0; x < boardDimension; x++)
            {
                for (int y = 0; y < boardDimension; y++)
                {
                    if (secretBoard[x, y] == boardStatus.ship) shipCount++;
                }
            }
            return shipCount;
        }

        //build the random size ship 
        private void buildShip()
        {
            Random rand = new Random();
            int x = -1;
            int y = -1;
            boardStatus[,] tempBoard = new boardStatus[boardDimension, boardDimension];
            for (x = 0; x < boardDimension; x++) 
            {
                for (y = 0; y < boardDimension; y++)
                {
                    tempBoard[x, y] = secretBoard[x, y];
                }
            }

            int numShips = shipCount();
            while (true)
            {
                for (x = 0; x < boardDimension; x++) 
                {
                    for (y = 0; y < boardDimension; y++)
                    {
                        secretBoard[x, y] = tempBoard[x, y];
                    }
                }
                while (true) 
                {
                    x = rand.Next(boardDimension);
                    y = rand.Next(boardDimension);
                    if (secretBoard[x, y] == boardStatus.empty)
                    {
                        secretBoard[x, y] = boardStatus.ship;
                        break;
                    }
                }
                int size = rand.Next(boardDimension);
                int i = 1;
                if (rand.Next() % 2 == 0) // vertical alignment of ship
                {
                    for (i = 1; i < size; i++)
                    {
                        if (x - i < 0 || x - i > boardDimension)
                        {
                            break;
                        }
                        else if (secretBoard[x - i, y] == boardStatus.ship) break;

                        else secretBoard[x - i, y] = boardStatus.ship;
                    }
                    if (i <= size + 1)
                    {
                        for (int t = 1; t < (size + 1) - i; t++)
                        {
                            if (x + t < 0 || x + t > boardDimension)
                            {
                                break;
                            }
                            if (secretBoard[x + t, y] == boardStatus.ship) break;
                            secretBoard[x + t, y] = boardStatus.ship;
                        }
                    }
                }
                else // horizontal ship
                {
                    for (i = 1; i < size; i++)
                    {
                        if (y - i < 0 || y - i > boardDimension)
                        {
                            break;
                        }
                        else if (secretBoard[x, y - i] == boardStatus.ship) break;
                        else secretBoard[x, y - i] = boardStatus.ship;
                    }
                    if (i <= size + 1)
                    {
                        for (int t = 1; t < (size + 1) - i; t++)
                        {
                            if (y + t < 0 || y + t > boardDimension)
                            {
                                break;
                            }
                            if (secretBoard[x, y + t] == boardStatus.ship) break;
                            secretBoard[x, y + t] = boardStatus.ship;
                        }
                    }
                }
               if (shipCount() - numShips == size) break; 
            }
        }

        public void fireShot(int x, int y) 
        {
            int zeroValue = 0;
            switch (secretBoard[x - 1, y - 1])
            {
                case boardStatus.ship:
                    secretBoard[x - 1, y - 1] = boardStatus.hit;
                    board[x - 1, y - 1] = boardStatus.hit;
                    Console.WriteLine(boardStatus.hit);
                    break;
                case boardStatus.empty:
                    secretBoard[x - 1, y - 1] = boardStatus.miss;
                    board[x - 1, y - 1] = boardStatus.miss;
                    Console.WriteLine(boardStatus.miss);
                    break;
                case boardStatus.hit:
                case boardStatus.miss:
                case boardStatus.sunk:
                    zeroValue = 1 / zeroValue;
                    break;
                default:
                    Console.WriteLine("Error in firing shot");
                    break;
            }
        }

        private void boardBoarder(int x, int dimension)
        {
            if (x == 0)
            {
                Console.Write("   ");
                for (int i = 0; i < dimension; i++)
                {
                    Console.Write(i + 1 + "  ");
                }
                Console.WriteLine();
            }
            if (x < 9)
             Console.Write(x + 1 + "  ");
            else
                Console.Write(x + 1 + " ");
        }
    }
}
