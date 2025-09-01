namespace SumoHelp.SumoTerms;

using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Text.Json;

class SumoTermDownloader
{
	private static readonly Regex BracketsRegex = new Regex(@"\[.*\]");

	public Dictionary<string, string> GetTerms(string url)
	{
		Dictionary<string, string> glossary = new Dictionary<string, string>();
		HtmlWeb web = new HtmlWeb();
		var doc = web.Load(url);

		var dlNodes = doc.DocumentNode.SelectNodes("//dl");

		foreach (var dl in dlNodes)
		{
			var dts = dl.SelectNodes(".//dt");
			var dds = dl.SelectNodes(".//dd");

			if (dts.Count == dds.Count)
			{
				for (int i = 0; i < dts.Count; i++)
				{
					string term = dts[i].InnerText.Trim().Split(' ')[0];
					string definition = RemoveSpecialChars(RemoveBrackets(dds[i].InnerText.Trim()));
					glossary[term] = definition;
				}
			}
		}

		return glossary;
	}

	private string RemoveBrackets(string text)
	{
		return BracketsRegex.Replace(text, string.Empty);
	}

	private string RemoveSpecialChars(string text)
	{
		// Remove thin spaces
		text = text.Replace("\u2009", string.Empty);
		text = text.Replace("\"", string.Empty);
		text = text.Replace("&nbsp;", string.Empty);

		return text;
	}

	public void SaveToJson(Dictionary<string, string> glossary, string filePath)
	{
		var options = new JsonSerializerOptions
		{
			WriteIndented = true
		};

		var json = JsonSerializer.Serialize(glossary, options);

		json = Regex.Unescape(json);
		File.WriteAllText(filePath, json);
	}
}
