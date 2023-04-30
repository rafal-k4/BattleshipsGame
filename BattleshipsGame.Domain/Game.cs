using BattleshipsGame.Domain.Core;
using BattleshipsGame.Domain.Entities;
using BattleshipsGame.Domain.ValueObjects;
using OneOf;

namespace BattleshipsGame.Domain;

public class Game
{
    internal List<Ship> Ships { get; }

    private Board Board { get; }
    private Random Random { get; }
    private GameSettings GameSettings { get; }

    public Game(GameSettings gameSettings)
    {
        Board = new Board();
        Random = gameSettings.RandomSeed.HasValue
            ? new Random(gameSettings.RandomSeed.Value)
            : new Random();
        Ships = new List<Ship>();
        GameSettings = gameSettings;

        PrepareGame();
    }

    public bool IsGameFinished()
    {
        return Ships.All(x => x.IsSunk());
    }

    public string GetBoard()
    {
        return Board.GetBoardAsString(GameSettings.DisplayShipPositions);
    }

    public OneOf<Hit, Miss, Sunk, AlreadyHit, CoordinatesNotInRange, InvalidCoordinates> HitTarget(string? coordinates)
    {
        if (!Coordinates.TryFrom(coordinates, out var coordinatesValue))
            return new InvalidCoordinates();

        if (coordinatesValue.RowIndex > Board.BOARD_HEIGHT - 1 || coordinatesValue.ColumnIndex > Board.BOARD_LENGTH - 1)
            return new CoordinatesNotInRange();

        return Board
            .HitTarget(coordinatesValue)
            .Match<OneOf<Hit, Miss, Sunk, AlreadyHit, CoordinatesNotInRange, InvalidCoordinates>>(
                hit =>
                {
                    var targetedShip = GetShipByCoordinate(coordinatesValue);
                    return targetedShip.IsSunk()
                        ? new Sunk()
                        : hit;
                },
                miss => miss,
                alreadyHit => alreadyHit);
    }

    private Ship GetShipByCoordinate(Coordinates coordinates)
    {
        return Ships.Single(x => x.Squares.Any(x => x.Row == coordinates.RowIndex && x.Column == coordinates.ColumnIndex));
    }

    private void PrepareGame()
    {
        CreateShips();
        PlaceShipsOnBoard();
    }

    private void PlaceShipsOnBoard()
    {
        foreach (var ship in Ships)
        {
            var directionValues = typeof(Board.Direction).GetEnumValues();
            var randomIndex = Random.Next(directionValues.Length);
            var randomDirection = (Board.Direction)directionValues.GetValue(randomIndex)!;

            var freeSpaces = Board.GetAvailableSpacesToPlaceShip(ship.Length, randomDirection);

            var randomlyChosenFreeSpace = freeSpaces[Random.Next(freeSpaces.Count)];

            ship.OccupySpace(randomlyChosenFreeSpace);
        }
    }

    private void CreateShips()
    {
        const int lengthOfBattleShip = 5;
        const int lengthOfDestroyer = 4;

        var battleship = new Ship(lengthOfBattleShip);
        var destroyer1 = new Ship(lengthOfDestroyer);
        var destroyer2 = new Ship(lengthOfDestroyer);

        Ships.Add(battleship);
        Ships.Add(destroyer1);
        Ships.Add(destroyer2);
    }
}