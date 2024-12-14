using AngleSharp.Html.Parser;

namespace TheAggregate.Api.Shared.Util;

public static class HtmlUtils
{
    public static bool IsHtmlString(string fragment)
    {
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(fragment);
        var children = doc.Body?.Children;
        Banner.Log(children?.Length.ToString()?? "0");
        return children?.Any()?? false;
    }

    public static string CleanWhitespace(string input)
    {
        if(string.IsNullOrWhiteSpace(input)) return string.Empty;
        var current = 0;
        var output = new char[input.Length];
        var skipped = false;

        foreach (var c in input.ToCharArray())
        {
            if (char.IsWhiteSpace(c))
            {
                if (!skipped)
                {
                    if(current > 0) output[current++] = ' ';
                    skipped = true;
                }
            }
            else
            {
                skipped = false;
                output[current++] = c;
            }
        }
        return new string(output, 0, current);
    }

    public static string GetPlainTextFromHtml(string html)
    {
        if(!IsHtmlString(html)) return html;
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(html);
        return doc.DocumentElement.TextContent;
    }

    public static string WrapPlainText(string text)
    {
        var paras = text.Split('\n');
        List<string> htmlParas = [];
        foreach (var para in paras)
        {
            var pString = $"<p>{para}</p>";
            htmlParas.Add(pString);
        }
        return string.Join("\n", htmlParas);
    }
}