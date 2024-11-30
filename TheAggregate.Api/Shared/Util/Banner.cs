namespace TheAggregate.Api.Shared.Util;

public static class Banner
{
    public static void Log(string message)
    {
        Console.WriteLine(WriteStars(message));
        Console.WriteLine($"[Timeline47] {message}");
        Console.WriteLine(WriteStars(message));
    }

    private static string WriteStars(string message)
    {
        var stars = "";
        var msgLength = ("[Timeline47] " + message).Length;
        for (var i = 0; i <= msgLength - 1; i++)
        {
            stars += "*";
        }

        return stars;
    }
}