using System.Collections.Generic;

namespace GraphsExtensibility.Converters
{
    public interface IFileReaderService
    {
        List<string> ReadFile(string fileName);
    }
}