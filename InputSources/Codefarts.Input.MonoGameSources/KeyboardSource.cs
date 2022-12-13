using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class KeyboardSource : IInputSource
{
    private Keys[] keys = Enum.GetValues<Keys>();

    public KeyboardSource()
    {
    }

    public KeyboardSource(PlayerIndex playerIndex)
    {
        this.PlayerIndex = playerIndex;
    }

    public PlayerIndex PlayerIndex { get; set; } = PlayerIndex.One;

    public string Name
    {
        get
        {
            return $"Keyboard - {this.PlayerIndex}";
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
        var state = Keyboard.GetState(this.PlayerIndex);

        var results = new List<PollingData>();

        foreach (var key in keys)
        {
            results.Add(new PollingData(this.Name, key.ToString(), state[key] == KeyState.Down ? 1 : 0));
        }

        return results;
    }
}