using System.Text.RegularExpressions;

namespace Amadeus.Bot.Helper;

public static class StringHelper
{
    public static string StripHtml (this string input)
    {
        return Regex.Replace(input, "<[a-zA-Z/].*?>", string.Empty);
    }
}