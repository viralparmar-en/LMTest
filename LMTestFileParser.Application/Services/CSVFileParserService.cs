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
    private string _fileToProcess { get; set; } = "";
    private static readonly string basePath = AppContext.BaseDirectory;
    public string _message { get; set; } = "";
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


    public List<string> GetHeaderListFromFile(string uploadedFilepath)
    {
        throw new NotImplementedException();
    }

    public List<string> GetMissingHeaders()
    {
        throw new NotImplementedException();
    }

    public ConfigModel GetConfigForABank(string bankName)
    {
        try
        {
            if (!string.IsNullOrEmpty(bankName))
            {
                return _configReader.GetConfigByBankName(bankName);
            }
            else
            {
                _message = "Bank name not provided for retreiving config details.";
                return new();
            }
        }
        catch (Exception ex)
        {
            _message = "Something went wrong while fetching config data for bank. Please check error.txt for more details";
            Console.WriteLine(ex.ToString());
            return new();
        }
    }

    public bool IsValidFileType(string filename)
    {
        try
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var extension = Path.GetExtension(filename);
                if (extension.Equals(".csv"))
                {
                    return true;
                }
                else
                {
                    _message = "Invalid file type, the file should be of type .csv";
                    return false;
                }
            }
            return false;
        }
        catch (Exception ex)
        {
            _message = $"Something went wrong, check errlog.txt file for further details.";
            return false;
        }

    }

    public bool ProcessFile(string filepath)
    {
        _fileprocessor.ReadFromFile("");
        return false;
    }

    public bool SaveFile()
    {
        throw new NotImplementedException();
    }

    public bool CopyFile(string bankName, string filepath)
    {

        try
        {
            if (!string.IsNullOrEmpty(bankName) && IsValidFileType(filepath))
            {
                bankName = bankName.ToLower().Replace(" ", "");
                string bankFolder = Path.Combine(basePath, $"RawBankFiles\\{bankName}"); // Change this to your desired folder
                string newfileName = $"{bankName}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";
                _fileToProcess = newfileName;
                string desitnationfullPath = Path.Combine(bankFolder, _fileToProcess);
                if (_fileprocessor.SaveUploadedFile(bankFolder, filepath, desitnationfullPath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                _message = $"Bank name and file path are required. {_message}";
                return false;
            }
        }
        catch (Exception)
        {
            _message = "There is some error processing the file, see the errorlog.txt for further details";
            return false;
        }
    }

    public bool UploadFile(string bankName, string filepath)
    {
        throw new NotImplementedException();
    }
}
