using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class KeyboardSource : IInputSource
{
    private Keys[] keys = Enum.GetValues<Keys>();
    private string[] keyNames;
    private PollingData[] results = new PollingData[Enum.GetValues<Keys>().Length];
    private string name;

    public KeyboardSource()
    {
        this.keyNames = this.keys.Select(x => x.ToString()).ToArray();
        this.name = $"Keyboard";
    }

    public KeyboardSource(PlayerIndex playerIndex)  
    {
        this.PlayerIndex = playerIndex;
        this.name = $"Keyboard - {this.PlayerIndex}";
    }

    public PlayerIndex PlayerIndex { get; } = PlayerIndex.One;

    public string Name
    {
        get
        {
            return this.name;
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

        for (var i = 0; i < this.keys.Length; i++)
        {
            var key = this.keys[i];
            this.results[i] = new PollingData(this.name, this.keyNames[i], state[key] == KeyState.Down ? 1 : 0, DataType.Button);
        }

        return results;
    }
}