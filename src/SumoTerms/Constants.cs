namespace SumoHelp.SumoTerms;
using System.Text.Json;

static class Constants
{
	public const string CompanyName = "Raijinsoft";
	public const string AppName = "sumohelp";
	public const string OutputFileName = "terms.json";
	public const string TermsUrl = "https://en.wikipedia.org/wiki/Glossary_of_sumo_terms";
	public static readonly JsonSerializerOptions JsonOptions;

	static Constants()
	{
		JsonOptions = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true,
			WriteIndented = true
		};
	}
}
