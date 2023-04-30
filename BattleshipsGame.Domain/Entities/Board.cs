using BattleshipsGame.Domain.Core;
using BattleshipsGame.Domain.ValueObjects;
using OneOf;
using System.Text;

namespace BattleshipsGame.Domain.Entities;

internal class Board
{
    internal enum Direction
    {
        Vertical = 1,
        Horizontal
    }

    internal const int BOARD_HEIGHT = 10;
    internal const int BOARD_LENGTH = 10;

    internal Square[,] Squares { get; }

    internal Board()
    {
        Squares = new Square[BOARD_HEIGHT, BOARD_LENGTH];
        for (int r = 0; r < BOARD_HEIGHT; r++)
        {
            for (int c = 0; c < BOARD_LENGTH; c++)
            {
                Squares[r, c] = new Square(r, c);
            }
        }
    }

    internal string GetBoardAsString(bool shouldDisplayShipPositions)
    {
        var board = new StringBuilder();

        var columns = $"  {string.Join(" ", Enumerable.Range(0, BOARD_LENGTH))}";
        board.AppendLine(columns);

        for (int rowIndex = 0; rowIndex < BOARD_HEIGHT; rowIndex++)
        {
            var boardRow = new StringBuilder();
            boardRow.Append((char)('A' + rowIndex) + " ");

            for (int colIndex = 0; colIndex < BOARD_LENGTH; colIndex++)
            {
                var square = Squares[rowIndex, colIndex];

                boardRow.Append($"{square.GetDisplayChar(shouldDisplayShipPositions)} ");
            }
            board.AppendLine(boardRow.ToString());
        }

        return board.ToString();
    }

    internal OneOf<Hit, Miss, AlreadyHit> HitTarget(Coordinates coordinates)
    {
        var targetSquare = Squares[coordinates.RowIndex, coordinates.ColumnIndex];

        return targetSquare.HitAndGetResult();
    }

    internal List<List<Square>> GetAvailableSpacesToPlaceShip(int length, Direction direction)
    {
        var rowsLength = Squares.GetLength(0);
        var colsLength = Squares.GetLength(1);

        return direction == Direction.Horizontal
            ? GetHorizontalAvailableSpaces(length, rowsLength, colsLength)
            : GetVerticalAvailableSpaces(length, rowsLength, colsLength);
    }

    private List<List<Square>> GetVerticalAvailableSpaces(int length, int rowsLength, int colsLength)
    {
        var availableSpaces = new List<List<Square>>();

        for (int rowIndex = 0; rowIndex < rowsLength - length; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colsLength; colIndex++)
            {
                var freeSquares = new List<Square>();
                bool canFit = true;
                for (int shipOffset = 0; shipOffset < length; shipOffset++)
                {
                    if (Squares[rowIndex + shipOffset, colIndex].State != Square.SquareState.Empty)
                    {
                        canFit = false;
                        break;
                    }

                    freeSquares.Add(Squares[rowIndex + shipOffset, colIndex]);
                }

                if (canFit)
                {
                    availableSpaces.Add(freeSquares);
                }
            }
        }

        return availableSpaces;
    }

    private List<List<Square>> GetHorizontalAvailableSpaces(int length, int rowsLength, int colsLength)
    {
        var availableSpaces = new List<List<Square>>();

        for (int rowIndex = 0; rowIndex < rowsLength; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colsLength - length; colIndex++)
            {
                var freeSquares = new List<Square>();
                bool canFit = true;
                for (int shipOffset = 0; shipOffset < length; shipOffset++)
                {
                    if (Squares[rowIndex, colIndex + shipOffset].State != Square.SquareState.Empty)
                    {
                        canFit = false;
                        break;
                    }

                    freeSquares.Add(Squares[rowIndex, colIndex + shipOffset]);
                }

                if (canFit)
                {
                    availableSpaces.Add(freeSquares);
                }
            }
        }

        return availableSpaces;
    }
}
