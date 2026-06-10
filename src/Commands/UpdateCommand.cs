namespace SumoHelp.Commands;

using System.ComponentModel;
using Spectre.Console.Cli;
using SumoTerms;
using Core;

public class UpdateCommandSettings : CommandSettings;

[Description("Update the local help term database with the latest terms from the remote source.")]
public class UpdateCommand : Command<UpdateCommandSettings>
{
	public override int Execute(CommandContext context, UpdateCommandSettings settings)
	{
		SumoTermDownloader downloader = new SumoTermDownloader();
		SumoTermSaver termSaver = new SumoTermSaver();
		var glossary = downloader.GetTerms(Constants.TermsUrl);

		termSaver.Save(glossary);

		if (File.Exists(UserDataPaths.Instance.GetTermsFilePath()))
		{
			Console.WriteLine("Terms saved to " + UserDataPaths.Instance.GetTermsFilePath());
		}

		return 0;
	}
}
