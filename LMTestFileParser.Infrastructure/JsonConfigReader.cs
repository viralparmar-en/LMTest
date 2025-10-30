using System.Text.Json;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Infrastructure;

public class JsonConfigReader : IJsonConfigReader
{
    //Refactor
    public T ReadJsonFile<T>(string fileName)
    {
        try
        {
            string basePath = AppContext.BaseDirectory;
            string filePath = Path.Combine(basePath, fileName);

            if (!File.Exists(filePath))
                throw new FileNotFoundException(" file not found", filePath);

            string jsonContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonContent);

        }
        catch (Exception)
        {
            return default;
        }
    }
}
