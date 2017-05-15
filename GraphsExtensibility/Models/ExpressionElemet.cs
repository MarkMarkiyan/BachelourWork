namespace GraphsExtensibility.Models
{
    public class ExpressionElement
    {

        public ExpressionElement(string value) {
            this.Value = value;
        }
        string Value { get; set; }

        bool IsNumber
        {
            get
            {
                int result;
                return int.TryParse(Value, out result);
            }
        }
    }
}