namespace LMTestFileParser.Domain.Models;

public class ComplexParamConfigModel : ParamConfigModel
{
    public string? ColumnToExtract { get; set; }
    public string? Delimiter { get; set; }
}
