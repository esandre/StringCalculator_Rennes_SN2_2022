namespace StringCalculator
{
    public static class UIntEnumerableExtensions
    {
        public static uint Sum(this IEnumerable<uint> elements)
            => elements.Aggregate((previous, element) => previous + element);
    }
}
