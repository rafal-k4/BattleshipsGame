namespace BattleshipsGame.Domain;
public class GameSettings
{
    public int? RandomSeed { get; init; }

#if DEBUG
    public bool DisplayShipPositions { get; init; } = false;
#endif
}
