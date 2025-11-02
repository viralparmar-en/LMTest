using System.Globalization;
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Application.Services;
using LMTestFileParser.Domain.Models;
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
    [InlineData("", "barclays", "|", null)]
    [InlineData("PriceMultiplier", "", "|", null)]
    [InlineData("PriceMultiplier", "barclays", "", null)]
    [InlineData("PriceMultiplier", "InstIdentCode:DE000C4SA5W8|;InstFullName:DAX|;InstClassification:FFICSX|;NotionalCurr:EUR|;PriceMultiplier:25.0|;UnderlInstCode:DE0008469008|;UnderlIndexName:DAX PERFORMANCE-INDEX|;OptionType:OTHR|;StrikePrice:0.0|;OptionExerciseStyle:|;ExpiryDate:2020-09-18|;DeliveryType:PHYS|", "|;", "25.0")]
    public void ProcessComplexColumn_ExtractsDesiredColumnFromTheString_returnsString(string columnNameToExtract, string complexColumnValue, string delimiter, string result)
    {
        var _result = _fileParserService.ProcessComplexColumn(columnNameToExtract, complexColumnValue, delimiter);

        Assert.IsType<CSVFileParserService>(_fileParserService);
        Assert.Equal(result, _result);
    }


}