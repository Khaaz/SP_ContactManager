namespace Data.Validation
{
    class StringVerifier : IVerifier
    {
        public int Minimum { get; private set; }
        public int Maximum { get; private set; }
        public StringVerifier(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        public bool Verify(string elem)
        {
            return elem.Length >= Minimum && elem.Length <= Maximum;
        }
    }
}
