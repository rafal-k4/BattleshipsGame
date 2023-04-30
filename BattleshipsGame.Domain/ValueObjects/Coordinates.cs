using System.Text.RegularExpressions;
using ValueOf;

namespace BattleshipsGame.Domain.ValueObjects;

public class Coordinates : ValueOf<string?, Coordinates>
{
    public int RowIndex { get; private set; }
    public int ColumnIndex { get; private set; }

    private static readonly Regex CoordinatesRegex = new(@"^([a-zA-Z]{1})(\d+)$", RegexOptions.Compiled);

    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new InvalidCoordinatesException();

        Value = Value?.Trim();

        var regexMatch = CoordinatesRegex.Match(Value!);

        if (!regexMatch.Success)
            throw new InvalidCoordinatesException();

        const int letterCapturedGroupIndex = 1;
        const int numberCapturedGroupIndex = 2;
        var rowLetter = regexMatch.Groups[letterCapturedGroupIndex].Value.ToUpper();

        RowIndex = char.Parse(rowLetter) - 'A';
        ColumnIndex = int.Parse(regexMatch.Groups[numberCapturedGroupIndex].Value);
    }

    protected override bool TryValidate()
    {
        try
        {
            Validate();
            return true;
        }
        catch (InvalidCoordinatesException)
        {
            return false;
        }
    }
}

public class InvalidCoordinatesException : ArgumentException
{ 
}
