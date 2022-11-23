using StringCalculator.Test.Utilities;

namespace StringCalculator.Test
{
    public class CalculateurTest
    {
        private static readonly Random Random = new Random();

        private static IEnumerable<uint> CasACombiner 
            => new uint[] { 0, 1, uint.MaxValue / 2, Random.NextUint(uint.MaxValue / 2) };

        public static IEnumerable<object[]> CasTests2Nombres 
            => new CartesianProduct(CasACombiner, CasACombiner);

        [Theory]
        [MemberData(nameof(CasTests2Nombres))]
        public void Test2Nombres(uint x, uint y)
        {
            // ETANT DONNE une chaîne "x, y"
            var chaîne = $"{x},{y}";

            // QUAND on l'envoie à Add
            var résultat = Calculateur.Add(chaîne);

            // ALORS on obtient un entier x+y
            Assert.Equal(x + y, résultat);
        }

        [Fact]
        public void NombresNegatif()
        {
            // ETANT DONNE une chaîne "-1, -1"
            var chaîne = $"-1,-1";

            // QUAND on l'envoie à Add
            void Act() => Calculateur.Add(chaîne);

            // ALORS une exception est lancée
            Assert.Throws<OverflowException>(Act);
        }
    }
}