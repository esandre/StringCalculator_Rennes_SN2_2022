namespace StringCalculator
{
    public class NombreNegatifException : OverflowException
    {
        internal NombreNegatifException(int position, string nombreFautif)
        {
            Position = position;
            NombreFautif = nombreFautif;
        }

        public int Position { get; }
        public string NombreFautif { get; }
    }
}
