using System.Collections.Generic;

namespace GraphsExtensibility.Converters
{
    public interface IConverter
    {
        int[,] Convert(IEnumerable<string> lines);
    }
}