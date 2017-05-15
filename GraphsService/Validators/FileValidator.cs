using System.Collections.Generic;
using AutoGraph.Extensibility.Validators;

namespace AutoGraph.Service.Validators
{
    public class FileValidator : IFileValidator
    {
        public bool IsFileValideMatrix(IEnumerable<string> fileLines)
        {
            return true;
        }
    }
}