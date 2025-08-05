namespace SumoHelp.Core;

using Spectre.Console;

class TableHelpers
{
	public static void OutputTable(Dictionary<string, string> terms)
	{
		var table = new Table();

		table.ShowRowSeparators();
		table.AddColumn("Term");
		table.AddColumn("Description");

		foreach (var term in terms)
		{
			table.AddRow(term.Key, term.Value);
		}

		AnsiConsole.Write(table);
	}
}
