using System.Collections.Generic;

namespace Data.Validation
{
    /// <summary>
    /// Handles validating the arguments
    /// </summary>
    class Validator
    {
        /// <summary>
        /// Validate all arguments against the verifiers
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="verifiers"></param>
        /// <returns></returns>
        public static bool Validate(List<string> arguments, List<IVerifier> verifiers)
        {
            if (arguments.Count < verifiers.Count)
            {
                return false;
            }

            for (int i = 0; i < verifiers.Count; ++i)
            {
                if (!verifiers[i].Verify(arguments[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}