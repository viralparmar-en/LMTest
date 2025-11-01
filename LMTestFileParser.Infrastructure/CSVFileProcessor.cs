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
            Console.WriteLine("Here");
            string basePath = AppContext.BaseDirectory;

            using var reader = new StreamReader("DataExtractor_Example_Input.csv");
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

    public void WriteToFile(string filePath)
    {
        string basePath = AppContext.BaseDirectory;
        var records = new List<dynamic>();
        dynamic record = new ExpandoObject();
        using (var writer = new StreamWriter(Path.Combine(basePath, "ofile.csv")))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(records);
            writer.Flush();
            //Console.WriteLine(recordRead.ToString());
            // record.Id = 1;
            // record.Name = "one";
            //records.Add(recordRead);
            // Do something with the record.
            // writer.ToString();
        }
    }
}