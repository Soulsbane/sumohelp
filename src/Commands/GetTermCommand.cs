namespace SumoHelp.Commands;

using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

public class GetTermCommandSettings : CommandSettings
{
	[Description("Term to search for in the terms database")]
	[CommandArgument(0, "[term]")]
	public string? Term { get; init; } = string.Empty;
}

public class GetTermCommand : Command<GetTermCommandSettings>
{
	void GetTerm(string termToFind)
	{
		var termLoader = new SumoTermLoader();
		string definition = termLoader.FindExact(termToFind);

		if (definition != string.Empty)
		{
			Console.WriteLine($"{termToFind} - {definition}");
		}
		else
		{
			var searchResults = termLoader.FindAll(termToFind);

			if (searchResults.Count > 0)
			{
				if (searchResults.Count == 1)
				{
					Console.WriteLine($"{searchResults.First().Key} - {searchResults.First().Value}");
				}
				else
				{
					var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
						.Title("No exact match found. Do you want to search for similar terms?")
						.AddChoices(searchResults.Keys));

					Console.WriteLine($"{choice} - {searchResults[choice]}");
				}
			}
			else
			{
				Console.WriteLine("No similar terms found.");
			}
		}
	}

	public override int Execute(CommandContext context, GetTermCommandSettings settings)
	{
		if (string.IsNullOrWhiteSpace(settings.Term))
		{
			Console.WriteLine("Please provide a term to search for.");
			return -1;
		}

		GetTerm(settings.Term);
		return 0;
	}
}
