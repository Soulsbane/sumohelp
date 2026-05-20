namespace SumoHelp.Addons;

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using SumoTerms;

class LuaAddonManager
{
	private readonly string _addonsDir;
	 List<LuaAddon>_addons = new List<LuaAddon>();

	private readonly Dictionary<string, ILuaApi> _apis;

	public LuaAddonManager()
	{
		DirectoryInfo userDir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
		_addonsDir = Path.Combine(userDir.FullName, Constants.CompanyName, Constants.AppName, "addons");
		_apis = new Dictionary<string, ILuaApi>(StringComparer.OrdinalIgnoreCase);
	}

	private void AddApi(string name, ILuaApi api)
	{
		_apis.Add(name, api);
	}

	public void LoadAddons()
	{
		LoadUserDirAddons();
		LoadZippedAddons();
	}

	private void LoadUserDirAddons()
	{
		if (!Directory.Exists(_addonsDir))
		{
			Console.WriteLine($"Addons directory not found at {_addonsDir}. No addons loaded.");
			Directory.CreateDirectory(_addonsDir);

			return;
		}

		List<string> dirs = new List<string>(Directory.EnumerateDirectories(_addonsDir));
		Console.WriteLine($"Found {dirs.Count} addon directories.");
		AddApi("TermLoader", new SumoTermLoader());

		foreach(string dir in dirs)
		{
			string[] luaFiles = Directory.GetFiles(dir, "*.lua");

			foreach (string luaFile in luaFiles)
			{
				var termLoader = new SumoTermLoader();
				LuaAddon addon = new LuaAddon();

				addon.AddApi("TermLoader", new SumoTermLoader());
				_addons.Add(addon);
				addon.DoFile(luaFile);
				addon.CallFunc("OnInitialize");

			}
		}
	}

	private  void LoadZippedAddons()
	{
		if (Directory.Exists(_addonsDir))
		{
			List<string> addonFiles= new List<string>(Directory.EnumerateFiles(_addonsDir));
			Console.WriteLine($"Found {addonFiles.Count} addon archives in {_addonsDir}.");

			foreach (string addonFile in addonFiles)
			{
				using var file = File.OpenRead(addonFile);
				using var zip = new ZipArchive(file, ZipArchiveMode.Read);

				foreach (var entry in zip.Entries)
				{
					using StreamReader stream = new StreamReader(entry.Open());
					Console.WriteLine(stream.ReadToEnd());
				}
			}
		}
	}


}
