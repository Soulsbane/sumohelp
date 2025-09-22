namespace SumoHelp.SumoTerms;

abstract class TermBase
{
	private readonly string _termsFilePath;
	private readonly string _sumoHelpDir;

	protected TermBase()
	{
		DirectoryInfo userDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
		_sumoHelpDir = Path.Combine(userDir.FullName, Constants.CompanyName, Constants.AppName);
		_termsFilePath = Path.Combine(_sumoHelpDir, Constants.OutputFileName);
	}

	public string GetTermsFilePath() => _termsFilePath;
	protected string GetSumoHelpDir() => _sumoHelpDir;
}
