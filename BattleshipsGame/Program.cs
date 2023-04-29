using BattleshipsGame.Domain;

var gameSettings = new GameSettings()
{
    RandomSeed = null,
    DisplayShipPositions = true
};

var game = new Game(gameSettings);


Console.WriteLine(game.GetBoard());


//// Place battleship
//int bsRow = game.Random.Next(GRID_SIZE);
//int bsCol = game.Random.Next(GRID_SIZE - BATTLESHIP_SIZE + 1);
//for (int i = bsCol; i < bsCol + BATTLESHIP_SIZE; i++)
//{
//    var square = game.Board.Squares[bsRow, i];
//    square.Occupy();
//    game.Battleship.Squares.Add(square);
//}

//// Place destroyers
//for (int d = 0; d < 2; d++)
//{
//    int dsRow = game.Random.Next(GRID_SIZE);
//    int dsCol = game.Random.Next(GRID_SIZE - DESTROYER_SIZE + 1);
//    for (int i = dsCol; i < dsCol + DESTROYER_SIZE; i++)
//    {
//        var square = game.Board.Squares[dsRow, i];
//        square.Occupy();
//        game.Destroyers[d].Squares.Add(square);
//    }
//}

//int numSunk = 0;
//while (numSunk < NUM_SHIPS)
//{
//    // Display grid
//    //Console.WriteLine("  0123456789");
//    //for (int r = 0; r < GRID_SIZE; r++)
//    //{
//    //    Console.Write((char)('A' + r) + " ");
//    //    for (int c = 0; c < GRID_SIZE; c++)
//    //    {
//    //        var square = game.Board.Squares[r, c];
//    //        Console.Write(square.GetDisplayChar());
//    //    }
//    //    Console.WriteLine();
//    //}

//    // Get user input
//    Console.Write("Enter target: ");
//    string input = Console.ReadLine();
//    int row = input[0] - 'A';
//    int col = int.Parse(input.Substring(1));
//    if (row < 0 || row >= GRID_SIZE || col < 0 || col >= GRID_SIZE)
//    {
//        Console.WriteLine("Invalid input, please try again.");
//        continue;
//    }

//    // Check if target hits or misses
//    var squareClicked = game.Board.Squares[row, col];
//    if (squareClicked.State == SquareState.Empty)
//    {
//        Console.WriteLine("Miss!");
//        squareClicked.State = SquareState.Missed;
//    }
//    else
//    {
//        //Console
//    }
//        if (squareClicked.State == SquareState.Hit)
//        {
//            Console.WriteLine("Target already hit, please try again.");
//        }
//        else
//        {
//            Console.WriteLine("Hit!");
//            squareClicked.State = SquareState.Hit;

//            // Check if ship is sunk
//            if (game.Battleship.IsSunk())
//            {
//                Console.WriteLine("Battleship sunk!");
//                numSunk++;
//            }
//            else
//            {
//                foreach (var destroyer in game.Destroyers)
//                {
//                    if (destroyer.IsSunk())
//                    {
//                        Console.WriteLine("Destroyer sunk!");
//                        numSunk++;
//                        break;
//                    }
//                }
//            }
//        }
//    }
//}

Console.WriteLine("All ships sunk! Game over.");
Console.ReadLine();












