using System.Collections.Generic;
using GraphsExtensibility.Models;

namespace GraphsService.Comparers
{
    public class NodeComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node x, Node y)
        {
            return x.Index == y.Index;
        }

        public int GetHashCode(Node obj)
        {
            return GetHashCode(obj);
        }
    }
}
