using LMTestFileParser.Domain.Models;
using LMTestFileParser.Infrastructure.Interface;

namespace LMTestFileParser.Tests.Mocks;

public class FakeJsonConfigReader : IConfigReader
{
    public ConfigModel GetConfigByBankName(string bankName)
    {
        throw new NotImplementedException();
    }
}