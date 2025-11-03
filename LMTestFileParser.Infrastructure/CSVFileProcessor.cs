using System.Dynamic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
    public List<CSVRowModel> ReadFromFile(string filePath, int HeaderAt)
    {
        try
        {
            string basePath = AppContext.BaseDirectory;
            Console.WriteLine(filePath);
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            //Read until the header row is reached
            for (int i = 0; i < HeaderAt; i++)
            {
                csv.Read();
            }

            //Read the header ahd insert the header into first row of the list
            csv.ReadHeader();
            if (csv.Parser.Record != null)
            {
                var list = new List<CSVRowModel>
                {
                    new()
                    {
                        Row = [.. csv.Parser.Record]
                    }
                };
                // Read the remaining subsequent rows and insert into list
                while (csv.Read())
                {

                    var recordRead = csv.Parser.Record!.ToList();
                    list.Add(new CSVRowModel()
                    {
                        Row = recordRead
                    });
                }
                return list;
            }
            else
            {
                return [];
            }
        }
        catch (Exception)
        {
            return [];
        }
    }

    public void WriteToFile(string bankName, List<dynamic> records)
    {
        var outputDirectory = Path.Combine("OutputFiles", bankName);
        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }
        string basePath = AppContext.BaseDirectory;
        string filename = Path.Combine(basePath, outputDirectory, $"{bankName}_{DateTime.UtcNow:yyyyMMdd_HHmmss}.csv");
        using var writer = new StreamWriter(filename);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(records);
        Console.WriteLine($"Saving successful at {filename}");
        writer.Flush();

    }
}