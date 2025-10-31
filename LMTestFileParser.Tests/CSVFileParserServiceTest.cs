using System.Globalization;
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Application.Services;

namespace LMTestFileParser.Tests;

public class CSVFileParserServiceTest
{
    private readonly IFileParserService _fileParserService;
    public CSVFileParserServiceTest()
    {
        _fileParserService = new CSVFileParserService();
    }

    [Theory]
    [InlineData("", false)]
    public void IsValidFile_ChecksIfItsValidFile_ReturnsBool(string filename, bool result)
    {
        var _result = _fileParserService.IsValidFile(filename);
        Assert.Equal(result, _result);
    }
}