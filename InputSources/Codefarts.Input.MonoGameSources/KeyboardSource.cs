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
    private List<PollingData> resultsToReturn = new List<PollingData>();
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

    public IEnumerable<PollingData> Poll(bool onlyStateChanges)
    {
        var state = Keyboard.GetState(this.PlayerIndex);

        this.resultsToReturn.Clear();
        for (var i = 0; i < this.keys.Length; i++)
        {
            var key = this.keys[i];
            var pollingData = this.results[i];
            var oldValue = pollingData.Value;
            var newValue = state[key] == KeyState.Down ? 1 : 0;
            // if state changed add to result
            if (!onlyStateChanges || (onlyStateChanges && oldValue != newValue))
            {
                pollingData.Value = newValue;
                this.resultsToReturn.Add(pollingData);
            }
        }

        return this.resultsToReturn;
    }

    private void InitPollingData()
    {
        // cache the results in the array
        for (var i = 0; i < this.keys.Length; i++)
        {
            this.results[i] = new PollingData(this.name, this.keyNames[i], 0, 0, 1, DataType.Button);
        }
    }
}