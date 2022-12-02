using Codefarts.Input.Interfaces;

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

    public event EventHandler<InputSourceArgs>? Changed;

    public void Poll()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            this.OnChanged(new InputSourceArgs(this.Name, key.Key.ToString(), 0, EventType.Other));
        }
    }

    protected virtual void OnChanged(InputSourceArgs e)
    {
        this.Changed?.Invoke(this, e);
    }
}