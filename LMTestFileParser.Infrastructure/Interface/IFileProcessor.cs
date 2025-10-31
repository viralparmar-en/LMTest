using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Infrastructure.Interface;

public interface IFileProcessor
{
    bool SaveUploadedFile();
    public void ReadFromFile();
}