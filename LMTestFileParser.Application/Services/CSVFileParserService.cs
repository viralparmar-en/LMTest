using LMTestFileParser.Application.Interface;
using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Application.Services;

// Check if valid Bank name and is provided
// Check if valid file and is of type csv
// Get the column names of the file uploaded
// Parse the json file with bank names and column names for each bank
public class CSVFileParserService : IFileParserService
{
    private readonly IConfigReader _configReader;
    private readonly IFileProcessor _fileprocessor;
    public CSVFileParserService(IConfigReader configReader, IFileProcessor fileprocessor)
    {
        _configReader = configReader;
        _fileprocessor = fileprocessor;
    }
    public CSVFileParserService()
    {
        _configReader = new JsonConfigReader();
        _fileprocessor = new CSVFileProcessor();
    }

    public string _message { get; set; } = "";

    public List<string> GetHeaderListFromFile(string uploadedFilepath)
    {
        throw new NotImplementedException();
    }

    public List<string> GetMissingHeaders()
    {
        throw new NotImplementedException();
    }

    public ConfigModel GetOutputHeaderListFromConfig(string bankName)
    {
        try
        {
            return _configReader.GetConfigByBankName(bankName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new();
        }
    }

    public bool IsValidFile(string filename)
    {
        try
        {
            var extension = Path.GetExtension(filename);
            return true;
        }
        catch
        {
            return false;
        }

    }

    public bool ProcessData(string data)
    {
        _fileprocessor.ReadFromFile();
        return false;
    }

    public bool SaveFile()
    {
        throw new NotImplementedException();
    }

    public bool Uploadile(string filepath)
    {
        throw new NotImplementedException();
    }
}
