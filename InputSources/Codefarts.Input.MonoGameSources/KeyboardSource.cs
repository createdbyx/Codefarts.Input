using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class KeyboardSource : IInputSource
{
    private Keys[] keys = Enum.GetValues<Keys>();
    private string[] keyNames = Enum.GetNames(typeof(Keys));
    private PollingData[] results = new PollingData[Enum.GetValues<Keys>().Length];
    private string name;

    public KeyboardSource()
    {
        this.name = $"Keyboard";
        this.InitPollingData();
    }

    public KeyboardSource(PlayerIndex playerIndex)
    {
        this.PlayerIndex = playerIndex;
        this.name = $"Keyboard - {this.PlayerIndex}";
        this.InitPollingData();
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
            return keyNames;
        }
    }

    public IEnumerable<PollingData> Poll()
    {
        var state = Keyboard.GetState(this.PlayerIndex);

        for (var i = 0; i < this.keys.Length; i++)
        {
            var key = this.keys[i];
            this.results[i].Value = state[key] == KeyState.Down ? 1 : 0;
        }

        return results;
    }

    private void InitPollingData()
    {
        // cache the results in the array
        for (var i = 0; i < this.keys.Length; i++)
        {
            this.results[i] = new PollingData(this.name, this.keyNames[i], 0, DataType.Button);
        }
    }
}