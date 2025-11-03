
using LMTestFileParser.Domain.Models;

namespace LMTestFileParser.Tests.CustomComparers;

public class SimpleParamConfigComparer : IEqualityComparer<SimpleParamConfigModel>
{
    public bool Equals(SimpleParamConfigModel x, SimpleParamConfigModel y)
    {
        if (x == null || y == null)
            return false;

        return x.DestinationColumnName == y.DestinationColumnName &&
               x.SourceColunn == y.SourceColunn &&
               x.SourceIndex == y.SourceIndex;
    }

    public int GetHashCode(SimpleParamConfigModel obj)
    {
        if (obj == null)
            return 0;

        return HashCode.Combine(obj.DestinationColumnName, obj.SourceColunn, obj.SourceIndex);
    }
}

