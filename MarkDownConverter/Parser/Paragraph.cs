namespace MarkDownConverter.Parser;

public static class Paragraph
{
    public static string ConvertToHtml(string lines)
    {
        return $"<p>{lines}</p>\n";
    }
}
