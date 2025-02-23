using System.Text.RegularExpressions;

namespace Common.Application.Extensions;

public class MessageBrokerExtensions
{
    public static string GetQueueName<T>()
    {
        var value = Regex.Replace(typeof(T).Name, "([A-Z])", "-$1").ToLowerInvariant();
        return value.Substring(1, value.Length - 1);
    }
}