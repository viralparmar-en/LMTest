using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Tests.CustomComparers;

public class ComplexParamConfigComparer : IEqualityComparer<ComplexParamConfigModel>
{
    public bool Equals(ComplexParamConfigModel x, ComplexParamConfigModel y)
    {
        if (x == null || y == null)
            return false;

        return x.ColumnToExtract == y.ColumnToExtract &&
               x.Delimiter == y.Delimiter &&
               x.DestinationColumnName == y.DestinationColumnName &&
               x.SourceColunn == y.SourceColunn &&
               x.SourceIndex == y.SourceIndex;
    }

    public int GetHashCode(ComplexParamConfigModel obj)
    {
        if (obj == null)
            return 0;

        return HashCode.Combine(
            obj.ColumnToExtract,
            obj.Delimiter,
            obj.DestinationColumnName,
            obj.SourceColunn,
            obj.SourceIndex
        );
    }
}