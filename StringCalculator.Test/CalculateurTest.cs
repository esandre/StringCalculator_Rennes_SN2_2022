using StringCalculator.Test.Utilities;

namespace StringCalculator.Test
{
    public class CalculateurTest
    {
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
        public void TestSautsLigne()
        {
            // ETANT DONNE une chaîne "x, y" ayant un saut de ligne en cours de nombre
            var chaîne = "1,1" + Environment.NewLine + "0";

            // QUAND on l'envoie à Add
            var résultat = Calculateur.Add(chaîne);

            // ALORS on obtient un entier x+y
            Assert.Equal<uint>(11, résultat);
        }

        // Si des nombres négatifs sont présents une exception est lancée, elle contient les nombres et leurs positions.

        [Fact]
        public void NombresNegatif()
        {
            // ETANT DONNE une chaîne "-1, 0"
            const string chaîne = $"-1,0";

            // QUAND on l'envoie à Add
            void Act() => Calculateur.Add(chaîne);

            // ALORS une exception est lancée
            var exception = Assert.Throws<NombreNegatifException>(Act);

            // ET elle contient 1 en position
            Assert.Equal(1, exception.Position);

            // ET elle contient -1 comme nombre fautif
            Assert.Equal("-1", exception.NombreFautif);
        }

        [Fact]
        public void DeuxNombresNegatif()
        {
            // ETANT DONNE une chaîne "-1, -2"
            const string chaîne = $"-1,-2";

            // QUAND on l'envoie à Add
            void Act() => Calculateur.Add(chaîne);

            // ALORS une exception est lancée
            var exceptionMère = Assert.Throws<AggregateException>(Act);
            var exceptionsFilles = exceptionMère
                .InnerExceptions
                .OfType<NombreNegatifException>()
                .ToArray();

            // ET elle contient deux NombreNegatifException
            Assert.Equal(2, exceptionsFilles.Count());

            // ET chacune contient la position et le nombre fautif
            Assert.Contains(exceptionsFilles, exception => exception.Position == 1 && exception.NombreFautif == "-1");
            Assert.Contains(exceptionsFilles, exception => exception.Position == 2 && exception.NombreFautif == "-2");
        }
    }
}