using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Infrastructure.Interface;

public interface IConfigReader
{
    ConfigModel GetConfigByBankName(string bankName);
}