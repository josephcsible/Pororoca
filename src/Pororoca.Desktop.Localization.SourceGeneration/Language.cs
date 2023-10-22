namespace Pororoca.Desktop.Localization.SourceGeneration;

public enum Language
{
    Portuguese,
    English,
    Russian,
    Italian
}

public static class LanguageExtensions
{
    public static string ToLCID(this Language lang) => lang switch
    {
        Language.Portuguese => "pt-br",
        Language.English => "en-gb",
        Language.Russian => "ru-ru",
        Language.Italian => "it-it",
        _ => "en-gb",
    };

    public static Language GetLanguageByLCID(string lcid) => lcid switch
    {
        "pt-br" => Language.Portuguese,
        "en-gb" => Language.English,
        "ru-ru" => Language.Russian,
        "it-it" => Language.Italian,
        _ => throw new KeyNotFoundException($"No language found for LCID '{lcid}'.")
    };
}