using BattleshipsGame.Domain.ValueObjects;

namespace BattleShipsGame.Domain.Tests.ValueObjects;

public class CoordinatesTests
{
    [TestCase("A0", 0, 0)]
    [TestCase("  A0   ", 0, 0)]
    [TestCase("B3", 1, 3)]
    [TestCase("c11", 2, 11)]
    [TestCase("d3", 3, 3)]
    public void TryFrom__ShouldReturnTrue_And_CreateCoordinateInstance__WhenInputIsValid(string input, int rowIndex, int columnIndex)
    {
        var validResult = Coordinates.TryFrom(input, out var coordinates);

        using (new AssertionScope())
        {
            coordinates.Should().NotBeNull();
            validResult.Should().Be(true);
            coordinates.RowIndex.Should().Be(rowIndex);
            coordinates.ColumnIndex.Should().Be(columnIndex);
        }
    }

    [TestCase("xxxx")]
    [TestCase("11")]
    [TestCase("21a")]
    [TestCase("..")]
    [TestCase("__")]
    [TestCase("  ")]
    [TestCase("")]
    [TestCase(null)]
    public void TryFrom__ShouldReturnFalse_And_OutputsNull__WhenInput_IsInvalid(string? input)
    {
        var validResult = Coordinates.TryFrom(input, out var coordinates);

        using (new AssertionScope())
        {
            coordinates.Should().BeNull();
            validResult.Should().Be(false);
        }
    }

    [TestCase("A0", 0, 0)]
    [TestCase("  A0   ", 0, 0)]
    [TestCase("B3", 1, 3)]
    [TestCase("c11", 2, 11)]
    [TestCase("d3", 3, 3)]
    public void From__ShouldReturnValueObject__WhenInputIsValid(string input, int rowIndex, int columnIndex)
    {
        var coordinates = Coordinates.From(input);

        using (new AssertionScope())
        {
            coordinates.Should().NotBeNull();
            coordinates.RowIndex.Should().Be(rowIndex);
            coordinates.ColumnIndex.Should().Be(columnIndex);
        }
    }

    [TestCase("xxxx")]
    [TestCase("11")]
    [TestCase("21a")]
    [TestCase("..")]
    [TestCase("__")]
    [TestCase("  ")]
    [TestCase("")]
    [TestCase(null)]
    public void TryFrom__ShouldThrowException__WhenInput_IsInvalid(string? input)
    {
        var act = () => Coordinates.From(input);

        act.Should().ThrowExactly<InvalidCoordinatesException>();
    }
}
