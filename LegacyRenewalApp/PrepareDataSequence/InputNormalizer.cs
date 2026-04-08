namespace LegacyRenewalApp;

public static class InputNormalizer
{
    public static string Normalize(string value)
        => value.Trim().ToUpperInvariant();
}