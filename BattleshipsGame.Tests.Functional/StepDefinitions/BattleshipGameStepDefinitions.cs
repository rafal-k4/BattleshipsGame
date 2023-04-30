using BattleshipsGame.Domain;

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

    [When(@"game board is returned")]
    public void WhenGameBoardIsReturned()
    {
        var game = _scenarioContext.Get<Game>();
        _scenarioContext.Set(game.GetBoard());
    }

    [Then(@"board should looks like following")]
    public void ThenBoardShouldLooksLikeFollowing(Table table)
    {
        var expectedBoardWithoutWhiteSpaces = string.Join(Environment.NewLine, 
            table.Rows
            .SelectMany(x => x.Values)
            .Select(x => x.Replace(" ", string.Empty)));

        var resultBoardWithoutWhiteSpaces = _scenarioContext.Get<string>()
            .Trim()
            .Replace(" ", string.Empty);

        resultBoardWithoutWhiteSpaces.Should().Be(expectedBoardWithoutWhiteSpaces);
    }
}
