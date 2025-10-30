using LMTestFileParser.Application.Interface;

namespace LMTestFileParser.Application.Services;

// Check if valid Bank name and is provided
// Check if valid file and is of type csv
// Get the column names of the file uploaded
// Parse the json file with bank names and column names for each bank
public class CSVFileParserService : ICSVFileParserService
{
    public bool IsValidFile(string filename)
    {
        throw new NotImplementedException();
    }
}
