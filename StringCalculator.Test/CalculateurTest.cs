using StringCalculator.Test.Utilities;
using System.Threading.Channels;

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
        
        [Fact]
        public void IgnorerPlusDe1000()
        {
            // ETANT DONNE une chaine de la forme "x,y,z" où un élément est supérieur à 1000
            const string chaîne = "1,1001,1000";

            // QUAND on l'envoie à Add
            var résultat = Calculateur.Add(chaîne);

            // ALORS le résultat est le même que le résultat d'une même chaîne sans cet élément
            var chaîneSansGrandNombre = "1, 1000";
            var résultatSansGrandNombre = Calculateur.Add(chaîneSansGrandNombre);

            Assert.Equal(résultatSansGrandNombre, résultat);
        }

        [Theory]
        [InlineData("🎄")]
        [InlineData("🦃")]
        [InlineData("/")]
        [InlineData("//")]
        public void ChangementDélimitateur(string délimitateur)
        {
            // ETANT DONNE une ligne //<délimitateur> avant tout nombre
            var définitionDélimitateur = $"//{délimitateur}" + Environment.NewLine;
            var chaîne = $"1{délimitateur}1";

            // QUAND on envoie la ligne précédente puis 1<délimitateur>1 à Add
            var résultat = Calculateur.Add(définitionDélimitateur + chaîne);

            // ALORS on obtient le même résultat que 1,1
            var résultatDélimitateurVanilla = Calculateur.Add("1,1");

            Assert.Equal(résultatDélimitateurVanilla, résultat);
        }
    }
}