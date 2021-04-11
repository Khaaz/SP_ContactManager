using System.Text.RegularExpressions;

namespace Data.Validation
{
    class RegexVerifier : IVerifier
    {
        public string Pattern { get; private set; }
        public RegexVerifier(string regex)
        {
            Pattern = regex;
        }

        public bool Verify(string elem)
        {
            Match m = Regex.Match(elem, Pattern);
            return m.Success;
        }
    }
}