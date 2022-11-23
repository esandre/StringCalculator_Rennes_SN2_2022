namespace StringCalculator.Test.Utilities
{
    internal class AdditionTestCaseGenerator
    {
        private const uint MaxValueForAnElement = 1000;
        private const int MaxNumberOfElementsToTest = 1000; //int.MaxValue trop coûteux en temps de test

        private readonly List<int> _nombreElementsATester = new ();
        private readonly List<Func<int, IEnumerable<uint>>> _casATester = new ();

        private AdditionTestCaseGenerator()
        {
            _casATester.Add(CasToutZero);
            _casATester.Add(CasToutUn);
            _casATester.Add(CasMaximalRéparti);
            _casATester.Add(CasMaximalUnique);

            _nombreElementsATester.Add(1);
            _nombreElementsATester.Add(2);
            _nombreElementsATester.Add(3);
            _nombreElementsATester.Add(MaxNumberOfElementsToTest); 
        }

        public AdditionTestCaseGenerator(Random random) : this()
        {
            _casATester.Add(nbElements => CasAléatoireRéparti(nbElements, random));

            _nombreElementsATester.Add(random.Next(MaxNumberOfElementsToTest));
        }

        private static uint MaxValueForANumberOfElements(int nombreElements)
        {
            var maxParElement = (uint)(uint.MaxValue / nombreElements);
            return maxParElement > MaxValueForAnElement ? MaxValueForAnElement : maxParElement;
        }

        private static IEnumerable<uint> CasToutZero(int nombreElements)
            => Enumerable.Repeat<uint>(0, nombreElements);

        private static IEnumerable<uint> CasToutUn(int nombreElements)
            => Enumerable.Repeat<uint>(1, nombreElements);

        private static IEnumerable<uint> CasMaximalRéparti(int nombreElements)
            => Enumerable.Repeat(MaxValueForANumberOfElements(nombreElements), nombreElements);

        private static IEnumerable<uint> CasMaximalUnique(int nombreElements)
            => CasToutZero(nombreElements - 1).Append(MaxValueForAnElement);

        private static IEnumerable<uint> CasAléatoireRéparti(int nombreElements, Random random)
            => Enumerable.Repeat(random.NextUint(MaxValueForANumberOfElements(nombreElements)), nombreElements);

        public IEnumerable<object[]> Generate()
        {
            foreach (var nombreElementsIntéressant in _nombreElementsATester)
            {
                foreach (var casATester in _casATester)
                {
                    yield return casATester(nombreElementsIntéressant).Cast<object>().ToArray();
                }
            }
        }
    }
}
