using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Infrastructure.Interface;

public interface IFileProcessor
{
    bool SaveUploadedFile(string bankFolder, string filePath, string folderPath);
    List<CSVRowModel> ReadFromFile(string filePath, int HeaderAt);
    void WriteToFile(string bankName, List<dynamic> records);
}