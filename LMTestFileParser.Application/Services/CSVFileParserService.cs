using System.Dynamic;
using LMTestFileParser.Application.Interface;
using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Application.Services;

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
    public ConfigModel GetHeaderIndexMap(ConfigModel parseConfig, List<CSVRowModel> records)
    {
        if (records == null || records.Count == 0 || records[0].Row == null)
            return parseConfig;

        var headerRow = records[0].Row;

        if (headerRow != null)
        {
            // Process SimpleParamConfigs
            if (parseConfig.SimpleParamConfigs != null)
            {
                foreach (var item in parseConfig.SimpleParamConfigs)
                {
                    if (!string.IsNullOrEmpty(item.SourceColunn))
                    {
                        item.SourceIndex = headerRow.IndexOf(item.SourceColunn);
                    }
                }
            }

            // Process ComplexParamConfigs
            if (parseConfig.ComplexParamConfigs != null)
            {
                foreach (var item in parseConfig.ComplexParamConfigs)
                {
                    if (!string.IsNullOrEmpty(item.SourceColunn))
                    {
                        item.SourceIndex = headerRow.IndexOf(item.SourceColunn);
                    }
                }
            }
        }
        return parseConfig;
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
            _message = $"Something went wrong while fetching config data for bank. {ex}";
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
            _message = $"Something went wrong while validating file type, {ex}";
            return false;
        }

    }
    public bool ProcessFile(string bankName, string filepath)
    {
        try
        {
            if (string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(filepath) || !IsValidFileType(filepath))
                return false;

            var parseConfig = GetConfigForABank(bankName);
            if (parseConfig == null || string.IsNullOrEmpty(parseConfig.BankName))
                return false;

            if (!CopyFile(bankName, filepath))
                return false;

            var records = _fileprocessor.ReadFromFile(_fileToProcess, parseConfig.HeaderRowAt);
            if (records == null)
                return false;

            parseConfig = GetHeaderIndexMap(parseConfig, records);
            SaveFile(parseConfig, records);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
    public string? ProcessComplexColumn(string columnNameToExtract, string complexColumnValue, char delimiter)
    {
        complexColumnValue = complexColumnValue.Replace(";", "");
        var parts = complexColumnValue.Split(delimiter);

        foreach (var part in parts)
        {
            if (part.StartsWith($"{columnNameToExtract}"))
            {

                return part.Substring(columnNameToExtract.Length).Replace(":", "");
            }

        }
        return null;
    }
    public bool SaveFile(ConfigModel configModel, List<CSVRowModel> data)
    {
        var records = new List<dynamic>();
        if (configModel == null || string.IsNullOrEmpty(configModel.BankName) || data.Count == 0)
            return false;
        for (int i = 1; i < data.Count; i++)
        {
            dynamic record = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)record;
            var row = data[i].Row;

            if (row == null) continue;

            // Process Simple Parameters
            if (configModel.SimpleParamConfigs != null)
            {
                foreach (var item in configModel.SimpleParamConfigs)
                {
                    if (!string.IsNullOrEmpty(item.DestinationColumnName) && item.SourceIndex >= 0 && item.SourceIndex < row.Count)
                    {
                        expandoDict[item.DestinationColumnName] = row[item.SourceIndex];
                    }
                }
            }

            // Process Complex Parameters
            if (configModel.ComplexParamConfigs != null)
            {
                foreach (var item in configModel.ComplexParamConfigs)
                {
                    if (!string.IsNullOrEmpty(item.DestinationColumnName) && item.SourceIndex >= 0 && item.SourceIndex < row.Count)
                    {
                        expandoDict[item.DestinationColumnName] = ProcessComplexColumn(item.ColumnToExtract!, row[item.SourceIndex], item.Delimiter) ?? "";
                    }
                }
            }

            records.Add(record);
        }

        _fileprocessor.WriteToFile(configModel.BankName, records);
        return true;
    }
    public bool CopyFile(string bankName, string filepath)
    {
        try
        {
            if (string.IsNullOrEmpty(bankName) || !IsValidFileType(filepath))
            {
                _message = $"Bank name and valid file path are required. {_message}";
                return false;
            }

            var sanitizedBankName = bankName.ToLower().Replace(" ", "");
            var bankFolder = Path.Combine(basePath, "RawBankFiles", sanitizedBankName);
            var newFileName = $"{sanitizedBankName}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv";

            var destinationFullPath = Path.Combine(bankFolder, newFileName);
            _fileToProcess = destinationFullPath;
            return _fileprocessor.SaveUploadedFile(bankFolder, filepath, destinationFullPath);
        }
        catch (Exception ex)
        {
            _message = $"There was an error copying the file. {ex}.";
            return false;
        }
    }
    public bool UploadFile(string bankName, string filepath)
    {
        throw new NotImplementedException();
    }
}
