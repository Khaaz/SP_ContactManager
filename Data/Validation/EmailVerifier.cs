using System.ComponentModel.DataAnnotations;

namespace Data.Validation
{
    class EmailVerifier : IVerifier
    {
        public bool Verify(string elem)
        {
            return elem != null && new EmailAddressAttribute().IsValid(elem);
        }
    }
}
