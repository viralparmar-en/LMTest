using System.Dynamic;
using System.Globalization;
using CsvHelper;
using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Infrastructure;

public class CSVFileProcessor : IFileProcessor
{
    public bool SaveUploadedFile(string bankFolder, string source, string destination)
    {
        // Ensure the folder exists
        if (!Directory.Exists(bankFolder))
        {
            Directory.CreateDirectory(bankFolder);
        }
        File.Copy(source, destination);
        return true;
    }
    public void ReadFromFile(string filePath)
    {
        string basePath = AppContext.BaseDirectory;
        filePath = Path.Combine(basePath, "DataExtractor_Example_Input.csv");
        var list = new List<CSVRowModel>();
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        while (csv.Read())
        {
            var recordRead = csv.Parser.Record!.ToList();
            list.Add(new CSVRowModel()
            {
                Row = recordRead
            });
        }
    }

    public void WriteToFile(string filePath)
    {
        string basePath = AppContext.BaseDirectory;
        var records = new List<dynamic>();
        dynamic record = new ExpandoObject();
        using (var writer = new StreamWriter(Path.Combine(basePath, "ofile.csv")))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(records);
            //Console.WriteLine(recordRead.ToString());
            // record.Id = 1;
            // record.Name = "one";
            //records.Add(recordRead);
            // Do something with the record.
            // writer.ToString();
        }
    }
}