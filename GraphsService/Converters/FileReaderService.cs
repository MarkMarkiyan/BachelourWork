using System.Collections.Generic;
using System.IO;
using GraphsExtensibility.Converters;
using System.Text;

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

        public List<string> ReadTextFromFile(Stream stream)
        {
            var lines = new List<string>();

            string text;

            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
               text = reader.ReadToEnd();
            }
            foreach (var item in text.Split('\n')) 
            {
                lines.Add(item);
            }

            stream.Close();
            return lines;
        }
    }
}