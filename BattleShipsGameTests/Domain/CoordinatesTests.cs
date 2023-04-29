using BattleshipsGame.Domain;


namespace BattleShipsGameTests.Domain;

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

    [Test]
    public void From__ShouldThrowNotImplemnetedException()
    {
        var act = () => Coordinates.From("aa");

        act.Should().ThrowExactly<NotImplementedException>();
    }
}
