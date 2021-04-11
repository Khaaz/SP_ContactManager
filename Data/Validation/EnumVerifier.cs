using Data.Structures;

namespace Data.Validation
{
    class EnumVerifier : IVerifier
    {
        public bool Verify(string elem)
        {
            try
            {
                elem.GetEnum<Relationships>();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
