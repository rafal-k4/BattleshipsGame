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
        if (!Squares.Any())
            throw new ArgumentException("Squares were not assigned");

        foreach (var square in Squares)
        {
            if (square.State != Square.SquareState.Hit)
            {
                return false;
            }
        }
        return true;
    }

    internal void OccupySpace(List<Square> squareToOccupy)
    {
        if (squareToOccupy.Count != Length)
            throw new ArgumentException($"Square count {{{squareToOccupy.Count}}} does not correspond to expected Length {{{Length}}} of this ship");

        foreach (var square in squareToOccupy)
        {
            square.Occupy();
            Squares.Add(square);
        }
    }
}
