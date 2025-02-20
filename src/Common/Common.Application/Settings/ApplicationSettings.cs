namespace Common.Application.Settings;

public class ApplicationSettings
{
    public ApplicationSettings() => Secret = default!;

    public string Secret { get; private set; }
}