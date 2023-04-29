namespace BattleshipsGame.Domain;

public class Ship
{
    public List<Square> Squares { get; }

    public readonly int Length;

    public Ship(int lengthOfShip)
    {
        Squares = new List<Square>();
        Length = lengthOfShip;
    }

    public bool IsSunk()
    {
        foreach (var square in Squares)
        {
            if (square.State != SquareState.Hit)
            {
                return false;
            }
        }
        return true;
    }

    internal void OccupySpace(List<Square> randomlyChosenFreeSpace)
    {
        foreach(var square in randomlyChosenFreeSpace)
        {
            square.Occupy();
            Squares.Add(square);
        }
    }
}
