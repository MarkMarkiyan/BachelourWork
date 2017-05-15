namespace GraphsExtensibility.Models
{
    public class Node
    {
        public int Index { get; private set; }

        public int Value { get; private set; }

        public Node(int index, int value)
        {
            Index = index;
            Value = value;
        }
    }
}