using BattleshipsGame.Domain;
using BattleshipsGame.Domain.ValueObjects;

namespace BattleShipsGame.Domain.Tests;

public class GameTests
{
    private Game _sut;
    private GameSettings _settings;

    private string[] BattleshipCoordinates = new[] { "A2", "B2", "C2", "D2", "E2" };
    private string[] Destroyer1Coordinates = new[] { "B3", "C3", "D3", "E3" };
    private string[] Destroyer2Coordinates = new[] { "H1", "H2", "H3", "H4" };

    [SetUp]
    public void Initialize()
    {
        _settings = new GameSettings
        {
            RandomSeed = 420,
            DisplayShipPositions = false
        };
        _sut = new Game(_settings);
    }

    [Test]
    public void Constructor__ShouldCreate_OneBattleship_And_TwoDestroyers()
    { 
        // Arrange
        var battleshipLength = 5;
        var destroyerLength = 4;
        var expectedNumberOfShips = 3;

        var battleships = _sut.Ships.Where(x => x.Length == battleshipLength);
        var destroyers = _sut.Ships.Where(x => x.Length == destroyerLength);

        // Assert
        using (new AssertionScope())
        {
            _sut.Ships.Should().HaveCount(expectedNumberOfShips);
            battleships.Should().ContainSingle();
            destroyers.Should().HaveCount(2);
        }
    }

    [Test]
    public void Constructor__ShouldPlace_Ships_OnExpectedSquares__When_RandomSeedProvided()
    {
        // Arrange
        var battleshipLength = 5;
        var destroyerLength = 4;

        var battleship = _sut.Ships.Single(x => x.Length == battleshipLength);
        var destroyers = _sut.Ships.Where(x => x.Length == destroyerLength).ToList();

        // Assert
        using (new AssertionScope())
        {
            var expectedBattleshipsCoordinates = BattleshipCoordinates.Select(Coordinates.From);
            expectedBattleshipsCoordinates.Select(x => new { Row = x.RowIndex, Column = x.ColumnIndex })
                .Should()
                .BeEquivalentTo(battleship.Squares.Select(x => new { x.Row, x.Column } ));

            var expectedFirstDestroyerCoordinates = Destroyer1Coordinates
                .Select(Coordinates.From)
                .Select(x => new { Row = x.RowIndex, Column = x.ColumnIndex });
            var resultFirstDestroyerCoordinates = destroyers[0].Squares.Select(x => new { x.Row, x.Column });
            expectedFirstDestroyerCoordinates.Should().BeEquivalentTo(resultFirstDestroyerCoordinates);

            var expectedSecondDestroyerCoordinates = Destroyer2Coordinates
                .Select(Coordinates.From)
                .Select(x => new { Row = x.RowIndex, Column = x.ColumnIndex });
            var resultSecondDestroyerCoordinates = destroyers[1].Squares.Select(x => new { x.Row, x.Column });
            expectedSecondDestroyerCoordinates.Should().BeEquivalentTo(resultSecondDestroyerCoordinates);
        }
    }
}
