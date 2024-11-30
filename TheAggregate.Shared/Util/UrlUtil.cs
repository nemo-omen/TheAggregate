namespace TheAggregate.Shared.Util;

public static class UrlUtil
{
    public static string NormalizeUrl(string url)
    {
        return url.Trim().TrimEnd('/').ToLowerInvariant();
    }
}