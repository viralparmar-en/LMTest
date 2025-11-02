using LMTestFileParser.Application.Interface;
using LMTestFileParser.Infrastructure.Interface;

public class App
{
    private readonly IFileParserService _csvFileParserService;

    private readonly string[] _args;
    public App(IFileParserService csvFileParserService, string[] args)
    {
        _csvFileParserService = csvFileParserService;
        _args = args;
    }

    public void Run()
    {
        if (_args.Length < 2)
            Console.WriteLine("The application requires two parameters, <Bank Name> <Filepath.csv> ");
        else
        {
            string bankName = _args[0];
            string filePath = _args[1];
            if (File.Exists(filePath))
            {
                _csvFileParserService.ProcessFile(bankName, filePath);
                Console.WriteLine(_csvFileParserService._message);
            }
            else
            {
                Console.WriteLine($"File not found at path {filePath}.");
            }
        }

    }
}