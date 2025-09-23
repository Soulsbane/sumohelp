namespace SumoHelp.SumoTerms;
using System.Reflection;

using System.Text.Json;

class SumoTermLoader : TermBase
{
	private Dictionary<string, string> _sumoTerms;

	public SumoTermLoader()
	{
		_sumoTerms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		if (UserTermFileExists())
		{
			LoadUserTerms();
		}
		else
		{
			LoadEmbeddedTerms();
		}
	}

	private void  LoadEmbeddedTerms()
	{
		using var stream = Assembly
			.GetExecutingAssembly()
			.GetManifestResourceStream("SumoHelp.data.terms.json")!;

		_sumoTerms = JsonSerializer.Deserialize<Dictionary<string, string>>(stream, Constants.JsonOptions) ??
			throw new InvalidOperationException("Failed to load terms.");
	}

	private void LoadUserTerms()
	{
		var userTermsJson = File.ReadAllText(GetTermsFilePath());
		_sumoTerms = JsonSerializer.Deserialize<Dictionary<string, string>>(userTermsJson, Constants.JsonOptions) ??
			throw new InvalidOperationException("Failed to load terms.");
	}

	public string FindExact(string term)
	{
		return _sumoTerms.GetValueOrDefault(term, "");
	}

	public Dictionary<string, string> FindAll(string termToFind)
	{
		return _sumoTerms
			.Where(kvp => kvp.Key.StartsWith(termToFind, StringComparison.OrdinalIgnoreCase))
			.ToDictionary();
	}

	public Dictionary<string, string> GetAll()
	{
		return _sumoTerms;
	}
}

