using System;
using System.Collections.Generic;
using GraphSharpDemo.Extensibility;

namespace GraphsService
{
    public class ParallelFormBuilder : IParallelFormBuilder
    {
        public List<List<int>> GetParallelForm(int[,] graphAsMatrix)
        {
            if (graphAsMatrix == null)
                return null;

            var numOfArguments = getNumOfArguments(graphAsMatrix);
            var firstLayer = new List<int>();
            for (int i = 0; i < numOfArguments; i++)
            {
                firstLayer.Add(i + 1);
            }

            var paralellForm = new List<List<int>> { firstLayer };

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
                    if (isNodeValid)
                        newLayer.Add(column + 1);
                }

                if (newLayer.Count > 0)
                    paralellForm.Add(newLayer);
                currentSize += newLayer.Count;
            }
            return paralellForm;
        }

        public List<List<int>> GetOptimizeParalellForm(List<List<int>> parallelForm, int councurency)
        {
            var newParallelForm = new List<List<int>>();

            newParallelForm.Add(parallelForm[0]);

            var curentCouncurency = 0;
            var newLevel = new List<int>();

            for (int i = 1; i < parallelForm.Count; i++)
            {
                for (int j = 0; j < parallelForm[i].Count; j++)
                {
                    if (!CheckForDependency(parallelForm, newLevel, parallelForm[i][j])) {
                        newLevel.Add(parallelForm[i][j]);
                        curentCouncurency++;
                        if (curentCouncurency >= councurency)
                        {
                            councurency = 0;
                            newParallelForm.Add(newLevel);
                            newLevel = new List<int>();
                        }
                    }
                }
            }
            return newParallelForm; 
        }

        private bool CheckForDependency(List<List<int>> parallelForm, List<int> level,  int element) {

            for (int i = 0; i < level.Count; i++)
            {
                if (parallelForm[level[i]][element] == 1)
                    return true;
            }

            return false;
        }

        private bool isParentValid(List<List<int>> layers, int nodeIndex)
        {
            foreach (var layer in layers)
            {
                foreach (var item in layer)
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