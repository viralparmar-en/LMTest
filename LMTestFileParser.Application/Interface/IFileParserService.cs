using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Application.Interface;

// Check if valid Bank name and is provided
// Check if valid file and is of type csv
// Get the column names of the file uploaded
// Parse the json file with bank names and column names for each bank
public interface IFileParserService
{
    string _message { get; set; }
    bool IsValidFileType(string filename);
    bool CopyFile(string bankName, string filepath);
    bool UploadFile(string bankName, string filepath);
    ConfigModel GetHeaderIndexMap(ConfigModel parseConfig, List<CSVRowModel> records);
    ConfigModel GetConfigForABank(string bankName);
    List<string> GetMissingHeaders();
    bool ProcessFile(string bankName, string filepath);
    string? ProcessComplexColumn(string columnName, string complexColumnValue, char delimiter);
    bool SaveFile(ConfigModel configModel, List<CSVRowModel> data);
}
