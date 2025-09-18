namespace SumoHelp.SumoTerms;

abstract class TermBase
{
	private readonly string _termsFilePath;
	private readonly string _sumoHelpDir;

	protected TermBase(string companyName, string appName, string outputFileName)
	{
		DirectoryInfo userDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
		_sumoHelpDir = Path.Combine(userDir.FullName, companyName, appName);
		_termsFilePath = Path.Combine(_sumoHelpDir, outputFileName);
	}

	public string GetTermsFilePath() => _termsFilePath;
	protected string GetSumoHelpDir() => _sumoHelpDir;
}

class TermBaseImpl(string companyName, string appName, string outputFileName)
	: TermBase(companyName, appName, outputFileName);
