// See https://aka.ms/new-console-template for more information
using System.Text.Json;

var jsonStr = """
              {
              	"Terms": {
              		"Yokozuna": "The highest rank achievable in sumo. There is no set criteria for becoming a yokozuna except one must first be an Ozeki.",
              		"Ozeki": "The rank immediately under Yokozuna. Ozeki must have 33 wins over the three last tournaments competed in. In addition, an Ozeki must have won 10 matches in their last tournament. There are no set rules for becoming an Ozeki. Other things considered are beating a yokozuna and winning a basho. Also any illegal moves or dodging will count against a rikishi."
              	}
              }
              """;

sumohelp.SumoTerms? sumoTerms = JsonSerializer.Deserialize<sumohelp.SumoTerms>(jsonStr);

if (sumoTerms == null)
{
	Console.WriteLine("Failed to deserialize JSON.");
}
else
{
	foreach (var (key, value) in sumoTerms.Terms)
	{
		Console.WriteLine(key + ": " + value);
	}
}

var termLoader = new sumohelp.SumoTermLoader();
termLoader.Load("data/terms.json");
var definition = termLoader.FindExact("yokozuna");

if (definition != null)
{
	Console.WriteLine($"Definition of Yokozuna: {definition}");
}
else
{
	Console.WriteLine("Term not found.");
}
