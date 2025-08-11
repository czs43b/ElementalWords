using ElementalWords.Data;

namespace ElementalWords.Services;

public static class ElementWordService
{
    private const int MaxSymbolLength = 3;

    private static readonly Dictionary<string, (string Symbol, string Name)> SymbolsToUppercase =
        ElementDataMappings.Elements.ToDictionary(
            kvp => kvp.Key.ToUpperInvariant(),
            kvp => (kvp.Key, kvp.Value)
        );

    public static IReadOnlyList<IReadOnlyList<string>> ElementalForms(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return [];
        }

        var results = new List<string[]>();
        var element = new List<string>();
        var upperInput = input.ToUpperInvariant();

        FindElements(element, upperInput, results, 0);

        return [.. results];
    }

    private static void FindElements(
        List<string> element,
        string input,
        List<string[]> results,
        int index)
    {
        if (index == input.Length)
        {
            results.Add([.. element]);
            return;
        }

        var maxLength = Math.Min(MaxSymbolLength, input.Length - index);

        for (var length = 1; length <= maxLength; length++)
        {
            var key = new string(input.AsSpan(index, length));

            if (!SymbolsToUppercase.TryGetValue(key, out var primaryKey))
            {
                continue;
            }

            var formattedElement = $"{primaryKey.Name} ({primaryKey.Symbol})";

            element.Add(formattedElement);
            FindElements(element, input, results, index + length);
            element.RemoveAt(element.Count - 1);
        }
    }
}
