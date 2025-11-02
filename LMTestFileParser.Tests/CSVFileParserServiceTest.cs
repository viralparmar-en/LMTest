using System.Globalization;
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Application.Services;
using LMTestFileParser.Infrastructure.Interface;
using LMTestFileParser.Tests.Mocks;

namespace LMTestFileParser.Tests;

public class CSVFileParserServiceTest
{
    private readonly IFileParserService _fileParserService;
    private readonly IFileProcessor _csvfileProcessor;
    private readonly IConfigReader _csvConfigReadaer;
    public CSVFileParserServiceTest()
    {
        _csvfileProcessor = new FakeCSVFileProcessor();
        _csvConfigReadaer = new FakeJsonConfigReader();
        _fileParserService = new CSVFileParserService(_csvConfigReadaer, _csvfileProcessor);
    }

    [Theory]
    [InlineData("", false)]
    [InlineData("file.xls", false)]
    [InlineData("file.csv", true)]
    public void IsValidFile_ChecksIfItsValidFile_ReturnsBool(string filename, bool result)
    {
        var _result = _fileParserService.IsValidFileType(filename);
        Assert.IsType<CSVFileParserService>(_fileParserService);
        Assert.Equal(result, _result);
    }
    [Theory]
    [InlineData("", "", false)]
    [InlineData("", "file.xls", false)]
    [InlineData("barclays", "file.xls", false)]
    [InlineData("", "file.csv", false)]
    [InlineData("barclays", "file.csv", true)]
    public void CopyFile_CopiesFileIfValid_returnsbool(string bankName, string filename, bool result)
    {
        var _result = _fileParserService.CopyFile(bankName, filename);

        Assert.IsType<CSVFileParserService>(_fileParserService);
        Assert.Equal(result, _result);
    }

    [Theory]
    [InlineData("", "", '|', "")]
    [InlineData("", "", '|', "")]
    [InlineData("", "barclays", '|', "")]
    [InlineData("", "", '|', "")]
    [InlineData("", "barclays", '|', "")]
    public void ProcessComplexColumn_ExtractsDesiredColumnFromTheString_returnsString(string columnNameToExtract, string complexColumnValue, char delimiter, string result)
    {
        var _result = _fileParserService.ProcessComplexColumn(columnNameToExtract, complexColumnValue, delimiter);

        Assert.IsType<CSVFileParserService>(_fileParserService);
        Assert.Equal(result, _result);
    }

}