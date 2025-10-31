using System.Globalization;
using CsvHelper;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Infrastructure;

public class CSVFileProcessor : IFileProcessor
{
    public bool SaveUploadedFile()
    {
        throw new NotImplementedException();
    }
    public void ReadFromFile()
    {
        string basePath = AppContext.BaseDirectory;
        string filePath = Path.Combine(basePath, "DataExtractor_Example_Input.csv");
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            while (csv.Read())
            {
                var record = csv.Parser.RawRecord;
                Console.WriteLine(record.ToString());
                // Do something with the record.
            }
        }
    }
}