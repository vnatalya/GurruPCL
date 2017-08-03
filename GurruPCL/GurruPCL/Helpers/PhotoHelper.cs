using GurruPCL.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace GurruPCL.Helpers
{
    public static class PhotoHelper
    {
        static string text;
        public static Form GetFormFrom(string photo)
        {
            text = photo;
            return new Form()
            {
                BusinessPhone = GetPhone(),
                Email  = GetEmail(),
                OrganizationName = text
            };
        }

        static string GetEmail()
        {
            Regex emailRegex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(text);           

            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in emailMatches)
            {
                text = text.Replace(emailMatch.ToString(), "");
                sb.AppendLine(emailMatch.Value);
            }

            return sb.ToString();
        }

        static string GetPhone()
        {
            var exp = new Regex(@"(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}", RegexOptions.IgnoreCase);
            
            MatchCollection phoneMatches = exp.Matches(text);
            StringBuilder sb = new StringBuilder();

            foreach (Match emailMatch in phoneMatches)
            {
                text = text.Replace(emailMatch.ToString(), "");
                sb.AppendLine(emailMatch.Value);
            }

            return sb.ToString();
        }
    }
}
