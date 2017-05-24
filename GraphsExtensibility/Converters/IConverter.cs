using System.Collections.Generic;

namespace GraphsExtensibility.Converters
{
    public interface IConverter
    {
        string[,] Convert(IEnumerable<string> lines);
    }
}