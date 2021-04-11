namespace Data.Validation
{
    public interface IVerifier
    {
        /// <summary>
        /// Verify whether an argument is valid or not.
        /// </summary>
        /// <param name="elem">The argument to verify</param>
        /// <returns>Whether the argument was valid or not</returns>
        bool Verify(string elem);
    }
}
