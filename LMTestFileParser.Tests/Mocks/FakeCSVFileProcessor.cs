using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Tests.Mocks;

public class FakeCSVFileProcessor : IFileProcessor
{

    public List<CSVRowModel> ReadFromFile(string filePath)
    {
        throw new NotImplementedException();
    }
    public bool SaveUploadedFile(string bankFolder, string filePath, string folderPath)
    {
        return true;
    }

    public void WriteToFile(string filePath)
    {
        throw new NotImplementedException();
    }


}