using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OXY.Net.Framework.Utils
{
    public class StringMatch
    {
        public StringMatch(string restrictToPattern, string skipPattern)
        {
            this.RestrictToPattern = restrictToPattern;
            this.SkipPattern = skipPattern;
        }

        public string RestrictToPattern { get; set; }
        public string SkipPattern { get; set; }

        public bool Matches(string value)
        {
            bool skip = false;
            bool restrict = true;

            if (!string.IsNullOrEmpty(SkipPattern))
                skip = Matches(value, SkipPattern);
            if (!string.IsNullOrEmpty(RestrictToPattern))
                restrict = Matches(value, RestrictToPattern);

            return (!skip && restrict);
        }

        protected bool Matches(string value, string pattern)
        {
            return Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
