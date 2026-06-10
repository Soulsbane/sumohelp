using System.Reflection;
using System.Text.Json;
using Lua;
using SumoHelp.Addons;
using SumoHelp.Core;

namespace SumoHelp.SumoTerms;

[LuaObject]
internal partial class SumoTermLoader : ILuaApi
{
	private Dictionary<string, string> _sumoTerms;

	public SumoTermLoader()
	{
		_sumoTerms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		if (UserDataPaths.Instance.UserTermFileExists())
			LoadUserTerms();
		else
			LoadEmbeddedTerms();
	}

	public ILuaApi GetInterface()
	{
		return this;
	}

	public string GetName()
	{
		return "TermLoader";
	}

	private void LoadEmbeddedTerms()
	{
		using var stream = Assembly
			.GetExecutingAssembly()
			.GetManifestResourceStream("SumoHelp.data.terms.json")!;

		_sumoTerms = JsonSerializer.Deserialize<Dictionary<string, string>>(stream, Constants.JsonOptions) ??
		             throw new InvalidOperationException("Failed to load terms.");
	}

	private void LoadUserTerms()
	{
		var userTermsJson = File.ReadAllText(UserDataPaths.Instance.GetTermsFilePath());
		_sumoTerms = JsonSerializer.Deserialize<Dictionary<string, string>>(userTermsJson, Constants.JsonOptions) ??
		             throw new InvalidOperationException("Failed to load terms.");
	}

	[LuaMember("AddTerm")]
	// ReSharper disable once MemberCanBePrivate.Global This is exported to the Lua Addon API so it needs to be public
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

	public void Add(string term, string definition)
	{
		_sumoTerms.Add(term, definition);
	}
}
