using System.Collections.Generic;
using GraphsExtensibility.Models;

namespace GraphsExtensibility.Converters
{
    public interface IMatrixToGraphConverter
    {
        List<NodesConnection> Convert(string[,] matrix);

        string[,] ConvertGraphToMatrix(List<NodesConnection> graph);

    }
}