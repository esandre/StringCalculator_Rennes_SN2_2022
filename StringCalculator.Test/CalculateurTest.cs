namespace StringCalculator.Test
{
    public class CalculateurTest
    {
        [Fact]
        public void Test2Nombres()
        {
            // ETANT DONNE une chaîne "0, 0"
            const string chaîne = "0,0";

            // QUAND on l'envoie à Add
            var résultat = Calculateur.Add(chaîne);

            // ALORS on obtient un entier
            Assert.Equal(0, résultat);
        }
    }
}