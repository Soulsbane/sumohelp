using SumoHelp.SumoTerms;

namespace SumoHelp.Core;

public sealed class UserDataPaths
{
	private readonly string _termsFilePath;
	private readonly string _sumoHelpDir;
	private static readonly Lazy<UserDataPaths> _instance = new Lazy<UserDataPaths>(() => new UserDataPaths());
	public static UserDataPaths Instance => _instance.Value;

	private UserDataPaths()
	{
		DirectoryInfo userDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

		_sumoHelpDir = Path.Combine(userDir.FullName, Constants.CompanyName, Constants.AppName);
		_termsFilePath = Path.Combine(_sumoHelpDir, Constants.OutputFileName);
	}

	public string GetTermsFileDir() => _termsFilePath;
	public string GetSumoHelpDir() => _sumoHelpDir;
	public bool UserTermFileExists() => File.Exists(_termsFilePath);
}
