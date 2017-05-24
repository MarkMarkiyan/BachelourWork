namespace GraphsExtensibility.Models
{
    public class Node
    {
        public int Index { get; private set; }

        public string Value { get; private set; }

        public Node(int index, string value)
        {
            Index = index;
            Value = value;
        }
    }
}