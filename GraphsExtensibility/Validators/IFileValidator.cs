using System.Collections.Generic;

namespace AutoGraph.Extensibility.Validators
{
    public interface IFileValidator
    {
        bool IsFileValideMatrix(IEnumerable<string> fileLines);
    }
}