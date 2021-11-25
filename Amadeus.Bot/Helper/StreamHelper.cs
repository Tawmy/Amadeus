namespace Amadeus.Bot.Helper;

public static class StreamHelper
{
    // https://stackoverflow.com/questions/1879395/how-do-i-generate-a-stream-from-a-string
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}