using Codefarts.Input.Interfaces;
using Codefarts.Input.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Codefarts.Input.MonoGameSources;

public class GamePadSource : IInputSource
{
    private string name;
    private PollingData[] results = new PollingData [22];

    public GamePadSource()
    {
        this.name = $"GamePad";
    }

    public GamePadSource(PlayerIndex playerIndex)    
    {
        this.PlayerIndex = playerIndex;
        this.name = $"GamePad - {this.PlayerIndex}";
    }

    public PlayerIndex PlayerIndex { get;  } = PlayerIndex.One;

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
            return new[]
            {
                "X",
                "Y",
                "A",
                "B",
                "BigButton",
                "Back",
                "Start",
                "LeftShoulder",
                "RightShoulder",
                "LeftStick",
                "RightStick",
                "LeftTrigger",
                "RightTrigger",
                "LeftThumbStickX",
                "LeftThumbStickY",
                "RightThumbStickX",
                "RightThumbStickY",
                "IsConnected",
                "Right",
                "Left",
                "Up",
                "Down"
            };
        }
    }

    public IEnumerable<PollingData> Poll()
    {
        var state = GamePad.GetState(this.PlayerIndex);

        // buttons
        results[0] = new PollingData(this.name, "X", state.Buttons.X == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[1] = new PollingData(this.name, "Y", state.Buttons.Y == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[2] = new PollingData(this.name, "A", state.Buttons.A == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[3] = new PollingData(this.name, "B", state.Buttons.B == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[4] = new PollingData(this.name, "BigButton", state.Buttons.BigButton == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[5] = new PollingData(this.name, "Back", state.Buttons.Back == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[6] = new PollingData(this.name, "Start", state.Buttons.Start == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[7] = new PollingData(this.name, "LeftShoulder", state.Buttons.LeftShoulder == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[8] = new PollingData(this.name, "RightShoulder", state.Buttons.RightShoulder == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[9] = new PollingData(this.name, "LeftStick", state.Buttons.LeftStick == ButtonState.Pressed ? 1 : 0, DataType.Value);
        results[10] = new PollingData(this.name, "RightStick", state.Buttons.RightStick == ButtonState.Pressed ? 1 : 0, DataType.Value);

        results[11] = new PollingData(this.name, "LeftTrigger", state.Triggers.Left, DataType.Value);
        results[12] = new PollingData(this.name, "RightTrigger", state.Triggers.Right, DataType.Value);

        results[13] = new PollingData(this.name, "LeftThumbStickX", state.ThumbSticks.Left.X, DataType.Value);
        results[14] = new PollingData(this.name, "LeftThumbStickY", state.ThumbSticks.Left.Y, DataType.Value);
        results[15] = new PollingData(this.name, "RightThumbStickX", state.ThumbSticks.Right.X, DataType.Value);
        results[16] = new PollingData(this.name, "RightThumbStickY", state.ThumbSticks.Right.Y, DataType.Value);
                
        results[17] = new PollingData(this.name, "IsConnected", state.IsConnected ? 1 : 0, DataType.Other);

        results[18] = new PollingData(this.name, "Right", state.DPad.Right == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[19] = new PollingData(this.name, "Left", state.DPad.Left == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[20] = new PollingData(this.name, "Up", state.DPad.Up == ButtonState.Pressed ? 1 : 0, DataType.Button);
        results[21] = new PollingData(this.name, "Down", state.DPad.Down == ButtonState.Pressed ? 1 : 0, DataType.Button);

        return results;
    }
}