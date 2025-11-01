using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Tests.Mocks;

public class FakeCSVFileProcessor : IFileProcessor
{
    public void ReadFromFile(string filePath)
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