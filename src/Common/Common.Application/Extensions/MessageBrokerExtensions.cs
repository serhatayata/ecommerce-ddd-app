using System.Text.RegularExpressions;

namespace Common.Application.Extensions;

public class MessageBrokerExtensions
{
    public static string GetQueueName<T>()
    {
        var filtered = Regex.Replace(typeof(T).Name, "([A-Z])", "-$1").ToLowerInvariant();
        return filtered.Substring(1, filtered.Length - 1);
    }

    public static string GetQueueName(Type type)
    {
        var filtered = Regex.Replace(type.Name, "([A-Z])", "-$1").ToLowerInvariant();
        return filtered.Substring(1, filtered.Length - 1);
    }

    public static string GetExchangeName<T>()
    {
        var filtered = Regex.Replace(typeof(T).Name, "([A-Z])", "-$1").ToLowerInvariant();
        return filtered.Substring(1, filtered.Length - 1);
    }
}