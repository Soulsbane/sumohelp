using System.Diagnostics.CodeAnalysis;
using Spectre.Console;
using Spectre.Console.Cli;

namespace SumoHelp.Commands;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ListCommandSettings : CommandSettings
{
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class ListCommand : Command<ListCommandSettings>
{
	public override int Execute(CommandContext context, ListCommandSettings settings)
	{
		var termLoader = new SumoHelp.SumoTermLoader();
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
