using System.Diagnostics;

namespace Common
{
    public static class Helper
    {
        public static string UpperFirstChar(this string input)
        {
            return input.ToString().ToUpper() + input.Substring(1);
        }

        public static void OpenGivenUrl(this string input)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {input}"));
        }
    }
}
