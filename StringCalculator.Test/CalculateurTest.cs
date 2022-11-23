using StringCalculator.Test.Utilities;

namespace StringCalculator.Test
{
    public class CalculateurTest
    {
        private static readonly Random Random = new Random();
        
        public static IEnumerable<object[]> CasTestsNNombres 
            => new AdditionTestCaseGenerator(new Random()).Generate();

        [Theory]
        [MemberData(nameof(CasTestsNNombres))]
        public void TestNNombres(params uint[] parameters)
        {
            // ETANT DONNE une chaîne "x, y, ..."
            var chaîne = string.Join(',', parameters);

            // QUAND on l'envoie à Add
            var résultat = Calculateur.Add(chaîne);

            // ALORS on obtient un entier x+y+...
            Assert.Equal(parameters.Sum(), résultat);
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