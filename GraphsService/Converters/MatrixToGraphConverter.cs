 using System.Collections.Generic;
using System.Linq;
using GraphsExtensibility.Converters;
using GraphsExtensibility.Models;
using GraphsService.Comparers;

namespace GraphsService.Converters
{
    public class MatrixToGraphConverter : IMatrixToGraphConverter
    {
        public List<NodesConnection> Convert(string[,] matrix)
        {
            var nodes = new List<Node>();
            var connections = new List<NodesConnection>();

            int size = (int) System.Math.Sqrt(matrix.Length);

            for (var line = 0; line < size; line++)
            {
                for (var column = 0; column < size; column++)
                {
                    if (matrix[line, column] != "0" && matrix[line, column] != "0\r")
                    {
                        string value = matrix[line, column];
                        var nodeA = new Node(line+1, value);
                        var nodeB = new Node(column + 1, value);

                        connections.Add(new NodesConnection
                        {
                            Child = nodeB,
                            Parent = nodeA
                        });
                        if (!nodes.Contains(nodeA, new NodeComparer()))
                        {
                            nodes.Add(nodeA);
                        }
                        if (!nodes.Contains(nodeB, new NodeComparer()))
                        {
                            nodes.Add(nodeB);
                        }
                    }
                }
            }
            return connections;
        }
    }
} 