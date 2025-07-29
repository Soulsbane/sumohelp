namespace SumoHelp.Commands;

using System.ComponentModel;
using Spectre.Console.Cli;

public class UpdateCommandSettings : CommandSettings;

[Description("Update the local help term database with the latest terms from the remote source.")]
public class UpdateCommand : Command<UpdateCommandSettings>
{
	public override int Execute(CommandContext context, UpdateCommandSettings settings)
	{
		return 0;
	}
}
