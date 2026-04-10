using System.Text.RegularExpressions;

namespace EmirOtomotiv.Core.Application.Common.Helpers;

public static class SlugHelper
{
    private static readonly (string From, string To)[] TurkishMap =
    [
        ("ş", "s"), ("Ş", "s"),
        ("ğ", "g"), ("Ğ", "g"),
        ("ü", "u"), ("Ü", "u"),
        ("ö", "o"), ("Ö", "o"),
        ("ı", "i"), ("İ", "i"),
        ("ç", "c"), ("Ç", "c"),
    ];

    public static string Generate(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        string slug = input.ToLowerInvariant();

        foreach (var (from, to) in TurkishMap)
            slug = slug.Replace(from, to);

        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"[\s-]+", "-");
        slug = slug.Trim('-');

        return slug;
    }
}
