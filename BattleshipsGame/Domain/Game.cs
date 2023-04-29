using System.Text;

namespace BattleshipsGame.Domain;

public class Game
{
    private Board Board { get; }
    private Random Random { get; }
    private List<Ship> Ships { get; }
    private GameSettings GameSettings { get; }

    public Game(GameSettings gameSettings)
    {
        Board = new Board();
        Random = new Random();
        Ships = new List<Ship>();

        PrepareGame();
        GameSettings = gameSettings;
    }

    public string GetBoard()
    {
        var board = new StringBuilder();

        var columns = $"  {string.Join(" ", Enumerable.Range(0, Board.BOARD_LENGTH))}";
        board.AppendLine(columns);
        
        for (int rowIndex = 0; rowIndex < Board.BOARD_HEIGHT; rowIndex++)
        {
            var boardRow = new StringBuilder();
            boardRow.Append((char)('A' + rowIndex) + " ");
            
            for (int colIndex = 0; colIndex < Board.BOARD_LENGTH; colIndex++)
            {
                var square = Board.Squares[rowIndex, colIndex];

                boardRow.Append($"{square.GetDisplayChar(GameSettings.DisplayShipPositions)} ");
            }
            board.AppendLine(boardRow.ToString());
        }

        return board.ToString();
    }

    private void PrepareGame()
    {
        CreateShips();
        PlaceShipsOnBoard();
    }

    private void PlaceShipsOnBoard()
    {
        foreach(var ship in Ships)
        {
            var directionValues = typeof(Direction).GetEnumValues();
            var randomIndex = Random.Next(directionValues.Length);
            var randomDirection = (Direction)directionValues.GetValue(randomIndex)!;

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

public enum Direction
{
    Vertical = 1,
    Horizontal
}
