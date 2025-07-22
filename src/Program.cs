
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;
using SumoHelp.Commands;


var app = new CommandApp<GetTermCommand>();

app.Configure(config =>
{
	config.AddCommand<ListCommand>("list")
		.WithDescription("List all the terms from the local help term database")
		.WithExample("list", "term");
});

try
{
	return app.Run(args);
}
catch (Exception ex)
{
	AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
	return -1;
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class GetTermCommandSettings : CommandSettings
{
	[Description("Term to search for in the terms database")]
	[CommandArgument(0, "[term]")]
	public string? Term { get; init; } = string.Empty;
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class GetTermCommand : Command<GetTermCommandSettings>
{
	void GetTerm(string termToFind)
	{
		var termLoader = new SumoHelp.SumoTermLoader();
		string definition = termLoader.FindExact(termToFind);

		if (definition != string.Empty)
		{
			Console.WriteLine($"Definition for {termToFind} is: {definition}");
		}
		else
		{
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

