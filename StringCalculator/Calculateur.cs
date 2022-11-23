namespace StringCalculator
{
    public static class Calculateur
    {
        public static uint Add(string chaîne)
        {
            var elementsAsString = chaîne
                .Replace(Environment.NewLine, string.Empty)
                .Split(',');

            try
            {
                return elementsAsString
                    .AsParallel()
                    .Select(element => ParseWithChecks(element, elementsAsString))
                    .Sum();
            } 
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Count == 1)
                    throw ex.InnerExceptions[0];
                else throw;
            }
        }

        private static uint ParseWithChecks(string element, IEnumerable<string> allElements)
        {
            try
            {
                return uint.Parse(element);
            }
            catch (OverflowException)
            {
                var indexOf = new List<string>(allElements).IndexOf(element);
                throw new NombreNegatifException(indexOf + 1, element);
            }
        }
    }
}