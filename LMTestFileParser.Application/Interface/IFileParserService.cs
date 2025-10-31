using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Application.Interface;

// Check if valid Bank name and is provided
// Check if valid file and is of type csv
// Get the column names of the file uploaded
// Parse the json file with bank names and column names for each bank
public interface IFileParserService
{
    string _message { get; set; }
    bool IsValidFile(string filename);
    bool Uploadile(string filepath);
    List<string> GetHeaderListFromFile(string uploadedFilepath);
    ConfigModel GetOutputHeaderListFromConfig(string bankName);
    List<string> GetMissingHeaders();
    bool ProcessData(string data);
    bool SaveFile();
}
