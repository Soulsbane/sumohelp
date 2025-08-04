namespace SumoHelp.Commands;

using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

public class ListCommandSettings : CommandSettings
{
	[Description("List of terms to display from the local help term database. If not specified, all terms will be listed.")]
	[CommandArgument(0, "[listType]")]
	public string ListType { get; init; } = string.Empty;
}

public class ListCommand : Command<ListCommandSettings>
{
	public override int Execute(CommandContext context, ListCommandSettings settings)
	{
		if (settings.ListType == "all" || string.IsNullOrWhiteSpace(settings.ListType))
		{
			return ListAllTerms();
		}

		return 0;
	}

	private int ListAllTerms()
	{
		var termLoader = new SumoTermLoader();
		var allTerms = termLoader.GetAll();

		if (allTerms.Count == 0)
		{
			Console.WriteLine("No terms found in the database.");
			return 0;
		}

		var table = new Table();

		table.ShowRowSeparators();
		table.AddColumn("Term");
		table.AddColumn("Description");

		foreach (var term in allTerms)
		{
			table.AddRow(term.Key, term.Value);
		}

		AnsiConsole.Write(table);

		return 0;
	}
}
