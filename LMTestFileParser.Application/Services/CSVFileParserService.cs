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
        if (records != null)
        {
            if (parseConfig.SimpleParamConfigs != null && parseConfig.SimpleParamConfigs.Count > 0)
            {
                foreach (var source in parseConfig.SimpleParamConfigs)
                {
                    var result = parseConfig.SimpleParamConfigs.Where(x => x.SourceColunn == source.SourceColunn!);
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            item.SourceIndex = records[0].Row!.IndexOf(source.SourceColunn!);
                        }
                    }
                    //SimpleColumnIndexHeaderMap.Add(records[0].Row!.IndexOf(source.SourceColunn!), source.SourceColunn!);
                }
            }
            if (parseConfig.ComplexParamConfigs != null && parseConfig.ComplexParamConfigs.Count > 0)
            {
                foreach (var source in parseConfig.ComplexParamConfigs)
                {
                    var result = parseConfig.ComplexParamConfigs.Where(x => x.SourceColunn == source.SourceColunn!);
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            item.SourceIndex = records[0].Row!.IndexOf(source.SourceColunn!);
                        }
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
        catch (Exception)
        {
            _message = $"Something went wrong, check errlog.txt file for further details.";
            return false;
        }

    }
    public bool ProcessFile(string bankName, string filepath)
    {
        try
        {
            if (!string.IsNullOrEmpty(bankName) && !string.IsNullOrEmpty(filepath) && IsValidFileType(filepath))
            {
                var parseConfig = GetConfigForABank(bankName);
                if (parseConfig != null && !string.IsNullOrEmpty(parseConfig.BankName))
                {
                    if (CopyFile(bankName, filepath))
                    {
                        var records = _fileprocessor.ReadFromFile(_fileToProcess, parseConfig.HeaderRowAt);

                        if (records != null)
                        {
                            parseConfig = GetHeaderIndexMap(parseConfig, records);
                            SaveFile(parseConfig, records);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
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

        // Dictionary<int, string> CombinedColumns = [];
        // foreach (var column in SimpleColumns)
        // {
        //     CombinedColumns[column.Key] = column.Value;
        // }
        // foreach (var column in ComplexColumns)
        // {
        //     CombinedColumns[column.Key] = column.Value;
        // }
        for (int i = 1; i < data.Count; i++)
        {
            dynamic record = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)record;

            if (configModel.SimpleParamConfigs != null)
            {
                foreach (var item in configModel.SimpleParamConfigs)
                {
                    expandoDict[configModel.SimpleParamConfigs!.First(x => x.SourceColunn == item.SourceColunn).DestinationColumnName!] = data[i].Row![item.SourceIndex];
                }
            }
            if (configModel.ComplexParamConfigs != null)
            {
                foreach (var item in configModel.ComplexParamConfigs)
                {
                    Console.WriteLine(item.ColumnToExtract);
                    expandoDict[configModel.ComplexParamConfigs.First(x => x.ColumnToExtract == item.ColumnToExtract).DestinationColumnName!] = ProcessComplexColumn(configModel.ComplexParamConfigs.First(x => x.ColumnToExtract == item.ColumnToExtract).ColumnToExtract!, data[i].Row![item.SourceIndex], '|') ?? "";
                }

            }
            records.Add(record);
        }
        _fileprocessor.WriteToFile(records);
        //  _fileprocessor.ReadFromFile("", 2);
        return true;
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
