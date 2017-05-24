using System.Collections.Generic;
using System.IO;

namespace GraphsExtensibility.Converters
{
    public interface IFileReaderService
    {
        List<string> ReadTextFromFile(Stream file);

        List<string> ReadFile(string fileName);
    }
}