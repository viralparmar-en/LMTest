using System.Text.Json;
using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Infrastructure;

public class JsonConfigReader : IConfigReader
{
    //Refactor
    public ConfigModel GetConfigByBankName(string bankName)
    {
        try
        {
            string basePath = AppContext.BaseDirectory;
            string filePath = Path.Combine(basePath, "outputconfig.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Config file not found", filePath);

            string jsonContent = File.ReadAllText(filePath);
            var jsonObj = JsonSerializer.Deserialize<OutputConfigModel>(jsonContent);
            if (jsonObj != null && jsonObj.Configs != null)
            {
                return jsonObj.Configs
                            .FirstOrDefault(x => x.BankName == bankName) ?? new();
            }
            else
            {
                return new();
            }

        }
        catch (Exception ex)
        {
            throw new FileNotFoundException(ex.ToString());
        }
    }
}
