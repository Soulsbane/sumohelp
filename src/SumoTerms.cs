using System.Text.Json;

namespace sumohelp;

public record SumoTerms(IDictionary<string, string> Terms);

class SumoTermLoader
{
	private SumoTerms? _terms;

	public void Load(string jsonFilePath)
	{
		using var stream = new FileStream(jsonFilePath, FileMode.Open, FileAccess.Read);
		var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			ReadCommentHandling = JsonCommentHandling.Skip,
			AllowTrailingCommas = true
		};

		_terms = JsonSerializer.Deserialize<SumoTerms>(stream, options) ?? throw new InvalidOperationException("Failed to load terms.");
	}

	public string? FindExact(string term)
	{
		if (_terms == null)
		{
			throw new InvalidOperationException("Terms not loaded.");
		}

		return _terms.Terms.TryGetValue(term, out var definition) ? definition : null;
	}
}
