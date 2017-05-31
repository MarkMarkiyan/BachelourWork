using System.Collections.Generic;

namespace GraphSharpDemo.Extensibility
{
    public interface IParallelFormBuilder
    {
        List<List<int>> GetOptimizedParallelForm(string[,] graphAsMatrix, int cuncurency);

        List<List<int>> GetParallelForm(string[,] graphAsMatrix);
    }
}