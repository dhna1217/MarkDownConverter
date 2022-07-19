using System.Text.RegularExpressions;

namespace MarkDownConverter.Parser.Inline;

public static class Anchor
{
    public const string ANCHOR = @"\[(.+?)\]\((|[^\s]+)\)"; //No space allowed in parenthesis
    public const string ANCHOR2 = @"\[([^\]]+)\]\(([^\)]+)\)"; //Space allowed in parenthesis

    public static string Process(ParserType parserType, string line)
    {
        if(parserType == ParserType.RegularExpression)
        {
            var regex = new Regex(ANCHOR);

            return regex.Replace(line, "<a href=\"$2\">$1</a>");
        }
        else
        {
            return line;
        }
    }
}

public enum ParserType
{
    RegularExpression = 0,

    Manual = 1
}
