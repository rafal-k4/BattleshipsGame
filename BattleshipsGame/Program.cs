using BattleshipsGame.Domain;

var gameSettings = new GameSettings()
{
    RandomSeed = null,
    DisplayShipPositions = true
};

var game = new Game(gameSettings);


while (game.IsGameFinished() == false)
{
    //Console.Clear();

    Console.WriteLine(game.GetBoard());

    Console.Write("Enter target: ");
    string? input = Console.ReadLine();

    Coordinates coordinates;
    while(Coordinates.TryFrom(input, out coordinates) == false)
    {
        Console.WriteLine("Wrong target, target should contain one letter and number, examples: [A0, B4, J9]");
        input = Console.ReadLine();
    }

    var result = game.HitTarget(coordinates);

    Console.WriteLine(result.Match(
        hit => "Hit!",
        miss => "Miss!",
        sunk => "Ship sunk, keep up good job!",
        alreadyHit => "Target already hit, please try again.",
        coordinatesNotInRange => "Coordinates are out of the scope of the board, please provide a valid ones"));
}

Console.WriteLine("Congratulations! You won!");

