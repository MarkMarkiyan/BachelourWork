using System.Collections.Generic;
using System.IO;
using GraphsExtensibility.Converters;

namespace AutoGraph.Service.Converters
{
    public class FileReaderService : IFileReaderService
    {
        public List<string> ReadFile(string fileName)
        {
            var lines = new List<string>();
            var file = new StreamReader(fileName);

            string line;
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);
            }
            file.Close();
            return lines;
        }
    }
}