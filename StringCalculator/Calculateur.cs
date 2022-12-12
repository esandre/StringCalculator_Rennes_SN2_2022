namespace StringCalculator
{
    [String("Test")]
    public static class Calculateur
    {
        public static uint Add(string chaîne)
        {
            var délimitateur = ",";

            if (chaîne.StartsWith("//"))
            {
                var lignes = chaîne.Split(Environment.NewLine);

                délimitateur = new string(lignes.First().Skip(2).ToArray());
                chaîne = string.Join(Environment.NewLine, lignes.Skip(1));
            }

            var elementsAsString = chaîne
                .Replace(Environment.NewLine, string.Empty)
                .Split(délimitateur);

            try
            {
                return elementsAsString
                    .AsParallel()
                    .Select(element => ParseWithChecks(element, elementsAsString))
                    .Where(nombre => nombre <= 1000)
                    .Sum();
            } 
            catch (AggregateException ex)
            {
                if (ex.InnerExceptions.Count == 1)
                    throw ex.InnerExceptions[0];
                throw;
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