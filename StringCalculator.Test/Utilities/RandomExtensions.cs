namespace StringCalculator.Test.Utilities
{
    internal static class RandomExtensions
    {
        public static uint NextUint(this Random random, uint maxValue) 
            => (uint) random.NextInt64(maxValue);
    }
}
