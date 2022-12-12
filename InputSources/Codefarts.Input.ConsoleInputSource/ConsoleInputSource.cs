using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;

namespace Codefarts.Input.ConsoleInputSource;

public class ConsoleInputSource : IInputSource
{
    public string Name
    {
        get
        {
            return "Console";
        }
    }

    public string[] Sources
    {
        get
        {
            var keys = Enum.GetNames(typeof(ConsoleKey));
            return keys;
        }
    }

    public IEnumerable<PollingData> Poll()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            return new[]
            {
                new PollingData(this.Name, key.Key.ToString(), 0)
            };
        }

        return Enumerable.Empty<PollingData>();
    }
}