using System.Dynamic;
using System.Globalization;
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Application.Services;
using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure.Interface;
using LMTestFileParser.Tests.CustomComparers;
using LMTestFileParser.Tests.Mocks;

namespace LMTestFileParser.Tests;

public class CSVFileParserServiceTest
{
    private readonly IFileParserService _fileParserService;
    private readonly IFileProcessor _csvfileProcessor;
    private readonly IConfigReader _csvConfigReadaer;
    private readonly ConfigModel _configModel;
    private readonly List<CSVRowModel> _records;
    public CSVFileParserServiceTest()
    {
        _csvfileProcessor = new FakeCSVFileProcessor();
        _csvConfigReadaer = new FakeJsonConfigReader();
        _fileParserService = new CSVFileParserService(_csvConfigReadaer, _csvfileProcessor);

        _configModel = new ConfigModel();
        _records =
        [
            new()
            {
                Row = ["ISIN","CFICode","Venue","AlgoParams","ContractSize"]
            },
            new()
            {
                Row = ["DE000C4SA5W8","FFICSX","XEUR","20001145","DAX"]
            },
            new()
            {
                 Row = ["DE000C4SA5W8","FFICSX","XEUR","20001145","DAX"]
            },
            new()
            {
                 Row = ["DE000C4SA5W8","FFICSX","XEUR","20001145","DAX"]
            },
            new()
            {
                 Row = ["DE000C4SA5W8","FFICSX","XEUR","20001145","DAX"]
            },
        ];
        var simpleParamConfigList = new List<SimpleParamConfigModel>
        {
            new()
            {
                SourceIndex = 0,
                SourceColunn = "ISIN",
                DestinationColumnName = "ISIN"
            },
            new()
            {
                SourceIndex = 0,
                SourceColunn = "CFICode",
                DestinationColumnName = "CFICode"
            }
        };
        var complexParamConfigList = new List<ComplexParamConfigModel>()
         {
            new()
            {
                SourceIndex = 0,
                SourceColunn = "AlgoParams",
                DestinationColumnName = "InstFullName",
                ColumnToExtract ="InstFullName",
                Delimiter = "|;"
            },
            new()
            {
                SourceIndex = 0,
                SourceColunn = "AlgoParams",
                DestinationColumnName = "ContractSize",
                ColumnToExtract ="ContractSize",
                Delimiter = "|;"
            }
        };


        _configModel.BankName = "BarclaysBank";
        _configModel.HeaderRowAt = 2;
        _configModel.SimpleParamConfigs = simpleParamConfigList;
        _configModel.ComplexParamConfigs = complexParamConfigList;

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

    [Fact]
    public void GetHeaderIndexMap_MapColumnIndexToExtractedConfig_returnsConfigWithMappedIndexes()
    {
        var result = new ConfigModel();
        var simpleParamConfigList = new List<SimpleParamConfigModel>
        {
            new()
            {
                SourceIndex = 0,
                SourceColunn = "ISIN",
                DestinationColumnName = "ISIN"
            },
            new()
            {
                SourceIndex = 1,
                SourceColunn = "CFICode",
                DestinationColumnName = "CFICode"
            }
        };
        var complexParamConfigList = new List<ComplexParamConfigModel>()
         {
            new()
            {
                SourceIndex = 3,
                SourceColunn = "AlgoParams",
                DestinationColumnName = "InstFullName",
                ColumnToExtract ="InstFullName",
                Delimiter = "|;"
            },
            new()
            {
                SourceIndex = 3,
                SourceColunn = "AlgoParams",
                DestinationColumnName = "ContractSize",
                ColumnToExtract ="ContractSize",
                Delimiter = "|;"
            }
        };


        result.BankName = "BarclaysBank";
        result.HeaderRowAt = 2;
        result.SimpleParamConfigs = simpleParamConfigList;
        result.ComplexParamConfigs = complexParamConfigList;

        var _result = _fileParserService.GetHeaderIndexMap(_configModel, _records);

        // Assert
        Assert.True(AreEqual(result, _result));

        static bool AreEqual(ConfigModel a, ConfigModel b)
        {
            return a.BankName == b.BankName &&
                   a.HeaderRowAt == b.HeaderRowAt &&
                   a.SimpleParamConfigs!.SequenceEqual(b.SimpleParamConfigs!, new SimpleParamConfigComparer()) &&
                   a.ComplexParamConfigs!.SequenceEqual(b.ComplexParamConfigs!, new ComplexParamConfigComparer());
        }

    }
    [Fact]
    public void CreateClassObjectForCSVSchema_GenerateOutputObjectClassUsingConfigPropertyNames_returnsBool()
    {
        // Arrange
        var expectedPropertyNames = new List<string> { "ISIN", "CFICode", "InstFullName", "ContractSize" };

        // Act
        var actualPropertyNames = _fileParserService.CreateClassObjectForCSVSchema(_configModel, _records);

        var propertyNames = GetPropertyNames(actualPropertyNames[0]);

        static List<string> GetPropertyNames(dynamic obj)
        {
            var dict = obj as IDictionary<string, object>;
            return [.. dict!.Keys];
        }

        // Assert
        Assert.Equal(expectedPropertyNames.OrderBy(n => n), propertyNames.OrderBy(n => n));
    }

}
