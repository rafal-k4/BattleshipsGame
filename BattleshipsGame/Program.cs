using BattleshipsGame.Domain;

var gameSettings = new GameSettings()
{
    RandomSeed = null,
    DisplayShipPositions = false
};

var game = new Game(gameSettings);

Console.SetWindowSize(150, 50);

Console.WriteLine("Welcome in game of battleships, do your best and crush your opponent!");
Console.WriteLine();
Console.WriteLine("Rules:");
Console.WriteLine("In order to play this game, you need to type on your keyboard target coordinates (row letter followed by number column -> for example B2)");
Console.WriteLine("Your luck decides on if you hit 'X', miss '*' or sunk your opponents ship");
Console.WriteLine("There are 3 ships to destroy: 1x battleship (5 squares) and 2x destroyers (4 squares), good luck!");
Console.WriteLine();

var boardTopCursorPosition = Console.GetCursorPosition();

while (game.IsGameFinished() == false)
{
    Console.SetCursorPosition(boardTopCursorPosition.Left, boardTopCursorPosition.Top);

    Console.WriteLine(game.GetBoard());

    var boardBottomCursorPosition = Console.GetCursorPosition();
    var displayResultLine = (Left: boardBottomCursorPosition.Left, Top: boardBottomCursorPosition.Top + 1);

    ClearCurrentConsoleLine();
    Console.Write("Enter target: ");
    string? input = Console.ReadLine();

    Coordinates coordinates;
    while(Coordinates.TryFrom(input, out coordinates) == false)
    {
        Console.WriteLine("Wrong target, target should contain one letter and number, examples: [A0, B4, J9]");

        Console.SetCursorPosition(boardBottomCursorPosition.Left, boardBottomCursorPosition.Top);
        ClearCurrentConsoleLine();

        Console.Write("Enter target: ");
        input = Console.ReadLine();
    }

    Console.SetCursorPosition(displayResultLine.Left, displayResultLine.Top);
    ClearCurrentConsoleLine();
    Console.SetCursorPosition(displayResultLine.Left, displayResultLine.Top);

    var result = game.HitTarget(coordinates);

    Console.WriteLine(result.Match(
        hit => "Hit!",
        miss => "Miss!",
        sunk => "Ship sunk, keep up good job!",
        alreadyHit => "Target already hit, please try again.",
        coordinatesNotInRange => "Coordinates are out of the scope of the board, please provide a valid ones"));
}

Console.WriteLine("Congratulations! You won!");


static void ClearCurrentConsoleLine()
{
    int currentLineCursor = Console.CursorTop;
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, currentLineCursor);
}