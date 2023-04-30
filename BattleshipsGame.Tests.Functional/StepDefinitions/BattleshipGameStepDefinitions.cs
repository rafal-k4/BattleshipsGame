using BattleshipsGame.Domain;
using BattleshipsGame.Tests.Functional.StepDefinitions.Models;

namespace BattleshipsGame.Tests.Functional.StepDefinitions;

[Binding]
public class BattleshipGameStepDefinitions
{
    private readonly ScenarioContext _scenarioContext;

    public BattleshipGameStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"game is created with given seed (\d*)")]
    public void GivenGameIsCreatedWithGivenSeed(int seed)
    {
        var gameSettings = new GameSettings
        {
            RandomSeed = seed,
            DisplayShipPositions = false
        };
        var game = new Game(gameSettings);

        _scenarioContext.Set(game);
    }

    [Given(@"coordinates are '(.*)'")]
    public void GivenCoordinatesAre(string coordinates)
    {
        _scenarioContext.Set(new Coordinates { Values = coordinates.Split(" ").ToList() });
    }

    [When(@"game board is returned")]
    public void WhenGameBoardIsReturned()
    {
        var game = _scenarioContext.Get<Game>();
        _scenarioContext.Set(new GameBoard { Value = game.GetBoard() });
    }

    [When(@"rockets shots are fired")]
    public void WhenRocketsShotsAreFired()
    {
        var game = _scenarioContext.Get<Game>();
        var coordinates = _scenarioContext.Get<Coordinates>();

        var hitResults = new HitResults();

        foreach(var coordinate in coordinates.Values)
        {
            var hitResult = game.HitTarget(coordinate);
            hitResults.Values.Add(hitResult.Value.GetType().Name.ToString().ToLower());
        }

        _scenarioContext.Set(hitResults);
    }

    [Then(@"board should looks like following")]
    public void ThenBoardShouldLooksLikeFollowing(Table table)
    {
        var expectedBoardWithoutWhiteSpaces = string.Join(Environment.NewLine, 
            table.Rows
            .SelectMany(x => x.Values)
            .Select(x => x.Replace(" ", string.Empty)));

        var resultBoardWithoutWhiteSpaces = _scenarioContext.Get<GameBoard>()
            .Value
            .Trim()
            .Replace(" ", string.Empty);

        resultBoardWithoutWhiteSpaces.Should().Be(expectedBoardWithoutWhiteSpaces);
    }

    [Then(@"result should be '(.*)'")]
    public void ThenResultShouldBe(string result)
    {
        var expectedHitResults = result.Split(" ").Select(x => x.ToLower());
        var hitResults = _scenarioContext.Get<HitResults>();

        hitResults.Values.Should().Contain(expectedHitResults);
    }

    [Then(@"game status should be as finished")]
    public void ThenGameStatusShouldBeAsFinished()
    {
        var game = _scenarioContext.Get<Game>();

        game.IsGameFinished().Should().BeTrue();
    }

}
