using System.Reflection;
if (args.Length > 0)
{
	Console.WriteLine("Usage: SumoHelp");
	Console.WriteLine($"{args[0]}");
}
else
{
	Console.WriteLine("No arguments provided.");
}

var termLoader = new SumoHelp.SumoTermLoader();

termLoader.Load();
string? definition = termLoader.FindExact("yokozuna");

if (definition != null)
{
	Console.WriteLine($"Definition of Yokozuna: {definition}");
}
else
{
	Console.WriteLine("Term not found.");
}
