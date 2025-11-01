namespace LMTestFileParser.Domain.Models;

public class ConfigModel
{
    public string? BankName { get; set; }
    public int HeaderRowAt { get; set; }
    public List<SimpleParamConfigModel>? SimpleParamConfigs { get; set; }
    public List<ComplexParamConfigModel>? ComplexParamConfigs { get; set; }
}