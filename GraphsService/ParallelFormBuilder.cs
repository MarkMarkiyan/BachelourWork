using System;
using System.Collections.Generic;
using GraphSharpDemo.Extensibility;

namespace GraphsService
{
    class ParallelFormBuilder : IParallelFormBuilder
    {
        public List<List<int>> GetParallelForm(int[,] graphAsMatrix)
        {
            var numOfArguments = getNumOfArguments(graphAsMatrix);
            var firstLayer = new List<int>();
            for (int i = 0; i < numOfArguments; i++)
            {
                firstLayer.Add(i + 1);
            }

            var paralellForm = new List<List<int>> {firstLayer};

            var size = Math.Sqrt(graphAsMatrix.Length);
            var currentSize = numOfArguments;

            while (currentSize < size)
            {
                var newLayer = new List<int>();

                for (int column = numOfArguments; column < size; column++)
                {
                    if (isParentValid(paralellForm, column + 1))
                    {
                        continue;
                    }
                    var isNodeValid = true;
                    for (int line = 0; line < size; line++)
                    {
                        if (graphAsMatrix[line, column] != 0 && !isParentValid(paralellForm, line + 1))
                        {
                            isNodeValid = false;
                            break;
                        }
                    }
                    if(isNodeValid)
                     newLayer.Add(column + 1);
                }

                if (newLayer.Count > 0)
                    paralellForm.Add(newLayer);
                currentSize += newLayer.Count;
            }
            return paralellForm; 
        }

        private bool isParentValid(List<List<int>> layers, int nodeIndex) {
            foreach (var layer in layers)
            {
                foreach(var item in layer)
                {
                    if (nodeIndex == item)
                        return true;
                }
            }
            return false;
        }

        private int getNumOfArguments(int[,] graphAsMatrix)
        {
                var size = (int)Math.Sqrt(graphAsMatrix.Length);
            for (int column = 0; column < size; column++)
            {
                for (int line = 0; line < size; line++)
                {
                    if (graphAsMatrix[line, column] != 0)
                    {
                        return column;
                    }
                }
            }
            return size;
        }
    }
}