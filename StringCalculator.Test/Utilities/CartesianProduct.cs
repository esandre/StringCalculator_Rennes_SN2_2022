using System.Collections;

namespace StringCalculator.Test.Utilities
{
    internal class CartesianProduct : IEnumerable<object[]>
    {
        private readonly IEnumerable[] _parameters;

        public CartesianProduct(params IEnumerable[] parameters)
        {
            _parameters = parameters;
        }

        private object[][] InlineData => _parameters
            .Select(p => p.Cast<object>().ToArray())
            .ToArray();

        private IEnumerable<object[]> AddParameter(
            object[][] combinaisons,
            object[] paramètre)
        {
            foreach (var valeur in paramètre)
            foreach (var combinaison in combinaisons)
                yield return combinaison.Append(valeur).ToArray();
        }

        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            var combinaisons = InlineData
                .First()
                .Select(value => new object[] { value })
                .ToArray();

            foreach (var paramètreSupplémentaire in InlineData.Skip(1))
                combinaisons = AddParameter(combinaisons, paramètreSupplémentaire).ToArray();

            return combinaisons.Cast<object[]>().GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
