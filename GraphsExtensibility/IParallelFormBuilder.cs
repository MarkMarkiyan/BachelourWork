using System.Collections.Generic;

namespace GraphSharpDemo.Extensibility
{
    public interface IParallelFormBuilder
    {
        List<List<int>> GetParallelForm(int[,] graphAsMatrix);
    }
}