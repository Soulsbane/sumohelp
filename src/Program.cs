
using Spectre.Console;

if (args.Length > 0)
{
	GetTerm(args[0]);
}
else
{
	Console.WriteLine("No arguments provided.");
}

void GetTerm(string termToFind)
{
	var termLoader = new SumoHelp.SumoTermLoader();
	string? definition = termLoader.FindExact(termToFind);

	if (definition != null)
	{
		Console.WriteLine($"Definition for {termToFind} is: {definition}");
	}
	else
	{
		Console.WriteLine("Term not found.");
		var searchResults = termLoader.FindAll(termToFind);

		if (searchResults.Count > 0)
		{
			var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
				.Title("No exact match found. Do you want to search for similar terms?")
				.AddChoices(searchResults.Keys));

			 	Console.WriteLine($"{searchResults[choice]}");
		}
		else
		{
			Console.WriteLine("No similar terms found.");
		}
	}
}
