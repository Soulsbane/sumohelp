namespace SumoHelp.SumoTerms;

using System.Text.RegularExpressions;
using System.Text.Json;

class SumoTermSaver : TermBase
{
	public void Save(Dictionary<string, string> glossary)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(glossary, options);

		if (!Directory.Exists(GetSumoHelpDir()))
		{
			Directory.CreateDirectory(GetSumoHelpDir());
		}

		json = Regex.Unescape(json);
		File.WriteAllText(GetTermsFilePath(), json);
	}
}

