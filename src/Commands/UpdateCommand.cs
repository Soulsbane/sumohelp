namespace SumoHelp.Commands;

using System.ComponentModel;
using Spectre.Console.Cli;
using SumoTerms;

public class UpdateCommandSettings : CommandSettings;

[Description("Update the local help term database with the latest terms from the remote source.")]
public class UpdateCommand : Command<UpdateCommandSettings>
{
	public override int Execute(CommandContext context, UpdateCommandSettings settings)
	{
		const string url = "https://en.wikipedia.org/wiki/Glossary_of_sumo_terms";
		SumoTermDownloader downloader = new SumoTermDownloader();
		SumoTermSaver termSaver = new SumoTermSaver("Raijinsoft", "sumohelp", "terms.json");
		var glossary = downloader.GetTerms(url);

		termSaver.Save(glossary);

		if (File.Exists(termSaver.TermsFilePath))
		{
			Console.WriteLine("Terms saved to " + termSaver.TermsFilePath);
		}

		return 0;
	}
}
