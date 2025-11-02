using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Tests.Mocks;

public class FakeCSVFileProcessor : IFileProcessor
{
    public List<CSVRowModel> ReadFromFile(string filePath, int HeaderAt)
    {
        throw new NotImplementedException();
    }

    public bool SaveUploadedFile(string bankFolder, string filePath, string folderPath)
    {
        return true;
    }

    public void WriteToFile(List<dynamic> records)
    {
        throw new NotImplementedException();
    }
}