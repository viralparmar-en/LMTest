using LMTestFileParser.Application.Interface;

public class App
{
    private readonly ICSVFileParserService _csvFileParserService;
    private readonly string[] _args;

    public App(ICSVFileParserService csvFileParserService, string[] args)
    {
        _csvFileParserService = csvFileParserService;
        _args = args;
    }

    public void Run()
    {
        if (_args.Length > 0)
        {
            string filePath = _args[0];
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine($"{_args[0]}");
                Console.WriteLine("File not found.");
            }
        }
        //_csvFileParserService.IsValidFile("");
    }
}