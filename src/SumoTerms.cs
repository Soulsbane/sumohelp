namespace SumoHelp;
using System.Reflection;

using System.Text.Json;

public record SumoTerms(IDictionary<string, string> Terms);

class SumoTermLoader
{
	private readonly SumoTerms? _terms;

	public SumoTermLoader()
	{
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true
		};

		using var stream = Assembly
			.GetExecutingAssembly()
			.GetManifestResourceStream("SumoHelp.data.terms.json")!;

		_terms = JsonSerializer.Deserialize<SumoTerms>(stream, options) ?? throw new InvalidOperationException("Failed to load terms.");
	}

	public string? FindExact(string term)
	{
		if (_terms == null)
		{
			throw new InvalidOperationException("Terms not loaded.");
		}

		return _terms.Terms.TryGetValue(term.ToLower(), out var definition) ? definition : null;
	}

	public Dictionary<string, string> FindAll(string termToFind)
	{
		if (_terms == null)
		{
			throw new InvalidOperationException("Terms not loaded.");
		}

		return _terms.Terms
			.Where(kvp => kvp.Key.StartsWith(termToFind, StringComparison.OrdinalIgnoreCase))
			.ToDictionary();
	}
}
