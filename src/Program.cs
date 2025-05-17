
if (args.Length > 0)
{
	GetTerm(args[0]);
}
else
{
	Console.WriteLine("No arguments provided.");
}

void GetTerm(string termToFind)
{
	var termLoader = new SumoHelp.SumoTermLoader();
	string? definition = termLoader.FindExact(termToFind);

	if (definition != null)
	{
		Console.WriteLine($"Definition for {termToFind} is: {definition}");
	}
	else
	{
		Console.WriteLine("Term not found.");
	}
}
