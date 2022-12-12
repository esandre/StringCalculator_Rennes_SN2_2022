namespace StringCalculator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StringAttribute : Attribute
    {
        public string Value { get; }

        public StringAttribute(string value)
        {
            Value = value;
        }
    }
}
