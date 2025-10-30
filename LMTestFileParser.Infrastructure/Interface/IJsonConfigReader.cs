namespace LMTestFileParser.Infrastructure.Interface;

public interface IJsonConfigReader
{
    T ReadJsonFile<T>(string fileName);
}