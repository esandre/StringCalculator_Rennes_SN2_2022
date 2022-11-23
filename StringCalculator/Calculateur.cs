namespace StringCalculator
{
    public static class Calculateur
    {
        public static uint Add(string chaîne)
        {
            return chaîne
                .Split(',')
                .Select(uint.Parse)
                .Sum();
        }
    }
}