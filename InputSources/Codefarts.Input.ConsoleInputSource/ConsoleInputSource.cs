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

    //  public event EventHandler<InputSourceArgs>? Changed;

    public IEnumerable<PollingData> Poll()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            return new[]
            {
              //  new PollingData(this.Name, key.Key.ToString(), 1),
                new PollingData(this.Name, key.Key.ToString(), 0)
                //         this.OnChanged(new InputSourceArgs(this.Name, key.Key.ToString(), 0, EventType.Other));
            };
        }

        return Enumerable.Empty<PollingData>();
        // protected virtual void OnChanged(InputSourceArgs e)
        // {
        //     this.Changed?.Invoke(this, e);
        // }
    }
}