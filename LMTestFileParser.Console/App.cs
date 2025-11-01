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
        //var configObject = _csvFileParserService.GetOutputHeaderListFromConfig("BarclaysBank");
        //  var service = _csvFileParserService.CopyFile("Bank Of Baroda", "ofile.csv");
        //Console.WriteLine(configObject.SimpleParamConfigs[0].SourceColunn);
        // _csvFileParserService.SaveFile();
        _csvFileParserService.ProcessFile("BarclaysBank", "DataExtractor_Example_Input.csv");
        Console.WriteLine(_csvFileParserService._message);
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
    }
}