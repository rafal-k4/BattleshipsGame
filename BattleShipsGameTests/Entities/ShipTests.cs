using BattleshipsGame.Domain.Entities;

namespace BattleShipsGame.Domain.Tests.Entities;

public class ShipTests
{
    [Test]
    public void OccupySpace__Should_AssignProvidedSquares()
    {
        var expectedShipLength = 3;
        var ship = new Ship(expectedShipLength);
        
        ship.OccupySpace(new List<Square>
        {
            new Square(0,1),
            new Square(0,2),
            new Square(0,3)
        });

        ship.Squares.Count.Should().Be(expectedShipLength);
    }

    [Test]
    public void OccupySpace__Should_ThrowException__WhenProvidedSquareToOccupy_Does_NotMatch_ShipLength()
    {
        var expectedShipLength = 5;
        var ship = new Ship(expectedShipLength);
        
        var act = () => ship.OccupySpace(new List<Square>
        {
            new Square(0,1),
            new Square(0,2),
            new Square(0,3)
        });

        act.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Square count {3} does not correspond to expected Length {5} of this ship");
    }

    [Test]
    public void IsSunk__Should_ReturnFalse__WhenShip_IsPartiallyHit()
    {
        var ship = new Ship(3);
        var squareToOccupy = new List<Square>
        {
            new Square(0,1),
            new Square(0,2),
            new Square(0,3)
        };
        ship.OccupySpace(squareToOccupy);

        squareToOccupy[1].HitAndGetResult();
        squareToOccupy[2].HitAndGetResult();

        var isSunk = ship.IsSunk();

        isSunk.Should().BeFalse();
    }

    [Test]
    public void IsSunk__Should_ReturnFalse__WhenShip_WasNotHit()
    {
        var ship = new Ship(3);
        var squareToOccupy = new List<Square>
        {
            new Square(0,1),
            new Square(0,2),
            new Square(0,3)
        };
        ship.OccupySpace(squareToOccupy);

        var isSunk = ship.IsSunk();

        isSunk.Should().BeFalse();
    }

    [Test]
    public void IsSunk__Should_ReturnTrue__WhenShip_IsFullyHit()
    {
        var ship = new Ship(3);
        var squareToOccupy = new List<Square>
        {
            new Square(0,1),
            new Square(0,2),
            new Square(0,3)
        };
        ship.OccupySpace(squareToOccupy);

        squareToOccupy[0].HitAndGetResult();
        squareToOccupy[1].HitAndGetResult();
        squareToOccupy[2].HitAndGetResult();

        var isSunk = ship.IsSunk();

        isSunk.Should().BeTrue();
    }

    [Test]
    public void IsSunk__Should_ThrowException__WhenSquares_WereNotPreviouslyAssigned()
    {
        var ship = new Ship(3);
        
        var act = () => ship.IsSunk();

        act.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Squares were not assigned");
    }
}
