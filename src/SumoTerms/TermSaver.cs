namespace SumoHelp.SumoTerms;

using System.Text.RegularExpressions;
using System.Text.Json;

class SumoTermSaver
{
	private readonly string _termsFilePath;
	private readonly string _sumoHelpDir;

	public SumoTermSaver(string companyName, string appName, string outputFileName)
	{
		DirectoryInfo userDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
		_sumoHelpDir = Path.Combine(userDir.FullName, companyName, appName);
		_termsFilePath = Path.Combine(_sumoHelpDir, outputFileName);
	}

	public string TermsFilePath => _termsFilePath;

	public void Save(Dictionary<string, string> glossary)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(glossary, options);

		if (!Directory.Exists(_sumoHelpDir))
		{
			Directory.CreateDirectory(_sumoHelpDir);
		}

		json = Regex.Unescape(json);
		File.WriteAllText(_termsFilePath, json);
	}
}

