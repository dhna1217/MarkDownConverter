using HtmlTags;

namespace MarkDownConverter.Parser;

public static class Heading
{    
    public static string ConvertToHtml(int level, string line)
    {
        var convertedHtml = string.Empty;

        line = line.TrimStart();

        if(level == 1)
        {
            convertedHtml = $"<h1>{line}</h1>";
        }
        else if (level == 2)
        {
            convertedHtml = $"<h2>{line}</h2>";
        }
        else if (level == 3)
        {
            convertedHtml = $"<h3>{line}</h3>";
        }
        else if (level == 4)
        {
            convertedHtml = $"<h4>{line}</h4>";
        }
        else if (level == 5)
        {
            convertedHtml = $"<h5>{line}</h5>";
        }
        else if (level == 6)
        {
            convertedHtml = $"<h6>{line}</h6>";
        }

        return convertedHtml;
    }

    


    
}
