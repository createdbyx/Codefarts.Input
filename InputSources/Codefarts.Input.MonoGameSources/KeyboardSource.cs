using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class KeyboardSource : IInputSource
{
    private KeyboardState previousState;
    private Keys[] keys = Enum.GetValues<Keys>();

    public string Name
    {
        get
        {
            return "Keyboard";
        }
    }

    public string[] Sources
    {
        get
        {
            var keys = Enum.GetNames(typeof(Keys));
            return keys;
        }
    }

    //  public event EventHandler<InputSourceArgs>? Changed;

    public IEnumerable<PollingData> Poll()
    {
        var state = Keyboard.GetState();

        var results = new List<PollingData>();

        foreach (var key in keys)
        {
            // var noChange = state[key] == this.previousState[key];
            // if (noChange)
            // {
            //     var wasPressed = state[key] == KeyState.Down && this.previousState[key] == KeyState.Up;
            //     var inputValue = wasPressed ? 1 : -1; // 1 == pressed -1 = released
            //     results.Add(new PollingData(this.Name, key.ToString(), inputValue));
            //     continue;
            // }

            //  var wasPressed = state[key] == KeyState.Down && this.previousState[key] == KeyState.Up;
            //  var wasReleased = state[key] == KeyState.Up && this.previousState[key] == KeyState.Down;
            //  var inputValue = wasPressed ? 1 : (wasReleased ? 0 : 1); // 1 == pressed -1 = released
            //this.OnChanged(new InputSourceArgs(this.Name, key.ToString(), inputValue, EventType.Other));
            results.Add(new PollingData(this.Name, key.ToString(), state[key] == KeyState.Down ? 1 : 0));
        }

        // if (Console.KeyAvailable)
        // {
        //     var key = Console.ReadKey(true);
        //     this.OnChanged(new InputSourceArgs(this.Name, key.Key.ToString(), 0, EventType.Other));
        // }
        this.previousState = state;
        return results;
    }

    // protected virtual void OnChanged(InputSourceArgs e)
    // {
    //     this.Changed?.Invoke(this, e);
    // }
}