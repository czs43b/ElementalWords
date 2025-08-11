using ElementalWords.Services;

namespace ElementalWords.ConsoleApp;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("*** Welcome to the Elemental Forms finder. ***");
        Console.WriteLine("*** Enter a word and I'll try to express it using chemical element symbols. ***");
        Console.WriteLine("*** Example: 'Yes' => Yttrium (Y) + Einsteinium (Es) ***");
        Console.WriteLine();
        Console.WriteLine("Type in a word to see its elemental forms, or type 'exit' to quit the app.");
        Console.WriteLine();

        while (true)
        {
            Console.Write("Please enter a word: ");

            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Please enter a valid word (without any spaces).");
                continue;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            var results = ElementWordService.ElementalForms(input);

            if (results.Count is 0)
            {
                Console.WriteLine($"No elemental forms found for '{input}'.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"The search performed found {results.Count} elemental form(s) for the word '{input}':");

                var counter = 1;

                foreach (var result in results)
                {
                    Console.WriteLine();
                    Console.WriteLine($"  {counter++}. {string.Join(" + ", result)}");
                }
            }

            Console.WriteLine();
        }
    }
}
