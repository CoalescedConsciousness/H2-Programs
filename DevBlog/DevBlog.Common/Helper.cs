using System.Diagnostics;

namespace Common
{
    public static class Helper
    {
        /// <summary>
        /// Extension Method for making the first letter in a string uppercase.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UpperFirstChar(this string input)
        {
            return input.ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Extension Method for opening an URL in the commandline.
        /// </summary>
        /// <param name="input"></param>
        public static void OpenGivenUrl(this string input)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {input}"));
        }
    }
}
