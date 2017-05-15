using System;
using System.Collections.Generic;
using System.Linq;
using GraphsExtensibility.Converters;

namespace GraphsService.Converters
{
    public class FileToMatrixConverter : IConverter
    {
        private char seperator = ' ';

        public int[,] Convert(IEnumerable<string> lines)
        {
            var size = lines.ToList().Count;
            int[,] graphMatrix = new int[size, size];

            int lineIndex = 0;
            foreach (var line in lines)
            {
                int columnIndex = 0;
                var itemsInRow = line.Split(seperator);
                foreach (var item in itemsInRow)
                {
                    graphMatrix[lineIndex, columnIndex] = Int32.Parse(item);
                    columnIndex++;
                }

                lineIndex++;
            }
            return graphMatrix;
        }
    }
}