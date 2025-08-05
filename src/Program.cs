
using Spectre.Console;
using Spectre.Console.Cli;
using SumoHelp.Commands;

var app = new CommandApp<GetTermCommand>();

app.Configure(config =>
{
	config.AddCommand<ListCommand>("list");
	config.AddCommand<UpdateCommand>("update");
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

