namespace LMTestFileParser.Domain.Models;

public class ParamConfigModel
{
    public int SourceIndex { get; set; } = 0;
    public string? SourceColunn { get; set; }
    public string? DestinationColumnName { get; set; }
}