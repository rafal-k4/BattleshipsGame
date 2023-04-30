using BattleshipsGame.Domain.Core;
using BattleshipsGame.Domain.Entities;

namespace BattleShipsGame.Domain.Tests.Entities;

public class SquareTests
{
    [Test]
    public void Constructor__ShouldAssignStateEmpty_AndCorrectCoordinates()
    {
        var expectedRow = 5;
        var expectedColumn = 6;
        var expectedState = Square.SquareState.Empty;

        var result = new Square(expectedRow, expectedColumn);

        using (new AssertionScope())
        {
            result.Row.Should().Be(expectedRow);
            result.Column.Should().Be(expectedColumn);
            result.State.Should().Be(expectedState);
        }
    }

    [Test]
    public void Occupy__ShouldChangeState_To_Occupied()
    {
        var square = new Square(2, 3);

        square.Occupy();

        square.State.Should().Be(Square.SquareState.Occupied);
    }

    [Test]
    public void HitAndGetResult__Should_ChangeStateToMiss_And_ReturnMiss__WhenStateWasEmpty()
    {
        var square = new Square(2, 3);

        var result = square.HitAndGetResult();

        result.Value.Should().BeOfType<Miss>();
        square.State.Should().Be(Square.SquareState.Missed);
    }

    [Test]
    public void HitAndGetResult__Should_ChangeStateToHit_And_ReturnHit__WhenStateWasOccupied()
    {
        var square = new Square(2, 3);
        square.Occupy();

        var result = square.HitAndGetResult();

        result.Value.Should().BeOfType<Hit>();
        square.State.Should().Be(Square.SquareState.Hit);
    }

    [Test]
    public void HitAndGetResult__Should_ReturnAlreadyHit__WhenStateWas_Missed()
    {
        var square = new Square(2, 3);

        var firstHit = square.HitAndGetResult();
        var secondHit = square.HitAndGetResult();

        secondHit.Value.Should().BeOfType<AlreadyHit>();
        square.State.Should().Be(Square.SquareState.Missed);
    }

    [Test]
    public void HitAndGetResult__Should_ReturnAlreadyHit__WhenStateWas_Hit()
    {
        var square = new Square(2, 3);
        square.Occupy();

        var firstHit = square.HitAndGetResult();
        var secondHit = square.HitAndGetResult();

        secondHit.Value.Should().BeOfType<AlreadyHit>();
        square.State.Should().Be(Square.SquareState.Hit);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void GetDisplayChar__ShouldReturn_ExpectedCharacter__WhenState_WasEmpty(bool shouldShowOccupiedField)
    {
        var expectedChar = '-';
        var square = new Square(2, 3);

        var displayChar = square.GetDisplayChar(shouldShowOccupiedField);

        displayChar.Should().Be(expectedChar);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void GetDisplayChar__ShouldReturn_ExpectedCharacter__WhenState_WasMissed(bool shouldShowOccupiedField)
    {
        var expectedChar = '*';
        var square = new Square(2, 3);
        square.HitAndGetResult();

        var displayChar = square.GetDisplayChar(shouldShowOccupiedField);

        displayChar.Should().Be(expectedChar);
    }

    [TestCase(true, 'O')]
    [TestCase(false, '-')]
    public void GetDisplayChar__ShouldReturn_ExpectedCharacter__WhenState_WasOccupied(bool shouldShowOccupiedField, char expectedChar)
    {
        var square = new Square(2, 3);
        square.Occupy();

        var displayChar = square.GetDisplayChar(shouldShowOccupiedField);

        displayChar.Should().Be(expectedChar);
    }

    [TestCase(true)]
    [TestCase(false)]
    public void GetDisplayChar__ShouldReturn_ExpectedCharacter__WhenState_WasHit(bool shouldShowOccupiedField)
    {
        var expectedChar = 'X';
        var square = new Square(2, 3);
        square.Occupy();
        square.HitAndGetResult();

        var displayChar = square.GetDisplayChar(shouldShowOccupiedField);

        displayChar.Should().Be(expectedChar);
    }
}
