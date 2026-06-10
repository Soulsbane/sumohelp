namespace SumoHelp.SumoTerms;

using Core;
using System.Text.RegularExpressions;
using System.Text.Json;

class SumoTermSaver
{
	public void Save(Dictionary<string, string> glossary)
	{
		var json = JsonSerializer.Serialize(glossary, Constants.JsonOptions);

		if (!Directory.Exists(UserDataPaths.Instance.GetSumoHelpPath()))
		{
			Directory.CreateDirectory(UserDataPaths.Instance.GetSumoHelpPath());
		}

		json = Regex.Unescape(json);
		File.WriteAllText(UserDataPaths.Instance.GetTermsFilePath(), json);
	}
}
