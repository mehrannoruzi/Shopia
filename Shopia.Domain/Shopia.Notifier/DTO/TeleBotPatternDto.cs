using System.Text.RegularExpressions;

namespace Shopia.Domain
{
    public class TeleBotPatternDto
    {
        public Regex Pattern { get; set; }
        public TeleBotRequestType TeleBotRequestType { get; set; }

        public TeleBotPatternDto(string pattern, TeleBotRequestType type)
        {
            Pattern = new Regex(pattern);
            TeleBotRequestType = type;
        }
    }
}
