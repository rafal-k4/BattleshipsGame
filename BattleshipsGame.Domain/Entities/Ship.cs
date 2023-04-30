namespace BattleshipsGame.Domain.Entities;

internal class Ship
{
    internal List<Square> Squares { get; }

    internal readonly int Length;

    internal Ship(int lengthOfShip)
    {
        Squares = new List<Square>();
        Length = lengthOfShip;
    }

    internal bool IsSunk()
    {
        foreach (var square in Squares)
        {
            if (square.State != Square.SquareState.Hit)
            {
                return false;
            }
        }
        return true;
    }

    internal void OccupySpace(List<Square> randomlyChosenFreeSpace)
    {
        foreach (var square in randomlyChosenFreeSpace)
        {
            square.Occupy();
            Squares.Add(square);
        }
    }
}
