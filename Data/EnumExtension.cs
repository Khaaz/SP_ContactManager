using System;

namespace Data
{
    public static class EnumExtension
    {
        /// <summary>
        /// Extend String to add a generic GetEnum method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_string"></param>
        /// <returns>The Enum representation for this string</returns>
        public static T GetEnum<T>(this string _string)
        {
            return (T)Enum.Parse(typeof(T), _string, true);

        }

        /// <summary>
        /// Extend Enum to add a generic GetString method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_enum"></param>
        /// <returns>The String representation for this enum</returns>
        public static String GetString<T>(this T _enum)
        {
            return Enum.GetName(typeof(T), _enum).ToLower();
        }
    }
}
