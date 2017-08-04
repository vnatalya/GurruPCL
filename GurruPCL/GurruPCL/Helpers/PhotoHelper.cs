using GurruPCL.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace GurruPCL.Helpers
{
    public static class PhotoHelper
    {
        public static string GetEmail(string text)
        {
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(text);           

            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in emailMatches)
            {
                sb.AppendLine(emailMatch.Value);
            }

            return sb.ToString();
        }

        public static string GetPhone(string text)
        {
            var exp = new Regex(@"(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}", RegexOptions.IgnoreCase);
            
            MatchCollection phoneMatches = exp.Matches(text);
            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in phoneMatches)
            {
                sb.AppendLine(emailMatch.Value);
            }

            return sb.ToString();
        }
    }
}
