using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
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

    public IEnumerable<PollingData> Poll()
    {
        var state = Keyboard.GetState();

        var results = new List<PollingData>();

        foreach (var key in keys)
        {
            results.Add(new PollingData(this.Name, key.ToString(), state[key] == KeyState.Down ? 1 : 0));
        }

        this.previousState = state;
        return results;
    }
}