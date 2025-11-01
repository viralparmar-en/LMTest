using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Infrastructure.Interface;

public interface IFileProcessor
{
    bool SaveUploadedFile(string bankFolder,string filePath, string folderPath);
    public void ReadFromFile(string filePath);
    public void WriteToFile(string filePath);
}