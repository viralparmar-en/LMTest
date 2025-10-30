namespace LMTestFileParser.Application.Interface;

// Check if valid Bank name and is provided
// Check if valid file and is of type csv
// Get the column names of the file uploaded
// Parse the json file with bank names and column names for each bank
public interface ICSVFileParserService
{
    bool IsValidFile(string filename);
//     using (var reader = new StreamReader("file.csv"))
// using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
// {
//     var records = csv.GetRecords<MyClass>();
//}
    // if (args.Length > 0)
    // {
    //     string bankName = args[0];
    //     string filePath = args[1];
    //     if (File.Exists(filePath))
    //     {
    //         string content = File.ReadAllText(filePath);
    //         Console.WriteLine(content);
    //     }
    //     else
    //     {
    //         Console.WriteLine("File not found.");
    //     }
    // }
}
