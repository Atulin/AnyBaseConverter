using System.Text;

namespace AnyBaseConverter;

public static class AnyBaseConverter
{
    private static ReadOnlySpan<char> Chars => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".AsSpan();
    private static int Max => Chars.Length - 1;
    
    /// <summary>
    /// Convert value in (almost) any base to a base 10 value
    /// </summary>
    /// <param name="source">Base <paramref name="b"/> value to convert</param>
    /// <param name="b">Base of the value being converted</param>
    /// <returns>The value converted to base 10</returns>
    /// <exception cref="ArgumentException">Thrown when the base value <paramref name="b"/> exceeds the characters in the internal list</exception>
    public static ulong AnyToDecimal(this string source, int b)
    {
        if (b > Max)
        {
            throw new ArgumentException($"Base cannot be higher than {Max}", nameof(b));
        }
        
        ulong result = 0;
        var sourceSpan = source.AsSpan();
        var len = source.Length;
        
        for (var i = len; i > 0; i--)
        {
            var val = (ulong)Chars.IndexOf(sourceSpan[i - 1]);
            var multiplier = (ulong)Math.Pow(b, len - i);
            result += val * multiplier;
        }

        return result;
    }

    /// <summary>
    /// Convert a base 10 value to (almost) any other base
    /// </summary>
    /// <param name="source">Base 10 value to be converted</param>
    /// <param name="b">Base to convert to</param>
    /// <returns>The value converted to base <paramref name="b"/></returns>
    /// <exception cref="ArgumentException">Thrown when the base value <paramref name="b"/> exceeds the characters in the internal list</exception>
    public static string DecimalToAny(this ulong source, int b)
    {
        if (b > Max)
        {
            throw new ArgumentException($"Base cannot be higher than {Max}", nameof(b));
        }

        var num = source;
        var sb = new StringBuilder();

        while (num > 0)
        {
            var rest = (int)(num % (ulong)b);

            sb.Insert(0, Chars[rest]);
            
            num /= (ulong)b;
        }
        
        return sb.ToString();
    }
}