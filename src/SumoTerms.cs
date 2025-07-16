namespace SumoHelp;
using System.Reflection;

using System.Text.Json;

class SumoTermLoader
{
	private Dictionary<string, string> _sumoTerms;

	public SumoTermLoader()
	{
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true
		};

		_sumoTerms = new Dictionary<string, string>();

		using var stream = Assembly
			.GetExecutingAssembly()
			.GetManifestResourceStream("SumoHelp.data.terms.json")!;

		_sumoTerms = JsonSerializer.Deserialize<Dictionary<string, string>>(stream, options) ?? throw new InvalidOperationException("Failed to load terms.");
	}

	public string? FindExact(string term)
	{
		return _sumoTerms.TryGetValue(term.ToLower(), out var definition) ? definition : null;
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
