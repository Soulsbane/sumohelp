using Lua;
using SumoHelp.Addons;

namespace SumoHelp.SumoTerms;
using System.Reflection;

using System.Text.Json;

[LuaObject]
partial class SumoTermLoader : TermBase, ILuaApi
{
	private Dictionary<string, string> _sumoTerms;

	public ILuaApi GetInterface() => this;
	public string GetName() => "TermAPI";

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

	[LuaMember("AddTerm")]
	public void AddTerm(string term, string definition)
	{
		_sumoTerms.Add(term, definition);
	}

	[LuaMember("FindExact")]
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

